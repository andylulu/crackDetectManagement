
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            var code = new sys_codeService();
            var model = new
            {
                dataSource = new{
                    dsPricing = code.GetValueTextListByType("Pricing")
                },
                urls = new{
                    query = "/api/Pyn/CamaraType",
                    newkey = "/api/Pyn/CamaraType/newkey",
                    edit = "/api/Pyn/CamaraType/edit"
                },
                resx = new{
                    noneSelect = "请先选择一条数据！",
                    editSuccess = "保存成功！",
                    auditSuccess = "单据已审核！"
                },
                form = new{
                    ID = "" ,
                    Serie = "" ,
                    Type = "" ,
                    Description = "" ,
                    IsEnable = "" ,
                    CreatePerson = "" ,
                    CreateDate = "" ,
                    UpdatePerson = "" ,
                    UpdateDate = "" 
                },
                defaultRow = new {
                   
                },
                setting = new{
                    idField = "ID",
                    postListFields = new string[] { "ID" ,"Serie" ,"Type" ,"Description" ,"IsEnable" ,"CreatePerson" ,"CreateDate" ,"UpdatePerson" ,"UpdateDate" }
                }
            };

            return View(model);
        }
    }

    public class CamaraTypeApiController : ApiController
    {
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
        <field name='CreateDate'		cp='equal'></field>   
        <field name='UpdatePerson'		cp='equal'></field>   
        <field name='UpdateDate'		cp='equal'></field>   
    </where>
</settings>");
            var service = new CamaraTypeService();
            var pQuery = query.ToParamQuery();
            var result = service.GetDynamicListWithPaging(pQuery);
            return result;
        }

        public string GetNewKey()
        {
            return new CamaraTypeService().GetNewKey("ID", "maxplus").PadLeft(6, '0'); ;
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
