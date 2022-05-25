using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublishingHouse_DataTransferObjects.Request.Shopping
{
    public class UpdateShoppingRequest
    {
   
        public int Id { get; set; }
        public int BookId { get; set; }
        public int CustomerId { get; set; }
    }
}
