using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModificationTests : TestBase
    {
        [Test]

        public void GroupModificationTest()
        {
            GroupData newData = new GroupData("group_modified_name");
            newData.Header = "group_modified_header";
            newData.Footer = "group_modified_footer";

            app.Groups.Modify(1, newData);
        }
    }

}
