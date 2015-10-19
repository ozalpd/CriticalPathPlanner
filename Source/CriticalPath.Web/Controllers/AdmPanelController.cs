using CriticalPath.Data;
using CriticalPath.Web.Models;
using OzzIdentity.Controllers;
using OzzUtils;
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Web.Mvc;
using System.Threading.Tasks;
using OzzIdentity;
using OzzIdentity.Models;

namespace CriticalPath.Web.Controllers
{
    public class AdmPanelController : AbstractAdminController
    {
        [Authorize]
        public override ActionResult Index()
        {
            var sb = new StringBuilder();
            sb.Append("<h4>Test Panel</h4>");

            var idContext = new OzzIdentityDbContext();
            var users = idContext.Users;

            foreach (var user in users)
            {
                sb.Append(user.Id);
                sb.Append(" ");
                sb.Append(user.UserName);
                sb.Append(" ");
                sb.Append(user.FirstName);
                sb.Append(" ");
                sb.Append(user.LastName);
                sb.Append("<br>");
            }
            return Content(sb.ToString());
        }

        protected override string AdminRole
        {
            get { return SecurityRoles.Admin; }
        }

        //will be used in SeedRoles action
        protected override string[] GetApplicationRoles()
        {
            return new string[] {
                SecurityRoles.Admin,
                SecurityRoles.Supervisor,
                SecurityRoles.Clerk,
                SecurityRoles.Observer
            };
        }

        [Authorize]
        public async Task<ActionResult> SeedData()
        {
            var sb = new StringBuilder();

            await SeedProducts(sb);
            await DataContext.SaveChangesAsync(this);
            await SeedContacts(sb);
            await DataContext.SaveChangesAsync(this);
            //TODO: Seed ProcessTemplates

            return Content(sb.ToString());
        }

        private async Task SeedContacts(StringBuilder sb)
        {
            var query = await DataContext.Contacts.CountAsync();
            if (query > 0)
            {
                sb.Append("Database has customer records already!<br>");
                return;
            }

            Customer[] customers = {new Customer {
                CompanyName = "Nurplex",
                City = "Norfolk",
                CustomerCode = "Nurplex",
                Address1 = "389 Cleveland Street",
                Notes = "In aliqua fugiat magna adipisicing magna cillum enim exercitation ullamco cillum ullamco.",
                Phone1 = "(944) 444-2599",
                Phone2 = "(877) 466-3224"
            }, new Customer
            {
                 CompanyName = "Wazzu",
                CustomerCode = "1234",
                 City = "Homestead",
                 Address1 = "118 Applegate Court",
                 Phone1 = "+1 (888) 435-3835",
                 Phone2 = "+1 (898) 597-3808",
                 Notes = "Laborum aliquip pariatur aliqua dolor voluptate cillum proident officia in.",
            }, new Customer {
                     CompanyName = "Qiao Corp.",
                CustomerCode = "Qiao",
                     City = "Hartsville/Hartley",
                     Address1 = "253 Reeve Place",
                     Phone1 = "+1 (880) 473-3081",
                     Phone2 = "+1 (972) 568-2580",
                     Notes = "Mollit Lorem cupidatat voluptate occaecat sunt sunt eu in dolore ad eiusmod.",
            }, new Customer {
                     CompanyName = "Solaren",
                CustomerCode = "555",
                     City = "Olney",
                     Address1 = "694 Ashford Street",
                     Phone1 = "+1 (983) 445-2431",
                     Phone2 = "+1 (931) 552-3165",
                     Notes = "Ea ea amet consectetur excepteur velit reprehenderit tempor dolor eiusmod laborum voluptate aute.",
            }, new Customer {
                     CompanyName = "Cowtown Incorporated",
                CustomerCode = "Cowtown",
                     City = "Edgewater",
                     Address1 = "397 Vandam Street",
                     Phone1 = "+1 (999) 498-3117",
                     Phone2 = "+1 (876) 457-3103",
                     Notes = "Do ex magna mollit excepteur eu fugiat sit minim ullamco est est.",
            }, new Customer {
                     CompanyName = "Genesynk Associates",
                CustomerCode = "Genesynk",
                     City = "Tedrow",
                     Address1 = "415 Newport Street",
                     Phone1 = "+1 (984) 410-2831",
                     Phone2 = "+1 (873) 545-2215",
                     Notes = "Ullamco reprehenderit esse id qui voluptate ipsum veniam.",
            }, new Customer {
                     CompanyName = "Zilch Industries",
                CustomerCode = "Zilch",
                     City = "Katonah",
                     Address1 = "594 Eckford Street",
                     Phone1 = "+1 (835) 416-2323",
                     Phone2 = "+1 (845) 440-2230",
                     Notes = "Mollit minim cillum ex reprehenderit nisi proident nulla.",
            } };

            int i = Randomizer.RandomNr(0, customers.Length - 1);
            customers[i].Contacts.Add(new Contact
            {
                FirstName = "Tucker",
                LastName = "Gentry",
                EmailHome = "tucker.gentry@undefined.tv"
            });
            i = Randomizer.RandomNr(0, customers.Length - 1);
            customers[i].Contacts.Add(new Contact
            {
                FirstName = "Thelma",
                LastName = "Mcdaniel",
                EmailWork = "thelma.mcdaniel@undefined.me"
            });
            i = Randomizer.RandomNr(0, customers.Length - 1);
            customers[i].Contacts.Add(new Contact
            {
                FirstName = "Michael",
                LastName = "Bradshaw",
                EmailHome = "michael.bradshaw@undefined.info"
            });
            i = Randomizer.RandomNr(0, customers.Length - 1);
            customers[i].Contacts.Add(new Contact
            {
                FirstName = "Jannie",
                LastName = "Murray",
                EmailWork = "jannie.murray@undefined.org"
            });
            i = Randomizer.RandomNr(0, customers.Length - 1);
            customers[i].Contacts.Add(new Contact
            {
                FirstName = "Weber",
                LastName = "Gross",
                EmailWork = "weber.gross@undefined.biz"
            });
            i = Randomizer.RandomNr(0, customers.Length - 1);
            customers[i].Contacts.Add(new Contact
            {
                FirstName = "Myra",
                LastName = "Aguilar",
                EmailHome = "myra.aguilar@undefined.tv"
            });

            i = Randomizer.RandomNr(0, customers.Length - 1);
            customers[i].Contacts.Add(new Contact
            {
                FirstName = "Bray",
                LastName = "Spencer",
                EmailWork = "bray.spencer@undefined.ca"
            });
            i = Randomizer.RandomNr(0, customers.Length - 1);
            customers[i].Contacts.Add(new Contact
            {
                FirstName = "Ruiz",
                LastName = "Moody",
                EmailHome = "ruiz.moody@undefined.biz"
            });
            i = Randomizer.RandomNr(0, customers.Length - 1);
            customers[i].Contacts.Add(new Contact
            {
                FirstName = "Bridgette",
                LastName = "Murray",
                EmailWork = "bridgette.murray@undefined.tv"
            });
            foreach (var item in customers)
            {
                DataContext.Companies.Add(item);
                sb.Append("Customer: ");
                sb.Append(item.CompanyName);
                sb.Append(" added<br>");
            }

        }

        private async Task SeedProducts(StringBuilder sb)
        {
            var query = await DataContext.ProductCategories.CountAsync();
            if (query > 0)
            {
                sb.Append("Database has Product records already!<br>");
                return;
            }

            string[] catgKadin = {
                "Etek",
                "Şort",
                "Bluz / Tshirt",
                "Elbise",
                //"Pantolon / Tayt",
                //"Hırka / Kazak",
                //"Ceket / Kaban",
                //"Büyük Beden",
                //"Sweatshirt",
                //"Eşofman",
                //"Tulum"
            };
            string[] catgErkek = {
                "Şort",
                "T-Shirt",
                "Pantolon",
                //"Denim Pantolon",
                "Gömlek",
                "Takım Elbise",
                //"Mayo Şort",
                //"Sweatshirt",
                //"Hırka / Kazak",
                //"Ceket / Kaban",
                //"Yelek",
                //"Eşofman"
            };
            string[] catgCocuk = {
                "Çocuk T-Shirt",
                //"Çocuk Body",
                "Çocuk Elbise",
                "Çocuk Etek",
                "Çocuk Pantolon",
                "Çocuk Şort",
                "Çocuk Gömlek",
                "Çocuk Mont",
                //"Çocuk Hırka",
                //"Çocuk Kazak",
                //"Çocuk Eşofman",
                //"Çocuk Tulum",
                //"Çocuk Hastane Çıkışı",
                //"Çocuk Tayt",
                //"Çocuk Deniz Şortu"
            };

            int countCatg = 0;
            ProductCategory catg1 = new ProductCategory() { Title = "Kadın Giyim" };
            countCatg = AddCategory(catg1, catgKadin, sb);

            ProductCategory catg2 = new ProductCategory() { Title = "Erkek Giyim" };
            DataContext.ProductCategories.Add(catg2);
            countCatg = AddCategory(catg2, catgErkek, sb);

            ProductCategory catg3 = new ProductCategory() { Title = "Çocuk Giyim" };
            DataContext.ProductCategories.Add(catg3);
            countCatg = AddCategory(catg3, catgCocuk, sb);
        }

        private int AddCategory(ProductCategory parentCatg, string[] subCategories, StringBuilder sb)
        {
            int countCatg = 0;
            DataContext.ProductCategories.Add(parentCatg);
            sb.Append("Category ");
            sb.Append(parentCatg.Title);
            sb.Append(" added<br>");
            foreach (var item in subCategories)
            {
                var catg = new ProductCategory()
                {
                    Title = item,
                    ParentCategory = parentCatg
                };
                DataContext.ProductCategories.Add(catg);
                countCatg++;
                Random rnd = new Random();
                int countProduct = rnd.Next(3, 7);
                for (int i = 0; i < countProduct; i++)
                {
                    string[] lips = Text.GetRandomLipsumSentence().Split(' ');
                    int wordCount = rnd.Next(2, 4);
                    var prod = new Product() { Category = catg };
                    for (int j = 0; j < wordCount; j++)
                    {
                        if (string.IsNullOrEmpty(prod.Title))
                        {
                            prod.Title = lips[rnd.Next(0, lips.Length - 1)].ToSentenceCase();
                        }
                        else
                        {
                            prod.Title = prod.Title + " " + lips[rnd.Next(0, lips.Length - 1)].ToSentenceCase();
                        }
                    }
                    DataContext.Products.Add(prod);
                }
            }
            sb.Append(countCatg);
            sb.Append(" sub categies added<br>");
            return countCatg;
        }


        protected CriticalPathContext DataContext
        {
            get
            {
                if (_dataContext == null)
                {
                    _dataContext = new CriticalPathContext();
                }
                return _dataContext;
            }
        }
        private CriticalPathContext _dataContext;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_dataContext != null)
                {
                    _dataContext.Dispose();
                    _dataContext = null;
                }
            }
            base.Dispose(disposing);
        }


        //

        //public override async Task<ActionResult> SeedRoles()
        //{
        //    var result = await base.SeedRoles();

        //    StringBuilder sb = new StringBuilder();

        //    var supervisor = new OzzIdentity.Models.OzzUser()
        //    {
        //        FirstName = "Super",
        //        LastName = "Doe",
        //        Email = "super1@mail.xy",
        //        UserName = "super1@mail.xy"
        //    };
        //    var clerk = new OzzIdentity.Models.OzzUser()
        //    {
        //        FirstName = "Clerk",
        //        LastName = "Doe",
        //        Email = SecurityRoles.Clerk + "@mail.xy",
        //        UserName = SecurityRoles.Clerk + "@mail.xy"
        //    };
        //    var observer = new OzzIdentity.Models.OzzUser()
        //    {
        //        FirstName = "Observer",
        //        LastName = "Doe",
        //        Email = SecurityRoles.Observer + "@mail.xy",
        //        UserName = SecurityRoles.Observer + "@mail.xy"
        //    };

        //    var superResult = await UserManager.CreateAsync(supervisor, "Dnm!2345");
        //    if (superResult.Succeeded)
        //    {
        //        await UserManager.AddToRoleAsync(supervisor.Id, SecurityRoles.Supervisor);
        //    }

        //    var clerkResult = await UserManager.CreateAsync(clerk, "Dnm!2345");
        //    if (clerkResult.Succeeded)
        //    {
        //        await UserManager.AddToRoleAsync(clerk.Id, SecurityRoles.Clerk);
        //    }

        //    var obsResult = await UserManager.CreateAsync(observer, "Dnm!2345");
        //    if (obsResult.Succeeded)
        //    {
        //        await UserManager.AddToRoleAsync(observer.Id, SecurityRoles.Observer);
        //    }
        //    await UserManager.CreateAsync(new OzzIdentity.Models.OzzUser()
        //    {
        //        FirstName = "User",
        //        LastName = "Doe",
        //        Email = "user@mail.xy",
        //        UserName = "user1"
        //    }, "Dnm!2345");

        //    return result;
        //}
    }
}