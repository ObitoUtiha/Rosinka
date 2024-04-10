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
    /// Interaction logic for BirthCertificateWindow.xaml
    /// </summary>
    public partial class BirthCertificateWindow : Window
    {
        BirthCertificate _curentCert = new BirthCertificate();
        Child _currentChild = new Child();
        public BirthCertificateWindow(Child currentChild)
        {
            InitializeComponent();
            _curentCert = currentChild.BirthCertificate;
            _currentChild = currentChild;
            if (currentChild.BirthCertificate != null)
            {
                BirthCertificateIdTb.IsEnabled = false;
                BirthCertificateDateTb.SelectedDate = _curentCert.BirthCertificateDate;
                BirthCertificateIdTb.Text = _curentCert.BirthCertificateId;
                IssuedByTb.Text = _curentCert.IssuedBy;
                PlaceOfBirthTb.Text = _curentCert.PlaceOfBirth;
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if(BirthCertificateDateTb.SelectedDate == null || string.IsNullOrWhiteSpace(BirthCertificateDateTb.Text) || string.IsNullOrWhiteSpace(BirthCertificateIdTb.Text) ||
                string.IsNullOrWhiteSpace(IssuedByTb.Text) || string.IsNullOrWhiteSpace(PlaceOfBirthTb.Text))
            {
                MessageBox.Show("Проверьте корректность заполнения данных");
                return;
            }
            if (_curentCert == null)
            {
                if(AppData.Context.BirthCertificate.Where(p=>p.BirthCertificateId.ToString() == BirthCertificateIdTb.Text).Any())
                {
                    MessageBox.Show("Данный номер сертификата уже существует");
                    return;
                }
                BirthCertificate birthCertificate = new BirthCertificate
                {
                    BirthCertificateDate = BirthCertificateDateTb.SelectedDate,
                    IssuedBy = IssuedByTb.Text,
                    PlaceOfBirth = PlaceOfBirthTb.Text
                };
                AppData.Context.BirthCertificate.Add(birthCertificate);
                AppData.Context.SaveChanges();
                _currentChild.BirthCertificate = birthCertificate;
            }
            else
            {
                _curentCert.BirthCertificateDate = BirthCertificateDateTb.SelectedDate;
                _curentCert.IssuedBy = IssuedByTb.Text;
                _curentCert.PlaceOfBirth = PlaceOfBirthTb.Text;

            }
            AppData.Context.SaveChanges();
            this.Close();
        }
    }
}
