using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DropDownCrud.Models
{
    public class City
    {
        [Key]
        public int DistID { get; set; }
        public string DistricName { get; set; }
        [ForeignKey("State")]
        public int StateID { get; set; }
        public State State { get; set; }
    }
}
