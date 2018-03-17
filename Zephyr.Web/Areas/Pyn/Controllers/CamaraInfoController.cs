
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Zephyr.Core;
using Zephyr.Models;

namespace Zephyr.Areas.Pyn.Controllers
{
    public class CamaraInfoController : Controller
    {
        public ActionResult Index()
        {
            var camaraInfoService = new CamaraInfoService();
            var model = new
            {
          
                urls = new {
                    query = "/api/Pyn/CamaraInfo",
                    //remove = "/api/Pyn/CamaraInfo/",
                   // billno = "/api/Pyn/CamaraInfo/getnewbillno",
                    //audit = "/api/Pyn/CamaraInfo/audit/",
                    //newkey = "/api/Pyn/CamaraInfo/getnewkey",
                    edit = "/api/Pyn/CamaraInfo/getedit"
                    
                },
                resx = new {
                    detailTitle = "单据明细",
                    noneSelect = "请先选择一条单据！",
                    deleteConfirm = "确定要删除选中的单据吗？",
                    deleteSuccess = "删除成功！",
                    //auditSuccess = "单据已审核！"
                },
                dataSource = new{
                    viaductStakeID = camaraInfoService.GetViaductStakeIDList(),
                    //startStakeCode = camaraInfoService.GetValueTextListByType("StartStakeCode"),
                    //endStakeCode = camaraInfoService.GetValueTextListByType("CamaraInfo")
                   // isEnable = codeService.GetValueTextListByType("IsEnable")
                },
                form = new{
                    CamaraCode = "",
                    ViaductStakeID = 0,
                    StartStakeCode = "",
                    EndStakeCode = "",
                    CamaraIP = "",
                    Price = 0.00,
                    CamaraTypeID = 0,
                    Description = "",
                    IsEnable = "True",
                    CreatePerson = "",
                    CreateDate = "",
                    UpdatePerson = "",
                    UpdateDate = ""

                },
                //idField="ID"
                defaultRow = new
                {
                    
                },
                setting = new
                {
                    idField = "CamaraCode",
                    postListFields = new string[] { "CamaraCode", "ViaductStakeID", "StartStakeCode", "EndStakeCode", "CamaraIP", "Price", "CamaraTypeID", "Description", "IsEnable", "CreatePerson", "CreateDate", "UpdatePerson", "UpdateDate" }
                }
            };

            return View(model);
        }
    }

    public class CamaraInfoApiController : ApiController
    {

        
        public dynamic Get(RequestWrapper query)
        {
            query.LoadSettingXmlString(@"
<settings defaultOrderBy='CamaraCode'>
    <select>*</select>
    <from>CamaraInfo</from>
    <where defaultForAll='true' defaultCp='equal' defaultIgnoreEmpty='true' >
        <field name='CamaraCode'		cp='equal'></field>   
        <field name='ViaductStakeID'		cp='equal'></field>   
        <field name='StartStakeCode'		cp='equal'></field>   
        <field name='EndStakeCode'		cp='equal'></field>
        <field name='CamaraIP'		cp='equal'></field>
        <field name='Price'		cp='equal'></field>
        <field name='CamaraTypeID'		cp='equal'></field>
        <field name='Description'		cp='equal'></field>   
        <field name='IsEnable'		cp='equal'></field>   
        <field name='CreatePerson'		cp='equal'></field>   
        <field name='CreateDate'		cp='daterange'></field>   
        <field name='UpdatePerson'		cp='equal'></field>   
        <field name='UpdateDate'		cp='daterange'></field>   
    </where>
</settings>");
            var service = new CamaraInfoService();
            var pQuery = query.ToParamQuery();
            var result = service.GetDynamicListWithPaging(pQuery);
            return result;
        }

        
        [System.Web.Http.HttpPost]
        public void GetEdit(dynamic data)
        {
            var listWrapper = RequestWrapper.Instance().LoadSettingXmlString(@"
<settings>
    <table>
        CamaraInfo
    </table>
    <where>
        <field name='CamaraCode' cp='equal'></field>
    </where>
</settings>");
            var service = new CamaraInfoService();
            var result = service.Edit(null, listWrapper, data);
        }
    }
}
