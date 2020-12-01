using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using noche.Context;
using noche.Models;
using noche.Repository;
using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;


namespace noche.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUsers _repository;

        public AuthController(IConfiguration configuration, IUsers ius)
        {
            _repository = ius;
            _configuration = configuration;
        }

        //[HttpPost]
        //[Route("login")]
        [HttpPost]
        [Route("login")]

        public ResponseNocheServices Login(Users model)
        {
            // login start
            ResponseNocheServices rm = new ResponseNocheServices();
            // Users result = new Users();
            try
            {
                rm.Data = _repository.Authsignin(model);
                rm.SetResponse(1);

                // login end

                // read secret_key from appsettings
                var secretKey = _configuration.GetValue<string>("SecretKey");
                var key = Encoding.ASCII.GetBytes(secretKey);

                // create claims users

                var claims = new ClaimsIdentity(new[]
                {
                  // new Claim("sub", "Alice"),
                  new Claim("data" ,JsonConvert.SerializeObject(rm.Data)),
                });


                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = claims,

                    // Nuestro token va a durar un día
                    Expires = DateTime.UtcNow.AddDays(30),
                    // Credenciales para generar el token usando nuestro secretykey y el algoritmo hash 256
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var createdToken = tokenHandler.CreateToken(tokenDescriptor);
                // tokenHandler.WriteToken(createdToken);
                rm.Message = (tokenHandler.WriteToken(createdToken));
            }

            catch (Exception ex)
            {
                //return BadRequest("Revisar usuario/contraseña " + ex.Message);
                rm.SetResponse(-1, "Revisar usuario/contraseña " + ex.Message);
            }

            return rm;

        }
    }
}
