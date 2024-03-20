using System;
using System.Collections.Generic;
using System.Text;

using System.IO;

using log4net;

namespace maqdel.Infra.IO
{
    /// <summary>
    /// Tool to manage text file
    /// </summary>
    public class TextFileTool : ITextFileTool
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(TextFileTool));

        /// <summary>
        /// File name
        /// </summary>
        private string _filename;

        /// <summary>
        /// File body
        /// </summary>
        private StringBuilder _file;

        /// <summary>
        /// Constructor
        /// </summary>
        public TextFileTool()
        {
            _logger.Info("TextFileTool");
            this._file = new StringBuilder();
        }

        /// <summary>
        /// Constructor with file name
        /// </summary>
        /// <param name="Filename">File name</param>
        public TextFileTool(string Filename)
        {
            _logger.Info("TextFileTool");
            try
            {
                this._filename = Filename;
                this._file = new StringBuilder();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Constructor with file name and initial text
        /// </summary>
        /// <param name="Filename">File name</param>
        /// <param name="File">Initial text</param>
        public TextFileTool(string Filename, string File)
        {
            _logger.Info("TextFileTool");
            try
            {
                this._filename = Filename;
                this._file = new StringBuilder(File);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Clean file
        /// </summary>
        public void CleanFile()
        {
            _logger.Info("CleanFile");
            try
            {
                this._file = new StringBuilder();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Add EOL to file
        /// </summary>
        public void AddEOL()
        {
            _logger.Info("AddEOL");
            try
            {
                this._file.Append(InfraHelper.GetEOL());
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Add text to file
        /// </summary>
        /// <param name="Text">Text</param>
        public void AddText(string Text)
        {
            _logger.Info("AddText");
            try
            {
                this._file.Append(Text);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Add text with length, filler and align
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="Length"></param>
        /// <param name="Filler"></param>
        /// <param name="Align"></param>
        public void AddText(string Text, int Length, string Filler, int Align)
        {
            _logger.Info("AddText");
            try
            {
                this._file.Append(InfraHelper.FillString(Text, Filler, Length, 0));
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Add text line
        /// </summary>
        /// <param name="TextLine">Text line</param>
        public void AddTextLine(string TextLine)
        {
            _logger.Info("AddTextLine");
            try
            {
                this._file.Append(TextLine);
                this.AddEOL();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Load text file
        /// </summary>
        /// <returns>True if file is loaded</returns>
        public bool LoadFile()
        {
            _logger.Info("LoadFile");
            try
            {                
                this._file = new StringBuilder(IOHelper.OpenTextFile(this._filename));
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Get the file text
        /// </summary>
        /// <returns>string</returns>
        public string GetFileText()
        {
            _logger.Info("GetFileText");
            try
            {
                return this._file.ToString();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Get the file length
        /// </summary>
        /// <returns></returns>
        public int GetFileLength()
        {
            _logger.Info("GetFileLength");
            try
            {
                return this._file.Length;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public int GetLinesCount()
        {
            _logger.Info("GetLinesCount");
            try
            {
                string[] Lines = this._file.ToString().Split('\n');
                return Lines.Length;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public string[] GetLines()
        {
            _logger.Info("GetLines");
            string[] Answer;
            try
            {
                Answer = this._file.ToString().Split('\n');
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return Answer;
        }

        public int Find(string Text)
        {
            _logger.Info("Find");
            int Answer = -1;
            try
            {
                string source = this._file.ToString();
                Answer = source.IndexOf(Text);
            }
            catch (Exception Ex)
            {
                Answer = -1;
                throw Ex;
            }
            return Answer;
        }

        public List<int> GetPositions(string source, string searchString)
        {
            _logger.Info("GetPositions");
            List<int> ret = new List<int>();
            int len = searchString.Length;
            int start = -len;
            while (true)
            {
                start = source.IndexOf(searchString, start + len);
                if (start == -1)
                {
                    break;
                }
                else
                {
                    ret.Add(start);
                }
            }
            return ret;
        }

        public List<int> FindAll(string Text)
        {
            _logger.Info("FindAll");
            List<int> Answer = new List<int>();
            try
            {
                int len = Text.Length;
                int start = -len;
                while (true)
                {
                    start = this._file.ToString().IndexOf(Text, start + len);
                    if (start == -1)
                    {
                        break;
                    }
                    else
                    {
                        Answer.Add(start);
                    }
                }
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
            return Answer;
        }

        /// <summary>
        /// Save the to file
        /// </summary>
        /// <returns></returns>
        public bool SaveFile()
        {
            _logger.Info("SaveFile");
            try
            {
                IOHelper.SaveTextFile(this._filename, this._file.ToString());
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
