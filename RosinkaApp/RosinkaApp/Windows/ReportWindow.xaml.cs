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
    /// Interaction logic for ReportWindow.xaml
    /// </summary>
    public partial class ReportWindow : Window
    {
        Child _child = new Child();
        List<Parent> _parents = new List<Parent>();
        public ReportWindow(Child child, List<ChildParent> parents)
        {
            InitializeComponent();
            _child = child;
            _parents = parents.Select(p => p.Parent).ToList();
        }

        public int Calk = 0;
        public ParentForReport FIO_Parent(int iteration, int change)
        {
            ParentForReport fio = new ParentForReport();
            if (iteration >= _parents.Count || iteration < 0)
            {
                fio.ParentFullName = " ";
                fio.PhoneNumber = " ";
                fio.ParentBirthday = " ";
                if (change == 1)
                    Calk += 1;
                return fio;
            }
            if (string.IsNullOrWhiteSpace(_parents[iteration].FirstName))
            {
                fio.ParentFullName = " ";
                fio.PhoneNumber = " ";
                fio.ParentBirthday = " ";
            }
            else
            {

                fio.ParentFullName = _parents[iteration].ParentFullName;
                fio.PhoneNumber = _parents[iteration].PhoneNumber;
                fio.ParentBirthday = _parents[iteration].ParentBirthday;
            }
            if (change == 1)
                Calk += 1;
            return fio;
        }

        public class ParameterData
        {
            public string Text { get; set; }
            public byte[] Data { get; set; }

            // Конструктор для установки данных параметра
            public ParameterData(string description, byte[] data)
            {
                Text = description;
                Data = data;
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if(ChildEndDateTb.SelectedDate == null || string.IsNullOrWhiteSpace(ChildEndDateTb.Text) || string.IsNullOrWhiteSpace(ChildMaleTb.Text))
            {
                MessageBox.Show("Проверьте корректность введённых полей");
                return;
            }
            var helper = new WordHelper("Templates/ChildTemplate.docx");
            var items = new Dictionary<string, ParameterData>
            {
                {"<Child_Id>", new ParameterData(_child.ChildId.ToString(), null)},
                {"<Child_Photo>", new ParameterData(" ", _child.ChildPhoto)},
                {"<Date_Now>", new ParameterData($"{DateTime.Now.ToString("dd.MM.yyyy")}", null)},
                {"<Start_Date>", new ParameterData($"{_child.DateOfIssue.Value.ToString("dd.MM.yyyy")}", null) },
                {"<End_Date>", new ParameterData($"{ChildEndDateTb.SelectedDate.Value.ToString("dd.MM.yyyy")}", null) },
                {"<Child_FIO>", new ParameterData(_child.ChildFullName, null)},
                {"<Child_Birthday>", new ParameterData(_child.Birthday.Value.ToString("dd.MM.yyyy"), null)},
                {"<Child_BirthPlace>", new ParameterData(_child.BirthCertificate.PlaceOfBirth, null)},
                {"<Child_Male>", new ParameterData(ChildMaleTb.Text, null)},
                {"<Child_ Nationality>", new ParameterData(_child.Nationality, null)},
                {"<Disability>", new ParameterData(_child.HealthCard.Disability, null)},
                {"<HealthGroup>", new ParameterData(_child.HealthCard.HealthGroup, null)},
                {"<ExtensiveTreatment>", new ParameterData(_child.HealthCard.ExtensiveTreatment, null)},
                {"<Comment>", new ParameterData(_child.HealthCard.Comment, null)},
                {"<User_Role>", new ParameterData(AppData.currentUser.Role.RoleName, null)},
                {"<User_Name>", new ParameterData(AppData.currentUser.MentorFullName, null)},
                {"<Parent>",  new ParameterData(FIO_Parent(Calk, 1).ParentFullName, null) },
                {"<Parent_Phone>", new ParameterData(FIO_Parent(Calk - 1, 0).PhoneNumber, null) },
                {"<Parent_Birthday>", new ParameterData(FIO_Parent(Calk - 1, 0).ParentBirthday, null) },
                {"<Parent2>",  new ParameterData(FIO_Parent(Calk, 1).ParentFullName, null) },
                {"<Parent_Phone2>", new ParameterData(FIO_Parent(Calk - 1, 0).PhoneNumber, null) },
                {"<Parent_Birthday2>", new ParameterData(FIO_Parent(Calk - 1, 0).ParentBirthday, null) },
                {"<Parent3>",  new ParameterData(FIO_Parent(Calk, 1).ParentFullName, null) },
                {"<Parent_Phone3>", new ParameterData(FIO_Parent(Calk - 1, 0).PhoneNumber, null) },
                {"<Parent_Birthday3>", new ParameterData(FIO_Parent(Calk - 1, 0).ParentBirthday, null) },
                {"<Parent4>",  new ParameterData(FIO_Parent(Calk, 1).ParentFullName, null) },
                {"<Parent_Phone4>", new ParameterData(FIO_Parent(Calk - 1, 0).PhoneNumber, null) },
                {"<Parent_Birthday4>", new ParameterData(FIO_Parent(Calk - 1, 0).ParentBirthday, null) },
                {"<Parent5>",  new ParameterData(FIO_Parent(Calk, 1).ParentFullName, null) },
                {"<Parent_Phone5>", new ParameterData(FIO_Parent(Calk - 1, 0).PhoneNumber, null) },
                {"<Parent_Birthday5>", new ParameterData(FIO_Parent(Calk - 1, 0).ParentBirthday, null) }
            };
            helper.Process(items);
            Calk = 0;
            MessageBox.Show($"Файл сохранен по следующему пути:\n{helper.NewPath}");
            this.Close();
        }
    }
}
