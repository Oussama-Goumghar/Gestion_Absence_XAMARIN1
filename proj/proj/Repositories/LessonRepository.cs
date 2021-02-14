using DataAccessXamarin;
using proj.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace proj.Repositories
{
    
    class LessonRepository
    {SQLiteConnection connection;
        public LessonRepository()
        {
            connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            connection.CreateTable<Lesson>();
            // connection.Execute("INSERT INTO Lesson (LessonName) VALUES('C#')");
            //connection.Execute("INSERT INTO Lesson (LessonName) VALUES('JAVA')");
            //connection.Execute("INSERT INTO Lesson (LessonName) VALUES('PHP')");
            //connection.Execute("INSERT INTO Lesson (LessonName) VALUES('C')");
        }
        public List<Lesson> GetLessonName()
        {

            //connection.Execute("INSERT INTO Student VALUES");
            var ss = connection.Query<Lesson>("SELECT * FROM Lesson ");
            return ss;

        }
        public List<Lesson> GetLessonNameByFilier(int idfilier)
        {

            //connection.Execute("INSERT INTO Student VALUES");
            var ss = connection.Query<Lesson>($"SELECT * FROM Lesson WHERE IdFiliere ='{idfilier}' ");
            return ss;

        }
        public int insertToLesson(string LessonName , int FilierId)
        {
           int res =  connection.Execute($"INSERT INTO Lesson (LessonName,IdFiliere) VALUES('{LessonName}','{FilierId}')");
            return res;
        }
        //connection.Execute("INSERT INTO Filiere (FiliereName) VALUES('informatique')");
    }
}
