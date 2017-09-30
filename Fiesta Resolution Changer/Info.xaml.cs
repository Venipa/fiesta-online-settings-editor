using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            LoadContributors();
        }
        private void LoadContributors()
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
        private void Contributors_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (contributorlist.SelectedItem != null)
            {
                ListBoxItem or = (ListBoxItem)contributorlist.SelectedItem;
                Process.Start(or.Tag.ToString());
            }
        }
    }
   }
