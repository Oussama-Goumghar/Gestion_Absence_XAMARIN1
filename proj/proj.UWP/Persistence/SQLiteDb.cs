using System;
using System.IO;
using SQLite;
using Xamarin.Forms;
using DataAccessXamarin.UWP;
using Windows.Storage;

[assembly: Dependency(typeof(SQLiteDb))]

namespace DataAccessXamarin.UWP
{
    public class SQLiteDb : ISQLiteDb
    {
        public SQLiteConnection GetConnection()
        {
			var documentsPath = ApplicationData.Current.LocalFolder.Path;
        	var path = Path.Combine(documentsPath, "MySQLite.db3");
        	return new SQLiteConnection(path);
        }
    }
}

