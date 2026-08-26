using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rad.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;


namespace Rad.DAL
{
    public class GuestManagerDbContext : IdentityDbContext<AppUser>
    {
        public GuestManagerDbContext(DbContextOptions<GuestManagerDbContext> options)
        : base(options)
        {
        }
        public DbSet<Accomodation> Accomodations { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<Photo> Photo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Accomodation>()
            .Property(a => a.PricePerNight)
            .HasPrecision(10, 2);

            modelBuilder.Entity<Accomodation>().HasData(
                new Accomodation
                {
                    ID = 1,
                    Name = "Kuća Marijan",
                    Capacity = 2,
                    Size = 30,
                    ImageUrl = "/images/sofija.jpg",
                    PricePerNight = 50.00m,
                    Description = "Kuća Marijan je prostor koji može primiti dvoje ljudi, a nalazi se nedaleko od maslenika i voćnjaka pa je najbolje vrijeme za posjet ljeto i rana jesen." +
                    "Unutrašnjost je inspirirana kućama iz prošlosti koje su prije nekoliko godina na našim prostorima bile bijeg od gradske vreve." +
                    "Osim krova nad glavom, ispred same kuće postoji terasa na kojima se provode tople ljetne večeri uživajući u tišini i ugodnim temperaturama.",                                 
                    PoolDistance = 200,
                    PoolImg = "/images/braco_bazen.png"
                },
                new Accomodation
                {
                    ID = 2,
                    Name = "Kuća Draga",
                    Capacity = 5,
                    Size = 50,
                    ImageUrl = "/images/Janko.jpg",
                    PricePerNight = 90.00m,
                    Description = "Apartman Sofija je moderan i udoban apartman smješten u srcu grada. Sadrži sve potrebne sadržaje za ugodan boravak, uključujući potpuno opremljenu kuhinju, prostranu dnevnu sobu i udobnu spavaću sobu. Idealno za parove ili male obitelji.",
                    PoolDistance = 150,
                    PoolImg = "/images/draga_bazen.png"
                },
                new Accomodation
                {
                    ID = 3,
                    Name = "Kuća Braco",
                    Capacity = 2,
                    Size = 50,
                    ImageUrl = "/images/braco1.jpg",
                    PricePerNight = 70.00m,
                    Description = "Apartman Sofija je moderan i udoban apartman smješten u srcu grada. Sadrži sve potrebne sadržaje za ugodan boravak, uključujući potpuno opremljenu kuhinju, prostranu dnevnu sobu i udobnu spavaću sobu. Idealno za parove ili male obitelji.",                          
                    PoolDistance = 200,
                    PoolImg = "/images/braco_bazen.png"
                },
                new Accomodation
                {
                    ID = 4,
                    Name = "Kuća Laura",
                    Capacity = 4,
                    Size = 56,
                    ImageUrl = "/images/laura.jpg",
                    PricePerNight = 70.00m,
                    Description = "Apartman Sofija je moderan i udoban apartman smješten u srcu grada. Sadrži sve potrebne sadržaje za ugodan boravak, uključujući potpuno opremljenu kuhinju, prostranu dnevnu sobu i udobnu spavaću sobu. Idealno za parove ili male obitelji.",
                    PoolDistance = 400,
                    PoolImg = "/images/laura_bazen.png"
                },
                new Accomodation
                {
                    ID = 5,
                    Name = "Kuća Janko",
                    Capacity = 4,
                    Size = 60,
                    ImageUrl = "/images/more.jpeg",
                    PricePerNight = 90.00m,
                    Description = "Apartman Sofija je moderan i udoban apartman smješten u srcu grada. Sadrži sve potrebne sadržaje za ugodan boravak, uključujući potpuno opremljenu kuhinju, prostranu dnevnu sobu i udobnu spavaću sobu. Idealno za parove ili male obitelji.",
                    PoolDistance = 200,
                    PoolImg = "/images/laura_bazen.png"
                }
             
            );

            modelBuilder.Entity<Photo>().HasData(
                new Photo
                {
                    ID = 1,
                    AccomodationID = 1,
                    ImageUrl = "/images/marijan1.jpeg"
                },
                new Photo
                {
                    ID = 2,
                    AccomodationID = 1,
                    ImageUrl = "/images/marijan2.jpeg"
                },
                new Photo
                {
                    ID = 3,
                    AccomodationID = 1,
                    ImageUrl = "/images/marijan3.jpeg"
                },
                new Photo
                {
                    ID = 4,
                    AccomodationID = 2,
                    ImageUrl = "/images/draga1.jpeg"
                },
                new Photo
                {
                    ID = 5,
                    AccomodationID = 2,
                    ImageUrl = "/images/draga2.jpeg"
                },
                new Photo
                {
                    ID = 6,
                    AccomodationID = 2,
                    ImageUrl = "/images/draga3.jpeg"
                },
                new Photo
                {
                    ID = 7,
                    AccomodationID = 2,
                    ImageUrl = "/images/draga4.jpeg"
                },
                new Photo
                {
                    ID = 8,
                    AccomodationID = 3,
                    ImageUrl = "/images/braco1.jpeg"
                },
                new Photo
                {
                    ID = 9,
                    AccomodationID = 3,
                    ImageUrl = "/images/braco2.jpeg"
                },
                new Photo
                {
                    ID = 10,
                    AccomodationID = 3,
                    ImageUrl = "/images/braco3.jpeg"
                },
                new Photo
                {
                    ID = 11,
                    AccomodationID = 4,
                    ImageUrl = "/images/laura1.jpeg"
                },
                new Photo
                {
                    ID = 12,
                    AccomodationID = 4,
                    ImageUrl = "/images/laura2.jpeg"
                },
                new Photo
                {
                    ID = 13,
                    AccomodationID = 4,
                    ImageUrl = "/images/laura3.jpeg"
                },
                new Photo
                {
                    ID = 14,
                    AccomodationID = 4,
                    ImageUrl = "/images/laura4.jpeg"
                },
                new Photo
                {
                    ID = 15,
                    AccomodationID = 4,
                    ImageUrl = "/images/laura5.jpeg"
                },
                new Photo
                {
                    ID = 16,
                    AccomodationID = 4,
                    ImageUrl = "/images/laura6.jpeg"
                },
                new Photo
                {
                    ID = 17,
                    AccomodationID = 5,
                    ImageUrl = "/images/janko1.jpeg"
                },
                new Photo
                {
                    ID = 18,
                    AccomodationID = 5,
                    ImageUrl = "/images/janko2.jpeg"
                },
                new Photo
                {
                    ID = 19,
                    AccomodationID = 5,
                    ImageUrl = "/images/janko3.jpeg"
                },
                new Photo
                {
                    ID = 20,
                    AccomodationID = 5,
                    ImageUrl = "/images/janko4.jpeg"
                }
            );

        }

    }
}
