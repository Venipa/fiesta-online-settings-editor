using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Fiesta_Resolution_Changer
{
    /// <summary>
    /// Interaktionslogik für Info.xaml
    /// </summary>
    public partial class Info : Window
    {
        public Info()
        {
            InitializeComponent();
            loadContributors();
        }
        private void loadContributors()
        {
            List<string> l = Properties.Resources.contributors.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
            foreach(string cont in l)
            {
                string[] user = cont.Split('|');
                contributorlist.Items.Insert(0, new ListBoxItem()
                {
                    Content = $"{user.First()}, {user[1]}",
                    Tag = user[2],
                    Cursor = Cursors.Hand
                });
            }
        }
        private void contributors_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (contributorlist.SelectedItem != null)
            {
                ListBoxItem or = (ListBoxItem)contributorlist.SelectedItem;
                Process.Start(or.Tag.ToString());
            }
        }
    }
   }
