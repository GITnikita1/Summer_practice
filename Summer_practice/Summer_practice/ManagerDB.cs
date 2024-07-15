using Dapper;
using Npgsql;
using System.Data;

namespace DDPlanet
{
    internal class ManagerDB
    {
        private readonly NpgsqlConnection _link;
        public ManagerDB(string database_login)
        {
            _link = new NpgsqlConnection(database_login);
        }
        public NpgsqlConnection GetLink()
        {
            if (_link.State != ConnectionState.Open)
            {
                _link.Open();
            }
            return _link;
        }
        public void CloseConnection()
        {
            if (_link.State == ConnectionState.Open)
            {
                _link.Close();
            }
        }
        public static void FirstTask(NpgsqlConnection link)
        {
            var code_sql = "SELECT \"Users\".user_name, \"Roles\".rol_name FROM \"Users\" JOIN \"Users_Roles\" ON \"Users\".user_id = \"Users_Roles\".user_id JOIN \"Roles\" ON \"Users_Roles\".rol_id = \"Roles\".rol_id";
            var users_roles = link.Query(code_sql);
            Console.WriteLine("    Пользователь                 Роль");
            Console.WriteLine("---------------------------------------------");
            foreach (var user_rol in users_roles)
            {
                Console.WriteLine($"{user_rol.user_name}\t {user_rol.rol_name}");
            }
        }
        public static void SecondTask(NpgsqlConnection link)
        {
            var code_sql = "SELECT \"Roles\".rol_name, COUNT(\"Users_Roles\".user_id) AS user_count FROM \"Roles\" LEFT JOIN \"Users_Roles\" ON \"Roles\".rol_id = \"Users_Roles\".rol_id GROUP BY \"Roles\".rol_name";
            var counts_rol = link.Query(code_sql);
            foreach (var count_rol in counts_rol)
            {
                if (count_rol.user_count == 1)
                {
                    Console.WriteLine($"Роль \"{count_rol.rol_name}\" имеет {count_rol.user_count} человек.");
                }
                else
                {
                    Console.WriteLine($"Роль \"{count_rol.rol_name}\" имеют {count_rol.user_count} человек.");
                }
            }
        }
    }
}