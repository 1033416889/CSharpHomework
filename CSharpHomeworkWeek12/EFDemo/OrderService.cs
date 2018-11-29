using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace EFDemo
{
    public class OrderService
    {

        public void Add(Order order)
        {
            using (var db = new OrderDB())
            {
                if (db.Order.Where(o => o.Id == order.Id).Count() == 0)
                {
                    db.Order.Add(order);
                    //db.Order.Attach(order);
                    //db.Entry(order).State = EntityState.Added;
                    db.SaveChanges();
                }
            }
        }

        public void Delete(String orderId)
        {
            using (var db = new OrderDB())
            {
                var order = db.Order.Include("Items").SingleOrDefault(o => o.Id == orderId);
                db.OrderItem.RemoveRange(order.Items);
                db.Order.Remove(order);
                db.SaveChanges();
            }
        }


        public void Update(Order order)
        {
            using (var db = new OrderDB())
            {
                db.Order.Attach(order);
                db.Entry(order).State = EntityState.Modified;
                order.Items.ForEach(
                    item => db.Entry(item).State = EntityState.Modified);
                db.SaveChanges();
            }
        }

        public Order GetOrder(String Id)
        {
            using (var db = new OrderDB())
            {
                return db.Order.Include("Items").
                  SingleOrDefault(o => o.Id == Id);
            }
        }

        public List<Order> GetAllOrders()
        {
            using (var db = new OrderDB())
            {
                return db.Order.Include("items").ToList<Order>();
            }
        }


        public List<Order> QueryByCustormer(String custormer)
        {
            using (var db = new OrderDB())
            {
                return db.Order.Include("items")
                  .Where(o => o.Customer.Equals(custormer)).ToList<Order>();
            }
        }

        public List<Order> QueryByGoods(String product)
        {
            using (var db = new OrderDB())
            {
                var query = db.Order.Include("items")
                  .Where(o => o.Items.Where(
                    item => item.Product.Equals(product)).Count() > 0);
                return query.ToList<Order>();
            }
        }

        public List<Order> QueryByPrice(double price)
        {
            using (var db = new OrderDB())
            {
                var query = db.Order.Include("items").Where(
                    o => o.Items.Where(
                        item => item.UnitPrice * item.Quantity < price).Count()>0);
                return query.ToList<Order>();
            }
        }

        public string Export()
        {
            DateTime time = System.DateTime.Now;
            string fileName = "orders_" + time.Year + "_" + time.Month
                + "_" + time.Day + "_" + time.Hour + "_" + time.Minute
                + "_" + time.Second + ".xml";
            Export(fileName);
            return fileName;
        }

        public void Export(String fileName)
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<Order>));
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                using (var db = new OrderDB())
                {
                    xs.Serialize(fs, db.Order.Include("items").ToList<Order>());
                }
            }
        }
        public List<Order> Import(string path)
        {
            if (Path.GetExtension(path) != ".xml")
                throw new ArgumentException("It isn't a xml file!");
            XmlSerializer xs = new XmlSerializer(typeof(List<Order>));
            List<Order> result = new List<Order>();

            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                using (var db = new OrderDB())
                {
                    List<Order> temp = (List<Order>)xs.Deserialize(fs);
                    temp.ForEach(order =>
                    {
                        if (!db.Order.ToList().Contains(order))
                        {
                            result.Add(order);
                        }
                    });
                }
            }
            return result;
        }


    }
}
