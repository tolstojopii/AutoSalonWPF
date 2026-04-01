using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSalon.Entities
{
    public static class DbInitializer
    {
        public static void Initialize(AutoSalonContext context)
        {
            context.Database.EnsureCreated();

            // Роли
            if (!context.Roles.Any())
            {
                context.Roles.AddRange(
                    new Role { Id = 1, Name = "admin" },
                    new Role { Id = 2, Name = "client" }
                );
                context.SaveChanges();
            }

            // Пользователь admin (пароль 111)
            if (!context.Users.Any(u => u.Login == "admin"))
            {
                context.Users.Add(new User
                {
                    Login = "admin",
                    Password = "111",
                    Roleid = 1
                });
                context.SaveChanges();
            }

            // Пол (Мужской, Женский)
            if (!context.Genders.Any())
            {
                context.Genders.AddRange(
                    new Gender { Code = 'М', Name = "Мужской" },
                    new Gender { Code = 'Ж', Name = "Женский" }
                );
                context.SaveChanges();
            }
        }
    }
}