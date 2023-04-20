using System.Collections.Generic;
using LokalRisteriet.Models;
using LokalRisteriet.Persistence;

namespace LokalRisteriet.ViewModels
{
    public class AddOnViewModel
    {
        private AddOnRepo _addOnRepo;
        public AddOnViewModel() => _addOnRepo = new AddOnRepo();

        public void AddAddOn(AddOn addOn) => _addOnRepo.addAddOn(addOn);

        public void DeleteAddOn(AddOn addOn) => _addOnRepo.deleteAddOn(addOn);

        public List<AddOn> GetAddOns() => _addOnRepo.GetAllAddOns();

        public void UpdateAddOn(AddOn addOn) => _addOnRepo.updateAddOn(addOn);

        public AddOn GetAddOnByID(int id) => _addOnRepo.GetAddOnByID(id);

    }
}
