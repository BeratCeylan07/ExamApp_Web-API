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
    [Route("api/logs/[action]")]
    [ApiController]
    [Authorize]
    public class LogController : Controller
    {
        private readonly SnDbContext _context;
        private readonly IConfiguration _configuration;

        public LogController(SnDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Log_List_Model>>> getLogList(string actionuid, int subeid)
        {
            return await _context.ActionLogs.Where(x => x.SubeId == subeid && x.ActionUid == actionuid).Select(s => new Log_List_Model()
            {
                Id = s.Id,
                ActionUid = s.ActionUid,
                Udi = s.Udi,
                Baslik = s.Baslik,
                Aciklama = s.Aciklama,
                IsCreatedDate = s.IsCreatedDate,
                user = _context.Kullanicilars.Where(x => x.Id == s.UserId).First()
            }).ToListAsync();
        }
    }
}

