using System;
using System.Collections.Generic;
using System.Text;
using Zephyr.Core;

namespace Zephyr.Models
{
    [Module("Pyn")]
    public class CamaraTypeService : ServiceBase<CamaraType>
    {
       
    }

    public class CamaraType : ModelBase
    {
        [Identity]
        [PrimaryKey]   
        public int ID { get; set; }
        public string Serie { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public bool? IsEnable { get; set; }
        public string CreatePerson { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdatePerson { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string Remark { get; set; }
    }
}
