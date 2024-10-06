using Collegeapp1.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Collegeapp1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[EnableCors(PolicyName = "AllowOnlyGoogleApps")]
    //[Authorize(Roles = "SuperAdmin,Admin")]
    [AllowAnonymous]

    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public ActionResult Login(LoginDTO model)
        {
            if (!ModelState.IsValid)
            {
                     return BadRequest("Please provide username and password");
            }
            LoginResponseDTO response = new() { Username=model.Username};
             byte[] key = null;
                if (model.Policy == "Local")
                {
                    key =  Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JWTSecretForLocal"));
                }
                else if(model.Policy == "Microsoft")
                {
                    key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JWTSecretKeyForMicrosoft"));
                }
                else if (model.Policy == "Google")
                {
                    key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JWTSecretKeyForGoogle"));
                }
         

            if (model.Username == "Suraj" && model.Password=="12345"){
               

               

              
                var tokenhandler = new JwtSecurityTokenHandler();
                var tokenDescripter = new SecurityTokenDescriptor()
                {
                    Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                    {
                        //userrname
                        new Claim(ClaimTypes.Name, model.Username),
                        //sample Role
                        new Claim(ClaimTypes.Role,"Admin")
                    }),
                    Expires=DateTime.Now.AddHours(4),
                    SigningCredentials=new (new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha512Signature)
                };

                var token =tokenhandler.CreateToken(tokenDescripter);
                response.token=tokenhandler.WriteToken(token);
            }
            else
            {
                return Ok("Invalid username and password");
            }
            return Ok(response);
               
        }
    }
}
