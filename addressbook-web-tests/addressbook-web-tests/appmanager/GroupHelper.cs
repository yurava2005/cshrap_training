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
    public class GroupHelper : HelperBase
    {

        public GroupHelper(ApplicationManager manager) 
            : base (manager)
        {
        }

        public GroupHelper Create(GroupData group)
        {
            manager.Navigator.GotoGroupsPage();
            InitGroupCreation();
            FillGroupForm(group);
            SubmitGroupCreation();
            manager.Navigator.ReturnToGroupsPage();
            return this;
        }

        public GroupHelper Modify(int index, GroupData newData)
        {
            manager.Navigator.GotoGroupsPage();
            PrepareGroup();
            SelectGroup(index);
            InitGroupModification();
            FillGroupForm(newData);
            SubmitGroupModification();
            manager.Navigator.ReturnToGroupsPage();
            return this;
        }


        public GroupHelper Remove(int index)
        {
            manager.Navigator.GotoGroupsPage();
            PrepareGroup();
            SelectGroup(index);
            RemoveGroup();
            manager.Navigator.ReturnToGroupsPage();
            return this;
        }

        private GroupHelper PrepareGroup()
        {
            if (! IsElementPresent(By.Name("selected[]")))
            {
                GroupData grp = new GroupData("special_group_name");
                Create(grp);
            }
            return this;
        }



        public GroupHelper InitGroupCreation()
        {
            driver.FindElement(By.Name("new")).Click();
            return this;
        }

        public GroupHelper FillGroupForm(GroupData group)
        {
            Type(By.Name("group_name"), group.Name);
            Type(By.Name("group_header"), group.Header);
            Type(By.Name("group_footer"), group.Footer);
            return this;
        }


        public GroupHelper SubmitGroupCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            return this;
        }

        public GroupHelper SelectGroup(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + index + " ]")).Click();
            return this;
        }

        public GroupHelper RemoveGroup()
        {
            driver.FindElement(By.Name("delete")).Click();
            return this;
        }

        public GroupHelper InitGroupModification()
        {
            driver.FindElement(By.Name("edit")).Click();
            return this;
        }

        public GroupHelper SubmitGroupModification()
        {
            driver.FindElement(By.Name("update")).Click();
            return this;
        }

    }
}
