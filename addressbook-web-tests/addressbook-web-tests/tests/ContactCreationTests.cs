using NUnit.Framework;


namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCteationTests : TestBase
    {

        [Test]
        public void ContactCreationTest()
        {
            app.Navigator.GotoHomePage();
            app.Auth.Login(new AccountData("admin", "secret"));
            app.Contacts.InitContactCreation();
            ContactData contact = new ContactData("first_name");
            contact.Middlename = "middle_name";
            contact.Lastname = "last_name";
            app.Contacts.FillContactForm(contact);
            app.Contacts.SubmitContactCreation();
            app.Navigator.ReturnToHomePage();
        }
    }
}
