using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpHomeworkProject2
{
    class MyException : ApplicationException
    {
        public MyException(string message) : base(message) { }
    }


    class OrderService:Order
    {
        public void AddOrder()
        {
            try
            {
                Console.WriteLine("请依次输入添加订单的数据(订单ID,客户名,商品名,数目,单价),并且以空格分隔各个数据:");
                OrderDetails newOrderDetails = new OrderDetails();
                string data = Console.ReadLine();
                string[] datas = data.Split(' ');
                newOrderDetails.ID = long.Parse(datas[0]);
                newOrderDetails.User = datas[1];
                newOrderDetails.Name = datas[2];
                newOrderDetails.Num = int.Parse(datas[3]);
                newOrderDetails.Price = int.Parse(datas[4]);
                if (MyAdd(newOrderDetails))
                {
                    Console.WriteLine("添加成功！");
                }
                else
                {
                    Console.WriteLine("已存在当前ID的订单，添加失败！");
                }
            }
            catch
            {
                Console.WriteLine("输入有误！");
            }
        }

        public void DeleteOrder()
        {
            Console.WriteLine("请输入删除订单的编号:");
            try
            {
                long id = long.Parse(Console.ReadLine());
                int n = Binary_Search(id);
                if (n == -1)
                {
                    Console.WriteLine("此编号订单不存在，删除失败！");
                }
                else
                {
                    orders.RemoveAt(n);
                    Console.WriteLine("删除成功！");
                }
            }
            catch
            {
                Console.WriteLine("输入的订单编号格式有误！");
            }

        }

        public void UpdataOrder()
        {
            Console.WriteLine("请输入修改订单的编号");
            try
            {
                long id = long.Parse(Console.ReadLine());
                int n = Binary_Search(id);
                if (n == -1)
                {
                    Console.WriteLine("此订单编号不存在，修改失败！");
                    return;
                }

                Console.WriteLine("请输入需要修改的数据编号(从1到4分别为客户名，商品名，数目，单价):");
                int dataNum = int.Parse(Console.ReadLine());
                if (dataNum < 1 || dataNum > 4)
                {
                    Console.WriteLine("输入编号有误");
                    return;
                }
                switch (dataNum)
                {
                    case 1:
                        Console.WriteLine("请输入客户名:");
                        string user = Console.ReadLine();
                        orders[n].User = user;
                        break;
                    case 2:
                        Console.WriteLine("请输入商品名:");
                        string name = Console.ReadLine();
                        orders[n].Name = name;
                        break;
                    case 3:
                        Console.WriteLine("请输入商品数目:");
                        int num = int.Parse(Console.ReadLine());
                        orders[n].Num = num;
                        break;
                    case 4:
                        Console.WriteLine("请输入商品单价:");
                        int price = int.Parse(Console.ReadLine());
                        orders[n].Price = price;
                        break;
                }
                Console.WriteLine("修改成功！");
            }
            catch
            {
                Console.WriteLine("输入有误！");
            }
        }


        public void FindOrder()
        {
            Console.WriteLine("请输入查询方式(1.ID 2.商品名 3.客户名 4.订单金额(大于)):");
            try
            {
                int opNum = int.Parse(Console.ReadLine());
                switch (opNum)
                {
                    case 1:
                        Console.WriteLine("请输入订单编号:");
                        long id = long.Parse(Console.ReadLine());
                        int n = Binary_Search(id);
                        if (n == -1)
                        {
                            Console.WriteLine("此订单编号不存在，查询失败！");
                            return;
                        }
                        DisplayOrder(n);
                        break;
                    case 2:
                        Console.WriteLine("请输入商品名:");
                        string tName = Console.ReadLine();
                        var m = from t in orders where t.Name == tName select t;
                        if(m.Count()==0)
                        {
                            Console.WriteLine("未找到该商品名的订单");
                        }
                        foreach(var t in m)
                        {
                            Console.WriteLine(t.ToString());
                        }
                        break;
                    case 3:
                        Console.WriteLine("请输入客户名:");
                        string tUser = Console.ReadLine();
                        m = from t in orders where t.User == tUser select t;
                        if (m.Count() == 0)
                        {
                            Console.WriteLine("未找到该客户名的订单");
                        }
                        foreach (var t in m)
                        {
                            Console.WriteLine(t.ToString());
                        }
                        break;
                    case 4:
                        Console.WriteLine("请输入总价需要大于的数值:");
                        int tWholePrice = int.Parse(Console.ReadLine());
                        m = from t in orders where t.Num * t.Price > tWholePrice select t;
                        if (m.Count() == 0)
                        {
                            Console.WriteLine("未找到符合条件的订单");
                        }
                        foreach (var t in m)
                        {
                            Console.WriteLine(t.ToString());
                        }
                        break;
                    default:
                        Console.WriteLine("输入编号有误！");
                        break;
                }
            }
            catch
            {
                Console.WriteLine("输入有误！");
            }
        }

        public void DisplayAllOrders()
        {
            if (orders.Count == 0)
            {
                Console.WriteLine("订单为空！");
            }
            else
            {
                Console.WriteLine("\t编号\t客户名\t商品名\t数目\t价格");
                foreach(OrderDetails data in orders)
                {
                    Console.WriteLine($"\t{data.ID}\t{data.User}\t{data.Name}\t{data.Num}\t{data.Price}");
                }
            }
        }

        public void StartOrderService()
        {
            Console.WriteLine("欢迎进入订单管理系统！\n系统目录：\n1.添加订单;\n2.删除订单;\n3.修改订单;\n4.查询订单;\n5.所有订单\n6.退出系统");
            while (true)
            {
                Console.WriteLine("请输入操作编号:");
                if(int.TryParse(Console.ReadLine(),out int num))
                {
                    if(num<1||num>6)
                    {
                        Console.WriteLine("输入有误，请重新输入\n");
                    }
                    else
                    {
                        switch (num)
                        {
                            case 1:
                                AddOrder();
                                break;
                            case 2:
                                DeleteOrder();
                                break;
                            case 3:
                                UpdataOrder();
                                break;
                            case 4:
                                FindOrder();
                                break;
                            case 5:
                                DisplayAllOrders();
                                break;
                            case 6:
                                return;
                        }
                        Console.WriteLine("");
                    }
                }
                else
                {
                    Console.WriteLine("输入有误，请重新输入\n");
                }

            }
        }
    }
}
