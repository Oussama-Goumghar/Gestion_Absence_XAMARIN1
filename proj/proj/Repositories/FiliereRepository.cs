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
    
    class FiliereRepository
    {
        SQLiteAsyncConnection connection;
        
    public FiliereRepository()
        {
            connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            connection.CreateTableAsync<Filiere>();

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
    }

}
