using System;
using System.Collections.Generic;
using System.Text;
using Zephyr.Core;

namespace Zephyr.Models
{
    public class sys_backupService : ServiceBase<sys_backup>
    {
       
    }

    public class sys_backup : ModelBase
    {

        [PrimaryKey]
        public string PermissionCode{ get; set; }
        public string PermissionName{ get; set; }
        public string ParentCode{ get; set; }
        public string CreatePerson { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdatePerson { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
