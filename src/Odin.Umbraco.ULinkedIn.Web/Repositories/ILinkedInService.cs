using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using website.Odin.Umbraco.ULinkedIn.Web.Models;

namespace Odin.Umbraco.ULinkedIn.Web.Repositories
{
    public interface ILinkedInService
    {
        Value CompanyUpdateFindById(string updateKey);

        LinkedInCompanyUpdates CompanyUpdateListAll(int count);

        LinkedInCompanyUpdates CompanyUpdateListAllJobPostings(int count);

        LinkedInCompanyUpdates CompanyUpdateListAllNewProducts(int count);

        LinkedInCompanyUpdates CompanyUpdateListAllStatusUpdates(int count);
    }
}
