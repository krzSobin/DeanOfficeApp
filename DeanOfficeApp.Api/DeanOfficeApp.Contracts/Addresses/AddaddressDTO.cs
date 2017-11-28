using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeanOfficeApp.Contracts.Addresses
{
    public class AddAddressDTO
    {
        public string City { get; set; }
        public string Road { get; set; }
        public string House { get; set; }
        public string Country { get; set; }

        public int? UserId { get; set; }
    }
}
