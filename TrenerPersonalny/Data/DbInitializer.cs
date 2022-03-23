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

            var person = new List<Person>
            {
                new Person
                {
                    LastName = "Nowak"
                }
            };

            foreach (var pe in person)
            {
                context.Person.Add(pe);
            }
            context.SaveChanges();

            var excerciseType = new List<ExcerciseType>
            {
                new ExcerciseType
                {
                    Type = "Klatka"
                },
                new ExcerciseType
                {
                    Type = "Plecy"
                },
                new ExcerciseType
                {
                    Type = "Barki"
                },
                new ExcerciseType
                {
                    Type = "Biceps"
                },
                new ExcerciseType
                {
                    Type = "Triceps"
                },
                new ExcerciseType
                {
                    Type = "Nogi + łydki"
                },

            };

            foreach (var ext in excerciseType)
            {
                context.ExcerciseType.Add(ext);
            }

            var excercises = new List<Excercises>
            {
            new Excercises
            {

                Name = "Wyciskanie na ławce płaskiej",
                Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                PictureUrl = "/Photos/Atlas/Biceps-hantelki.jpg",
                excerciseTypeId = 1

                },
                new Excercises
                {

                    Name = "Uginanie ramion z hantelkami",
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                    PictureUrl  = "/Photos/Atlas/Biceps-hantelki.jpg",
                    excerciseTypeId = 3
                },
                new Excercises
                {

                    Name = "Wyciskanie na ławce poziomej",
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                    PictureUrl  = "/Photos/Atlas/Biceps-hantelki.jpg",
                    excerciseTypeId = 1
                },
                new Excercises
                {

                    Name = "Podciąganie nachwytem",
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                    PictureUrl  = "/Photos/Atlas/Biceps-hantelki.jpg",
                    excerciseTypeId = 2
                },
            };

            foreach (var ex in excercises)
            {
                context.Excercises.Add(ex);
            }

            context.SaveChanges();

            var userRole = new List<UserRoles>
            {
                new UserRoles
                {
                    role = "Client"
                },
                new UserRoles
                {
                    role = "Trainer"
                },
                new UserRoles
                {
                    role = "Admin"
                },
            };

            foreach (var ur in userRole)
            {
                context.UserRoles.Add(ur);
            }

            context.SaveChanges();

            var trainers = new List<Trainers>
            {
                new Trainers
                {
                    Description ="Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                    PictureUrl ="/Photos/Trainers/Adamiak.png",
                    Price = 200,
                    personId = 1
                },
                new Trainers
                {
                    Description ="Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.k",
                    PictureUrl ="/Photos/Trainers/Nowak.png",
                    Price = 150,
                    Rating = 6,
                    personId = 1
                },
                new Trainers
                {
                    Description ="Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                    PictureUrl ="/Photos/Trainers/Kowalski.png",
                    Price = 190,
                    Rating = 2,
                    personId = 1
                },
            };
            foreach (var tr in trainers)
            {
                context.Trainers.Add(tr);
            }

            context.SaveChanges();
        }
    }
}
