using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Drawing;

using maqdel.Infra;

using log4net;

namespace maqdel.Infra
{
    public class Helper
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(Helper));

        /// <summary>
        /// Convert an object to string
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static string ConvertToString(object Value)
        {
            _logger.Info("ConvertToString");
            string answer = "";
            try
            {
                if (Value == null)
                {
                    answer = "";
                }
                else
                {
                    answer = Value.ToString();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("ConvertToString, Exception:", ex);
            }
            return answer;
        }

        /// <summary>
        /// Convert a boolean value to a string "Yes/No"
        /// </summary>
        /// <param name="Value">A boolean value</param>
        /// <returns>A string</returns>
        public static string ConvertToYesNo(bool Value)
        {
            _logger.Info("ConvertToYesNo");
            string answer = "";
            try
            {
                if (Value == true)
                {
                    answer = "Yes";
                }
                else
                {
                    answer = "No";
                }
            }
            catch (Exception ex)
            {
                _logger.Error("ConvertToYesNo, Exception:", ex);
            }
            return answer;
        }

        /// <summary>
        /// Convert a boolean value to an int "1/0"
        /// </summary>
        /// <param name="Value">A boolean value</param>
        /// <returns>An integer</returns>
        public static int ConvertToInt(bool Value)
        {
            _logger.Info("ToInt");
            int answer = -1;
            try
            {
                if (Value == true)
                {
                    answer = 1;
                }
                else
                {
                    answer = 0;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("ConvertToInt, Exception:", ex);
            }
            return answer;
        }

        /// <summary>
        /// Convert a datetime value to a string "yyyymmdd hh:mm:ss"
        /// </summary>
        /// <param name="DateTime">A datetime value</param>
        /// <returns>A string</returns>
        public static string ConvertToSQLServerDateTime(DateTime DateTime)
        {
            _logger.Info("ConvertToSQLServerDateTime");
            string answer = "";
            try
            {
                StringBuilder stringBuilder = new StringBuilder("");
                stringBuilder.Append("'");
                stringBuilder.Append(DateTime.Year.ToString());
                stringBuilder.Append(FillString(DateTime.Month.ToString(), "0", 2, 1));
                stringBuilder.Append(FillString(DateTime.Day.ToString(), "0", 2, 1));
                stringBuilder.Append(" ");
                stringBuilder.Append(FillString(DateTime.Hour.ToString(), "0", 2, 1));
                stringBuilder.Append(":");
                stringBuilder.Append(FillString(DateTime.Minute.ToString(), "0", 2, 1));
                stringBuilder.Append(":");
                stringBuilder.Append(FillString(DateTime.Second.ToString(), "0", 2, 1));
                stringBuilder.Append("'");
                answer = stringBuilder.ToString();
            }
            catch (Exception ex)
            {
                _logger.Error("ConvertToSQLServerDateTime, Exception:", ex);
            }
            return answer;
        }

        /// <summary>
        /// Convert a datetime value to a string "yyyymmdd"
        /// </summary>
        /// <param name="DateTime">A datetime value</param>
        /// <returns>A string</returns>
        public static string ConvertToSQLServerDate(DateTime DateTime)
        {
            _logger.Info("ConvertToSQLServerDate");
            string answer = "";
            try
            {
                StringBuilder stringBuilder = new StringBuilder("");
                stringBuilder.Append(DateTime.Year.ToString());
                stringBuilder.Append(FillString(DateTime.Month.ToString(), "0", 2, 1));
                stringBuilder.Append(FillString(DateTime.Day.ToString(), "0", 2, 1));
                answer = stringBuilder.ToString();
            }
            catch (Exception ex)
            {
                _logger.Error("ConvertToSQLServerDate, Exception:", ex);
            }
            return answer;
        }

        /// <summary>
        /// Convert a datetime value to a string "hh:mm:ss"
        /// </summary>
        /// <param name="DateTime">A datetime value</param>
        /// <returns>A string</returns>
        public static string ConvertToSQLServerTime(DateTime DateTime)
        {
            _logger.Info("ConvertToSQLServerTime");
            string answer = "";
            try
            {
                StringBuilder stringBuilder = new StringBuilder("");
                stringBuilder.Append("'");
                stringBuilder.Append(FillString(DateTime.Hour.ToString(), "0", 2, 1));
                stringBuilder.Append(":");
                stringBuilder.Append(FillString(DateTime.Minute.ToString(), "0", 2, 1));
                stringBuilder.Append(":");
                stringBuilder.Append(FillString(DateTime.Second.ToString(), "0", 2, 1));
                stringBuilder.Append("'");
                answer = stringBuilder.ToString();
            }
            catch (Exception ex)
            {
                _logger.Error("ConvertToSQLServerTime, Exception:", ex);
            }
            return answer;
        }

        /// <summary>
        /// Convert an int value to a boolean value
        /// </summary>
        /// <param name="Value">An integer value</param>
        /// <returns>A boolean value</returns>
        public static bool ConvertToBool(int Value)
        {
            _logger.Info("ConvertToBool");
            bool answer = false;
            try
            {             
                if (Value == 1)
                {
                    answer = true;
                }                
            }
            catch (Exception ex)
            {
                _logger.Error("ConvertToBool, Exception:", ex);
            }
            return answer;
        }

        /// <summary>
        /// Fill a string and align
        /// </summary>
        /// <param name="Text">A string</param>
        /// <param name="Filler">A string</param>
        /// <param name="Length">An integer value</param>
        /// <param name="Align">An integer value</param>
        /// <returns></returns>
        public static string FillString(string Text, string Filler, int Length, int Align)
        {
            _logger.Info("FillString");
            string answer = "";
            try
            {
                int textLength = Text.Length;
                if (textLength < Length)
                {
                    for (int i = 0; i < (Length - textLength); i++)
                    {
                        if (Align == 0)
                        {
                            Text = Text + Filler;
                        }
                        else
                        {
                            Text = Filler + Text;
                        }
                    }
                }
                if (textLength > Length)
                {
                    Text = Text.Remove(Length, (textLength - Length));
                }
                answer = Text;
            }
            catch (Exception ex)
            {
                _logger.Error("FillString, Exception:", ex);
            }
            return answer;
        }

        /// <summary>
        /// Fill a string
        /// </summary>
        /// <param name="Text">A string</param>
        /// <param name="Filler">A string</param>
        /// <param name="Length">An integer</param>
        /// <returns>A string</returns>
        public static string FillString(string Text, string Filler, int Length)
        {
            _logger.Info("FillString");
            string answer = "";
            try
            {
                int textLength = Text.Length;
                if (textLength < Length)
                {
                    for (int i = 0; i < (Length - textLength); i++)
                    {
                        Text = Text + Filler;
                    }
                }
                if (textLength > Length)
                {
                    Text = Text.Remove(Length, (textLength - Length));
                }
                answer = Text;
            }
            catch (Exception ex)
            {
                _logger.Error("FillString, Exception:", ex);
            }
            return answer;
        }

        /// <summary>
        /// Get the EOL
        /// </summary>
        /// <returns>A string</returns>
        public static string GetEOL()
        {
            _logger.Info("GetEOL");
            string answer = "";
            try
            {
                answer = "\r\n";
            }
            catch (Exception ex)
            {
                _logger.Error("GetEOL, Exception:", ex);
            }
            return answer;
        }

        /// <summary>
        /// Convert text to HTML
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public static string ConvertTextToHTML(string Text)
        {
            _logger.Info("ConvertTextToHTML");
            string answer = Text;
            try
            {
                answer = answer.Replace("á", "&aacute;");
                answer = answer.Replace("é", "&eacute;");
                answer = answer.Replace("í", "&iacute;");
                answer = answer.Replace("ó", "&oacute;");
                answer = answer.Replace("ú", "&uacute;");
                answer = answer.Replace("ñ", "&ntilde;");
                answer = answer.Replace("Á", "&Aacute;");
                answer = answer.Replace("É", "&Eacute;");
                answer = answer.Replace("Í", "&Iacute;");
                answer = answer.Replace("Ó", "&Oacute;");
                answer = answer.Replace("Ú", "&Uacute;");
                answer = answer.Replace("Ñ", "&Ntilde;");
            }
            catch (Exception ex)
            {
                _logger.Error("ConvertTextToHTML, Exception:", ex);
            }
            return answer;
        }

        /// <summary>
        /// Convert the first char of an string to upper
        /// </summary>
        /// <param name="Text">A string value</param>
        /// <returns>A string</returns>
        public static string UpperFirstCharacter(string Text)
        {
            _logger.Info("UpperFirstCharacter");
            string answer = "";
            try
            {
                if (Text.Length > 0)
                {
                    StringBuilder stringBuilder = new StringBuilder("");
                    string First = Text[0].ToString();
                    stringBuilder.Append(First.ToUpper());
                    stringBuilder.Append(Text.Substring(1, Text.Length - 1));
                    answer = stringBuilder.ToString();
                }
                return answer;
            }
            catch (Exception ex)
            {
                _logger.Error("UpperFirstCharacter, Exception:", ex);
            }
            return answer;
        }

        /// <summary>
        /// Get the name of the month
        /// </summary>
        /// <param name="Month"></param>
        /// <returns></returns>
        public static string ConvertToMonthName(int Month)
        {
            _logger.Info("ConvertToMonthName");
            string answer = "";
            try
            {
                switch (Month)
                {
                    case 1:
                        answer = "January";
                        break;
                    case 2:
                        answer = "February";
                        break;
                    case 3:
                        answer = "March";
                        break;
                    case 4:
                        answer = "April";
                        break;
                    case 5:
                        answer = "May";
                        break;
                    case 6:
                        answer = "June";
                        break;
                    case 7:
                        answer = "July";
                        break;
                    case 8:
                        answer = "August";
                        break;
                    case 9:
                        answer = "September";
                        break;
                    case 10:
                        answer = "October";
                        break;
                    case 11:
                        answer = "November";
                        break;
                    case 12:
                        answer = "December";
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("ConvertToMonthName, Exception:", ex);
            }
            return answer;
        }

        /// <summary>
        /// Validate if the object is a number
        /// </summary>
        /// <param name="Object">Objeto a evaluar</param>
        /// <returns></returns>
        public static bool IsNumeric(object Object)
        {
            _logger.Info("");
            var answer = false;
            try
            {
                bool isNumeric;
                double retNumber;
                isNumeric = Double.TryParse(Convert.ToString(Object), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNumber);
                return isNumeric;
            }
            catch (Exception ex)
            {
                _logger.Error("IsNumeric, Exception:", ex);
            }
            return answer;
        }

        /// <summary>
        /// Validate if the string is a number
        /// </summary>
        /// <param name="String">Texto a evaluar</param>
        /// <returns></returns>
        public static bool IsNumeric(string String)
        {
            _logger.Info("IsNumeric");
            var answer = false;
            try
            {
                bool isNumeric;
                double retNumber;
                isNumeric = Double.TryParse(String, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNumber);
                return isNumeric;
            }
            catch (Exception ex)
            {
                _logger.Error("IsNumeric, Exception:", ex);
            }
            return answer;
        }

        /// <summary>
        /// Validate if is a date
        /// </summary>
        /// <param name="Value"></param>
        /// <param name="Variable"></param>
        /// <returns></returns>
        public static bool IsDate(string Value, ref DateTime Variable)
        {
            _logger.Info("IsDate");
            bool answer = false;
            try
            {
                int preDate = Value.IndexOf("/", 0);
                if (preDate > 0)
                {
                    Variable = Convert.ToDateTime(Value);
                    answer = true;
                }
                else
                {
                    if (answer == false)
                    {
                        StringBuilder stringBuilder = new StringBuilder();
                        if (Value.Length == 8)
                        {
                            stringBuilder.Append(Value.Substring(0, 4));
                            stringBuilder.Append("/");
                            stringBuilder.Append(Value.Substring(4, 2));
                            stringBuilder.Append("/");
                            stringBuilder.Append(Value.Substring(6, 2));

                            //Variable = Convert.ToDateTime(Day + "/" + Month + "/" + Year);
                            Variable = Convert.ToDateTime(stringBuilder.ToString());

                            //Variable = DateTime.Now;                        

                            answer = true;
                        }
                        if (Value.Length == 7)
                        {
                            stringBuilder.Append(Value.Substring(0, 3));
                            stringBuilder.Append("/");
                            stringBuilder.Append(Value.Substring(4, 5));
                            stringBuilder.Append("/");
                            stringBuilder.Append(Value.Substring(6, 6));

                            Variable = Convert.ToDateTime(stringBuilder.ToString());

                            answer = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("IsDate, Exception:", ex);
            }
            return answer;
        }

        /// <summary>
        /// Convert a string to int
        /// </summary>
        /// <param name="String"></param>
        /// <returns></returns>
        public static int ConvertToInt(string String)
        {
            _logger.Info("ConvertToInt");
            var answer = -1;
            try
            {
                return int.Parse(String);
            }
            catch (Exception ex)
            {
                _logger.Error("ConvertToInt, Exception:", ex);
            }
            return answer;
        }

        /// <summary>
        /// Convert a string to int or return an error value
        /// </summary>
        /// <param name="String"></param>
        /// <param name="ErrorValue"></param>
        /// <returns></returns>
        public static int ConvertToInt(string String, int ErrorValue)
        {
            _logger.Info("ConvertToInt");
            var answer = -1;
            try
            {
                return int.Parse(String);
            }
            catch (Exception ex)
            {
                _logger.Error("ConvertToInt, Exception:", ex);
            }
            return answer;
        }

        /// <summary>
        /// Convert a string to decimal
        /// </summary>
        /// <param name="String"></param>
        /// <returns></returns>
        public static decimal ConvertToDecimal(string String)
        {
            _logger.Info("ConvertToDecimal");
            decimal answer = -1;
            try
            {
                answer = decimal.Parse(String);
            }
            catch (Exception ex)
            {
                _logger.Error("ConvertToDecimal, Exception:", ex);
            }
            return answer;
        }

        /// <summary>
        /// Get string from a string value
        /// </summary>
        /// <param name="SourceString">A source string</param>
        /// <param name="Start">An integer</param>
        /// <param name="Length">An integer</param>
        /// <returns>A string</returns>
        public static string GetSubstring(string SourceString, int Start, int Length)
        {
            _logger.Info("GetSubstring");
            string answer = "";
            try
            {
                if (Length <= SourceString.Length)
                {
                    answer = SourceString.Substring(Start, Length);
                }                
            }
            catch (Exception ex)
            {
                _logger.Error("GetSubstring, Exception:", ex);
            }
            return answer;
        }

        /// <summary>
        /// Get string from a string value
        /// </summary>
        /// <param name="SourceString">A source string</param>
        /// <param name="Start">An integer</param>
        /// <param name="Length">An integer</param>
        /// <param name="CleanSpaces">A string</param>
        /// <returns></returns>
        public static string GetSubstring(string SourceString, int Start, int Length, bool CleanSpaces)
        {
            _logger.Info("GetSubstring");
            string answer = "";
            try
            {
                if (Length <= SourceString.Length)
                {
                    if (CleanSpaces)
                    {
                        string CadenaLimpia = SourceString.Substring(Start, Length).TrimEnd();
                        answer = CadenaLimpia;
                    }
                    else
                    {
                        answer = SourceString.Substring(Start, Length);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("GetSubstring, Exception:", ex);
            }
            return answer;
        }
        
        /// <summary>
        /// Get a fortnight int value from a datetime value
        /// </summary>
        /// <param name="DateTime">A datetime</param>
        /// <returns>An integer</returns>
        public static int GetFortnight(DateTime DateTime)
        {
            _logger.Info("GetFortnight");
            int answer = -1;
            try
            {
                if (DateTime.Day <= 15)
                {
                    answer = ((DateTime.Month) * 2) - 1;
                }
                else
                {
                    answer = ((DateTime.Month) * 2);
                }
            }
            catch (Exception ex)
            {
                _logger.Error("GetFortnight, Exception:", ex);
            }
            return answer;
        }
        /* 
        public static void Example()
        {
            _logger.Info("");
            try
            {
                
            }
            catch (Exception ex)
            {
                _logger.Error(", Exception:", ex);
            }
        } 
        */
    }
}
