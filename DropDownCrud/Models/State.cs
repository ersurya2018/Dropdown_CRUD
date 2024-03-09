using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DropDownCrud.Models
{
    public class State
    {
        [Key]
        public int SId { get; set; }
        public string StateName { get; set; }

        [ForeignKey("Country")]
        public int CountyID { get; set; }
        public Country Country { get; set; }

    }
}
