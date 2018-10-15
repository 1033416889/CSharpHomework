using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpHomeworkProject2
{
    class OrderDetails
    {
        public long ID { get; set; }
        public string User { get; set; }
        public string Name { get;set; }
        public int Num { get; set; }
        public int Price { get; set; }

        public OrderDetails(long id,string user,string name, int num , int price)
        {
            this.ID = id;
            this.User = user;
            this.Name = name;
            this.Num = num;
            this.Price = price;
        }

        public OrderDetails()
        {
        }

        public override string ToString()
        {
            return $"订单号:{ID}  用户名:{User}  商品名:{Name}  数目:{Num}  单价:{Price}";
        }
    }
}
