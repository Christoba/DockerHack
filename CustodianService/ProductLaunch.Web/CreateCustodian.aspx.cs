using kCura.Hack.Data;
using kCura.Hack.Messaging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProductLaunch.Web
{
    public partial class CreateCustodian : Page
    {
        private static Dictionary<string, Country> _Countries;
        private static Dictionary<string, CustodianType> _Types;

        public static void PreloadStaticDataCache()
        {
            WebHelper.LogService.LogInfo("Preloading data cache.");

            _Countries = new Dictionary<string, Country>();
            _Types = new Dictionary<string, CustodianType>();

            _Countries = new Dictionary<string, Country>()
                             {
                                 {
                                     "United States",
                                     new Country()
                                         {
                                             CountryCode = "US",
                                             CountryName = "United States"
                                         }
                                 },
                                 {
                                     "Canada",
                                     new Country()
                                         {
                                             CountryCode = "CA",
                                             CountryName = "Canada"
                                         }
                                 },
                                 {
                                     "Mexico",
                                     new Country()
                                         {
                                             CountryCode = "MX",
                                             CountryName = "Mexico"
                                         }
                                }
                             };

            _Types = new Dictionary<string, CustodianType>()
                         {
                             {
                                 "Person",
                                 new CustodianType() { TypeCode = "P", TypeName = "Person" }
                             },
                             {
                                 "Entity",
                                 new CustodianType() { TypeCode = "E", TypeName = "Entity" }
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
            ddlCustType.Items.Clear();
            ddlCustType.Items.AddRange(_Types.Select(x => new ListItem(x.Value.TypeName, x.Key)).ToArray()); 
        }

        private void PopulateCountries()
        {
            ddlCountry.Items.Clear();
            ddlCountry.Items.AddRange(_Countries.Select(x => new ListItem(x.Value.CountryName, x.Key)).ToArray());
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            var country = _Countries[ddlCountry.SelectedValue];
            var custType = _Types[ddlCustType.SelectedValue];

            var prospect = new Custodian
            {
                CompanyName = txtCompanyName.Text,
                EmailAddress = txtEmail.Text,
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                Country = country,
                CustodianType = custType
            };

            this.SavePublish(prospect);

            Server.Transfer("Success.aspx");
        }

        private void SavePublish(Custodian custodian)
        {
            var eventMessage = new CustodianCreatedEvent
                                   {
                                       Custodian = custodian,
                                       CreatedAt = DateTime.Now
                                   };

            WebHelper.LogService.LogInfo("Publishing new custodian {0} at {1}", eventMessage.Custodian, eventMessage.CreatedAt);

            MessageQueue.Publish(eventMessage);
        }
    }
}