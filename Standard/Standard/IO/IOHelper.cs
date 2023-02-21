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
        public static byte[] FileToByteArray(string FileName)
        {
            _logger.Info("FileToByteArray");
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
                _logger.Error("FileToByteArray, Exception:", ex);
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
                if (texFileData.Length > 0)
                {
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
        /// Save a string to file
        /// </summary>
        /// <param name="FileNameFullPath">Nombre del archivo</param>
        /// <param name="FileText">Texto a guardar</param>
        public static void SaveTextFile(string FileNameFullPath, string FileText)
        {
            _logger.Info("SaveTextFile");
            try
            {
                TextWriter tw = new StreamWriter(FileNameFullPath);
                tw.Write(FileText);
                tw.Close();
            }
            catch (Exception ex)
            {
                _logger.Error("SaveTextFile, Exception:", ex);
            }
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
            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    byte[] array = ((MemoryStream)stream).ToArray();
                    answer = Convert.ToBase64String(array);
                }
            }
            catch (Exception ex)
            {
                _logger.Error("StreamToBase64", ex);
                answer = "";
            }
            return answer;
        }

        /// <summary>
        /// Get StreamReader from filenae
        /// </summary>
        /// <param name="FileName">Nombre del archivo</param>
        /// <returns>StreamReader</returns>
        public static StreamReader OpenFileToStreamReader(string FileName)
        {
            _logger.Info("OpenFileToStreamReader");
            StreamReader answer = null;
            try
            {
                answer = new StreamReader(FileName);

            }
            catch (Exception ex)
            {
                _logger.Error("OpenFileToStreamReader", ex);
            }
            return answer;
        }

        /// <summary>
        /// Get a list of Drives Info
        /// </summary>
        /// <returns>List<DriveInfo></returns>
        public static List<DriveInfo> GetDrivesInfo()
        {
            _logger.Info("GetDrivesNames");
            List<DriveInfo> listDrivesInfo = new List<DriveInfo>();
            try
            {
                listDrivesInfo = DriveInfo.GetDrives().ToList();
            }
            catch (Exception ex)
            {
                _logger.Error("GetDrivesInfo, Exception:", ex);
            }
            return listDrivesInfo;
        }

        /// <summary>
        /// Get a list of drives names
        /// </summary>
        /// <returns></returns>
        public static List<string> GetDrivesNames()
        {
            _logger.Info("GetDrivesNames");
            List<string> listDrives = new List<string>();
            try
            {
                foreach (var driveInfo in GetDrivesInfo())
                {
                    listDrives.Add(driveInfo.Name);
                }
            }
            catch (Exception ex)
            {
                _logger.Error("GetDrivesNames, Exception:", ex);
            }
            return listDrives;
        }

        /// <summary>
        /// Get a list of directories on path
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public static List<string> GetPathDirectories(string Path)
        {
            _logger.Info("GetPathDirectories");
            List<string> listDirectories = new List<string>();
            try
            {
                foreach (var directory in Directory.GetDirectories(Path))
                {
                    listDirectories.Add(directory);
                }
            }
            catch (Exception ex)
            {
                _logger.Error("GetPathDirectories, Exception:", ex);
            }
            return listDirectories;
        }

        /// <summary>
        /// Get a list of files on path
        /// </summary>
        /// <param name="Path"></param>
        /// <returns>List<string></returns>
        public static List<string> GetPathFiles(string Path)
        {
            _logger.Info("GetPathFiles");
            List<string> listDirectories = new List<string>();
            try
            {
                foreach (var directory in Directory.GetFiles(Path))
                {
                    listDirectories.Add(directory);
                }
            }
            catch (Exception ex)
            {
                _logger.Error("GetPathFiles, Exception:", ex);
            }
            return listDirectories;
        }

        /// <summary>
        /// Get list of files in path and subfolder
        /// </summary>
        /// <param name="Path"></param>
        /// <returns>List<String></returns>
        public static List<String> GetPathAllFiles(string Path)
        {
            _logger.Info("GetPathAllFiles");
            List<String> listFiles = new List<String>();
            try
            {
                foreach (string file in Directory.GetFiles(Path))
                {
                    listFiles.Add(file);
                }
                foreach (string directory in Directory.GetDirectories(Path))
                {
                    listFiles.AddRange(GetPathFiles(directory));
                }
            }
            catch (System.Exception ex)
            {
                _logger.Error("GetPathAllFiles, Exception:", ex);
            }

            return listFiles;
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