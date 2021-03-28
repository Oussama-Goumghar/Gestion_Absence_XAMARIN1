using proj.Models;
using proj.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLiteNetExtensionsAsync.Extensions;


namespace proj.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AbsencePage : ContentPage
    {
        FiliereRepository dataFilier=new FiliereRepository();
        LessonRepository dataLesson = new LessonRepository();
        StudentRepository StudentData = new StudentRepository();
        AbsenceRepository DataAbsence = new AbsenceRepository() ;
        Dictionary<String, int> DcFilier;
        Dictionary<String, int> DcLesson;
        ObservableCollection<Student> StudentList;
        ObservableCollection<Student> selectedList;

        int Idselected;

         public AbsencePage()
        {
            InitializeComponent();
           
        }

        async protected override void OnAppearing()
        {
            DcFilier = new Dictionary<string, int>();
            var Nameist = new List<string>();
            var flName = await dataFilier.GetFilierName();
            foreach (Filiere Data in flName)
            {
                DcFilier.Add(Data.FiliereName, Data.IdFiliere);
                Nameist.Add(Data.FiliereName);
            }
            Breed1.ItemsSource = Nameist;
            base.OnAppearing();
        }
        async public void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {


            int selectedIndex = Breed1.SelectedIndex;

            if (selectedIndex != -1)

            {

                var FilierName = Breed1.Items[selectedIndex];
                var id = DcFilier[FilierName];

  
                Filiere filiere =await dataFilier.connection.GetWithChildrenAsync<Filiere>(id);
                var LessonVar = filiere.lessons;
                StudentList = new ObservableCollection<Student>(filiere.students);


                listUser.ItemsSource = StudentList;

                DcLesson = new Dictionary<string, int>();
                var NameList = new List<string>();
              
                foreach (Lesson Data in LessonVar)
                {
                    DcLesson.Add(Data.LessonName, Data.IdLesson);
                    NameList.Add(Data.LessonName);

                }
                LessonPicker.ItemsSource = NameList;
            }
        }
        public void OnLessonPickerSelectedIndexChanged(object sender, EventArgs e)
        {


            int selectedIndex = LessonPicker.SelectedIndex;

            if (selectedIndex != -1)
            {
                var LessonName = LessonPicker.Items[selectedIndex];
                Idselected = DcLesson[LessonName];
                Console.WriteLine(LessonName);
                Console.WriteLine(Idselected);
            }
            
        }

                private void BtnSave(object sender, EventArgs e)
        {

            if (selectedList!=null)
            {

                foreach (var student in selectedList)
                {
                   
                    Absence absence = new Absence()
                    {
                        IdStudent = student.IdStudent,
                        IdLesson = Idselected,
                        IsPresent = false,
                        Date = DateTime.Now

                    };
                    DataAbsence.InsertAbsence(absence);

                }
            }
            
            IEnumerable<Student> presentstudents;
            if (selectedList != null && selectedList.Count > 0)
            {
                presentstudents = StudentList.Where(std => selectedList.All(absentStd => absentStd.IdStudent != std.IdStudent));
             
            }
            else
            {
                presentstudents = StudentList.AsEnumerable();
            }
            Console.WriteLine($"count f present Student ${presentstudents.Count()}");

            if (presentstudents.Count() > 0)
            {
                foreach (var student in presentstudents)
                {
                    Console.WriteLine(student.ToString() + $" this id is present  {Idselected}");
                    Absence absence = new Absence()
                    {
                        IdStudent = student.IdStudent,
                        IdLesson = Idselected,
                        IsPresent = true,
                        Date = DateTime.Now
                    };
                    DataAbsence.InsertAbsence(absence);

                }
            }

            if (selectedList != null)
            {
                DisplayAlert("Saved!", $"{presentstudents.Count()} étudiants Present, and {selectedList.Count} étudiants Absent.", "Cancel");
                 Navigation.PushAsync(new HomePage2());
            }
            else
            {
                DisplayAlert("Saved!", $"Tous les etudiant sont presents", "Cancel");
                 Navigation.PushAsync(new HomePage2());
            }
        }
        public void chechbox_CheckChanged(object sender, EventArgs e)
        {


            selectedList = new ObservableCollection<Student>();

            for (int i = 0; i < StudentList.Count; i++)
            {
                Student item = StudentList[i];

                if (item.IsChecked)
                {
                    selectedList.Add(item);
                }
            }

            

        }
        async public void BtnCanel(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new HomePage2());


        }
    }
}


