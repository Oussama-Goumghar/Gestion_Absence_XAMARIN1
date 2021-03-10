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
    
    class FiliereRepository
    {
        SQLiteAsyncConnection connection;
        
    public FiliereRepository()
        {
           connection = DependencyService.Get<ISQLiteDb>().GetConnection();

            connection.CreateTableAsync<Filiere>().Wait();

            
      connection.ExecuteAsync("INSERT INTO Filiere (FiliereName) VALUES('informatique')");
      connection.ExecuteAsync("INSERT INTO Filiere (FiliereName) VALUES('Genie mecanique')");

        }
       async public Task<List<Filiere>> getall()
         {
            return await connection.Table<Filiere>().ToListAsync();
         }
       async public Task<List<Filiere>> GetFilierName()
        {

            var ss =await connection.QueryAsync<Filiere>("SELECT * FROM Filiere ");
            return ss;
           
        }


        async public Task<bool> AddFiliere(Filiere filiere)
        {
            if (await CheckExistance(filiere.FiliereName))
            {
                return false;
            }
            else
            {
                await connection.InsertAsync(filiere);
                return true;
            }

        }

        async public Task<bool> CheckExistance(string filiereName)
        {
            var data = connection.Table<Filiere>();
            var filiere = await data.Where(x => x.FiliereName==filiereName).FirstOrDefaultAsync();
            if (filiere != null)
            {
                return true;
            }
            else
                return false;
        }

        async public Task<Filiere> GetFiliereById(int id)
        {
            var data = connection.Table<Filiere>();
           return await data.Where(x => x.IdFiliere == id).FirstOrDefaultAsync();
        }
    }

}
