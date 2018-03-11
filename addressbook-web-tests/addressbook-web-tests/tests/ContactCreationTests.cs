using System.Collections.Generic;
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

            List<ContactData> oldContacts = app.Contacts.GetContactList();
            app.Contacts.Create(contact);
            List<ContactData> newContacts = app.Contacts.GetContactList();
            oldContacts.Add(contact);

            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }

    }
}