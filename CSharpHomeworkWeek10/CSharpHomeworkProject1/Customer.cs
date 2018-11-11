using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpHomeworkProject1
{
    [Serializable]
    public class Customer
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public Customer(string id = "",string name = "")
        {
            Id = id;
            Name = name;
        }

        public Customer()
        {
            Id = "";
            Name = "";
        }

        public override string ToString()
        {
            return $"customerId:{Id}, CustomerName:{Name}";
        }
    }
}
