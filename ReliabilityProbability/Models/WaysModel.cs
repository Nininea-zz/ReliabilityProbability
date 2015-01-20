using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReliabilityProbability.Models
{
    public class WaysModel
    {
        public string Name { get; set; }
        public string Way { get; set; }
        public decimal WayProp { get; set; }
        public List<string> Elements { get; set; }
    }
}