namespace GQL.QueryBuilders.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// 刪減字首的字串
        /// </summary>
        /// <param name="text"></param>
        /// <param name="startText"></param>
        /// <returns></returns>
        public static string TrimStart(this string text, string startText)
        {
            if (string.IsNullOrEmpty(startText))
            {
                return text;
            }

            if (text.StartsWith(startText) &&
                text.Length >= startText.Length)
            {
                int outTextLength = text.Length - startText.Length;
                string outText = text.Substring(text.Length, outTextLength);
                return outText;
            }
            return text;
        }

        /// <summary>
        /// 刪減字尾的字串
        /// </summary>
        /// <param name="text"></param>
        /// <param name="startText"></param>
        /// <returns></returns>
        public static string TrimEnd(this string text, string startText)
        {
            if (string.IsNullOrEmpty(startText))
            {
                return text;
            }

            if (text.EndsWith(startText) &&
                text.Length >= startText.Length)
            {
                int outTextLength = text.Length - startText.Length;
                string outText = text.Substring(0, outTextLength);
                return outText;
            }
            return text;
        }
    }
}
