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
    public partial class HomePage : ContentPage
    {
       // string usernam;
        public HomePage( ) //string name
        {
            // this.usernam = name;
            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();
            // lbn.Text = "Welecom : "+usernam;
        }

        async    public  void lougOut_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }
        async public void BtnAddStudent(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddStudent());
        }

        async public void Button_Clicked(object sender, EventArgs e)
        {
           
            await Navigation.PushAsync(new AbsencePage());


        }
        async public void BtnLessonPage(object sender, EventArgs e)
        {
           
            await Navigation.PushAsync(new LessonPage());


        }
        async public void BtnSearch(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new StatisticsPage());


        }

        async public void ToolbarItem_Activated(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FilierePage());

        }
    }
}