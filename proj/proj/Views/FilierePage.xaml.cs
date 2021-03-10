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
    public partial class FilierePage : ContentPage
    {
        FiliereRepository dataFilier = new FiliereRepository();
        public FilierePage()
        {
            InitializeComponent();
        }

        async public void BtnCancel(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomePage());
        }

        async private void BtnSave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(filierName_Entry.Text))
            {
                await DisplayAlert("Erorr ", "Il faut remplire le champ de la filiére", "Ok", "Cancel");
            }
            else
            {
                Filiere filiere = new Filiere()
                {
                    FiliereName = filierName_Entry.Text
                };

            FiliereRepository dataFilier = new FiliereRepository();

            bool resultat =await dataFilier.AddFiliere(filiere);

                if (resultat)
                {
                   await DisplayAlert("Congratulation ", "La filiére est ajouter avec succée ", "Ok");
                    await Navigation.PushAsync(new HomePage());
                }
                else
                {
                    await DisplayAlert("Error ", "La fliére est déja Existe", "Ok");
                    await Navigation.PushAsync(new FilierePage());

                }

            }

        }
    }
}