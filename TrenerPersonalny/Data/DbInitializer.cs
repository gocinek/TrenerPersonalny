using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrenerPersonalny.Models;

namespace TrenerPersonalny.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApiDbContext context)
        {
            if (context.Excercises.Any()) return;

            var excercises = new List<Excercise>
            {
                new Excercise
                {

                    Name = "Wyciskanie na ławce płaskiej",
                    Description = "Połóż się i wyciskaj",
                    PictureUrl  = "",
                    Type="klata",
                },
                new Excercise
                {

                    Name = "Wyciskanie na ławce poziomej",
                    Description = "Połóż się i wyciskaj",
                    PictureUrl  = "",
                    Type="klata",
                },
                new Excercise
                {

                    Name = "Podciąganie nachwytem",
                    Description = "Podciągaj się nachwytem",
                    PictureUrl  = "",
                    Type="plecy",
                },
            };

            foreach (var ex in excercises)
            {
                context.Excercises.Add(ex);
            }

            context.SaveChanges();

            var userRole = new List<UserRole>
            {
                new UserRole
                {
                    role = "Client"
                },
                new UserRole
                {
                    role = "Trainer"
                },
                new UserRole
                {
                    role = "Admin"
                },
            };

            foreach (var ur in userRole)
            {
                context.UserRoles.Add(ur);
            }

            context.SaveChanges();
        }
    }
}
