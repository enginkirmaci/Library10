using System;
using System.Text.RegularExpressions;

namespace Library10.Common.Extensions
{
    public static class FileExtensions
    {
        public const string SearchPatternEverything = "*.*";
        public const string FilePathFormat = "{0}/{1}";

        private static readonly Regex PathSplitRegEx = new Regex(@"\/|\\", RegexOptions.Singleline);
        private static readonly Regex FileNameRegEx = new Regex(@"([^\/|\\]+)(?=$)", RegexOptions.Singleline);
        private static readonly Regex FileNameNoExtRegEx = new Regex(@"([^\/|\\]+)(?=\.\w+$)", RegexOptions.Singleline);
        private static readonly Regex FileNameExtRegex = new Regex(@"[^.]+$", RegexOptions.Singleline);

        public static string PathToFileName(this string path)
        {
            return FileNameRegEx.Match(path).Value;
        }

        public static string[] PathToDirectories(this string path)
        {
            var filename = PathToFileName(path);

            if (string.IsNullOrEmpty(filename))
                return PathSplitRegEx.Split(path);
            else
                return PathSplitRegEx.Split(path.Replace(filename, string.Empty));
        }

        public static string PathToFileNameWithoutExtension(this string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return string.Empty;

            string result = FileNameNoExtRegEx.Match(fileName).Value;
            if (string.IsNullOrEmpty(result))
                return fileName;

            return result;
        }

        public static string FileNameExt(this string fileName)
        {
            return FileNameExtRegex.Match(fileName).Value;
        }

        public static string GenerateFileNameByTime(this string prefix, string ext)
        {
            return prefix + DateTime.Now.ToString("MMddyyyhhmmss") + ext;
        }

        public static string GenerateSlug(this string phrase, int maxLength = 100)
        {
            string str = phrase.ToLower();
            // invalid chars, make into spaces
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces/hyphens into one space
            str = Regex.Replace(str, @"[\s-]{2,}", " ").Trim();
            // cut and trim it
            str = str.Substring(0, str.Length <= maxLength ? str.Length : maxLength).Trim();
            // hyphens
            str = Regex.Replace(str, @"\s", "-");

            return str;
        }
    }
}