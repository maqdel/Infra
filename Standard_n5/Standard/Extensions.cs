using System;

namespace maqdel.Infra
{
    public static class Extensions
    {   
        /// <summary>
        /// Get character count
        /// </summary>
        /// <returns>An integer</returns>
        public static int CharacterCount(this string str)
        {            
            int characterCount = -1;
            char[] characters = str.ToCharArray();
            if(characters != null) characterCount = characters.Length;
            return characterCount;
        }

        /// <summary>
        /// Capitalize string
        /// </summary>
        /// <returns>A string</returns>
        public static string ToCapitalize(this string str)
        {
            string capitalizeString = str;
            if(str.Length > 0){
                capitalizeString = char.ToUpper(str[0]) + str.Substring(1);
            }
            return capitalizeString;
        }

        /// <summary>
        /// To universal datetime string
        /// </summary>
        /// <returns>A string</returns>
        public static string ToUniversalDateTime(this System.DateTime dt)
        {
            return InfraHelper.ConvertToUniversalDateTime(dt);
        }

        /// <summary>
        /// To universal date string
        /// </summary>
        /// <returns>A string</returns>
        public static string ToUniversalDate(this System.DateTime dt)
        {
            return InfraHelper.ConvertToUniversalDate(dt);
        }

        /// <summary>
        /// To universal time string
        /// </summary>
        /// <returns>A string</returns>
        public static string ToUniversalTime(this System.DateTime dt)
        {
            return InfraHelper.ConvertToUniversalTime(dt);
        }                

        /// <summary>
        /// Get word count
        /// </summary>
        /// <returns>An integer</returns>
        public static int WordCount(this string str)
        {            
            int wordCount = 0;
            if(str.Length >= 3){
                string[] words = str.Split(' ');
                if(words != null) wordCount = words.Length;
            }            
            return wordCount;
        }
    }
}