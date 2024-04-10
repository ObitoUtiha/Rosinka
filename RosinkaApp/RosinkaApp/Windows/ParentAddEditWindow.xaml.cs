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
    /// Interaction logic for ParentAddEditWindow.xaml
    /// </summary>
    public partial class ParentAddEditWindow : Window
    {
        private Parent _parent = new Parent();
        public ParentAddEditWindow(Parent currentChildParent)
        {
            InitializeComponent();
            _parent = currentChildParent;
            if(currentChildParent != null )
            {
                FullNameTb.Text = _parent.ParentFullName;
                PhoneNumberTb.Text = _parent.PhoneNumber;
            }
        }

        private void BtnPass_Click(object sender, RoutedEventArgs e)
        {
            PassportWindow passportWindow = new PassportWindow(_parent);
            passportWindow.Show();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] fullName = FullNameTb.Text.Split(' ');
                if (fullName.Length < 3)
                {
                    MessageBox.Show("Ошибка!\nВозможно вы не ввели все данные в текстовые поля");
                    return;
                }
                if (_parent != null)
                {
                    _parent.FirstName = fullName[1];
                    _parent.LastName = fullName[0];
                    _parent.Patronymic = fullName[2];
                    _parent.PhoneNumber = PhoneNumberTb.Text;
                }
                else if (_parent == null)
                {
                    AppData.Context.Parent.Add(new Parent()
                    {
                        FirstName = fullName[1],
                        LastName = fullName[0],
                        Patronymic = fullName[2],
                        PhoneNumber = PhoneNumberTb.Text
                    });
                }
                AppData.Context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Возникла ошибка!\nПроверьте подключение к базе данных\nИли проверьте правильность заполненных полей");
            }
        }
    }
}
