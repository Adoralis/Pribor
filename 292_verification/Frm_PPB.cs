using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace _292_verification
{
    public partial class Frm_PPB : Device
    {
        public Frm_PPB()
        {
            InitializeComponent();
        }

        byte Comand = 0;
        Int16 cntMain = 0;
        ushort tryCnt = 0;
        byte numByte = 0;

        public byte[] dataReceived;
        //const byte numberOfCommands = 33;
        int[] err_array = new int[100];     //массив для запоминания ошибок
        byte cntErr = 0;                    //счетчик для массива ошибок
        public bool FL_NORMA = true;

        private void btnStartPPB_Click(object sender, EventArgs e)
        {
            btnStartPPB.Enabled = false;

            Port_finder();

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
