using log4net;
using System;
using System.Collections.Generic;
using System.Text;

namespace maqdel.Infra
{
    public enum PrefixType
    {
        None,
        UniversalDatetime,
        DateTime,
        Time,
    }

    public enum SufixType
    {
        None,
        UniversalDatetime,
        DateTime,
        Time,
    }

    /// <remarks>Class: ConsoleHelper</remarks> 
    /// <summary>
    /// <remarks>ConsoleHelper2</remarks>
    /// Helper to assist with console code
    /// ID string generated is "T:maqdel.Infra.X".
    /// </summary>
    public static class ConsoleHelper
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(ConsoleHelper));

        /// <summary>
        /// Add a prefix and sufix to a text
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="Prefix"></param>
        /// <param name="Sufix"></param>
        /// <returns></returns>
        private static string AddPrefixAndSufix(string Text, PrefixType Prefix, SufixType Sufix)
        {
            _logger.Info("AddPrefixAndSufix");
            string Answer = "";
            try
            {
                switch (Prefix)
                {
                    case PrefixType.UniversalDatetime:
                        Text = InfraHelper.ConvertToUniversalDateTime(DateTime.Now) + " " + Text;
                        break;
                    case PrefixType.DateTime:
                        Text = DateTime.Now.ToString() + " " + Text;
                        break;
                    case PrefixType.Time:
                        Text = DateTime.Now.ToShortTimeString() + " " + Text;
                        break;
                }
                switch (Sufix)
                {
                    case SufixType.UniversalDatetime:
                        Text = Text + " " + InfraHelper.ConvertToUniversalDateTime(DateTime.Now);
                        break;
                    case SufixType.DateTime:
                        Text = Text + " " + DateTime.Now.ToString();
                        break;
                    case SufixType.Time:
                        Text = Text + " " + DateTime.Now.ToShortTimeString();
                        break;
                }
                Answer = Text;
            }
            catch (Exception ex)
            {
                _logger.Error("AddPrefixAndSufix, Exception:", ex);
            }
            return Answer;
        }

        /// <remarks>Method: SetConsoleColor</remarks> 
        /// <summary>
        /// Set console TextColor
        /// </summary>
        /// <param name="TextColor"></param>
        public static void SetConsoleColor(ConsoleColor TextColor)
        {
            _logger.Info("SetConsoleColor");
            try
            {
                Console.ForegroundColor = TextColor;
            }
            catch (Exception ex)
            {
                _logger.Error("SetConsoleColor, Exception:", ex);
            }
        }

        /// <remarks>Method: SetConsoleColor</remarks> 
        /// <summary>
        /// Set console TextColor and BackgroundColor
        /// </summary>
        /// <param name="TextColor"></param>
        /// <param name="BackgroundColor"></param>
        public static void SetConsoleColor(ConsoleColor TextColor, ConsoleColor BackgroundColor)
        {
            _logger.Info("SetConsoleColor");
            try
            {
                System.Console.ForegroundColor = TextColor;
                System.Console.BackgroundColor = BackgroundColor;
            }
            catch (Exception ex)
            {
                _logger.Error("SetConsoleColor, Exception:", ex);
            }
        }

        /// <remarks>Method: Cls</remarks> 
        /// <summary>
        /// Clear screen and set TextColor
        /// </summary>
        /// <param name="TextColor"></param>
        public static void Cls(ConsoleColor TextColor)
        {
            _logger.Info("Cls");
            try
            {
                SetConsoleColor(TextColor);
                System.Console.Clear();
            }
            catch (Exception ex)
            {
                _logger.Error("Cls, Exception:", ex);
            }
        }

        /// <remarks>Method: Cls</remarks> 
        /// <summary>
        /// Clear screen and set TextColor and BackgroundColor
        /// </summary>
        /// <param name="TextColor"></param>
        /// <param name="BackgroundColor"></param>
        public static void Cls(ConsoleColor TextColor, ConsoleColor BackgroundColor)
        {
            _logger.Info("Cls");
            try
            {
                SetConsoleColor(TextColor, BackgroundColor);
                System.Console.Clear();
            }
            catch (Exception ex)
            {
                _logger.Error("Cls, Exception:", ex);
            }
        }
        
        /// <remarks>Method: WriteIn</remarks> 
        /// <summary>
        /// Write text in a console position
        /// </summary>
        /// <param name="Col"></param>
        /// <param name="Row"></param>
        /// <param name="Text"></param>
        public static void WriteIn(int Col, int Row, string Text)
        {
            _logger.Info("WriteIn");
            try
            {
                System.Console.SetCursorPosition(Col, Row);
                System.Console.Write(Text);
            }
            catch (Exception ex)
            {
                _logger.Error("WriteIn, Exception:", ex);
            }
        }

        /// <remarks>Method: WriteIn</remarks> 
        /// <summary>
        /// Write text in a console position with a TextColor
        /// </summary>
        /// <param name="Col"></param>
        /// <param name="Row"></param>
        /// <param name="Text"></param>
        /// <param name="TextColor"></param>
        public static void WriteIn(int Col, int Row, string Text, ConsoleColor TextColor)
        {
            _logger.Info("WriteIn");
            try
            {
                SetConsoleColor(TextColor);
                WriteIn(Col, Row, Text);
            }
            catch (Exception ex)
            {
                _logger.Error("WriteIn, Exception:", ex);
            }
        }

        /// <remarks>Method: WriteIn</remarks> 
        /// <summary>
        /// Write text in a console position with a TextColor and BackgroundColor
        /// </summary>
        /// <param name="Col"></param>
        /// <param name="Row"></param>
        /// <param name="Text"></param>
        /// <param name="TextColor"></param>
        /// <param name="BackgroundColor"></param>
        public static void WriteIn(int Col, int Row, string Text, ConsoleColor TextColor, ConsoleColor BackgroundColor)
        {
            _logger.Info("WriteIn");
            try
            {
                SetConsoleColor(TextColor, BackgroundColor);
                WriteIn(Col, Row, Text);
            }
            catch (Exception ex)
            {
                _logger.Error("WriteIn, Exception:", ex);
            }
        }

        /// <summary>
        /// Write text with Prefix and Sufix in a console position 
        /// </summary>
        /// <param name="Col"></param>
        /// <param name="Row"></param>
        /// <param name="Text"></param>
        /// <param name="Prefix"></param>
        /// <param name="Sufix"></param>
        public static void WriteIn(int Col, int Row, string Text, PrefixType Prefix, SufixType Sufix)
        {
            _logger.Info("WriteIn");
            try
            {
                Text = AddPrefixAndSufix(Text, Prefix, Sufix);
                WriteIn(Col, Row, Text);
            }
            catch (Exception ex)
            {
                _logger.Error("WriteIn, Exception:", ex);
            }
        }

        /// <summary>
        /// Write text with Prefix and Sufix in a console position with a TextColor 
        /// </summary>
        /// <param name="Col"></param>
        /// <param name="Row"></param>
        /// <param name="Text"></param>
        /// <param name="Prefix"></param>
        /// <param name="Sufix"></param>
        /// <param name="TextColor"></param>
        public static void WriteIn(int Col, int Row, string Text, PrefixType Prefix, SufixType Sufix, ConsoleColor TextColor)
        {
            _logger.Info("WriteIn");
            try
            {
                SetConsoleColor(TextColor);
                WriteIn(Col, Row, Text, Prefix, Sufix);
            }
            catch (Exception ex)
            {
                _logger.Error("WriteIn, Exception:", ex);
            }
        }

        /// <remarks>Method: WriteIn</remarks> 
        /// <summary>
        /// Write text with Prefix and Sufix in a console position with a TextColor and BackgroundColor
        /// </summary>
        /// <param name="Col"></param>
        /// <param name="Row"></param>
        /// <param name="Text"></param>
        /// <param name="TextColor"></param>
        /// <param name="BackgroundColor"></param>
        public static void WriteIn(int Col, int Row, string Text, PrefixType Prefix, SufixType Sufix, ConsoleColor TextColor, ConsoleColor BackgroundColor)
        {
            _logger.Info("WriteIn");
            try
            {
                SetConsoleColor(TextColor, BackgroundColor);
                WriteIn(Col, Row, Text, Prefix, Sufix);
            }
            catch (Exception ex)
            {
                _logger.Error("WriteIn, Exception:", ex);
            }
        }
        
        /// <summary>
        /// Write vertical text in a console position
        /// </summary>
        /// <param name="Col"></param>
        /// <param name="Row"></param>
        /// <param name="Text"></param>
        public static void WriteVerticalIn(int Col, int Row, string Text)
        {
            _logger.Info("WriteVerticalIn");
            try
            {
                foreach (var currentLetter in Text)
                {
                    System.Console.SetCursorPosition(Col, Row);
                    System.Console.Write(currentLetter);
                    Row++;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("WriteVerticalIn, Exception:", ex);
            }
        }

        /// <summary>
        /// Write vertical text with Prefix and Sufix in a console position with a TextColor
        /// </summary>
        /// <param name="Col"></param>
        /// <param name="Row"></param>
        /// <param name="Text"></param>
        /// <param name="TextColor"></param>
        public static void WriteVerticalIn(int Col, int Row, string Text, ConsoleColor TextColor)
        {
            _logger.Info("WriteVerticalIn");
            try
            {
                SetConsoleColor(TextColor);
                WriteVerticalIn(Col, Row, Text);
            }
            catch (Exception ex)
            {
                _logger.Error("WriteVerticalIn, Exception:", ex);
            }
        }

        /// <summary>
        /// Write vertical text with Prefix and Sufix in a console position with a TextColor and BackgroundColor
        /// </summary>
        /// <param name="Col"></param>
        /// <param name="Row"></param>
        /// <param name="Text"></param>
        /// <param name="TextColor"></param>
        /// <param name="BackgroundColor"></param>
        public static void WriteVerticalIn(int Col, int Row, string Text, ConsoleColor TextColor, ConsoleColor BackgroundColor)
        {
            _logger.Info("WriteVerticalIn");
            try
            {
                SetConsoleColor(TextColor, BackgroundColor);
                WriteVerticalIn(Col, Row, Text);
            }
            catch (Exception ex)
            {
                _logger.Error("WriteVerticalIn, Exception:", ex);
            }
        }

        /// <summary>
        /// Write vertical text with Prefix and Sufix in a console position
        /// </summary>
        /// <param name="Col"></param>
        /// <param name="Row"></param>
        /// <param name="Text"></param>
        /// <param name="Prefix"></param>
        /// <param name="Sufix"></param>
        public static void WriteVerticalIn(int Col, int Row, string Text, PrefixType Prefix, SufixType Sufix)
        {
            _logger.Info("WriteVerticalIn");
            try
            {
                Text = AddPrefixAndSufix(Text, Prefix, Sufix);
                WriteVerticalIn(Col, Row, Text);
            }
            catch (Exception ex)
            {
                _logger.Error("WriteVerticalIn, Exception:", ex);
            }
        }

        /// <summary>
        /// Write vertical text with Prefix and Sufix in a console position with a TextColor
        /// </summary>
        /// <param name="Col"></param>
        /// <param name="Row"></param>
        /// <param name="Text"></param>
        /// <param name="Prefix"></param>
        /// <param name="Sufix"></param>
        /// <param name="TextColor"></param>
        public static void WriteVerticalIn(int Col, int Row, string Text, PrefixType Prefix, SufixType Sufix, ConsoleColor TextColor)
        {
            _logger.Info("WriteVerticalIn");
            try
            {
                Text = AddPrefixAndSufix(Text, Prefix, Sufix);
                WriteVerticalIn(Col, Row, Text, TextColor);
            }
            catch (Exception ex)
            {
                _logger.Error("WriteVerticalIn, Exception:", ex);
            }
        }

        /// <summary>
        /// Write vertical text with Prefix and Sufix in a console position with a TextColor and BackgroundColor
        /// </summary>
        /// <param name="Col"></param>
        /// <param name="Row"></param>
        /// <param name="Text"></param>
        /// <param name="Prefix"></param>
        /// <param name="Sufix"></param>
        /// <param name="TextColor"></param>
        /// <param name="BackgroundColor"></param>
        public static void WriteVerticalIn(int Col, int Row, string Text, PrefixType Prefix, SufixType Sufix, ConsoleColor TextColor, ConsoleColor BackgroundColor)
        {
            _logger.Info("WriteVerticalIn");
            try
            {

                Text = AddPrefixAndSufix(Text, Prefix, Sufix);
                WriteVerticalIn(Col, Row, Text, TextColor, BackgroundColor);
            }
            catch (Exception ex)
            {
                _logger.Error("WriteVerticalIn, Exception:", ex);
            }
        }

        /// <remarks>Method: WriteCol</remarks> 
        /// <summary>
        /// Write a column in the console
        /// </summary>
        /// <param name="Col"></param>
        /// <param name="Row"></param>
        /// <param name="ocurrs"></param>
        public static void WriteCol(int Col, int Row, int ocurrs)
        {
            _logger.Info("WriteCol");
            try
            {
                int prtLine = 1;
                for (int row = Row; row <= (Row + ocurrs - 1); row++)
                {
                    ConsoleHelper.WriteIn(Col, row, prtLine.ToString());
                    prtLine++;
                    if (prtLine == 10) prtLine = 0;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("WriteCol, Exception:", ex);
            }
        }

        /// <remarks>Method: WriteCol</remarks> 
        /// <summary>
        /// Write a column in the console and set TextColor
        /// </summary>
        /// <param name="Col"></param>
        /// <param name="Row"></param>
        /// <param name="ocurrs"></param>
        /// <param name="TextColor"></param>
        public static void WriteCol(int Col, int Row, int ocurrs, ConsoleColor TextColor)
        {
            _logger.Info("WriteCol");
            try
            {
                SetConsoleColor(TextColor);
                int prtLine = 1;
                for (int row = Row; row <= (Row + ocurrs - 1); row++)
                {
                    ConsoleHelper.WriteIn(Col, row, prtLine.ToString());
                    prtLine++;
                    if (prtLine == 10) prtLine = 0;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("WriteCol, Exception:", ex);
            }
        }

        /// <remarks>Method: WriteCol</remarks> 
        /// <summary>
        /// Write a column in the console and set TextColor and BackgroundColor
        /// </summary>
        /// <param name="Col"></param>
        /// <param name="Row"></param>
        /// <param name="ocurrs"></param>
        /// <param name="TextColor"></param>
        /// <param name="BackgroundColor"></param>
        public static void WriteCol(int Col, int Row, int ocurrs, ConsoleColor TextColor, ConsoleColor BackgroundColor)
        {
            _logger.Info("WriteCol");
            try
            {
                SetConsoleColor(TextColor, BackgroundColor);
                int prtLine = 1;
                for (int row = Row; row <= (Row + ocurrs - 1); row++)
                {
                    ConsoleHelper.WriteIn(Col, row, prtLine.ToString());
                    prtLine++;
                    if (prtLine == 10) prtLine = 0;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("WriteCol, Exception:", ex);
            }
        }

        /// <remarks>Method: WriteCol</remarks> 
        /// <summary>
        /// Write a column in the console
        /// </summary>
        /// <param name="Col"></param>
        /// <param name="Row"></param>
        /// <param name="ocurrs"></param>
        /// <param name="Filler"></param>
        public static void WriteCol(int Col, int Row, int ocurrs, string Filler)
        {
            _logger.Info("WriteCol");
            try
            {
                int prtLine = 1;
                for (int row = Row; row <= (Row + ocurrs - 1); row++)
                {
                    ConsoleHelper.WriteIn(Col, row, Filler);
                    prtLine++;
                    if (prtLine == 10) prtLine = 0;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("WriteCol, Exception:", ex);
            }
        }

        /// <remarks>Method: WriteCol</remarks> 
        /// <summary>
        /// Write a column in the console and set TextColor
        /// </summary>
        /// <param name="Col"></param>
        /// <param name="Row"></param>
        /// <param name="ocurrs"></param>
        /// <param name="Filler"></param>
        /// <param name="TextColor"></param>
        public static void WriteCol(int Col, int Row, int ocurrs, string Filler, ConsoleColor TextColor)
        {
            _logger.Info("WriteCol");
            try
            {
                SetConsoleColor(TextColor);
                int prtLine = 1;
                for (int row = Row; row <= (Row + ocurrs - 1); row++)
                {
                    ConsoleHelper.WriteIn(Col, row, Filler);
                    prtLine++;
                    if (prtLine == 10) prtLine = 0;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("WriteCol, Exception:", ex);
            }
        }

        /// <remarks>Method: WriteCol</remarks> 
        /// <summary>
        /// Write a column in the console and set TextColor and BackgroundColor
        /// </summary>
        /// <param name="Col"></param>
        /// <param name="Row"></param>
        /// <param name="ocurrs"></param>
        /// <param name="Filler"></param>
        /// <param name="TextColor"></param>
        /// <param name="BackgroundColor"></param>
        public static void WriteCol(int Col, int Row, int ocurrs, string Filler, ConsoleColor TextColor, ConsoleColor BackgroundColor)
        {
            _logger.Info("WriteCol");
            try
            {
                SetConsoleColor(TextColor, BackgroundColor);
                int prtLine = 1;
                for (int row = Row; row <= (Row + ocurrs - 1); row++)
                {
                    ConsoleHelper.WriteIn(Col, row, Filler);
                    prtLine++;
                    if (prtLine == 10) prtLine = 0;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("WriteCol, Exception:", ex);
            }
        }

        /// <remarks>Method: WriteRow</remarks> 
        /// <summary>
        /// Write a row in the console
        /// </summary>
        /// <param name="Col"></param>
        /// <param name="Row"></param>
        /// <param name="ocurrs"></param>
        public static void WriteRow(int Col, int Row, int ocurrs)
        {
            _logger.Info("WriteRow");
            try
            {
                int prtLine = 1;
                for (int col = Col; col <= (Col + ocurrs - 1); col++)
                {
                    ConsoleHelper.WriteIn(col, Row, prtLine.ToString());
                    prtLine++;
                    if (prtLine == 10) prtLine = 0;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("WriteRow, Exception:", ex);
            }
        }

        /// <remarks>Method: WriteRow</remarks> 
        /// <summary>
        /// Write a row in the console and set TextColor
        /// </summary>
        /// <param name="Col"></param>
        /// <param name="Row"></param>
        /// <param name="ocurrs"></param>
        /// <param name="TextColor"></param>
        public static void WriteRow(int Col, int Row, int ocurrs, ConsoleColor TextColor)
        {
            _logger.Info("WriteRow");
            try
            {
                SetConsoleColor(TextColor);
                int prtLine = 1;
                for (int col = Col; col <= (Col + ocurrs - 1); col++)
                {
                    ConsoleHelper.WriteIn(col, Row, prtLine.ToString());
                    prtLine++;
                    if (prtLine == 10) prtLine = 0;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("WriteRow, Exception:", ex);
            }
        }

        /// <remarks>Method: WriteRow</remarks> 
        /// <summary>
        /// Write a row in the console and set TextColor and BackgroundColor
        /// </summary>
        /// <param name="Col"></param>
        /// <param name="Row"></param>
        /// <param name="ocurrs"></param>
        /// <param name="TextColor"></param>
        /// <param name="BackgroundColor"></param>
        public static void WriteRow(int Col, int Row, int ocurrs, ConsoleColor TextColor, ConsoleColor BackgroundColor)
        {
            _logger.Info("WriteRow");
            try
            {
                SetConsoleColor(TextColor);
                int prtLine = 1;
                for (int col = Col; col <= (Col + ocurrs - 1); col++)
                {
                    ConsoleHelper.WriteIn(col, Row, prtLine.ToString());
                    prtLine++;
                    if (prtLine == 10) prtLine = 0;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("WriteRow, Exception:", ex);
            }
        }

        /// <remarks>Method: WriteRow</remarks> 
        /// <summary>
        /// Write a Row in the console
        /// </summary>
        /// <param name="Col"></param>
        /// <param name="Row"></param>
        /// <param name="ocurrs"></param>
        /// <param name="Filler"></param>
        public static void WriteRow(int Col, int Row, int ocurrs, string Filler)
        {
            _logger.Info("WriteRow");
            try
            {
                int prtLine = 1;
                for (int col = Col; col <= (Col + ocurrs - 1); col++)
                {
                    ConsoleHelper.WriteIn(col, Row, Filler);
                    prtLine++;
                    if (prtLine == 10) prtLine = 0;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("WriteRow, Exception:", ex);
            }
        }

        /// <remarks>Method: WriteRow</remarks> 
        /// <summary>
        /// Write a Row in the console and set TextColor
        /// </summary>
        /// <param name="Col"></param>
        /// <param name="Row"></param>
        /// <param name="ocurrs"></param>
        /// <param name="Filler"></param>
        /// <param name="TextColor"></param>
        public static void WriteRow(int Col, int Row, int ocurrs, string Filler, ConsoleColor TextColor)
        {
            _logger.Info("WriteRow");
            try
            {
                SetConsoleColor(TextColor);
                int prtLine = 1;
                for (int col = Col; col <= (Col + ocurrs - 1); col++)
                {
                    ConsoleHelper.WriteIn(col, Row, Filler);
                    prtLine++;
                    if (prtLine == 10) prtLine = 0;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("WriteRow, Exception:", ex);
            }
        }

        /// <remarks>Method: WriteRow</remarks> 
        /// <summary>
        /// Write a Row in the console and set TextColor and BackgroundColor
        /// </summary>
        /// <param name="Col"></param>
        /// <param name="Row"></param>
        /// <param name="ocurrs"></param>
        /// <param name="Filler"></param>
        /// <param name="TextColor"></param>
        /// <param name="BackgroundColor"></param>
        public static void WriteRow(int Col, int Row, int ocurrs, string Filler, ConsoleColor TextColor, ConsoleColor BackgroundColor)
        {
            _logger.Info("WriteRow");
            try
            {
                SetConsoleColor(TextColor, BackgroundColor);
                int prtLine = 1;
                for (int col = Col; col <= (Col + ocurrs - 1); col++)
                {
                    ConsoleHelper.WriteIn(col, Row, Filler);
                    prtLine++;
                    if (prtLine == 10) prtLine = 0;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("WriteRow, Exception:", ex);
            }
        }

        /// <summary>
        /// Write a window in the console with text
        /// </summary>
        /// <param name="Col"></param>
        /// <param name="Row"></param>
        /// <param name="Width"></param>
        /// <param name="Header"></param>
        /// <param name="Text"></param>
        /// <param name="Footer"></param>
        public static void WriteWindow(int Col, int Row, int Width, string Header, string Text, string Footer)
        {
            _logger.Info("WriteWindow");
            try
            {
                WriteWindow(
                    Col, Row,
                    Width,
                    Header,
                    new List<string>() { Text },
                    Footer                    
                    );
            }
            catch (Exception ex)
            {
                _logger.Error("WriteWindow, Exception:", ex);
            }
        }

        /// <summary>
        /// Write a window in the console with text with TextColor
        /// </summary>
        /// <param name="Col"></param>
        /// <param name="Row"></param>
        /// <param name="Width"></param>
        /// <param name="Header"></param>
        /// <param name="Text"></param>
        /// <param name="Footer"></param>
        /// <param name="TextColor"></param>
        public static void WriteWindow(int Col, int Row, int Width, string Header, string Text, string Footer, ConsoleColor TextColor)
        {
            _logger.Info("WriteWindow");
            try
            {
                SetConsoleColor(TextColor);
                WriteWindow(
                    Col, Row,
                    Width,
                    Header,
                    new List<string>() { Text },
                    Footer
                );
            }
            catch (Exception ex)
            {
                _logger.Error("WriteWindow, Exception:", ex);
            }
        }

        /// <summary>
        /// Write a window in the console with text with TextColor and BackgroundColor
        /// </summary>
        /// <param name="Col"></param>
        /// <param name="Row"></param>
        /// <param name="Width"></param>
        /// <param name="Header"></param>
        /// <param name="Text"></param>
        /// <param name="Footer"></param>
        /// <param name="TextColor"></param>
        /// <param name="BackgroundColor"></param>
        public static void WriteWindow(int Col, int Row, int Width, string Header, string Text, string Footer, ConsoleColor TextColor, ConsoleColor BackgroundColor)
        {
            _logger.Info("WriteWindow");
            try
            {
                SetConsoleColor(TextColor, BackgroundColor);
                WriteWindow(
                    Col, Row,
                    Width,
                    Header,
                    new List<string>() { Text },
                    Footer
                );
            }
            catch (Exception ex)
            {
                _logger.Error("WriteWindow, Exception:", ex);
            }
        }

        /// <summary>
        /// Write a window in the console with body
        /// </summary>
        /// <param name="Col"></param>
        /// <param name="Row"></param>
        /// <param name="Width"></param>
        /// <param name="Header"></param>
        /// <param name="Body"></param>
        /// <param name="Footer"></param>
        public static void WriteWindow(int Col, int Row, int Width, string Header, List<string> Body, string Footer)
        {
            _logger.Info("WriteWindow");
            try
            {
                List<string> windowlines = new List<string>();

                string line;

                //first line
                line = InfraHelper.FillString("┌", "─", Width) + "┐";
                windowlines.Add(line);

                //all line
                if (Header.Length > 0) {
                    line = InfraHelper.FillString("│ " + Header, " ", Width) + "│";
                    windowlines.Add(line);
                } else {
                    line = InfraHelper.FillString("│", " ", Width) + "│";
                    windowlines.Add(line);
                }

                //EHL line
                line = InfraHelper.FillString("├", "─", Width) + "┤";
                windowlines.Add(line);

                if (Body.Count > 0)
                {
                    foreach (var mes in Body)
                    {
                        line = InfraHelper.FillString("│ " + mes, " ", Width) + "│";
                        windowlines.Add(line);
                    }
                }

                line = InfraHelper.FillString("│", " ", Width) + "│";
                windowlines.Add(line);

                if (Footer.Length > 0)
                {
                    line = InfraHelper.FillString("│ " + Footer, " ", Width) + "│";
                    windowlines.Add(line);
                }
                else
                {
                    line = InfraHelper.FillString("│", " ", Width) + "│";
                    windowlines.Add(line);
                }

                //final line
                line = InfraHelper.FillString("└", "─", Width) + "┘";
                windowlines.Add(line);

                ConsoleHelper.WriteLines(Col, Row, windowlines);
            }
            catch (Exception ex)
            {
                _logger.Error("WriteWindow, Exception:", ex);
            }
        }

        /// <summary>
        /// Write a window in the console with body and TextColor
        /// </summary>
        /// <param name="Col"></param>
        /// <param name="Row"></param>
        /// <param name="Width"></param>
        /// <param name="Header"></param>
        /// <param name="Body"></param>
        /// <param name="Footer"></param>
        /// <param name="TextColor"></param>
        public static void WriteWindow(int Col, int Row, int Width, string Header, List<string> Body, string Footer, ConsoleColor TextColor)
        {
            _logger.Info("WriteWindow");
            try
            {
                SetConsoleColor(TextColor);
                WriteWindow(
                    Col, Row,
                    Width,
                    Header,
                    Body,
                    Footer
                );
            }
            catch (Exception ex)
            {
                _logger.Error("WriteWindow, Exception:", ex);
            }
        }

        /// <summary>
        /// Write a window in the console with body and TextColor and BackgroundColor
        /// </summary>
        /// <param name="Col"></param>
        /// <param name="Row"></param>
        /// <param name="Width"></param>
        /// <param name="Header"></param>
        /// <param name="Body"></param>
        /// <param name="Footer"></param>
        /// <param name="TextColor"></param>
        /// <param name="BackgroundColor"></param>
        public static void WriteWindow(int Col, int Row, int Width, string Header, List<string> Body, string Footer, ConsoleColor TextColor, ConsoleColor BackgroundColor)
        {
            _logger.Info("WriteWindow");
            try
            {
                SetConsoleColor(TextColor, BackgroundColor);
                WriteWindow(
                    Col, Row,
                    Width,
                    Header,
                    Body,
                    Footer
                );
            }
            catch (Exception ex)
            {
                _logger.Error("WriteWindow, Exception:", ex);
            }
        }

        /// <summary>
        /// Write a set of lines in the console
        /// </summary>
        /// <param name="Col"></param>
        /// <param name="Row"></param>
        /// <param name="Lines"></param>
        public static void WriteLines(int Col, int Row, List<string> Lines)
        {
            _logger.Info("WriteLines");
            try
            {
                foreach(var line in Lines)
                {
                    WriteIn(Col, Row, line);
                    Row++;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("WriteLines, Exception:", ex);
            }
        }

        /// <summary>
        /// Write a set of lines in the console with TextColor
        /// </summary>
        /// <param name="Col"></param>
        /// <param name="Row"></param>
        /// <param name="Lines"></param>
        /// <param name="TextColor"></param>
        public static void WriteLines(int Col, int Row, List<string> Lines, ConsoleColor TextColor)
        {
            _logger.Info("WriteLines");
            try
            {
                SetConsoleColor(TextColor);
                WriteLines(Col, Row, Lines);
            }
            catch (Exception ex)
            {
                _logger.Error("WriteLines, Exception:", ex);
            }
        }

        /// <summary>
        /// Write a set of lines in the console with TextColor and BackgroundColor
        /// </summary>
        /// <param name="Col"></param>
        /// <param name="Row"></param>
        /// <param name="Lines"></param>
        /// <param name="TextColor"></param>
        /// <param name="BackgroundColor"></param>
        public static void WriteLines(int Col, int Row, List<string> Lines, ConsoleColor TextColor, ConsoleColor BackgroundColor)
        {
            _logger.Info("WriteLines");
            try
            {                
                SetConsoleColor(TextColor, BackgroundColor);
                WriteLines(Col, Row, Lines);
            }
            catch (Exception ex)
            {
                _logger.Error("WriteLines, Exception:", ex);
            }
        }

        /* 
        /// <remarks>Method: </remarks> 
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
