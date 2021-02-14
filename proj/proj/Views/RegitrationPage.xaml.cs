using proj.Models;
using proj.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using SQLite;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace proj.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class RegitrationPage : ContentPage
    {

      

       
      
       



        public RegitrationPage()
        {
            InitializeComponent();
        }

        public async void btnAddUser(object sender, EventArgs e)
        {
            User users = new User()
            {
            Username = txtUsername.Text,
            Password = txtPassword.Text,
            Email = txtemail.Text,
            Phone = txtPhone.Text,

        };
           
            UserRepository usedb = new UserRepository();


            try
            {
                
               
                {

                    int rowadd = usedb.AddUser(users);
                    if (rowadd <=0)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            var resut = await this.DisplayAlert("Erorr ", "user not add ", "Ok","Cancel");
                            if (resut)
                            {
                                await Navigation.PushAsync(new RegitrationPage());
                            }

                        });
                      
                    }
                    else
                    {

                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            var resut = await this.DisplayAlert("Congratulation ", "user Registeration Sucessfull ", "Ok", "Cancel");
                            if (resut)
                            {
                                await Navigation.PushAsync(new LoginPage());
                            }
                        });
                    }
                    
                }
                

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
        }
    }
    }
