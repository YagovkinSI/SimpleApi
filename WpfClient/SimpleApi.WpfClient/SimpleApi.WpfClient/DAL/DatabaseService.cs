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

        public async Task<bool> AddNote(Note note)
        {
            try
            {
                DbContext.Notes.Add(note);
                await DbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка базы данных! Сообщение не сохранено и не отправлено!\r\n{ex.Message}", 
                    "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
        }
        public async void AddSending(long noteId, bool success, string response)
        {    
            try
            {
                var sending = new Sending
                {
                    NoteId = noteId,
                    SendDate = DateTime.Now,
                    Success = success,
                    Error = success ? "" : response
                };
                DbContext.Sendings.Add(sending);
                await DbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка базы данных! Не сохранен результат отвправи сообщения!\r\n{ex.Message}",
                    "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
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
                MessageBox.Show($"Ошибка базы данных! Не удалось считать неотрплавнные сообщения!\r\n{ex.Message}",
                    "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                return new Note[0];
            }
        }
    }
}
