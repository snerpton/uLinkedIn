using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using website.Odin.Umbraco.ULinkedIn.Web.Models;

namespace Odin.Umbraco.ULinkedIn.Web.Repositories
{
    public class LinkedInService : ILinkedInService
    {
        private readonly ILinkedInCompanyUpdateRepository _linkedInCompanyUpdateRepository;
        private static readonly object CacheLockObject = new object();



        public LinkedInService(ILinkedInCompanyUpdateRepository linkedInCompanyUpdateRepository)
        {
            _linkedInCompanyUpdateRepository = linkedInCompanyUpdateRepository;
        }
        
        public Value CompanyUpdateFindById(string updateKey)
        {
            return _linkedInCompanyUpdateRepository.FindById(updateKey);
        }

        public LinkedInCompanyUpdates CompanyUpdateListAll(int count)
        {
            return _linkedInCompanyUpdateRepository.ListAll(count);
        }

        public LinkedInCompanyUpdates CompanyUpdateListAllJobPostings(int count)
        {
            return _linkedInCompanyUpdateRepository.ListAllJobPostings(count);
        }

        public LinkedInCompanyUpdates CompanyUpdateListAllNewProducts(int count)
        {
            return _linkedInCompanyUpdateRepository.ListAllNewProducts(count);
        }

        public LinkedInCompanyUpdates CompanyUpdateListAllStatusUpdates(int count)
        {
            return _linkedInCompanyUpdateRepository.ListAllStatusUpdates(count);
        }
    }
}
