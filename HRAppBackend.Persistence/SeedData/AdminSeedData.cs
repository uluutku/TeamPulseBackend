using HRAppBackend.Domain.Entities;
using HRAppBackend.Persistence.Context;
using Microsoft.AspNetCore.Identity;


namespace HRAppBackend.Persistence.SeedData
{
    public class AdminSeedData
    {

        public static async void SeedData(AppDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {

            // Veritabanında herhangi bir kullanıcı var mı kontrol et
            if (!context.Roles.Any(x => x.Name == "Admin"))
            {
                //sirket yoneticisi rolunu olustur Admin olarak
                IdentityRole adminRole = new() { Name = "Admin" };
                roleManager.CreateAsync(adminRole).Wait();
            }

            if (!context.Roles.Any(x => x.Name == "User"))
            {
                IdentityRole adminRole = new() { Name = "User" };
                roleManager.CreateAsync(adminRole).Wait();
            }

            if (!context.Roles.Any(x => x.Name == "SiteAdmin"))
            {
                IdentityRole adminRole = new() { Name = "SiteAdmin" };
                roleManager.CreateAsync(adminRole).Wait();
            }


            if (!context.Users.Any())
            {
                //Kullanıcı oluştur
                var user = new AppUser
                {
                    UserName = "admin@example.com",
                    Email = "admin@example.com",
                    PhoneNumber = "05456557583",
                    EmailConfirmed = false,
                    ConfirmCode = 123456, // ConfirmCode değerini dilediğiniz gibi değiştirin
                                          // Diğer gerekli özellikleri de burada set edebilirsiniz
                };

                // Kullanıcıyı veritabanına ekleyin
                await userManager.CreateAsync(user, "AAAaaa.123"); // Şifreyi güvenli bir şifre ile değiştirin
                AppUser createdUser = await userManager.FindByNameAsync("admin@example.com");
                IdentityRole role = await roleManager.FindByNameAsync("Admin");
                await userManager.AddToRoleAsync(createdUser, role.Name);


                var user2 = new AppUser
                {
                    UserName = "user@example.com",
                    Email = "user@example.com",
                    PhoneNumber = "05456557557",
                    EmailConfirmed = false,
                    ConfirmCode = 123456, // ConfirmCode değerini dilediğiniz gibi değiştirin
                                          // Diğer gerekli özellikleri de burada set edebilirsiniz
                };

                await userManager.CreateAsync(user2, "AAAaaa.123"); // Şifreyi güvenli bir şifre ile değiştirin
                AppUser createdUser2 = await userManager.FindByNameAsync("user@example.com");
                IdentityRole role2 = await roleManager.FindByNameAsync("User");
                await userManager.AddToRoleAsync(createdUser2, role2.Name);

                var user3 = new AppUser
                {
                    UserName = "admin@teampulse.com",
                    Email = "admin@teampulse.com",
                    PhoneNumber = "05456557587",
                    EmailConfirmed = false,
                    ConfirmCode = 123456, // ConfirmCode değerini dilediğiniz gibi değiştirin
                                          // Diğer gerekli özellikleri de burada set edebilirsiniz
                };

                // Kullanıcıyı veritabanına ekleyin
                await userManager.CreateAsync(user3, "AAAaaa.123"); // Şifreyi güvenli bir şifre ile değiştirin
                AppUser createdUser3 = await userManager.FindByNameAsync("admin@teampulse.com");
                IdentityRole role3 = await roleManager.FindByNameAsync("Admin");
                await userManager.AddToRoleAsync(createdUser3, role3.Name);

                 var user4 = new AppUser
                {
                    UserName = "admin@emirhanyazilim.com",
                    Email = "admin@emirhanyazilim.com",
                    PhoneNumber = "05456557552",
                    EmailConfirmed = false,
                    ConfirmCode = 123456, // ConfirmCode değerini dilediğiniz gibi değiştirin
                                          // Diğer gerekli özellikleri de burada set edebilirsiniz
                };

                // Kullanıcıyı veritabanına ekleyin
                await userManager.CreateAsync(user4, "AAAaaa.123"); // Şifreyi güvenli bir şifre ile değiştirin
                AppUser createdUser4 = await userManager.FindByNameAsync("admin@emirhanyazilim.com");
                IdentityRole role4 = await roleManager.FindByNameAsync("Admin");
                await userManager.AddToRoleAsync(createdUser4, role4.Name);


				var user5 = new AppUser
				{
					UserName = "siteadmin@teampulse.com",
					Email = "siteadmin@teampulse.com",
					PhoneNumber = "05434241881",
					EmailConfirmed = false,
					ConfirmCode = 123456, // ConfirmCode değerini dilediğiniz gibi değiştirin
										  // Diğer gerekli özellikleri de burada set edebilirsiniz
				};

				// Kullanıcıyı veritabanına ekleyin
				await userManager.CreateAsync(user5, "AAAaaa.123"); // Şifreyi güvenli bir şifre ile değiştirin
				AppUser createdUser5 = await userManager.FindByNameAsync("siteadmin@teampulse.com");
				IdentityRole role5 = await roleManager.FindByNameAsync("SiteAdmin");
				await userManager.AddToRoleAsync(createdUser5, role5.Name);
			}



            if (!context.Professions.Any())

            {
                context.Professions.AddRange(
                    new Profession { Name = "Müdür" },

                    new Profession { Name = "Mühendis" },

                    new Profession { Name = "Yazılımcı" }

                );

                context.SaveChanges();
            }

            // Departmanları ekle
            if (!context.Departments.Any())
            {

                context.Departments.AddRange(

                    new Department { Name = "IT" },

                    new Department { Name = "IK" },

                    new Department { Name = "İşletme" }

                );


                context.SaveChanges();

            }


            if (!context.Companies.Any())
            {
                Company company = new Company
                {
                    CompanyName = "Team Pulse Anonim Şirketi",
                    CompanyDescription = "Birbirinden cevval 6 kişinin kurduğu bu kuruluş fenadır!",
                    CompanyAddress = "ANKARA",
                    CompanyPhone = "05434241930",
                    LogoName = "TeamPulseAŞ",
                    LogoPath = "https://i.ibb.co/G9mmRQd/1a08f6f0-c084-4b5c-b05f-94b2dfe404be-removebg-preview-copy.png",
                    SubscriptionStart = new DateTime(2021, 4, 5, 5, 4, 3),
                    SubscriptionEnd = new DateTime(2027, 4, 5, 5, 4, 3),

                };


                Company company1 = new Company
                {                    
                    CompanyName = "Humanity Empower HR Solutions",
                    CompanyDescription = "Humanity Empower HR Solutions is a forward-thinking human resources company dedicated to fostering productive work environments and maximizing human potential.",
                    CompanyAddress = "İSTANBUL",
                    CompanyPhone = "05456217583",
                    LogoName = "HRApp",
                    LogoPath = "https://ushrcompany.com/wp-content/uploads/2020/03/website_logo_transparent_background.png",
                    SubscriptionStart = new DateTime(2021, 4, 5, 5, 4, 3),
                    SubscriptionEnd = new DateTime(2027, 4, 5, 5, 4, 3),
                    
                };


                Company company2 = new Company
                {
                    CompanyName = "Emirhan Yazilim Programlama Hizmetleri AS.",
                    CompanyDescription = "Emirhan AS 2025 yılında kurulmuştur.",
                    CompanyAddress = "KOCAELİ",
                    CompanyPhone = "05523321155",
                    LogoName = "EmirhanAS",
                    LogoPath = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/34/EY_logo_2019.svg/768px-EY_logo_2019.svg.png",
                    SubscriptionStart = new DateTime(2021, 4, 5, 5, 4, 3),
                    SubscriptionEnd = new DateTime(2027, 4, 5, 5, 4, 3),
                    
                };


                context.Companies.Add(company);
                context.Companies.Add(company1);
                context.Companies.Add(company2);
                context.SaveChanges();
            }


            if (!context.Employees.Any())
            {
                AppUser createdUser = await userManager.FindByNameAsync("admin@example.com");
                // Employee örneği oluşturun
                var employee = new Employee
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Address = "Oyle boyle sokak yirmi karakter",
                    City = "İSTANBUL",
                    County = "KARTAL",
                    BirthDate = new DateTime(1990, 4, 5, 5, 4, 3),
                    PlaceOfBirth = "Bursa",
                    FileName = "string",
                    FilePath = "https://bebereviews.com/wp-content/uploads/2016/02/tumblr_nmrsc4Xx111sm9iflo1_1280.jpg",
                    StartDate = DateTime.Now,
                    ProfessionId = 1,
                    DepartmentId = 1,
                    TRIdentityNumber = "15994981",
                    Salary = 15.470M,
                    Status = Domain.Enums.Status.Active,
                    AppUserId = (await userManager.FindByNameAsync("admin@example.com")).Id,
                    CompanyId = 2,

                    // Diğer gerekli özellikleri de burada set edebilirsiniz
                };

                createdUser.Employee = employee;
                employee.AppUser = createdUser;

                // Oluşturulan örnekleri veritabanına ekleyin
                await context.Employees.AddAsync(employee);
                // Veritabanını güncelle
                await context.SaveChangesAsync();


                AppUser createdUser2 = await userManager.FindByNameAsync("user@example.com");
                // Employee örneği oluşturun
                var employee2 = new Employee
                {
                    FirstName = "Michael",
                    LastName = "Doe",
                    Address = "Soyle boyle sokak yirmi karakter",
                    City = "İSTANBUL",
                    County = "KARTAL",
                    BirthDate = new DateTime(1990, 4, 5, 5, 4, 3),
                    PlaceOfBirth = "Kars",
                    FileName = "string",
                    FilePath = "http://pbs.twimg.com/profile_images/1357933786332344321/vqW7SvQj.jpg",
                    StartDate = DateTime.Now,
                    ProfessionId = 2,
                    DepartmentId = 1,
                    TRIdentityNumber = "859949815",
                    Salary = 15.470M,
                    Status = Domain.Enums.Status.Active,
                    AppUserId = (await userManager.FindByNameAsync("user@example.com")).Id,
                    CompanyId = 2,

                    // Diğer gerekli özellikleri de burada set edebilirsiniz
                };

                createdUser2.Employee = employee2;
                employee2.AppUser = createdUser2;

                // Oluşturulan örnekleri veritabanına ekleyin
                await context.Employees.AddAsync(employee2);
                // Veritabanını güncelle
                await context.SaveChangesAsync();


                AppUser createdUser3 = await userManager.FindByNameAsync("admin@emirhanyazilim.com");
                // Employee örneği oluşturun
                var employee3 = new Employee
                {
                    FirstName = "Emirhan",
                    LastName = "Sen",
                    Address = "Gulbahar Bahce Sokak Kocaeli No:65",
                    City = "KOCAELİ",
                    County = "İZMİT",
                    BirthDate = new DateTime(1998, 4, 5, 5, 4, 3),
                    PlaceOfBirth = "KOCAELİ",
                    FileName = "string",
                    FilePath = "https://d1tlrxy0mfxnyo.cloudfront.net/avatar/497343/d2c5ebdf-766d-37a8-87bd-a0090c14ec68.jpg",
                    StartDate = DateTime.Now,
                    ProfessionId = 2,
                    DepartmentId = 1,
                    TRIdentityNumber = "98765435444",
                    Salary = 75.470M,
                    Status = Domain.Enums.Status.Active,
                    AppUserId = (await userManager.FindByNameAsync("admin@emirhanyazilim.com")).Id,
                    CompanyId = 3,

                    // Diğer gerekli özellikleri de burada set edebilirsiniz
                };

                createdUser3.Employee = employee3;
                employee3.AppUser = createdUser3;

                // Oluşturulan örnekleri veritabanına ekleyin
                await context.Employees.AddAsync(employee3);
                // Veritabanını güncelle
                await context.SaveChangesAsync();



				AppUser createdUser4 = await userManager.FindByNameAsync("siteadmin@teampulse.com");
				// Employee örneği oluşturun
				var employee4 = new Employee
				{
					FirstName = "Mert",
					LastName = "Batmaz",
					Address = "Gulbahar Bahce Sokak Adana Ceyhan No:65",
					City = "ADANA",
					County = "CEYHAN",
					BirthDate = new DateTime(1998, 4, 5, 5, 4, 3),
					PlaceOfBirth = "ADANA",
					FileName = "string",
					FilePath = "https://pbs.twimg.com/media/DBrmERTUMAAmlQi.jpg",
					StartDate = DateTime.Now,
					ProfessionId = 1,
					DepartmentId = 1,
					TRIdentityNumber = "12345678910",
					Salary = 75.470M,
					Status = Domain.Enums.Status.Active,
					AppUserId = (await userManager.FindByNameAsync("siteadmin@teampulse.com")).Id,
					CompanyId = 1,

					// Diğer gerekli özellikleri de burada set edebilirsiniz
				};

				createdUser4.Employee = employee4;
				employee4.AppUser = createdUser4;

				// Oluşturulan örnekleri veritabanına ekleyin
				await context.Employees.AddAsync(employee4);
				// Veritabanını güncelle
				await context.SaveChangesAsync();


				AppUser createdUser5 = await userManager.FindByNameAsync("admin@teampulse.com");
				// Employee örneği oluşturun
				var employee5 = new Employee
				{
					FirstName = "Utku",
					LastName = "Ulu",
					Address = "Gulbahar Bahce Sokak İstanbul Beyoğlu No:65",
					City = "İSTANBUL",
					County = "BEYOĞLU",
					BirthDate = new DateTime(1998, 4, 5, 5, 4, 3),
					PlaceOfBirth = "İSTANBUL",
					FileName = "string",
					FilePath = "https://i.pinimg.com/originals/e5/18/eb/e518eb72f8b9b0726fe97cc798876f1e.jpg",
					StartDate = DateTime.Now,
					ProfessionId = 1,
					DepartmentId = 1,
					TRIdentityNumber = "12345678911",
					Salary = 75.470M,
					Status = Domain.Enums.Status.Active,
					AppUserId = (await userManager.FindByNameAsync("admin@teampulse.com")).Id,
					CompanyId = 1,

					// Diğer gerekli özellikleri de burada set edebilirsiniz
				};

				createdUser5.Employee = employee5;
				employee5.AppUser = createdUser5;

				// Oluşturulan örnekleri veritabanına ekleyin
				await context.Employees.AddAsync(employee5);
				// Veritabanını güncelle
				await context.SaveChangesAsync();
			}
        }
    }
}
