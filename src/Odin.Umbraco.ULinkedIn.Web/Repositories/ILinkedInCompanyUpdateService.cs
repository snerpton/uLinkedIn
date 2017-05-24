using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using website.Odin.Umbraco.ULinkedIn.Web.Models;

namespace Odin.Umbraco.ULinkedIn.Web.Repositories
{
    public interface ILinkedInCompanyUpdateService
    {
        Value FindById(string updateKey);

        LinkedInCompanyUpdates ListAll(int count);

        LinkedInCompanyUpdates ListAllJobPostings(int count);

        LinkedInCompanyUpdates ListAllNewProducts(int count);

        LinkedInCompanyUpdates ListAllStatusUpdates(int count);
    }



    public class LinkedInStatusUpdate
    {
        /// <summary>
        /// Share author. From property updateContent.company.name
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// Image thumbnail. From property updateContent.companyStatusUpdate.share.content.thumbnailUrl
        /// </summary>
        public string ImageUrlThumbnail { get; set; }
        /// <summary>
        /// Share text. From property updateContent.companyStatusUpdate.share.comment
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Share timestamp. From property updateContent.companyStatusUpdate.share.timestamp
        /// </summary>
        public string Timestamp { get; set; }
    }
}
