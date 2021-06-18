using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
            
            //Checking which skin they selected and saving it
            switch (Selected[1])
            {
                case "1":
                    SkinSelected = 1;
                    //Writing the skin they selected to the Player Preferences file
                    File.WriteAllText(_path, "1");
                    break;
                case "2":
                    SkinSelected = 2;
                    File.WriteAllText(_path, "2");
                    break;
                case "3":
                    SkinSelected = 3;
                    File.WriteAllText(_path, "3");
                    break;
            }
        }

        private void BtnBack_OnClick(object sender, RoutedEventArgs e)
        {
            Home_Page hp = new Home_Page();
            hp.Show();
            Close();
        }

        private void BtnAction_OnClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Exit_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
