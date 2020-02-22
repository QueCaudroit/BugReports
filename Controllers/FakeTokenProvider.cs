using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BugReportModule.Secret;

namespace BugReportModule.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FakeTokenProviderController : ControllerBase
    {
        private readonly ILogger<FakeTokenProviderController> _logger;

        public FakeTokenProviderController(ILogger<FakeTokenProviderController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get([FromQuery]string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("roles", role ?? "admin")
                }),
                SigningCredentials = JwtCredentials.credentials
            };

            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);

            return token;
        }
    }
}
