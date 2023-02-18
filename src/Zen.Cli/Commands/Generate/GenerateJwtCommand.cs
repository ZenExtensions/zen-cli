using System.Text;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace Zen.Cli.Commands
{
    public class GenerateJwtCommandSettings : ZenCommandSettings
    {
        [Description("Claims for jwt token")]
        [CommandOption("-c|--claim")]
        public string[]? Claims { get; set; }

        [Description("Secret for jwt token")]
        [CommandOption("-s|--secret")]
        public string? Secret { get; set; }

        [Description("Issuer for the jwt token")]
        [CommandOption("-i|--issuer")]
        public string? Issuer { get; set; }

        [Description("Audience for the jwt token")]
        [CommandOption("-a|--audience")]
        public string? Audience { get; set; }

        [Description("Expiry date for jwt token (defaults to after 15 minutes)")]
        [CommandOption("-e|--expiry")]
        public DateTime? Expiry { get; set; }

        public Dictionary<string,string> ClaimsDictionary { get; private set; } = new Dictionary<string, string>();

        public override ValidationResult Validate()
        {
            if (Claims is not null)
            {
                foreach(var claim in Claims)
                {
                    var parts = claim.Split('=');
                    if(parts.Length != 2)
                    {
                        return ValidationResult.Error($"Invalid claim format: {claim}");
                    }
                    var key = parts[0];
                    var value = parts[1];
                    if (string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(value)) 
                    {
                        return ValidationResult.Error($"Invalid claim format: {claim}");
                    }
                    ClaimsDictionary[key] = value;
                }
            }

            if (!string.IsNullOrWhiteSpace(Secret))
            {
                if (Secret.Length < 16)
                    return ValidationResult.Error("Secret must be at least 16 characters long");
            }

            if(Expiry is null)
            {
                Expiry = DateTime.UtcNow.AddMinutes(15);
            }

            if(Expiry < DateTime.UtcNow)
            {
                return ValidationResult.Error("Expiry date must be in the future");
            }

            return ValidationResult.Success();
        }
    }
    public class GenerateJwtCommand : ZenCommand<GenerateJwtCommandSettings>
    {
        public override void OnExecute(CommandContext context, CancellationToken cancellationToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var descriptor = new SecurityTokenDescriptor()
            {
                Issuer = Settings.Issuer,
                Audience = Settings.Audience,
                Expires = Settings.Expiry,
                IssuedAt = DateTime.UtcNow
            };
            if (!string.IsNullOrWhiteSpace(Settings.Secret))
            {
                var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Settings.Secret));
                descriptor.SigningCredentials = new SigningCredentials(
                    key: securityKey,
                    algorithm: SecurityAlgorithms.HmacSha256Signature
                );
            }
            descriptor.Claims = new Dictionary<string, object>();
            foreach (var (key, value) in Settings.ClaimsDictionary) 
            {
                if (long.TryParse(value, out var longVal))
                {
                    descriptor.Claims[key] = longVal;
                    continue;
                }
                if (bool.TryParse(value, out var boolVal))
                {
                    descriptor.Claims[key] = boolVal;
                    continue;
                }
                descriptor.Claims[key] = value;
            }

            var securityToken = tokenHandler.CreateToken(descriptor);
            var token = tokenHandler.WriteToken(securityToken);
            Terminal.Write(token);

        }
    }
}