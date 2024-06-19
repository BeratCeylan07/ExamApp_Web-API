using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using btk_exam_project_api.JWTModel;
using btk_exam_project_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace btk_exam_project_api.Controllers
{
    [Route("api/Auth/[action]")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly SnDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly JWTSettingsModel _jwtsettings;

        public AuthAPIController(SnDbContext context, IConfiguration configuration, IOptions<JWTSettingsModel> jwtsetting)
        {
            _context = context;
            _configuration = configuration;
            _jwtsettings = jwtsetting.Value;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAction([FromBody] UserLoginClass loginModel)
        {
            var user = await _context.Kullanicilars.FirstOrDefaultAsync(x => x.KullaniciAdi == loginModel.userName);
            if (user is not null)
            {
                if (user.Sifre == loginModel.userPassword)
                {

                    UserLoginResponseClass response = new UserLoginResponseClass
                    {
                        title = "Başarılı",
                        status = true,
                        message = "Lütfen Bekleyin Yönlendiriliyorsunuz",
                        userUID = user.Uid,
                        userID = user.Id,
                        subeID = user.SubeId,
                        Token = SetToken(user)
                    };
                    return StatusCode(200, response);
                }
                else
                {
                    UserLoginResponseClass response = new UserLoginResponseClass
                    {
                        title = "Başarısız",
                        status = false,
                        message = "Lütfen Kullanıcı Adı veya Şifrenizi Kontrol Ederek Tekrar Deneyiniz",
                        Token = null,
                        //   userModel = null
                    };
                    return StatusCode(401, response);
                }
            }
            else
            {
                UserLoginResponseClass response = new UserLoginResponseClass
                {
                    title = "Uyarı",
                    message = "Kullanıcı Bulunamadı, Bilgilerinizi Kontrol Ederek Tekrar Deneyiniz",
                    Token = null,
                    //    userModel = null
                };
                return StatusCode(401, response);
            }
        }
        private string SetToken(Kullanicilar usermodel)
        {
            if (_jwtsettings.Key == null) throw new Exception("JWT Key Değeri Null Olamaz");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtsettings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.GivenName, "Berat Ceylan - BTK Akademi"),
                new Claim(ClaimTypes.Name, usermodel.Ad),
                new Claim(ClaimTypes.Surname, usermodel.Soyad),
                new Claim(ClaimTypes.UserData, usermodel.KullaniciAdi + " " + usermodel.Sifre)
            };

            var token = new JwtSecurityToken(
                _jwtsettings.Issuer,
                _jwtsettings.Audience,
                claims,
                expires: DateTime.Now.AddDays(2),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        static string ComputeSHA256Hash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // ComputeHash - returns byte array
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2")); // Convert byte to hexadecimal string
                }
                return builder.ToString();
            }
        }
    }

    public class UserLoginClass
    {
        public string userName { get; set; }
        public string userPassword { get; set; }
    }

    public class UserLoginResponseClass
    {
        public bool status { get; set; }
        public string message { get; set; }
        public string title { get; set; }
        // public Kullanicilar userModel { get; set; }
        public string Token { get; set; }
        public string userUID { get; set; }
        public int subeID { get; set; }
        public int userID { get; set; }
    }

    public class TokenModel
    {
        public string Token { get; set; }
        public DateTime Expiry { get; set; }
    }
}

