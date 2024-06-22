using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using btk_exam_project_api.CustomModels;
using btk_exam_project_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;

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
        public async Task<ActionResult<IEnumerable<Kullanicilar>>> TeacherList(int subeID)
        {
            return await _context.Kullanicilars.Where(x => x.SubeId == subeID && x.Role == 2).Select(s => new Kullanicilar()
            {
                Id = s.Id,
                Ad = s.Ad,
                Soyad = s.Soyad,
                UserDersSets = s.UserDersSets.Select(c => new UserDersSet()
                {
                    Ders = c.Ders,
                }).ToList(),
                IsActive = s.IsActive,
                Eposta = s.Eposta,
                Tel = s.Tel,
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
        [HttpGet]
        public async Task<ActionResult<Teacher_Detail_Model>> TeacherInfo(string teacherUID)
        {
            return await _context.Kullanicilars.Where(x => x.Uid == teacherUID).Select(s => new Teacher_Detail_Model()
            {
                teacherID = s.Id,
                teacherUID = s.Uid,
                setedLessonUID = s.UserDersSets.Select(f => f.Ders.Uid).First(),
                Ad = s.Ad,
                Soyad = s.Soyad,
                Telefon = s.Tel,
                Eposta = s.Eposta,
                IsCreatedDate = s.IsCreatedDate,
                IsModifiedDate = s.IsModifiedDate,
                isCreatedUser = _context.Kullanicilars.Where(a => a.Id == s.IsCreatedUserid).First(),
                IsModifiedUser = _context.Kullanicilars.Where(b => b.Id == s.IsModifiedUserid).First()
            }).FirstAsync();
        }
        [HttpGet]
        public async Task<ActionResult<Teacher_Sums_Model>> TeacherSums(string teacherUID)
        {
            DateTime today = DateTime.Today;
            DateTime startOfWeek = GetStartOfWeek(today);
            DateTime endOfWeek = GetEndOfWeek(today);
            Teacher_Sums_Model sums = new Teacher_Sums_Model();
            sums.toplam_ders_tanim = _context.TeacherHaftaGunSets.Where(x => x.Teacher.Uid == teacherUID).Count();
            sums.gunluk_oturum_saat_toplam = _context.DersOturumSets.Where(x => x.Teacher.Uid == teacherUID && x.Teacher.Role == 2 && x.Baslangic.Date == DateTime.Now.Date).Sum(t => (long)Convert.ToDouble(t.Baslangic.Hour));
            sums.gunluk_oturum_toplam = _context.DersOturumSets.Where(x => x.Teacher.Uid == teacherUID && x.Teacher.Role == 2 && x.Baslangic.Date == DateTime.Now.Date).Count();
            sums.haftalik_oturum_toplam = _context.DersOturumSets.Where(x => x.Teacher.Uid == teacherUID && x.Teacher.Role == 2 && x.Baslangic >= startOfWeek && x.Baslangic <= endOfWeek).Count();
            sums.haftalik_oturum_saat_toplam = _context.DersOturumSets.Where(x => x.Teacher.Uid == teacherUID && x.Teacher.Role == 2 && x.Baslangic >= startOfWeek && x.Baslangic <= endOfWeek).Sum(t => (long)Convert.ToDouble(t.Baslangic.Hour));
            return Ok(sums);
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
            ad = ad.ToUpper();

            soyad = soyad.ToLower();
            soyad = soyad.Replace('ö', 'o');
            soyad = soyad.Replace('ü', 'u');
            soyad = soyad.Replace('ğ', 'g');
            soyad = soyad.Replace('ş', 's');
            soyad = soyad.Replace('ı', 'i');
            soyad = soyad.Replace('ç', 'c');
            soyad = soyad.ToUpper();

            var result = new TRNameSurname
            {
                Ad = ad,
                Soyad = soyad
            };
            return result;

        }
        public static DateTime GetStartOfWeek(DateTime date)
        {
            int diff = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;
            return date.AddDays(-1 * diff).Date;
        }

        public static DateTime GetEndOfWeek(DateTime date)
        {
            return GetStartOfWeek(date).AddDays(6);
        }
        public class TRNameSurname
        {
            public string Ad { get; set; }
            public string Soyad { get; set; }
        }
    }
}

