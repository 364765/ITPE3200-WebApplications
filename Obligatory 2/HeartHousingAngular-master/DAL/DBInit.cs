using System.Runtime.CompilerServices;
using Castle.Core.Resource;
using HeartHousingAngular.Models;
using Microsoft.EntityFrameworkCore;

namespace HeartHousingAngular.DAL
{
    public class DBInit
    {
        public static void Seed(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            RentalDbContext context = serviceScope.ServiceProvider.GetRequiredService<RentalDbContext>();
            //RentalDbContext context = serviceScope.ServiceProvider.GetRequiredService<RentalDbContext>();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();


            /*---------------DB Seeding data---------------------*/
            if (!context.Rentals.Any())
            {
                var rentals = new List<Rental>
                {
                    new Rental
                    {
                        Name = "Modern appartment",
                        Address = "Storgata 11, Oslo",
                        PricePrNight = 1500,
                        RentalType = "Appartment",
                        BedNr = 1,
                        BathNr = 1,
                        Area = 20,
                        Description = "Modern appartment located centrally in Oslo.",
                        ImageUrl = "assets/images/appartment1/Livingroom.png",
                        ImageUrl2 = "assets/images/appartment1/Bedroom.png",
                        ImageUrl3 = "assets/images/appartment1/Bathroom.png",

                    },
                    new Rental
                    {
                        Name = "Cozy cabin",
                        Address = "Holetråkket 4, Rena",
                        PricePrNight = 800,
                        RentalType = "Cabin",
                        BedNr = 1,
                        BathNr = 1,
                        Area = 15,
                        Description = "Cozy cabin in Skramstadsætra, close to Rena.",
                        ImageUrl = "assets/images/cabin1/Livingroom.png",
                        ImageUrl2 = "assets/images/cabin1/Bedroom.png",
                        ImageUrl3 = "assets/images/cabin1/Bathroom.png",

                    },
                    new Rental
                    {
                        Name = "Renovated house",
                        Address = "Benterudbakken 7, Hamar",
                        PricePrNight = 3000,
                        RentalType = "House",
                        BedNr = 2,
                        BathNr = 2,
                        Area = 50,
                        Description = "Newly renovated house in Hamar, 15 minutes away from the city centre.",
                        ImageUrl = "assets/images/house1/Livingroom.png",
                        ImageUrl2 = "assets/images/house1/Bedroom.png",
                        ImageUrl3 = "assets/images/house1/Bathroom.png",
                    },
                    new Rental
                    {
                    Name = "Cozy cabin",
                    Address = "Holetråkket 4, Rena",
                    PricePrNight = 800,
                    RentalType = "Cabin",
                    BedNr = 1,
                    BathNr = 1,
                    Area = 15,
                    Description = "Cozy cabin in Skramstadsætra, close to Rena.",
                    ImageUrl = "assets/images/cabin1/Livingroom.png",
                    ImageUrl2 = "assets/images/cabin1/Bedroom.png",
                    ImageUrl3 = "assets/images/cabin1/Bathroom.png",

                },
                    new Rental
                    {
                        Name = "Cozy cabin",
                        Address = "Holetråkket 4, Rena",
                        PricePrNight = 800,
                        RentalType = "Cabin",
                        BedNr = 1,
                        BathNr = 1,
                        Area = 15,
                        Description = "Cozy cabin in Skramstadsætra, close to Rena.",
                        ImageUrl = "assets/images/cabin1/Livingroom.png",
                        ImageUrl2 = "assets/images/cabin1/Bedroom.png",
                        ImageUrl3 = "assets/images/cabin1/Bathroom.png",

                    },
                    new Rental
                    {
                        Name = "Cozy cabin",
                        Address = "Holetråkket 4, Rena",
                        PricePrNight = 800,
                        RentalType = "Cabin",
                        BedNr = 1,
                        BathNr = 1,
                        Area = 15,
                        Description = "Cozy cabin in Skramstadsætra, close to Rena.",
                        ImageUrl = "assets/images/cabin1/Livingroom.png",
                        ImageUrl2 = "assets/images/cabin1/Bedroom.png",
                        ImageUrl3 = "assets/images/cabin1/Bathroom.png",

                    },
                    new Rental
                    {
                        Name = "Cozy cabin",
                        Address = "Holetråkket 4, Rena",
                        PricePrNight = 800,
                        RentalType = "Cabin",
                        BedNr = 1,
                        BathNr = 1,
                        Area = 15,
                        Description = "Cozy cabin in Skramstadsætra, close to Rena.",
                        ImageUrl = "assets/images/cabin1/Livingroom.png",
                        ImageUrl2 = "assets/images/cabin1/Bedroom.png",
                        ImageUrl3 = "assets/images/cabin1/Bathroom.png",

                    },
                    new Rental
                    {
                        Name = "Cozy cabin",
                        Address = "Holetråkket 4, Rena",
                        PricePrNight = 800,
                        RentalType = "Cabin",
                        BedNr = 1,
                        BathNr = 1,
                        Area = 15,
                        Description = "Cozy cabin in Skramstadsætra, close to Rena.",
                        ImageUrl = "assets/images/cabin1/Livingroom.png",
                        ImageUrl2 = "assets/images/cabin1/Bedroom.png",
                        ImageUrl3 = "assets/images/cabin1/Bathroom.png",

                    },
                    new Rental
                    {
                        Name = "Cozy cabin",
                        Address = "Holetråkket 4, Rena",
                        PricePrNight = 800,
                        RentalType = "Cabin",
                        BedNr = 1,
                        BathNr = 1,
                        Area = 15,
                        Description = "Cozy cabin in Skramstadsætra, close to Rena.",
                        ImageUrl = "assets/images/cabin1/Livingroom.png",
                        ImageUrl2 = "assets/images/cabin1/Bedroom.png",
                        ImageUrl3 = "assets/images/cabin1/Bathroom.png",

                    },
                    new Rental
                    {
                        Name = "Cozy cabin",
                        Address = "Holetråkket 4, Rena",
                        PricePrNight = 800,
                        RentalType = "Cabin",
                        BedNr = 1,
                        BathNr = 1,
                        Area = 15,
                        Description = "Cozy cabin in Skramstadsætra, close to Rena.",
                        ImageUrl = "assets/images/cabin1/Livingroom.png",
                        ImageUrl2 = "assets/images/cabin1/Bedroom.png",
                        ImageUrl3 = "assets/images/cabin1/Bathroom.png",

                    },
                    new Rental
                    {
                        Name = "Cozy cabin",
                        Address = "Holetråkket 4, Rena",
                        PricePrNight = 800,
                        RentalType = "Cabin",
                        BedNr = 1,
                        BathNr = 1,
                        Area = 15,
                        Description = "Cozy cabin in Skramstadsætra, close to Rena.",
                        ImageUrl = "assets/images/cabin1/Livingroom.png",
                        ImageUrl2 = "assets/images/cabin1/Bedroom.png",
                        ImageUrl3 = "assets/images/cabin1/Bathroom.png",

                    },
                    new Rental
                    {
                        Name = "Cozy cabin",
                        Address = "Holetråkket 4, Rena",
                        PricePrNight = 800,
                        RentalType = "Cabin",
                        BedNr = 1,
                        BathNr = 1,
                        Area = 15,
                        Description = "Cozy cabin in Skramstadsætra, close to Rena.",
                        ImageUrl = "assets/images/cabin1/Livingroom.png",
                        ImageUrl2 = "assets/images/cabin1/Bedroom.png",
                        ImageUrl3 = "assets/images/cabin1/Bathroom.png",

                    },
                    new Rental
                    {
                        Name = "Cozy cabin",
                        Address = "Holetråkket 4, Rena",
                        PricePrNight = 800,
                        RentalType = "Cabin",
                        BedNr = 1,
                        BathNr = 1,
                        Area = 15,
                        Description = "Cozy cabin in Skramstadsætra, close to Rena.",
                        ImageUrl = "assets/images/cabin1/Livingroom.png",
                        ImageUrl2 = "assets/images/cabin1/Bedroom.png",
                        ImageUrl3 = "assets/images/cabin1/Bathroom.png",

                    }
                };
                context.AddRange(rentals);
                context.SaveChanges();

                if (!context.Orders.Any())
                {
                    var orders = new List<Order>
                {
                    new Order
                    {
                        OrderId = 1,
                        NightsNr = 3,
                        TotalPrice = 4500
                    },
                    new Order
                    {
                    OrderId = 2,
                    NightsNr = 2,
                    TotalPrice = 3000
                    }
                    };
                    context.AddRange(orders);
                    context.SaveChanges();
                }
            }
        }
    }
}