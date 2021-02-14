using DataAccessXamarin;
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
        async public Task<int> AddStudent(Student student)
        {
            int rows =await connection.InsertAsync(student);
            return rows;
        }
        async public Task<List<Student>> GetStudntByFilier(int  filierid)
        {

            //connection.Execute("INSERT INTO Student VALUES");
            var ss =await connection.QueryAsync<Student>($"SELECT * FROM Student  WHERE IdFiliere='{filierid}'");
            return ss;

        }
        //async public Task<IEnumerable<Student>> GetStudent()
        //{
        //    return (from u in connection.Table<Student>()
        //            select u).ToList();
        //}
        async public Task<Student> GetStudntByid(int idLStudent)
        {
            return await connection.Table<Student>().FirstOrDefaultAsync(std => std.IdStudent == idLStudent);
          

        }

        
    }
}


