using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

using log4net;

namespace maqdel.Infra
{
    public static class ConsoleHelper
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(ConsoleHelper));
        
        /// <summary>
        /// Write text in a console position
        /// </summary>
        /// <param name="Col"></param>
        /// <param name="Row"></param>
        /// <param name="Text"></param>
        public static void WriteIn(int Col, int Row, string Text) {                                    
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

        /// <summary>
        /// Write text in a console position with a color
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
                System.Console.ForegroundColor = TextColor;
                System.Console.SetCursorPosition(Col, Row);            
                System.Console.Write(Text);              
            }
            catch (Exception ex)
            {
                _logger.Error("WriteIn, Exception:", ex);
            }
        }

        /// <summary>
        /// Write a column in the console
        /// </summary>
        /// <param name="Col"></param>
        /// <param name="Row"></param>
        /// <param name="ocurrs"></param>
        /// <param name="TextColor"></param>
        public static void WriteCol(int Col, int Row, int ocurrs, ConsoleColor TextColor){
            _logger.Info("WriteRow");
            try
            {                            
                int prtLine = 1;
                for (int row = Row; row <= (Row + ocurrs - 1); row++) {
                    ConsoleHelper.WriteIn(Col, row, prtLine.ToString(), TextColor);
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
        /// Write a column in the console
        /// </summary>
        /// <param name="Col"></param>
        /// <param name="Row"></param>
        /// <param name="ocurrs"></param>
        /// <param name="Filler"></param>
        /// <param name="TextColor"></param>
        public static void WriteCol(int Col, int Row, int ocurrs, string Filler, ConsoleColor TextColor){
            _logger.Info("WriteRow");
            try
            {                            
                int prtLine = 1;
                for (int row = Row; row <= (Row + ocurrs - 1); row++) {
                    ConsoleHelper.WriteIn(Col, row, Filler, TextColor);
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
        /// Write a row in the console
        /// </summary>
        /// <param name="Col"></param>
        /// <param name="Row"></param>
        /// <param name="ocurrs"></param>
        /// <param name="TextColor"></param>
        public static void WriteRow(int Col, int Row, int ocurrs, ConsoleColor TextColor)
        {
            _logger.Info("WriteCol");
            try
            {
                int prtLine = 1;
                for (int col = Col; col <= (Col + ocurrs - 1); col++)
                {
                    ConsoleHelper.WriteIn(col, Row, prtLine.ToString(), TextColor);
                    prtLine++;
                    if (prtLine == 10) prtLine = 0;
                }                
            }
            catch (Exception ex)
            {
                _logger.Error("WriteCol, Exception:", ex);
            }
        }

        /// <summary>
        /// Write a Row in the console
        /// </summary>
        /// <param name="Col"></param>
        /// <param name="Row"></param>
        /// <param name="ocurrs"></param>
        /// <param name="Filler"></param>
        /// <param name="TextColor"></param>
        public static void WriteRow(int Col, int Row, int ocurrs, string Filler, ConsoleColor TextColor)
        {
            _logger.Info("WriteCol");
            try
            {
                int prtLine = 1;
                for (int col = Col; col <= (Col + ocurrs - 1); col++)
                {
                    ConsoleHelper.WriteIn(col, Row, Filler, TextColor);
                    prtLine++;
                    if (prtLine == 10) prtLine = 0;
                }                
            }
            catch (Exception ex)
            {
                _logger.Error("WriteCol, Exception:", ex);
            }
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