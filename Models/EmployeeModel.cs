using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SolrNetSample.Models
{
    public class EmployeeModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Please enter first name")]
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        [StringLength(200)]
        public string Address { get; set; }
        [Required]
        public decimal? Salary { get; set; }
        [Required]
        [StringLength(50)]
        public string Department { get; set; }
    }
}
