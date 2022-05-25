using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublishingHouse_DataTransferObjects.Request.Shopping
{
    public class AddShoppingRequest
    {
   
        public int BookId { get; set; }
        public int CustomerId { get; set; }

        [Required]
        public DateTime ShoppingDate { get; set; }
    }
}
