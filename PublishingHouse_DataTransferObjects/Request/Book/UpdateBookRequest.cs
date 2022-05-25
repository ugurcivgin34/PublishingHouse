using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublishingHouse_DataTransferObjects.Request.Book
{
    public class UpdateBookRequest
    {
        
        public int Id { get; set; }
        public int WriterId { get; set; }
        public int CategoryId { get; set; }



        [Required]
        public string BookName { get; set; }
    }
}
