using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace FlowerApp.Models
{
    public class Users
    {
        [PrimaryKey, AutoIncrement]
        public int UserID { get; set; }

        [Unique]
        public string UserUsername { get; set; }

        public string UserPassword { get; set; }

        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }

        [Unique]
        public string UserEmail { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public int ZIP { get; set; }

        public int PhoneNumber { get; set; }

        [OneToMany]
        public List<FlowerArrangements>FlowerArrangements{get; set;}
    }

    public class Admins
    {
        [PrimaryKey, AutoIncrement]
        public int AdminID { get; set; }


        public string AdminEmail { get; set; }
        public string AdminFirstName { get; set; }
        public string AdminLastName { get; set; }
        public string AdminPassword { get; set; }

        [Unique]
        public string AdminUsername { get; set; }

        [OneToMany]
        public List<Flowers> Flowers { get; set; }

        [OneToMany]
        public List<Arrangements> Arrangements { get; set; }

    }

    public class Flowers
    {
        [PrimaryKey, AutoIncrement]
        public int FlowerID { get; set; }


        public string Color { get; set; }
        public int BasePrice { get; set; }
        public string Type { get; set; }

        [OneToMany]
        public List<FlowerArrangements> FlowerArrangements { get; set; }

        [ForeignKey(typeof(Admins))]
        public int AdminID { get; set; }
    }

    public class FlowerArrangements //join table
    {
        [PrimaryKey, AutoIncrement]
        public int FlowerArrangementID { get; set; }


        public int Price { get; set; }

        [ForeignKey(typeof(Users))]
        public int UserID { get; set; }

        [ForeignKey(typeof(Flowers))]
        public int FlowerID { get; set; }

        [ForeignKey(typeof(Arrangements))]
        public int ArrangementID { get; set; }

        [ForeignKey(typeof(Carts))]
        public int CartID { get; set; }
    }
    public class Arrangements
    {
        [PrimaryKey, AutoIncrement]
        public int ArrangementID { get; set; }

            
        public string Count { get; set; }
        public float SpecialRate { get; set; }
        public string Occassion { get; set; }

        [OneToMany]
        public List<FlowerArrangements> FlowerArrangements { get; set; }

        [ForeignKey(typeof(Admins))]
        public int AdminID { get; set; }
    }

    public class Carts
    {
        [PrimaryKey, AutoIncrement]
        public int CartID { get; set; }


        public float TotalPrice { get; set; }
    }
    public class Orders
    {
        [PrimaryKey, AutoIncrement]
        public int OrderID { get; set; }


        public string Type { get; set; }
        public string Date { get; set; }
        public string DeliveryAddress { get; set; }
        public int DeliveryZIP { get; set; }
        public string DeliveryState { get; set; }
        public string Status { get; set; }

        [ForeignKey(typeof(Carts))]
        public int CartID { get; set; }
    }
}
