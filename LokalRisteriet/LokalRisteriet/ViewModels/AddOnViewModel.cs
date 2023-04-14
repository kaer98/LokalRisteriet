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

        public void AddAddOn(AddOn addOn)
        {
            _addOnRepo.addAddOn(addOn);
        }

        public void DeleteAddOn(AddOn addOn)
        {
            _addOnRepo.deleteAddOn(addOn);
        }

        public List<AddOn> GetAddOns(AddOn addOn)
        {
            return _addOnRepo.GetAllAddOns();
        }

        public void UpdateAddOn(AddOn addOn)
        {
            _addOnRepo.updateAddOn(addOn);
        }

    }
}
