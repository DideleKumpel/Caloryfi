using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caloryfi.Model
{
    public class MealModel
    {
        public int Id { get; set; }
        public int Calories { get; set; }
        public int Carbs { get; set; }
        public int Proteins { get; set; }
        public int Fats { get; set; }
        public virtual ICollection<IngriedentsModel> Ingriedents { get; set; }
    }
}
