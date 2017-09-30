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
