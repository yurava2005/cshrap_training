using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;


namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCteationTests : TestBase
    {

              [Test]
        public void ContactCreationTest()
        {
            GotoHomePage();
            Login(new AccountData("admin", "secret"));
            InitContactCreation();
            ContactData contact = new ContactData("first_name");
            contact.Middlename = "middle_name";
            contact.Lastname = "last_name";
            FillContactForm(contact);
            SubmitContactCreation();
            ReturnToHomePage();
        }

    }
}
