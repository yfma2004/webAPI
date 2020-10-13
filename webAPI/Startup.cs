using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CommonUtitlity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Repository.sugar;
using Swashbuckle.AspNetCore.Swagger;
//using Token;
using webAPI.AuthHelp;

namespace webAPI
{
    public class Startup
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
             
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //序列化实体属性大小写保持原样
            services.AddMvc()
               .AddJsonOptions(opt =>
               {
                   opt.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
               });

            //时间格式转换
            services.AddMvc()
              .AddJsonOptions(opt =>
              {
                  opt.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.IsoDateTimeConverter()
                  {
                      DateTimeFormat = "yyyy-MM-dd HH:mm:ss"
                  }); ;
              });



            services.AddSingleton<Microsoft.AspNetCore.Http.IHttpContextAccessor, Microsoft.AspNetCore.Http.HttpContextAccessor>();



            #region Token


            services.AddAuthorization(options =>

            {
                options.AddPolicy("System", policy => policy.RequireClaim("SystemType").Build());

                options.AddPolicy("Client", policy => policy.RequireClaim("ClientType").Build());

                options.AddPolicy("Admin", policy => policy.RequireClaim("AdminType").Build());

            });

            #endregion



            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v0.1.0",
                    Title = "大家的 API",
                    Description = "webAPI说明文档",
                    TermsOfService = "None",
                    Contact = new Swashbuckle.AspNetCore.Swagger.Contact { Name = "街角", Email = "734649885@qq.com", Url = "" }
                });

                var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "webAPI.xml");//这个就是刚刚配置的xml文件名
                c.IncludeXmlComments(xmlPath, true);//默认的第二个参数是false，这个是controller的注释，记得修改

                var xmlModelPath = Path.Combine(basePath, "Model.xml");//这个就是Model层的xml文件名

                c.IncludeXmlComments(xmlModelPath);


                //添加header验证信息

                //c.OperationFilter<SwaggerHeader>();
                var security = new Dictionary<string, IEnumerable<string>> { { "Bearer", new string[] { } }, };

                c.AddSecurityRequirement(security);//添加一个必须的全局安全信息，和AddSecurityDefinition方法指定的方案名称要一致，这里是Bearer。

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 参数结构: \"Authorization: Bearer {token}\"",

                    Name = "Authorization",//jwt默认的参数名称

                    In = "header",//jwt默认存放Authorization信息的位置(请求头中)

                    Type = "apiKey"

                });


            });


            //new ConfigurationUtil(Configuration);

            services.AddSingleton<ConfigurationUtil>();

            //数据库配置
            BaseDBConfig.ConnectionString = Configuration.GetSection("AppSettings:SqlServerConnection").Value;

            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseMiddleware(typeof(ExceptionHandlerMiddleWare));

            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "api文档 V1");
            });
            #endregion

            //app.UseCors(builder =>
            //{
            //    builder.AllowAnyHeader();
            //    builder.AllowAnyMethod();
            //    builder.WithOrigins();
            //});

            app.UseMiddleware<TokenAuth>();

            app.UseMvc();
           

        }
    }
}
