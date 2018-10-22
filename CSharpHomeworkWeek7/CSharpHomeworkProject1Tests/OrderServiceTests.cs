using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharpHomeworkProject1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpHomeworkProject1.Tests
{
    [TestClass()]
    public class OrderServiceTests
    {
        private List<Order> GetOders()
        {
            Customer customer1 = new Customer(1, "Customer1");
            Customer customer2 = new Customer(2, "Customer2");

            Goods milk = new Goods(1, "Milk", 69.9);
            Goods eggs = new Goods(2, "eggs", 4.99);
            Goods apple = new Goods(3, "apple", 5.59);

            OrderDetail orderDetails1 = new OrderDetail(1, apple, 800);
            OrderDetail orderDetails2 = new OrderDetail(2, eggs, 2);
            OrderDetail orderDetails3 = new OrderDetail(3, milk, 1);

            Order order1 = new Order(1, customer1);
            Order order2 = new Order(2, customer2);
            Order order3 = new Order(3, customer2);
            order1.AddDetails(orderDetails1);
            order1.AddDetails(orderDetails2);
            order1.AddDetails(orderDetails3);
            order2.AddDetails(orderDetails2);
            order2.AddDetails(orderDetails3);
            order3.AddDetails(orderDetails3);

            List<Order> orders = new List<Order>();
            orders.Add(order1);
            orders.Add(order2);
            orders.Add(order3);

            return orders;
        }

        private OrderService Init()
        {
            OrderService os = new OrderService();

            List<Order> orders = GetOders();

            foreach (Order order in orders)
            {
                os.AddOrder(order);
            }

            return os;
        }

        OrderService os = new OrderService();

        [TestMethod()]
        public void AddOrderTest()
        {
            os = new OrderService();
            List<Order> orders = GetOders();
            os.AddOrder(orders[0]);
            Assert.IsNotNull(os);
        }

        [TestMethod()]
        public void AddOrderTest1()
        {
            List<Order> orders = GetOders();
            OrderService os2 = new OrderService();
            os2.AddOrder(orders[0]);
            os2.AddOrder(orders[0]);
            Assert.IsTrue(os2.QueryAllOrders().Count == 1);
        }

        [TestMethod()]
        public void RemoveOrderTest()
        {
            os = new OrderService();
            os.AddOrder(GetOders()[0]);
            os.RemoveOrder(GetOders()[0].Id);
            Assert.IsTrue(os.QueryAllOrders().Count == 0);
        }

        [TestMethod()]
        public void RemoveOrderTest1()
        {
            os.RemoveOrder(1);
        }

        [TestMethod()]
        public void GetByIdTest()
        {
            os = new OrderService();
            Order order = os.GetById(0);
            Assert.IsNull(order);
        }

        [TestMethod()]
        public void GetByIdTest1()
        {
            os = Init();
            Order order = os.GetById(GetOders()[0].Id);
            Assert.AreEqual(order.Id, GetOders()[0].Id);
        }

        [TestMethod()]
        public void QueryByGoodsNameTest()
        {
            os = Init();
            List<Order> orders = os.QueryByGoodsName(GetOders()[0].Details[0].Goods.Name);
            var query = os.QueryAllOrders().Where(order =>
                        order.Details.Where(d => d.Goods.Name == GetOders()[0].Details[0].Goods.Name).Count() > 0);
            Assert.AreEqual(query.Count(), orders.Count);
        }

        [TestMethod()]
        public void QueryByCustomerNameTest()
        {
            os = Init();
            List<Order> orders = os.QueryByCustomerName(GetOders()[0].Customer.Name);
            var query = os.QueryAllOrders().Where(order => order.Customer.Name == GetOders()[0].Customer.Name);
            Assert.AreEqual(query.Count(), orders.Count);
        }

        [TestMethod()]
        public void QueryByPriceTest()
        {
            os = Init();
            List<Order> orders = os.QueryByPrice(999);
            var query = os.QueryAllOrders().Where(order => order.Amount > 999);
            Assert.AreEqual(query.Count(), orders.Count);
        }

        [TestMethod()]
        public void UpdataCustomerTest()
        {
            os = Init();
            Customer newCustomer = GetOders()[0].Customer;
            newCustomer.Id = 999;
            newCustomer.Name = "xxxxxxxxxx";
            os.UpdataCustomer(GetOders()[0].Id, newCustomer);
            Assert.IsTrue(os.GetById(GetOders()[0].Id).Customer.Id == 999 &&
                os.GetById(GetOders()[0].Id).Customer.Name == newCustomer.Name);
        }

        [TestMethod()]
        public void ExportTest()
        {
            os = Init();
            os.Export();
        }

        [TestMethod()]
        public void ImportTest()
        {
            os = new OrderService();
            os.Import();
        }

        [TestMethod()]
        public void ExportTest1()
        {
            os = new OrderService();
            os.Export();
        }
    }
}