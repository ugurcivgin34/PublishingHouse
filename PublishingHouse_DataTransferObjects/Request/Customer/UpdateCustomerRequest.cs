﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublishingHouse_DataTransferObjects.Request.Customer
{
    public class UpdateCustomerRequest
    {
        
        public int Id { get; set; }

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
