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
    public partial class MyForm : Form
    {
        public void Mouse_enter(Button btn)
        {
            btn.BackColor = SystemColors.ButtonShadow;
        }

        public void Mouse_leave(Button btn)
        {
            btn.BackColor = SystemColors.ButtonFace;
        }

    }
}