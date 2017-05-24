using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using website.Odin.Umbraco.ULinkedIn.Web.Models;

namespace Odin.Umbraco.ULinkedIn.Web.Repositories
{
    public class LinkedInCompanyUpdateRepositoryCached : ILinkedInCompanyUpdateRepository
    {
        private readonly LinkedInCompanyUpdateRepository _linkedInCompanyUpdateRepo;
        private static readonly object CacheLockObject = new object();
        private readonly int cacheTimeInSeconds = 30;

        public LinkedInCompanyUpdateRepositoryCached(string accessToken, string companyId)
        {
            _linkedInCompanyUpdateRepo = new LinkedInCompanyUpdateRepository(accessToken, companyId);
        }

        public Value FindById(string updateKey)
        {
            string cacheKey = "LinkedInCompanyUpdateRepositoryCached-FindById-" + updateKey;
            var result = HttpRuntime.Cache[cacheKey] as Value;
            if (result == null)
            {
                lock (CacheLockObject)
                {
                    result = HttpRuntime.Cache[cacheKey] as Value;
                    if (result == null)
                    {
                        result = _linkedInCompanyUpdateRepo.FindById(updateKey);
                        HttpRuntime.Cache.Insert(cacheKey, result, null,
                            DateTime.Now.AddSeconds(cacheTimeInSeconds), TimeSpan.Zero);
                    }
                }
            }
            return result;
        }

        public LinkedInCompanyUpdates ListAll(int count)
        {
            string cacheKey = "LinkedInCompanyUpdateRepositoryCached-ListAll-" + count;
            var result = HttpRuntime.Cache[cacheKey] as LinkedInCompanyUpdates;
            if (result == null)
            {
                lock (CacheLockObject)
                {
                    result = HttpRuntime.Cache[cacheKey] as LinkedInCompanyUpdates;
                    if (result == null)
                    {
                        result = _linkedInCompanyUpdateRepo.ListAll(count);
                        HttpRuntime.Cache.Insert(cacheKey, result, null,
                            DateTime.Now.AddSeconds(cacheTimeInSeconds), TimeSpan.Zero);
                    }
                }
            }
            return result;
        }

        public LinkedInCompanyUpdates ListAllJobPostings(int count)
        {
            string cacheKey = "LinkedInCompanyUpdateRepositoryCached-ListAllJobPostings-" + count;
            var result = HttpRuntime.Cache[cacheKey] as LinkedInCompanyUpdates;
            if (result == null)
            {
                lock (CacheLockObject)
                {
                    result = HttpRuntime.Cache[cacheKey] as LinkedInCompanyUpdates;
                    if (result == null)
                    {
                        result = _linkedInCompanyUpdateRepo.ListAllJobPostings(count);
                        HttpRuntime.Cache.Insert(cacheKey, result, null,
                            DateTime.Now.AddSeconds(cacheTimeInSeconds), TimeSpan.Zero);
                    }
                }
            }
            return result;
        }

        public LinkedInCompanyUpdates ListAllNewProducts(int count)
        {
            string cacheKey = "LinkedInCompanyUpdateRepositoryCached-ListAllNewProducts-" + count;
            var result = HttpRuntime.Cache[cacheKey] as LinkedInCompanyUpdates;
            if (result == null)
            {
                lock (CacheLockObject)
                {
                    result = HttpRuntime.Cache[cacheKey] as LinkedInCompanyUpdates;
                    if (result == null)
                    {
                        result = _linkedInCompanyUpdateRepo.ListAllNewProducts(count);
                        HttpRuntime.Cache.Insert(cacheKey, result, null,
                            DateTime.Now.AddSeconds(cacheTimeInSeconds), TimeSpan.Zero);
                    }
                }
            }
            return result;
        }

        public LinkedInCompanyUpdates ListAllStatusUpdates(int count)
        {
            string cacheKey = "LinkedInCompanyUpdateRepositoryCached-ListAllStatusUpdates-" + count;
            var result = HttpRuntime.Cache[cacheKey] as LinkedInCompanyUpdates;
            if (result == null)
            {
                lock (CacheLockObject)
                {
                    result = HttpRuntime.Cache[cacheKey] as LinkedInCompanyUpdates;
                    if (result == null)
                    {
                        result = _linkedInCompanyUpdateRepo.ListAllStatusUpdates(count);
                        HttpRuntime.Cache.Insert(cacheKey, result, null,
                            DateTime.Now.AddSeconds(cacheTimeInSeconds), TimeSpan.Zero);
                    }
                }
            }
            return result;
        }
    }
}
