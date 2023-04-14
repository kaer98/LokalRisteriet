
namespace LokalRisteriet.Models
{
    public class AddOn
    {
        public string Name { get; private set; }
        public double Price { get; private set; }

        public AddOn(string name, double price)
        {
            Name = name;
            Price = price;
        }
    }
}
