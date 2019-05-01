using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeMatrixes
{
    public partial class FMain : Form
    {
        /*Matrice First = new Matrice(8, 5);
        Matrice Seconde = new Matrice(5, 8);*/
        StockDeMatrices stock = new StockDeMatrices();

        public FMain() {
            InitializeComponent();

            Controls.Add(stock);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void BtnAddition_Click(object sender, EventArgs e)
        {
            //First.AddMatrice(Seconde);

        }

        private void BtnSoustraction_Click(object sender, EventArgs e)
        {
            //First.SubstractMatrice(Seconde);
        }

        private void BtnMultiplier_Click(object sender, EventArgs e)
        {
            //First.MultiplyMatrice(Seconde);
        }

        
        private void UpdownSize_ValueChanged(object sender, EventArgs e)
        {
            /* btn plus
        {
        Largeur text box +=5;

            reveal matrice(largeur textbox)

            location of matrices = 5+ First.width+20 + Seconde.width +20 + ....*/
            //prévoir des caps de taille de matrice ou trucs du genre?

        }

        private void UpdownSizeFont_ValueChanged(object sender, EventArgs e)
        {
            //modifier les valeurs des polices
            //prévoir un cap de taille de police?
        }

        private void CheckedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}