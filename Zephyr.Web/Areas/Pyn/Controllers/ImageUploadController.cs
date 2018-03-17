
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Zephyr.Areas.Pyn.Models;
using Zephyr.Core;
using Zephyr.Models;
using System.IO;
using MyImageDFS.Model;
using System.Net;



namespace Zephyr.Areas.Pyn.Controllers
{
    public class ImageUploadController : Controller
    {
        IImageServerInfoRepsitory _imageServerInfoRepository;

        public ActionResult Index(HttpPostedFileBase FileData)
        {
            return View();
        }

        public ImageUploadController()
        {
            this._imageServerInfoRepository  = new ImageServerInfoRepository();
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Upload(HttpPostedFileBase FileData)
        {
            try
            {

                if (FileData == null || FileData.ContentLength == 0)
                {
                    return Json(new { retSts = false, message = "图片上传失败!" }, JsonRequestBehavior.AllowGet);
                }
                // 获取上传的图片名称和扩展名称
                string fileFullName = Path.GetFileName(FileData.FileName);
                string fileExtName = Path.GetExtension(fileFullName);
                if (!CommonHelper.CheckImageFormat(fileExtName))
                {
                    return Json(new { retSts = false, message = "上传图片格式错误，请重新选择！" }, JsonRequestBehavior.AllowGet);
                }
                // 获取可用的图片服务器集合
                List<ImageServerInfo> serverList = this._imageServerInfoRepository
                    .GetAllUseableServers();
                // 获取要保存的图片服务器索引号
                int serverIndex = CommonHelper.GetServerIndex(serverList.Count);
                // 获取指定图片服务器的信息
                string serverUrl = serverList[serverIndex].ServerUrl;
                int serverID = serverList[serverIndex].ServerId;
                string serverFullUrl = string.Format("http://{0}/FileUploadHandler.ashx?serverId={1}&name={2}",
                    serverUrl, serverID, fileFullName);
                // 借助WebClient上传图片到指定服务器
                WebClient client = new WebClient();
                //Uri uri = new Uri(serverFullUrl);
                client.UploadData(serverFullUrl, CommonHelper.StearmToBytes(FileData.InputStream));
               // client.UploadDataCompleted += new UploadDataCompletedEventHandler(UploadDataCallBack);
               // client.UploadDataAsync(uri, "POST", CommonHelper.StearmToBytes(FileData.InputStream));

                return Json(new { retSts = true, message = "上传完毕！" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                //Console.WriteLine("异常信息："+e);
                return Json(new { retSts = false, message = "上传过程异常！" }, JsonRequestBehavior.AllowGet);
            }
        }
        /*
        public void UploadDataCallBack(Object sender, UploadDataCompletedEventArgs e)
        {
            byte[] data = (byte[])e.Result;
            string reply = System.Text.Encoding.UTF8.GetString(data);
            Console.WriteLine(reply);
        }*/
    }
}
