using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReliabilityProbability.Models
{
    public class MainModel
    {
        public string Ways { get; set; }
        public string Result { get; set; }
        public decimal Propability { get; set; }
        public virtual List<Element> WholeElements { get; set; }
        public virtual List<WaysModel> WayModels { get; set; }
    }
}