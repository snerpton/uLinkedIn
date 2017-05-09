using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using global::Umbraco.Web;
using website.Odin.Umbraco.ULinkedIn.PropertyEditor.Helpers;

namespace Odin.Umbraco.ULinkedIn.PropertyEditor.Helpers
{ 
    internal static class uLinkedInHelper
    {
        /*
        /// <summary>
        /// Gets the pre value options by alias.
        /// </summary>
		/// <param name="contentTypeAlias">The content type alias.</param>
		/// <param name="propertyAlias">The property alias.</param>
        /// <returns></returns>
		internal static IDictionary<string, string> GetPreValueOptionsByAlias(string contentTypeAlias,
			string propertyAlias)
        {
	        var services = UmbracoContext.Current.Application.Services;
	        var contentType = services.ContentTypeService.GetContentType(contentTypeAlias);
			var property = contentType.PropertyTypes.SingleOrDefault(x => x.Alias == propertyAlias);
	        if (property == null)
		        return null;

	        var preValues = services.DataTypeService.GetPreValuesCollectionByDataTypeId(property.DataTypeDefinitionId);
	        if (!preValues.IsDictionaryBased)
		        return null;

	        return preValues.PreValuesAsDictionary.ToDictionary(x => x.Key, x => x.Value.Value);
        }
        */
        
        internal static OAuth2Provider GenerateLinkedInOAuth2Provider()
        {
            return new OAuth2Provider()
            {
                ClientId = Constants.LinkedInClientId,
                ClientSecret = Constants.LinkedInClientSecret,
                AuthUri = Constants.LinkedInRequestAuthorizationBaseUrl,
                AccessTokenUri = Constants.LinkedInRequestAccessTokenBaseUrl,
                UserInfoUri = Constants.LinkedInUserInfoUrl,
                Scope = Constants.LinkedInScope,
                State = Constants.LinkedInState,
                Offline = false
            };
        }






    }
}