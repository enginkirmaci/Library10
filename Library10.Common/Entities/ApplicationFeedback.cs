using System;

namespace Library10.Common.Entities
{
    public class ApplicationFeedback
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}