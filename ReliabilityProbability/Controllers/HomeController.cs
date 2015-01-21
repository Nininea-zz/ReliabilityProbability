using ReliabilityProbability.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReliabilityProbability.Controllers
{
    public class HomeController : Controller
    {
        public List<string> PartitionData = new List<string>();

        public ActionResult Index()
        {
            return View(new MainModel());
        }
        [HttpPost]
        public ActionResult CalculateData(MainModel model)
        {
            string[] ways;
            List<WaysModel> result = new List<WaysModel>();
            List<string> wholeElements = new List<string>();

            if (!string.IsNullOrEmpty(model.Ways))
            {
                /*შეყვანილი სტრინგიდან ვიღებთ ელემენტებს და შესაბამისად გზებს, ვწერთ მოდელში*/
                ways = model.Ways.Split(';');
                int counter = 1;
                foreach (var way in ways)
                {
                    var w = way.Trim();
                    if (!string.IsNullOrEmpty(w))
                    {
                        var elements = w.Split(',').ToList();
                        wholeElements.AddRange(elements);

                        WaysModel m = new WaysModel();
                        m.Name = "P" + counter;
                        m.Elements = new List<string>();
                        m.Elements = elements;
                        m.Way = w.Replace(',', '\0');

                        if (model.WholeElements != null && elements != null)
                        {
                            decimal wayprop = 1;
                            foreach (var el in elements)
                            {
                                var prop = model.WholeElements.FirstOrDefault(x => x.ElementValue == el).Probability;
                                wayprop *= prop.Value;
                            }
                            m.WayProp = wayprop;
                        }

                        result.Add(m);
                        counter++;
                    }
                }
            }
            if (model.WholeElements == null || result.Count == 0)
            {
                model.WholeElements = wholeElements.Distinct().Select(x => new Element { ElementValue = x }).ToList();
            }

            else
            {
                decimal total = 0;
                /*ვიძახებთ კომნიბაციების დათვლის მეთოდს*/
                var combinations = GetCombination(result.Select(x => 
                    new WayModel { Name = x.Name, Propability = x.WayProp })
                    .ToList(), out total)
                    .OrderBy(x => x.Name.Length).ThenBy(x => x.Name);


                /*ვინახავთ მოდელში შესაბამისი ინფორმაციის გამოსატანად*/
                var formula = string.Join(" ", combinations.Select(x => x.Name));
                model.Result = formula.Substring(1);
                model.WayModels = result;
                model.Propability = total;
            }
            return View("Index", model);
        }

        /*ვაწვდით ელემენტებს შესაბამისი წოენებით*/
        public List<WayModel> GetCombination(List<WayModel> list, out decimal total)
        {
            List<WayModel> combinations = new List<WayModel>();
            total = 0;

            double count = Math.Pow(2, list.Count);
            for (int i = 1; i <= count - 1; i++)
            {
                string str = Convert.ToString(i, 2).PadLeft(list.Count, '0');
                string value = "";
                decimal prop = 1;

                for (int j = 0; j < str.Length; j++)
                {
                    /*იღებს ყველა კომბინაციას შესაბამისი წონებით*/
                    if (str[j] == '1')
                    {
                        value += list[j].Name;
                        prop *= list[j].Propability;
                    }
                }
                /*ხარისხის მიხედვით ვუცვლით ნიშანს ელემენტს*/
                if ((value.Length / 2) % 2 == 1)
                {
                    total += prop;
                    value = "+" + value;
                }
                else
                {
                    total -= prop;
                    value = "-" + value;
                }
                combinations.Add(new WayModel { Name = value, Propability = prop });
            }
            return combinations;
        }
    }
}