using HtmlAgilityPack;
using Library10.Encodings;
using Library10.Net.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml;

namespace Library10.Net
{
    public class RSSAggregator
    {
        public HttpClient HttpClient { get; set; }
        private static string[] formats = new string[0];

        public RSSAggregator()
        {
            //var httpClientHandler = new HttpClientHandler();
            //httpClientHandler.AllowAutoRedirect = false;

            HttpClient = new HttpClient();

            HttpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla / 5.0(compatible; MSIE 10.0; Windows NT 6.3; WOW64; Trident / 7.0)");

            HttpClient.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue()
            {
                NoCache = true
            };
        }

        async public Task<Stream> GetFeedAsyncs(string url, string charset)
        {
            var uri = new Uri(url);

            var response = await HttpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();

            Stream stream = null;
            if (!string.IsNullOrEmpty(charset))
            {
                var responseByteArray = await response.Content.ReadAsByteArrayAsync();

                var encoder = new Encoder(true);
                var encoding = encoder.GetEncodingFromString(charset);

                var responseString = encoding.GetString(responseByteArray, 0, responseByteArray.Length);

                stream = new MemoryStream();
                StreamWriter writer = new StreamWriter(stream);

                writer.Write(responseString);
                writer.Flush();
                stream.Position = 0;
            }
            else
                stream = await response.Content.ReadAsStreamAsync();

            return stream;
        }

        public IEnumerable<RSSItem> ParseFeeds(Stream stream, ParserSetting setting)
        {
            var htmlDoc = new HtmlDocument()
            {
                OptionFixNestedTags = true,
                OptionAutoCloseOnEnd = true
            };

            var items = new List<RSSItem>();

            var settings = new XmlReaderSettings()
            {
                DtdProcessing = DtdProcessing.Ignore
            };

            using (XmlReader reader = XmlReader.Create(new StreamReader(stream, false), settings))
            {
                bool openItem = false;
                string elementName = string.Empty;

                var item = new RSSItem();

                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:

                            elementName = reader.Name;

                            if (reader.Name == "item" || reader.Name == "entry")
                            {
                                item = new RSSItem();
                                openItem = true;
                            }
                            else if (reader.Name == "link" && item != null)
                            {
                                item.Link = reader.GetAttribute("href");
                            }
                            else if (reader.Name == "media:content" && item != null)
                            {
                                item.Image = reader.GetAttribute("url");
                            }
                            break;

                        case XmlNodeType.Text:
                        case XmlNodeType.CDATA:

                            if (openItem)
                            {
                                try
                                {
                                    var text = reader.Value.ToString().Trim();

                                    switch (elementName)
                                    {
                                        case "title":
                                            item.Title = text.Replace("<![CDATA[", string.Empty).Replace("]]>", string.Empty);
                                            break;

                                        case "link":
                                            item.Link = text;
                                            break;

                                        case "description":
                                            item.Description += text;
                                            break;

                                        case "pubDate":
                                        case "published":
                                            item.Date = getTimeStamp(text);
                                            break;

                                        case "image":
                                        case "content":
                                            item.Image = text;
                                            break;
                                    }
                                }
                                catch
                                {
                                }
                            }
                            break;

                        case XmlNodeType.EndElement:
                            if ((reader.Name == "item" || reader.Name == "entry") && openItem)
                            {
                                openItem = false;

                                if (string.IsNullOrWhiteSpace(item.Image) && !string.IsNullOrWhiteSpace(item.Description))
                                {
                                    htmlDoc.LoadHtml(item.Description);
                                    item.Description = htmlDoc.DocumentNode.InnerText;

                                    var images = htmlDoc.DocumentNode.Elements("img");
                                    if (images.Count() == 0)
                                        images = htmlDoc.DocumentNode.Elements("ımg");

                                    var image = images.FirstOrDefault();

                                    if (image != null && image.Attributes.Contains("src"))
                                        item.Image = image.Attributes["src"].Value;
                                }
                                else if (!string.IsNullOrWhiteSpace(item.Image) && item.Image.Contains("src="))
                                {
                                    htmlDoc.LoadHtml(item.Image);

                                    if (string.IsNullOrWhiteSpace(item.Description))
                                        item.Description = htmlDoc.DocumentNode.InnerText;

                                    var images = htmlDoc.DocumentNode.Elements("img");
                                    if (images.Count() == 0)
                                        images = htmlDoc.DocumentNode.Elements("ımg");

                                    var image = images.FirstOrDefault();

                                    if (image != null && image.Attributes.Contains("src"))
                                        item.Image = image.Attributes["src"].Value;
                                }

                                items.Add(item);
                            }

                            break;
                    }
                }
            }
            htmlDoc = null;

            return items;
        }

        private static DateTime getTimeStamp(string date)
        {
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0);

            try
            {
                return DateTime.Parse(date);
            }
            catch { }

            try
            {
                return DateTime.ParseExact(ConvertZoneToLocalDifferential(date), Rfc822DateTimePatterns, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AdjustToUniversal);
            }
            catch { }

            return DateTime.Now;
        }

        public static string[] Rfc822DateTimePatterns
        {
            get
            {
                if (formats.Length > 0)
                {
                    return formats;
                }
                else
                {
                    formats = null;
                    formats = new string[35];

                    // two-digit day, four-digit year patterns
                    formats[0] = "ddd',' dd MMM yyyy HH':'mm':'ss'.'fffffff zzzz";
                    formats[1] = "ddd',' dd MMM yyyy HH':'mm':'ss'.'ffffff zzzz";
                    formats[2] = "ddd',' dd MMM yyyy HH':'mm':'ss'.'fffff zzzz";
                    formats[3] = "ddd',' dd MMM yyyy HH':'mm':'ss'.'ffff zzzz";
                    formats[4] = "ddd',' dd MMM yyyy HH':'mm':'ss'.'fff zzzz";
                    formats[5] = "ddd',' dd MMM yyyy HH':'mm':'ss'.'ff zzzz";
                    formats[6] = "ddd',' dd MMM yyyy HH':'mm':'ss'.'f zzzz";
                    formats[7] = "ddd',' dd MMM yyyy HH':'mm':'ss zzzz";

                    // two-digit day, two-digit year patterns
                    formats[8] = "ddd',' dd MMM yy HH':'mm':'ss'.'fffffff zzzz";
                    formats[9] = "ddd',' dd MMM yy HH':'mm':'ss'.'ffffff zzzz";
                    formats[10] = "ddd',' dd MMM yy HH':'mm':'ss'.'fffff zzzz";
                    formats[11] = "ddd',' dd MMM yy HH':'mm':'ss'.'ffff zzzz";
                    formats[12] = "ddd',' dd MMM yy HH':'mm':'ss'.'fff zzzz";
                    formats[13] = "ddd',' dd MMM yy HH':'mm':'ss'.'ff zzzz";
                    formats[14] = "ddd',' dd MMM yy HH':'mm':'ss'.'f zzzz";
                    formats[15] = "ddd',' dd MMM yy HH':'mm':'ss zzzz";

                    // one-digit day, four-digit year patterns
                    formats[16] = "ddd',' d MMM yyyy HH':'mm':'ss'.'fffffff zzzz";
                    formats[17] = "ddd',' d MMM yyyy HH':'mm':'ss'.'ffffff zzzz";
                    formats[18] = "ddd',' d MMM yyyy HH':'mm':'ss'.'fffff zzzz";
                    formats[19] = "ddd',' d MMM yyyy HH':'mm':'ss'.'ffff zzzz";
                    formats[20] = "ddd',' d MMM yyyy HH':'mm':'ss'.'fff zzzz";
                    formats[21] = "ddd',' d MMM yyyy HH':'mm':'ss'.'ff zzzz";
                    formats[22] = "ddd',' d MMM yyyy HH':'mm':'ss'.'f zzzz";
                    formats[23] = "ddd',' d MMM yyyy HH':'mm':'ss zzzz";

                    // two-digit day, two-digit year patterns
                    formats[24] = "ddd',' d MMM yy HH':'mm':'ss'.'fffffff zzzz";
                    formats[25] = "ddd',' d MMM yy HH':'mm':'ss'.'ffffff zzzz";
                    formats[26] = "ddd',' d MMM yy HH':'mm':'ss'.'fffff zzzz";
                    formats[27] = "ddd',' d MMM yy HH':'mm':'ss'.'ffff zzzz";
                    formats[28] = "ddd',' d MMM yy HH':'mm':'ss'.'fff zzzz";
                    formats[29] = "ddd',' d MMM yy HH':'mm':'ss'.'ff zzzz";
                    formats[30] = "ddd',' d MMM yy HH':'mm':'ss'.'f zzzz";
                    formats[31] = "ddd',' d MMM yy HH':'mm':'ss zzzz";

                    // Fall back patterns
                    formats[32] = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffffffK"; // RoundtripDateTimePattern

                    //2015-02-23T07:18:58+02:00

                    formats[33] = DateTimeFormatInfo.InvariantInfo.UniversalSortableDateTimePattern;
                    formats[34] = DateTimeFormatInfo.InvariantInfo.SortableDateTimePattern;

                    return formats;
                }
            }
        }

        private static string ConvertZoneToLocalDifferential(string s)
        {
            string zoneRepresentedAsLocalDifferential = String.Empty;

            //------------------------------------------------------------
            //  Validate parameter
            //------------------------------------------------------------
            if (String.IsNullOrEmpty(s))
            {
                throw new ArgumentNullException("s");
            }

            if (s.EndsWith(" UT", StringComparison.OrdinalIgnoreCase))
            {
                zoneRepresentedAsLocalDifferential = String.Concat(s.Substring(0, (s.LastIndexOf(" UT") + 1)), "+00:00");
            }
            else if (s.EndsWith(" GMT", StringComparison.OrdinalIgnoreCase))
            {
                zoneRepresentedAsLocalDifferential = String.Concat(s.Substring(0, (s.LastIndexOf(" GMT") + 1)), "+00:00");
            }
            else if (s.EndsWith(" EST", StringComparison.OrdinalIgnoreCase))
            {
                zoneRepresentedAsLocalDifferential = String.Concat(s.Substring(0, (s.LastIndexOf(" EST") + 1)), "-05:00");
            }
            else if (s.EndsWith(" EDT", StringComparison.OrdinalIgnoreCase))
            {
                zoneRepresentedAsLocalDifferential = String.Concat(s.Substring(0, (s.LastIndexOf(" EDT") + 1)), "-04:00");
            }
            else if (s.EndsWith(" CST", StringComparison.OrdinalIgnoreCase))
            {
                zoneRepresentedAsLocalDifferential = String.Concat(s.Substring(0, (s.LastIndexOf(" CST") + 1)), "-06:00");
            }
            else if (s.EndsWith(" CDT", StringComparison.OrdinalIgnoreCase))
            {
                zoneRepresentedAsLocalDifferential = String.Concat(s.Substring(0, (s.LastIndexOf(" CDT") + 1)), "-05:00");
            }
            else if (s.EndsWith(" MST", StringComparison.OrdinalIgnoreCase))
            {
                zoneRepresentedAsLocalDifferential = String.Concat(s.Substring(0, (s.LastIndexOf(" MST") + 1)), "-07:00");
            }
            else if (s.EndsWith(" MDT", StringComparison.OrdinalIgnoreCase))
            {
                zoneRepresentedAsLocalDifferential = String.Concat(s.Substring(0, (s.LastIndexOf(" MDT") + 1)), "-06:00");
            }
            else if (s.EndsWith(" PST", StringComparison.OrdinalIgnoreCase))
            {
                zoneRepresentedAsLocalDifferential = String.Concat(s.Substring(0, (s.LastIndexOf(" PST") + 1)), "-08:00");
            }
            else if (s.EndsWith(" PDT", StringComparison.OrdinalIgnoreCase))
            {
                zoneRepresentedAsLocalDifferential = String.Concat(s.Substring(0, (s.LastIndexOf(" PDT") + 1)), "-07:00");
            }
            else if (s.EndsWith(" Z", StringComparison.OrdinalIgnoreCase))
            {
                zoneRepresentedAsLocalDifferential = String.Concat(s.Substring(0, (s.LastIndexOf(" Z") + 1)), "+00:00");
            }
            else if (s.EndsWith(" A", StringComparison.OrdinalIgnoreCase))
            {
                zoneRepresentedAsLocalDifferential = String.Concat(s.Substring(0, (s.LastIndexOf(" A") + 1)), "-01:00");
            }
            else if (s.EndsWith(" M", StringComparison.OrdinalIgnoreCase))
            {
                zoneRepresentedAsLocalDifferential = String.Concat(s.Substring(0, (s.LastIndexOf(" M") + 1)), "-12:00");
            }
            else if (s.EndsWith(" N", StringComparison.OrdinalIgnoreCase))
            {
                zoneRepresentedAsLocalDifferential = String.Concat(s.Substring(0, (s.LastIndexOf(" N") + 1)), "+01:00");
            }
            else if (s.EndsWith(" Y", StringComparison.OrdinalIgnoreCase))
            {
                zoneRepresentedAsLocalDifferential = String.Concat(s.Substring(0, (s.LastIndexOf(" Y") + 1)), "+12:00");
            }
            else
            {
                zoneRepresentedAsLocalDifferential = s;
            }

            return zoneRepresentedAsLocalDifferential;
        }
    }
}