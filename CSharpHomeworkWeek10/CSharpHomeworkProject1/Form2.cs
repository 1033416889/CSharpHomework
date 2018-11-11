using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace CSharpHomeworkProject1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }


        public virtual void button1_Click(object sender, EventArgs e)
        {
            bool flag = true;
            if (textBox1.Text == null || textBox1.Text == "")
            {
                flag = false;
                new Form5("订单ID不能为空！").ShowDialog();
            }
            if (!Regex.IsMatch(textBox1.Text,@"^[0-2]\d{3}[0-1]\d[0-3]\d{4}$" ))
            {
                flag = false;
                new Form5("订单ID格式有误！").ShowDialog();
            }
            if(textBox1.Text!=null&&textBox1.Text!="" && Form1.orders.Where(o => o.Id == textBox1.Text).Count() > 0)
            {
                flag = false;
                new Form5("该订单已存在！").ShowDialog();
            }
            if (!Regex.IsMatch(textBox3.Text,@"^1\d{10}$" ))
            {
                flag = false;
                new Form5("电话号码有误！").ShowDialog();
            }
            if (flag)
            {
                Customer customer = new Customer(textBox3.Text, textBox4.Text);
                Order order = new Order(textBox1.Text, customer);
                Form1.orders.Add(order);
                this.Close();
            }

        }
    }
}
