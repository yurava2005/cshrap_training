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
            PrepareContact();
            InitContactModification(index);
            FillContactForm(newContact);
            SubmitContactModification();
            return this;
        }


        public ContactHelper Remove(int index)
        {
            manager.Navigator.GotoHomePage();
            PrepareContact();
            SelectContact(index);
            RemoveContact();
            return this;
        }

        private ContactHelper PrepareContact()
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
            return this;
        }

        public ContactHelper SelectContact(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + index + " ]")).Click();
            return this;
        }

        public ContactHelper RemoveContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            driver.SwitchTo().Alert().Accept();
            return this;
        }

        public ContactHelper InitContactModification(int index)
        {

            driver.FindElement(By.XPath("(//img[@alt='Edit'])[" + index + "]")).Click();
            return this;
        }

        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            return this;
        }


    }

}
