namespace _292_verification
{
    partial class Frm_numOfDevice
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.numDevice = new System.Windows.Forms.Label();
            this.numberOfDevice_text = new System.Windows.Forms.TextBox();
            this.btnEnter = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.numDevice);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(702, 100);
            this.panel1.TabIndex = 0;
            // 
            // numDevice
            // 
            this.numDevice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numDevice.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.numDevice.Location = new System.Drawing.Point(0, 0);
            this.numDevice.Name = "numDevice";
            this.numDevice.Size = new System.Drawing.Size(702, 100);
            this.numDevice.TabIndex = 0;
            this.numDevice.Text = "Введите заводской номер прибора:";
            this.numDevice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numberOfDevice_text
            // 
            this.numberOfDevice_text.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.numberOfDevice_text.Location = new System.Drawing.Point(71, 105);
            this.numberOfDevice_text.Margin = new System.Windows.Forms.Padding(5);
            this.numberOfDevice_text.Name = "numberOfDevice_text";
            this.numberOfDevice_text.Size = new System.Drawing.Size(230, 46);
            this.numberOfDevice_text.TabIndex = 1;
            // 
            // btnEnter
            // 
            this.btnEnter.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnEnter.Location = new System.Drawing.Point(353, 105);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(134, 46);
            this.btnEnter.TabIndex = 2;
            this.btnEnter.Text = "Ввод";
            this.btnEnter.UseVisualStyleBackColor = true;
            this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
            this.btnEnter.MouseEnter += new System.EventHandler(this.btnEnter_MouseEnter);
            this.btnEnter.MouseLeave += new System.EventHandler(this.btnEnter_MouseLeave);
            // 
            // Frm_numOfDevice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 173);
            this.Controls.Add(this.btnEnter);
            this.Controls.Add(this.numberOfDevice_text);
            this.Controls.Add(this.panel1);
            this.Name = "Frm_numOfDevice";
            this.Text = "numOfDevice";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label numDevice;
        private System.Windows.Forms.TextBox numberOfDevice_text;
        private System.Windows.Forms.Button btnEnter;
    }
}