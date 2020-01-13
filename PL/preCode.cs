using BE;
using BL;
using System;
using System.Collections.Generic;

namespace PL
{

    public class preCode
    {
        BL.MyBl mybl = new MyBl();
        public void initialize()
        {
            /*
            #region add_hosts_and_HusingUnit
            Host host = new Host();
            host.HostKey = 11111111;
            host.PrivateName = "david";
            host.FamilyName = "cohn";
            host.PhoneNumber = "054-5555555";
            host.MailAddress = "sale@gmai.com";
            #region david_BankAccount
            host.BankAccount = new BankAccount();
            host.BankAccount.BankAccountNumber = 11111111;
            host.BankAccount.BankName = "poalim";
            host.BankAccount.BankNumber = "123";
            host.BankAccount.BranchAddress = "jefo 121";
            host.BankAccount.BranchCity = "jeruselm";
            host.BankAccount.BranchNumber = 432;
            #endregion david_BankAccount
            host.CollectionClearance = "";
            mybl.AddHost(host);

            #region add_hosting_units_to_david

            HostingUnit hu = new HostingUnit();
            hu.Owner = "david cohn";
            hu.HostingUnitName = "hotel clifornia";
            hu.UnitArea = BE.Area.North;
            hu.HostKey = 11111111;
            mybl.GetHost(hu.HostKey).AddHostingUnit(hu);

            hu = new HostingUnit();
            hu.Owner = "david cohn";
            hu.HostingUnitName = "hotel hong-kong";
            hu.UnitArea = BE.Area.North;
            hu.HostKey = 11111111;
            mybl.GetHost(hu.HostKey).AddHostingUnit(hu);


            hu = new HostingUnit();
            hu.Owner = "david cohn";
            hu.HostingUnitName = "hotel paris";
            hu.UnitArea = BE.Area.North;
            hu.HostKey = 11111111;
            mybl.GetHost(hu.HostKey).AddHostingUnit(hu);

            #endregion add_hosting_units_to_david
            
            
            host = new Host();
            host.HostKey = 22222222;
            host.PrivateName = "moses";
            host.FamilyName = "levi";
            host.PhoneNumber = "054-6666666";
            host.MailAddress = "hotel@gmai.com";
            #region levi_BankAccount
            host.BankAccount = new BankAccount();
            host.BankAccount.BankAccountNumber = 11111112;
            host.BankAccount.BankName = "poalim";
            host.BankAccount.BankNumber = "123";
            host.BankAccount.BranchAddress = "jefo 121";
            host.BankAccount.BranchCity = "jeruselm";
            host.BankAccount.BranchNumber = 432;
            #endregion levi_BankAccount
            host.CollectionClearance = "";
            mybl.AddHost(host);
            #region add_hosting_units_to_moses

            hu = new HostingUnit();
            hu.Owner = "moses levi";
            hu.HostingUnitName = "hotel tal-aviv";
            hu.UnitArea = BE.Area.Center;
            hu.HostKey = 22222222;
            mybl.GetHost(hu.HostKey).AddHostingUnit(hu);

            hu = new HostingUnit();
            hu.Owner = "moses levi";
            hu.HostingUnitName = "hotel jeruselm";
            hu.UnitArea = BE.Area.Jerusalem;
            hu.HostKey = 11111111;
            mybl.GetHost(hu.HostKey).AddHostingUnit(hu);


            hu = new HostingUnit();
            hu.Owner = "moses levi";
            hu.HostingUnitName = "hotel tavriea";
            hu.UnitArea = BE.Area.North;
            hu.HostKey = 11111111;
            mybl.GetHost(hu.HostKey).AddHostingUnit(hu);

            hu = new HostingUnit();
            hu.Owner = "moses levi";
            hu.HostingUnitName = "hotel hifa";
            hu.UnitArea = BE.Area.North;
            hu.HostKey = 11111111;
            mybl.GetHost(hu.HostKey).AddHostingUnit(hu);

            #endregion add_hosting_units_to_moses

            host = new Host();
            host.HostKey = 33333333;
            host.PrivateName = "dan";
            host.FamilyName = "danon";
            host.PhoneNumber = "054-1010101";
            host.MailAddress = "zimer@gmai.com";
            #region dan_BankAccount
            host.BankAccount = new BankAccount();
            host.BankAccount.BankAccountNumber = 1152582;
            host.BankAccount.BankName = "laomi";
            host.BankAccount.BankNumber = "525";
            host.BankAccount.BranchAddress = "gargmel 1010";
            host.BankAccount.BranchCity = "heifa";
            host.BankAccount.BranchNumber = 852;
            #endregion dan_BankAccount
            host.CollectionClearance = "";
            mybl.AddHost(host);
            #region add_hosting_units_to_dan

            hu = new HostingUnit();
            hu.Owner = "dan danon";
            hu.HostingUnitName = "zimer of the north";
            hu.UnitArea = BE.Area.North;
            hu.HostKey = 33333333;
            mybl.GetHost(hu.HostKey).AddHostingUnit(hu);

            hu = new HostingUnit();
            hu.Owner = "dan danon";
            hu.HostingUnitName = "zimer of the Jerusalem";
            hu.UnitArea = BE.Area.Jerusalem;
            hu.HostKey = 33333333;
            mybl.GetHost(hu.HostKey).AddHostingUnit(hu);

            hu = new HostingUnit();
            hu.Owner = "dan danon";
            hu.HostingUnitName = "zimer of the South";
            hu.UnitArea = BE.Area.South;
            hu.HostKey = 33333333;
            mybl.GetHost(hu.HostKey).AddHostingUnit(hu);


            #endregion add_hosting_units_to_dan

            #endregion add_hosts_and_HusingUnit
           */

            #region guestRequests
            #region parz_request
            GuestRequest gr = new GuestRequest();
            gr.PrivateName = "zareh";
            gr.FamilyName = "parz";
            gr.MailAddress = "penhes@gmail.com";
            gr.Status = BE.ClientStatus.Open;
            gr.RegistrationDate = new DateTime(1 / 1 / 2020);
            gr.EntryDate = new DateTime(25 / 4 / 2020);
            gr.ReleaseDate = new DateTime(1 / 5 / 2020);
            gr.Area = Area.Jerusalem;
            gr.SubArea = "old city";
            gr.HostingType = HostingType.Hotel;
            gr.Adults = 2;
            gr.Children = 1;
            gr.Pool = Requirements.Necessary;
            gr.Jacuzzi = Requirements.NotNecessary;
            gr.Garden = Requirements.NotNecessary;
            gr.ChildrensAttractions = Requirements.Necessary;
            gr.SpredBads = Requirements.NotNecessary;
            gr.AirCondsner = Requirements.Necessary;
            //gr.frisider = Requirements.Possible;
            gr.SingogNaerBy = Requirements.Necessary;
            gr.NaerPublicTrensportion = Requirements.Possible;
            mybl.AddGuestRequest(gr);
            #endregion parz_request
                            

            #region kalmen_request

            gr = new GuestRequest();
            gr.PrivateName = "kalmen";
            gr.FamilyName = "kalmenovitz";
            gr.MailAddress = "kalmenovitz@gmail.com";
            gr.Status = BE.ClientStatus.Open;
            gr.RegistrationDate = new DateTime(1 / 3 / 2020);
            gr.EntryDate = new DateTime(5 / 2 / 2020);
            gr.ReleaseDate = new DateTime(15 / 2 / 2020);
            gr.Area = Area.North;
            gr.SubArea = "tabrios";
            gr.HostingType = HostingType.Zimmer;
            gr.Adults = 4;
            gr.Children = 0;
            gr.Pool = Requirements.Necessary;
            gr.Jacuzzi = Requirements.Necessary;
            gr.Garden = Requirements.Necessary;
            gr.ChildrensAttractions = Requirements.NotNecessary;
            gr.SpredBads = Requirements.Necessary;
            gr.AirCondsner = Requirements.Necessary;
            //gr.frisider = Requirements.Necessary;
            gr.SingogNaerBy = Requirements.Possible;
            gr.NaerPublicTrensportion = Requirements.Possible;
            mybl.AddGuestRequest(gr);
            #endregion kalmen_request

           

            #region afreim_request

            gr = new GuestRequest();
            gr.PrivateName = "afreim";
            gr.FamilyName = "cohn";
            gr.MailAddress = "cohn@gmail.com";
            gr.Status = BE.ClientStatus.Open;
            gr.RegistrationDate = new DateTime(31 / 11 / 2019);
            gr.EntryDate = new DateTime(29 / 12 / 2019);
            gr.ReleaseDate = new DateTime(3 / 1 / 2020);
            gr.Area = Area.Center;
            gr.SubArea = "tal aviv";
            gr.HostingType = HostingType.RentingRoom;
            gr.Adults = 2;
            gr.Children = 2;
            gr.Pool = Requirements.NotNecessary;
            gr.Jacuzzi = Requirements.NotNecessary;
            gr.Garden = Requirements.NotNecessary;
            gr.ChildrensAttractions = Requirements.Necessary;
            gr.SpredBads = Requirements.NotNecessary;
            gr.AirCondsner = Requirements.Necessary;
           // gr.frisider = Requirements.Necessary;
            gr.SingogNaerBy = Requirements.Necessary;
            gr.NaerPublicTrensportion = Requirements.Necessary;
            mybl.AddGuestRequest(gr);
            #endregion afreim_request
           
            #region golomb_request

            gr = new GuestRequest();
            gr.PrivateName = "aser";
            gr.FamilyName = "golomb";
            gr.MailAddress = "golomb@gmail.com";
            gr.Status = BE.ClientStatus.Open;
            gr.RegistrationDate = new DateTime(3 / 3/ 2020);
            gr.EntryDate = new DateTime(25 / 9 / 2020);
            gr.ReleaseDate = new DateTime(5 / 10 / 2020);
            gr.Area = Area.Jerusalem;
            gr.SubArea = "old city";
            gr.HostingType = HostingType.Hotel;
            gr.Adults = 2;
            gr.Children = 5;
            gr.Pool = Requirements.Possible;
            gr.Jacuzzi = Requirements.Possible;
            gr.Garden = Requirements.NotNecessary;
            gr.ChildrensAttractions = Requirements.Necessary;
            gr.SpredBads = Requirements.NotNecessary;
            gr.AirCondsner = Requirements.Necessary;
           //gr.frisider = Requirements.Possible;
            gr.SingogNaerBy = Requirements.Necessary;
            gr.NaerPublicTrensportion = Requirements.Possible;
            mybl.AddGuestRequest(gr);
            #endregion golomb_request

            #endregion guestRequests
        }
    }
}