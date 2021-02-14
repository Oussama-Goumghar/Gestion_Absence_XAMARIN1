using DataAccessXamarin;
using proj.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace proj.Repositories
{
    class AbsenceRepository
    {
      private SQLiteConnection _connection;
       
            public AbsenceRepository()
        {
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();

            _connection.CreateTable<Absence>();
        }

        // public int InsertAbsence(int IdStudent, int IdLesson , Boolean IsPersnt)
        //
        // rows =  connection.Execute($"INSERT INTO Absence (IdStudent,IdLesson,IsPersnt) VALUES('{IdStudent}','{IdLesson}','{IsPersnt}')");
        //return rows;
        //}
        public int InsertAbsence(Absence absence)
        {

            //connection.Execute("INSERT INTO Student VALUES");
            int rows = _connection.Insert(absence);
            return rows;

        }
        public int CountAbsence()
        {
            var allItems = _connection.Table<Absence>().ToList();
            int count = allItems.Count();
            return count;



        }
        public List<Student> GetStudentByLessonId(int idLessons)
        {

            //connection.Execute("INSERT INTO Student VALUES");
            var ss = _connection.Query<Student>($"SELECT * FROM Student WHERE IdStudent in (SELECT IdStudent FROM Absence WHERE IdLesson = '{idLessons}') ");
            return ss;

        }
        public List<Absence> GetStudntByid(int idLStudent,int IdLessons)
        {

            //connection.Execute("INSERT INTO Student VALUES");
            var ss = _connection.Query<Absence>($"SELECT * FROM Absence WHERE IdStudent = '{idLStudent}' AND IdLesson={IdLessons}");
            return ss;

        }
        public int UpdateAbcense(int Id , bool IsPersnt)
        {
         
            int res = _connection.Execute($"update Absence set IsPersnt={IsPersnt} where IdAbsence={Id} ");
            return res;

        }

       
    }
}
