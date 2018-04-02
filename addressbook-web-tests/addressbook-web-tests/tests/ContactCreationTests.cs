using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;
using NUnit.Framework;


namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCteationTests : AuthTestBase
    {


        public static IEnumerable<ContactData> RandomContactDataProvider()
        {
            List<ContactData> contacts = new List<ContactData>();
            for (int i = 0; i < 5; i++)
            {
                contacts.Add(new ContactData(GenerateRandomString(30))
                {
                    Firstname = GenerateRandomString(100),
                    Lastname = GenerateRandomString(100),
                    Middlename = GenerateRandomString(100)
                });
            }
            return contacts;
        }

        public static IEnumerable<ContactData> ContactDataFromXmlFile()
        {
            List<ContactData> contacts = new List<ContactData>();
            return (List<ContactData>)
                new XmlSerializer(typeof(List<ContactData>))
                .Deserialize(new StreamReader(@"contacts.xml"));
        }


        public static IEnumerable<ContactData> ContactDataFromJsonFile()
        {

            return JsonConvert.DeserializeObject<List<ContactData>>(
                File.ReadAllText(@"contacts.json"));

        }



        [Test, TestCaseSource("ContactDataFromJsonFile")]
        public void ContactCreationTest(ContactData contact)
        {

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