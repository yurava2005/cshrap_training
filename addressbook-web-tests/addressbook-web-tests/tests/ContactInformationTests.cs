using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactInformationTests : AuthTestBase
    {

        [Test]
        public void TestContactInformation()
        {
            ContactData fromTable = app.Contacts.GetContactInformationFromTable(1);
            ContactData fromForm = app.Contacts.GetContactInformatiomFromEditForm(1);

            //verification
            Assert.AreEqual(fromTable, fromForm);
            Assert.AreEqual(fromTable.CleanedAddress, fromForm.CleanedAddress);
            Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
            Assert.AreEqual(fromTable.AllEmails, fromForm.AllEmails);

        }


        [Test]
        public void TestContactPageInformation()
        {
            ContactData fromPage = app.Contacts.GetContactInformationFromPage(1);
            ContactData fromForm = app.Contacts.GetContactInformatiomFromEditForm(1);

            //verification
            Assert.AreEqual(fromPage.FullContactInfo, fromForm.FullContactInfo);

        }

    }
}
