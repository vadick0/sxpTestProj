using sxpTestProj.Models;
using System.Collections.Generic;
using System.Xml.Linq;

namespace sxpTestProj.Service
{
    public static class XmlParse
    {
        public static List<Unit> Parse(string url)
        {
            List<Unit> Units = new List<Unit>();
            int i = 0;
            XDocument xdoc = XDocument.Load(url);
            foreach (XElement unit in xdoc.Element("Units").Elements("Unit"))
            {
                string nameAttribute = unit.Attribute("Title").Value;
                Unit u = new Unit
                {
                    Id = i,
                    Title = nameAttribute.ToString()
                };

                foreach (XElement person in unit.Elements("Employee"))
                {
                    string name = person.Attribute("Name").Value;
                    string position = person.Attribute("Position").Value;
                    string hireDate = person.Attribute("HireDate").Value;
                    Employee p = new Employee { Name = name, HireDate = hireDate, Position = position };
                    u.Employees.Add(p);
                }
                Units.Add(u);
                i++;
            }

            return Units;

        }
    }
}
