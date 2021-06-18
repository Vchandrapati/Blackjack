using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using static Blackjack.Classes.Variables;

namespace Blackjack
{
    public partial class Skins : Window
    {
        public Skins()
        {
            InitializeComponent();
        }

        private void Change_OnClick(object sender, RoutedEventArgs e)
        {
            Button skin = (Button) sender;
            var Selected = skin.Name.Split('_');
            
            if (Selected[1] == "1")
            {
                SkinSelected = 1;
                File.WriteAllText(_path, "1");
            }
            else if (Selected[1] == "2")
            {
                SkinSelected = 2;
                File.WriteAllText(_path, "2");
            }
            else if (Selected[1] == "3")
            {
                SkinSelected = 3;
                File.WriteAllText(_path, "3");
            }
        }

        private void BtnBack_OnClick(object sender, RoutedEventArgs e)
        {
            Home_Page hp = new Home_Page();
            hp.Show();
            Close();
        }
    }
}
