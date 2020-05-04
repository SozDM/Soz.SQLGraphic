using Soz.SQLConsole;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Soz.SQLGraphic
{
    public partial class LoginPage : Form
    {

        static List<int> UserIdList = new List<int>();

        public LoginPage()
        {
            InitializeComponent();
            GetUserIdList();
            RefreshUserList();
        }

        void GetUserIdList()
        {
            using (var context = new MyDBContext())
            {
                
                var users = context.UserManagers;
                UserIdList.Clear();
                foreach (var item in users)
                {
                    UserIdList.Add(item.Id);
                }
            }
        }

        void RefreshUserList()
        {
            using (var context = new MyDBContext())
            {
                dataGridUsers.Rows.Clear();
                foreach (var item in UserIdList)
                {
                    var thisUser = context.UserManagers.First(user => user.Id == item);
                    dataGridUsers.Rows.Add(thisUser.Id, thisUser.Name);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(textBox1.Text)|(!Int32.TryParse(textBox1.Text, out int result)))
            {
                warnings.Text = "Input valid UserId";
                warnings.Visible = true;
            }
            else if (UserIdList.Contains(Int32.Parse(textBox1.Text)))
            {
                int UserId = Int32.Parse(textBox1.Text);
                OrdersPage ordersPage = new OrdersPage(UserId);
                warnings.Visible = false;
                this.Hide();
                ordersPage.ShowDialog();
                this.Show();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
