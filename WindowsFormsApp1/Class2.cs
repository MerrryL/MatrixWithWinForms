using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    class Class2 : Panel
    {
        private Label label1;
        private Button btnAddition;
        private Button btnInvert;
        private Button btnMulti;

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddition = new System.Windows.Forms.Button();
            this.btnMulti = new System.Windows.Forms.Button();
            this.btnInvert = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // btnAddition
            // 
            this.btnAddition.Location = new System.Drawing.Point(0, 0);
            this.btnAddition.Name = "btnAddition";
            this.btnAddition.Size = new System.Drawing.Size(75, 23);
            this.btnAddition.TabIndex = 0;
            this.btnAddition.Text = "+";
            this.btnAddition.UseVisualStyleBackColor = true;
            // 
            // btnMulti
            // 
            this.btnMulti.Location = new System.Drawing.Point(0, 0);
            this.btnMulti.Name = "btnMulti";
            this.btnMulti.Size = new System.Drawing.Size(75, 23);
            this.btnMulti.TabIndex = 0;
            this.btnMulti.Text = "X";
            this.btnMulti.UseVisualStyleBackColor = true;
            // 
            // btnInvert
            // 
            this.btnInvert.Location = new System.Drawing.Point(0, 0);
            this.btnInvert.Name = "btnInvert";
            this.btnInvert.Size = new System.Drawing.Size(75, 23);
            this.btnInvert.TabIndex = 0;
            this.btnInvert.Text = "-1";
            this.btnInvert.UseVisualStyleBackColor = true;
            this.ResumeLayout(false);

        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
