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
            ContactData fromTable = app.Contacts.GetContactInformationFromTable(0);
            ContactData fromForm = app.Contacts.GetContactInformatiomFromEditForm(0);

            //verification
            Assert.AreEqual(fromTable, fromForm);
            Assert.AreEqual(fromTable.Address, fromForm.Address);
            Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
            Assert.AreEqual(fromTable.AllEmails, fromForm.AllEmails);

        }


        [Test]
        public void TestContactPageInformation()
        {
            ContactData fromPage = app.Contacts.GetContactInformationFromPage(0);
            ContactData fromForm = app.Contacts.GetContactInformatiomFromEditForm(0);

            //verification
            Assert.AreEqual(fromPage.FullName, fromForm.FullName);
            //Assert.AreEqual(fromPage.Address, fromForm.Address);
            //Assert.AreEqual(fromPage.AllPagePhones, fromForm.AllPagePhones);
            //Assert.AreEqual(fromPage.AllEmails, fromForm.AllEmails);
        }

    }
}
