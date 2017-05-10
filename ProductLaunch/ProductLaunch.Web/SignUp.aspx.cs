using ProductLaunch.Entities;
using ProductLaunch.Messaging;
using ProductLaunch.Messaging.Messages.Events;
using ProductLaunch.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProductLaunch.Web
{
    using kCura.Hack.Logging;

    public partial class SignUp : Page
    {
        private static Dictionary<string, Country> _Countries;
        private static Dictionary<string, Role> _Roles;

        private static ILogService logService = null;

        private static ILogService LogService
        {
            get
            {
                if (logService == null)
                {
                    logService = LogServiceFactory.CreateLogService();
                }

                return logService;
            }
        }

        public static void PreloadStaticDataCache()
        {
            LogService.LogInfo("Preloading data cache.");

            _Countries = new Dictionary<string, Country>();
            _Roles = new Dictionary<string, Role>();
            ////using (var context = new ProductLaunchContext())
            ////{
            ////    foreach (var country in context.Countries.OrderBy(x => x.CountryName))
            ////    {
            ////        _Countries[country.CountryCode] = country;
            ////    }
            ////    foreach (var role in context.Roles.OrderBy(x => x.RoleName))
            ////    {
            ////        _Roles[role.RoleCode] = role;
            ////    }
            ////}

            _Countries = new Dictionary<string, Country>()
                             {
                                 {
                                     "United States",
                                     new Country()
                                         {
                                             CountryCode = "US",
                                             CountryName = "United States"
                                         }
                                 }
                             };

            _Roles = new Dictionary<string, Role>()
                         {
                             {
                                 "Developer",
                                 new Role() { RoleCode = "Dev", RoleName = "Developer" }
                             }
                         };
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                PopulateRoles();
                PopulateCountries();
            }
        }

        private void PopulateRoles()
        {
            ddlRole.Items.Clear();
            ddlRole.Items.AddRange(_Roles.Select(x => new ListItem(x.Value.RoleName, x.Key)).ToArray()); 
        }

        private void PopulateCountries()
        {
            ddlCountry.Items.Clear();
            ddlCountry.Items.AddRange(_Countries.Select(x => new ListItem(x.Value.CountryName, x.Key)).ToArray());
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            var country = _Countries[ddlCountry.SelectedValue];
            var role = _Roles[ddlRole.SelectedValue];

            var prospect = new Prospect
            {
                CompanyName = txtCompanyName.Text,
                EmailAddress = txtEmail.Text,
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                Country = country,
                Role = role
            };

            this.SavePublish(prospect);

            Server.Transfer("ThankYou.aspx");
        }

        private void SavePublish(Prospect prospect)
        {
            var eventMessage = new ProspectSignedUpEvent
                                   {
                                       Prospect = prospect,
                                       SignedUpAt = DateTime.UtcNow
                                   };

            LogService.LogInfo("Publishing new prospect {0}", prospect);

            MessageQueue.Publish(eventMessage);
        }
    }
}