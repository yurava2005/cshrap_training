using NUnit.Framework;


namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCteationTests : AuthTestBase
    {

        [Test]
        public void ContactCreationTest()
        {
            ContactData contact = new ContactData("first_name");
            contact.Middlename = "middle_name";
            contact.Lastname = "last_name";
            app.Contacts.Create(contact);
        }
    }
}