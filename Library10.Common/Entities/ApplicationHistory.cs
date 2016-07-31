using System;

namespace Library10.Common.Entities
{
    public class ApplicationHistory
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string Message { get; set; }
        public bool ApplicationUpdate { get; set; }
        public bool ContentUpdate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}