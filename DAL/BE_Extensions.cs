using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using System.Xml.Linq;
using System.Reflection;
using System.Xml.Serialization;
using System.IO;

namespace DAL
{
    public static class BE_Extensions
    {
        #region Clones
        /// <summary>
        /// Clone
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public static Order Clone(this Order order)
        {
            return new Order
            {
                OrderKey = order.OrderKey,
                Status = order.Status,
                CreateDate = order.CreateDate,
                GuestRequestKey = order.GuestRequestKey,
                HostingUnitKey = order.HostingUnitKey,
                OrderDate = order.OrderDate
            };
        }
        /// <summary>
        /// Clone
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public static Host Clone(this Host host)
        {
            return new Host
            {
                HostKey = host.HostKey,
            PrivateName = host.PrivateName,
            FamilyName = host.FamilyName ,
            PhoneNumber = host.PhoneNumber,
            MailAddress = host.MailAddress,
            BankAccount = host.BankAccount.Clone(),
            CollectionClearance = host.CollectionClearance
            };
        }
        /// <summary>
        /// Clone
        /// </summary>
        /// <param name="bankAccount"></param>
        /// <returns></returns>
        public static BankAccount Clone(this BankAccount bankAccount)
        {
            return new BankAccount
            {
                BankNumber = bankAccount.BankNumber,
                BankName = bankAccount.BankName,
                BranchNumber = bankAccount.BranchNumber,
                BranchAddress = bankAccount.BranchAddress,
                BranchCity = bankAccount.BranchCity,
                BankAccountNumber = bankAccount.BankAccountNumber
            };
        }


        //To-Do
        public static HostingUnit Clone(this HostingUnit hostingUnit)
        {
            return new HostingUnit
            {
               //To-Do
            };
        }
        public static requirement Clone(this requirement requirement)
        {
            return new requirement
            {
                //To-Do
            };
        }
        public static GuestRequest Clone(this GuestRequest guestRequest)
        {
            return new GuestRequest
            {
                //To-Do
            };
        }




        #endregion Clones

        public static XElement ToXML(this Order d)
        {
            return new XElement("Order",
                                 new XElement("OrderKey", d.OrderKey.ToString()),
                                 new XElement("HostingUnitKey", d.HostingUnitKey.ToString()),
                                 new XElement("GuestRequestKey", d.GuestRequestKey.ToString()),
                                 new XElement("CreateDate", d.CreateDate.ToString("o")),
                                 new XElement("OrderDate", d.OrderDate.ToString("o")),
                                 new XElement("Status", d.Status.ToString())
                                  );
        }

        //public static XElement ToXML(this BattlePlayer b)
        //{
        //    return new XElement("BattlePlayer",
        //                         new XElement("Name", b.Name.ToString()),
        //                         new XElement("Board",
        //                                (from v in b.Board.ToString()
        //                                 select v).ToArray(),
        //                                (from v in b.OpponentBoard.ToString()
        //                                 select v).ToArray()
        //                             )
        //                         );
        //}

        public static void SaveToXML<T>(T source, string path)
        {
            FileStream file = new FileStream(path, FileMode.Create);
            XmlSerializer xmlSerializer = new XmlSerializer(source.GetType());
            xmlSerializer.Serialize(file, source);
            file.Close();
        }

        public static T LoadFromXML<T>(string path)
        {
            FileStream file = new FileStream(path, FileMode.Open);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            T result = (T)xmlSerializer.Deserialize(file);
            file.Close();
            return result;
        }

        public static string ToXMLstring<T>(this T toSerialize)
        {
            using (StringWriter textWriter = new StringWriter())
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                xmlSerializer.Serialize(textWriter, toSerialize);
                return textWriter.ToString();
            }
        }
        public static T ToObject<T>(this string toDeserialize)
        {
            using (StringReader textReader = new StringReader(toDeserialize))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                return (T)xmlSerializer.Deserialize(textReader);
            }
        }


    }
}