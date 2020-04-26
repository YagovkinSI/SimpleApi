using SimpleApi.WpfClient.Enums;
using SimpleApi.WpfClient.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace SimpleApi.WpfClient.Logger
{
    public class LogService : ILogService
    {
        private readonly RichTextBox textBox;

        public LogService(RichTextBox textBox)
        {
            this.textBox = textBox;
        }

        public void AddLog(string message, enLogType type)
        {
            var logElement = new LogElement(message, type);
            AddLog(logElement);
        }

        public void AddLog(LogElement logElement)
        {
            var textRange = new TextRange(textBox.Document.ContentEnd, textBox.Document.ContentEnd)
            {
                Text = $"{logElement.Date.ToShortTimeString()}: \t{logElement.Message}\r"
            };
            switch (logElement.LogType)
            {
                case enLogType.Message:
                case enLogType.Attantion:
                    textRange.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Black);
                    break;
                case enLogType.Error:
                    textRange.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Red);
                    break;
            }
        }
    }
}
