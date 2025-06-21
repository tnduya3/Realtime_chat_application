using project_cuoi_ky.Models;
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
    public partial class startchat : Form
    {
        public startchat(ApiUserInfo friendInfo)
        {
            _friendInfo = friendInfo;
            InitializeComponent();
            LoadFriendInfo();
        }

        public string InitialMessage => rtbMessage.Text;
        public bool MessageSent { get; private set; } = false;

        private ApiUserInfo _friendInfo;


        private void LoadFriendInfo()
        {
            if (_friendInfo != null)
            {
                lblUserName.Text = $"To: {_friendInfo.userName}";
                lblEmail.Text = _friendInfo.email;
            }
        }

        private void RtbMessage_KeyDown(object sender, KeyEventArgs e)
        {
            // Send message with Ctrl+Enter
            if (e.Control && e.KeyCode == Keys.Enter)
            {
                BtnSend_Click(sender, e);
                e.Handled = true;
            }
        }

        private void BtnSend_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(rtbMessage.Text))
            {
                MessageBox.Show("Please enter a message.", "Empty Message",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                rtbMessage.Focus();
                return;
            }

            MessageSent = true;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
