using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caloryfi.Model
{
    class MealModel
    {
       public DateTime Date_Added { get; set; }
       public virtual ICollection<IngriedentsModel> Ingriedents { get; set; }
    }
}
