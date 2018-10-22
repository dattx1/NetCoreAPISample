using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessAccess.Services.Interface;
using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Security;
using Security.AuthozirationAttributes;
using Security.SecurityModel;

namespace SampleNetCoreAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Blog")]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly IAuthozirationUtility _securityUtility;
        public BlogController(IBlogService blogService, IAuthozirationUtility securityUtility)
        {
            _blogService = blogService;
            _securityUtility = securityUtility;
        }

        [HttpPost]
        [CustomAuthorization(UserTypeEnum.Staff)]
        [Route("GetAllBlogs")]
        public IActionResult GetAllBlogs()
        {
            var result = _blogService.GetAllBlogs();
            return Json(ApiSecurity.Encode(result));
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("GetAccessToken")]
        public IActionResult GetAccessToken()
        {
            //Nên gọi function này ở Login Service để render access token theo login user
            var AccessToken = _securityUtility.RenderAccessToken(new current_user_access()
            {
                Email = "dattx1@gmail.com",
                ExpireTime = DateTime.Now.AddYears(1),
                UserName = "dattx1@gmail.com",
                UserType = new List<string>() { "Staff" }
            });
            return Json(AccessToken);
        }
    }
}