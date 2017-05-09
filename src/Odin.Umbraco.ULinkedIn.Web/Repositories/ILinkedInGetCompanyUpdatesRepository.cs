using website.Odin.Umbraco.ULinkedIn.Web.Models;

namespace website.Odin.Umbraco.ULinkedIn.Web.Repositories
{
    public interface ILinkedInGetCompanyUpdatesRepository
    {
        LinkedInCompanyUpdates GetAll();

        LinkedInCompanyUpdates GetJobPostings();

        LinkedInCompanyUpdates GetNewProduct();

        LinkedInCompanyUpdates GetStatusUpdate();

    }
}