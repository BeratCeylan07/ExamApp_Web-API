using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using btk_exam_project_api.CustomModels;
using btk_exam_project_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace btk_exam_project_api.Controllers
{
    [Route("api/teacher/[action]")]
    [ApiController]
    [Authorize]
    public class TeacherAPIController : Controller
    {
        private readonly SnDbContext _context;
        private readonly IConfiguration _configuration;
        public TeacherAPIController(SnDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Kullanicilar>>> TeacherList(int subeID, int role)
        {
            return await _context.Kullanicilars.Where(x => x.SubeId == subeID && x.Role == 2).Select(s => new Kullanicilar()
            {
                Id = s.Id,
                Ad = s.Ad,
                Soyad = s.Soyad,
                Ders = s.Ders,
                UserDersSets = s.UserDersSets,
                IsActive = s.IsActive,
                Eposta = s.Eposta,
                Uid = s.Uid
            }).OrderBy(o => o.Ad).ToListAsync();
        }
        [HttpPost]
        public async Task<ActionResult<Teacher_Post_Model>> NewTeacher([FromBody] Teacher_Post_Model model)
        {
            var name_surname = TR_FIX(model.Ad, model.Soyad);
            var teacher = new Kullanicilar
            {
                Uid = Guid.NewGuid().ToString(),
                Ad = name_surname.Ad,
                Soyad = name_surname.Soyad,
                Tel = model.Tel,
                Eposta = model.Eposta,
                IsActive = true,
                IsCreatedUserid = model.userID,
                IsModifiedUserid = model.userID,
                IsCreatedDate = DateTime.Now,
                IsModifiedDate = DateTime.Now,
                Role = 2,
                KullaniciAdi = name_surname.Ad.Substring(0, 2) + name_surname.Soyad.Substring(0, 2),
                Sifre = createPassword(),
                SubeId = model.subeID
            };
            await _context.Kullanicilars.AddAsync(teacher);
            await _context.SaveChangesAsync();
            int userID = await _context.Kullanicilars.Where(u => u.Uid == teacher.Uid).Select(s => s.Id).FirstAsync();
            int dersID = await _context.Ders.Where(u => u.Uid == model.dersUID).Select(s => s.Id).FirstAsync();

            var teacher_ders_set = new UserDersSet
            {
                Uid = Guid.NewGuid().ToString(),
                Userid = userID,
                Dersid = dersID,
                IsCreatedDate = DateTime.Now,
                IsCreatedUserId = model.userID,
                IsModifiedDate = DateTime.Now,
                IsModifiedUserId = model.userID,
                IsActive = true
            };
            await _context.UserDersSets.AddAsync(teacher_ders_set);
            await _context.SaveChangesAsync();

            return Ok();
        }

        string createPassword()
        {
            Random rnd = new Random();
            int result = rnd.Next(100000, 999999);
            return result.ToString();
        }

        TRNameSurname TR_FIX(string ad, string soyad)
        {
            ad = ad.ToLower();
            ad = ad.Replace('ö', 'o');
            ad = ad.Replace('ü', 'u');
            ad = ad.Replace('ğ', 'g');
            ad = ad.Replace('ş', 's');
            ad = ad.Replace('ı', 'i');
            ad = ad.Replace('ç', 'c');
            ad = ad.Replace(' ', '_');

            soyad = soyad.ToLower();
            soyad = soyad.Replace('ö', 'o');
            soyad = soyad.Replace('ü', 'u');
            soyad = soyad.Replace('ğ', 'g');
            soyad = soyad.Replace('ş', 's');
            soyad = soyad.Replace('ı', 'i');
            soyad = soyad.Replace('ç', 'c');
            soyad = soyad.Replace(' ', '_');

            var result = new TRNameSurname
            {
                Ad = ad,
                Soyad = soyad
            };
            return result;

        }

        public class TRNameSurname
        {
            public string Ad { get; set; }
            public string Soyad { get; set; }
        }
    }
}

