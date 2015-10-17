using CriticalPath.Data;
using CriticalPath.Web.Models;
using OzzIdentity.Controllers;
using OzzUtils;
using System;
using System.Text;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace CriticalPath.Web.Controllers
{
    public class AdmPanelController : AbstractAdminController
    {
        [Authorize]
        public override ActionResult Index()
        {
            return Content("AdmPanel");
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
        public async Task<ActionResult> SeedProducts()
        {
            string[] catgKadin = {
                "Etek",
                "Şort",
                "Bluz / Tshirt",
                "Elbise",
                "Pantolon / Tayt",
                "Hırka / Kazak",
                "Ceket / Kaban",
                "Büyük Beden",
                "Sweatshirt",
                "Eşofman",
                "Tulum"};
            string[] catgErkek = {
                "Şort",
                "T-Shirt",
                "Pantolon",
                "Denim Pantolon",
                "Gömlek",
                "Takım Elbise",
                "Mayo Şort",
                "Sweatshirt",
                "Hırka / Kazak",
                "Ceket / Kaban",
                "Yelek",
                "Eşofman"};
            string[] catgCocuk = {
                "Çocuk T-Shirt",
                "Çocuk Body",
                "Çocuk Elbise",
                "Çocuk Etek",
                "Çocuk Pantolon",
                "Çocuk Şort",
                "Çocuk Gömlek",
                "Çocuk Mont",
                "Çocuk Hırka",
                "Çocuk Kazak",
                "Çocuk Eşofman",
                "Çocuk Tulum",
                "Çocuk Hastane Çıkışı",
                "Çocuk Tayt",
                "Çocuk Deniz Şortu"};


            var sb = new StringBuilder();
            int countCatg = 0;
            ProductCategory catg1 = new ProductCategory() { Title = "Kadın Giyim" };
            countCatg = AddCategory(catg1, catgKadin, sb);

            ProductCategory catg2 = new ProductCategory() { Title = "Erkek Giyim" };
            DataContext.ProductCategories.Add(catg2);
            countCatg = AddCategory(catg2, catgErkek, sb);

            ProductCategory catg3 = new ProductCategory() { Title = "Çocuk Giyim" };
            DataContext.ProductCategories.Add(catg3);
            countCatg = AddCategory(catg3, catgCocuk, sb);

            await DataContext.SaveChangesAsync(this);


            return Content(sb.ToString());
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
                int countProduct = rnd.Next(5, 15);
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