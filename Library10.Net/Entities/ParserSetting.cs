namespace Library10.Net.Entities
{
    public class ParserSetting
    {
        public bool DontStripUnlikelys { get; set; }
        //public bool ListImageDisabled { get; set; }
        public string Charset { get; set; }

        public string Allowed { get; set; }
        public string[] Disallowed { get; set; }
    }
}