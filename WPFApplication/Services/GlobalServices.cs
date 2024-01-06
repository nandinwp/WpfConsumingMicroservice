using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Windows.Controls;
using System.Windows.Threading;
using WPFApplication.View;

namespace WPFApplication.Services
{
    public static class GlobalServices
    {
        public static int faseRequest = 0;
        public static Wait waitWpp = new Wait();

        public static FactoryAcc Acc = new FactoryAcc();

        public static void AppendTextWaitWindow(string text, Label lb)
        {
            if (lb.Dispatcher.CheckAccess())
            {
                lb.Content = text;
            }
            else
            {
                lb.Dispatcher.Invoke(new Action(() => lb.Content = text));
            }
        }
    }
}
