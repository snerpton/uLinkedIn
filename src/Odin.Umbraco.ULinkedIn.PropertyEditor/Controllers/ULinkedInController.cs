using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using Odin.Umbraco.ULinkedIn.PropertyEditor;
using Odin.Umbraco.ULinkedIn.PropertyEditor.Models;
using Odin.Umbraco.ULinkedIn.PropertyEditor.Helpers;
using Odin.Umbraco.ULinkedIn.PropertyEditor.Extensions;
using website.Odin.Umbraco.ULinkedIn.PropertyEditor.Helpers;
using website.Odin.Umbraco.ULinkedIn.PropertyEditor.ViewModels;
using website.Odin.Umbraco.ULinkedIn.PropertyEditor.Models;

namespace website.Odin.Umbraco.ULinkedIn.PropertyEditor.Controllers
{

    public class ULinkedInController : UmbracoAuthorizedController  //RenderMvcController
    {
        /// <summary>
        /// Session cache key for session variable that contains AngularJs Umbraco property editor's scope ID
        /// </summary>
        private string GetCacheKeyRequestOptions()
        {
            return Constants.LocalSessionPrefix + User.Identity.Name + "-RequestOptions";
        }
        
        public ActionResult RequestAnAuthorizationCode()
        {
            RequestOptions requestOptions = null;
            if (
                    Request.QueryString.AllKeys.Contains("o")
                    && !string.IsNullOrWhiteSpace(Request.QueryString["o"])
                    && RequestOptions.TryParse(Request.QueryString["o"], out requestOptions)
                    && requestOptions != null
                )
            {
                Session[GetCacheKeyRequestOptions()] = requestOptions;
            }
            else
            {
                Session[GetCacheKeyRequestOptions()] = new RequestOptions();
            }
            
            OAuth2Provider oAuth2Provider = null;
            if (CreateOAuth2ProviderFromRequestOptions(requestOptions, out oAuth2Provider) == false)
            {
                //throw new System.Exception(requestOptions.ToString());
                //ToDo: Style RequestAnAuthorizationCode template
                return View("RequestAnAuthorizationCodeError", new RequestAnAuthorizationCodeErrorViewModel() { HumanMessage = "Another fucking error: " + requestOptions.ContentTypeAlias + " : " + requestOptions.PropertyAlias + " : " + requestOptions.ScopeId });
            }
            
            return Redirect(OAuth2.CreateRedirect(oAuth2Provider, Constants.LocalRedirectUri));
        }

        public ActionResult RequestAnAuthorizationCodeCallback()
        {
            RequestOptions requestOptions = (RequestOptions)Session[GetCacheKeyRequestOptions()];

            //Check state. If the returned state value does not match the sent 
            //state value, then there may have been a CSRF attack.
            string state = Request.QueryString["state"];  //A value used to test for possible CSRF attacks.
            if (string.IsNullOrWhiteSpace(state) == true || state != Constants.LinkedInState)
            {
                Response.StatusCode = 401;
                Response.StatusDescription = "Unauthorised";
                Response.SuppressFormsAuthenticationRedirect = true;
                Response.Headers.Add("WWW-Authenticate", "ERROR. State does not match the state value that was sent. You are likely to be the victim of a CSRF attack. See LinkedIn documentation.");

                return new HttpUnauthorizedResult();
            }
            
            //Test if LinkedIn has detected an error with the request for an authorization code.
            if (string.IsNullOrWhiteSpace(Request.QueryString["error"]) == false)
            {
                return View("RequestAnAuthorizationCodeCallbackError", new RequestAnAuthorizationCodeCallbackErrorViewModel() {
                    Error = true,
                    ErrorCode = Request.QueryString["error"],
                    ErrorDescription = Request.QueryString["error_description"],
                    HumanMessage = Request.QueryString["error_description"]
                });
            }

            //Test if the code LinkedIn returned has a sensible value.
            string code = Request.QueryString["code"];
            if (string.IsNullOrWhiteSpace(Request.QueryString["code"]) == true)
            {
                return View("RequestAnAuthorizationCodeCallbackError", new RequestAnAuthorizationCodeCallbackErrorViewModel()
                {
                    Error = true,
                    ErrorCode = Request.QueryString["error"],
                    ErrorDescription = Request.QueryString["error_description"],
                    HumanMessage = "It looks as though LinkedIn had an issue authenticating. Please try again later. If the problem persists, please report the problem to the site administrator."
                });
            }

            //LinkedIn returned an 'Authorization Code' that we can attempt to exchange for an 'Authorization Code'.
            OAuth2Provider oAuth2Provider = null;
            if (CreateOAuth2ProviderFromRequestOptions(requestOptions, out oAuth2Provider) == false)
            {
                return View("RequestAnAuthorizationCodeCallbackError", new RequestAnAuthorizationCodeCallbackErrorViewModel()
                {
                    Error = true,
                    ErrorCode = string.Empty,
                    ErrorDescription = string.Empty,
                    HumanMessage = "Error creating the oAuth2Provider. Please try again later. If the problem persists, please report the problem to the site administrator."
                });
            }
            OAuth2AuthenticateResponse response = OAuth2.AuthenticateByCode(oAuth2Provider, Constants.LocalRedirectUri, Request.QueryString["code"]);
            
            string userInfoJson = OAuth2.GetUserInfo(oAuth2Provider, response.AccessToken, new Dictionary<string, string> { {"format", "json" } });
            LinkedInBasicProfile linkedInBasicProfile = null;
            if (string.IsNullOrWhiteSpace(userInfoJson) == false)
            {
                linkedInBasicProfile = userInfoJson.DeserializeJsonTo<LinkedInBasicProfile>();
            }
            
            return View("RequestAnAuthorizationCodeCallbackOK"
                , new RequestAnAuthorizationCodeCallbackOKViewModel() {
                    ScopeId = requestOptions.ScopeId,
                    ULinkedInProvertyModel = new ULinkedInModel() {
                        AccessToken = response.AccessToken,
                        AuthorizedUser = $"{linkedInBasicProfile.FirstName} {linkedInBasicProfile.LastName}",
                        ClientId = "",
                        ClientSecret = "",
                        Expires = response.Expires
                    }
            });
        }

        private bool CreateOAuth2ProviderFromRequestOptions(RequestOptions requestOptions, out OAuth2Provider oAuth2Provider)
        {
            oAuth2Provider = null;

            //Check request options are valid
            if (
                    requestOptions == null
                    || string.IsNullOrWhiteSpace(requestOptions.ContentTypeAlias)
                    || string.IsNullOrWhiteSpace(requestOptions.PropertyAlias)
                    || string.IsNullOrWhiteSpace(requestOptions.ScopeId)
                    //|| string.IsNullOrWhiteSpace(requestOptions.Callback)
                )
                return false;

            //Get prevalues from data-type and attempt to create oAuth2Provider
            IDictionary<string, string> propertyEditorPreValueOptions = uLinkedInHelper.GetPreValueOptionsByAlias(requestOptions.ContentTypeAlias, requestOptions.PropertyAlias);
            if (string.IsNullOrWhiteSpace(propertyEditorPreValueOptions["clientId"]) || string.IsNullOrWhiteSpace(propertyEditorPreValueOptions["clientSecret"]))
                return false;

            oAuth2Provider = uLinkedInHelper.GenerateLinkedInOAuth2Provider(propertyEditorPreValueOptions["clientId"], propertyEditorPreValueOptions["clientSecret"]);

            //Check oAuth2Provider id valid
            if (
                string.IsNullOrWhiteSpace(oAuth2Provider.ClientId)
                || string.IsNullOrWhiteSpace(oAuth2Provider.ClientSecret)
                || string.IsNullOrWhiteSpace(oAuth2Provider.AuthUri)
                || string.IsNullOrWhiteSpace(oAuth2Provider.State)
                || string.IsNullOrWhiteSpace(oAuth2Provider.Scope)
                )
            {
                return false;
            }

            return true;
        }
    }
}