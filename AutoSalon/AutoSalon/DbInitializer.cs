using AutoSalon.Entities;
using System.Collections;

namespace AutoSalon
{
    public static class DbInitializer
    {
        public static void Initialize(AutoSalonContext context)
        {
            context.Database.EnsureCreated();

            // Добавляем роли, если их нет
            if (!context.Roles.Any())
            {
                context.Roles.AddRange(
                    new Role { Id = 1, Name = "Администратор" },
                    new Role { Id = 2, Name = "Клиент" }
                );
                context.SaveChanges();
            }

            // Добавляем пользователя-администратора, если его нет
            if (!context.Users.Any(u => u.Login == "admin"))
            {
                context.Users.Add(new User
                {
                    Login = "admin",
                    Password = "admin",
                    Roleid = 1
                });
                context.SaveChanges();
            }

            // Добавляем полы, если их нет
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