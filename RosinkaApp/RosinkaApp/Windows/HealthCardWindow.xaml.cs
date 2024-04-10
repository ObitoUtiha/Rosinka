using Microsoft.Win32;
using RosinkaApp.Classes;
using RosinkaApp.Entities;
using System;
using System.Collections.Generic;
using System.IO;
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
using static System.Net.Mime.MediaTypeNames;

namespace RosinkaApp.Windows
{
    /// <summary>
    /// Interaction logic for HealthCardWindow.xaml
    /// </summary>
    public partial class HealthCardWindow : Window
    {
        private HealthCard _healthCard;
        private Child _currentChild;
        public HealthCardWindow(Child currentChild)
        {
            InitializeComponent();
            _healthCard = currentChild.HealthCard;
            _currentChild = currentChild;
            if(currentChild.HealthCard != null)
            {
                HealthCardId.IsEnabled = false;
                HealthCardId.Text = _healthCard.HealthCardId.ToString();
                Disability.Text = _healthCard.Disability;
                HealthGroup.Text = _healthCard.HealthGroup;
                ExtensiveTreatment.Text = _healthCard.ExtensiveTreatment;
                Comment.Text = _healthCard.Comment;
                _img = _healthCard.VaccinationCertificateFirstPage;
            }
        }

        private void BtnHealthCard_Click(object sender, RoutedEventArgs e)
        {
            HealthCardPhotoWindow healthCardPhotoWindow = new HealthCardPhotoWindow(_img);
            healthCardPhotoWindow.Show();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
           if(string.IsNullOrWhiteSpace(HealthCardId.Text) || string.IsNullOrWhiteSpace(HealthGroup.Text) || string.IsNullOrWhiteSpace(Disability.Text) || string.IsNullOrWhiteSpace(ExtensiveTreatment.Text))
            {
                MessageBox.Show("Проверьте корректность заполненных полей");
                return;
            }
           if(_healthCard == null)
            {
                //if(AppData.Context.HealthCard.Where(p=>p.HealthCardId.ToString() == HealthCardId.Text).Any())
                //{
                //    MessageBox.Show("Данная мед. карта существует");
                //    return;
                //}
                HealthCard healthCard = new HealthCard
                {
                    Disability = Disability.Text,
                    HealthGroup = HealthGroup.Text,
                    ExtensiveTreatment = ExtensiveTreatment.Text,
                    Comment = Comment.Text,
                    VaccinationCertificateFirstPage = _img
                };
                AppData.Context.HealthCard.Add(healthCard);
                AppData.Context.SaveChanges();
                _currentChild.HealthCard = healthCard;  
            }
            else
            {
                _healthCard.Disability = Disability.Text;
                _healthCard.HealthGroup = HealthGroup.Text;
                _healthCard.ExtensiveTreatment = ExtensiveTreatment.Text;
                _healthCard.Comment = Comment.Text;
                _healthCard.VaccinationCertificateFirstPage = _img;
            }
            AppData.Context.SaveChanges();
            this.Close();
        }

        private byte[] _img;

        private void downloadPhotoBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Images |*.png;*.jpg;*.jpeg";
            if (ofd.ShowDialog() == true)
            {
                BitmapImage image = new BitmapImage(new Uri(ofd.FileName));

                image.DecodePixelHeight = 200;
                image.DecodePixelWidth = 300;
                _img = File.ReadAllBytes(ofd.FileName);
            }
        }

        private void delPhotoBtn_Click(object sender, RoutedEventArgs e)
        {
            _img = null;
        }
    }
}
