﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using proj.Models;
using SQLite;
using proj.Views;
using Xamarin.Forms;
using System.Linq;
using Xamarin.Essentials;
using System.IO;
using DataAccessXamarin;

namespace proj.Repositories
{
    public  class UserRepository
     {
        SQLiteAsyncConnection connection;
     
        public UserRepository()
        {
            connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            connection.CreateTableAsync<User>();
        }

      

        //public IEnumerable<User> GetUsers()
        //{
        //    return (from u in connection.Table<User>()
        //            select u).ToList();
        //}
        //public void DeleteUser(int id)
        //{
        //    connection.Delete<User>(id);
        //}
        async public  Task<bool>  AddUser(User user)
        {
            if (await LoginValidate(user.Username,user.Password))
            {
                return false;
            }
            else
            {
                 await connection.InsertAsync(user);
                return true;
            }
             
        }

        async public Task<bool> LoginValidate(string userName1, string pwd1)
        {
            var data = connection.Table<User>();
            var d1 = await data.Where(x => x.Username == userName1 && x.Password == pwd1).FirstOrDefaultAsync();
            if (d1 != null)
            {
                return true;
            }
            else
                return false;
        }

        
        //public boolean checkAlreadyExist(String email)
        //{
        //    String query = "SELECT" + YOUR_EMAIL_COLUMN + FROM + TABLE_NAME + WHERE + YOUR_EMAIL_COLUMN + " =?";
        //    Cursor cursor = db.rawQuery(query, new String[] { email });
        //    if (cursor.getCount() > 0)
        //    {
        //        return false;
        //    }
        //    else
        //        return true;
        //}
    }
    
    }

