using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Drawing;

using System.Reflection;
using System.Diagnostics;

using log4net;

namespace maqdel.Infra.Diagnostics
{
    public static class DiagnosticsHelper
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(DiagnosticsHelper));

        /// <summary>
        /// Get assembly File Version Info
        /// </summary>
        /// <param name="Assembly"></param>
        /// <returns></returns>
        public static FileVersionInfo GetAssemblyFileVersionInfo(Assembly Assembly)
        {
            _logger.Info("GetAssemblyFileVersionInfo");
            FileVersionInfo answer = null;
            try
            {
                FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(Assembly.Location);                
                answer = fileVersionInfo;
            }
            catch (Exception ex)
            {
                _logger.Error("GetAssemblyFileVersionInfo, Exception:", ex);
            }
            return answer;
        }

        /// <summary>
        /// Get assembly Product FullName
        /// </summary>
        /// <param name="Assembly"></param>
        /// <returns></returns>
        public static string GetAssemblyProductFullName(Assembly Assembly)
        {
            _logger.Info("GetAssemblyProductFullName");
            string answer = "";
            try
            {
                FileVersionInfo fileVersionInfo = GetAssemblyFileVersionInfo(Assembly);

                var productName = fileVersionInfo.ProductName;
                var productVersion = fileVersionInfo.ProductVersion;                

                answer = productName + " " + productVersion;
            }
            catch (Exception ex)
            {
                _logger.Error("GetAssemblyProductFullName, Exception:", ex);
            }
            return answer;
        } 

        /// <summary>
        /// Get assembly Product Name
        /// </summary>
        /// <param name="Assembly"></param>
        /// <returns></returns>
        public static string GetAssemblyProductName(Assembly Assembly)
        {
            _logger.Info("GetAssemblyProductName");
            string answer = "";
            try
            {
                FileVersionInfo fileVersionInfo = GetAssemblyFileVersionInfo(Assembly);

                answer = fileVersionInfo.ProductName;
            }
            catch (Exception ex)
            {
                _logger.Error("GetAssemblyProductName, Exception:", ex);
            }
            return answer;
        } 

        /// <summary>
        /// Get assembly Product Version
        /// </summary>
        /// <param name="Assembly"></param>
        /// <returns></returns>
        public static string GetAssemblyProductVersion(Assembly Assembly)
        {
            _logger.Info("GetAssemblyProductVersion");
            string answer = "";
            try
            {
                FileVersionInfo fileVersionInfo = GetAssemblyFileVersionInfo(Assembly);

                answer = fileVersionInfo.ProductVersion;
            }
            catch (Exception ex)
            {
                _logger.Error("GetAssemblyProductVersion, Exception:", ex);
            }
            return answer;
        } 

        /// <summary>
        /// Get assembly Company
        /// </summary>
        /// <param name="Assembly"></param>
        /// <returns></returns>
        public static string GetAssemblyCompany(Assembly Assembly)
        {
            _logger.Info("GetAssemblyCompany");
            string answer = "";
            try
            {
                FileVersionInfo fileVersionInfo = GetAssemblyFileVersionInfo(Assembly);

                answer = fileVersionInfo.CompanyName;
            }
            catch (Exception ex)
            {
                _logger.Error("GetAssemblyCompany, Exception:", ex);
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