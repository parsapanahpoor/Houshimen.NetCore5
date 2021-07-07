using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities.Product
{
    public partial class WholeSaleOff
    {
        [Key]
        public int WholeSaleId { get; set; }
        public int ProductID { get; set; }
        public int WholeSaleCount { get; set; }
        public int WholeSaleOffPercent { get; set; }

        public virtual Product product { get; set; }
    }
}
