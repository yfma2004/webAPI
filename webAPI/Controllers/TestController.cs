using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IServices;
using Services;
using Model.Models;
using Model.Out;

namespace webAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        //// GET: api/Test
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}
 

        ///// <summary>
        ///// 求和接口
        ///// </summary>
        ///// <param name="i">参数i</param>
        ///// <param name="j">参数j</param>
        ///// <returns>求和的结果</returns>
        //[HttpGet]
        //public int Get(int i, int j)
        //{
        //    IAdvertisementServices advertisementServices = new AdvertisementServices();
        //    return advertisementServices.Sum(i, j);
        //}

        // GET: api/Blog/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetById")]
        public CommonRtnEntity GetById(int id)
        {
            string aa = null;
            string bb = aa ?? "666";

            IAdvertisementServices advertisementServices = new AdvertisementServices();
            List<UserInfo> list = advertisementServices.Query(d => d.ID == id);
            CommonRtnEntity rtnInfo = new CommonRtnEntity()
            {
                Success = true,
                Data = list,
                Message = "查询成功！"
            };
            return rtnInfo;
        }

        // POST: api/Test
        /// <summary>
        /// 添加用户信息
        /// </summary>
        /// <param name="userInfo">jsong格式的用户信息对象</param>
        /// <returns></returns>
        [HttpPost]
        public CommonRtnEntity Post([FromBody] UserInfo userInfo)
        {
            IAdvertisementServices advertisementServices = new AdvertisementServices();
            int userID = advertisementServices.Add(userInfo);
            CommonRtnEntity rtnInfo = new CommonRtnEntity() {
                Success= userID>0,
                 Data=userID,
                 Message="添加成功！"
            };           

            return rtnInfo;
        }

        // PUT: api/Test/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
