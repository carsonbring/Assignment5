using System.Collections.Generic;
namespace Assignment5.Models
{
    public class ShoppingCart
    {
        private static ShoppingCart _instance;

        public static ShoppingCart Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ShoppingCart();
                }
                return _instance;
            }
        }

        private ShoppingCart() // Private constructor to prevent instantiation outside of the class
        {
            Songs = new List<Song>();
        }

        public List<Song> Songs { get; set; }
    }
}
