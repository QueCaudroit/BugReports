using System;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;

namespace BugReportModule.Secret
{
    public static class JwtCredentials
    {
        public static SigningCredentials credentials;
        public static TokenValidationParameters validationParameters;
        static JwtCredentials()
        {
            var secret = "zjXqmH4grB3EFrc6Vd7KAob5gQFFo4wgigVuIAmxNuHThUspCbKern8gCz3dVRStNO1qJNLzPjpujjorKmnanw==";
            var symmetricKey = Convert.FromBase64String(secret);
            var key = new SymmetricSecurityKey(symmetricKey);
            credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            validationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = key,
                ValidateAudience = false,
                ValidateIssuer = false,
            };
        }
    }
}
