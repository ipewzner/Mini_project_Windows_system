using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using System.Xml.Serialization;
using System.IO;

namespace DataSource
{
   static public class DataSourceList
    {

        //static void con()
        //{
        //    Stream stream = File.OpenRead(Environment.CurrentDirectory + "\\hosts.xml");
        //    XmlSerializer xmlser = new XmlSerializer(typeof(List<Host>));
        //    var x = xmlser.Deserialize(stream);
        //    Console.WriteLine(x);

        //}

        //static void des()
        //{
        //    Stream stream = File.OpenWrite(Environment.CurrentDirectory + "\\hosts.xml");
        //    XmlSerializer xmlser = new XmlSerializer(typeof(List<Host>));
        //    xmlser.Serialize(stream, Hosts);
        //    stream.Close();
        //}



        public static List<Host> Hosts = new List<Host>();

        public static List<HostingUnit> HostingUnits = new List<HostingUnit>();

        public static List<Order> Orders = new List<Order>();

        public static List<GuestRequest> GuestRequests = new List<GuestRequest>();
    }
}
