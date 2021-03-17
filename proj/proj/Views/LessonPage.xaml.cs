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
        FiliereRepository dataFilier = new FiliereRepository();

        string selectedFilierName;
        Dictionary<String, int> Dc;
        int id;

        public LessonPage()
        {
            InitializeComponent();
        }

        async protected override void OnAppearing()
        {
            var Nameist = new List<string>();
            Dc = new Dictionary<string, int>();
            var flName = await dataFilier.GetFilierName();
            foreach (Filiere Data in flName)
            {
                Dc.Add(Data.FiliereName, Data.IdFiliere);
            }
            foreach (var value in Dc.Keys)
                Nameist.Add(value);
            PickerFilier.ItemsSource = Nameist;
            base.OnAppearing();
        }

        async public void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {


            int selectedIndex = PickerFilier.SelectedIndex;

            if (selectedIndex != -1)
            {
                FiliereRepository filiereRepository = new FiliereRepository();
                StudentRepository StudentData = new StudentRepository();
                selectedFilierName = PickerFilier.Items[selectedIndex];
                id = Dc[selectedFilierName];
                //Filiere filiere = await filiereRepository.GetFiliereById(id);
                //var count = filiere.lessons.Count();
                //await this.DisplayAlert("info", "le nombre des lessons est " + count, "ok");




            }
        }

        async private void Button_Clicked(object sender, EventArgs e)
        {
            int selectedIndex = PickerFilier.SelectedIndex;

            if (string.IsNullOrWhiteSpace(txtLessName.Text) && selectedIndex == -1) {
                await this.DisplayAlert("Error", "Il faut remplire tous les champs", "ok");
            }
            else {
                FiliereRepository filiereRepository = new FiliereRepository();

            var LessonName = txtLessName.Text;
            Console.WriteLine(selectedFilierName);
        var res=   await dataLesson.insertToLesson(LessonName, id);
            if (res)
            {
                //Filiere filiere = await filiereRepository.GetFiliereById(id);
                //var count = filiere.lessons.Count();
                await this.DisplayAlert("info","La lesson est ajouter avec success", "ok");

                    await Navigation.PushAsync(new HomePage2());
                }
                //if (filiere.lessons != null)
                //{
                //    var count = filiere.lessons.Count();
                //    await this.DisplayAlert("info", "le nombre des lessons est " + count, "ok");
                //}
                //else
                //{
                //    await this.DisplayAlert("info","la liste est null", "ok");

                //   }

            }




        }

        async private void Button_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomePage2());
        }
    }
}