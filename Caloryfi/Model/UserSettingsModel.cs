using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caloryfi.Model
{
    public class UserSettingsModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool Sex { get; set; }
        public int NumberOfMeals { get; set; }
        public int DietGoal { get; set; }
        public int ActivityLevel { get; set; }
        public decimal Kcal { get; set; }
        public decimal Carbs { get; set; }
        public decimal Proteins { get; set; }
        public decimal Fats { get; set; }
    }
}
