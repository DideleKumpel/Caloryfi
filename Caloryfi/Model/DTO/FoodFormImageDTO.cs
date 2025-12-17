using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caloryfi.Model.DTO
{
    public class FoodFormImageDTO
    {
        public string Name { get; set; }
        public double Weight { get; set; }
        public ImageModel Image { get; set; }
    }
}
