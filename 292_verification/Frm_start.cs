using System;

namespace _292_verification
{
    public partial class Frm_start : MyForm
    {

        Frm_numOfDevice NumOfDevice;

        Frm_PPB PPB;


        public string numOfDevice;
        public Frm_start()
        {
            InitializeComponent();
        }

        private void btnControlPPB_MouseEnter(object sender, EventArgs e)
        {
            Mouse_enter(btnControlPPB);
        }

        private void btnControlPPB_MouseLeave(object sender, EventArgs e)
        {
            Mouse_leave(btnControlPPB);
        }

        private void btnControlPSK_MouseEnter(object sender, EventArgs e)
        {
            Mouse_enter(btnControlPSK);
        }

        private void btnControlPSK_MouseLeave(object sender, EventArgs e)
        {
            Mouse_leave(btnControlPSK);
        }

        private void btnControlKPrA_MouseEnter(object sender, EventArgs e)
        {
            Mouse_enter(btnControlKPrA);
        }

        private void btnControlKPrA_MouseLeave(object sender, EventArgs e)
        {
            Mouse_leave(btnControlKPrA);
        }

        private void btnProgPPB_MouseEnter(object sender, EventArgs e)
        {
            Mouse_enter(btnProgPPB);
        }

        private void btnProgPPB_MouseLeave(object sender, EventArgs e)
        {
            Mouse_leave(btnProgPPB);
        }

        private void btnProgPSK_MouseEnter(object sender, EventArgs e)
        {
            Mouse_enter(btnProgPSK);
        }

        private void btnProgPSK_MouseLeave(object sender, EventArgs e)
        {
            Mouse_leave(btnProgPSK);
        }

        private void btnControlPPB_Click(object sender, EventArgs e)
        {
            NumOfDevice = new Frm_numOfDevice();
            NumOfDevice.Owner = this;
            NumOfDevice.ShowDialog(this);
            numOfDevice = NumOfDevice.numberOfDevice;
            PPB = new Frm_PPB();
            PPB.Owner = this;
            PPB.Show();

            Hide();



            Hide();

        }
        //////////
        //private void Mouse_enter(Button btn)
        //{
        //    btn.BackColor = SystemColors.ButtonShadow;
        //}

        //private void Mouse_leave(Button btn)
        //{
        //    btn.BackColor = SystemColors.ButtonFace;
        //}
        //////////
    }
}
