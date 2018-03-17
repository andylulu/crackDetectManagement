using System;
using System.Collections.Generic;
using System.Text;
using Zephyr.Core;

namespace Zephyr.Models
{
    [Module("Pyn")]
    public class CamaraInfoService : ServiceBase<CamaraInfo>
    {
        public List<dynamic> GetViaductStakeIDList()
        {
            var pQuery = ParamQuery.Instance()
                .Select("ID as value,ID as text");

            return base.GetDynamicList(pQuery);
        }
        public List<dynamic> GetStakeInfoListByID(int viaductStakeID)
        {
            var pQuery = ParamQuery.Instance()
                .Select("ViaductCod,StartStakeCode,EndStakeCode,StakeInterval")
                .AndWhere("ViaductStakeID", viaductStakeID);

            return base.GetDynamicList(pQuery);
        }
    }

    public class CamaraInfo : ModelBase
    {
        [Identity]
        [PrimaryKey]   
        public string CamaraCode { get; set; }
        public int ViaductStakeID { get; set; }
        public string StartStakeCode { get; set; }
        public string EndStakeCode { get; set; }
        public byte[] CamaraIP { get; set; }
        public decimal Price { get; set; }
        public int CamaraTypeID { get; set; }
        public string Description { get; set; }
        public bool? IsEnable { get; set; }
        public string CreatePerson { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdatePerson { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string Remark { get; set; }
    }
}
