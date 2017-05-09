using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using Odin.Umbraco.ULinkedIn.PropertyEditor;
using Odin.Umbraco.ULinkedIn.PropertyEditor.Models;
using Odin.Umbraco.ULinkedIn.PropertyEditor.Helpers;
using Odin.Umbraco.ULinkedIn.PropertyEditor.Extensions;
using website.Odin.Umbraco.ULinkedIn.PropertyEditor.Helpers;
using Newtonsoft.Json;
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
            if (
                    string.IsNullOrWhiteSpace("clientId")
                    || string.IsNullOrWhiteSpace("clientSecret")
                    || string.IsNullOrWhiteSpace("redirectUri")
                    || string.IsNullOrWhiteSpace("state")
                    || string.IsNullOrWhiteSpace("scope")
                )
            {
                //ToDo: Style RequestAnAuthorizationCode template
                return View("RequestAnAuthorizationCode");
            }
            else
            {
                //RequestOptions requestOptions;
                //if (
                //        Request.QueryString.AllKeys.Contains("o")
                //        && !string.IsNullOrWhiteSpace(Request.QueryString["o"])
                //        && RequestOptions.TryParse(Request.QueryString["o"], out requestOptions)
                //        && requestOptions != null
                //    )
                //{
                //    //Do something if we need to preserve the request options i.e. get clientID / secret
                //}
                RequestOptions requestOptions;
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
                
                return Redirect(OAuth2.CreateRedirect(uLinkedInHelper.GenerateLinkedInOAuth2Provider(), Constants.LocalRedirectUri));
            }
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
            OAuth2AuthenticateResponse response = OAuth2.AuthenticateByCode(uLinkedInHelper.GenerateLinkedInOAuth2Provider(), Constants.LocalRedirectUri, Request.QueryString["code"]);
            //ToDo: 
            // 1) handle response by parsing into OAuth2 object, and storing
            // 2) refresh token
            //return Redirect("http://bbc.co.uk");




            string userInfoJson = OAuth2.GetUserInfo(uLinkedInHelper.GenerateLinkedInOAuth2Provider(), response.AccessToken, new Dictionary<string, string> { {"format", "json" } });
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
                        //ClientId = "",
                        //ClientSecret = "",
                        Expires = response.Expires
                    }
            });
        }
    }
}