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
        public async Task<ActionResult<IEnumerable<Der>>> lesson_list(int subeID)
        {
            return await _context.Ders.Where(x => x.SubeId == subeID).ToListAsync();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Teacher_Ders_Set_List_Model>>> Teacher_Lesson_Set_List(string teacherUID)
        {
            return await _context.DersOturumUserSets.Where(x => x.UserDersSet.User.Uid == teacherUID).Select(s => new Teacher_Ders_Set_List_Model()
            {
                dersOturumUserSetUID = s.Uid,
                dersAd = s.Oturum.Ders.DersAd,
                bilgi = s.Bilgi,
                dersTarih = s.Oturum.Tarih,
                baslangic = s.Oturum.Baslangic,
                bitis = s.Oturum.Bitis
            }).ToListAsync();

        }
    }
}

