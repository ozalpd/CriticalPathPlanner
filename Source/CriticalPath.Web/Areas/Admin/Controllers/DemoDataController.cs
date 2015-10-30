using CriticalPath.Data;
using CriticalPath.Web.Controllers;
using CriticalPath.Web.Models;
using OzzUtils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CriticalPath.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = SecurityRoles.Admin)]
    public class DemoDataController : BaseController
    {
        // GET: Admin/DemoData
        public async Task<ActionResult> Index()
        {
            ViewBag.status = await GetRecordCounts();
            return View();
        }

        // GET: Admin/DemoData/Seed
        public async Task<ActionResult> Seed()
        {
            var sb = new StringBuilder();

            await SeedProducts(sb);
            await DataContext.SaveChangesAsync(this);
            await SeedContacts(sb);
            await DataContext.SaveChangesAsync(this);

            ViewBag.status = sb.ToString();

            return View();
        }

        private async Task SeedContacts(StringBuilder sb)
        {
            var contactsCount = await DataContext.Contacts.CountAsync();
            if (contactsCount > 0)
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
            }, new Customer {
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
                    CompanyName = "Loreanna Textile Corp",
                    City = "Norfolk",
                    CustomerCode = "Loreanna",
                    Address1 = "389 Cleveland Street",
                    Notes = "In aliqua fugiat magna adipisicing magna cillum enim exercitation ullamco cillum ullamco.",
                    Phone1 = "(944) 444-2599",
                    Phone2 = "(877) 466-3224"
            }, new Customer {
                    CompanyName = "Zelda Inc.",
                    CustomerCode = "Zelda",
                    City = "Homestead",
                    Address1 = "118 Applegate Court",
                    Phone1 = "+1 (888) 435-3835",
                    Phone2 = "+1 (898) 597-3808",
                    Notes = "Laborum aliquip pariatur aliqua dolor voluptate cillum proident officia in.",
            }, new Customer {
                    CompanyName = "Textile Corp.",
                    CustomerCode = "TextileCorp",
                    City = "Hartsville/Hartley",
                    Address1 = "253 Reeve Place",
                    Phone1 = "+1 (880) 473-3081",
                    Phone2 = "+1 (972) 568-2580",
                    Notes = "Mollit Lorem cupidatat voluptate occaecat sunt sunt eu in dolore ad eiusmod.",
            }, new Customer {
                    CompanyName = "Kaliburn LLC",
                    CustomerCode = "Kaliburn",
                    City = "Kaliburn",
                    Address1 = "694 Ashford Street",
                    Phone1 = "+1 (983) 445-2431",
                    Phone2 = "+1 (931) 552-3165",
                    Notes = "Ea ea amet consectetur excepteur velit reprehenderit tempor dolor eiusmod laborum voluptate aute.",
            }, new Customer {
                    CompanyName = "Medow Incorporated",
                    CustomerCode = "MedowInc",
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

            int i = 0;
            var c = customers[i];
            c.Contacts.Add(new Contact
            {
                FirstName = "Tucker",
                LastName = "Gentry",
                EmailHome = "tucker.gentry@" + c.CompanyName.ToLowerInvariant().Replace(".", "").Replace(" ", "") + ".tv"
            });
            i++; c = customers[i % customers.Length];
            c.Contacts.Add(new Contact
            {
                FirstName = "Thelma",
                LastName = "Mcdaniel",
                EmailWork = "thelma.mcdaniel@" + c.CompanyName.ToLowerInvariant().Replace(".", "").Replace(" ", "") + ".me"
            });
            i++; c = customers[i % customers.Length];
            c.Contacts.Add(new Contact
            {
                FirstName = "Michael",
                LastName = "Bradshaw",
                EmailHome = "michael.bradshaw@" + c.CompanyName.ToLowerInvariant().Replace(".", "").Replace(" ", "") + ".info"
            });
            i++; c = customers[i % customers.Length];
            c.Contacts.Add(new Contact
            {
                FirstName = "Jannie",
                LastName = "Murray",
                EmailWork = "jannie.murray@" + c.CompanyName.ToLowerInvariant().Replace(".", "").Replace(" ", "") + ".org"
            });
            i++; c = customers[i % customers.Length];
            c.Contacts.Add(new Contact
            {
                FirstName = "Weber",
                LastName = "Gross",
                EmailWork = "weber.gross@" + c.CompanyName.ToLowerInvariant().Replace(".", "").Replace(" ", "") + ".biz"
            });
            i++; c = customers[i % customers.Length];
            c.Contacts.Add(new Contact
            {
                FirstName = "Myra",
                LastName = "Aguilar",
                EmailHome = "myra.aguilar@" + c.CompanyName.ToLowerInvariant().Replace(".", "").Replace(" ", "") + ".tv"
            });

            i++; c = customers[i % customers.Length];
            c.Contacts.Add(new Contact
            {
                FirstName = "Bray",
                LastName = "Spencer",
                EmailWork = "bray.spencer@" + c.CompanyName.ToLowerInvariant().Replace(".", "").Replace(" ", "") + ".ca"
            });
            i++; c = customers[i % customers.Length];
            c.Contacts.Add(new Contact
            {
                FirstName = "Ruiz",
                LastName = "Moody",
                EmailHome = "ruiz.moody@" + c.CompanyName.ToLowerInvariant().Replace(".", "").Replace(" ", "") + ".biz"
            });
            i++; c = customers[i % customers.Length];
            c.Contacts.Add(new Contact
            {
                FirstName = "Bridgette",
                LastName = "Murray",
                EmailWork = "bridgette.murray@" + c.CompanyName.ToLowerInvariant().Replace(".", "").Replace(" ", "") + ".tv"
            });

            i++; c = customers[i % customers.Length];
            c.Contacts.Add(new Contact
            {
                FirstName = "Mücahit",
                LastName = "Demir",
                EmailWork = "mucahit.demir@" + c.CompanyName.ToLowerInvariant().Replace(".", "").Replace(" ", "") + ".ca"
            });
            i++; c = customers[i % customers.Length];
            c.Contacts.Add(new Contact
            {
                FirstName = "Özge",
                LastName = "Şahin",
                EmailHome = "ozge.sahin@" + c.CompanyName.ToLowerInvariant().Replace(".", "").Replace(" ", "") + ".biz"
            });
            i++; c = customers[i % customers.Length];
            c.Contacts.Add(new Contact
            {
                FirstName = "Selahattin",
                LastName = "Çakaler",
                EmailWork = "selahattin.c@" + c.CompanyName.ToLowerInvariant().Replace(".", "").Replace(" ", "") + ".tv"
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
            var categoryCount = await DataContext.ProductCategories.CountAsync();
            if (categoryCount > 0)
            {
                sb.Append("Database has ProductCategory records already!<br>");
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
            countCatg = AddCategory(catg1, catgKadin, sb, countCatg);

            ProductCategory catg2 = new ProductCategory() { Title = "Erkek Giyim" };
            DataContext.ProductCategories.Add(catg2);
            countCatg = AddCategory(catg2, catgErkek, sb, countCatg);

            ProductCategory catg3 = new ProductCategory() { Title = "Çocuk Giyim" };
            DataContext.ProductCategories.Add(catg3);
            countCatg = AddCategory(catg3, catgCocuk, sb, countCatg);
        }

        private int AddCategory(ProductCategory parentCatg, string[] subCategories, StringBuilder sb, int countCatg)
        {
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
                var lipsums = Text.LipsumSentences;
                for (int i = 0; i < countProduct; i++)
                {
                    string[] lips = lipsums[countCatg % lipsums.Length].Split(' ');
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

        private async Task<string> GetRecordCounts()
        {
            var sb = new StringBuilder();

            int count = 0;

            count = await DataContext.GetCustomerQuery().CountAsync();
            sb.Append("Database has ");
            sb.Append(count);
            sb.Append(" Customer records.<br>");

            count = await DataContext.GetSupplierQuery().CountAsync();
            sb.Append("Database has ");
            sb.Append(count);
            sb.Append(" Supplier records.<br>");

            count = await DataContext.Contacts.CountAsync();
            sb.Append("Database has ");
            sb.Append(count);
            sb.Append(" Contact records.<br>");

            count = await DataContext.ProductCategories.CountAsync();
            sb.Append("Database has ");
            sb.Append(count);
            sb.Append(" ProductCategory records.<br>");

            count = await DataContext.Products.CountAsync();
            sb.Append("Database has ");
            sb.Append(count);
            sb.Append(" Product records.<br>");

            return sb.ToString();
        }
    }
}