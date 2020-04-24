using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleApiServer.Models;
using SimpleApiServer.ResponseModels;
using Microsoft.EntityFrameworkCore;

namespace SimpleApiServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : ControllerBase
    {
        AppDbContext _dbContext;

        public NotesController(AppDbContext dbContext)
        {
            _dbContext = dbContext;

            if (!_dbContext.Notes.Any())
            {
                var note = new Note
                {
                    Date = DateTime.Now,
                    IpAdress = "Server",
                    Message = "First message"
                };
                _dbContext.Notes.Add(note);
                _dbContext.SaveChanges();
            }
        }

        // GET api/notes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Note>>> Get()
        {
            //доступ к списку имеется только с IP сервера
            return HttpContext.Connection.RemoteIpAddress.Equals(HttpContext.Connection.LocalIpAddress)
                ? await _dbContext.Notes.ToListAsync()
                : null;
        }

        //POST api/notes
        [HttpPost]
        public async Task<ActionResult<ResponseNotePost>> Post(Note note)
        {
            var ip = HttpContext.Connection.RemoteIpAddress.ToString();

            if (note.Message == null)
            {
                var result = new ResponseNotePost(note.Message, ip, DateTime.Now, "Message is null"); ;
                return new ActionResult<ResponseNotePost>(result);
            }

            note.IpAdress = ip;
            note.Date = DateTime.Now;

            try
            {
                _dbContext.Notes.Add(note);
                await _dbContext.SaveChangesAsync();
                var result = new ResponseNotePost(note);
                return new ActionResult<ResponseNotePost>(result);
            }
            catch (Exception ex)
            {
                var result = new ResponseNotePost(note.Message, ip, DateTime.Now, $"Datebase error: {ex.Message}");
                return new ActionResult<ResponseNotePost>(result);
            }
        }
    }
}