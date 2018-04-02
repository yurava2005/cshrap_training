using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace WebAddressbookTests

{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;
        private string allEmails;
        private string allInfo;
        private string cleanedAddress;

        public ContactData()
        {

        }

        public ContactData(string firstname)
        {
            Firstname = firstname;
        }

        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string WorkPhone { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public string Email3 { get; set; }
        public string Id { get; set; }
        public string Nickname { get; set; }
        public string Fax { get; set; }
        public string Homepage { get; set; }
        public string Birthday { get; set; }
        public string Annivesary { get; set; }
        public string SecondAddress { get; set; }
        public string SecondPhone { get; set; }
        public string Notes { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public string BDay { get;  set; }
        public string BYear { get;  set; }
        public string BMonth { get;  set; }
        public string ADay { get;  set; }
        public string AMonth { get;  set; }
        public string AYear { get;  set; }

        public string CleanedAddress
        {
            get
            {
                if (cleanedAddress != null)
                {
                    return cleanedAddress;
                }
                else
                {
                    return GetAddress(Address);
                }
            }
            set
            {
                cleanedAddress = value;
            }
        }
        public string AllPhones
        {
            get
            {
               if (allPhones != null)
                {
                    return allPhones;
                }
                else
                {
                    return (CleanUp(HomePhone) + CleanUp(MobilePhone) + CleanUp(WorkPhone) + CleanUp(SecondPhone)).Trim();

                }
            }
            set
            {
                allPhones = value;
            }
        }

        public string AllEmails
        {
            get
            {
                if (allEmails != null)
                {
                    return allEmails;
                }
                else
                {
                     return GetAllEmails(Email1, Email2, Email3);
                }
            }
            set
            {
                allEmails = value;
            }
        }

        public string FullContactInfo
        {
            get
            {
                if (allInfo != null)
                {
                    return allInfo;
                }
                else
                {
                    return (
                        WriteLn(
                          WriteLn(GetAllNames(Firstname, Middlename, Lastname))
                        + WriteLn(Nickname.Trim())
                        + WriteLn(Title.Trim())
                        + WriteLn(Company.Trim())
                        + WriteLn(GetAddress(Address)))

                        + WriteLn(WriteLn(GetAllPhones(HomePhone, MobilePhone, WorkPhone, Fax)))
                        + WriteLn(WriteLn(GetAllEmails(Email1, Email2, Email3))
                        + WriteLn(GetHomePage(Homepage)))
                        //------- Birthday & Anniversary missing rows
                        //-------
                        + LnWrite(WriteLn(GetSecondAddress(SecondAddress)))
                        + LnWrite(WriteLn(GetSecondPhone(SecondPhone)))
                        + LnWrite(Notes.Trim())).Trim();        
                }
            }
            set
            {
                allInfo = value;
            }
        }

        public string GetAllNames (string firstname, string middlename, string lastname)
        {
            string str = "";
            if (firstname != null && firstname != "")
            {
                str = Firstname + " ";
            }
            if (middlename != null && middlename != "")
            {
                str = str + Middlename + " ";
            }
            if (lastname != null && lastname != "")
            {
                str = str + Lastname + " ";
            }
            return str.Trim();
        }


        private string GetAllPhones(string homePhone, string mobilePhone, string workPhone, string fax)
        {
            string str = "";
            if (homePhone != null && homePhone != "")
            {
                str = str + "H: " + CleanUpString(homePhone);
            }
            if (mobilePhone != null && mobilePhone != "")
            {
                str = str + "M: " + CleanUpString(homePhone);
            }
            if (workPhone != null && workPhone != "")
            {
                str = str + "W: " + CleanUpString(homePhone);
            }
            if (fax != null && fax != "")
            {
                str = str + "F: " + CleanUpString(fax);
            }
            return str.Trim();
        }

        private string GetSecondPhone(string phone)
        {
            string str = "";
            if (phone != null && phone != "")
            {
                str = str + "P: " + CleanUpString(phone).Trim();
            }
            return str.Trim();
        }

        private string GetHomePage(string homePage)
        {
            string str = "";
            if (homePage != null && homePage != "")
            {
                str = str + "Homepage:\r\n" + CleanUpString(homePage).Trim();
            }
            return str.Trim();
        }

        private string GetAllEmails(string email1, string email2, string email3)
        {
            return (CleanUpString(email1) + CleanUpString(email2) + CleanUpString(email3)).Trim();
        }

        private string GetAddress(string address)
        {
            string str = "";
            if (address != null && address != "")
            {
                str = str + Regex.Replace(address, "\\s +\r\n", "\r\n").Trim();
            }
            return str;
        }


        private string GetSecondAddress(string address)
        {
            if (address != null && address != "")
            {
                return Regex.Replace(address, "\\s +\r\n", "\r\n").Trim();
            }
            else
            {
                return "\r\n";
            }        
        }


        private string WriteLn(string str)
        {
            string s = "\r\n";
            if (str != "\r\n")
            {
                if (str == null || str == "")
                {
                    return "";
                }

                return str + "\r\n";
            }
            return s;
        }


        private string LnWrite(string str)
        {
            if (str == null || str == "")
            {
                return "";
            }
            if (str == "\r\n")
            {
                return str;
            }
                return "\r\n" + str;
        }



        private string CleanUpString(string str)
        // changes 2 and more spaces for 1 only
        // to use for emails and phones
        // mistake corrected (from 11 task)
        {
            if (str == null || str == "")
            {
                return "";
            }
            return Regex.Replace(str, @"\s+", " ") + "\r\n";
        }

        private string CleanUp(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return Regex.Replace(phone, "[ ()-]", "") + "\r\n";
        }



        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }

            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }

            return Firstname == other.Firstname && Lastname == other.Lastname;
        }

        public override int GetHashCode()
        {
            return Firstname.GetHashCode() + Lastname.GetHashCode();
        }

        public override string ToString()
        {
            return "first name = " + Firstname + "; last name = " + Lastname;

        }

        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }

            if (Lastname.CompareTo(other.Lastname) == 0)
            { 
                return Firstname.CompareTo(other.Firstname);
            }
            return Lastname.CompareTo(other.Lastname);
        }
    }
}
