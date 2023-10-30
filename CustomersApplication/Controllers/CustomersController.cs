using CustomersApplication.Database;
using CustomersApplication.Model;
using Microsoft.AspNetCore.Mvc;

namespace CustomersApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private DatabaseContext _context;
        public CustomersController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet("GetCustomers")]
        public IActionResult GetCustomers()
        {
            try
            {
                var customers = _context.Customers.ToList();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetCustomer/{Id}")]
        public IActionResult GetCustomer([FromRoute] int id)
        {
            try
            {
                var customer = _context.Customers.FirstOrDefault(x => x.Id == id);
                if (customer == null)
                {
                    return BadRequest("Customer with that id doesn't exist");
                }
                return Ok(customer);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddCustomer")]
        public IActionResult AddCustomer([FromBody] CustomerModel modelRequest)
        {
            var customerDB = new CustomerDB();
            customerDB.Name= modelRequest.Name;
            customerDB.Surname= modelRequest.Surname;

            try
            {
                _context.Customers.Add(customerDB);
                _context.SaveChanges();
            } 
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(customerDB);
        }

        [HttpDelete("DeleteCustomer/{Id}")]
        public IActionResult DeleteCustomer([FromRoute] int id)
        {
            try
            {
                var customer = _context.Customers.FirstOrDefault(x => x.Id == id);
                if (customer != null)
                {
                    _context.Customers.Remove(customer);
                    _context.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            var customers = _context.Customers.ToList();
            return Ok(customers);
        }
    }
}
