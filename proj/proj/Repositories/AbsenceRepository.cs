using proj.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Essentials;

namespace proj.Repositories
{
    class AbsenceRepository
    {
        SQLiteConnection connection;
       
            public AbsenceRepository()
        {
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "databasePr.db3");
        connection = new SQLiteConnection(dbPath);
        connection.CreateTable<Absence>();
        }

        // public int InsertAbsence(int IdStudent, int IdLesson , Boolean IsPersnt)
        //
        // rows =  connection.Execute($"INSERT INTO Absence (IdStudent,IdLesson,IsPersnt) VALUES('{IdStudent}','{IdLesson}','{IsPersnt}')");
        //return rows;
        //}
        public int InsertAbsence(Absence absence)
        {

            //connection.Execute("INSERT INTO Student VALUES");
            int rows = connection.Insert(absence);
            return rows;

        }
        public int CountAbsence()
        {
            var allItems = connection.Table<Absence>().ToList();
            int count = allItems.Count();
            return count;



        }
        public List<Student> GetStudentByLessonId(int idLessons)
        {

            //connection.Execute("INSERT INTO Student VALUES");
            var ss = connection.Query<Student>($"SELECT * FROM Student WHERE IdStudent in (SELECT IdStudent FROM Absence WHERE IdLesson = '{idLessons}') ");
            return ss;

        }
        public List<Absence> GetStudntByid(int idLStudent,int IdLessons)
        {

            //connection.Execute("INSERT INTO Student VALUES");
            var ss = connection.Query<Absence>($"SELECT * FROM Absence WHERE IdStudent = '{idLStudent}' AND IdLesson={IdLessons}");
            return ss;

        }
        public int UpdateAbcense(int Id , bool IsPersnt)
        {
         
            int res = connection.Execute($"update Absence set IsPersnt={IsPersnt} where IdAbsence={Id} ");
            return res;

        }

       
    }
}
