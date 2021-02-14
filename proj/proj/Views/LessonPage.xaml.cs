using proj.Models;
using proj.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace proj.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LessonPage : ContentPage
    {
        LessonRepository dataLesson = new LessonRepository();
        string  selectedFilierName;
        Dictionary<String, int> Dc;
        int id;
        public LessonPage()
        {
            InitializeComponent();
            FiliereRepository dataFilier = new FiliereRepository();
            
           


            var Nameist = new List<string>();
            Dc = new Dictionary<string, int>();
            var flName = dataFilier.GetFilierName();
            foreach (Filiere Data in flName)
            {
                Dc.Add(Data.FiliereName, Data.IdFiliere);
            }
            foreach (var value in Dc.Keys)
                Nameist.Add(value);
            PickerFilier.ItemsSource = Nameist;
        }
        public void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {


            int selectedIndex = PickerFilier.SelectedIndex;

            if (selectedIndex != -1)
            {
                StudentRepository StudentData = new StudentRepository();
                selectedFilierName = PickerFilier.Items[selectedIndex];
                id = Dc[selectedFilierName];
            
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

            var LessonName = txtLessName.Text;
            Console.WriteLine(selectedFilierName);
            dataLesson.insertToLesson(LessonName, id);
        }
    }
}