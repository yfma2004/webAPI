using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Input;
using Model.Models;
using Model.Out;
using Model.Search;
using Services;

namespace webAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class RoleInfoController : BaseController
    {
        /// <summary>
        /// 查询角色
        /// </summary>
        /// <param name="searchInfo">查询条件</param>      
        /// <returns></returns>
        [HttpPost("GetList")]
        public CommonRtnEntity GetList([FromBody] SearchBase<CustomersSuppliersSearch> searchInfo)
        {

            IRoleInfoServices services = new RoleInfoServices();

            int totalCount = 0;

            List<RoleInfo> list = services.QueryPage(x =>
                x.Name.Contains(searchInfo.Data.Name)                
             , ref totalCount, searchInfo.PageIndex, searchInfo.PageSize, " CreateTime desc ");

            CommonRtnEntity rtnInfo = new CommonRtnEntity()
            {
                Success = list.Count > 0,
                Data = new
                {
                    TotalCount = totalCount,
                    Data = list
                },
                Message = "查询成功！"
            };
            return rtnInfo;
        }


        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="info">角色信息</param>      
        /// <returns></returns>
        [HttpPost("Add")]
        public CommonRtnEntity Add([FromBody] InRoleInfo info)
        {
            IRoleInfoServices services = new RoleInfoServices();

            int result = 0;

            RoleInfo roleInfo = info.Info;

            //编辑
            if (roleInfo.ID > 0)
            {
                roleInfo.UpdateTime = DateTime.Now;
                services.Update(roleInfo);
                result = roleInfo.ID;
            }
            else
            {
                result = services.Add(roleInfo);
            }

            CommonRtnEntity rtnInfo = new CommonRtnEntity()
            {
                Success = result > 0,
                Data = result,
                Message = result > 0 ? "添加成功！" : "添加失败！"
            };
            return rtnInfo;
        }



        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="ids">角色id集合</param>      
        /// <returns></returns>
        [HttpPost("Delete")]
        public CommonRtnEntity Delete([FromBody] object[] ids)
        {
            IRoleInfoServices services = new RoleInfoServices();
            bool result = services.DeleteByIds(ids);
            CommonRtnEntity rtnInfo = new CommonRtnEntity()
            {
                Success = result,
                Data = result,
                Message = result ? "删除成功！" : "删除失败"
            };
            return rtnInfo;
        }



        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <param name="searchInfo">查询条件</param>      
        /// <returns></returns>
        [HttpPost("GetRoleInfo")]
        public CommonRtnEntity GetRoleInfo([FromBody]  SearchBase<string> searchInfo)
        {
            IRoleInfoServices services = new RoleInfoServices();

            RoleInfo orderInfo = services.QueryByID(searchInfo.Data);

            CommonRtnEntity rtnInfo = new CommonRtnEntity()
            {
                Success = orderInfo != null,
                Data = new
                {
                    Info = orderInfo
                },
                Message = orderInfo != null ? "查询成功！" : "查询失败！"
            };
            return rtnInfo;
        }

    }
}