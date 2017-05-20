using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using website.Odin.Umbraco.ULinkedIn.Web.Models;

namespace Odin.Umbraco.ULinkedIn.Web.Repositories
{
    public class LinkedInService : ILinkedInService
    {
        private readonly ILinkedInCompanyUpdateRepository _linkedInCompanyUpdateRepository;

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



        //private void MyMethod()
        //{
        //    Debug.Print("CachedAlbumRepository:GetTopSellingAlbums");
        //    string cacheKey = "TopSellingAlbums-" + count;
        //    var result = HttpRuntime.Cache[cacheKey] as List<Album>;
        //    if (result == null)
        //    {
        //        lock (CacheLockObject)
        //        {
        //            result = HttpRuntime.Cache[cacheKey] as List<Album>;
        //            if (result == null)
        //            {
        //                result = _albumRepository.GetTopSellingAlbums(count).ToList();
        //                HttpRuntime.Cache.Insert(cacheKey, result, null,
        //                    DateTime.Now.AddSeconds(60), TimeSpan.Zero);
        //            }
        //        }
        //    }
        //    return result;
        //}
    }
}
