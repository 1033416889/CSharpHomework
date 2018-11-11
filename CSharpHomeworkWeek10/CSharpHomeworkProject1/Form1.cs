using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Xml;

namespace CSharpHomeworkProject1
{
    public partial class Form1 : Form
    {
        public static List<Order> orders = new List<Order>();
        public string KeyCode { get; set; }
        public static OrderService os = new OrderService();

        public Form1()
        {
            InitializeComponent();

            Customer customer1 = new Customer("13476541234", "Customer1");
            Customer customer2 = new Customer("15398760987", "Customer2");

            Goods milk = new Goods(1, "Milk", 69.9);
            Goods eggs = new Goods(2, "eggs", 4.99);
            Goods apple = new Goods(3, "apple", 5.59);

            OrderDetail orderDetails1 = new OrderDetail(1, apple, 800);
            OrderDetail orderDetails2 = new OrderDetail(2, eggs, 2);
            OrderDetail orderDetails3 = new OrderDetail(3, milk, 1);

            Order order1 = new Order("20170101123", customer1);
            Order order2 = new Order("20181231857", customer2);
            Order order3 = new Order("20181111111", customer2);
            order1.AddDetails(orderDetails1);
            order1.AddDetails(orderDetails2);
            order1.AddDetails(orderDetails3);
            order2.AddDetails(orderDetails2);
            order2.AddDetails(orderDetails3);
            order3.AddDetails(orderDetails3);

            orders.Add(order1);
            orders.Add(order2);
            orders.Add(order3);

            bindingSource1.DataSource = orders;

            textBox1.DataBindings.Add("Text", this, "KeyCode");

            comboBox1.SelectedIndex = 0;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (KeyCode != ""&&KeyCode!=null)
            {
                bool flag = false;
                try
                {
                    if (comboBox1.SelectedIndex == 0)
                    {
                        List<Order> neworders = orders.Where(s => s.Id.ToString() == KeyCode).ToList();
                        if (neworders.Count == 0) flag = true;
                        bindingSource1.DataSource = neworders;
                    }
                    else if (comboBox1.SelectedIndex == 1)
                    {
                        List<Order> neworders = orders.Where(order => order.Customer.Name == textBox1.Text).ToList();
                        if (neworders.Count == 0) flag = true;
                        bindingSource1.DataSource = neworders;

                    }
                    else if (comboBox1.SelectedIndex == 2)
                    {
                        List<Order> neworders = orders.Where(order =>
                            order.Details.Where(d => d.Goods.Name == textBox1.Text).Count() > 0).ToList();
                        if (neworders.Count == 0) flag = true;
                        bindingSource1.DataSource = neworders;
                    }
                    else if (comboBox1.SelectedIndex == 3)
                    {
                        List<Order> neworders = orders.Where(order => order.Amount > double.Parse(textBox1.Text)).ToList();
                        if (neworders.Count == 0) flag = true;
                        bindingSource1.DataSource = neworders;
                    }
                }
                catch
                {
                    new Form5("无法查询到该数据").ShowDialog();
                }
                if(flag) new Form5("无法查询到该数据").ShowDialog();

            }
            else
            {
                bindingSource1.DataSource = orders;
            }
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            new Form2().ShowDialog();
            bindingSource1.ResetBindings(true);
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            new Form3(e.RowIndex).ShowDialog();
            bindingSource1.ResetBindings(true);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            new Form4(dataGridView1.SelectedRows[0].Index).ShowDialog();
            bindingSource1.ResetBindings(true);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                button1.PerformClick();
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            new Form3(dataGridView1.SelectedRows[0].Index).ShowDialog();
            bindingSource1.ResetBindings(true);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "(HTML)|*.html";
            saveFileDialog1.Title = "保存html路径：";
            saveFileDialog1.ShowDialog();
            string path = System.IO.Path.GetFullPath(saveFileDialog1.FileName);

            if (saveFileDialog1.FileName != "")
            {

                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Order>));
                using (FileStream fs = new FileStream(@"..\..\OrderList.xml", FileMode.Create))
                {
                    xmlSerializer.Serialize(fs, orders);
                }

                XmlDocument doc = new XmlDocument();
                doc.Load(@"..\..\OrderList.xml");

                XPathNavigator nav = doc.CreateNavigator();
                nav.MoveToRoot();

                XslCompiledTransform xt = new XslCompiledTransform();
                xt.Load(@"..\..\OrderList.xslt");

                FileStream outFileStream = File.OpenWrite(path);
                XmlTextWriter writer =
                    new XmlTextWriter(outFileStream, System.Text.Encoding.UTF8);
                xt.Transform(nav, null, writer);

                outFileStream.Close();
            }

            System.Diagnostics.Process.Start("explorer.exe", path);
            new Form5("导出成功！").ShowDialog();
            
        }
    }
}
