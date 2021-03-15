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
using SQLiteNetExtensionsAsync.Extensions;

namespace proj.Repositories
{
    
    class LessonRepository
    {SQLiteAsyncConnection connection;

        FiliereRepository filiereRepository = new FiliereRepository();
        public LessonRepository()
        {
            connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            connection.CreateTableAsync<Lesson>();
        }
        async public Task<List<Lesson>> GetLessonName()
        {
            var ss =await connection.QueryAsync<Lesson>("SELECT * FROM Lesson");
            return ss;
        }
        async public Task<List<Lesson>> GetLessonNameByFilier(int idfilier)
        {
            
            var ss =await connection.QueryAsync<Lesson>($"SELECT * FROM Lesson WHERE IdFiliere ='{idfilier}' ");
            return ss;

        }
        async public Task<bool> insertToLesson(string LessonName , int FilierId)
        {
           if(await checkExistance(LessonName, FilierId))
            {
                return false;
            }
            else
            {
                Lesson lesson = new Lesson()
                {
                    LessonName = LessonName,
                    IdFiliere = FilierId
                };
                await connection.InsertAsync(lesson);
                //Filiere filiere = await filiereRepository.connection.GetWithChildrenAsync<Filiere>(FilierId);      
                //    filiere.lessons.Add(lesson);
                //    await connection.UpdateWithChildrenAsync(filiere);    
                  
                return true;

            }

        }

        async public Task<bool> checkExistance(string LessonName, int FilierId)
        {
            bool res = false;
            Filiere filiere = await filiereRepository.GetFiliereById(FilierId);
              
            foreach (var item in filiere.lessons)
            {
                if (item.LessonName == LessonName)
                {
                    res = true;
                }
            }
            return res;
            
        }


        
    }
}
