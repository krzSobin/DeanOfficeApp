using DeanOfficeApp.Api.Models;
using DeanOfficeApp.Contracts.Addresses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeanOfficeApp.Api.DAL.User
{
    public interface IUserAddressRepository : IDisposable
    {
        Address GetAddressById(int id);
        int InsertUserAddress(AddAddressDTO address, string connection);
        void RemoveAddress(Address address);
        bool Save();
    }
}