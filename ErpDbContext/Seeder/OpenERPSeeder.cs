using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using OpenERP.ErpDbContext.DataModel;

namespace OpenERP.ErpDbContext.DataModel
{

    public class OpenERPSeeder
    {
        private readonly OpenERPContext _ctx;
        private readonly IWebHostEnvironment _hosting;
        private readonly UserManager<User> _userManager;

        public OpenERPSeeder(OpenERPContext ctx,
          IWebHostEnvironment hosting,
          UserManager<User> userManager)
        {
            _ctx = ctx;
            _hosting = hosting;
            _userManager = userManager;
        }

        public async Task SeedAsync()
        {
            _ctx.Database.EnsureCreated();

            CreateCompany("Test");
            CreateUOM("Test", "EA");

            User user = await _userManager.FindByEmailAsync("manager@openerp.com");

            if (user == null)
            {
                user = new User();
                user.UserName = "manager@openerp.com";
                user.Email = "manager@openerp.com";
                user.FirstName = "manager";
                user.LastName = "manager";
                user.CompanyList = "TEST01";
            }

            var result = await _userManager.CreateAsync(user, "T0A55um3#");
            if (result != IdentityResult.Success)
            {
                throw new InvalidOperationException("Could not create new user");
            }

        }

        private void CreateCompany(string CompanyID)
        {

            Company company = new Company()
            {
                CompanyId = CompanyID,
                Name = CompanyID,
                Active = true
            };

            _ctx.Companies.Add(company);
            _ctx.SaveChanges();

        }

        private void CreateUOM(string CompanyID, string uom)
        {

            Uom uomRow = new Uom()
            {
                CompanyId = CompanyID,
                Uom1 = uom,
                Uomdescription = uom,
                Active = true
            };

            _ctx.Uoms.Add(uomRow);
            _ctx.SaveChanges();

        }
    }
}