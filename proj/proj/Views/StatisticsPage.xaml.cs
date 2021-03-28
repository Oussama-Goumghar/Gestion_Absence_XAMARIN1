using Microcharts;
using proj.Models;
using proj.Repositories;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Entry = Microcharts.ChartEntry;

namespace proj.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StatisticsPage : ContentPage
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
        ObservableCollection<Absence> NowselectedListAbsence;

        public StatisticsPage()
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
                var LessonVar = await dataLesson.GetLessonNameByFilier(id);
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
                var StudentListe = await DataAbsence.GetStudentByLessonId(idLesson);
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

            if (selectedIndex != -1)

            {
                var LessonName = LessonPicker.Items[selectedIndexLesson];
                var idLesson = DcLesson[LessonName];
                var StuentName = StudentPicker.Items[selectedIndex];
                int IdStudent = DcStudent[StuentName];

                NowselectedListAbsence = new ObservableCollection<Absence>(await DataAbsence.GetStudntByid(IdStudent, idLesson));
                NowselectedListPresnet = new ObservableCollection<Absence>(await DataAbsence.GetPresentByid(IdStudent, idLesson));
                students = new ObservableCollection<Student>();
                var countAbsence = NowselectedListAbsence.Count;
                var countPresent = NowselectedListPresnet.Count;
                List<Entry> entries = new List<Entry>
        {
            new Entry(countAbsence)
            {
                Color=SKColor.Parse("#FF1493"),
                Label="Abssent",
                ValueLabel=$"{countAbsence}",
            },
            new Entry(countPresent)
            {
                Color=SKColor.Parse("#00BFFF"),
                Label="Present",
                ValueLabel=$"{countPresent}",
            }
        };


                chart.Chart = new RadarChart { Entries = entries,LabelTextSize=45f };

            }

        }


        List<Entry> entries = new List<Entry>
        {
            new Entry(200)
            {
                Label="Abssence",
                ValueLabel="200",
                Color=SKColor.Parse("#FF1493"),
            },
            new Entry(400)
            {
                Label="Abssence",
                ValueLabel="400",
                Color=SKColor.Parse("#00BFFF"),
            }
        };

    }
}