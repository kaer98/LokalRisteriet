
namespace LokalRisteriet.Models
{
    public class AddOn
    {
        public string AddOnName { get; set; }
        public double Price { get; private set; }
        private int _addOnID;
        private int _addOnBookingID;

        public int AddOnBookingID
        {
            get { return _addOnBookingID; }
            set { _addOnBookingID = value; }
        }

        public int AddOnID
        {
            get { return _addOnID; }
            set { _addOnID = value; }
        }


        public AddOn(string name, double price)
        {
            AddOnName = name;
            Price = price;
        }
    }
}
