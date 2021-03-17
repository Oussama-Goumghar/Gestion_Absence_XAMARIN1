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
    public partial class AddStudent : ContentPage
    {
        FiliereRepository dataFilier = new FiliereRepository();
        string selectedFilierName;
        Dictionary<String,int> Dc;
        public AddStudent()
        {

            InitializeComponent();
           
        }

        async protected override void OnAppearing()
        {
            Dc = new Dictionary<string, int>();
            var Nameist = new List<string>();
            var flName = await dataFilier.GetFilierName();

            foreach (Filiere Data in flName)
            {

                Dc.Add(Data.FiliereName, Data.IdFiliere);


            }
            foreach (var value in Dc.Keys)
                Nameist.Add(value);
            txtFiliere.ItemsSource = Nameist;
            base.OnAppearing();
        }
        public void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {


            int selectedIndex = txtFiliere.SelectedIndex;

            if (selectedIndex > -1)
            {
                StudentRepository StudentData = new StudentRepository();
                selectedFilierName = txtFiliere.Items[selectedIndex];
            }
        }

        async public void BtnAddStudent(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtCin.Text) || string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtPhone.Text) || string.IsNullOrWhiteSpace(txtNom.Text) || string.IsNullOrWhiteSpace(txtPrenom.Text) || Dc[selectedFilierName]==null)
            {
                await DisplayAlert("Erorr ", "If remplire tous les champs ", "Ok", "Cancel");

            }
            else
            {



                Student student = new Student()
                {
                    cin = txtCin.Text,
                    Email = txtEmail.Text,
                    Phone = txtPhone.Text,
                    nom = txtNom.Text,
                    Prenom = txtPrenom.Text,
                    IdFiliere = Dc[selectedFilierName]
                };

                StudentRepository studentDB = new StudentRepository();


                try
                {


                    

                        bool rowadd = await studentDB.AddStudent(student);
                        if (!rowadd)
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                var resut = await this.DisplayAlert("Erorr ", "Student not add ", "Ok", "Cancel");
                                if (resut)
                                {
                                    await Navigation.PushAsync(new AddStudent());
                                }

                            });

                        }
                        else
                        {

                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                var resut = await this.DisplayAlert("Congratulation ", "Student Add Sucessfull ", "Ok", "Cancel");
                                if (resut)
                                {
                                    await Navigation.PushAsync(new HomePage2());
                                }
                            });
                        }
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                }

            }
        }
        async public void BtnCancel (object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomePage2());
        }

        
    }
}

                


            
        
    
