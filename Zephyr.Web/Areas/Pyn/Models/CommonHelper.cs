using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Zephyr.Areas.Pyn.Models
{
    public static class CommonHelper
    {
        #region 01.获取服务器索引号
        /// <summary>
        /// 01.获取服务器索引号
        /// </summary>
        /// <param name="serverCount">服务器数量</param>
        /// <returns>索引号</returns>
        public static int GetServerIndex(int serverCount)
        {
            Random rand = new Random();
            int randomNumber = rand.Next();
            int serverIndex = randomNumber % serverCount;

            return serverIndex;
        } 
        #endregion

        #region 02.将流转换为字节数组
        /// <summary>
        /// 02.将流转换为字节数组
        /// </summary>
        /// <param name="stream">流</param>
        /// <returns>字节数组</returns>
        public static byte[] StearmToBytes(Stream stream)
        {
            byte[] buffer = new byte[stream.Length];

            stream.Read(buffer, 0, buffer.Length);
            stream.Seek(0, SeekOrigin.Begin);

            return buffer;
        } 
        #endregion

        #region 03.检查上传的图片格式是否合法
        /// <summary>
        /// 03.检查上传的图片格式是否合法
        /// </summary>
        /// <returns>bool值</returns>
        public static bool CheckImageFormat(string extName)
        {
            if (extName == ".jpg" || extName == ".jpeg" || extName == ".png"
                || extName == ".gif" || extName == ".bmp")
            {
                return true;
            }

            return false;
        } 
        #endregion
    }
}