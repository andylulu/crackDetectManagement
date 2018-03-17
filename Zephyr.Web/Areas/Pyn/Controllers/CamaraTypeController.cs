
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
    public class CamaraTypeController : Controller
    {
        public ActionResult Index()
        {
            var codeService = new sys_codeService();
            var model = new
            {
          
                urls = new {
                    query = "/api/Pyn/CamaraType",
                    //remove = "/api/Pyn/CamaraType/",
                   // billno = "/api/Pyn/CamaraType/getnewbillno",
                    //audit = "/api/Pyn/CamaraType/audit/",
                    newkey = "/api/Pyn/CamaraType/getnewkey",
                    edit = "/api/Pyn/CamaraType/getedit"
                    
                },
                resx = new {
                    detailTitle = "单据明细",
                    noneSelect = "请先选择一条单据！",
                    deleteConfirm = "确定要删除选中的单据吗？",
                    deleteSuccess = "删除成功！",
                    //auditSuccess = "单据已审核！"
                },
                dataSource = new{
                    camaraSerie = codeService.GetValueTextListByType("CamaraSerie"),
                    camaraType = codeService.GetValueTextListByType("CamaraType"),
                   // isEnable = codeService.GetValueTextListByType("IsEnable")
                },
                form = new{
                    ID = "" ,
                    Serie = "" ,
                    Type = "" ,
                    Description = "" ,
                    IsEnable = "True" ,
                    CreatePerson = "" ,
                    CreateDate = "" ,
                    UpdatePerson = "" ,
                    UpdateDate = "" 
                },
                //idField="ID"
                defaultRow = new
                {
                    
                },
                setting = new
                {
                    idField = "ID",
                    postListFields = new string[] { "ID", "Serie", "Type", "Description", "IsEnable", "CreatePerson", "CreateDate", "UpdatePerson", "UpdateDate" }
                }
            };

            return View(model);
        }
    }

    public class CamaraTypeApiController : ApiController
    {


        public string GetNewKey()
        {
            return new CamaraTypeService().GetNewKey("ID", "maxplus");
        }
        
        public dynamic Get(RequestWrapper query)
        {
            query.LoadSettingXmlString(@"
<settings defaultOrderBy='ID'>
    <select>*</select>
    <from>CamaraType</from>
    <where defaultForAll='true' defaultCp='equal' defaultIgnoreEmpty='true' >
        <field name='ID'		cp='equal'></field>   
        <field name='Serie'		cp='equal'></field>   
        <field name='Type'		cp='equal'></field>   
        <field name='Description'		cp='equal'></field>   
        <field name='IsEnable'		cp='equal'></field>   
        <field name='CreatePerson'		cp='equal'></field>   
        <field name='CreateDate'		cp='daterange'></field>   
        <field name='UpdatePerson'		cp='equal'></field>   
        <field name='UpdateDate'		cp='daterange'></field>   
    </where>
</settings>");
            var service = new CamaraTypeService();
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
        CamaraType
    </table>
    <where>
        <field name='ID' cp='equal'></field>
    </where>
</settings>");
            var service = new CamaraTypeService();
            var result = service.Edit(null, listWrapper, data);
        }
    }
}
