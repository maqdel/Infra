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
        /// Get the File Version Info from an assembly
        /// </summary>
        /// <param name="Assembly"></param>
        /// <returns></returns>
        public static FileVersionInfo GetFileVersionInfo(Assembly Assembly)
        {
            _logger.Info("GetFileVersionInfo");
            FileVersionInfo answer = null;
            try
            {
                FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(Assembly.Location);                
                answer = fileVersionInfo;
            }
            catch (Exception ex)
            {
                _logger.Error("GetFileVersionInfo, Exception:", ex);
            }
            return answer;
        }

        /// <summary>
        /// Get the Product FullName
        /// </summary>
        /// <param name="Assembly"></param>
        /// <returns></returns>
        public static string GetProductFullName(Assembly Assembly)
        {
            _logger.Info("GetProductFullName");
            string answer = "";
            try
            {
                FileVersionInfo fileVersionInfo = GetFileVersionInfo(Assembly);

                var companyName = fileVersionInfo.CompanyName;
                var productName = fileVersionInfo.ProductName;
                var productVersion = fileVersionInfo.ProductVersion;                

                answer = productName + " " + productVersion + " ";
            }
            catch (Exception ex)
            {
                _logger.Error("GetProductFullName, Exception:", ex);
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