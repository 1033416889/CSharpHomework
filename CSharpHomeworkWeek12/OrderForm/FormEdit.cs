using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EFDemo;

namespace OrderForm
{
    public partial class FormEdit : Form
    {
        Order result = null;
        public FormEdit()
        {
            InitializeComponent();
            result = new Order();
            orderItemBindingSource.DataSource = result.Items;
            this.Text = "新建订单";
        }
        public FormEdit(Order order)
        {
            InitializeComponent();
            result = order;
            orderItemBindingSource.DataSource = result.Items;
            textBox1.Text = order.Id;
            textBox2.Text = order.Customer;
            this.Text = "修改订单";
            FormMain.orderService.Delete(order.Id);
        }

        public Order getResult()
        {
            return result; 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            result.Id = textBox1.Text;
            result.Customer = textBox2.Text;
            result.CreateTime = DateTime.Now;
            this.Close();
        }
    }
}
