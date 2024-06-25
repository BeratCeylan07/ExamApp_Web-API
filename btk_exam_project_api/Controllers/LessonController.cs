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
    [Route("api/lesson/[action]")]
    [ApiController]
    [Authorize]

    public class LessonController : Controller
    {
        private readonly SnDbContext _context;
        private readonly IConfiguration _configuration;
        public LessonController(SnDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lesson_List_Model>>> lesson_list(int subeID)
        {
            return await _context.Ders.Where(x => x.SubeId == subeID).Select(s => new Lesson_List_Model()
            {
                Uid = s.Uid,
                DersAd = s.DersAd,
                Bilgi = s.Bilgi,
                DersOturumSets = s.DersOturumSets,
                toplamKayitliOgrenci = _context.DersOturumUserSets.Where(h => h.Oturum.Ders.Uid == s.Uid).Count(),
                IsActive = s.IsActive
            }).ToListAsync();
        }
        [HttpPost]
        public async Task<ActionResult<Der>> new_lesson([FromBody] new_lesson_model model)
        {
            var lesson = new Der
            {
                Uid = Guid.NewGuid().ToString(),
                DersAd = model.dersAd,
                Bilgi = model.bilgi,
                SubeId = model.subeID,
                IsCreatedDate = DateTime.Now,
                IsCreatedUserId = model.userID,
                IsModifiedDate = DateTime.Now,
                IsModifiedUserId = model.userID,
                IsActive = true
            };
            await _context.Ders.AddAsync(lesson);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Teacher_Ders_Set_List_Model>>> Teacher_Lesson_Set_List(string teacherUID)
        {
            return await _context.DersOturumSets.Where(x => x.Teacher.Uid == teacherUID).Select(s => new Teacher_Ders_Set_List_Model()
            {
                dersOturumUserSetUID = s.Uid,
                dersAd = s.Ders.DersAd,
                dersTarih = s.Tarih,
                baslangic = s.Baslangic,
                bitis = s.Bitis
            }).ToListAsync();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student_Ders_Set_List_Model>>> StudentList_Of_Teacher_List(string teacherUID)
        {

            return await _context.DersOturumUserSets.Where(x => x.Oturum.Teacher.Uid == teacherUID).Select(s => new Student_Ders_Set_List_Model()
            {
                studentUID = s.Student.Uid,
                dersAd = s.Oturum.Ders.DersAd,
                ad = s.Student.Ad,
                soyad = s.Student.Soyad
            }).ToListAsync();
        }
        [HttpGet]
        public async Task<ActionResult<Lesson_Detail_Model>> lesson_Detail(string lessonUID)
        {
            return await _context.Ders.Where(x => x.Uid == lessonUID).Select(s => new Lesson_Detail_Model()
            {
                Uid = s.Uid,
                DersAd = s.DersAd,
                Bilgi = s.Bilgi,
                IsActive = s.IsActive,
                IsCreatedDate = s.IsCreatedDate,
                IsModifiedDate = s.IsModifiedDate,
                IsCreatedUserId = s.IsCreatedUserId,
                IsModifiedUserId = s.IsModifiedUserId,
                isCreatedUser = _context.Kullanicilars.Where(k => k.Id == s.IsCreatedUserId).First(),
                isModifiedUser = _context.Kullanicilars.Where(k => k.Id == s.IsModifiedUserId).First()
            }).FirstAsync();
        }
        [HttpGet]
        public async Task<ActionResult<Lesson_Record_Sum_Model>> lesson_record_sums(string lessonUID)
        {
            var result = new Lesson_Record_Sum_Model();
            result.toplamOturum = await _context.DersOturumSets.Where(x => x.Ders.Uid == lessonUID && x.IsActive == true).CountAsync();
            result.toplamOgretmen = await _context.UserDersSets.Where(x => x.Ders.Uid == lessonUID && x.IsActive).Select(s => s.Userid).Distinct().CountAsync();
            result.toplamOgrenci = await _context.DersOturumUserSets.Where(x => x.Oturum.Ders.Uid == lessonUID && x.Oturum.IsActive == true).CountAsync();
            return result;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LessonDashboardChartsModel>>> lessonCharts(int subeID, bool daily)
        {
            if (daily)
            {
                return await _context.DersOturumSets.Where(x => x.Ders.SubeId == subeID && x.Tarih.Date == DateTime.Now.Date).Select(s => new LessonDashboardChartsModel()
                {
                    label = s.Ders.DersAd,
                    y = s.DersOturumUserSets.Count()
                }).ToListAsync();
            }
            else
            {
                return await _context.DersOturumSets.Where(x => x.Ders.SubeId == subeID).Select(s => new LessonDashboardChartsModel()
                {
                    label = s.Ders.DersAd,
                    y = s.DersOturumUserSets.Count()
                }).ToListAsync();
            }
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DersOturumSet>>> lesson_session_list(string lessonUID)
        {
            return await _context.DersOturumSets.Where(x => x.Ders.Uid == lessonUID).Select(s => new DersOturumSet()
            {
                Uid = s.Uid,
                Baslangic = s.Baslangic,
                Bitis = s.Bitis,
                IsActive = s.IsActive,
                DersOturumUserSets = s.DersOturumUserSets,
                Tarih = s.Tarih,
                Teacher = s.Teacher
            }).ToListAsync();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<lesson_user_set_list_model>>> lesson_user_set_list(string lessonUID)
        {
            return await _context.DersOturumUserSets.Where(x => x.Oturum.Ders.Uid == lessonUID).Select(s => new lesson_user_set_list_model()
            {
                oturum = s.Oturum.Tarih.ToString("dd.MM.yyyy") + " / " + s.Oturum.Baslangic.ToString("HH:mm") + " - " + s.Oturum.Bitis.ToString("HH:mm"),
                student = s.Student,
                kayitDate = s.IsCreatedDate
            }).ToListAsync();
        }
        [HttpGet]
        public async Task<ActionResult<Ders_Oturum_Record_Model>> session_of_Lesson_Info(string lessonSessionUID)
        {
            return await _context.DersOturumSets.Where(x => x.Uid == lessonSessionUID).Select(s => new Ders_Oturum_Record_Model()
            {
                Uid = s.Uid,
                Ders = s.Ders,
                Baslangic = s.Baslangic,
                Bitis = s.Bitis,
                IsActive = s.IsActive,
                IsCreatedDate = s.IsCreatedDate,
                IsModifiedDate = s.IsModifiedDate,
                Tarih = s.Tarih,
                Teacher = s.Teacher,
                IsCreatedUser = _context.Kullanicilars.Where(x => x.Id == s.IsCreatedUserId).First(),
                IsModifiedUser = _context.Kullanicilars.Where(x => x.Id == s.IsModifiedUserId).First(),
                DersOturumUserSets = s.DersOturumUserSets.Select(s => new DersOturumUserSet()
                {
                    Uid = s.Uid,
                    IsActive = s.IsActive,
                    Bilgi = s.Bilgi,
                    IsCreatedDate = s.IsCreatedDate,
                    IsModifiedDate = s.IsModifiedDate,
                    Status = s.Status,
                    Student = s.Student
                }).ToList()
            }).FirstAsync();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DersOturumUserSet>>> student_list_of_lessonSession(string lessonSessionUID)
        {
            return await _context.DersOturumUserSets.Where(x => x.Oturum.Uid == lessonSessionUID).Select(s => new DersOturumUserSet()
            {
                Uid = s.Uid,
                Student = s.Student,
                Bilgi = s.Bilgi,
                IsActive = s.IsActive,
                Status = s.Status,
                IsCreatedDate = s.IsCreatedDate,
            }).ToListAsync();
        }
        [HttpPost]
        public async Task<ActionResult<Lesson_New_Session_Model>> new_lesson_session([FromBody] Lesson_New_Session_Model model)
        {
            int dersID = await _context.Ders.Where(x => x.Uid == model.lessonUID).Select(s => s.Id).FirstAsync();
            int teacherID = await _context.Kullanicilars.Where(x => x.Uid == model.teacherUID).Select(s => s.Id).FirstAsync();
            var new_session = new DersOturumSet
            {
                Uid = Guid.NewGuid().ToString(),
                Baslangic = Convert.ToDateTime(model.baslangic),
                Bitis = Convert.ToDateTime(model.bitis),
                Tarih = model.tarih,
                IsCreatedDate = DateTime.Now,
                IsCreatedUserId = model.userID,
                IsModifiedDate = DateTime.Now,
                IsModifiedUserId = model.userID,
                IsActive = true,
                Teacherid = teacherID,
                Dersid = dersID
            };
            await _context.DersOturumSets.AddAsync(new_session);
            await _context.SaveChangesAsync();

            return Ok();
        }
        [HttpPost]
        public async Task<ActionResult<Lesson_Student_Set>> student_lesson_session_set([FromBody] Lesson_Student_Set model)
        {
            int lessonID = await _context.DersOturumSets.Where(x => x.Uid == model.lessonSessionUID).Select(s => s.Id).FirstAsync();
            int studentID = await _context.Kullanicilars.Where(x => x.Uid == model.studentUID).Select(s => s.Id).FirstAsync();
            var lesso_student_set = new DersOturumUserSet
            {
                Uid = Guid.NewGuid().ToString(),
                IsCreatedDate = DateTime.Now,
                IsModifiedDate = DateTime.Now,
                IsCreatedUserId = model.userID,
                IsModifiedUserId = model.userID,
                Bilgi = model.bilgi,
                OturumId = lessonID,
                StudentId = studentID,
                IsActive = true,
                Status = 1
            };
            await _context.DersOturumUserSets.AddAsync(lesso_student_set);
            await _context.SaveChangesAsync();
            return Ok();

        }



        [HttpGet]
        public async Task<ActionResult<IEnumerable<Kullanicilar>>> teacher_list(int subeID)
        {
            return await _context.Kullanicilars.Where(x => x.SubeId == subeID).ToListAsync();
        }
    }
}

