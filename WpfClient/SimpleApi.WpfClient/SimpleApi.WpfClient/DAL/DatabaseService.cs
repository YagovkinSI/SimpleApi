using Microsoft.EntityFrameworkCore;
using SimpleApi.WpfClient.DAL.Models;
using SimpleApi.WpfClient.Services;
using SimpleApi.WpfClient.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SimpleApi.WpfClient.DAL
{
    public class DatabaseService : IDatabaseService
    {     
        private AppDbContext _dbContext;
        private AppDbContext DbContext
        {
            get
            {
                if (_dbContext == null)
                {
                    _dbContext = new AppDbContext();
                }
                return _dbContext;
            }
        } 

        public async Task<AppActionResult> AddNote(Note note)
        {
            try
            {
                DbContext.Notes.Add(note);
                await DbContext.SaveChangesAsync();
                return new AppActionResult(true);
            }
            catch (Exception ex)
            {
                return new AppActionResult(false, 
                    $"Ошибка сохранения сообщения в локальную БД! Сообщение не отправлено!\r\t{ex.Message}");
            }
        }

        public async Task<AppActionResult> AddSending(long noteId, AppActionResult actionResult)
        {    
            try
            {
                var sending = new Sending
                {
                    NoteId = noteId,
                    SendDate = DateTime.Now,
                    Success = actionResult.Success,
                    Error = actionResult.Error
                };
                DbContext.Sendings.Add(sending);
                await DbContext.SaveChangesAsync();
                return new AppActionResult(true);
            }
            catch (Exception ex)
            {
                return new AppActionResult(false,
                       $"Ошибка сохранения результата отправки в локальную БД! \r\t{ex.Message}");
            }
        }

        public async Task<Note[]> GetNotSendedNotes()
        {
            try
            {
                return await DbContext.Notes
                    .Include(n => n.Sendings)
                    .Where(n => n.Sendings.All(s => !s.Success))
                    .ToArrayAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка базы данных! Не удалось считать неотрплавнные сообщения!\r\t{ex.Message}",
                    "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                return new Note[0];
            }
        }
    }
}
