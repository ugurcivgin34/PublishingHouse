using PublishingHouse_Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublishingHouse_Entities.Concrete
{
    public class Book : IEntity
    {
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int WriterId { get; set; }
        public int CustomerId { get; set; }
        public int CategoryId { get; set; }



        [Required]
        public string BookName { get; set; }

        [ForeignKey("WriterId")]
        public virtual Writer Writer { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }



    }
}
