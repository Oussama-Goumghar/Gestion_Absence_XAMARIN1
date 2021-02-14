using DataAccessXamarin;
using proj.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace proj.Repositories
{
    
    class LessonRepository
    {SQLiteAsyncConnection connection;
        public LessonRepository()
        {
            connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            connection.CreateTableAsync<Lesson>();
        }
        async public Task<List<Lesson>> GetLessonName()
        {
            var ss =await connection.QueryAsync<Lesson>("SELECT * FROM Lesson ");
            return ss;
        }
        async public Task<List<Lesson>> GetLessonNameByFilier(int idfilier)
        {
            var ss =await connection.QueryAsync<Lesson>($"SELECT * FROM Lesson WHERE IdFiliere ='{idfilier}' ");
            return ss;

        }
        async public Task<int> insertToLesson(string LessonName , int FilierId)
        {
           int res =await  connection.ExecuteAsync($"INSERT INTO Lesson (LessonName,IdFiliere) VALUES('{LessonName}','{FilierId}')");
            return res;
        }
        
    }
}
