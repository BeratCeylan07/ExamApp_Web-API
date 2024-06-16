using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using btk_exam_project_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace btk_exam_project_api.Controllers
{
    [Route("api/student/[action]")]
    [ApiController]
    [Authorize]
    public class StudentAPIController : ControllerBase
    {
        private readonly SnDbContext _context;
        private readonly IConfiguration _configuration;

        public StudentAPIController(SnDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student_List_Model>>> UserList(int subeID)

        {

                return await _context.Kullanicilars.Where(x => x.SubeId == subeID && x.Role == 1).Select(s => new Student_List_Model
                {
                    userUID = s.Uid,
                    name = s.Ad,
                    surname = s.Soyad,
                    phone = s.Tel,
                }).ToListAsync();
           
        }
        [HttpGet]   
        public async Task<ActionResult<IEnumerable<DashBoardStudentListOfExam>>> DashBoardStudentList(int subeID)
        {
            return await _context.UserOturumSets.Where(x => x.User.SubeId == subeID).Select(s => new DashBoardStudentListOfExam
            {
                userUIID = s.User.Uid,
                examSetUID = s.Uid,
                studentInfo = s.User.Ad + " " + s.User.Soyad,
                examInfo = s.Oturum.DenemeSinav.DenemeAdi + " " + s.Oturum.DenemeSinav.SinavKategori,
                examSetStatus = s.Status,
                examSetDate = s.IsCreatedDate
            }).ToListAsync();
        }
        [HttpPost]
        public async Task<ActionResult<Kullanicilar>> NewStudent([FromBody] Student_Model model)
        {
            model.Ad = model.Ad.ToLower();
            model.Ad = model.Ad.Replace('ö', 'o');
            model.Ad = model.Ad.Replace('ü', 'u');
            model.Ad = model.Ad.Replace('ğ', 'g');
            model.Ad = model.Ad.Replace('ş', 's');
            model.Ad = model.Ad.Replace('ı', 'i');
            model.Ad = model.Ad.Replace('ç', 'c');
            model.Ad = model.Ad.Replace(' ', '_');

            model.Soyad = model.Soyad.ToLower();
            model.Soyad = model.Soyad.Replace('ö', 'o');
            model.Soyad = model.Soyad.Replace('ü', 'u');
            model.Soyad = model.Soyad.Replace('ğ', 'g');
            model.Soyad = model.Soyad.Replace('ş', 's');
            model.Soyad = model.Soyad.Replace('ı', 'i');
            model.Soyad = model.Soyad.Replace('ç', 'c');
            model.Soyad = model.Soyad.Replace(' ', '_');

            var student = new Kullanicilar
            {
                Uid = Guid.NewGuid().ToString(),
                Ad = model.Ad,
                Soyad = model.Soyad,
                Tel = model.Telefon,
                Eposta = model.Eposta,
                IsActive = true,
                KullaniciAdi = model.Ad.Substring(0, 2) + model.Soyad.Substring(0,2),
                Sifre = createPassword(),
                Role = model.role,
                IsCreatedDate = DateTime.Now,
                IsModifiedDate = DateTime.Now,
                IsCreatedUserid = model.userID,
                IsModifiedUserid = model.userID,
                SubeId = model.subeID
            };
            _context.Kullanicilars.Add(student);
            await _context.SaveChangesAsync();
            var actionLog = new ActionLog
            {
                ActionUid = student.Uid,
                Baslik = "Öğrenci İşlemleri",
                Aciklama = "Yeni Öğrenci Kaydı Yapıldı",
                UserId = model.userID,
                SubeId = model.subeID
            };
            postActionLog(actionLog);
            StudentReponseModel response = new StudentReponseModel
            {
                title = "İşlem Başarılı",
                message = "Öğrenci kaydı Eklendi",
                status = true,
                student = student
            };
            return StatusCode(200,response);
        }
        [HttpPost]
        public async Task<ActionResult<Student_Model>> EditUser([FromBody] Student_Model model)
        {
            model.Ad = model.Ad.ToLower();
            model.Ad = model.Ad.Replace('ö', 'o');
            model.Ad = model.Ad.Replace('ü', 'u');
            model.Ad = model.Ad.Replace('ğ', 'g');
            model.Ad = model.Ad.Replace('ş', 's');
            model.Ad = model.Ad.Replace('ı', 'i');
            model.Ad = model.Ad.Replace('ç', 'c');
            model.Ad = model.Ad.Replace(' ', '_');

            model.Soyad = model.Soyad.ToLower();
            model.Soyad = model.Soyad.Replace('ö', 'o');
            model.Soyad = model.Soyad.Replace('ü', 'u');
            model.Soyad = model.Soyad.Replace('ğ', 'g');
            model.Soyad = model.Soyad.Replace('ş', 's');
            model.Soyad = model.Soyad.Replace('ı', 'i');
            model.Soyad = model.Soyad.Replace('ç', 'c');
            model.Soyad = model.Soyad.Replace(' ', '_');

            var student = _context.Kullanicilars.Where(x => x.Uid == model.studentUID).First();
            student.Ad = model.Ad;
            student.Soyad = model.Soyad;
            student.Tel = model.Telefon;
            student.Eposta = model.Eposta;
            student.IsActive = true;
            student.IsCreatedDate = student.IsCreatedDate;
            student.IsModifiedDate = DateTime.Now;
            student.IsCreatedUserid = model.userID;
            student.IsModifiedUserid = model.userID;
            student.SubeId = student.SubeId;
            student.KullaniciAdi = student.KullaniciAdi;
            student.Role = student.Role;
            student.IsActive = student.IsActive;
            student.Role = student.Role;
            student.IsCreatedDate = student.IsCreatedDate;
            student.IsModifiedDate = DateTime.Now;
            student.IsCreatedUserid = student.IsCreatedUserid;
            student.IsModifiedUserid = model.userID;

            _context.Entry(student).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            var actionLog = new ActionLog
            {
                ActionUid = student.Uid,
                Baslik = "Öğrenci İşlemi",
                Aciklama = "Öğrenci Bilgileri Güncellendi",
                UserId = model.userID,
                SubeId = model.subeID
            };
            postActionLog(actionLog);
            StudentReponseModel response = new StudentReponseModel
            {
                title = "İşlem Başarılı",
                message = "Öğrenci kaydı Eklendi",
                status = true,
                student = student
            };
            return StatusCode(200, response);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<userRecordModel>>> StudentDetail(string UID, int subeid)
        {
            var student = await _context.Kullanicilars.Where(x => x.Uid == UID && x.SubeId == subeid).Select(s => new userRecordModel
            {
                userUID = s.Uid,
                Ad = s.Ad,
                Soyad = s.Soyad,
                Telefon = s.Tel,
                Eposta = s.Eposta,
                subeNo = s.Sube.SubeNumber,
                toplamSinavKayit = s.UserOturumSets.Where(x => x.IsActive == true).Count(),
                toplamBekleyen = s.UserOturumSets.Where(x => x.IsActive == true && x.Oturum.Tarih.Date >= DateTime.Now.Date).Count(),
                toplamKatilim = s.UserOturumSets.Where(x => x.IsActive == true && x.Status != 1 && x.Status != 0).Count(),
                IsCreatedDate = s.IsCreatedDate,
                IsModifiedDate = s.IsModifiedDate,
                isCreatedUser = _context.Kullanicilars.Where(x => x.Id == s.IsCreatedUserid).FirstOrDefault(),
                IsModifiedUser = _context.Kullanicilars.Where(x => x.Id == s.IsModifiedUserid).FirstOrDefault()
            }).FirstOrDefaultAsync();
            return StatusCode(200, student);
        }

        void postActionLog(ActionLog model)
        {
            var action_log = new ActionLog
            {
                Udi = Guid.NewGuid().ToString(),
                Baslik = model.Baslik,
                Aciklama = model.Aciklama,
                IsCreatedDate = DateTime.Now,
                ActionUid = model.ActionUid,
                SubeId = model.SubeId,
                UserId = model.UserId
            };
            _context.ActionLogs.Add(action_log);
            _context.SaveChanges();
        }
        string createPassword()
        {
            Random rnd = new Random();
            int result = rnd.Next(100000, 999999);
            return result.ToString();
        }
    }

    public class Student_List_Model
    {
        public string userUID { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string phone { get; set; }
    }
    public class Student_Model
    {
        public string studentUID { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Telefon { get; set; }
        public string Eposta { get; set; }
        public int userID { get; set; }
        public int subeID { get; set; }
        public int role { get; set; }
    }
    public class StudentReponseModel
    {
        public bool status { get; set; }
        public string message { get; set; }
        public string title { get; set; }
        public Kullanicilar student { get; set; }
    }
    public class DashBoardStudentListOfExam
    {
        public string userUIID { get; set; }
        public string examSetUID { get; set; }
        public string studentInfo { get; set; }
        public string examInfo { get; set; }
        public int examSetStatus { get; set; }
        public DateTime examSetDate { get; set; }
    }
    public class userRecordModel
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Telefon { get; set; }
        public string Eposta { get; set; }
        public string userUID { get; set; }
        public string userID { get; set; }
        public int toplamKatilim { get; set; }
        public int toplamBekleyen { get; set; }
        public int toplamSinavKayit { get; set; }
        public int subeNo { get; set; }
        public DateTime IsCreatedDate { get; set; }
        public DateTime IsModifiedDate { get; set; }
        public Kullanicilar isCreatedUser { get; set; }
        public Kullanicilar IsModifiedUser { get; set; }
        public IEnumerable<UserOturumSet> sinavKayitlari { get; set; }
    }
}

