using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DropDownCrud.Models
{
    public class Country
    {
        [Key]
        public int CID { get; set; }
        public string CountryName { get; set; }
    }
}
