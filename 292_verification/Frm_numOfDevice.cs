using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _292_verification
{
    public partial class Frm_numOfDevice : MyForm
    {
        public Frm_numOfDevice()
        {
            InitializeComponent();
        }

        public string numberOfDevice = "0";
        private void btnEnter_Click(object sender, EventArgs e)
        {
            numberOfDevice = numberOfDevice_text.Text;
            Close();
        }

        private void btnEnter_MouseEnter(object sender, EventArgs e)
        {
            Mouse_enter(btnEnter);
        }

        private void btnEnter_MouseLeave(object sender, EventArgs e)
        {
            Mouse_leave(btnEnter);
        }
    }
}
