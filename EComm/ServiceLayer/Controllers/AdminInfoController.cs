using DAL.DataAccess;
using DAL.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [EnableCors("AllowGetAndPost")]
    
    public class AdminInfoController : ControllerBase
    {
        private readonly IAdminInfoService<AdminInfo> _adminInfoService;
        private readonly IConfiguration _configuration;
        public AdminInfoController(IAdminInfoService<AdminInfo> adminInfoService,IConfiguration configuration)
        {
            this._adminInfoService = adminInfoService;
            this._configuration = configuration;
        }

        [HttpPost("ValidateAdmin")]
        public async Task<IActionResult> ValidateAdmin([FromBody]AdminInfo adminInfo)
        {
          var isValid=await  _adminInfoService.ValidateAdmin(adminInfo);
            if (isValid)
            {
                //Generate Token
                var token=  GenerateToken(adminInfo);
                return Ok(token);//2000
            }
            else
            {
                return Unauthorized();//401
            }
        }

        [NonAction]
        public string GenerateToken(AdminInfo adminInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signingCredential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            var claims = new[]
            {
                new Claim(ClaimTypes.Email,adminInfo.EmailId),
                new Claim(ClaimTypes.Role,adminInfo.Role)
            };

            var token = new JwtSecurityToken(issuer, audience, claims, expires: DateTime.Now.AddMinutes(30), signingCredentials: signingCredential);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
