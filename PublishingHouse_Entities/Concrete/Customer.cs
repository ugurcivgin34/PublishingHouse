using PublishingHouse_Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublishingHouse_Entities.Concrete
{
    public class Customer: BaseUser,IEntity
    {
        public virtual ICollection<Shopping> Shoppings { get; set; }

    }
}
