using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;

namespace MyImageDFS.Model
{
    public class ImageFacadeRepository
    {
        MyImageServerEntities db;

        public ImageFacadeRepository()
        {
            db = new MyImageServerEntities();
        }

        public ImageStatusEnum Add(ImageInfo imageEntity)
        {
            // 首先是图片信息表
            db.ImageInfo.Add(imageEntity);
            // 其次是图片服务器信息表
            ImageServerInfo serverEntity = db.ImageServerInfo.FirstOrDefault(
                s => s.ServerId == imageEntity.ImageServerId);
            if (serverEntity != null)
            {
                // 当前服务器存储数量+1
                serverEntity.CurPicAmount += 1;
            }
            // 一起提交到SQL Server数据库中
            int result = db.SaveChanges();

            if (result > 0)
            {
                return ImageStatusEnum.Successful;
            }
            else
            {
                return ImageStatusEnum.Failure;
            }
        }
    }
}
