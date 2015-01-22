using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReliabilityProbability.Models
{
    public class WayModel
    {
        public string Name { get; set; }
        public decimal Propability { get; set; }
        public List<Element> Elements { get; set; }
    }
}