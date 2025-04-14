using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project_cuoi_ky
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void registerLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            register_form registerForm = new register_form();
            registerForm.Show();
            this.Hide();

            // sau khi đăng ký thành công, quay lại form đăng nhập
            if (registerForm.ShowDialog() == DialogResult.OK)
            {
                this.Show();
            }

        }

        private void forgotLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            forgot forgotForm = new forgot();
            forgotForm.Show();
        }
    }
}
