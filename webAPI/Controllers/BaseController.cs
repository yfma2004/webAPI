using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Newtonsoft.Json;
using Services;

namespace webAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]//跨域
    public class BaseController : ControllerBase
    {
       
        //public JsonResult Json<T>(T content)
        //{
        //    JsonSerializerSettings jsonSerializer = new JsonSerializerSettings();
        //    jsonSerializer.Converters.Add(new Newtonsoft.Json.Converters.EntityKeyMemberConverter());


        //    jsonSerializer.Converters.Add(new Newtonsoft.Json.Converters.IsoDateTimeConverter()
        //    {
        //        DateTimeFormat = "yyyy-MM-dd HH:mm:ss"
        //    });            
        //    return new JsonResult(content, jsonSerializer);
        //}

        UserInfo _currentUserInfo;


        public UserInfo CurrentUserInfo {
            get
            {
                if (_currentUserInfo == null)
                {                    
                    UserServices userServices = new UserServices();
                    _currentUserInfo= userServices.GetUserInfo(HttpContext.Items["LoginName"] + "");
                }
                return _currentUserInfo;
            }
         }



    }
}