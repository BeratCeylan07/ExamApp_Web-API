using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using btk_exam_project_api.CustomModels;
using btk_exam_project_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace btk_exam_project_api.Controllers
{
    [Route("api/exam/[action]")]
    [ApiController]
    [Authorize]
    public class ExamAPIController : Controller
    {
        private readonly SnDbContext _context;
        private readonly IConfiguration _configuration;
        public ExamAPIController(SnDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExamListModel>>> List(int subeID)
        {
            return await _context.DenemeSinavs.Where(x => x.Subeid == subeID).Select(s => new ExamListModel()
            {
                examUID = s.Uid,
                examInfo = s.DenemeAdi,
                examLocation = s.SinavYeri,
                catAndPub = s.SinavKategori + " / " + s.YayinAdi
            }).ToListAsync();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExamListForDashboardModel>>> ExamListForDashBoard(int subeID)
        {
            return await _context.DenemeSinavs.Where(x => x.Subeid == subeID).Select(s => new ExamListForDashboardModel()
            {
                id = s.Id,
                uid = s.Uid,
                exam = s.DenemeAdi,
                catandpub = s.SinavKategori + " / " + s.YayinAdi,
                
            }).ToListAsync();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExamDashboardChartModel>>> examCharts(int subeID, bool daily)
        {
            if (daily)
            {
                return await _context.DenemeSinavs.Where(x => x.Subeid == subeID && x.DenemeSinaviOturums.Where(a => a.Tarih.Date == DateTime.Now.Date).Count() > 0).Select(s => new ExamDashboardChartModel()
                {
                    label = s.DenemeAdi + " " + s.YayinAdi + " / " + s.SinavKategori,
                    y = _context.UserOturumSets.Where(x => x.Oturum.DenemeSinavId == s.Id).Count()
                }).ToListAsync();
            }
            else
            {
                return await _context.DenemeSinavs.Where(x => x.Subeid == subeID).Select(s => new ExamDashboardChartModel()
                {
                    label = s.DenemeAdi + " " + s.YayinAdi + " / " + s.SinavKategori,
                    y = _context.UserOturumSets.Where(x => x.Oturum.DenemeSinavId == s.Id).Count()
                }).ToListAsync();
            }
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExamDashboardChartModel>>> studentExamScoreChart(string studentUID)
        {
            return await _context.UserOturumSets.Where(x => x.User.Uid == studentUID).Select(s => new ExamDashboardChartModel()
            {
                label = s.Oturum.DenemeSinav.DenemeAdi + " " + s.Oturum.DenemeSinav.SinavKategori + " " + s.Oturum.DenemeSinav.YayinAdi,
                yNet = s.Net,
                examDate = s.Oturum.Tarih
            }).OrderBy(o => o.examDate).ToListAsync();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExamInListModel>>> ExamListWithNotUserSet(string userUID)
        {
            return await _context.DenemeSinavs.Where(x => x.DenemeSinaviOturums.Where(y => y.UserOturumSets.Where(z => z.User.Uid == userUID).Count() == 0).Count() == 0).Select(s => new ExamInListModel()
            {
                examUID = s.Uid,
                examInfo = s.DenemeAdi,
                catAndPub = s.SinavKategori + " / " + s.YayinAdi,
                examLocation = s.SinavYeri
            }).ToListAsync();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentExamListModel>>> SessionListOfExamForStudent(string userUID)
        {
            return await _context.UserOturumSets.Where(x => x.User.Uid == userUID).Select(s => new StudentExamListModel()
            {
                id = s.Id,
                uid = s.Uid,
                session = s.Oturum,
                examName = s.Oturum.DenemeSinav.DenemeAdi,
                examCat = s.Oturum.DenemeSinav.SinavKategori,
                examPub = s.Oturum.DenemeSinav.YayinAdi,
                examAmount = s.Oturum.DenemeSinav.Ucret,
                status = s.Status,
                isCreatedDate = s.IsCreatedDate,
                isModifiedDate = s.IsModifiedDate,
                isCreatedUserid = s.IsCreatedUserid,
                isModifiedUserid = s.IsModifiedUserid,
                isActive = s.IsActive,
                dogru = s.Dogru,
                yanlis = s.Yanlis,
                net = s.Net
            }).ToListAsync();
        }
        [HttpPost]
        public async Task<ActionResult<Exam_Post_Model>> NewExam([FromBody] Exam_Post_Model model)
        {
            var exam = new DenemeSinav
            {
                Uid = Guid.NewGuid().ToString(),
                Subeid = model.Subeid,
                IsCreatedDate = DateTime.Now,
                IsModifiedDate = DateTime.Now,
                IsCreatedUserid = model.userID,
                IsModifiedUserid = model.userID,
                IsActive = model.IsActive,
                DenemeAdi = model.DenemeAdi,
                YayinAdi = model.YayinAdi,
                YayinLogo = model.YayinLogo,
                SinavKategori = model.SinavKategori,
                KitapcikAdetMaliyet = model.KitapcikAdetMaliyet,
                KitapcikToplam = model.KitapcikToplam,
                SinavYeri = model.sinavYeri,
                Ucret = model.Ucret
            };
            await _context.DenemeSinavs.AddAsync(exam);
            await _context.SaveChangesAsync();
            var actionLog = new ActionLog
            {
                ActionUid = exam.Uid,
                Baslik = "Yeni Deneme Sınavı",
                Aciklama = "Yeni Deneme Sınavı Açıldı",
                UserId = model.userID,
                SubeId = model.Subeid
            };
            postActionLog(actionLog);
            return Ok();
        }
        [HttpPost]
        public async Task<ActionResult<Exam_Session_Post_Model>> NewSessionOfExam([FromBody] Exam_Session_Post_Model model)
        {
            int examID = await _context.DenemeSinavs.Where(x => x.Uid == model.examuid).Select(s => s.Id).FirstAsync();
            int subeID = await _context.DenemeSinavs.Where(x => x.Uid == model.examuid).Select(s => s.Subeid).FirstAsync();

            var exam_session = new DenemeSinaviOturum
            {
                Uid = Guid.NewGuid().ToString(),
                DenemeSinavId = examID,
                IsActive = true,
                Bilgi = model.sessioninfo,
                IsCreatedDate = DateTime.Now,
                IsCreatedUserid = model.userid,
                IsModifiedDate = DateTime.Now,
                IsModifiedUserid = model.userid,
                Kontenjan = model.kontenjan,
                OturumNo = createPassword(),
                Tarih = model.examdate
            };
            await _context.DenemeSinaviOturums.AddAsync(exam_session);
            await _context.SaveChangesAsync();
            var actionLog = new ActionLog
            {
                ActionUid = exam_session.Uid,
                Baslik = "Yeni Oturum",
                Aciklama = "Deneme Sınavı İçin Yeni Oturum Açıldı",
                UserId = model.userid,
                SubeId = subeID
            };
            postActionLog(actionLog);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<Exam_Record_Data_Model>> DetailExam(string examUID)
        {
            return await _context.DenemeSinavs.Where(x => x.Uid == examUID).Select(s => new Exam_Record_Data_Model()
            {
                UID = s.Uid,
                DenemeAdi = s.DenemeAdi,
                IsActive = s.IsActive,
                YayinAdi = s.YayinAdi,
                IsCreatedDate = s.IsCreatedDate,
                IsModifiedDate = s.IsModifiedDate,
                KitapcikAdetMaliyet = s.KitapcikAdetMaliyet,
                IsCreatedUser = _context.Kullanicilars.Where(a => a.Id == s.IsCreatedUserid).First(),
                IsModifiedUser = _context.Kullanicilars.Where(b => b.Id == s.IsModifiedUserid).First(),
                SinavYeri = s.SinavYeri,
                Ucret = s.Ucret,
                YayinLogo = s.YayinLogo,
                SinavKategori = s.SinavKategori,
                KitapcikToplam = s.KitapcikToplam
            }).FirstOrDefaultAsync();
        }
        [HttpGet]
        public async Task<ActionResult<Exam_Session_Record_Model>> ExamSessionDetail(string examSessionUID)
        {
            return await _context.DenemeSinaviOturums.Where(x => x.Uid == examSessionUID).Select(s => new Exam_Session_Record_Model()
            {
                ExamSessionUID = s.Uid,
                oturumNo = s.OturumNo,
                IsActive = s.IsActive,
                IsCreatedDate = s.IsCreatedDate,
                IsModifiedDate = s.IsModifiedDate,
                IsCreatedUser = _context.Kullanicilars.Where(x => x.Id == s.IsCreatedUserid).First(),
                IsModifiedUser = _context.Kullanicilars.Where(x => x.Id == s.IsModifiedUserid).First(),
                Kontenjan = s.Kontenjan,
                SessionBilgi = s.Bilgi,
                SessionDate = s.Tarih,
                ToplamOnKayit = s.UserOturumSets.Where(x => x.Status == 0).Count(),
                ToplamDevamsiz = s.UserOturumSets.Where(x => x.Status == 4).Count(),
                ToplamKatilimSaglayan = s.UserOturumSets.Where(x => x.Status == 2).Count(),
                ToplamKitapcikAlan = s.UserOturumSets.Where(x => x.Status == 3).Count(),
                ToplamKesinKayit = s.UserOturumSets.Where(x => x.Status != 0).Count()
            }).FirstOrDefaultAsync();
        }
  
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Exam_Session_Student_List_Model>>> examUserSetList(string examSessionUID)
        {
            return await _context.UserOturumSets.Where(x => x.Oturum.Uid == examSessionUID).Select(s => new Exam_Session_Student_List_Model()
            {
                IsActive = s.IsActive,
                IsCreatedDate = s.IsCreatedDate,
                SessionSetId = s.Id,
                SessionSetUID = s.Uid,
                Status = s.Status,
                Students = s.User,
                Sessions = s.Oturum,
                dogru = s.Dogru,
                yanlis = s.Yanlis,
                net = s.Net
            }).ToListAsync();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Exam_Session_Student_List_Model>>> ExamSessionStudentList(string examUID)
        {
            return await _context.UserOturumSets.Where(x => x.Oturum.DenemeSinav.Uid == examUID).Select(s => new Exam_Session_Student_List_Model()
            {
                IsActive = s.IsActive,
                IsCreatedDate = s.IsCreatedDate,
                SessionSetId = s.Id,
                SessionSetUID = s.Uid,
                Status = s.Status,
                Students = s.User,
                Sessions = s.Oturum,
                dogru = s.Dogru,
                yanlis = s.Yanlis,
                net = s.Net
            }).ToListAsync();
        }
        [HttpGet]
        public async Task<ActionResult<Exam_Record_Sums_Model>> ExamSums(string examUID)
        {
            return await _context.DenemeSinavs.Where(x => x.Uid == examUID).Select(s => new Exam_Record_Sums_Model()
            {
                ToplamKayitliOgrenci = s.DenemeSinaviOturums.Select(s => s.UserOturumSets.Where(a => a.Status != 0).Count()).First(),
                ToplamKitapcik = s.KitapcikToplam,
                ToplamMaliyet = s.KitapcikToplam * s.KitapcikAdetMaliyet,
                ToplamOturum = s.DenemeSinaviOturums.Count(),
                GuncelCiro = s.DenemeSinaviOturums.Select(s => s.UserOturumSets.Where(b => b.Status != 0).Count()).First() * s.Ucret,
                NetKar = s.DenemeSinaviOturums.Select(s => s.UserOturumSets.Where(b => b.Status != 0).Count()).First() * s.Ucret - s.KitapcikToplam * s.KitapcikAdetMaliyet
            }).FirstOrDefaultAsync();
        }
        [HttpPost]
        public async Task<ActionResult<Exam_Score_Post_Model>> ExamUserScoreSet([FromBody] Exam_Score_Post_Model model)
        {
            var sessionset = await _context.UserOturumSets.Where(x => x.Uid == model.sessionSetUID).FirstAsync();
            bool dortBirRule = await _context.UserOturumSets.Where(x => x.Uid == model.sessionSetUID).Select(s => s.Oturum.DenemeSinav.DortBirRule).FirstAsync();
            sessionset.Dogru = model.d;
            sessionset.Yanlis = model.y;
            sessionset.Status = 2;
            double net_Sonuc = model.d - model.y / 4;
            if (dortBirRule == true)
            {
                sessionset.Net = net_Sonuc;
            }   
            else
            {
                sessionset.Net = model.d;
            }
            _context.Entry(sessionset).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DenemeSinaviOturum>>> SessionListOfExam(string examUID)
        {
            return await _context.DenemeSinaviOturums.Where(x => x.DenemeSinav.Uid == examUID).Select(s => new DenemeSinaviOturum()
            {
                Uid = s.Uid,
                DenemeSinav = s.DenemeSinav,
                OturumNo = s.OturumNo,
                IsActive = s.IsActive,
                Bilgi = s.Bilgi,
                IsCreatedDate = s.IsCreatedDate,
                IsModifiedDate = s.IsModifiedDate,
                Kontenjan = s.Kontenjan,
                UserOturumSets = s.UserOturumSets,
                Tarih = s.Tarih
            }).ToListAsync();
        }
        [HttpGet]
        public async Task<DenemeSinav> GetExamUID(string sessionUID)
        {
            return await _context.DenemeSinaviOturums.Where(x => x.Uid == sessionUID).Select(s => s.DenemeSinav).FirstAsync();
           
        }
        [HttpPost]
        public async Task<ActionResult<Student_Exam_Set_Model>> SingleStudentExamSet([FromBody] Student_Exam_Set_Model model)
        {
            int studentID = await _context.Kullanicilars.Where(x => x.Uid == model.studentUID).Select(s => s.Id).FirstAsync();
            int subeID = await _context.Kullanicilars.Where(x => x.Uid == model.studentUID).Select(s => s.SubeId).FirstAsync();
            int examSessionID = await _context.DenemeSinaviOturums.Where(x => x.Uid == model.examSessionUID).Select(s => s.Id).FirstAsync();
            var exam_sesion_student_Set = new UserOturumSet
            {
                Uid = Guid.NewGuid().ToString(),
                IsActive = true,
                IsCreatedDate = DateTime.Now,
                IsModifiedDate = DateTime.Now,
                IsCreatedUserid = model.userID,
                IsModifiedUserid = model.userID,
                OturumId = examSessionID,
                UserId = studentID,
                Status = 1,
                Dogru = 0,
                Yanlis = 0,
            };
            await _context.UserOturumSets.AddAsync(exam_sesion_student_Set);
            await _context.SaveChangesAsync();
            var actionLog = new ActionLog
            {
                ActionUid = exam_sesion_student_Set.Uid,
                Baslik = "Deneme Sınavına Öğrenci Kaydı",
                Aciklama = "Öğrenci Deneme Sınavına Kaydedildi",
                UserId = model.userID,
                SubeId = subeID
            };
            postActionLog(actionLog);
            return Ok();
        }
        [HttpPost]
        public async Task<ActionResult<DenemeSinav>> EditExam([FromBody] Exam_Post_Model model)
        {
            var exam = await _context.DenemeSinavs.FirstOrDefaultAsync(x => x.Uid == model.examUID);
            exam.DenemeAdi = model.DenemeAdi;
            exam.YayinAdi = model.YayinAdi;
            exam.YayinLogo = model.YayinLogo;
            exam.SinavKategori = model.SinavKategori;
            exam.Ucret = model.Ucret;
            exam.IsActive = model.IsActive;
            exam.KitapcikToplam = model.KitapcikToplam;
            exam.KitapcikAdetMaliyet = model.KitapcikAdetMaliyet;
            exam.SinavYeri = model.sinavYeri;
            _context.Entry(exam).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            var actionLog = new ActionLog
            {
                ActionUid = exam.Uid,
                Baslik = "Deneme Sınavı",
                Aciklama = "Deneme Sınavı Bilgileri Güncellendi",
                UserId = model.userID,
                SubeId = exam.Subeid
            };
            postActionLog(actionLog);
            return Ok();
        }
        [HttpPost]
        public async Task<ActionResult<DenemeSinav>> EditSession([FromBody] Exam_Session_Post_Model model)
        {
            var session = await _context.DenemeSinaviOturums.FirstOrDefaultAsync(x => x.Uid == model.examSessionUID);
            session.Tarih = model.examdate;
            session.Kontenjan = model.kontenjan;
            session.Bilgi = model.sessioninfo;
            session.IsModifiedDate = DateTime.Now;
            session.IsModifiedUserid = model.userid;
            _context.Entry(session).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            var actionLog = new ActionLog
            {
                ActionUid = session.Uid,
                Baslik = "Deneme Sınavı Oturumu",
                Aciklama = "Oturum Bilgileri Güncellendi",
                UserId = model.userid,
                SubeId = model.subeID
            };
            postActionLog(actionLog);
            return Ok();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Kullanicilar>>> StudentListNotInExam(string examUID)
        {
            return await _context.Kullanicilars.Where(x => x.UserOturumSets.Where(y => y.Oturum.DenemeSinav.Uid == examUID).Count() <= 0).ToListAsync();
        }
        [HttpPost]
        public async Task<ActionResult<Student_Yoklama_Model>> sinavYoklama([FromBody] Student_Yoklama_Model model)
        {
            var student_set = await _context.UserOturumSets.FirstOrDefaultAsync(x => x.Uid == model.studentSessionSetUID);
            student_set.Status = model.status;

            _context.Entry(student_set).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
            
        }
        [HttpPost]
        public async Task<ActionResult<Wp_Log_Post_Model>> WP_LOG([FromBody] Wp_Log_Post_Model model)
        {
            int studentID = await _context.Kullanicilars.Where(x => x.Uid == model.studentUID).Select(s => s.Id).FirstAsync();
            var message_log = new KullaniciMesajLog
            {
                Uid = Guid.NewGuid().ToString(),
                IsActive = true,
                IsCreatedDate = DateTime.Now,
                IsModifiedDate = DateTime.Now,
                IsCreatedUserid = model.userID,
                IsModifiedUserid = model.userID,
                MesajTarih = DateTime.Now,
                Mesaj = model.mesaj,
                SubeId = model.subeID,
                UserId = studentID
            };
            await _context.KullaniciMesajLogs.AddAsync(message_log);
            await _context.SaveChangesAsync();
            var actionLog = new ActionLog
            {
                ActionUid = message_log.Uid,
                Baslik = "Whatsapp",
                Aciklama = "Whatsapp Mesajı Gönderildi",
                UserId = model.userID,
                SubeId = model.subeID
            };
            postActionLog(actionLog);
            return Ok();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Wp_Log_Get_Model>>> wp_logs(int type, string uid)
        {
            switch(type)
            {
                case 1:
                    return await _context.KullaniciMesajLogs.Where(x => x.User.Uid == uid).Select(s => new Wp_Log_Get_Model()
                    {
                        Uid = s.Uid,
                        Student = s.User,
                        IsActive = s.IsActive,
                        Mesaj = s.Mesaj.ToString().Replace("%0A","\n"),
                        MesajTarih = s.MesajTarih,
                        IsCreatedUserid = s.IsCreatedUserid,
                        IsCreatedDate = s.IsCreatedDate,
                        IsModifiedUserid = s.IsModifiedUserid,
                        IsModifiedDate = s.IsModifiedDate,
                        isCreatedUser = _context.Kullanicilars.Where(x => x.Id == s.IsCreatedUserid).First()
                    }).ToListAsync();
                    break;
                case 2:
                    return await _context.KullaniciMesajLogs.Where(x => x.User.UserOturumSets.Where(x => x.Oturum.DenemeSinav.Uid == uid).Count() > 0).Select(s => new Wp_Log_Get_Model()
                    {
                        Uid = s.Uid,
                        Student = s.User,
                        IsActive = s.IsActive,
                        Mesaj = s.Mesaj.ToString().Replace("%0A", "\n"),
                        MesajTarih = s.MesajTarih,
                        IsCreatedUserid = s.IsCreatedUserid,
                        IsCreatedDate = s.IsCreatedDate,
                        IsModifiedUserid = s.IsModifiedUserid,
                        IsModifiedDate = s.IsModifiedDate,
                        isCreatedUser = _context.Kullanicilars.Where(x => x.Id == s.IsCreatedUserid).First()
                    }).ToListAsync();
                    break;
                case 3:
                    return await _context.KullaniciMesajLogs.Where(x => x.User.UserOturumSets.Where(x => x.Oturum.Uid == uid).Count() > 0).Select(s => new Wp_Log_Get_Model()
                    {
                        Uid = s.Uid,
                        Student = s.User,
                        IsActive = s.IsActive,
                        Mesaj = s.Mesaj.ToString().Replace("%0A", "\n"),
                        MesajTarih = s.MesajTarih,
                        IsCreatedUserid = s.IsCreatedUserid,
                        IsCreatedDate = s.IsCreatedDate,
                        IsModifiedUserid = s.IsModifiedUserid,
                        IsModifiedDate = s.IsModifiedDate,
                        isCreatedUser = _context.Kullanicilars.Where(x => x.Id == s.IsCreatedUserid).First()
                    }).ToListAsync();
                default:
                    return StatusCode(400, "Eksik Parametre");

            }       
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
        int createPassword()
        {
            Random rnd = new Random();
            int result = rnd.Next(100000, 999999);
            return result;
        }
    }
}

