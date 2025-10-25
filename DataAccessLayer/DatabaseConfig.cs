using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public static class DatabaseConfig
    {
        // подключение к БД
        public static string ConnectionString =>
            @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\aleks\Desktop\project C#\Лаба 11111\Лаба 11111\BookLibrary.mdf;Integrated Security=True";
    }
}
