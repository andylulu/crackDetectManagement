using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyImageDFS.Model
{
    public interface IImageServerInfoRepsitory
    {
        List<ImageServerInfo> GetAllUseableServers();

        ImageServerInfo GetServerInfoByID(int serverID);
    }

    public class ImageServerInfoRepository : IImageServerInfoRepsitory
    {
        MyImageServerEntities db;

        public ImageServerInfoRepository()
        {
            db = new MyImageServerEntities();
        }

        public List<ImageServerInfo> GetAllUseableServers()
        {
            List<ImageServerInfo> serverList = db.ImageServerInfo
                .Where<ImageServerInfo>(s => s.FlgUsable == true
                    && s.CurPicAmount < s.MaxPicAmount)
                .ToList();

            return serverList;
        }

        public ImageServerInfo GetServerInfoByID(int serverID)
        {
            ImageServerInfo serverEntity = db.ImageServerInfo.
                Where<ImageServerInfo>(s => s.ServerId == serverID).FirstOrDefault();

            return serverEntity;
        }
    }
}
