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
    
    class FiliereRepository
    {
        SQLiteConnection connection;
        
    public FiliereRepository()
        {
            connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            connection.CreateTable<Filiere>();

     // connection.Execute("INSERT INTO Filiere (FiliereName) VALUES('informatique')");
     // connection.Execute("INSERT INTO Filiere (FiliereName) VALUES('Genie mecanique')");



        }
        public List<Filiere> getall()
    {
            try
            {
              
                {
                    return connection.Table<Filiere>().ToList();
                }
            }
            catch (SQLiteException e)
            {
                return null;
            }
        }
        public List<Filiere> GetFilierName()
        {

            //connection.Execute("INSERT INTO Student VALUES");
            var ss = connection.Query<Filiere>("SELECT * FROM Filiere ");
            return ss;
           
        }
    }

}
