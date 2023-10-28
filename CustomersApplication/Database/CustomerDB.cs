using System.ComponentModel.DataAnnotations;

namespace CustomersApplication.Database
{
    public class CustomerDB
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
