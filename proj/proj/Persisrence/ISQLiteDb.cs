using SQLite;

namespace DataAccessXamarin
{
    public interface ISQLiteDb
    {
        SQLiteConnection GetConnection();
    }
}

