using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DeanOfficeApp.Api.Models;
using System.Data.SqlClient;
using DeanOfficeApp.Contracts.Addresses;
using System.Data;

namespace DeanOfficeApp.Api.DAL.User
{
    public class UserAddressRepository : IUserAddressRepository
    {
        private readonly ApplicationDbContext context;
        private readonly string connection;

        public UserAddressRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void RemoveAddress(Address address)
        {
            context.Addresses.Remove(address);
        }

        public Address GetAddressById(int id)
        {
            return context.Addresses.FirstOrDefault(a => a.Id == id);
        }

        public int InsertUserAddress(AddAddressDTO address, string connection)
        {
            using (var con = new SqlConnection(connection))
            using (var command = new SqlCommand("AddAddress", con))
            {
                var procedureParams = new List<SqlParameter>();

                procedureParams.Add(new SqlParameter("City", address.City));
                procedureParams.Add(new SqlParameter("Road", address.Road));
                procedureParams.Add(new SqlParameter("House", address.House));
                procedureParams.Add(new SqlParameter("Country", address.Country));
                procedureParams.Add(new SqlParameter("UserId", address.UserId));

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddRange(procedureParams.ToArray());

                command.Parameters.Add("@NewId", SqlDbType.Int).Direction = ParameterDirection.Output;

                con.Open();
                command.ExecuteNonQuery();

                return Convert.ToInt32(command.Parameters["@NewId"].Value);
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~UserAddressRepository() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        public bool Save()
        {
           return context.SaveChanges() > 0;
        }
        #endregion
    }
}