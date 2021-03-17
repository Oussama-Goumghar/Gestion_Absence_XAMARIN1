using proj.Models;
using proj.Repositories;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace proj.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class SearchPage : ContentPage
    {
        FiliereRepository dataFilier;
        Dictionary<String, int> DcFilier;
        Dictionary<String, int> DcLesson;
        Dictionary<String, int> DcStudent;
        LessonRepository dataLesson = new LessonRepository();
        AbsenceRepository DataAbsence = new AbsenceRepository();
        StudentRepository StudentData = new StudentRepository();
        ObservableCollection<Absence> sourceData;
        ObservableCollection<Student> students;
        ObservableCollection<Absence> NowselectedListPresnet;
        ObservableCollection<Student> NowselectedListAbsence;
      

        public SearchPage()
        {
            dataFilier = new FiliereRepository();
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
            FilierPicker.ItemsSource = Nameist;
            base.OnAppearing();
        }


        async public void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = FilierPicker.SelectedIndex;
            if (selectedIndex != -1)
            {
                var FilierName = FilierPicker.Items[selectedIndex];
                var id = DcFilier[FilierName];
                DcLesson = new Dictionary<string, int>();
                var NameList = new List<string>();
                var LessonVar =await dataLesson.GetLessonNameByFilier(id);
                foreach (Lesson Data in LessonVar)
                {
                    DcLesson.Add(Data.LessonName, Data.IdLesson);
                    NameList.Add(Data.LessonName);
                }
                LessonPicker.ItemsSource = NameList;
            }
        }
        async public void OnLessonPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = LessonPicker.SelectedIndex;
            if (selectedIndex != -1)
            {
                var LessonName = LessonPicker.Items[selectedIndex];
                var idLesson = DcLesson[LessonName];
                DcStudent = new Dictionary<string, int>();
                var NameList = new List<string>();
                var StudentListe =await DataAbsence.GetStudentByLessonId(idLesson);
                foreach (Student Data in StudentListe)
                {
                    bool KeyExist = DcStudent.ContainsKey(Data.nom);
                    if (!KeyExist)
                    {
                        DcStudent.Add(Data.nom, Data.IdStudent);
                        NameList.Add(Data.nom);
                    }
                }
                StudentPicker.ItemsSource = NameList;
            }
        }
        async public void OnStudentPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = StudentPicker.SelectedIndex;
            int selectedIndexLesson = LessonPicker.SelectedIndex;

            if (selectedIndex != -1 )

            {
                var LessonName = LessonPicker.Items[selectedIndexLesson];
                var idLesson = DcLesson[LessonName];
                var StuentName = StudentPicker.Items[selectedIndex];
                int IdStudent = DcStudent[StuentName];
                
                sourceData = new ObservableCollection<Absence>(await DataAbsence.GetStudntByid(IdStudent,idLesson));
                students = new ObservableCollection<Student>();
                //foreach (var sd in sourceData)
                //{
                //    int ids = sd.IdStudent;
                //    Student s =await StudentData.GetStudntByid(ids);
                //    Student stdn = new Student()
                //    {

                //        IdAbsence = sd.IdAbsence,
                //        nom = s.nom,
                //        Prenom = s.Prenom,
                //        IsChecked = sd.IsPresent


                //    };
                //    students.Add(stdn);
                //}

                listStudent.ItemsSource = sourceData;
            }

        }
        public void chechbox_CheckChanged(object sender, EventArgs e)
        {


            NowselectedListPresnet = new ObservableCollection<Absence>();
           // NowselectedListAbsence = new ObservableCollection<Student>();

            for (int i = 0; i < sourceData.Count; i++)
            {
                Absence item = sourceData[i];

                if (item.IsPresent)
                {
                    NowselectedListPresnet.Add(item);
                }
            }

         //   DisplayAlert("Title", NowselectedListAbsence.Count + " student are relly absence   " + NowselectedListPresnet.Count + "student are relly presnt", "Cancel");

        }
        async public void Update_btn(object sender, EventArgs e)
        {
            if (NowselectedListPresnet.Count > 0)
            {
                foreach (var absence in NowselectedListPresnet)
                {
                   await DataAbsence.UpdateAbcense(absence.IdAbsence, true);
                }
            }

            //if (NowselectedListAbsence.Count > 0)
            //{
            //    foreach (var student in NowselectedListAbsence)
            //    {
            //       await DataAbsence.UpdateAbcense(student.IdAbsence, true);
                    
            //    }
                
            //}
         await  DisplayAlert("UPDATE", "table absence update", "OK");
            await Navigation.PushAsync(new HomePage2());
        }
     async public void BtnCanel(object sender, EventArgs e)
    {

        await Navigation.PushAsync(new HomePage2());


    }
   }
    
}