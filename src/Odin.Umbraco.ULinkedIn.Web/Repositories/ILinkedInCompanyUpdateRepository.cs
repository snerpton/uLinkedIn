using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using website.Odin.Umbraco.ULinkedIn.Web.Models;

namespace Odin.Umbraco.ULinkedIn.Web.Repositories
{
    public interface ILinkedInCompanyUpdateRepository
    {
        Value FindById(string updateKey);

        LinkedInCompanyUpdates ListAll(int count);

        LinkedInCompanyUpdates ListAllJobPostings(int count);

        LinkedInCompanyUpdates ListAllNewProducts(int count);

        LinkedInCompanyUpdates ListAllStatusUpdates(int count);
    }
}
