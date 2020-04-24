using Microsoft.EntityFrameworkCore;
using SimpleApi.WpfClient.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SimpleApi.WpfClient.DAL
{
    public static class DbHelper
    {     
        private static AppDbContext _dbContext;
        private static AppDbContext DbContext
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

        public static async Task<long?> AddNote(string message)
        {
            var note = new Note 
            { 
                Message = message,
                CreateDate = DateTime.Now
            };

            try
            {
                DbContext.Notes.Add(note);
                await DbContext.SaveChangesAsync();
                return note.Id;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка базы данных! Сообщение не сохранено и не отправлено!\r\n{ex.Message}", 
                    "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                return null;
            }
        }

        internal static async void AddSending(long noteId, bool success, string response)
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
    }
}
