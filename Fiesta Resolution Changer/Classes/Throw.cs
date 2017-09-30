using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Fiesta_Resolution_Changer.Classes
{
    class Throw
    {
        public static void Message(string Message)
        {
            MessageBox.Show(Message.ToString(), "Message", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
