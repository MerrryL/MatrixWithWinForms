using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeMatrixes
{
    class ElementMatrice : TextBox //Element de base de la matrice
    {
        private double tag; //supprimer tag
        public int elLigne;
        public int elColonne;

        public ElementMatrice(double tag, int lig, int col) //todo : forcer le numérique (chiffres, .,, arrows, backspace delete)
        {
            this.tag = tag;
            this.Text = this.tag.ToString();
            this.Width = 50;
            this.Font = new Font(this.Font.FontFamily, 14);
            this.elLigne = lig;
            this.elColonne = col;

            /*rajouter un tooltip sur le form et le poser ici*/
            ToolTip TooltipElement = new ToolTip();
            TooltipElement.AutoPopDelay = 2500;
            TooltipElement.InitialDelay = 100;
            TooltipElement.ReshowDelay = 5000;
            TooltipElement.ShowAlways = true;

            TooltipElement.SetToolTip(this, "Ligne : " + this.elLigne.ToString() + ", " + "Colonne" + this.elColonne.ToString() + ":" + "\n" + this.Text);


            this.TextChanged += (sender, e) =>/* a virer et/ou modifier pour remplacer le tooltip*/
            {
                TooltipElement.SetToolTip(this, "Ligne : " + this.elLigne.ToString() + ", " + "Colonne" + this.elColonne.ToString() + "\n" + this.Text);
                // rajouter ligne et colonne dans le tooltip

            };

            this.KeyPress += (sender, e) =>
            {
                char separateur = (System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0]);

                if ((e.KeyChar == '.') || (e.KeyChar == ',')) //TODO : fix this shit
                {
                    if (this.Text.Contains('.') && this.Text.Contains(','))
                    {
                        e.Handled = true; 
                    }
                    else
                    {
                        e.KeyChar = separateur;
                    }
                }
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar == '.' && e.KeyChar == ',';
            };



        }
        public void SetValue(double value) //juste deux accesseurs pour gagner de l'espace
        {
            this.Text = value.ToString();
        }

        public double GetValue()
        {
            return (Double.Parse(this.Text));
        }
    }
}
