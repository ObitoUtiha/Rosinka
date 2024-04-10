using RosinkaApp.Classes;
using RosinkaApp.Entities;
using System;
using System.Collections.Generic;
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

namespace RosinkaApp.Windows
{
    /// <summary>
    /// Interaction logic for PassportWindow.xaml
    /// </summary>
    public partial class PassportWindow : Window
    {
        private Parent parent = new Parent();
        private Passport _passport = new Passport();
        public PassportWindow(Parent currentparent)
        {
            InitializeComponent();
            parent = currentparent;
            _passport = currentparent.Passport;
            if(_passport != null)
            {
                PassportSeries.Text = _passport.PassportSeries;
                PassportNumber.Text = _passport.PassportNumber;
                ActualAddressTb.Text = _passport.ActualAddress;
                RegistrationAddressTb.Text = _passport.RegistrationAddress;
                CitizenshipTb.Text = _passport.Citizenship;
                BirthdayTb.SelectedDate = _passport.Birthday;
            }
        }

        private void BtnPassport_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(PassportNumber.Text) || string.IsNullOrWhiteSpace(PassportSeries.Text) || string.IsNullOrWhiteSpace(RegistrationAddressTb.Text) || string.IsNullOrWhiteSpace(ActualAddressTb.Text) ||
                string.IsNullOrWhiteSpace(CitizenshipTb.Text) || BirthdayTb.SelectedDate == null || string.IsNullOrWhiteSpace(BirthdayTb.Text))
            {
                MessageBox.Show("Ошибка!\nВозможно вы не ввели все данные в текстовые поля");
                return;
            }
            if(_passport == null)
            {
                if (AppData.Context.Passport.Where(p => p.PassportSeries == PassportSeries.Text && p.PassportNumber ==  PassportNumber.Text).Any())
                {
                    MessageBox.Show("Данный паспорт уже существует");
                    return;
                }
                Passport newPass = new Passport()
                {
                PassportSeries = PassportSeries.Text,
                PassportNumber = PassportNumber.Text,
                ActualAddress = ActualAddressTb.Text,
                RegistrationAddress = RegistrationAddressTb.Text,
                Citizenship = CitizenshipTb.Text,
                Birthday = BirthdayTb.SelectedDate
            };
                AppData.Context.Passport.Add(newPass);
                AppData.Context.SaveChanges();
                parent.Passport = newPass;
            }
            else if(_passport.PassportSeries != null)
            {
                _passport.PassportSeries = PassportSeries.Text;
                _passport.PassportNumber = PassportNumber.Text;
                _passport.ActualAddress = ActualAddressTb.Text;
                _passport.RegistrationAddress = RegistrationAddressTb.Text;
                _passport.Citizenship = CitizenshipTb.Text;
                _passport.Birthday = BirthdayTb.SelectedDate;
            }
            AppData.Context.SaveChanges();
        }
    }
}
