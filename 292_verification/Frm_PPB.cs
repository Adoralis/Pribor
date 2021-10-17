using System;
using System.Windows.Forms;


namespace _292_verification
{
    public partial class Frm_PPB : MyForm
    {
        public Frm_PPB()
        {
            InitializeComponent();
        }
        byte Comand = 0;                    //номер команды
        Int16 cntMain = 0;                  //счетчик массива команд 
        ushort cntTry = 0;                  //количество попыток обращения
        byte numByte = 0;                   //

        public byte[] dataReceived;
        //const byte numberOfCommands = 33;
        int[] err_array = new int[100];     //массив для запоминания ошибок
        byte cntErr = 0;                    //счетчик для массива ошибок
        public bool FL_NORMA = true;

        private void btnStartPPB_Click(object sender, EventArgs e)
        {
            btnStartPPB.Enabled = false;
            var starter = Device.Port_finder();
            if (starter)
            {
                Tray_PPB.Items.Add($"{DateTime.Now} \t\t КПрА подключен к "+ Device.Port.PortName);
                //this.Invoke();
            }
            else
            {
                Tray_PPB.Items.Add($"{DateTime.Now} \t\t КПрА НЕ подключен!");
                var result = MessageBox.Show("КПрА не подключен!", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Retry)
                {
                    cntTry += 1;
                    btnStartPPB_Click(btnStartPPB,null);
                }
                if (result == DialogResult.Cancel)
                {
                    Close();
                    new Frm_start().Show();
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        
            
    }
}
