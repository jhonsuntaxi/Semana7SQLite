using System;
using System.Collections.Generic;
using System.Text;
using SQLite;


namespace Semana7SQLite
{
    public interface Database
    {
        SQLiteAsyncConnection GetConnection();
    }
}
