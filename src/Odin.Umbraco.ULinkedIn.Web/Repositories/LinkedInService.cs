using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using website.Odin.Umbraco.ULinkedIn.Web.Models;

namespace Odin.Umbraco.ULinkedIn.Web.Repositories
{

    public class LinkedInCompanyUpdateService : ILinkedInCompanyUpdateService
    {
        private readonly ILinkedInCompanyUpdateRepository _companyUpdateRepo;

        //public LinkedInCompanyUpdateService(string accessToken, string companyId)
        //{
        //    if (string.IsNullOrWhiteSpace(accessToken))
        //        throw new ArgumentException("accessToken is null or whitespace");

        //    if (string.IsNullOrWhiteSpace(companyId))
        //        throw new ArgumentException("companyId is null or whitespace");
            
        //    _companyUpdateRepo = new LinkedInCompanyUpdateRepositoryCached(accessToken, companyId);
        //}

        public LinkedInCompanyUpdateService(ILinkedInCompanyUpdateRepository companyUpdateRepository)
        {
            _companyUpdateRepo = companyUpdateRepository ?? throw new ArgumentNullException("companyUpdateRepository is null");
        }
        
        public Value FindById(string updateKey)
        {
            return _companyUpdateRepo.FindById(updateKey);
        }

        public LinkedInCompanyUpdates ListAll(int count)
        {
            return _companyUpdateRepo.ListAll(count);
        }

        public LinkedInCompanyUpdates ListAllJobPostings(int count)
        {
            return _companyUpdateRepo.ListAllJobPostings(count);
        }

        public LinkedInCompanyUpdates ListAllNewProducts(int count)
        {
            return _companyUpdateRepo.ListAllNewProducts(count);
        }

        public LinkedInCompanyUpdates ListAllStatusUpdates(int count)
        {
            return _companyUpdateRepo.ListAllStatusUpdates(count);
        }
    }
}
