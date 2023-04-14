
namespace LokalRisteriet.Models
{
    public class AddOn
    {
        public string Name { get; private set; }
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


        public AddOn(int addOnID, string name, double price, int addOnBookingID)
        {
            _addOnID = addOnID;
            Name = name;
            Price = price;
            _addOnBookingID = addOnBookingID;
        }
    }
}
