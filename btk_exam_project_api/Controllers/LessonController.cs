﻿using System;
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
            return await _context.Ders.Where(x => x.SubeId == subeID).Select(s => new Der()
            {
                Uid = s.Uid,
                DersAd = s.DersAd,
                Bilgi = s.Bilgi,
                DersOturumSets = s.DersOturumSets,
                UserDersSets = s.UserDersSets.Select(j => new UserDersSet()
                {
                    Uid = j.Uid,
                    User = j.User
                }).ToList(),
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
                oturum = s.Oturum,
                student = s.Student,
                kayitDate = s.IsCreatedDate
            }).ToListAsync();
        }
    }
}
