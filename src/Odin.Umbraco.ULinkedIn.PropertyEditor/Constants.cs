namespace Odin.Umbraco.ULinkedIn.PropertyEditor
{
    internal class Constants
    {
        /*
        internal static string ConsumerKey
        {
            get { return "O0zPY+FTZb9xhisPypYQFAGOGK/j7X4o".Decrypt(); }
        }

        internal static string ConsumerSecret
        {
            get { return "Z5vKVOyJuEDX3h9NBwM7R7Hik4aVUy5F/r6AaT2fy5YvunjsTEbZWQ==".Decrypt(); }
        }

        internal const string RequestTokenUrl = "https://api.twitter.com/oauth/request_token";
        internal const string AuthorizeUrl = "https://api.twitter.com/oauth/authorize";
        internal const string AccessTokenUrl = "https://api.twitter.com/oauth/access_token";

        internal const string VerifyCredentialsUrl = "https://api.twitter.com/1.1/account/verify_credentials.json";
        */


        /// <summary>
        /// LinkedIn URL to request an authorization code
        /// </summary>
        internal const string LinkedInRequestAuthorizationBaseUrl = "https://www.linkedin.com/oauth/v2/authorization";
        /// <summary>
        /// LinkedIn URL to exchange an authorization code for an access token
        /// </summary>
        internal const string LinkedInRequestAccessTokenBaseUrl = "https://www.linkedin.com/oauth/v2/accessToken";
        /// <summary>
        /// LinkedIn API key
        /// </summary>
        internal const string LinkedInClientId = "86cswku1qfb2gj";
        /// <summary>
        /// LinkedIn secret key
        /// </summary>
        internal const string LinkedInClientSecret = "jWtSscFFo7GuRL56";
        /// <summary>
        /// URL that LinkedIn redirects following an attempt to authorize. LinkedIn calls this 'redirectUri' 
        /// </summary>
        internal const string LocalRedirectUri = "http://linkedin-test.local/umbraco/backoffice/auth/ulinkedin/RequestAnAuthorizationCodeCallback/";
        /// <summary>
        /// A value used to test for possible CSRF attacks.
        /// </summary>
        internal const string LinkedInState = "c8C1TtZ5PV1bbN6Y7hi9Md";
        /// <summary>
        /// A URL-encoded, space delimited list of member permissions your 
        /// application is requesting on behalf of the user.  If you do not 
        /// specify a scope in your call, we will fall back to using the 
        /// default member permissions you defined in your application 
        /// configuration.
        /// </summary>
        internal const string LinkedInScope = "r_basicprofile rw_company_admin";
        /// <summary>
        /// Prefix for session data key. Used to store Umbraco data which is 
        /// required to persist from beginning of LinkedIn authorization 
        /// process to saving authorization token.
        /// </summary>
        internal const string LocalSessionPrefix = "uLinkedIn-";




        internal const string LinkedInUserInfoUrl = "https://api.linkedin.com/v1/people/~";

    }
}
