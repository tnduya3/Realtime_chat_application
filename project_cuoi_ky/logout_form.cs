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
    public partial class logout_form : Form
    {
        public logout_form()
        {
            InitializeComponent();
        }

        private void btnTrue_Click(object sender, EventArgs e)
        {
            // result dialog == ok
            this.DialogResult = DialogResult.OK;
        }

        private void btnFalse_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
