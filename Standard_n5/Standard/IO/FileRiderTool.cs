using System;
using System.Collections.Generic;
using System.Text;

using log4net;

namespace maqdel.Infra.IO
{
    public class FileRiderTool
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(FileRiderTool));

        private StringBuilder internalLog = new StringBuilder();

        public DateTime ProcessStart;
        public DateTime ProcessEnd;

        public bool ProcessRunning { get; set; }

        private bool showLog = true;


        public string Path { get; set; }
        public string Drive { get; set; }
        public long RideDepth { get; set; }

        public long CountDirectories { get; set; }
        public long CountDirectoriesErrors { get; set; }
        public long CountFiles { get; set; }

        public long CountSearchFiles { get; set; }

        private List<string> lstDrivers { get; set; }
        private List<string> lstDirectories { get; set; }
        //public List<string> lstDirectoriesEmpty { get; set; }
        private List<string> lstDirectoriesErrors { get; set; }
        private List<string> lstFiles { get; set; }

        private List<string> lstSearchFiles { get; set; }


        public int LevelOneCount { get; set; }
        public int LevelOnePointer { get; set; }
        public string LevelOnePath { get; set; }
        public int LevelTwoCount { get; set; }
        public int LevelTwoPointer { get; set; }
        public string LevelTwoPath { get; set; }

        public int LevelThreeCount { get; set; }
        public int LevelThreePointer { get; set; }
        public string LevelThreePath { get; set; }

        public string Status { get; set; }

        public string InitialPath { get; set; }
        public string SearchCriteria { get; set; }

        public FileRiderTool()
        {
            _logger.Info("FileRiderTool");
            try
            {
                this.ProcessRunning = false;    
            }
            catch (Exception ex)
            {
                _logger.Error("FileRiderTool, Exception:", ex);
            }
        }
        
        private void LogAddLine(string line)
        {
            _logger.Info("LogAddLine");
            try
            {
                if (this.showLog)
                {
                    this.internalLog.AppendLine(line);
                }

            }
            catch (Exception ex)
            {
                _logger.Error("LogAddLine, Exception:", ex);
            }
        }

        private void LogAddText(string text)
        {
            _logger.Info("LogAddText");
            try
            {
                if (this.showLog)
                {
                    this.internalLog.Append(text);
                }

            }
            catch (Exception ex)
            {
                _logger.Error("LogAddText, Exception:", ex);
            }
        }

        private void LogClean()
        {
            _logger.Info("LogClean");
            try
            {
                if (this.showLog)
                {
                    this.internalLog = new StringBuilder();
                }

            }
            catch (Exception ex)
            {
                _logger.Error("LogClean, Exception:", ex);
            }
        }

        private void RidePath(string path, int depth)
        {
            _logger.Info("RidePath");
            string[] fileEntries;
            string[] Directories;

            try
            {
                this.RideDepth = depth;
                this.Path = path;

                if (depth == 2 || depth == 3)
                {
                    this.Status = "Getting files...";
                }



                fileEntries = System.IO.Directory.GetFiles(path);

                if (fileEntries.Length > 0)
                {
                    foreach (string fileName in fileEntries)
                    {
                        this.lstFiles.Add(fileName);
                        this.CountFiles++;
                    }

                }

                this.lstDirectories.Add(path);
                this.CountDirectories++;

                if (depth == 2 || depth == 3)
                {
                    this.Status = "Getting directories";
                }


                Directories = System.IO.Directory.GetDirectories(@"" + path);

                if (depth == 2)
                {
                    this.LevelTwoCount = Directories.Length;
                    this.LevelTwoPointer = 0;
                    this.LevelTwoPath = "";

                    this.LevelThreeCount = Directories.Length;
                    this.LevelThreePointer = 0;
                    this.LevelThreePath = "";
                }
                if (depth == 3)
                {
                    this.LevelThreeCount = Directories.Length;
                    this.LevelThreePointer = 0;
                    this.LevelThreePath = "";
                }

                if (Directories.Length > 0)
                {

                    int ptrDirectory = 1;
                    foreach (string dir in Directories)
                    {
                        if (depth == 2)
                        {
                            this.LevelTwoPointer = ptrDirectory;
                            this.LevelTwoPath = dir;

                            this.LevelThreePointer = 0;
                            this.LevelThreePath = "";
                        }
                        if (depth == 3)
                        {
                            this.LevelThreePointer = ptrDirectory;
                            this.LevelThreePath = dir;
                        }

                        this.RidePath(dir, depth + 1);
                        ptrDirectory++;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("RidePath, Exception:", ex);
                this.lstDirectoriesErrors.Add(path + " " + ex.Message);
                this.CountDirectoriesErrors++;
            }
            fileEntries = null;
            Directories = null;
        }

        private void FullRide()
        {
            _logger.Info("FullRide");
            //if (!this.ProcessRunning) {

            System.Diagnostics.Process.GetCurrentProcess().PriorityClass = System.Diagnostics.ProcessPriorityClass.RealTime;

            System.IO.DriveInfo[] Drives;

            this.ProcessStart = DateTime.Now;
            this.ProcessEnd = DateTime.Now;
            this.ProcessRunning = true;

            try
            {
                this.LogClean();
                this.LogAddLine("Full ride");
                this.InitialPath = "All drives";

                //this.Status = "Full ride";

                this.lstDrivers = new List<string>();
                this.lstDirectories = new List<string>();
                //this.lstDirectoriesEmpty = new List<string>();
                this.lstDirectoriesErrors = new List<string>();
                this.lstFiles = new List<string>();

                this.CountDirectories = 0;
                this.CountFiles = 0;

                this.LevelOneCount = 0;
                this.LevelOnePointer = 0;

                this.LevelTwoCount = 0;
                this.LevelTwoPointer = 0;

                this.LevelThreeCount = 0;
                this.LevelThreePointer = 0;

                //this.Status = "Getting drives...";

                Drives = System.IO.DriveInfo.GetDrives();

                this.LevelOneCount = Drives.Length;

                if (Drives.Length > 0)
                {

                    int ptrDriver = 1;
                    foreach (System.IO.DriveInfo drive in Drives)
                    {
                        this.LevelOnePointer = ptrDriver;
                        this.LevelOnePath = drive.Name;

                        this.Drive = drive.Name;
                        this.LogAddLine(ptrDriver + " driver " + drive.Name);

                        if (drive.IsReady == true)
                        {
                            this.LogAddText(": Ready");
                            this.LogAddText(" (DriveType: " + drive.DriveType);
                            this.LogAddText(" - VolumeLabel: " + drive.VolumeLabel);
                            this.LogAddText(" - DriveFormat: " + drive.DriveFormat + ")");
                            this.lstDrivers.Add(drive.Name);

                            this.RidePath(drive.Name, 1);

                        }
                        else
                        {
                            this.LogAddText(": Not ready");
                        }
                        ptrDriver++;
                    }



                    this.LevelOneCount = 0;
                    this.LevelOnePointer = 0;
                    this.LogAddLine("");
                    this.LogAddLine("Resume");
                    this.LogAddLine("");
                    this.LogAddLine("Files: " + this.CountFiles.ToString());
                    this.LogAddLine("Directories: " + this.CountDirectories.ToString());
                    this.LogAddLine("Unacces directories: " + this.CountDirectoriesErrors.ToString());
                    this.LogAddLine("");


                }

                this.LevelOneCount = 0;
                this.LevelOnePointer = 0;

                this.LevelTwoCount = 0;
                this.LevelTwoPointer = 0;

                this.LevelThreeCount = 0;
                this.LevelThreePointer = 0;


            }
            catch (Exception ex)
            {
                _logger.Error("FullRide, Exception:", ex);
            }
            this.ProcessRunning = false;
            this.ProcessEnd = DateTime.Now;
            Drives = null;
            //}  

            System.Diagnostics.Process.GetCurrentProcess().PriorityClass = System.Diagnostics.ProcessPriorityClass.Normal;

        }

        public void ExecuteFullRide()
        {
            _logger.Info("ExecuteFullRide");
            try
            {
                if (!this.ProcessRunning)
                {
                    System.Threading.Thread t = new System.Threading.Thread(this.FullRide);
                    t.Start();
                }



            }
            catch (Exception ex)
            {
                _logger.Error("ExecuteFullRide, Exception:", ex);
            }
        }

        private void SearchPath(string path, int depth)
        {
            _logger.Info("SearchPath");
            string[] fileEntries;
            string[] Directories;

            try
            {
                this.RideDepth = depth;
                this.Path = path;

                fileEntries = System.IO.Directory.GetFiles(path);

                if (fileEntries.Length > 0)
                {
                    foreach (string fileName in fileEntries)
                    {
                        this.lstFiles.Add(fileName);
                        this.CountFiles++;

                        if (fileName.IndexOf(this.SearchCriteria) >= 0)
                        {
                            this.lstSearchFiles.Add(fileName);
                            this.LogAddLine(fileName);
                            this.CountSearchFiles++;
                        }
                    }
                }

                this.lstDirectories.Add(path);
                this.CountDirectories++;

                Directories = System.IO.Directory.GetDirectories(@"" + path);

                if (depth == 1)
                {
                    this.LevelOneCount = Directories.Length;
                    this.LevelOnePointer = 0;
                    this.LevelOnePath = "";
                }
                if (depth == 2)
                {
                    this.LevelTwoCount = Directories.Length;
                    this.LevelTwoPointer = 0;
                    this.LevelTwoPath = "";
                }

                if (Directories.Length > 0)
                {

                    int ptrDirectory = 1;
                    foreach (string dir in Directories)
                    {
                        if (depth == 1)
                        {
                            this.LevelOnePointer = ptrDirectory;
                            this.LevelOnePath = dir;
                        }
                        if (depth == 2)
                        {
                            this.LevelTwoPointer = ptrDirectory;
                            this.LevelTwoPath = dir;
                        }
                        this.SearchPath(dir, depth + 1);
                        ptrDirectory++;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("SearchPath, Exception:", ex);
                                this.lstDirectoriesErrors.Add(path + " " + ex.Message);
                this.CountDirectoriesErrors++;
            }            
            fileEntries = null;
            Directories = null;
        }

        private void PathRide()
        {
            _logger.Info("PathRide");
            //if (!this.ProcessRunning) {
            //System.IO.DriveInfo[] Drives;


            System.Diagnostics.Process.GetCurrentProcess().PriorityClass = System.Diagnostics.ProcessPriorityClass.RealTime;

            this.ProcessStart = DateTime.Now;
            this.ProcessEnd = DateTime.Now;
            this.ProcessRunning = true;

            try
            {
                this.LogClean();
                this.LogAddLine("Buscando");

                //this.lstDrivers = new List<string>();
                this.lstDirectories = new List<string>();
                //this.lstDirectoriesEmpty = new List<string>();
                this.lstDirectoriesErrors = new List<string>();
                this.lstFiles = new List<string>();

                this.lstSearchFiles = new List<string>();

                this.CountDirectories = 0;
                this.CountFiles = 0;
                this.CountSearchFiles = 0;

                this.LevelOneCount = 0;
                this.LevelOnePointer = 0;

                this.LevelTwoCount = 0;
                this.LevelTwoPointer = 0;

                this.SearchPath(this.InitialPath, 1);

                this.LevelOneCount = 0;
                this.LevelOnePointer = 0;

                this.LevelTwoCount = 0;
                this.LevelTwoPointer = 0;


                this.LogAddLine("");
                this.LogAddLine("Resume");
                this.LogAddLine("");
                this.LogAddLine("Files: " + this.CountFiles.ToString());
                this.LogAddLine("Directories: " + this.CountDirectories.ToString());
                this.LogAddLine("Unacces directories: " + this.CountDirectoriesErrors.ToString());
                this.LogAddLine("");
            }
            catch (Exception ex)
            {
                _logger.Error("PathRide, Exception:", ex);
            }
            this.ProcessRunning = false;
            this.ProcessEnd = DateTime.Now;

            System.Diagnostics.Process.GetCurrentProcess().PriorityClass = System.Diagnostics.ProcessPriorityClass.Normal;

            //Drives = null;
            //}            
        }

        public void ExecutePathRide(string InitialPath, string SearchCriteria)
        {
            _logger.Info("ExecutePathRide");
            try
            {
                if (!this.ProcessRunning)
                {
                    this.InitialPath = InitialPath;
                    this.SearchCriteria = SearchCriteria;

                    System.Threading.Thread t = new System.Threading.Thread(this.PathRide);
                    t.Start();
                }


            }
            catch (Exception ex)
            {
                _logger.Error("ExecutePathRide, Exception:", ex);
            }
        }

        public string GetLog()
        {
            _logger.Info("GetLog");
            string Answer = "";
            try
            {
                Answer = this.internalLog.ToString();
            }
            catch (Exception ex)
            {
                _logger.Error("GetLog, Exception:", ex);
            }
            return Answer;
        }

        public long SearchOnRide(string Text)
        {
            _logger.Info("SearchOnRide");
            long Answer = 0;
            try
            {
                this.lstSearchFiles = new List<string>();

                this.LogClean();

                if (this.lstFiles != null)
                {
                    if (this.lstFiles.Count > 0)
                    {
                        this.LogAddLine("Buscando en " + this.lstFiles.Count.ToString() + " archivo(s)");
                        foreach (string Line in this.lstFiles)
                        {
                            if (Line.IndexOf(Text) >= 0)
                            {
                                Answer++;
                                this.lstSearchFiles.Add(Line);
                                this.LogAddLine(Line);
                            }
                        }
                    }
                    else
                    {
                        this.LogAddLine("La lista ride esta vacia");
                    }
                }
                else
                {
                    this.LogAddLine("No hay lista ride");
                }
            }
            catch (Exception ex)
            {
                _logger.Error("SearchOnRide, Exception:", ex);
            }
            return Answer;
        }

        public System.Data.DataTable GetSearchFiles()
        {
            _logger.Info("GetSearchFiles");
            System.Diagnostics.Process.GetCurrentProcess().PriorityClass = System.Diagnostics.ProcessPriorityClass.RealTime;
            System.Data.DataTable Answer = new System.Data.DataTable();
            try
            {
                System.Data.DataTable DT = new System.Data.DataTable();
                DT.Columns.Add("File");
                if (this.lstSearchFiles != null)
                {

                    foreach (string Line in this.lstSearchFiles)
                    {
                        DT.Rows.Add(Line);
                    }
                }
                Answer = DT;
            }
            catch (Exception ex)
            {
                _logger.Error("GetSearchFiles, Exception:", ex);
            }
            System.Diagnostics.Process.GetCurrentProcess().PriorityClass = System.Diagnostics.ProcessPriorityClass.Normal;
            return Answer;

        }

        public System.Data.DataTable GetFiles()
        {
            _logger.Info("GetFiles");
            System.Diagnostics.Process.GetCurrentProcess().PriorityClass = System.Diagnostics.ProcessPriorityClass.RealTime;
            System.Data.DataTable Answer = new System.Data.DataTable();
            try
            {
                System.Data.DataTable DT = new System.Data.DataTable();
                DT.Columns.Add("File");
                if (this.lstFiles != null)
                {
                    foreach (string Line in this.lstFiles)
                    {
                        DT.Rows.Add(Line);
                    }
                }
                Answer = DT;
            }
            catch (Exception ex)
            {
                _logger.Error("GetFiles, Exception:", ex);
            }
            System.Diagnostics.Process.GetCurrentProcess().PriorityClass = System.Diagnostics.ProcessPriorityClass.Normal;
            return Answer;
        }

        public System.Data.DataTable GetDirectories()
        {
            _logger.Info("GetDirectories");
            System.Diagnostics.Process.GetCurrentProcess().PriorityClass = System.Diagnostics.ProcessPriorityClass.RealTime;
            System.Data.DataTable Answer = new System.Data.DataTable();
            try
            {
                System.Data.DataTable DT = new System.Data.DataTable();
                DT.Columns.Add("File");
                if (this.lstDirectories != null)
                {
                    foreach (string Line in this.lstDirectories)
                    {
                        DT.Rows.Add(Line);
                    }
                }
                Answer = DT;
            }
            catch (Exception ex)
            {
                _logger.Error("GetDirectories, Exception:", ex);
            }
            System.Diagnostics.Process.GetCurrentProcess().PriorityClass = System.Diagnostics.ProcessPriorityClass.Normal;
            return Answer;
        }

        public System.Data.DataTable GetUnAccessDirectories()
        {
            _logger.Info("GetUnAccessDirectories");
            System.Diagnostics.Process.GetCurrentProcess().PriorityClass = System.Diagnostics.ProcessPriorityClass.RealTime;
            System.Data.DataTable Answer = new System.Data.DataTable();
            try
            {
                System.Data.DataTable DT = new System.Data.DataTable();
                DT.Columns.Add("File");
                if (this.lstDirectoriesErrors != null)
                {
                    foreach (string Line in this.lstDirectoriesErrors)
                    {
                        DT.Rows.Add(Line);
                    }
                }
                Answer = DT;
            }
            catch (Exception ex)
            {
                _logger.Error("GetUnAccessDirectories, Exception:", ex);
            }
            System.Diagnostics.Process.GetCurrentProcess().PriorityClass = System.Diagnostics.ProcessPriorityClass.Normal;
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
