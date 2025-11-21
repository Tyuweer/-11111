using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public static class DatabaseConfig
    {
        public static string ConnectionString =>
            @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=
            C:\USERS\ALEKS\DESKTOP\PROJECT C#\ЛАБА4АИС_САНЕК\-11111-DEVELOP\ЛАБА 11111\BOOKLIBRARY.MDF;
            Integrated Security=True";
    }
}
