using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace zamara.Data
{
    internal class DbInitializer
    {
        private ModelBuilder builder;

        public DbInitializer(ModelBuilder builder)
        {
            this.builder = builder;
        }

        public void Seed()
        {

            builder.Entity<IdentityRole>().HasData(new List<IdentityRole>
                    {
                    new IdentityRole {
                        Id="0b3463a2-0c6e-4a29-86cd-352a972e127a",
                        Name = "Admin",
                        NormalizedName = "ADMIN"
                    },
                    new IdentityRole {
                        Id="839e2c44-75b2-47d1-a280-1b40fa101dbf",
                        Name = "Staff",
                        NormalizedName = "STAFF"
                    },
                    new IdentityRole {
                        Id="db7e5c48-b9b9-4225-89d9-9e48dfe12361",
                        Name = "User1",
                        NormalizedName = "USER1"
                    },
                    new IdentityRole {
                        Id="31123878-9202-4d18-b968-14941124892e",
                        Name = "User2",
                        NormalizedName = "USE2"
                    },
                    new IdentityRole {
                        Id="c5119adf-4a5a-41b0-886b-55b12994fceb",
                        Name = "User3",
                        NormalizedName = "USER3"
                    },
                    });

                 
        }
    }
}