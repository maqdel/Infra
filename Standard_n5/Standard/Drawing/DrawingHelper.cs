using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;


using log4net;
using System.Drawing;

namespace maqdel.Infra.Drawing
{
    public static class DrawingHelper
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(DrawingHelper));

        /// <summary>
        /// Convert an Image object in a bute array
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        private static byte[] ImageToByteArray(System.Drawing.Bitmap img)
        {
            _logger.Info("ImageToByteArray");
            byte[] answer = null;
            try {
                using (var stream = new MemoryStream())
                {
                    img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                    answer = stream.ToArray();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("ImageToByteArray", ex);
            }
            return answer;

        }

        public static System.Drawing.Bitmap ToImage(byte[] ByteArray)
        {
            _logger.Info("ToImage");
            Bitmap answer = null;
            try
            {
                // Create a new memory stream from the byte array
                MemoryStream memoryStream = new MemoryStream(ByteArray);

                // Create a new bitmap object from the memory stream
                Bitmap bitmap = new Bitmap(memoryStream);

                // Dispose of the memory stream
                memoryStream.Dispose();

                // Return the bitmap object
                answer =  bitmap;
            }
            catch (Exception ex)
            {
                _logger.Error("ToImage", ex);
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