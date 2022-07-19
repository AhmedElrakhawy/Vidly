using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Vidly.Models;
using Vidly.Repository.IRepository;
using Vidly.ViewModel;

namespace Vidly.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        private ICustomersRepository _CustomerRepository;
        private IMemberShipTypeRepository _MemberShipTypeRepository;
        public CustomersController(ICustomersRepository customersRepository, IMemberShipTypeRepository memberShipTypeRepository)
        {
            _CustomerRepository = customersRepository;
            _MemberShipTypeRepository = memberShipTypeRepository;
        }
        //protected override void Dispose(bool disposing)
        //{
        //    base.Dispose();
        //}

        public async Task<IActionResult> CustomerForm()
        {
            var MemberShipTypes = await _MemberShipTypeRepository.GetAll(SD.MemberShipeTypeUrl);
            var ViewModel = new CustomerFormViewModel
            {
                Customer = new Customer(),
                MemberShipTypes = MemberShipTypes
            };
            return View(ViewModel);
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetAllCustomers()
        {
            var Customers = await _CustomerRepository.GetAll(SD.CustomerApiUrl);
            var MemberShipes = await _MemberShipTypeRepository.GetAll(SD.MemberShipeTypeUrl);
            foreach (var customer in Customers)
            {
                customer.MemberShipType = MemberShipes.Where(x => x.Id == customer.Id).FirstOrDefault();
            }
            return Json(new { Data = Customers });
        }
        public async Task<IActionResult> Details(int Id)
        {
            var Customer = await _CustomerRepository.GetAsync(SD.CustomerApiUrl, Id);
            if (Customer == null)
                return NotFound();

            return View(Customer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var ViewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MemberShipTypes = await _MemberShipTypeRepository.GetAll(SD.MemberShipeTypeUrl)
                };
                return View("CustomerForm", ViewModel);
            }
            if (customer.Id == 0)
                await _CustomerRepository.CreateAsync(SD.CustomerApiUrl, customer);
            else
            {
                await _CustomerRepository.UpdateAsync(SD.CustomerApiUrl, customer);
            }
            return RedirectToAction("Index", "Customers");
        }
        public async Task<IActionResult> Edit(int Id)
        {
            var Customer = await _CustomerRepository.GetAsync(SD.CustomerApiUrl, Id);
            CustomerFormViewModel ViewModel = new CustomerFormViewModel
            {
                Customer = Customer,
                MemberShipTypes = await _MemberShipTypeRepository.GetAll(SD.MemberShipeTypeUrl)
            };
            return View("CustomerForm", ViewModel);
        }
        public async Task<IActionResult> Delete(int Id)
        {
            var Status = await _CustomerRepository.DeleteAsync(SD.CustomerApiUrl, Id);
            if (Status)
            {
                return Json(new { success = true, message = "Deleted Successful" });
            }
            return Json(new { success = false, message = "Deleted Not Successful" });
        }
    }
}
