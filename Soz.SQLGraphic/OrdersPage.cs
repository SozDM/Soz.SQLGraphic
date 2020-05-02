using Soz.SQLConsole;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Soz.SQLGraphic
{
    public partial class OrdersPage : Form
    {
        Order orders = new Order();
        int UserIdGlob = 0;
        List<int> OrderIdList = new List<int>();

        public OrdersPage(int UserId)
        {
            UserIdGlob = UserId;
            InitializeComponent();
            labelTop.Text = $"Orders made by User#{UserId}";

            using (var context = new MyDBContext())
            {
                var user = context.UserManagers.First(item => item.Id == UserId);
                var order = context.Orders;

                labelName.Text = $"Name: {user.Name}";
                labelAddress.Text = $"Address: {user.Address}";

                RefreshOrdersList(UserId);
            }
        }

        private void RefreshOrdersList(int UserId)
        {
            dataGridView1.Rows.Clear();
            using (var context = new MyDBContext())
            {
                OrderIdList = new List<int>(orders.GetOrdersIdByUser(UserId));
                if (OrderIdList[0] != 0)
                {
                    foreach (var orderId in OrderIdList)
                    {
                        var thisOrder = context.Orders.First(item => item.Id == orderId);
                        dataGridView1.Rows.Add(thisOrder.Id, thisOrder.Date, thisOrder.Amount, thisOrder.Description);
                    }
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OrdersPage_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void buttonSaveOrder_Click(object sender, EventArgs e)
        {
            if (Int32.TryParse(textBoxOrderId.Text, out int orderId))
            {
                if (OrderIdList.Contains(Int32.Parse(textBoxOrderId.Text)))
                {
                    SaveOrder(orderId);
                }
            }
            else if (textBoxOrderId.Text == "")
            {
                SaveNewOrder();
            }
            else WarnUserId();
        }

        public void SaveOrder(int orderId)
        {
            if (!string.IsNullOrWhiteSpace(textBoxAmount.Text) & (Int32.TryParse(textBoxAmount.Text, out int result)))
            {
                int orderAmount = result;
                if (MessageBox.Show($"Do you want to save order #{orderId}?", $"Edit order #{orderId}", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    orders.Edit(orderId, orderAmount, textBoxDescription.Text);
                    RefreshOrdersList(UserIdGlob);
                    warnings.Visible = false;
                }
            }
            else WarnAmount();
        }

        public void SaveNewOrder()
        {
            if (Int32.TryParse(textBoxAmount.Text, out int result))
            {
                int orderAmount = result;
                if (MessageBox.Show($"Do you want to save new order?", $"Edit new order", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    orders.Add(UserIdGlob, orderAmount, textBoxDescription.Text);
                    RefreshOrdersList(UserIdGlob);
                    warnings.Visible = false;
                }
            }
            else WarnAmount();
        }

        private void buttonEditOrder_Click(object sender, EventArgs e)
        {
            if (Int32.TryParse(textBoxOrderId.Text, out int orderId))
            {
                if (OrderIdList.Contains(Int32.Parse(textBoxOrderId.Text)))
                {
                    using (var context = new MyDBContext())
                    {
                        var order = context.Orders.First(item => item.Id == orderId);
                        textBoxAmount.Text = order.Amount.ToString();
                        textBoxDescription.Text = order.Description;
                        warnings.Visible = false;
                    }
                }
                else WarnAmount();
            }
            else WarnAmount();
        }

        private void WarnAmount()
        {
            warnings.Text = "Input valid amount";
            warnings.Visible = true;
        }

        public void WarnUserId()
        {
            warnings.Text = "Input valid UserId";
            warnings.Visible = true;
        }

        private void textBoxAmount_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonDeleteOrder_Click(object sender, EventArgs e)
        {
            if (Int32.TryParse(textBoxOrderId.Text, out int orderId))
            {
                if ((textBoxOrderId.Text != "") & (OrderIdList.Contains(Int32.Parse(textBoxOrderId.Text))))
                {
                    if (MessageBox.Show($"Do you want to delete order #{orderId}?", $"Delete order #{orderId}", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        warnings.Visible = false;
                        orders.Delete(orderId);
                        RefreshOrdersList(UserIdGlob);
                    }
                }
            }
            else warnings.Visible = true;
        }

    }
}
