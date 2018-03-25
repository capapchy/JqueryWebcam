using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace WebApplication1
{
    /// <summary>
    /// Handler1 的摘要说明
    /// </summary>
    public class Handler1 : IHttpHandler
    {

        //public void ProcessRequest(HttpContext context)
        //{
        //    context.Response.ContentType = "text/plain";
        //    context.Response.Write("Hello World");
        //}

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        #region 静态字段  
        static JavaScriptSerializer jss = new JavaScriptSerializer();
        #endregion

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";

            var imageStr = context.Request["image"].Replace("data:image/png;base64,", "").Trim();

            string fileName = DateTime.Now.Ticks.ToString();

            var path = "~/upload/" + fileName;//上传至upload文件夹  

            Base64StringToImage(path, imageStr);

            //UpFileResponseModel model = new UpFileResponseModel() { code = 1, msg = "\u6210\u529f", picUrl = "/upload/" + fileName + ".jpg" };

            context.Response.Write("OK");

        }

        private byte[] String_To_Bytes2(string strInput)
        {
            int numBytes = (strInput.Length) / 2;
            byte[] bytes = new byte[numBytes];

            for (int x = 0; x < numBytes; ++x)
            {
                bytes[x] = Convert.ToByte(strInput.Substring(x * 2, 2), 16);
            }

            return bytes;
        }

        /// <summary>  
        /// base64编码的文本 转为    图片  
        /// </summary>  
        /// <param name="txtFilePath">文件相对路径</param>  
        /// <param name="str">图片字符串</param>  
        private void Base64StringToImage(string txtFilePath, string str)
        {
            try
            {
                String inputStr = str;
                byte[] arr = Convert.FromBase64String(inputStr);
                MemoryStream ms = new MemoryStream(arr);
                Bitmap bmp = new Bitmap(ms);

                bmp.Save(System.Web.HttpContext.Current.Server.MapPath(txtFilePath) + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                //bmp.Save(txtFileName + ".bmp", ImageFormat.Bmp);  
                //bmp.Save(txtFileName + ".gif", ImageFormat.Gif);  
                //bmp.Save(txtFileName + ".png", ImageFormat.Png);  
                ms.Close();
                //imgPhoto.ImageUrl = txtFilePath + ".jpg";  
                //MessageBox.Show("转换成功！");  
            }
            catch (Exception ex)
            {

            }
        }
    }
}