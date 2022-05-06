using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrenerPersonalny.Configuration;
using TrenerPersonalny.Models;
using TrenerPersonalny.Models.Orders;

namespace TrenerPersonalny.Data
{
    public class DbInitializer
    {
        public static async Task Initialize(ApiDbContext context, UserManager<Client> userManager)
        {

            if (!userManager.Users.Any())
            {
                var admin = new Client
                {
                    Email = "admin@example.com",
                    UserName = "admin",
                    Person = new Person
                    {
                        LastName = "Nowak",
                        ProfileImg = "/Photos/Trainers/Kowalski.png"
                    }
                    // NormalizedUserName = "ADMIN",
                    //  PasswordSalt = salt,
                    //Password = HashPass.hashPass("Test1!", salt)
                };

                await userManager.CreateAsync(admin, "Test1!");
                await userManager.AddToRoleAsync(admin, "Admin");

                var trainer = new Client
                {
                    Email = "trainer@example.com",
                    UserName = "trainer",
                    Person = new Person
                    {
                        LastName = "Testowy",
                        Trainers = new Trainers
                        {
                            Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                            Price = 200
                        }
                    }
                    // NormalizedUserName = "Trainer",

                };

                await userManager.CreateAsync(trainer, "Test1!");
                await userManager.AddToRoleAsync(trainer, "Trainer");

                var trainer2 = new Client
                {
                    Email = "trainer2@example.com",
                    UserName = "trainer2",
                    Person = new Person
                    {
                        LastName = "Smuda",
                        FirstName = "Jose carlos",
                        ProfileImg = "/Photos/Trainers/Smuda.png",
                        Trainers = new Trainers
                        {
                            Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                            Price = 300
                        }
                    }
                    // NormalizedUserName = "Trainer",

                };

                await userManager.CreateAsync(trainer2, "Test1!");
                await userManager.AddToRoleAsync(trainer2, "Trainer");

                var trainer3 = new Client
                {
                    Email = "trainer3@example.com",
                    UserName = "trainer3",
                    Person = new Person
                    {
                        LastName = "Adamiak",
                        FirstName = "Anna",
                        ProfileImg = "/Photos/Trainers/Adamiak.png",
                        Trainers = new Trainers
                        {
                            Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                            Price = 175
                        }
                    }
                    // NormalizedUserName = "Trainer",

                };

                await userManager.CreateAsync(trainer3, "Test1!");
                await userManager.AddToRoleAsync(trainer3, "Trainer");


                //salt = HashPass.salt();
                var client = new Client
                {
                    Email = "client@example.com",
                    UserName = "client",
                    Person = new Person
                    {
                        LastName = "Kowalski",
                        ProfileImg = "/Photos/Trainers/Nowak.png"
                    }
                    //NormalizedUserName = "client",

                };

                await userManager.CreateAsync(client, "Test1!");
                await userManager.AddToRoleAsync(client, "Client");

            }

            if (context.Excercises.Any()) return;

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
                    Type = "Nogi"
                },
                 new ExcerciseType
                {
                    Type = "Łydki"
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
                PictureUrl = "/Photos/Atlas/wyciskanie-na-lawce-plaskiej.jpg",
                ExcerciseTypeId = 3

                },
                new Excercises
                {

                    Name = "Uginanie ramion z hantelkami - na ławce prostej",
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                    PictureUrl  = "/Photos/Atlas/Biceps-hantelki.jpg",
                    ExcerciseTypeId = 4
                },
                new Excercises
                {

                    Name = "Wyciskanie na ławce poziomej",
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                    PictureUrl  = "/Photos/Atlas/wyciskanie-na-lawce-poziomej.jpg",
                    ExcerciseTypeId = 3
                },
                new Excercises
                {

                    Name = "Podciąganie nachwytem",
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                    PictureUrl  = "/Photos/Atlas/Podciąganie-nachwytem.jpg",
                    ExcerciseTypeId = 2
                },
                new Excercises
                {

                    Name = "Przysiady ze sztangą z przodu",
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                    PictureUrl  = "/Photos/Atlas/Przysiady-ze-sztanga-przod.jpeg",
                    ExcerciseTypeId = 6
                }
            };

            foreach (var ex in excercises)
            {
                context.Excercises.Add(ex);
            }

            var orders = new List<Order>
            {
                new Order
                {
                    BuyerId = "admin",
                    OrderTrainer = new List<OrderTrainer>
                    {
                        new OrderTrainer {
                            Price = 175,
                            TrainerOrdered = new TrainerPersonOrdered
                            {
                                TrainerId = 1,
                                Name = "trainer",
                                PictureUrl = "/Photos/Trainers/Adamiak.png"

                            }
                        }
                    },
                    Expired = DateTime.Now.AddMonths(1),
                    Summary = 175
                },
                new Order
                {
                    BuyerId = "client",
                    OrderTrainer = new List<OrderTrainer>
                    {
                        new OrderTrainer {
                            Price = 300,
                            TrainerOrdered = new TrainerPersonOrdered
                            {
                                TrainerId = 1,
                                Name = "trainer2",
                                PictureUrl = "/Photos/Trainers/Smuda.png"
                            }
                        }
                    },
                    Expired = DateTime.Now.Date.AddMonths(1),
                    Summary = 300
                }

            };
            foreach (var ord in orders)
            {
                context.Orders.Add(ord);
            }

      
            context.SaveChanges();
            
        }
    }
}
