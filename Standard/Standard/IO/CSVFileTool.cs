using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Drawing;


using log4net;

namespace maqdel.Infra.IO
{
    public class CSVFileTool
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(CSVFileTool));

        private string _filename;

        private StringBuilder _file;

        public int Rows;

        public int Columns;

        public string[,] Fields;

        public CSVFileTool()
        {            
            _logger.Info("CSVFileTool");
            try
            {
                this._file = new StringBuilder();    
            }
            catch (Exception ex)
            {
                _logger.Error("CSVFileTool, Exception:", ex);
            }
        }

        public CSVFileTool(string Filename)
        {
            _logger.Info("CSVFileTool");
            try
            {
                this._filename = Filename;
                this._file = new StringBuilder();
            }
            catch (Exception ex)
            {
                _logger.Error("CSVFileTool, Exception:", ex);
            }
        }

        public bool LoadCSVData(string FileData)
        {
            _logger.Info("LoadFile");
            var answer = false;
            try
            {
                _file = new StringBuilder(FileData);

                Rows = this.GetLinesCount();

                Columns = this.GetLineColumns();
                Fields = this.GetFields();

                answer =  true;
            }
            catch (Exception ex)
            {
                _logger.Error("LoadFile, Exception:", ex);
            }
            return answer;
        }

        public bool LoadFile()
        {
            _logger.Info("LoadFile");
            var answer = false;
            try
            {
                string filetext = IOHelper.OpenTextFile(this._filename);
                string lastChar = filetext.Substring(filetext.Length - 1, 1);
                filetext = filetext.Remove(filetext.Length - 1);
                lastChar = filetext.Substring(filetext.Length - 1, 1);

                LoadCSVData(filetext);

                answer =  true;
            }
            catch (Exception ex)
            {
                _logger.Error("LoadFile, Exception:", ex);
            }
            return answer;
        }

        public bool LoadFile(int MaxLines)
        {
            _logger.Info("LoadFile");
            var answer = false;
            try
            {
                string filetext = IOHelper.OpenTextFile(this._filename);
                string lastChar = filetext.Substring(filetext.Length - 1, 1);
                filetext = filetext.Remove(filetext.Length - 1);
                lastChar = filetext.Substring(filetext.Length - 1, 1);                

                this._file = new StringBuilder(filetext);

                this.Rows = this.GetLinesCount();

                if (this.Rows < MaxLines)
                {
                    this.Columns = this.GetLineColumns();
                    this.Fields = this.GetFields();
                }
                answer = true;
            }
            catch (Exception ex)
            {
                _logger.Error("LoadFile, Exception:", ex);
            }
            return answer;
        }

        private int GetLinesCount()
        {
            _logger.Info("GetLinesCount");
            int answer = -1;
            try
            {
                string[] lines = this._file.ToString().Split('\n');
                answer = lines.Length;
            }
            catch (Exception ex)
            {
                _logger.Error("GetLinesCount, Exception:", ex);
            }
            return answer;
        }

        private int GetLineColumns()
        {
            _logger.Info("GetLineColumns");
            var answer = -1;
            try
            {
                string[] lines = this._file.ToString().Split('\n');

                string[] fields;
                string line = lines[0];
                
                fields = line.Split(',');
                answer = fields.Length;
            }
            catch (Exception ex)
            {
                _logger.Error("GetLineColumns, Exception:", ex);
            }
            return answer;
        }

        public string[] GetLines()
        {                        
            _logger.Info("GetLines");
            string[] answer = new string[0];
            try
            {
                answer = this._file.ToString().Split('\n');
            }
            catch (Exception ex)
            {
                _logger.Error("GetLines, Exception:", ex);
            }
            return answer;
        }

        public string[,] GetFields()
        {
            _logger.Info("GetFields");
            string[,] answer = new string[0, 0];
            try
            {
                string[] fields;
                string[] lines = this._file.ToString().Split('\n');
                if (lines.Length > 0)
                {
                    answer = new string[this.Rows, this.Columns];
                    for (int pntLine = 0; pntLine < lines.Length; pntLine++)
                    {
                        if (lines[pntLine].Length > 0)
                        {
                            lines[pntLine] = this.SetCSVLine(lines[pntLine]);
                            fields = lines[pntLine].Split(',');
                            if (fields.Length > 0)
                            {
                                for (int pntField = 0; pntField < fields.Length; pntField++)
                                {
                                    fields[pntField] = fields[pntField].Replace("[%c_comma]", ",");
                                    answer[pntLine, pntField] = fields[pntField];
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("GetFields, Exception:", ex);
                answer = new string[0, 0];
            }
            return answer;
        }

        private string SetCSVLine(string line)
        {
            _logger.Info("GetFields");
            string answer = "";            
            try
            {
                StringBuilder cleanLine;
                int pntPos;
                int pntComa;
                int pntComilla;
                int count;
                string field;
                string letter;
                string splitter;

                cleanLine = new StringBuilder();
                pntPos = 0;
                count = 0;
                splitter = "";

                while (pntPos < line.Length)
                {
                    if (pntPos < line.Length)
                    {
                        //pntNext = pntPos;
                        letter = line.Substring(pntPos, 1);
                        if (letter == ",")
                        {
                            cleanLine.Append(splitter + "");
                            count++;
                            pntPos++;
                            if (pntPos < line.Length)
                            {
                                letter = line.Substring(pntPos, 1);
                            }
                            field = "";
                        }
                        if (letter == "\"")
                        {
                            //tiene comillas
                            pntComilla = line.IndexOf("\"", pntPos + 1);
                            if (pntComilla > 0)
                            {
                                field = line.Substring(pntPos + 1, (pntComilla - pntPos - 1));
                            }
                            else
                            {
                                field = line.Substring(pntPos + 1, (line.Length - pntPos - 1));
                                pntComilla = line.Length;
                            }
                            field = field.Replace(",", "[%c_comma]");
                            cleanLine.Append(splitter + field);
                            count++;
                            pntPos = pntComilla + 1;
                            if (pntPos < line.Length)
                            {
                                letter = line.Substring(pntPos, 1);
                            }
                            else
                            {
                                letter = "\r";
                            }
                            //Letter = Line.Substring(pntPos, 1);
                            if (letter == ",")
                            {
                                pntPos++;
                                if (pntPos < line.Length)
                                {
                                    letter = line.Substring(pntPos, 1);
                                }
                            }
                            if (letter == "\r")
                            {
                                pntPos = line.Length + 1;
                            }
                            //Field = Field;
                        }
                        if (letter != "\"" && letter != "," && letter != "\r")
                        {
                            //no tiene comillas
                            pntComa = line.IndexOf(",", pntPos);
                            if (pntComa > pntPos)
                            {
                                field = line.Substring(pntPos, (pntComa - pntPos));
                            }
                            else
                            {
                                field = line.Substring(pntPos, (line.Length - pntPos - 1));
                            }
                            field = field.Replace(",", "[%c_comma]");
                            cleanLine.Append(splitter + field);
                            count++;
                            if (pntComa > 0)
                            {
                                pntPos = pntComa + 1;
                                letter = line.Substring(pntPos, 1);
                            }
                            else
                            {
                                pntPos = line.Length + 1;
                            }
                            //Field = Field;
                        }
                        if (letter == "\r")
                        {
                            if (pntPos == line.Length - 1)
                            {
                                pntPos = line.Length + 1;
                                cleanLine.Append(splitter + "");
                            }
                        }
                    }
                    if (splitter == "")
                    {
                        if (count > 0)
                        {
                            splitter = ",";
                        }
                    }

                    letter = "";
                }

                answer = cleanLine.ToString();
            }
            catch (Exception ex)
            {
                _logger.Error("SetCSVLine, Exception:", ex);
            }
            return answer;
        }

        private string CleanCSVLine(string ine)
        {
            _logger.Info("CleanCSVLine");
            string Answer = "";
            try
            {
                int pntPos;
                int pntA;
                int pntB;
                int pntX;
                string field;

                StringBuilder cleanLine = new StringBuilder();
                pntPos = 0;
                pntA = ine.IndexOf("\"", pntPos);
                if (pntA >= 0 && pntA < ine.Length)
                {
                    cleanLine.Append(ine.Substring(0, pntA - 1));
                }
                else
                {
                    return ine;
                }
                pntPos = 0;
                while (pntPos < ine.Length)
                {
                    pntA = ine.IndexOf("\"", pntPos);

                    if (pntA == -1)
                    {
                        pntPos++;
                        if (pntPos < ine.Length)
                        {
                            //Field = Line.Substring(pntPos, ((Line.Length - 2) - pntPos + 1));
                            field = ine.Substring(pntPos, 2);
                            field = ine.Substring(pntPos, ((ine.Length - 1) - pntPos));
                            field = field.Replace("\"", "");
                            field = field.Replace("\" ", "");
                            field = field.Replace(" \"", "");
                            field = field.Replace(",", "[%c_comma]");
                            cleanLine.Append("," + field);
                        }
                        pntPos = ine.Length;
                    }

                    if (pntA >= 0 && pntA < ine.Length)
                    {
                        pntB = ine.IndexOf("\"", pntA + 1);
                        if (pntB >= 0 && pntB < ine.Length)
                        {
                            field = ine.Substring(pntA, (pntB - pntA + 1));
                            field = field.Replace("\"", "");
                            field = field.Replace("\" ", "");
                            field = field.Replace(" \"", "");
                            field = field.Replace(",", "[%c_comma]");
                            cleanLine.Append("," + field);

                            pntX = ine.IndexOf(",", pntA + 1);

                            //pntPos = pntB + 1;
                            pntPos = pntB;
                        }
                        else
                        {
                            pntPos = pntA + 1;
                        }
                    }
                    else
                    {
                        pntPos++;
                    }

                }

                Answer = cleanLine.ToString();
            }
            catch (Exception ex)
            {
                _logger.Error("GetFields, Exception:", ex);
            }
            return Answer;
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