using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

using log4net;

namespace maqdel.Infra.IO
{
    public static class IOHelper
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(IOHelper));

        /// <summary>
        /// Validate if a file exist
        /// </summary>
        /// <param name="FileName">A file path</param>
        /// <returns>A boolean value</returns>
        public static bool FileExist(string FileName)
        {
            _logger.Info("FileExist");
            bool answer = false;
            try
            {
                if (System.IO.File.Exists(FileName))
                {
                    answer = true;
                }
                else
                {
                    answer = false;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("FileExist, Exception:", ex);
            }
            return answer;
        }

        public static bool DirectoryExist(string DirectoryPath)
        {
            _logger.Info("DirectoryExist");
            bool answer = false;
            try
            {
                if (System.IO.Directory.Exists(DirectoryPath))
                {
                    answer = true;
                }
                else
                {
                    answer = false;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("DirectoryExist, Exception:", ex);
            }
            return answer;
        }

        /// <summary>
        /// Copy a file
        /// </summary>
        /// <param name="SourceFile">A source file path</param>
        /// <param name="TargetFile">A target file path</param>
        /// <returns>A boolean value</returns>
        public static bool CopyFile(string SourceFile, string TargetFile)
        {
            _logger.Info("CopyFile");
            bool answer = false;
            try
            {
                System.IO.File.Copy(SourceFile, TargetFile);
                answer = true;
            }
            catch (Exception ex)
            {
                _logger.Error("CopyFile, Exception:", ex);
            }
            return answer;
        }

        /// <summary>
        /// Delete a file
        /// </summary>
        /// <param name="FileName">A file path</param>
        /// <returns>A boolean value</returns>
        public static bool DeleteFile(string FileName)
        {
            _logger.Info("DeleteFile");
            bool answer = false;
            try
            {
                if (System.IO.File.Exists(FileName))
                {
                    System.IO.File.Delete(FileName);
                    answer = true;
                }
                else
                {
                    answer = false;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("DeleteFile, Exception:", ex);
            }
            return answer;
        }

        /// <summary>
        /// Move a file
        /// </summary>
        /// <param name="SourceFile">A source file path</param>
        /// <param name="TargetFile">A target file path</param>
        /// <returns>A boolean value</returns>
        public static bool MoveFile(string SourceFile, string TargetFile)
        {
            _logger.Info("MoveFile");
            bool answer = false;
            try
            {
                CopyFile(SourceFile, TargetFile);
                DeleteFile(TargetFile);
                answer = true;
            }
            catch (Exception ex)
            {
                _logger.Error("MoveFile, Exception:", ex);
            }
            return answer;
        }

        /// <summary>
        /// Get the file extension from a file path
        /// </summary>
        /// <param name="FileName">A file path</param>
        /// <returns>A string</returns>
        public static string GetFileExtension(string FileName)
        {
            _logger.Info("GetFileExtension");
            string answer = "";
            try
            {
                answer = FileName.Substring(FileName.Length - 3, 3);
            }
            catch (Exception ex)
            {
                _logger.Error("GetFileExtension, Exception:", ex);
            }
            return answer;
        }

        /// <summary>
        /// Get the file extension from a file path
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="ExtensionLength"></param>
        /// <returns>A string</returns>
        public static string GetFileExtension(string FileName, int ExtensionLength)
        {
            _logger.Info("GetFileExtension");
            string answer = "";
            try
            {
                answer = FileName.Substring(FileName.Length - ExtensionLength, ExtensionLength);
            }
            catch (Exception ex)
            {
                _logger.Error("GetFileExtension, Exception:", ex);
            }
            return answer;
        }

        /// <summary>
        /// Get the file title from a file path
        /// </summary>
        /// <param name="FileName">A file path</param>
        /// <returns>A string</returns>
        public static string GetFileTitle(string FileName)
        {
            _logger.Info("GetFileTitle");
            string answer = "";
            try
            {
                int position = FileName.IndexOf(".");
                if (position > 0)
                {
                    FileName = FileName.Substring(0, position);
                }
                answer = FileName;
            }
            catch (Exception ex)
            {
                _logger.Error("GetFileTitle, Exception:", ex);
            }
            return answer;
        }

        /// <summary>
        /// Convert a file to byte array from a file path
        /// </summary>
        /// <param name="FileName">A valid file path</param>
        /// <returns>A byte array</returns>
        public static byte[] ToByteArray(string FileName)        
        {
            _logger.Info("ToByteArray");
            Byte[] answer;
            try
            {
                FileInfo fileInfo = new FileInfo(FileName);
                long fileLength = fileInfo.Length;
                fileInfo = null;
                answer = new byte[Convert.ToInt32(fileLength)];

                FileStream fileSream = new FileStream(FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                int iBytesRead = fileSream.Read(answer, 0, Convert.ToInt32(fileLength));
                fileSream.Close();
                fileSream = null;
            }
            catch (Exception ex)
            {
                _logger.Error("ToByteArray, Exception:", ex);
                answer = null;
            }
            return answer;
        }

        public static string OpenTextFile(string FilePath)
        {            
            _logger.Info("OpenTextFile");
            string answer = "";
            try
            {
                string texFileData = "";
                TextReader textReader = new StreamReader(FilePath);
                texFileData = textReader.ReadToEnd();
                textReader.Close();
                if(texFileData.Length > 0){
                    answer = texFileData;
                }                
            }
            catch (Exception ex)
            {
                _logger.Error("OpenTextFile, Exception:", ex);
            }
            return answer;
        } 


        /// <summary>
        /// Convert stream to base64
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static string StreamToBase64(Stream stream)
        {
            _logger.Info("StreamToBase64");
            string answer;
            try {
                using (var memoryStream = new MemoryStream())
                {
                    byte[] array = ((MemoryStream)stream).ToArray();
                    answer = Convert.ToBase64String(array);
                }
            }
            catch (Exception ex) {
                _logger.Error("StreamToBase64", ex);
                answer = "";
            }
            return answer;
        }

        /*         /// <summary>
                /// Get an image from a byte array
                /// </summary>
                /// <param name="ByteArray">A byte array</param>
                /// <returns>An image</returns>
                public static Image GetImage(byte[] ByteArray)
                {
                    _logger.Info("");
                    try
                    {
                        MemoryStream ms = new MemoryStream(ByteArray);
                        Image returnImage = Image.FromStream(ms);
                        return returnImage;
                    }
                    catch (Exception Ex)
                    {
                        _logger.Error(", Exception:", ex);
                    }

                }

                /// <summary>
                /// Convert an image to a byte array
                /// </summary>
                /// <param name="Image">An image</param>
                /// <returns>A byte array</returns>
                public static byte[] ToByteArray(System.Drawing.Image Image)
                {
                    _logger.Info("");
                    try
                    {
                        MemoryStream ms = new MemoryStream();
                        Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        return ms.ToArray();
                    }
                    catch (Exception Ex)
                    {
                        _logger.Error(", Exception:", ex);
                    }

                } */


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