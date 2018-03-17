
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
            var model = new
            {
                urls = new {
                    query = "/api/Pyn/CamaraType",
                    remove = "/api/Pyn/CamaraType/",
                    billno = "/api/Pyn/CamaraType/getnewbillno",
                    audit = "/api/Pyn/CamaraType/audit/",
                    edit = "/Pyn/CamaraType/edit/"
                },
                resx = new {
                    detailTitle = "单据明细",
                    noneSelect = "请先选择一条单据！",
                    deleteConfirm = "确定要删除选中的单据吗？",
                    deleteSuccess = "删除成功！",
                    auditSuccess = "单据已审核！"
                },
                dataSource = new{
                    //dsPurpose = new sys_codeService().GetValueTextListByType("Purpose")
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
                idField="ID"
            };

            return View(model);
        }
    }

    public class CamaraTypeApiController : ApiController
    {
        

        public string GetNewBillNo()
        {
            return new CamaraTypeService.GetNewKey("ID", "dateplus");
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
        <field name='CreateDate'		cp='startwith'></field>   
        <field name='UpdatePerson'		cp='equal'></field>   
        <field name='UpdateDate'		cp='startwith'></field>   
    </where>
</settings>");
            var service = new CamaraTypeService();
            var pQuery = query.ToParamQuery();
            var result = service.GetDynamicListWithPaging(pQuery);
            return result;
        }
    }
}
