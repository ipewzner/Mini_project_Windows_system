using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using System.Xml.Linq;
using System.Reflection;

namespace DAL
{
    public static class BE_Extensions
    {
       
       public static XElement ToXML(this Order d)
        {
            return new XElement("Order",
                                 new XElement("OrderKey", d.OrderKey.ToString()),
                                 new XElement("HostingUnitKey", d.HostingUnitKey.ToString()),
                                 new XElement("GuestRequestKey", d.GuestRequestKey.ToString()),
                                 new XElement("CreateDate", d.CreateDate.ToString()),
                                 new XElement("OrderDate", d.OrderDate.ToString()),
                                 new XElement("Status", d.Status.ToString())
                                  );
        }
        
    }
}