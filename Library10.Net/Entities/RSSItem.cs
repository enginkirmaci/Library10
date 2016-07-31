using System;

namespace Library10.Net.Entities
{
    public class RSSItem
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Image { get; set; }
        public bool isRead { get; set; }
    }
}