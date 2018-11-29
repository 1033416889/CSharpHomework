using EFDemo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrderForm
{
    public partial class FormMain : Form
    {
        public static OrderService orderService;
        BindingSource fieldsBS = new BindingSource();
        public FormMain()
        {
            InitializeComponent();
            init();
        }

        public void init()
        {
            List<OrderItem> items1 = new List<OrderItem>() {
                new OrderItem("1", "apple", 10.0, 20),
                new OrderItem("2", "egg", 2.0, 100)
            };

            List<OrderItem> items2 = new List<OrderItem>() {
                new OrderItem("1", "apple", 10.0, 20),
                new OrderItem("3", "orange", 5.0, 50)
            };

            Order order1 = new Order("001", "jia", DateTime.Now, items1);

            Order order2 = new Order("002","liuwang",DateTime.Now,items2);
            Order order3 = new Order("003", "jams", DateTime.Now, items2);
   

            orderService = new OrderService();
            //orderService.Add(order2);
            //orderService.Add(order1);
            //orderService.Add(order3);

            orderBindingSource.DataSource = orderService.GetAllOrders();
            var tmp = orderService.GetAllOrders();
            orderItemBindingSource.DataSource = orderService.GetAllOrders()[0].Items;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FormEdit form2 = new FormEdit();
            form2.ShowDialog();
            Order newOrder = form2.getResult();
            if (newOrder!=null){
                orderService.Add(newOrder);
                orderBindingSource.DataSource = orderService.GetAllOrders();
                Order nowOrder = (Order)orderBindingSource.Current;
                orderItemBindingSource.DataSource = nowOrder.Items;
            }
            
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            FormEdit form2 = new FormEdit((Order)orderBindingSource.Current);
            form2.ShowDialog();
            Order newOrder = form2.getResult();
            orderService.Add(newOrder);
            orderBindingSource.DataSource = orderService.GetAllOrders();
            Order nowOrder = (Order)orderBindingSource.Current;
            orderItemBindingSource.DataSource = nowOrder.Items;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Order o=(Order)orderBindingSource.Current;
            if (o != null)
            {
                orderService.Delete(o.Id);
                orderBindingSource.DataSource=orderService.GetAllOrders();
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            DialogResult result = saveFileDialog1.ShowDialog();
            if (result.Equals(DialogResult.OK))
            {
                String fileName = saveFileDialog1.FileName;
                orderService.Export(fileName);
            }
           
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result.Equals(DialogResult.OK))
            {
                String fileName = openFileDialog1.FileName;
                orderService.Import(fileName);
                orderBindingSource.DataSource = orderService.GetAllOrders();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (cbField.SelectedIndex)
            {
                case 0:
                    orderBindingSource.DataSource =
                        orderService.GetAllOrders();
                    break;
                case 1:
                    uint id = 0;
                    uint.TryParse(txtValue.Text, out id);
                    orderBindingSource.DataSource = orderService.GetOrder(txtValue.Text);
                    break;
                case 2:
                    orderBindingSource.DataSource =
                            orderService.QueryByCustormer(txtValue.Text);
                    break;
                case 3:
                    orderBindingSource.DataSource =
                            orderService.QueryByGoods(txtValue.Text);
                    break;
                case 4:
                    double price = 0;
                    double.TryParse(txtValue.Text, out price);
                    orderBindingSource.DataSource =
                           orderService.QueryByPrice(price);
                    break;
            }

        }

        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            Order nowOrder = (Order)orderBindingSource.Current;
            orderItemBindingSource.DataSource = nowOrder.Items;
        }
    }
}
