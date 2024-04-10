using RosinkaApp.Classes;
using RosinkaApp.Pages;
using RosinkaApp.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RosinkaApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AppData.MainFrame = MainFrame;
            MainFrame.Navigate(new ChildPage());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void Animation(Border border)
        {
            await Task.Delay(50);
            while (border.Width < 212)
            {
                if (border.IsMouseOver == false)
                {
                    BackAnimation(border);
                    return;
                }
                border.Width = border.Width + 4;
                await Task.Delay(1);
            }
        }

        private async void BackAnimation(Border border)
        {
            await Task.Delay(100);
            while (border.Width > 52)
            {
                if (border.IsMouseOver == true)
                {
                    Animation(border);
                    return;
                }
                border.Width = border.Width - 4;
                await Task.Delay(1);
            }
        }

        private void groupBtn_MouseEnter(object sender, MouseEventArgs e)
        {
           // AnimationCalc = 1;
            Animation(sender as Border);
        }

        private void groupBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            //AnimationCalc = 0;
            BackAnimation(sender as Border);
        }

        private void parentBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            //AnimationCalc = 1;
            Animation(sender as Border);
        }

        private void parentBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            //AnimationCalc = 0;
            BackAnimation(sender as Border);
        }

        private void childBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            //AnimationCalc = 1;
            Animation(sender as Border);
        }

        private void childBtn_MouseLeave(object sender, MouseEventArgs e)
        {
           // AnimationCalc = 0;
            BackAnimation(sender as Border);
        }

        private  void mainBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            //AnimationCalc = 1;
            Animation(sender as Border);
        }

        private  void mainBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            //AnimationCalc = 0;
            BackAnimation(sender as Border);
        }

        private void groupBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(new GroupPage());
        }

        private void parentBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(new ParentPage());
        }

        private void childBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(new ChildPage());
        }

        private void mainBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(new MentorPage());
        }

        private void menuBtn_Click(object sender, RoutedEventArgs e)
        {
            AuthorizationWindow authorizationWindow = new AuthorizationWindow();
            authorizationWindow.Show();
            this.Close();
        }
    }
}
