using System.Runtime.CompilerServices;
using Castle.Core.Resource;
using HeartHousing.Models;
using Microsoft.EntityFrameworkCore;

namespace HeartHousing.DAL
{
    public class DBInit
    {
        public static void Seed(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            RentalDbContext context = serviceScope.ServiceProvider.GetRequiredService<RentalDbContext>();
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
                        PricePrNigth = 1500,
                        RentalType = "Appartment",
                        BedNr = 1,
                        BathNr = 1,
                        Area = 20,
                        Description = "Modern appartment located centrally in Oslo.",
                        ImgUrl = "/images/appartment1/Livingroom.png",
                        ImgUrl2 = "/images/appartment1/Bedroom.png",
                        ImgUrl3 = "/images/appartment1/Bathroom.png",
                        UserID = "7261db5f-897f-4fc1-9cea-4e7051fd71df"

                    },
                    new Rental
                    {
                        Name = "Cozy cabin",
                        Address = "Holetråkket 4, Rena",
                        PricePrNigth = 800,
                        RentalType = "Cabin",
                        BedNr = 1,
                        BathNr = 1,
                        Area = 15,
                        Description = "Cozy cabin in Skramstadsætra, close to Rena.",
                        ImgUrl = "/images/cabin1/Livingroom.png",
                        ImgUrl2 = "/images/cabin1/Bedroom.png",
                        ImgUrl3 = "/images/cabin1/Bathroom.png",
                        UserID = "7261db5f-897f-4fc1-9cea-4e7051fd71df"

                    },
                    new Rental
                    {
                        Name = "Renovated house",
                        Address = "Benterudbakken 7, Hamar",
                        PricePrNigth = 3000,
                        RentalType = "House",
                        BedNr = 2,
                        BathNr = 2,
                        Area = 50,
                        Description = "Newly renovated house in Hamar, 15 minutes away from the city centre.",
                        ImgUrl = "/images/house1/Livingroom.png",
                        ImgUrl2 = "/images/house1/Bedroom.png",
                        ImgUrl3 = "/images/house1/Bathroom.png",
                        UserID = "7261db5f-897f-4fc1-9cea-4e7051fd71df"
                    }
                };
                context.AddRange(rentals);
                context.SaveChanges();
            }
        }
    }
}