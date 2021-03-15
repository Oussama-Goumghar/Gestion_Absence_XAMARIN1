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
		public SQLiteAsyncConnection GetConnection()
		{
			var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			var path = Path.Combine(documentsPath, "MySQLite.db1");

			return new SQLiteAsyncConnection(path);
		}
	}
}

