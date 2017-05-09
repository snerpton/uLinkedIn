using website.Odin.Umbraco.ULinkedIn.PropertyEditor.Models;

namespace website.Odin.Umbraco.ULinkedIn.PropertyEditor.ViewModels
{
    public class RequestAnAuthorizationCodeCallbackOKViewModel
    {
        /// <summary>
        /// Scope ID of the Umbraco property editor that initiated the request for an authorization code.
        /// </summary>
        public string ScopeId;
        /// <summary>
        /// Value stored in the Umbraco AngularJs model.value
        /// </summary>
        public ULinkedInModel ULinkedInProvertyModel;
    }
}