using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Xml.Serialization;
using LinqToDB.Mapping;

namespace WebAddressbookTests

{
    [Table]
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;
        private string allEmails;
        private string fullContactInfo;
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
        [JsonIgnore, XmlIgnore]
        public string Address { get; set; }
        [JsonIgnore, XmlIgnore]
        public string HomePhone { get; set; }
        [JsonIgnore, XmlIgnore]
        public string MobilePhone { get; set; }
        [JsonIgnore, XmlIgnore]
        public string WorkPhone { get; set; }
        [JsonIgnore, XmlIgnore]
        public string Email1 { get; set; }
        [JsonIgnore, XmlIgnore]
        public string Email2 { get; set; }
        [JsonIgnore, XmlIgnore]
        public string Email3 { get; set; }
        [JsonIgnore, XmlIgnore]
        public string Id { get; set; }
        [JsonIgnore, XmlIgnore]
        public string Nickname { get; set; }
        [JsonIgnore, XmlIgnore]
        public string Fax { get; set; }
        [JsonIgnore, XmlIgnore]
        public string Homepage { get; set; }
        [JsonIgnore, XmlIgnore]
        public string Birthday { get; set; }
        [JsonIgnore, XmlIgnore]
        public string Annivesary { get; set; }
        [JsonIgnore, XmlIgnore]
        public string SecondAddress { get; set; }
        [JsonIgnore, XmlIgnore]
        public string SecondPhone { get; set; }
        [JsonIgnore, XmlIgnore]
        public string Notes { get; set; }
        [JsonIgnore, XmlIgnore]
        public string Title { get; set; }
        [JsonIgnore, XmlIgnore]
        public string Company { get; set; }
        [JsonIgnore, XmlIgnore]
        public string BDay { get;  set; }
        [JsonIgnore, XmlIgnore]
        public string BYear { get;  set; }
        [JsonIgnore, XmlIgnore]
        public string BMonth { get;  set; }
        [JsonIgnore, XmlIgnore]
        public string ADay { get;  set; }
        [JsonIgnore, XmlIgnore]
        public string AMonth { get;  set; }
        [JsonIgnore, XmlIgnore]
        public string AYear { get;  set; }
        [JsonIgnore, XmlIgnore]
  
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
        [JsonIgnore, XmlIgnore]
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
        [JsonIgnore, XmlIgnore]
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
        [JsonIgnore, XmlIgnore]
        public string FullContactInfo
        {
            get
            {
                if (fullContactInfo != null)
                {
                    return fullContactInfo;
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
                fullContactInfo = value;
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
