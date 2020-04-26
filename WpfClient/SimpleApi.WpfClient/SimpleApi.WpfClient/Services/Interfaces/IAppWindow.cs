using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace SimpleApi.WpfClient.Services.Interfaces
{
    public interface IAppWindow
    {
        RichTextBox LogBox { get; }
        Dispatcher Dispatcher { get; }
    }
}
