﻿using System;
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

        public ContactData GetContactInformationFromTable(int index)
        {

            manager.Navigator.GotoHomePage();
            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"));
            string lastName = cells[1].Text;
            string firstName = cells[2].Text;
            string address = cells[3].Text;
            string allEmails = cells[4].Text;
            string allPhones = cells[5].Text;
            return new ContactData(firstName)
            {
                Lastname = lastName,
                Address = address,
                AllPhones = allPhones,
                AllEmails = allEmails
            };
        }

        public ContactData GetContactInformationFromPage(int index)
        {
            manager.Navigator.GotoHomePage();
            OpenContactInformation(index);

            string fullContactInfo = driver.FindElement(By.Id("content")).Text;
            //string fullName = driver.FindElement(By.TagName("br")).Text;
            //string middleName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            //string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");

            return new ContactData("")
            {
                FullContactInfo = fullContactInfo
            };
        }


        public ContactData GetContactInformatiomFromEditForm(int index)
        {
            manager.Navigator.GotoHomePage();
            InitContactModification(index);
            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string middleName = driver.FindElement(By.Name("middlename")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string nickName = driver.FindElement(By.Name("nickname")).GetAttribute("value");

            string company = driver.FindElement(By.Name("company")).GetAttribute("value");
            string title = driver.FindElement(By.Name("title")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");
            string secondAddress = driver.FindElement(By.Name("address2")).GetAttribute("value");

            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");
            string secondPhone = driver.FindElement(By.Name("phone2")).GetAttribute("value");
            string Fax = driver.FindElement(By.Name("fax")).GetAttribute("value");


            string email1 = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");
            string homePage = driver.FindElement(By.Name("homepage")).GetAttribute("value");

            string bDay = driver.FindElement(By.XPath(@"//select[@name='bday']//option[@selected='selected']")).Text;
            string bMonth = driver.FindElement(By.XPath(@"//select[@name='bmonth']//option[@selected='selected']")).Text;
            string bYear = driver.FindElement(By.Name("byear")).GetAttribute("value");

            string aDay = driver.FindElement(By.XPath(@"//select[@name='aday']//option[@selected='selected']")).Text;
            string aMonth = driver.FindElement(By.XPath(@"//select[@name='amonth']//option[@selected='selected']")).Text;
            string aYear = driver.FindElement(By.Name("ayear")).GetAttribute("value");

            string notes = driver.FindElement(By.Name("notes")).Text;

            return new ContactData(firstName)
            {
                Lastname = lastName,
                Middlename = middleName,
                Nickname = nickName,

                SecondAddress = secondAddress,
                Address = address,
                Company = company,
                Title = title,

                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone,
                SecondPhone = secondPhone,
                Fax= Fax,

                Email1 = email1,
                Email2 = email2,
                Email3 = email3, 
                Homepage = homePage,

                BDay = bDay,
                BMonth = bMonth,
                BYear = bYear,

                ADay = aDay,
                AMonth = aMonth,
                AYear = aYear,

                Notes = notes
            };
            //throw new NotImplementedException();
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

            //driver.FindElement(By.XPath("(//img[@alt='Edit'])[" + (index+1) + "]")).Click();
            driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"))[7]
                .FindElement(By.TagName("a")).Click();
            return this;
        }

        public ContactHelper OpenContactInformation(int index)
        {
            driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"))[6]
                .FindElement(By.TagName("a")).Click();
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
