using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class GuestRequest
    {
        int GuestRequestKey;
        string PrivateName;
        string FamilyName;
        string MailAddress;
        bool Status;
        DateTime RegistrationDate;
        DateTime EntryDate;
        DateTime ReleaseDate;
        int Area;
        int SubArea;
        int Type;
        int Adults;
        int Children;
        bool Pool;
        bool Jacuzzi;
        bool Garden;
        bool ChildrensAttractions;

        //----------------function-------------------
        GuestRequest()
        {
            if ((GuestRequestKey > 99999999) || (GuestRequestKey < 0))
                throw new IndexOutOfRangeException();

        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
