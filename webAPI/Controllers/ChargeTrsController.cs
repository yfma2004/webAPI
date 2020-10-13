using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace webAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChargeTrsController : BaseController
    {
        public IHttpContextAccessor Accessor;
        public ChargeTrsController(IHttpContextAccessor accessor)
        {
            Accessor = accessor;
        }
    }
}
