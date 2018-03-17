using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Zephyr.Core;
using Zephyr.Data;
using Zephyr.Models;
using Zephyr.Utils;
using Zephyr.Web;

namespace Zephyr.Areas.Sys.Controllers
{
    public class BackupController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        
    }

    public class BackupApiController : ApiController
    {
        [System.Web.Http.HttpPost]
        public void Post()
        {
         //   var fileStream = LogReader.ReadFileStream(Server.MapPath("~/databases/" + id + ".bak"));
         //   return File(fileStream, "text/plain", id + ".bak");
            string dbname = ConfigurationManager.AppSettings["dbname"];
            string backupName = DateTime.Now.ToString("yyyyMMddHHmmss");
            string savePath = HttpContext.Current.Server.MapPath("/databases/");

            //string backupSql1 = "BACKUP LOG '{0}' WITH NO_LOG";
            string backupSql = "BACKUP LOG [{0}] WITH NO_LOG;BACKUP DATABASE [{0}] to DISK ='{1}'";
            //backupSql = string.Format(backupSql, dbname, savePath + backupName + ".bak");

             
            //IDbContext dbCommand = new EditEventArgs().db;
            //dbCommand.Sql(backupSql).Execute();

            //Db.Context().Sql(String.Format(@backupSql1, dbname)).Execute();
            Db.Context().Sql(string.Format(@backupSql, dbname, savePath + backupName + ".bak")).Execute();

           // DbCommand dbCommand = new DbCommand();
            //IDbContext.Sql(backupSql);
        }
        public dynamic GetSystemBackup(RequestWrapper request) 
        {
            var page = ZConvert.To<int>(request["page"], 1);
            var rows = ZConvert.To<int>(request["rows"], 0);
            var backupDate = ZConvert.ToString(request["backupdate"]);

            var list = new List<dynamic>();
            var basepath = HttpContext.Current.Server.MapPath("/databases/");
            var di = new DirectoryInfo(basepath);
            if (!di.Exists) di.Create();

            string[] s = backupDate.Split('到');
            string s1 = "1990-01-01 00:00:00";
            string s2 = DateTime.Now.Date.ToString();
            switch (s.Length)
            { 
                case 1:
                    if (backupDate.Length > 0)
                    {
                        s1 = s[0];
                        s2 = s1;
                    }
                    break;
                case 2:
                    s1 = s[0];
                    s2 = s[1];
                    break;
            }

            int t1 = Convert.ToInt32(Convert.ToDateTime(s1).ToString("yyyyMMdd"));
            int t2 = Convert.ToInt32(Convert.ToDateTime(s2).ToString("yyyyMMdd"));

            foreach (var fi in di.GetFiles().Where(x => (Convert.ToInt32(x.FullName.Replace(basepath, "").Substring(0,8)) >= t1 && Convert.ToInt32(x.FullName.Replace(basepath, "").Substring(0,8)) <= t2)))
            {
                dynamic item = new ExpandoObject();
                item.filename = fi.FullName.Replace(basepath, "");
                item.size = (fi.Length / 1024).ToString() + " KB";
                item.time = fi.CreationTime.ToString();
                item.id = item.filename.Replace(".bak", "");
                list.Add(item);
            }

            var result = list.OrderByDescending(x => x.filename).Skip((page - 1) * rows).Take(rows);
            return new { rows = result, total = list.Count() };
        }
    }
}
