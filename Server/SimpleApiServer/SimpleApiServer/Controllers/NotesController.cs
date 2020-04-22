using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleApiServer.Models;
using SimpleApiServer.ViewModels;
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
            return await _dbContext.Notes.ToListAsync();
        }

        // POST api/notes
        [HttpPost]
        public async Task<ActionResult<ResponseToAddMessage>> Post(Note note)
        {
            var ip = HttpContext.Connection.RemoteIpAddress.ToString();

            if (note.Message == null)
            {
                var result = new ResponseToAddMessage(note.Message, ip, DateTime.Now, "Message is null"); ;
                return new ActionResult<ResponseToAddMessage>(result);
            }

            note.IpAdress = ip;
            note.Date = DateTime.Now;

            try
            {
                _dbContext.Notes.Add(note);
                await _dbContext.SaveChangesAsync();
                var result = new ResponseToAddMessage(note);                
                return new ActionResult<ResponseToAddMessage>(result);
            }
            catch (Exception ex)
            {
                var result = new ResponseToAddMessage(note.Message, ip, DateTime.Now, $"Datebase error: {ex.Message}");
                return new ActionResult<ResponseToAddMessage>(result);
            }
        }
    }
}