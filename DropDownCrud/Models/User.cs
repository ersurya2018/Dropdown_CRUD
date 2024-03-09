using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DropDownCrud.Models
{
    public class User
    {
        [Key]
        public int UID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int CountyID { get; set; }
        public int StateID { get; set; }
        public int CityID { get; set; }
        public bool IsActive { get; set; }
    }
}
