using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublishingHouse_DataTransferObjects.Request.Writer
{
    public class AddWriterRequest
    {

        [Required]
        public string UserName { get; set; }
        [Required]

        public string Password { get; set; }
        [Required]

        public string Email { get; set; }
        [Required]

        public string Role { get; set; }
        [Required]

        public string FirstName { get; set; }
        [Required]

        public string LastName { get; set; }
    }
}
