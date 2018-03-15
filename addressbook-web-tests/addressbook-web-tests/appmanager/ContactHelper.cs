using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager manager)
            : base (manager)
        {
        }

        public ContactHelper Create(ContactData contact)
        {
            manager.Navigator.GotoHomePage();
            InitContactCreation();
            FillContactForm(contact);
            SubmitContactCreation();
            manager.Navigator.ReturnToHomePage();
            return this;
        }


        public ContactHelper Modify(int index, ContactData newContact)
        {
            manager.Navigator.GotoHomePage();
            InitContactModification(index);
            FillContactForm(newContact);
            SubmitContactModification();
            manager.Navigator.GotoHomePage();
            return this;
        }


        public ContactHelper Remove(int index)
        {
            manager.Navigator.GotoHomePage();
            SelectContact(index);
            RemoveContact();
            manager.Navigator.GotoHomePage();
            return this;
        }

        public ContactHelper PrepareContact()
        {
            if (! IsElementPresent(By.Name("entry")))
            {
                ContactData cnt = new ContactData("special_contact_name");
                Create(cnt);
            }
            return this;
        }

        public ContactHelper InitContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }


        public ContactHelper FillContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.Firstname);
            Type(By.Name("middlename"), contact.Middlename);
            Type(By.Name("lastname"), contact.Lastname);
            return this;
        }
        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper SelectContact(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index+1) + " ]")).Click();
            return this;
        }

        public ContactHelper RemoveContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            driver.SwitchTo().Alert().Accept();
            contactCache = null;
            return this;
        }

        public ContactHelper InitContactModification(int index)
        {

            driver.FindElement(By.XPath("(//img[@alt='Edit'])[" + (index+1) + "]")).Click();
            return this;
        }

        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            contactCache = null;
            return this;
        }


        private List<ContactData> contactCache = null;

        public List<ContactData> GetContactList()
        {
            if (contactCache == null)
            {
                contactCache = new List<ContactData>();
                manager.Navigator.GotoHomePage();
                ICollection<IWebElement> elements = driver.FindElements(By.Name("entry"));
                foreach (IWebElement element in elements)
                {
                    ICollection<IWebElement> cells = element.FindElements(By.TagName("td"));
                    contactCache.Add(new ContactData(cells.ElementAt(2).Text)
                    {
                        Id = element.FindElement(By.TagName("input")).GetAttribute("value"),
                        Lastname = cells.ElementAt(1).Text
                    });
                }
            }

            return new List<ContactData>(contactCache);
        }

        public int GetContactCount()
        {

            return driver.FindElements(By.Name("entry")).Count();
        }

    }

}
