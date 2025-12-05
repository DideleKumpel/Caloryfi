using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caloryfi.Model
{
    public partial class IngriedentsModel: ObservableObject
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public int Kcal { get; set; }
        public int Carbs { get; set; }
        public int Proteins { get; set; }
        public int Fats { get; set; }
    }
}
