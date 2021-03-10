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

            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text) || string.IsNullOrWhiteSpace(txtemail.Text) || string.IsNullOrWhiteSpace(txtPhone.Text)){
               await DisplayAlert("Erorr ", "Il faut remplire tous les champs", "Ok", "Cancel");
            }
            else{
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
                        bool rowadd = await usedb.AddUser(users);
                        if (!rowadd)
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                var resut = await this.DisplayAlert("Erorr ", "User déja existe", "Ok", "Cancel");
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
    }
