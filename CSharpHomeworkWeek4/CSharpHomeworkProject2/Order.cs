using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpHomeworkProject2
{
    class Order
    {
        //实在不知道订单和订单明细有什么区别，就在这里写了个二分查找来提高查找速度

        public List<OrderDetails> orders;
        
        public Order()
        {
            orders = new List<OrderDetails>();
        }

        //用二分查找的方式以ID从小到大的方式插入数据
        public bool MyAdd(OrderDetails orderDetails)
        {
            int n = Upper_bound(orderDetails.ID);
            if (n == -1)
            {
                return false;
            }
            else
            {
                orders.Insert(n, orderDetails);
                return true;
            }
        }

        //二分查找数据
        public int MySearch(OrderDetails orderDetails)
        {
            int n = Binary_Search(orderDetails.ID);
            return n;
        }

        public int Upper_bound(long id)
        {
            int left = 0, right = orders.Count, mid = (left + right) / 2;
            while (left < right)
            {
                if (id == orders[mid].ID)
                {
                    return -1;
                }
                else if (id < orders[mid].ID)
                {
                    right = mid ;
                }
                else
                {
                    left = mid + 1;
                }
                mid = (left + right) / 2;
            }
            return left;
        }

        public int Binary_Search(long id)
        {
            if (orders.Count == 0) return -1;
            int left = 0, right = orders.Count-1, mid = (left + right) / 2;
            while (left < right)
            {
                if (id == orders[mid].ID)
                {
                    return mid;
                }
                else if (id < orders[mid].ID)
                {
                    right = mid;
                }
                else
                {
                    left = mid + 1;
                }
                mid = (left + right) / 2;
            }
            if (orders[left].ID == id) return left;
            else return -1;
        }

        public void DisplayOrder(int n)
        {
            Console.WriteLine($"订单号:{orders[n].ID}  用户名:{orders[n].User}  商品名:{orders[n].Name}  数目:{orders[n].Num}  单价:{orders[n].Price}");
        }
    }
}
