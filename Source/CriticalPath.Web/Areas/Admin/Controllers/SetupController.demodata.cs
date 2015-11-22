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

            await SeedFreightTerms(sb);
            await DataContext.SaveChangesAsync(this);

            await SeedCustomers(sb);
            await DataContext.SaveChangesAsync(this);

            await SeedSuppliers(sb);
            await DataContext.SaveChangesAsync(this);

            await SeedCurrencies(sb);
            await DataContext.SaveChangesAsync(this);

            await SeedProducts(sb);
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
            });
            i++; c = customers[i % customers.Length];
            c.Contacts.Add(new Contact
            {
                FirstName = "Thelma",
                LastName = "Mcdaniel",
                EmailWork = "thelma.mcdaniel@" + c.CompanyName.ToLowerInvariant().Replace(".", "").Replace(" ", "") + ".me",
            });
            i++; c = customers[i % customers.Length];
            c.Contacts.Add(new Contact
            {
                FirstName = "Michael",
                LastName = "Bradshaw",
                EmailHome = "michael.bradshaw@" + c.CompanyName.ToLowerInvariant().Replace(".", "").Replace(" ", "") + ".info",
            });
            i++; c = customers[i % customers.Length];
            c.Contacts.Add(new Contact
            {
                FirstName = "Jannie",
                LastName = "Murray",
                EmailWork = "jannie.murray@" + c.CompanyName.ToLowerInvariant().Replace(".", "").Replace(" ", "") + ".org",
            });
            i++; c = customers[i % customers.Length];
            c.Contacts.Add(new Contact
            {
                FirstName = "Weber",
                LastName = "Gross",
                EmailWork = "weber.gross@" + c.CompanyName.ToLowerInvariant().Replace(".", "").Replace(" ", "") + ".biz",
            });
            i++; c = customers[i % customers.Length];
            c.Contacts.Add(new Contact
            {
                FirstName = "Myra",
                LastName = "Aguilar",
                EmailHome = "myra.aguilar@" + c.CompanyName.ToLowerInvariant().Replace(".", "").Replace(" ", "") + ".tv",
            });

            i++; c = customers[i % customers.Length];
            c.Contacts.Add(new Contact
            {
                FirstName = "Bray",
                LastName = "Spencer",
                EmailWork = "bray.spencer@" + c.CompanyName.ToLowerInvariant().Replace(".", "").Replace(" ", "") + ".ca",
            });
            i++; c = customers[i % customers.Length];
            c.Contacts.Add(new Contact
            {
                FirstName = "Ruiz",
                LastName = "Moody",
                EmailHome = "ruiz.moody@" + c.CompanyName.ToLowerInvariant().Replace(".", "").Replace(" ", "") + ".biz",
            });
            i++; c = customers[i % customers.Length];
            c.Contacts.Add(new Contact
            {
                FirstName = "Bridgette",
                LastName = "Murray",
                EmailWork = "bridgette.murray@" + c.CompanyName.ToLowerInvariant().Replace(".", "").Replace(" ", "") + ".tv",
            });

            i++; c = customers[i % customers.Length];
            c.Contacts.Add(new Contact
            {
                FirstName = "Mücahit",
                LastName = "Demir",
                EmailWork = "mucahit.demir@" + c.CompanyName.ToLowerInvariant().Replace(".", "").Replace(" ", "") + ".ca",
            });
            i++; c = customers[i % customers.Length];
            c.Contacts.Add(new Contact
            {
                FirstName = "Özge",
                LastName = "Şahin",
                EmailHome = "ozge.sahin@" + c.CompanyName.ToLowerInvariant().Replace(".", "").Replace(" ", "") + ".biz",
            });
            i++; c = customers[i % customers.Length];
            c.Contacts.Add(new Contact
            {
                FirstName = "Selahattin",
                LastName = "Çakaler",
                EmailWork = "selahattin.c@" + c.CompanyName.ToLowerInvariant().Replace(".", "").Replace(" ", "") + ".tv",
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
                },
             new Supplier() {
                    CompanyName = "Immunics",
                    SupplierCode = "IMMUNICS-3423",
                    City = "Biddle",
                    Address1 = "660 Wilson Street",
                    Address2 = "880 Truxton Street, Levant, Palau, 8054",
                    Phone1 = "+1 (932) 417-2844",
                    Phone2 = "+1 (925) 451-3350",
                    Notes = "Eiusmod magna cillum eu deserunt. Irure ut excepteur excepteur fugiat esse do officia velit consectetur dolor culpa labore. Consectetur consequat excepteur dolore Lorem non. Ex qui voluptate in pariatur eu.",
                    Contacts = {
                        new Contact() { FirstName = "Mayra", LastName = "Solomon", EmailWork = "mayra.solomon@genmex.biz" },
                        new Contact() { FirstName = "Lillie", LastName = "Mcknight", EmailWork = "lillie.mcknight@cedward.tv" },
                        new Contact() { FirstName = "Jimenez", LastName = "Roberson", EmailWork = "jimenez.roberson@organica.info" },
                        new Contact() { FirstName = "Iris", LastName = "Whitney", EmailWork = "iris.whitney@vortexaco.co.uk" },
                }
              },
             new Supplier() {
                    CompanyName = "Dancerity",
                    SupplierCode = "DANCERITY-2214",
                    City = "Belleview",
                    Address1 = "718 Main Street",
                    Address2 = "804 Apollo Street, Robbins, American Samoa, 5682",
                    Phone1 = "+1 (921) 426-3382",
                    Phone2 = "+1 (988) 506-3471",
                    Notes = "Id ut nostrud et anim tempor. In reprehenderit non duis dolor. Eiusmod laboris cupidatat magna nostrud enim deserunt ullamco nostrud sint nostrud. Occaecat occaecat do ipsum eu dolor officia. Velit anim mollit consequat consectetur aliqua labore eiusmod nulla duis qui ipsum. Quis voluptate ex occaecat pariatur aliquip culpa commodo.",
                    Contacts = {
                        new Contact() { FirstName = "Rosario", LastName = "Mclean", EmailWork = "rosario.mclean@essensia.ca" },
                        new Contact() { FirstName = "Ann", LastName = "Strickland", EmailWork = "ann.strickland@exostream.org" },
                        new Contact() { FirstName = "Jewel", LastName = "Jordan", EmailWork = "jewel.jordan@rameon.us" },
                }
              },
             new Supplier() {
                    CompanyName = "Aclima",
                    SupplierCode = "ACLIMA-9468",
                    City = "Blackgum",
                    Address1 = "889 Holly Street",
                    Address2 = "481 Pulaski Street, Haring, Kentucky, 3976",
                    Phone1 = "+1 (902) 456-2965",
                    Phone2 = "+1 (973) 594-2326",
                    Notes = "Occaecat officia voluptate commodo culpa amet velit non irure sit nostrud fugiat officia. Esse elit qui anim dolor duis ex commodo. Tempor qui anim laborum cillum sunt. Lorem nostrud ipsum proident sunt mollit.",
                    Contacts = {
                        new Contact() { FirstName = "Gloria", LastName = "Avery", EmailWork = "gloria.avery@zenolux.com" },
                        new Contact() { FirstName = "Robin", LastName = "Knapp", EmailWork = "robin.knapp@gology.biz" },
                }
              },
             new Supplier() {
                    CompanyName = "Ronelon",
                    SupplierCode = "RONELON-5032",
                    City = "Allison",
                    Address1 = "395 Cass Place",
                    Address2 = "267 Eaton Court, Valle, Northern Mariana Islands, 4690",
                    Phone1 = "+1 (819) 571-2145",
                    Phone2 = "+1 (935) 480-2729",
                    Notes = "Est do dolor esse nisi cupidatat occaecat. Non tempor nostrud aliqua voluptate ea nulla pariatur nostrud est nisi do nulla voluptate. Pariatur aliqua culpa qui aute adipisicing Lorem officia pariatur enim non sint irure. Nisi mollit laborum voluptate consectetur cupidatat veniam sunt.",
                    Contacts = {
                        new Contact() { FirstName = "Edith", LastName = "Boyer", EmailWork = "edith.boyer@digique.net" },
                        new Contact() { FirstName = "Phillips", LastName = "Sears", EmailWork = "phillips.sears@lovepad.io" },
                        new Contact() { FirstName = "Abigail", LastName = "Zamora", EmailWork = "abigail.zamora@tropoli.me" },
                }
              },
             new Supplier() {
                    CompanyName = "Blanet",
                    SupplierCode = "BLANET-1389",
                    City = "Harborton",
                    Address1 = "697 Box Street",
                    Address2 = "103 Gold Street, Finderne, North Carolina, 1543",
                    Phone1 = "+1 (898) 499-2203",
                    Phone2 = "+1 (927) 482-3222",
                    Notes = "Culpa voluptate labore reprehenderit proident veniam eu proident ad cillum aliqua id sunt quis. Amet ea quis officia consequat voluptate sit fugiat proident irure in aute reprehenderit labore. Dolore et incididunt veniam sit aute. Ad ex elit labore velit nostrud eiusmod qui exercitation ex sint qui veniam aliqua. Elit sint cupidatat amet labore do do exercitation commodo consectetur. Sint tempor ipsum culpa pariatur.",
                    Contacts = {
                        new Contact() { FirstName = "Sue", LastName = "Huber", EmailWork = "sue.huber@exoblue.biz" },
                }
              },
             new Supplier() {
                    CompanyName = "Magnemo",
                    SupplierCode = "MAGNEMO-1768",
                    City = "Bangor",
                    Address1 = "802 Autumn Avenue",
                    Address2 = "681 Richards Street, Gwynn, Oklahoma, 993",
                    Phone1 = "+1 (936) 530-3933",
                    Phone2 = "+1 (985) 498-3529",
                    Notes = "Incididunt proident exercitation consectetur sint dolore sint tempor veniam cupidatat esse do tempor fugiat. Aute cillum veniam aliquip qui minim eu ut laboris nostrud nostrud quis. Esse adipisicing consequat commodo non laboris deserunt aute irure minim. Incididunt incididunt reprehenderit ipsum elit excepteur velit irure proident id nisi.",
                    Contacts = {
                        new Contact() { FirstName = "Mitchell", LastName = "Compton", EmailWork = "mitchell.compton@renovize.tv" },
                        new Contact() { FirstName = "Roxie", LastName = "Snider", EmailWork = "roxie.snider@xiix.info" },
                        new Contact() { FirstName = "Pam", LastName = "Gonzales", EmailWork = "pam.gonzales@applideck.co.uk" },
                }
              },
             new Supplier() {
                    CompanyName = "Candecor",
                    SupplierCode = "CANDECOR-6710",
                    City = "Matthews",
                    Address1 = "203 King Street",
                    Address2 = "923 Varick Street, Caln, Alabama, 9814",
                    Phone1 = "+1 (906) 553-2108",
                    Phone2 = "+1 (839) 550-2353",
                    Notes = "Exercitation enim nisi ipsum laborum aliqua minim nulla ex ex dolore ex exercitation. Excepteur laboris proident deserunt fugiat aliqua quis excepteur dolore. Incididunt proident est consectetur aliquip nostrud dolore. Et ullamco reprehenderit sint magna laboris sint esse adipisicing Lorem nisi.",
                    Contacts = {
                        new Contact() { FirstName = "Jolene", LastName = "Holder", EmailWork = "jolene.holder@zepitope.ca" },
                        new Contact() { FirstName = "Lily", LastName = "Horne", EmailWork = "lily.horne@zeam.org" },
                }
              },
             new Supplier() {
                    CompanyName = "Xth",
                    SupplierCode = "XTH-3396",
                    City = "Lupton",
                    Address1 = "994 Jackson Street",
                    Address2 = "561 Bouck Court, Westwood, Minnesota, 9986",
                    Phone1 = "+1 (911) 507-2245",
                    Phone2 = "+1 (931) 416-2699",
                    Notes = "Reprehenderit ea ad irure veniam velit minim. Mollit commodo non pariatur fugiat ipsum enim velit nostrud sint consequat adipisicing ullamco nostrud. Proident adipisicing eu quis veniam commodo. Excepteur est exercitation eu laboris aute non deserunt proident do. Elit elit deserunt in amet incididunt id labore cupidatat do laborum consectetur ex cillum. Exercitation elit fugiat cillum elit cupidatat pariatur ullamco consequat laboris tempor.",
                    Contacts = {
                        new Contact() { FirstName = "Levy", LastName = "Best", EmailWork = "levy.best@genekom.us" },
                        new Contact() { FirstName = "Stanton", LastName = "Kelly", EmailWork = "stanton.kelly@imperium.com" },
                }
              },
             new Supplier() {
                    CompanyName = "Toyletry",
                    SupplierCode = "TOYLETRY-6155",
                    City = "Rodanthe",
                    Address1 = "488 Wythe Avenue",
                    Address2 = "838 Bethel Loop, Bordelonville, Wyoming, 3389",
                    Phone1 = "+1 (850) 544-3169",
                    Phone2 = "+1 (958) 551-2260",
                    Notes = "Nostrud reprehenderit nulla exercitation quis sint voluptate. Ullamco duis adipisicing aliqua in irure consequat fugiat. Cillum officia ex est aliquip veniam do sint.",
                    Contacts = {
                        new Contact() { FirstName = "Conner", LastName = "Hammond", EmailWork = "conner.hammond@qnekt.biz" },
                }
              },
             new Supplier() {
                    CompanyName = "Gluid",
                    SupplierCode = "GLUID-9776",
                    City = "Macdona",
                    Address1 = "830 Lefferts Place",
                    Address2 = "398 Fenimore Street, Geyserville, Massachusetts, 9745",
                    Phone1 = "+1 (835) 549-2098",
                    Phone2 = "+1 (869) 531-2076",
                    Notes = "Id mollit exercitation cupidatat incididunt elit sint adipisicing elit. Est Lorem deserunt Lorem Lorem occaecat cupidatat exercitation minim sunt non exercitation aliqua amet anim. Laboris id ut adipisicing minim occaecat magna adipisicing do officia ullamco irure ipsum voluptate. Aliqua reprehenderit commodo commodo officia dolor do ad nulla. Nostrud consectetur ut laborum pariatur minim. Eiusmod nisi sint eiusmod cupidatat do laboris ea ipsum eiusmod qui amet nostrud labore.",
                    Contacts = {
                        new Contact() { FirstName = "Isabel", LastName = "Mcintyre", EmailWork = "isabel.mcintyre@panzent.net" },
                }
              },
             new Supplier() {
                    CompanyName = "Vertide",
                    SupplierCode = "VERTIDE-1627",
                    City = "Windsor",
                    Address1 = "707 Greenpoint Avenue",
                    Address2 = "734 Beach Place, Reno, District Of Columbia, 9328",
                    Phone1 = "+1 (968) 512-2627",
                    Phone2 = "+1 (910) 485-3403",
                    Notes = "Adipisicing dolor id irure aliquip veniam exercitation. Irure pariatur veniam ex deserunt laboris eiusmod id velit dolore. Ipsum ipsum elit culpa dolor anim reprehenderit pariatur ex ea culpa fugiat. Sunt anim sit ipsum occaecat ad proident consectetur deserunt. Sunt Lorem dolor dolore quis quis enim occaecat. Quis consequat sit pariatur ut aliqua nostrud magna. Dolore enim non anim irure ipsum eu officia aliqua velit cillum et ullamco tempor nostrud.",
                    Contacts = {
                        new Contact() { FirstName = "Caldwell", LastName = "Washington", EmailWork = "caldwell.washington@ovolo.io" },
                }
              },
             new Supplier() {
                    CompanyName = "Suretech",
                    SupplierCode = "SURETECH-7037",
                    City = "Saddlebrooke",
                    Address1 = "969 Victor Road",
                    Address2 = "923 Hamilton Walk, Darbydale, Florida, 8688",
                    Phone1 = "+1 (849) 499-3627",
                    Phone2 = "+1 (869) 451-3306",
                    Notes = "Incididunt anim reprehenderit eu excepteur. Irure fugiat non et ipsum aute consectetur irure qui esse laborum ipsum fugiat irure aliquip. Enim velit quis ad nisi. Duis deserunt voluptate aliquip proident.",
                    Contacts = {
                        new Contact() { FirstName = "Diane", LastName = "Skinner", EmailWork = "diane.skinner@geofarm.me" },
                }
              },
             new Supplier() {
                    CompanyName = "Geoform",
                    SupplierCode = "GEOFORM-4979",
                    City = "Kennedyville",
                    Address1 = "254 Estate Road",
                    Address2 = "164 Delevan Street, Ola, Michigan, 151",
                    Phone1 = "+1 (847) 441-3310",
                    Phone2 = "+1 (874) 590-2937",
                    Notes = "Ex veniam commodo voluptate occaecat irure anim elit eiusmod. Voluptate nisi sint Lorem non enim eiusmod. Est cupidatat veniam ex ea incididunt nisi ex laboris aliquip eu eiusmod. Id consequat consectetur ut culpa excepteur.",
                    Contacts = {
                        new Contact() { FirstName = "Albert", LastName = "Velasquez", EmailWork = "albert.velasquez@webiotic.biz" }
                }
              },
             new Supplier() {
                    CompanyName = "Ontality",
                    SupplierCode = "ONTALITY-1766",
                    City = "Marshall",
                    Address1 = "889 Nolans Lane",
                    Address2 = "288 Wortman Avenue, Manila, Colorado, 2649",
                    Phone1 = "+1 (992) 441-2651",
                    Phone2 = "+1 (902) 446-2763",
                    Notes = "Ex consectetur enim occaecat dolore ipsum sit eiusmod incididunt. Proident ex ex mollit adipisicing fugiat labore et ipsum dolore magna commodo quis. Proident excepteur amet elit amet irure labore labore."
              },
             new Supplier() {
                    CompanyName = "Comtent",
                    SupplierCode = "COMTENT-2906",
                    City = "Outlook",
                    Address1 = "430 Vandam Street",
                    Address2 = "163 Sullivan Place, Henrietta, Arkansas, 8680",
                    Phone1 = "+1 (874) 446-2484",
                    Phone2 = "+1 (994) 574-3628",
                    Notes = "Aliqua qui reprehenderit pariatur excepteur sint ex dolore sint ipsum ut aliquip ad. Id id irure nulla incididunt officia consequat minim pariatur deserunt sint ea aliqua tempor. Veniam eiusmod minim fugiat labore mollit ad sit non aliqua. Occaecat eu magna est esse quis ullamco amet ullamco. Officia nostrud voluptate tempor sint minim occaecat id duis dolore dolor quis ullamco Lorem magna. Ad voluptate ea Lorem dolor nisi occaecat cupidatat aute duis voluptate occaecat consequat incididunt eiusmod."
              },
             new Supplier() {
                    CompanyName = "Solgan",
                    SupplierCode = "SOLGAN-6543",
                    City = "Dola",
                    Address1 = "921 Hawthorne Street",
                    Address2 = "455 Wolcott Street, Coyote, New Mexico, 7035",
                    Phone1 = "+1 (840) 567-2790",
                    Phone2 = "+1 (848) 532-2094",
                    Notes = "Occaecat ut laborum exercitation exercitation. Est qui incididunt eu deserunt cillum aute aliqua. Duis dolore excepteur adipisicing Lorem anim labore culpa voluptate aliqua. Enim laborum proident est occaecat dolor nulla occaecat sunt do."
              },
             new Supplier() {
                    CompanyName = "Columella",
                    SupplierCode = "COLUMELLA-693",
                    City = "Gibsonia",
                    Address1 = "378 Williams Place",
                    Address2 = "759 Verona Street, Riner, Hawaii, 4585",
                    Phone1 = "+1 (809) 573-2396",
                    Phone2 = "+1 (912) 466-2597",
                    Notes = "Ad ea culpa veniam adipisicing labore voluptate aliqua cillum velit exercitation elit. Reprehenderit consequat cillum cillum est occaecat. Eiusmod sunt eiusmod culpa proident et consectetur incididunt consectetur est laborum quis. Consequat qui veniam velit fugiat aliqua culpa adipisicing duis excepteur qui occaecat ad. Qui sint voluptate irure ad dolore elit enim nulla."
              },
             new Supplier() {
                    CompanyName = "Hometown",
                    SupplierCode = "HOMETOWN-8865",
                    City = "Malo",
                    Address1 = "184 Gelston Avenue",
                    Address2 = "705 Legion Street, Chesterfield, Idaho, 2924",
                    Phone1 = "+1 (923) 459-2131",
                    Phone2 = "+1 (908) 540-2257",
                    Notes = "Amet sint eiusmod deserunt eiusmod pariatur aute dolor. Magna ullamco culpa pariatur do nisi fugiat id aliqua amet id anim mollit velit. Adipisicing reprehenderit labore deserunt nisi exercitation anim occaecat est eiusmod minim aliqua. Dolore consequat irure dolor dolor. Velit enim ad amet consequat ipsum est enim excepteur cillum. Aliqua non aliquip est nisi consectetur eiusmod voluptate laborum. Laborum exercitation cillum ea ullamco incididunt nostrud."
              },
             new Supplier() {
                    CompanyName = "Ronbert",
                    SupplierCode = "RONBERT-3095",
                    City = "Walker",
                    Address1 = "815 Polhemus Place",
                    Address2 = "805 Vandervoort Place, Cliffside, Louisiana, 4546",
                    Phone1 = "+1 (811) 515-3041",
                    Phone2 = "+1 (840) 485-2220",
                    Notes = "Ea reprehenderit mollit anim anim eu pariatur ad cupidatat minim culpa magna. Qui dolor non irure aliquip irure elit est et et deserunt voluptate exercitation commodo enim. Enim do occaecat amet consequat fugiat id quis sunt. Occaecat minim id ipsum reprehenderit officia anim labore dolore sint. Ullamco sit veniam ullamco cupidatat duis adipisicing nostrud officia aute."
              },
             new Supplier() {
                    CompanyName = "Ecrater",
                    SupplierCode = "ECRATER-8693",
                    City = "Knowlton",
                    Address1 = "698 Powers Street",
                    Address2 = "957 Albemarle Terrace, Matheny, Washington, 9454",
                    Phone1 = "+1 (937) 404-2295",
                    Phone2 = "+1 (818) 442-2328",
                    Notes = "Incididunt tempor non reprehenderit ex. Ut veniam deserunt dolor dolore commodo. Esse exercitation excepteur laborum occaecat dolore minim enim minim commodo eu. In cupidatat do ut veniam voluptate sint non officia laboris. Ea non esse et cupidatat ipsum ea."
              },
             new Supplier() {
                    CompanyName = "Zilidium",
                    SupplierCode = "ZILIDIUM-7035",
                    City = "Lumberton",
                    Address1 = "492 Martense Street",
                    Address2 = "292 Prescott Place, Grahamtown, Indiana, 1521",
                    Phone1 = "+1 (902) 444-3482",
                    Phone2 = "+1 (841) 519-3090",
                    Notes = "Sunt excepteur est sit cillum mollit ex eu adipisicing mollit laboris voluptate. Proident et Lorem enim aute incididunt in reprehenderit cupidatat in occaecat esse adipisicing consequat. Amet cupidatat deserunt exercitation ipsum nulla commodo. Aute culpa tempor eu occaecat aute qui tempor dolor deserunt est est culpa incididunt. In proident voluptate aliqua irure velit consectetur pariatur non ad consectetur aliqua officia amet esse. Proident deserunt consectetur est ipsum nostrud amet et. Fugiat reprehenderit tempor ipsum do cupidatat mollit aliqua aute duis esse Lorem."
             },
             new Supplier() {
                    CompanyName = "Rotodyne",
                    SupplierCode = "ROTODYNE-6766",
                    City = "Rowe",
                    Address1 = "275 Ferry Place",
                    Address2 = "246 Barbey Street, Dotsero, North Dakota, 5570",
                    Phone1 = "+1 (867) 587-3500",
                    Phone2 = "+1 (944) 501-3533",
                    Notes = "Occaecat minim excepteur ea consequat proident elit aliqua aliquip incididunt aliquip. Sit ad ex adipisicing anim. Ea non ex officia eu cupidatat deserunt eu ad reprehenderit aliqua ea velit. Id aliquip quis mollit proident ea laborum dolor sunt eiusmod incididunt velit laborum Lorem. Nulla excepteur dolor veniam ipsum labore quis velit dolor est nisi voluptate sint Lorem. Reprehenderit nisi minim aute in officia laboris ex ipsum magna aute id et nostrud eiusmod. Laboris ullamco laborum dolore amet excepteur qui qui tempor eu aliqua voluptate nisi qui."
             },
             new Supplier() {
                    CompanyName = "Zillanet",
                    SupplierCode = "ZILLANET-1908",
                    City = "Sparkill",
                    Address1 = "921 Miami Court",
                    Address2 = "603 Bryant Street, Churchill, Nebraska, 6871",
                    Phone1 = "+1 (857) 572-3314",
                    Phone2 = "+1 (951) 514-3485",
                    Notes = "Consectetur occaecat consequat nulla cillum deserunt eiusmod aliquip ex sint enim esse est anim. In tempor non anim consectetur. Aute est velit mollit aliquip enim magna consectetur nulla laboris magna ullamco aliquip. Anim amet deserunt officia cillum elit deserunt amet esse commodo."
              },
             new Supplier() {
                    CompanyName = "Geekology",
                    SupplierCode = "GEEKOLOGY-2576",
                    City = "Idledale",
                    Address1 = "718 Granite Street",
                    Address2 = "968 Driggs Avenue, Bartonsville, Iowa, 5241",
                    Phone1 = "+1 (954) 521-2928",
                    Phone2 = "+1 (813) 430-3234",
                    Notes = "Esse duis nostrud laborum aliquip laborum commodo ad aliqua est pariatur quis eu reprehenderit. Nulla anim sit adipisicing laborum reprehenderit minim duis ipsum in velit proident exercitation aliquip pariatur. Id ut elit reprehenderit in et ut et cupidatat irure commodo laborum. Et officia labore occaecat quis velit dolore laboris sint adipisicing veniam et occaecat nulla duis. Velit id voluptate fugiat est non ea. In qui Lorem esse occaecat qui laboris reprehenderit culpa excepteur incididunt deserunt culpa."
              },
             new Supplier() {
                    CompanyName = "Mitroc",
                    SupplierCode = "MITROC-5899",
                    City = "Boomer",
                    Address1 = "683 Monitor Street",
                    Address2 = "605 Concord Street, Dexter, Texas, 1711",
                    Phone1 = "+1 (995) 521-3482",
                    Phone2 = "+1 (973) 549-2579",
                    Notes = "Quis laboris pariatur in velit minim in fugiat reprehenderit aliqua duis. Elit elit deserunt cupidatat eiusmod occaecat aliquip tempor aliquip proident. Aliquip adipisicing sint tempor dolor laboris officia labore qui irure labore veniam ullamco dolore voluptate. Cupidatat pariatur ea Lorem labore est in excepteur laborum aute dolore mollit excepteur. Aliquip sit sit sunt consectetur proident enim officia deserunt."
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

            var suppliers = await (from s in DataContext.Companies.OfType<Supplier>()
                                   select s)
                                   .ToArrayAsync();

            int countCatg = 0;
            ProductCategory catg1 = new ProductCategory() { CategoryName = "Kadın Giyim" };
            countCatg = AddCategory(catg1, catgKadin, suppliers, sb, countCatg);

            ProductCategory catg2 = new ProductCategory() { CategoryName = "Erkek Giyim" };
            DataContext.ProductCategories.Add(catg2);
            countCatg = AddCategory(catg2, catgErkek, suppliers, sb, countCatg);

            ProductCategory catg3 = new ProductCategory() { CategoryName = "Çocuk Giyim" };
            DataContext.ProductCategories.Add(catg3);
            countCatg = AddCategory(catg3, catgCocuk, suppliers, sb, countCatg);
        }
        int suppliersAdded = 0;

        private int AddCategory(ProductCategory parentCatg, string[] subCategories, Supplier[] suppliers,
            StringBuilder sb, int countCatg)
        {
            DataContext.ProductCategories.Add(parentCatg);
            sb.Append("Category ");
            sb.Append(parentCatg.CategoryName);
            sb.Append(" added<br>");
            foreach (var item in subCategories)
            {
                var catg = new ProductCategory()
                {
                    CategoryName = item,
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
                    int addSupplier = rnd.Next(2, 5);
                    var prod = new Product()
                    {
                        Category = catg,
                    };
                    for (int j = 0; j < wordCount; j++)
                    {
                        if (string.IsNullOrEmpty(prod.Description))
                        {
                            prod.Description = lips[rnd.Next(0, lips.Length - 1)].ToSentenceCase();
                            prod.ProductCode = lips[rnd.Next(0, lips.Length - 1)].ToUpperInvariant() + "-" + rnd.Next(1999, 9999).ToString();
                        }
                        else
                        {
                            prod.Description = prod.Description + " " + lips[rnd.Next(0, lips.Length - 1)].ToSentenceCase();
                        }
                    }
                    for (int k = 0; k < addSupplier; k++)
                    {
                        prod.Suppliers.Add(suppliers[suppliersAdded % suppliers.Length]);
                        suppliersAdded++;
                    }
                    DataContext.Products.Add(prod);
                }
            }
            sb.Append(countCatg);
            sb.Append(" sub categies added<br>");
            return countCatg;
        }

    }
}