using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : TestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            ContactData newContact = new ContactData("new_first_name");
            newContact.Middlename = "new_middle_name";
            newContact.Lastname = "new_last_name";
            app.Contacts.Modify(1, newContact);
        }
    }
}
