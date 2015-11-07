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
    public partial class SetupController : BaseController
    {
        // GET: Admin/DemoData/Seed
        public async Task<ActionResult> SeedDemoData()
        {
            var sb = new StringBuilder();

            await SeedSizingStandards(sb);
            await DataContext.SaveChangesAsync(this);

            await SeedProducts(sb);
            await DataContext.SaveChangesAsync(this);

            await SeedCustomers(sb);
            await DataContext.SaveChangesAsync(this);

            await SeedSuppliers(sb);
            await DataContext.SaveChangesAsync(this);

            ViewBag.status = sb.ToString();

            return View("Index");
        }

        private async Task SeedCustomers(StringBuilder sb)
        {
            var contactsCount = await DataContext
                                .Companies
                                .OfType<Customer>()
                                .CountAsync();
            if (contactsCount > 0)
            {
                sb.Append("Database has customer records already!<br>");
                return;
            }

            Customer[] customers = MockCustomers();
            AddContactsToCustomers(customers);

            foreach (var item in customers)
            {
                DataContext.Companies.Add(item);
                sb.Append("Customer: ");
                sb.Append(item.CompanyName);
                sb.Append(" added<br>");
            }
        }

        private async Task SeedSuppliers(StringBuilder sb)
        {
            var contactsCount = await DataContext
                                .Companies
                                .OfType<Supplier>()
                                .CountAsync();
            if (contactsCount > 0)
            {
                sb.Append("Database has Supplier records already!<br>");
                return;
            }

            Supplier[] Suppliers = MockSuppliers();

            foreach (var item in Suppliers)
            {
                DataContext.Companies.Add(item);
                sb.Append("Supplier: ");
                sb.Append(item.CompanyName);
                sb.Append(" added<br>");
            }
        }

        private static void AddContactsToCustomers(Customer[] customers)
        {
            int i = 0;
            var c = customers[i];
            c.Contacts.Add(new Contact
            {
                FirstName = "Tucker",
                LastName = "Gentry",
                EmailHome = "tucker.gentry@" + c.CompanyName.ToLowerInvariant().Replace(".", "").Replace(" ", "") + ".tv",
                IsActive = true
            });
            i++; c = customers[i % customers.Length];
            c.Contacts.Add(new Contact
            {
                FirstName = "Thelma",
                LastName = "Mcdaniel",
                EmailWork = "thelma.mcdaniel@" + c.CompanyName.ToLowerInvariant().Replace(".", "").Replace(" ", "") + ".me",
                IsActive = true
            });
            i++; c = customers[i % customers.Length];
            c.Contacts.Add(new Contact
            {
                FirstName = "Michael",
                LastName = "Bradshaw",
                EmailHome = "michael.bradshaw@" + c.CompanyName.ToLowerInvariant().Replace(".", "").Replace(" ", "") + ".info",
                IsActive = true
            });
            i++; c = customers[i % customers.Length];
            c.Contacts.Add(new Contact
            {
                FirstName = "Jannie",
                LastName = "Murray",
                EmailWork = "jannie.murray@" + c.CompanyName.ToLowerInvariant().Replace(".", "").Replace(" ", "") + ".org",
                IsActive = true
            });
            i++; c = customers[i % customers.Length];
            c.Contacts.Add(new Contact
            {
                FirstName = "Weber",
                LastName = "Gross",
                EmailWork = "weber.gross@" + c.CompanyName.ToLowerInvariant().Replace(".", "").Replace(" ", "") + ".biz",
                IsActive = true
            });
            i++; c = customers[i % customers.Length];
            c.Contacts.Add(new Contact
            {
                FirstName = "Myra",
                LastName = "Aguilar",
                EmailHome = "myra.aguilar@" + c.CompanyName.ToLowerInvariant().Replace(".", "").Replace(" ", "") + ".tv",
                IsActive = true
            });

            i++; c = customers[i % customers.Length];
            c.Contacts.Add(new Contact
            {
                FirstName = "Bray",
                LastName = "Spencer",
                EmailWork = "bray.spencer@" + c.CompanyName.ToLowerInvariant().Replace(".", "").Replace(" ", "") + ".ca",
                IsActive = true
            });
            i++; c = customers[i % customers.Length];
            c.Contacts.Add(new Contact
            {
                FirstName = "Ruiz",
                LastName = "Moody",
                EmailHome = "ruiz.moody@" + c.CompanyName.ToLowerInvariant().Replace(".", "").Replace(" ", "") + ".biz",
                IsActive = true
            });
            i++; c = customers[i % customers.Length];
            c.Contacts.Add(new Contact
            {
                FirstName = "Bridgette",
                LastName = "Murray",
                EmailWork = "bridgette.murray@" + c.CompanyName.ToLowerInvariant().Replace(".", "").Replace(" ", "") + ".tv",
                IsActive = true
            });

            i++; c = customers[i % customers.Length];
            c.Contacts.Add(new Contact
            {
                FirstName = "Mücahit",
                LastName = "Demir",
                EmailWork = "mucahit.demir@" + c.CompanyName.ToLowerInvariant().Replace(".", "").Replace(" ", "") + ".ca",
                IsActive = true
            });
            i++; c = customers[i % customers.Length];
            c.Contacts.Add(new Contact
            {
                FirstName = "Özge",
                LastName = "Şahin",
                EmailHome = "ozge.sahin@" + c.CompanyName.ToLowerInvariant().Replace(".", "").Replace(" ", "") + ".biz",
                IsActive = true
            });
            i++; c = customers[i % customers.Length];
            c.Contacts.Add(new Contact
            {
                FirstName = "Selahattin",
                LastName = "Çakaler",
                EmailWork = "selahattin.c@" + c.CompanyName.ToLowerInvariant().Replace(".", "").Replace(" ", "") + ".tv",
                IsActive = true
            });
        }

        private Customer[] MockCustomers()
        {
            Customer[] customers =  {
                    new Customer
                    {
                        CompanyName = "Nurplex",
                        City = "Norfolk",
                        CustomerCode = "Nurplex",
                        Address1 = "389 Cleveland Street",
                        Notes = "In aliqua fugiat magna adipisicing magna cillum enim exercitation ullamco cillum ullamco.",
                        Phone1 = "(944) 444-2599",
                        Phone2 = "(877) 466-3224",
                        IsActive = true
                    },
                    new Customer
                    {
                        CompanyName = "Wazzu",
                        CustomerCode = "1234",
                        City = "Homestead",
                        Address1 = "118 Applegate Court",
                        Phone1 = "+1 (888) 435-3835",
                        Phone2 = "+1 (898) 597-3808",
                        Notes = "Laborum aliquip pariatur aliqua dolor voluptate cillum proident officia in.",
                        IsActive = true
                    },
                    new Customer
                    {
                        CompanyName = "Qiao Corp.",
                        CustomerCode = "Qiao",
                        City = "Hartsville/Hartley",
                        Address1 = "253 Reeve Place",
                        Phone1 = "+1 (880) 473-3081",
                        Phone2 = "+1 (972) 568-2580",
                        Notes = "Mollit Lorem cupidatat voluptate occaecat sunt sunt eu in dolore ad eiusmod.",
                        IsActive = true
                    },
                    new Customer
                    {
                        CompanyName = "Solaren",
                        CustomerCode = "555",
                        City = "Olney",
                        Address1 = "694 Ashford Street",
                        Phone1 = "+1 (983) 445-2431",
                        Phone2 = "+1 (931) 552-3165",
                        Notes = "Ea ea amet consectetur excepteur velit reprehenderit tempor dolor eiusmod laborum voluptate aute.",
                        IsActive = true
                    },
                    new Customer
                    {
                        CompanyName = "Cowtown Incorporated",
                        CustomerCode = "Cowtown",
                        City = "Edgewater",
                        Address1 = "397 Vandam Street",
                        Phone1 = "+1 (999) 498-3117",
                        Phone2 = "+1 (876) 457-3103",
                        Notes = "Do ex magna mollit excepteur eu fugiat sit minim ullamco est est.",
                        IsActive = true
                    },
                    new Customer
                    {
                        CompanyName = "Loreanna Textile Corp",
                        City = "Norfolk",
                        CustomerCode = "Loreanna",
                        Address1 = "389 Cleveland Street",
                        Notes = "In aliqua fugiat magna adipisicing magna cillum enim exercitation ullamco cillum ullamco.",
                        Phone1 = "(944) 444-2599",
                        Phone2 = "(877) 466-3224",
                        IsActive = true
                    },
                    new Customer
                    {
                        CompanyName = "Zelda Inc.",
                        CustomerCode = "Zelda",
                        City = "Homestead",
                        Address1 = "118 Applegate Court",
                        Phone1 = "+1 (888) 435-3835",
                        Phone2 = "+1 (898) 597-3808",
                        Notes = "Laborum aliquip pariatur aliqua dolor voluptate cillum proident officia in.",
                        IsActive = true
                    },
                    new Customer
                    {
                        CompanyName = "Textile Corp.",
                        CustomerCode = "TextileCorp",
                        City = "Hartsville/Hartley",
                        Address1 = "253 Reeve Place",
                        Phone1 = "+1 (880) 473-3081",
                        Phone2 = "+1 (972) 568-2580",
                        Notes = "Mollit Lorem cupidatat voluptate occaecat sunt sunt eu in dolore ad eiusmod.",
                        IsActive = true
                    },
                    new Customer
                    {
                        CompanyName = "Kaliburn LLC",
                        CustomerCode = "Kaliburn",
                        City = "Kaliburn",
                        Address1 = "694 Ashford Street",
                        Phone1 = "+1 (983) 445-2431",
                        Phone2 = "+1 (931) 552-3165",
                        Notes = "Ea ea amet consectetur excepteur velit reprehenderit tempor dolor eiusmod laborum voluptate aute.",
                        IsActive = true
                    },
                    new Customer
                    {
                        CompanyName = "Medow Incorporated",
                        CustomerCode = "MedowInc",
                        City = "Edgewater",
                        Address1 = "397 Vandam Street",
                        Phone1 = "+1 (999) 498-3117",
                        Phone2 = "+1 (876) 457-3103",
                        Notes = "Do ex magna mollit excepteur eu fugiat sit minim ullamco est est.",
                        IsActive = true
                    },
                    new Customer
                    {
                        CompanyName = "Genesynk Associates",
                        CustomerCode = "Genesynk",
                        City = "Tedrow",
                        Address1 = "415 Newport Street",
                        Phone1 = "+1 (984) 410-2831",
                        Phone2 = "+1 (873) 545-2215",
                        Notes = "Ullamco reprehenderit esse id qui voluptate ipsum veniam.",
                        IsActive = true
                    },
                    new Customer
                    {
                        CompanyName = "Zilch Industries",
                        CustomerCode = "Zilch",
                        City = "Katonah",
                        Address1 = "594 Eckford Street",
                        Phone1 = "+1 (835) 416-2323",
                        Phone2 = "+1 (845) 440-2230",
                        Notes = "Mollit minim cillum ex reprehenderit nisi proident nulla.",
                        IsActive = true
                    }
                };

            return customers;
        }

        private Supplier[] MockSuppliers()
        {
            Supplier[] suppliers = {
                new Supplier()
                {
                    CompanyName = "Cytrek",
                    City = "Munjor",
                    SupplierCode = "Munjor",
                    Address1 = "146 Newport Street",
                    Phone1 = "+1 (802) 565-2721",
                    Phone2 = "+1 (805) 494-2722",
                    Notes = "Ullamco laboris non occaecat minim consectetur ullamco cillum veniam enim aute reprehenderit. Officia aliqua ipsum magna nisi sunt eu aliquip nostrud. Anim excepteur nulla fugiat velit.",
                    Address2 = "650 Gold Street, Lloyd, Michigan, 5066",
                    IsActive = true
                },
                new Supplier()
                {
                    CompanyName = "Isosure",
                    SupplierCode = "Isosure",
                    City = "Cataract",
                    Address1 = "900 Hutchinson Court",
                    Phone1 = "+1 (867) 417-3491",
                    Phone2 = "+1 (971) 421-2361",
                    Notes = "Amet eu proident duis exercitation eu. Officia voluptate aliqua adipisicing duis aute. Duis ullamco pariatur deserunt adipisicing nisi officia laborum ipsum in adipisicing laborum adipisicing eiusmod proident. Commodo velit est mollit qui quis pariatur pariatur reprehenderit reprehenderit.",
                    Address2 = "635 Raleigh Place, Linganore, Kentucky, 2805"
                }
                ,new Supplier()
                {
                    CompanyName = "Realysis",
                    SupplierCode = "Realysis",
                    City = "Hebron",
                    Address1 = "121 Narrows Avenue",
                    Phone1 = "+1 (897) 537-3375",
                    Phone2 = "+1 (890) 587-2149",
                    Notes = "Non irure reprehenderit ex amet tempor. Cillum et enim enim qui magna laboris ullamco tempor eiusmod ipsum anim exercitation voluptate. Sunt ea pariatur deserunt culpa pariatur magna consectetur. Nisi aliquip nostrud cupidatat Lorem. Qui cillum voluptate adipisicing aute commodo aliquip consequat eiusmod.",
                    Address2 = "368 Ovington Avenue, Savannah, Arkansas, 8035"
                },
                new Supplier()
                {
                    CompanyName = "Bostonic",
                    SupplierCode = "Bostonic",
                    City = "Belvoir",
                    Address1 = "851 Ellery Street",
                    Phone1 = "+1 (920) 490-3596",
                    Phone2 = "+1 (992) 431-2791",
                    Notes = "Adipisicing nostrud aliquip anim dolore ex duis esse dolor reprehenderit dolor anim quis. Dolor id occaecat non voluptate mollit voluptate tempor cillum irure nostrud duis excepteur exercitation adipisicing. Tempor dolor do minim aliquip eu sint Lorem ipsum elit ullamco mollit qui. Consectetur laboris consectetur sint velit voluptate laborum culpa laborum nulla dolore tempor irure. Consequat qui minim irure minim proident laborum id velit nostrud aute officia nisi in et. Ad sit anim anim fugiat sunt consequat. Aute qui commodo cupidatat tempor enim.",
                    Address2 = "330 Emmons Avenue, Reinerton, New York, 3453"
                },
                new Supplier()
                {
                    CompanyName = "Ontality",
                    SupplierCode = "Ontality",
                    City = "Stollings",
                    Address1 = "156 Hyman Court",
                    Phone1 = "+1 (869) 425-3039",
                    Phone2 = "+1 (921) 528-3334",
                    Notes = "Exercitation sint proident consectetur sit consequat fugiat nisi veniam quis non. Voluptate aliquip aute consequat exercitation labore sunt consequat consectetur amet veniam qui incididunt minim officia. Elit est fugiat culpa do nostrud est ullamco ut culpa enim. Eu duis id sint esse consectetur deserunt. Velit consectetur aute Lorem ipsum id nostrud voluptate nostrud quis id. Aliqua laboris nostrud exercitation magna amet ipsum adipisicing labore occaecat.",
                    Address2 = "505 Jewel Street, Norfolk, District Of Columbia, 7514"
                },
                new Supplier()
                {
                    CompanyName = "Exozent",
                    SupplierCode = "Exozent",
                    City = "Noxen",
                    Address1 = "225 Wyckoff Avenue",
                    Phone1 = "+1 (946) 533-3029",
                    Phone2 = "+1 (883) 462-3586",
                    Notes = "Elit qui voluptate consectetur magna ullamco pariatur officia sint fugiat cupidatat. Duis consectetur commodo ullamco Lorem eu eu. Non minim minim culpa irure eu commodo in incididunt ex.",
                    Contacts = {
                        new Contact()
                        {
                            FirstName = "Savage",
                            LastName = "Stout",
                            EmailWork = "savage.stout@mail.co.uk"
                        },
                        new Contact()
                        {
                            FirstName = "Stone",
                            LastName = "Henry",
                            EmailWork = "stone.henry@mail.com"
                        }
                    },
                    Address2 = "431 Canal Avenue, Salvo, Marshall Islands, 7050"
                },
                new Supplier()
                {
                    CompanyName = "Billmed",
                    SupplierCode = "Billmed",
                    City = "Beyerville",
                    Address1 = "298 Ryerson Street",
                    Phone1 = "+1 (805) 472-2276",
                    Phone2 = "+1 (992) 462-3220",
                    Notes = "Velit ut Lorem consequat id. Reprehenderit duis in officia mollit in aliqua. Cillum aliqua aliqua ea non eu ipsum proident laboris ullamco duis nostrud officia elit officia. Cillum ut veniam elit nostrud aute veniam et enim mollit velit mollit ipsum. Non exercitation non nisi sit ea.",
                    Contacts = {
                        new Contact()
                        {
                            FirstName = "Henry",
                            LastName = "Stout",
                            EmailWork = "henry.stout@mail.co.uk"
                        },
                        new Contact()
                        {
                            FirstName = "Mann",
                            LastName = "Davis",
                            EmailWork = "mann.davis@mail.info"
                        }
                    },
                    Address2 = "555 Grattan Street, Osage, Kansas, 1981"
                },
                new Supplier()
                {
                    CompanyName = "Terrasys",
                    SupplierCode = "Code 123",
                    City = "Dunbar",
                    Address1 = "820 Independence Avenue",
                    Phone1 = "+1 (999) 573-2264",
                    Phone2 = "+1 (929) 600-3053",
                    Notes = "Ut id labore aute labore aliqua voluptate laborum pariatur sint Lorem quis. Elit consequat est consequat occaecat anim fugiat incididunt mollit laboris nulla culpa. Laborum consectetur officia fugiat adipisicing sit cillum mollit cupidatat. Ipsum sint pariatur ea commodo ullamco do. Est laboris consectetur magna fugiat officia ea enim tempor pariatur nisi aute do commodo. Nisi sunt proident excepteur occaecat pariatur et est cillum ut ullamco eu.",
                    Address2 = "839 Cadman Plaza, Gardiner, Maryland, 4728"
                },
                new Supplier()
                {
                    CompanyName = "Dyno",
                    SupplierCode = "Code 123",
                    City = "Weedville",
                    Address1 = "857 Remsen Street",
                    Phone1 = "+1 (855) 532-3892",
                    Phone2 = "+1 (848) 506-2957",
                    Notes = "Sint est dolor minim occaecat eu culpa irure ex qui. Ad ea proident veniam in. Reprehenderit nisi nisi non laborum ex aliquip sint pariatur consectetur nulla. Incididunt mollit elit nostrud et aliquip incididunt Lorem labore non voluptate. Mollit aliquip veniam officia adipisicing duis id. Elit proident deserunt consectetur Lorem commodo amet qui consectetur culpa in.",
                    Address2 = "343 Moore Place, Thomasville, South Dakota, 3566"
                },
                new Supplier()
                {
                    CompanyName = "Indexia",
                    SupplierCode = "Code 123",
                    City = "Watrous",
                    Address1 = "704 Sands Street",
                    Phone1 = "+1 (981) 502-2404",
                    Phone2 = "+1 (855) 544-3811",
                    Notes = "Mollit eiusmod ex enim velit reprehenderit voluptate mollit in proident sint mollit nostrud consectetur. Ex quis et mollit aliqua proident reprehenderit eiusmod exercitation deserunt. Exercitation cupidatat Lorem sit consectetur irure.",
                    Address2 = "200 Florence Avenue, Cleary, New Hampshire, 9877"
                },
                new Supplier()
                {
                    CompanyName = "Hyplex",
                    SupplierCode = "Code 123",
                    City = "Sperryville",
                    Address1 = "282 Estate Road",
                    Phone1 = "+1 (851) 453-2692",
                    Phone2 = "+1 (984) 598-3847",
                    Notes = "Dolor magna ex exercitation non elit id proident incididunt cillum exercitation ea mollit. Proident tempor et cupidatat quis. Magna cupidatat enim adipisicing ut sunt commodo sit ipsum culpa duis. Consectetur eiusmod ad officia proident. Sunt aliquip non qui in non nulla Lorem consequat anim eu sit. Culpa labore consectetur non culpa consectetur irure.",
                    Address2 = "396 Keen Court, Bordelonville, Puerto Rico, 172"
                },
                new Supplier()
                {
                    CompanyName = "Animalia",
                    SupplierCode = "Code 123",
                    City = "Mansfield",
                    Address1 = "746 Dwight Street",
                    Phone1 = "+1 (854) 445-3758",
                    Phone2 = "+1 (838) 487-3474",
                    Notes = "Enim reprehenderit anim commodo nulla. Irure velit in sunt nisi voluptate aute et. Fugiat nostrud Lorem id ex esse incididunt aliqua aliqua eiusmod veniam anim consequat deserunt adipisicing. Irure labore sint veniam tempor elit reprehenderit occaecat aute in. Sint non aute quis enim Lorem reprehenderit.",
                    Address2 = "822 Dooley Street, Nash, Montana, 4875"
                },
                new Supplier()
                {
                    CompanyName = "Rugstars",
                    SupplierCode = "Code 123",
                    City = "Farmington",
                    Address1 = "259 Montauk Court",
                    Phone1 = "+1 (840) 462-2727",
                    Phone2 = "+1 (869) 566-3926",
                    Notes = "Ea voluptate labore cupidatat reprehenderit deserunt. Irure velit ullamco elit in cillum ad est cillum fugiat pariatur. Consectetur dolore velit irure enim dolor anim sit qui.",
                    Address2 = "675 Classon Avenue, Driftwood, South Carolina, 1248"
                },
                new Supplier()
                {
                    CompanyName = "Ludak",
                    SupplierCode = "Code 123",
                    City = "Kipp",
                    Address1 = "525 Voorhies Avenue",
                    Phone1 = "+1 (804) 417-3811",
                    Phone2 = "+1 (903) 404-3255",
                    Notes = "Officia in in veniam dolore nostrud. Consequat ullamco sit proident eu nulla. Eiusmod excepteur ipsum incididunt sunt sit adipisicing tempor quis esse nostrud qui eu enim. Ut nisi ullamco anim anim officia dolor qui enim labore est et voluptate officia velit.",
                    Address2 = "862 Gain Court, Katonah, Washington, 7337"
                },
                new Supplier()
                {
                    CompanyName = "Medmex",
                    SupplierCode = "Code 123",
                    City = "Wakarusa",
                    Address1 = "248 Howard Alley",
                    Phone1 = "+1 (847) 569-2411",
                    Phone2 = "+1 (831) 521-2237",
                    Notes = "Excepteur ipsum consectetur ut fugiat eu magna nisi laborum anim nostrud sunt nisi. Consequat magna cillum nulla ex magna culpa aliqua proident laborum fugiat voluptate dolor mollit. Non magna dolor aute deserunt ipsum eu nostrud magna mollit aute pariatur. Consectetur labore deserunt ut ut. Reprehenderit occaecat excepteur reprehenderit quis in sint aliquip non adipisicing aliquip cupidatat adipisicing quis ipsum.",
                    Address2 = "775 Kent Street, Kenwood, Oklahoma, 8689"
                }
            };

            return suppliers;
        }


        private async Task SeedProducts(StringBuilder sb)
        {
            var categoryCount = await DataContext.ProductCategories.CountAsync();
            if (categoryCount > 0)
            {
                sb.Append("Database has ProductCategory records already!<br>");
                return;
            }

            var sizing = await DataContext.SizingStandards.FirstOrDefaultAsync();
            if (sizing == null)
            {
                sb.Append("Database has no SizingStandard records! Product records cannot be created without SizingStandard!<br>");
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
            countCatg = AddCategory(catg1, catgKadin, sb, sizing, countCatg);

            ProductCategory catg2 = new ProductCategory() { Title = "Erkek Giyim" };
            DataContext.ProductCategories.Add(catg2);
            countCatg = AddCategory(catg2, catgErkek, sb, sizing, countCatg);

            var sizingChildren = await DataContext.SizingStandards.FirstOrDefaultAsync(s => s.Title.Contains("child"));
            if (sizingChildren == null)
                sizingChildren = sizing;
            ProductCategory catg3 = new ProductCategory() { Title = "Çocuk Giyim" };
            DataContext.ProductCategories.Add(catg3);
            countCatg = AddCategory(catg3, catgCocuk, sb, sizingChildren, countCatg);
        }

        private int AddCategory(ProductCategory parentCatg, string[] subCategories, StringBuilder sb, SizingStandard sizing, int countCatg)
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
                    var prod = new Product()
                    {
                        Category = catg,
                        IsActive = true
                    };
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
                    prod.SizingStandard = sizing;
                    DataContext.Products.Add(prod);
                }
            }
            sb.Append(countCatg);
            sb.Append(" sub categies added<br>");
            return countCatg;
        }

    }
}