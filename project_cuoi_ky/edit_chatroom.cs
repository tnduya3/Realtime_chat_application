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
    public partial class edit_chatroom : Form
    {        
        public string ChatroomName => txtChatroomName.Text.Trim();
        public string Description => txtDescription.Text.Trim();
        public bool ChangesSaved { get; private set; } = false;
        
        private int _chatroomId;
        private string _originalName;
        private string _originalDescription;
        
        public edit_chatroom(int chatroomId, string currentName, string currentDescription = "")
        {
            _chatroomId = chatroomId;
            _originalName = currentName;
            _originalDescription = currentDescription;
            
            InitializeComponent();
            LoadChatroomInfo();
        }
        
        private void LoadChatroomInfo()
        {
            txtChatroomName.Text = _originalName;
            txtDescription.Text = _originalDescription;
            txtChatroomName.SelectAll();
            txtChatroomName.Focus();
        }
        
        private void TxtChatroomName_KeyDown(object sender, KeyEventArgs e)
        {
            // Save with Ctrl+Enter
            if (e.Control && e.KeyCode == Keys.Enter)
            {
                BtnSave_Click(sender, e);
                e.Handled = true;
            }
        }
        
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtChatroomName.Text))
            {
                MessageBox.Show("Please enter a chatroom name.", "Empty Name", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtChatroomName.Focus();
                return;
            }
            
            if (txtChatroomName.Text.Trim() == _originalName && 
                txtDescription.Text.Trim() == _originalDescription)
            {
                MessageBox.Show("No changes detected.", "No Changes", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            ChangesSaved = true;
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
