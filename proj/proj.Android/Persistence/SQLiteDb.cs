using System;
using System.IO;
using SQLite;
using Xamarin.Forms;
using DataAccessXamarin.Droid;

[assembly: Dependency(typeof(SQLiteDb))]

namespace DataAccessXamarin.Droid
{
	public class SQLiteDb : ISQLiteDb
	{
		public SQLiteConnection GetConnection()
		{
			var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			var path = Path.Combine(documentsPath, "MySQLite.db3");

			return new SQLiteConnection(path);
		}
	}
}

