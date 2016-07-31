using System;

namespace Library10.Common.Entities
{
    public class ApplicationPurchase
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string ANID2 { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}