using PublishingHouse_Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublishingHouse_Entities.Concrete
{
    public class Writer:BaseUser,IEntity
    {
        public virtual ICollection<Book> Books { get; set; }
    }
}
