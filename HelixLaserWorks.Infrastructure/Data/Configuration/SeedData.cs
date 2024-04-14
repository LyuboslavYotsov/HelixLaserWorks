using HelixLaserWorks.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace HelixLaserWorks.Infrastructure.Data.Configuration
{
    public class SeedData
    {
        public SeedData()
        {
            CreateThicknesses();
            CreateMaterialTypes();
            CreateMaterials();
            CreateUsers();
            CreateAdminRole();
            CreateAdminUserRole();
        }
        //USERS
        public IdentityUser CustomerUser { get; set; } = null!;

        public IdentityUser AdminUser { get; set; } = null!;

        //ROLE
        public IdentityRole AdminRole { get; set; } = null!;

        public IdentityUserRole<string> AdminUserRole { get; set; } = null!;

        //MATERIALS
        public Material MildSteel { get; set; } = null!;

        public Material StainlessSteel { get; set; } = null!;

        public Material Aluminum { get; set; } = null!;

        public Material Copper { get; set; } = null!;

        public Material ChipWood { get; set; } = null!;

        //MATERIAL TYPES
        public MaterialType MetalType { get; set; } = null!;

        public MaterialType WoodType { get; set; } = null!;

        public MaterialType PlasticType { get; set; } = null!;

        //THICKNESSES
        public List<Thickness> Thicknesses { get; set; } = new List<Thickness>();

        //MATERIAL THICKNESSES
        public List<MaterialThickness> MildSteelThichnesses { get; set; } = new List<MaterialThickness>();

        public List<MaterialThickness> StainlessSteelThicknesses { get; set; } = new List<MaterialThickness>();

        public List<MaterialThickness> AluminumThicknesses { get; set; } = new List<MaterialThickness>();

        public List<MaterialThickness> CopperThicknesses { get; set; } = new List<MaterialThickness>();

        public List<MaterialThickness> ChipwoodThicknesses { get; set; } = new List<MaterialThickness>();

        private void CreateMaterialTypes()
        {
            MetalType = new MaterialType()
            {
                Id = 1,
                Name = "Metal"
            };

            PlasticType = new MaterialType()
            {
                Id = 2,
                Name = "Plastic"
            };

            WoodType = new MaterialType()
            {
                Id = 3,
                Name = "Wood"
            };
        }

        private void CreateMaterials()
        {
            MildSteel = new Material()
            {
                Id = 1,
                MaterialTypeId = MetalType.Id,
                Name = "MildSteel37",
                Description = "Mild steel is a type of low-carbon steel that posesses a relatively low level of carbon content. This makes it malleable, ductile, and easy to weld. Our mild steel is the go-to choice for fabricators, welders, machinists, prototypers, and anyone else who needs a strong, reliable part that is easy to work with.",
                Density = 7.8,
                CorrosionResistance = false,
                ImageUrl = "https://i.ebayimg.com/images/g/-yEAAOSwMvpb0FqE/s-l1600.jpg",
                Specification = "Cold Rolled",
                PricePerSquareMeter = 10.99m,
                IsAvailable = true
            };

            StainlessSteel = new Material()
            {
                Id = 2,
                MaterialTypeId = MetalType.Id,
                Name = "StainlessSteel304",
                Description = "Weldable, formable, and easy to work with, 304 stainless is our first choice for projects that require massive strength and durability. Laser cut 304 stainless steel is oxidation resistant, making it easy to sanitize and maintain. This particular feature makes 304 stainless steel the go-to grade for many food service applications, from countertops to cookware.",
                Density = 7.9,
                CorrosionResistance = true,
                ImageUrl = "https://www.smetals.co.uk/wp-content/uploads/2023/04/Stainless-Steel-304-Grade-0.5mm-Panels-Image-2.jpg",
                PricePerSquareMeter = 29.00m,
                IsAvailable = true
            };

            Aluminum = new Material()
            {
                Id = 3,
                MaterialTypeId = MetalType.Id,
                Name = "AluminumH32",
                Description = "5052 H32 aluminum is strong, inexpensive, and lightweight. Whether you’re welding, machining, or bending, 5052 aluminum is going to be the go-to material for those projects that need excellent all-around material properties. Our laser cut aluminum is exceptionally lightweight and strong, making it perfect for projects where overall load is a concern.",
                Density = 2.68,
                CorrosionResistance = true,
                ImageUrl = "https://res.cloudinary.com/rsc/image/upload/bo_1.5px_solid_white,b_auto,c_pad,dpr_2,f_auto,h_399,q_auto,w_710/c_pad,h_399,w_710/F0434043-01?pgw=1",
                PricePerSquareMeter = 32.00m,
                IsAvailable = true
            };

            Copper = new Material()
            {
                Id = 4,
                MaterialTypeId = MetalType.Id,
                Name = "Copper",
                Description = "Our C110 half-hard copper is classified as electrolytic copper, which basically means it’s an extremely high purity (greater than 99% copper, ours is 99.9%). For your projects, this means that the material’s electrical properties won’t be hampered by any erroneous leftover elements. You’re getting one of the purest grades available.",
                Density = 8.96,
                CorrosionResistance = true,
                ImageUrl = "https://www.artisansupplies.com.au/wp-content/uploads/2015/11/copper.jpg",
                PricePerSquareMeter = 42.00m,
                IsAvailable = true
            };

            ChipWood = new Material()
            {
                Id= 5,
                MaterialTypeId = WoodType.Id,
                Name = "ChipWood",
                Description = "Chipboard, also known as particleboard, is an engineered wood product made from wood chips, shavings, and resin that are compressed and bonded together under heat and pressure. It’s commonly used in furniture, shelving, and construction applications as an affordable alternative to solid wood, offering good stability but with a slightly coarser texture. One of laser cut Chipboard’s greatest assets is that it’s completely green. Made entirely from recycled pasteboard, our chipboard is 100% recyclable.",
                Density = 0.63,
                CorrosionResistance = true,
                ImageUrl = "https://media.wickes.co.uk/is/image/wickes/normal/Chipboard-Flooring-Wickes-P5-T-G-Chipboard-Flooring-18mm-x-600mm-x-2-4m~N0705_164516_00?$ratio43$&fit=crop&extend=-50,-250,-50,0",
                PricePerSquareMeter = 6.00m,
                IsAvailable = true
            };

            foreach (var thickness in Thicknesses)
            {
                MildSteelThichnesses.Add(new MaterialThickness()
                {
                    MaterialId = MildSteel.Id,
                    ThicknessId = thickness.Id
                });

                ChipwoodThicknesses.Add(new MaterialThickness()
                {
                    MaterialId = ChipWood.Id,
                    ThicknessId = thickness.Id
                });

                if (thickness.Value < 12)
                {
                    StainlessSteelThicknesses.Add(new MaterialThickness()
                    {
                        MaterialId = StainlessSteel.Id,
                        ThicknessId = thickness.Id
                    });
                }

                if (thickness.Value < 5)
                {
                    AluminumThicknesses.Add(new MaterialThickness()
                    {
                        MaterialId = Aluminum.Id,
                        ThicknessId = thickness.Id
                    });

                    CopperThicknesses.Add(new MaterialThickness()
                    {
                        MaterialId = Copper.Id,
                        ThicknessId = thickness.Id
                    });
                }
            }

        }

        private void CreateThicknesses()
        {
            Thicknesses = new List<Thickness>()
            {
                    new Thickness()
                    {
                        Id = 1,
                        Value = 1
                    },
                    new Thickness()
                    {
                        Id = 2,
                        Value = 2
                    },
                    new Thickness()
                    {
                        Id = 3,
                        Value = 3
                    },
                    new Thickness()
                    {
                        Id = 4,
                        Value = 4
                    },
                    new Thickness()
                    {
                        Id = 5,
                        Value = 5
                    },
                    new Thickness()
                    {
                        Id = 6,
                        Value = 6
                    },
                    new Thickness()
                    {
                        Id = 7,
                        Value = 8
                    },
                    new Thickness()
                    {
                        Id = 8,
                        Value = 10
                    },
                    new Thickness()
                    {
                        Id = 9,
                        Value = 12
                    },
                    new Thickness()
                    {
                        Id = 10,
                        Value = 15
                    },
                    new Thickness()
                    {
                        Id = 11,
                        Value = 20
                    }
            };
        }

        private void CreateUsers()
        {
            var hasher = new PasswordHasher<IdentityUser>();

            CustomerUser = new IdentityUser()
            {
                Id = "2c2e7178-6349-4801-88fb-426de93ab2c7",
                UserName = "customer@mail.com",
                NormalizedUserName = "CUSTOMER@MAIL.COM",
                Email = "customer@mail.com",
                NormalizedEmail = "CUSTOMER@MAIL.COM",
            };

            CustomerUser.PasswordHash = hasher.HashPassword(CustomerUser, "customer123");

            AdminUser = new IdentityUser()
            {
                Id = "534e5524-cfeb-4561-b7fb-db4ded672702",
                UserName = "admin@mail.com",
                NormalizedUserName = "ADMIN@MAIL.COM",
                Email = "admin@mail.com",
                NormalizedEmail = "ADMIN@MAIL.COM"
            };

            AdminUser.PasswordHash = hasher.HashPassword(AdminUser, "admin123");
        }

        private void CreateAdminRole()
        {
            AdminRole = new IdentityRole()
            {
                Id = "cb7a13fd-35ff-4924-a06c-52edcb74e608",
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR",
            };
        }

        private void CreateAdminUserRole()
        {
            AdminUserRole = new IdentityUserRole<string>()
            {
                RoleId = "cb7a13fd-35ff-4924-a06c-52edcb74e608",
                UserId = "534e5524-cfeb-4561-b7fb-db4ded672702"
            };
        }
    }
}
