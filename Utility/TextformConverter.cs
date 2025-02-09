using System.Text.RegularExpressions;

namespace VueBlog.API.Utility
{
    public static class TextformConverter
    {
        public static string ToSlug(string title)
        {
            title = title.ToLower().Trim();
            title = Regex.Replace(title, @"[^\p{L}\p{N}\s]", ""); // 移除特殊字符
            title = Regex.Replace(title, @"\s+", "-"); // 替换空格为-
            return title;
        }
        public static string ContentToExcerpt(string content)
        {
            if (string.IsNullOrEmpty(content))
                return string.Empty;

            // 1. 使用正则表达式去除所有 HTML 标签（尖括号及其中内容）
            string plainText = Regex.Replace(content, "<.*?>", string.Empty);

            // 2. 截取前 32 个字符
            return (plainText.Length > 100 ? plainText.Substring(0, 100) : plainText) + "...";
        }
    }
}
