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
    class AbsenceRepository
    {
      private SQLiteAsyncConnection _connection;

        private Task<List<Absence>> absences;
            public AbsenceRepository()
        {
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();

            _connection.CreateTableAsync<Absence>();

            absences = GetAllAbsences();
        }

        async public Task<List<Absence>> GetAllAbsences()
        {
            return await _connection.Table<Absence>().ToListAsync();
        }

       
         public Task<int> InsertAbsence(Absence absence)
        {
            var row= _connection.InsertAsync(absence);
            return row;
        }
        public int CountAbsence()
        {
            
            int count = absences.Result.Count();
            return count;
        }
        async public Task<List<Student>> GetStudentByLessonId(int idLessons)
        {
            var ss =await _connection.QueryAsync<Student>($"SELECT * FROM Student WHERE IdStudent in (SELECT IdStudent FROM Absence WHERE IdLesson = '{idLessons}') ");
            return ss;

        }
        async public Task<List<Absence>> GetStudntByid(int idLStudent,int IdLessons)
        {
            var ss = await _connection.QueryAsync<Absence>($"SELECT * FROM Absence WHERE IdStudent = '{idLStudent}' AND IdLesson={IdLessons} AND IsPresent=0");
            return ss;

        }
        async public Task<List<Absence>> GetPresentByid(int idLStudent, int IdLessons)
        {
            var ss = await _connection.QueryAsync<Absence>($"SELECT * FROM Absence WHERE IdStudent = '{idLStudent}' AND IdLesson={IdLessons} AND IsPresent=1");
            return ss;

        }
        async public Task<int> UpdateAbcense(int Id , bool IsPersnt)
        {
         
            int res =await _connection.ExecuteAsync($"update Absence set IsPresent={IsPersnt} where IdAbsence={Id} ");
            return res;

        }

       
    }
}
