using System.Collections.Generic;
using VidlyAPI.Models;

namespace VidlyAPI.Repository.IRepository
{
    public interface ICustomersRepository
    {
        IEnumerable<Customer> GetAllCustomers();
        Customer GetCustomer(int Id);
        bool CustomerExcistbyId(int Id);
        bool CustomerExcistbyName(string Name);
        bool CreateCustomer(Customer customer);
        bool UpdateCustomer(Customer customer);
        bool DeleteCustomer(Customer customer);
        bool Save();
    }
}
