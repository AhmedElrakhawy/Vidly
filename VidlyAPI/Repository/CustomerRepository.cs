using System.Collections.Generic;
using System.Linq;
using VidlyAPI.Data;
using VidlyAPI.Models;
using VidlyAPI.Repository.IRepository;

namespace VidlyAPI.Repository
{
    public class CustomerRepository : ICustomersRepository
    {
        private readonly ApplicationDbContext _Context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _Context = context;
        }
        public bool CreateCustomer(Customer customer)
        {
            _Context.Customers.Add(customer);
            return Save();
        }

        public bool CustomerExcistbyId(int Id)
        {
            return _Context.Customers.Any(x => x.Id == Id);
        }
        public bool CustomerExcistbyName(string Name)
        {
            return _Context.Customers.Any(x => x.Name.ToLower().Trim() == Name.ToLower().Trim());
        }

        public bool DeleteCustomer(Customer customer)
        {

            _Context.Customers.Remove(customer);
            return Save();
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _Context.Customers.ToList().OrderBy(x => x.Name);
        }

        public Customer GetCustomer(int Id)
        {
            return _Context.Customers.SingleOrDefault(x => x.Id == Id);
        }

        public bool Save()
        {
            return _Context.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateCustomer(Customer customer)
        {
            _Context.Customers.Update(customer);
            return Save();
        }
    }
}
