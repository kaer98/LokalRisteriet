using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LokalRisteriet.Models;
using LokalRisteriet.Persistence;

namespace LokalRisteriet.ViewModels
{
    public class AddOnViewModel
    {
        private AddOnRepo _addOnRepo;
        public AddOnViewModel()
        {
            _addOnRepo = new AddOnRepo();

        }

        // Adds an AddOn object to the repository.
        public void AddAddOn(AddOn addOn)
        {
            _addOnRepo.addAddOn(addOn);
        }

        // Deletes an AddOn object from the repository.
        public void DeleteAddOn(AddOn addOn)
        {
            _addOnRepo.deleteAddOn(addOn);
        }

        // Retrieves a list of all AddOn objects from the repository.
        public List<AddOn> GetAddOns()
        {
            return _addOnRepo.GetAllAddOns();
        }

        // Updates an existing AddOn object in the repository.
        public void UpdateAddOn(AddOn addOn)
        {
            _addOnRepo.updateAddOn(addOn);
        }

        // Retrieves an AddOn object from the repository by its ID.
        public AddOn GetAddOnByID(int id)
        {
            return _addOnRepo.GetAddOnByID(id);
        }

    }
}
