using CommonUtitlity;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
//using Token.Model;

namespace webAPI.AuthHelp
{ 
    /// <summary>

    /// Token验证授权中间件

    /// </summary>

    public class TokenAuth

    {

        /// <summary>

        /// http委托

        /// </summary>

        private readonly RequestDelegate _next;

        /// <summary>

        /// 构造函数

        /// </summary>

        /// <param name="next"></param>

        public TokenAuth(RequestDelegate next)

        {

            _next = next;

        }

        /// <summary>

        /// 验证授权

        /// </summary>

        /// <param name="httpContext"></param>

        /// <returns></returns>

        public Task Invoke(HttpContext httpContext)

        {

            var headers = httpContext.Request.Headers;

            //检测是否包含'Authorization'请求头，如果不包含返回context进行下一个中间件，用于访问不需要认证的API

            if (!headers.ContainsKey("Authorization"))

            {

                return _next(httpContext);

            }

            var tokenStr = headers["Authorization"];

            try

            {

                string jwtStr = tokenStr.ToString().Substring("Bearer ".Length).Trim();

                //验证缓存中是否存在该jwt字符串

                //if (!RayPIMemoryCache.Exists(jwtStr))

                //{

                //    return httpContext.Response.WriteAsync("非法请求");

                //}

                //TokenModel tm = ((TokenModel)RayPIMemoryCache.Get(jwtStr));

                //提取tokenModel中的Sub属性进行authorize认证

                //JWT.JsonWebToken.DecodeToObject<AuthInfo>(token, secureKey);



                var token = new JwtSecurityToken(jwtStr);

                //获取到Token的一切信息
                var payload = token.Payload;
            
                var loginName = (from t in payload where t.Key == ClaimTypes.Name select t.Value).FirstOrDefault() + "";
                //var appID = (from t in payload where t.Key == ClaimTypes.Sid select t.Value).FirstOrDefault() + "";
                //var userData = (from t in payload where t.Key == ClaimTypes.UserData select t.Value).FirstOrDefault() + "";

                if (!string.IsNullOrEmpty(loginName))
                {


                    httpContext.Items.Add("LoginName", loginName);

                    
                }

                //SecurityToken token=(new JwtSecurityTokenHandler()).ReadToken(jwtStr);

                //请求Url
                var questUrl = httpContext.Request.Path.Value.ToLower();

                List<Claim> lc = new List<Claim>();

                //Claim c = new Claim(tm.Sub + "Type", tm.Sub);
                string sub = "Admin";
                Claim c = new Claim(sub + "Type", sub);
                lc.Add(c);

                //Claim c = new Claim(sub + "Type", sub);
                //lc.Add(c);

                ClaimsIdentity identity = new ClaimsIdentity(lc);

                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                httpContext.User = principal;

                return _next(httpContext);

            }

            catch (Exception)

            {

                return httpContext.Response.WriteAsync("token验证异常");

            }

        }

    }

}