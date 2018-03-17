using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyImageDFS.Model
{
    public interface IImageInfoRepsitory
    {
        ImageStatusEnum Add(ImageInfo model);
    }

    public class ImageInfoRepository : IImageInfoRepsitory
    {
        MyImageServerEntities db;

        public ImageInfoRepository()
        {
            db = new MyImageServerEntities();
        }

        public ImageStatusEnum Add(ImageInfo model)
        {
            db.ImageInfo.Add(model);
            int result = db.SaveChanges();

            if(result > 0)
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
