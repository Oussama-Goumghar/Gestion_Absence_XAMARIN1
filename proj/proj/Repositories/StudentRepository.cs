﻿using DataAccessXamarin;
using proj.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace proj.Repositories
{
    class StudentRepository
    {
        SQLiteAsyncConnection connection;

        public StudentRepository()
        {
            connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            connection.CreateTableAsync<Student>();
        }
        async public Task<bool> AddStudent(Student student)
        {
            if (await CheckExistence(student.cin))
            {
                return false;
            }
            else
            {
                await connection.InsertAsync(student);
                return true;
            }
            
        }
        async public Task<List<Student>> GetStudntByFilier(int  filierid)
        {
            var ss =await connection.QueryAsync<Student>($"SELECT * FROM Student  WHERE IdFiliere='{filierid}'");
            return ss;
        }
       
        async public Task<Student> GetStudntByid(int idLStudent)
        {
            return await connection.Table<Student>().FirstOrDefaultAsync(std => std.IdStudent == idLStudent);
        }

        async public Task<bool> CheckExistence(string CIN)
        {
            var data = connection.Table<Student>();
            var d1 = await data.Where(x => x.cin == CIN).FirstOrDefaultAsync();
            if (d1 != null)
            {
                return true;
            }
            else
                return false;
        }


    }
}


