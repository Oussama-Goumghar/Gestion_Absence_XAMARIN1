using SQLite;

namespace DataAccessXamarin
{
    public interface ISQLiteDb
    {
        SQLiteAsyncConnection GetConnection();
    }
}

