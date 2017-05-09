namespace website.Odin.Umbraco.ULinkedIn.Web.Models
{
    //General
    public class LinkedInCompanyUpdates
    {
        public int _total { get; set; }
        public Value[] values { get; set; }
    }
    public class Value
    {
        public bool isCommentable { get; set; }
        public bool isLikable { get; set; }
        public bool isLiked { get; set; }
        public int numLikes { get; set; }
        public long timestamp { get; set; }
        public Updatecomments updateComments { get; set; }
        public Updatecontent updateContent { get; set; }
        public string updateKey { get; set; }
        public string updateType { get; set; }
        public Likes likes { get; set; }
    }
    public class Updatecontent
    {
        public Company company { get; set; }
        public Companystatusupdate companyStatusUpdate { get; set; }
        public Companyjobupdate companyJobUpdate { get; set; }
    }
    public class Company
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    //Status Update
    public class Updatecomments
    {
        public int _total { get; set; }
    }
    public class Companystatusupdate
    {
        public Share share { get; set; }
    }
    public class Share
    {
        public string comment { get; set; }
        public string id { get; set; }
        public Source source { get; set; }
        public long timestamp { get; set; }
        public Visibility visibility { get; set; }
    }
    public class Source
    {
        public Serviceprovider serviceProvider { get; set; }
        public string serviceProviderShareId { get; set; }
    }
    public class Serviceprovider
    {
        public string name { get; set; }
    }
    public class Visibility
    {
        public string code { get; set; }
    }
    public class Likes
    {
        public int _total { get; set; }
        public Value1[] values { get; set; }
    }
    public class Value1
    {
        public Person person { get; set; }
    }
    public class Person
    {
        public string firstName { get; set; }
        public string id { get; set; }
        public string lastName { get; set; }
    }


    //Job posting
    public class Companyjobupdate
    {
        public Action action { get; set; }
        public Job job { get; set; }
    }
    public class Action
    {
        public string code { get; set; }
    }
    public class Job
    {
        public Company1 company { get; set; }
        public string description { get; set; }
        public int id { get; set; }
        public string locationDescription { get; set; }
        public Position position { get; set; }
        public Sitejobrequest siteJobRequest { get; set; }
    }
    public class Company1
    {
        public int id { get; set; }
        public string name { get; set; }
    }
    public class Position
    {
        public string title { get; set; }
    }
    public class Sitejobrequest
    {
        public string url { get; set; }
    }

    //new product
    //Not implemented


}



