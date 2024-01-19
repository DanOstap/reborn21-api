using System.ComponentModel.DataAnnotations;

namespace Reborn.Models
{
    public class Products
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public double price { get; set; }
        public string color { get; set; }
        public  string size { get; set; }
    }
}
