using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CodeMatrixes
{      
    class Matrice : Panel //ça c'est une matrice + son panneau de config (les 3/4 opérations applicables à une seule matrice)
    {
        public int lignes;
        public int colonnes;
        public double[,] matriceitself;
        public string nameOfInstance;
        public int largeurDeBoite=55;
        class ControlMatrice : Panel // le panneau de config = affiche quelques boutons qui font des opés sur la matrice
        {
            public ControlMatrice(string nameOfParent, int lig, int col) // constructeur
            {
                this.MinimumSize = new Size(130, 20);
                this.AutoSize = true;
                this.AutoSizeMode = AutoSizeMode.GrowAndShrink;

                Label mName = new Label();
                mName.AutoSize = true;
                mName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                mName.Location = new System.Drawing.Point(0, 0);
                mName.Size = new System.Drawing.Size(135, 31);
                mName.Text = nameOfParent; //TODO : fix
                Controls.Add(mName);

                Label mLignesCol = new Label();
                mLignesCol.AutoSize = true;
                mLignesCol.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                mLignesCol.Location = new System.Drawing.Point(3, 35);
                mLignesCol.Size = new System.Drawing.Size(90,125);
                mLignesCol.Text = lig + "L x " + col + "C";
                Controls.Add(mLignesCol);


                Button btnIdentity = new Button(); //transforme la matrice en matrice unité
                btnIdentity.Text = "MI";
                btnIdentity.Width = 35;
                btnIdentity.Left = 30;
                btnIdentity.Click += (sender, e) =>
                {
                    (this.Parent as Matrice).IdentityMatrice();
                };
                Button btnAllZero = new Button(); //transforme la matrice en matrice remplie de zéros
                btnAllZero.Text = "M0";
                btnAllZero.Width = 35;
                btnAllZero.Left = 66;
                btnAllZero.Click += (sender, e) =>
                {
                    (this.Parent as Matrice).FullZero();
                };
                Button btnRandom = new Button(); //transforme la matrice en matrice remplie de zéros
                btnRandom.Text = "MR";
                btnRandom.Width = 35;
                btnRandom.Left = 102;
                btnRandom.Click += (sender, e) =>
                {
                    (this.Parent as Matrice).RandomizeMatrix(false,false, 10);
                };
                Button btnTranspose = new Button(); //applique une transposée à la matrice
                btnTranspose.Text = "MT";
                btnTranspose.Width = 35;
                btnTranspose.Left = 138;
                btnTranspose.Click += (sender, e) =>
                {
                    (this.Parent as Matrice).TransposeMatrice();
                };

                TextBox txtScalaire = new TextBox();
                txtScalaire.Text = "3";
                txtScalaire.Width = 25;
                txtScalaire.Left = 205;
                Controls.Add(txtScalaire);

                Button btnScalaire = new Button(); //applique une transposée à la matrice
                btnScalaire.Text = "SCA";
                btnScalaire.Width = 35;
                btnScalaire.Left = 170;
                btnScalaire.Click += (sender, e) =>
                {
                    Int32.TryParse(txtScalaire.Text, out var scalaire);
                    (this.Parent as Matrice).MultiplyMatrice(scalaire);
                };
                Controls.Add(btnScalaire);




                Button btnCopy = new Button(); //TODO : copie-colle une matrice
                btnCopy.Text = "x2";
                btnCopy.BackColor = System.Drawing.Color.LimeGreen;
                btnCopy.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                btnCopy.Width = 35;
                btnCopy.Left = 284;
                btnCopy.Click += (sender, e) =>
                {
                    Matrice Copy = new Matrice((this.Parent as Matrice).largeurDeBoite, this.Parent as Matrice);
                    Copy.nameOfInstance += "Copy";
                    ((this.Parent as Matrice).Parent as StockDeMatrices).addStock(Copy);

                    //StockDeMatrices.Display();
                };
                Button btnDelete = new Button(); //Supprime une matrice
                btnDelete.Text = "X";

                btnDelete.Width = 35;
                btnDelete.Left = 340;
                //btnDelete.Anchor = AnchorStyles.Right;
                btnDelete.BackColor = Color.FromArgb(255, 255, 255);
                btnDelete.Click += (sender, e) =>
                {
                    (this.Parent as Matrice).Dispose();
                    //((this.Parent as Matrice).Parent as StockDeMatrices).Display(); //TODO : FIX this shit
                };

                Button determinant = new Button(); //TODO : crée un lbl qui appelle la fonction déterminant de la matrice
                determinant.Text = "Det";
                determinant.Width = 35;
                determinant.Left = 244;
                determinant.BackColor = Color.FromArgb(255, 255, 255);
                determinant.Click += (sender, e) =>
                {
                    determinant.Text = (this.Parent as Matrice).DeterminantMatrice((this.Parent as Matrice)).ToString();
                };

                Button btnSaveData = new Button(); //TODO : crée un lbl qui appelle la fonction déterminant de la matrice
                btnSaveData.Text = "Save";
                btnSaveData.Top = 35;
                btnSaveData.Left = 314;
                //btnSaveData. = Color.FromArgb(255, 255, 255);
                btnSaveData.Click += (sender, e) =>
                {
                    (this.Parent as Matrice).SaveData();
                };
                Controls.Add(btnSaveData);



                this.Controls.Add(btnIdentity);
                this.Controls.Add(btnAllZero);
                this.Controls.Add(btnRandom);
                this.Controls.Add(btnTranspose);
                this.Controls.Add(btnCopy);
                this.Controls.Add(btnDelete);
                this.Controls.Add(determinant);

            }
        }

        public List<List<ElementMatrice>> mylist = new List<List<ElementMatrice>>(); //"matrice" en elle-même. Composée de multiples textbox (elementmatrice)

        public void SetParameters(int lig, int col, string namekey, int largeur)  //Ces paramètres étaient définis dans les deux constructeurs, et pis c'était pas propre
        {
            this.lignes = lig;
            this.colonnes = col;
            this.nameOfInstance = namekey;
            this.largeurDeBoite = largeur;

            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.BackColor = Color.FromArgb(224, 224, 224);

            ControlMatrice controles = new ControlMatrice(nameOfInstance, lignes, colonnes); //crée un panneau de config
            this.Controls.Add(controles);

            this.matriceitself = new double[lignes,colonnes];
        }
        public void ConvertorTextToMat()
        {
            for (var i = 0; i < lignes; i++)
            {
                for (var j = 0; j < colonnes; j++)
                {
                    matriceitself[i, j] = mylist[i][j].GetValue();
                }
            }
        }
        public Matrice(int lig, int col, string namekey, int largeur) //constructeur basique : a juste besoin de savoir combien de lignes et colonnes 
        {
            SetParameters(lig, col, namekey, largeur);

            for (var i = 0; i < lignes; i++) // TODO : passer ça dans une fonction?
            {
                List<ElementMatrice> tampon = new List<ElementMatrice>();
                for (var j = 0; j < colonnes; j++)
                {
                    tampon.Add(new ElementMatrice(1, i+1, j+1));
                }
                mylist.Add(tampon);
            } 
            RevealMatrice(largeur);//TODO rajouter paramètre : largeur des edits + rajouter deuxième : taille police
        }
        public Matrice(int largeur, Matrice matricecopiee) //constructeur de copie, on passe en plus une "matrice". Y a un souci, copymatrice/addmatrice?
        {


            SetParameters(matricecopiee.lignes, matricecopiee.colonnes, matricecopiee.nameOfInstance, largeur);

            for (var i = 0; i < lignes; i++) // TODO : passer ça dans une fonction?
            {
                List<ElementMatrice> tampon = new List<ElementMatrice>();
                for (var j = 0; j < colonnes; j++)
                {
                    tampon.Add(new ElementMatrice(matricecopiee.mylist[i][j].GetValue(), i + 1, j + 1)); //matricecopiee.mylist[i][j].GetValue()
                }
                mylist.Add(tampon);
            }

            RevealMatrice(largeur);
        }
        public Matrice (string input, int largeur)
        {
            MessageBox.Show(input);
            Boolean start = false;
            Boolean next = false;
            List<string> values = new List<string>();
            string value="";
            foreach (char c in input)
            {

                if (c.Equals('µ'))
                {
                    start = true;
                    continue;
                }
                if (start == true)
                {
                    if (c.Equals('{'))
                    {
                        next = true;
                        continue;
                    }
                    if (c.Equals('}'))
                    {
                        next = false;
                        continue;
                    }
                    if (next == true)
                    {
                        if (c.Equals(' '))
                        {
                            
                        }
                        else
                        {
                            value += c;
                        }
                        if (c.Equals(';'))
                        {
                            MessageBox.Show(value);
                            values.Add(value);
                            value = "";
                        }
                    }
                }
            }

            int lignes = 5;
            int colonnes = 5;
            string nomdelamat ="test" ;


            SetParameters(lignes, colonnes, nomdelamat, largeur);

            for (var i = 0; i < lignes; i++) // TODO : passer ça dans une fonction?
            {
                List<ElementMatrice> tampon = new List<ElementMatrice>();
                for (var j = 0; j < colonnes; j++)
                {
                    Double.TryParse(values[i + j], out double thisvalue);
                    tampon.Add(new ElementMatrice(thisvalue, i + 1, j + 1)); //matricecopiee.mylist[i][j].GetValue()
                }
                mylist.Add(tampon);
            }
        }
        public void RevealMatrice(int largeur) //TODO : Améliorer
        {
            var i = 0;
            foreach (List<ElementMatrice> line in mylist)
            {
                var j = 0;
                foreach (ElementMatrice elemMat in line)
                {
                    elemMat.Top = elemMat.Height * i + 2 + 60;
                    elemMat.Left = largeur * j + 30;
                    elemMat.Width = largeur;

                    if (elemMat.Parent == null)
                        this.Controls.Add(elemMat);
                    else
                        elemMat.Refresh();

                    this.Controls.Add(elemMat);
                    j++;
                }
                i++;
            }
            ConvertorTextToMat();
        }

        /*public List<List<ElementMatrice>> CopyMatrice(Matrice copiedMatrice) //TODO : utilité de copy sur add ?
        {
            List<List<ElementMatrice>> mylist2 = new List<List<ElementMatrice>>();

            for (var i = 0; i < lignes; i++)
            {
                List<ElementMatrice> tampon = new List<ElementMatrice>();
                for (var j = 0; j < colonnes; j++)
                {
                    tampon.Add(copiedMatrice.mylist[i][j]);
                }
                mylist.Add(tampon);
            }

            return mylist2;
        }*/

        public void AddMatrice(Matrice addedMatrice) //TODO : utilité de add sur copy?
        {
            List<List<ElementMatrice>> mylist2 = addedMatrice.mylist;

            for (var i = 0; i < lignes; i++)
            {
                for (var j = 0; j < colonnes; j++)
                {
                    double trick = (mylist[i][j] as ElementMatrice).GetValue();
                    trick += (mylist2[i][j] as ElementMatrice).GetValue();
                    (mylist[i][j] as ElementMatrice).SetValue(trick);
                }
            }
            RevealMatrice(largeurDeBoite);
        }
        public void SubstractMatrice(Matrice substractedMatrice) //Fonction requise : soustraction de matrices
        {
            List<List<ElementMatrice>> mylist2 = substractedMatrice.mylist;

            for (var i = 0; i < lignes; i++)
            {
                for (var j = 0; j < colonnes; j++)
                {
                    double trick = (mylist[i][j] as ElementMatrice).GetValue();
                    trick -= (mylist2[i][j] as ElementMatrice).GetValue();
                    (mylist[i][j] as ElementMatrice).SetValue(trick);
                }
            }
            RevealMatrice(largeurDeBoite);
        }
        public void TransposeMatrice() //Fonction sur self : fais la transposée
        {
            List<List<ElementMatrice>> mylist2 = new List<List<ElementMatrice>>();

            for (var i = 0; i < colonnes; i++)
            {
                List<ElementMatrice> tampon = new List<ElementMatrice>();
                for (var j = 0; j < lignes; j++)
                {
                    tampon.Add(mylist[j][i]);
                }
                mylist2.Add(tampon);
            }
            
            mylist = mylist2;
            var line = lignes;
            this.lignes = colonnes;
            this.colonnes = line;

            RevealMatrice(largeurDeBoite);
        }

        public static Matrice operator+ (Matrice a, Matrice b)
        {
            if (a.lignes == b.lignes && a.colonnes==b.colonnes)
            {
                string name = String.Format("{0}+{1}", a.nameOfInstance, b.nameOfInstance);
                Matrice c = new Matrice(a.largeurDeBoite, a);
                c.nameOfInstance = name;

                for (var i = 0; i < c.lignes; i++)
                {
                    for (var j = 0; j < c.colonnes; j++)
                    {
                        c.mylist[i][j].SetValue(c.mylist[i][j].GetValue() + b.mylist[i][j].GetValue());
                    }
                }
                return c;
            }
            else
            {
                MessageBox.Show("valeurs de lignes/colonnes imcompatibles");
                return a;
            }
            
                
        }
        public static Matrice operator -(Matrice a, Matrice b)
        {
            if (a.lignes == b.lignes && a.colonnes == b.colonnes)
            {
                string name = String.Format("{0}-{1}", a.nameOfInstance, b.nameOfInstance);
                Matrice c = new Matrice(a.largeurDeBoite, a);
                c.nameOfInstance = name;

                for (var i = 0; i < c.lignes; i++)
                {
                    for (var j = 0; j < c.colonnes; j++)
                    {
                        c.mylist[i][j].SetValue(c.mylist[i][j].GetValue() - b.mylist[i][j].GetValue());
                    }
                }
                return c;
            }
            else
            {
                MessageBox.Show("valeurs de lignes/colonnes imcompatibles");
                return a;                
            }
            

        }
        public static Matrice operator *(Matrice a, Matrice b)
        {
            if (a.colonnes==b.lignes)
            {
                string name = String.Format("{0}*{1}", a.nameOfInstance, b.nameOfInstance);
                Matrice c = new Matrice(a.largeurDeBoite, a);
                c.nameOfInstance = name;

                for (var i = 0; i < c.lignes; i++) //multiplication en tant que telle
                {
                    for (var j = 0; j < c.colonnes; j++)
                    {
                        double somme = 0;
                        for (var k = 0; k < c.colonnes; k++)
                        {
                            somme += a.mylist[i][k].GetValue() * b.mylist[k][j].GetValue();

                        }
                        c.mylist[i][j].SetValue(somme);
                    }
                }
                return c; 
            }
            else
            {
                MessageBox.Show("valeurs de lignes/colonnes imcompatibles");
                return a;
            }
            

        }





        public void MultiplyMatrice(int scalaire) //Fonction requise : multiplication de matrices
        {
            List<List<ElementMatrice>> mylist2 = new List<List<ElementMatrice>>(); //besoin d'une 3eme "matrice" tampon

            for (var i = 0; i < lignes; i++)
            {
                for (var j = 0; j < colonnes; j++)
                {
                    mylist[i][j].SetValue(mylist[i][j].GetValue() * scalaire);                        
                }
            }

            RevealMatrice(largeurDeBoite);
        }

        public void OperationGaussJordan() //TODO : fix this shit
        {
            //var flag = 0;
            double linechange = 0;
            double pro = 0;
            var c = 0;
            var k = 0;
            double buffer = 0;
            for (var i =0; i<lignes; i++)
            {
                if (mylist[i][i].GetValue() == 0)
                {
                    c = 1;
                    while(mylist[i+c][i].GetValue() == 0 && (i + c) < lignes)
                    {
                        c++;
                    }
                    if ((i+c)== lignes)
                    {
                        break;
                    }
                    for (k=0;k<= lignes; k++)
                    {
                        buffer = mylist[i][k].GetValue();
                        mylist[i][k].SetValue(mylist[i+c][k].GetValue());
                        mylist[i + c][k].SetValue(mylist[i][k].GetValue());
                    }
                }
                for (var j=0; j< lignes; j++)
                {
                    if(i!= j)
                    {
                        pro = mylist[j][i].GetValue() / mylist[i][i].GetValue();
                        for (k=0; k< lignes; k++)
                        {
                            linechange = mylist[j][k].GetValue()- mylist[i][k].GetValue() * pro;
                            mylist[j][k].SetValue(linechange);
                        }
                    }
                }
            }
            //return flag;
        }

        //public ConverterOf : faudra bien implémenter un jour 
        public double DeterminantMatrice(Matrice matrice)
        {
            double determinant = 0;
            
            if (matrice.lignes == 1)
            {
                determinant += matrice.mylist[0][0].GetValue();
            }
            else
            {
                for (var j = 0; j < matrice.colonnes; j++)
                {
                    Matrice d = new Matrice(matrice.largeurDeBoite, matrice);

                    d.mylist.RemoveAt(0);
                    for (var i = 0; i < matrice.lignes - 1; i++)
                    {
                        //MessageBox.Show(String.Format("ligne {0} col {1} : {2}", i.ToString(), j.ToString(), d.mylist[i][j].Text));
                        d.mylist[i].RemoveAt(j);
                    }
                    d.lignes -= 1;
                    d.colonnes -= 1;
                    determinant += Math.Pow(-1, j)*matrice.mylist[0][j].GetValue() * DeterminantMatrice(d);
                }
                

            }
            return determinant;
        }

        public void IdentityMatrice() //Fonction sur self : transforme la matrice en matrice identité
        {
            for (var i = 0; i<lignes; i++)
            {
                for (var j = 0; j<colonnes; j++)
                {
                    if (i==j)
                    {
                        (mylist[i][j] as ElementMatrice).SetValue(1);
                    }
                    else
                    {
                        (mylist[i][j] as ElementMatrice).SetValue(0);
                    }
                }
            }
        }
        public void FullZero() //Fonction sur self : remplis la matrice de zéros
        {
            for (var i = 0; i < lignes; i++)
            {
                for (var j = 0; j < colonnes; j++)
                {                    
                        (mylist[i][j] as ElementMatrice).SetValue(0);
                }
            }
        }
        public void RandomizeMatrix(bool negatif, bool entier, int range) //Fonction sur self : remplis la matrice de randoms
        {
            if (entier==false)
            {
                range *= 100;
            }
            int max = range;
            int min = 0;
            if (negatif ==true)
            {
                min = -range;
            }
            Random rnd = new Random();
            for (var i = 0; i < lignes; i++)
            {
                for (var j = 0; j < colonnes; j++)
                {                  
                    if (entier == false)
                        {
                        double proute = (double)rnd.Next(min, max) /100;
                        (mylist[i][j] as ElementMatrice).SetValue(proute);
                        }
                    else
                    {
                        (mylist[i][j] as ElementMatrice).SetValue(rnd.Next(min, max));
                    }
                    
                }
            }
        }
        public void SaveData() //Fonction sur self : remplis la matrice de randoms
        {
            string lines = "Matrice " + this.nameOfInstance + "; " + lignes + "lignes x " + colonnes + "colonnes \n\n";
            for (var i = 0; i < lignes; i++)
            {
                lines += "{";
                for (var j = 0; j < colonnes; j++)
                {
                    lines += " " + (mylist[i][j] as ElementMatrice).GetValue() + " " + ";";
                }
                lines += "}\n";
            }
            lines += "\n\n";
            MessageBox.Show(lines);

            // Write the string to a file.
            System.IO.StreamWriter file = new System.IO.StreamWriter("C:\\Users\\ASUS ROG\\Documents\\Programmation - cours\\test.txt");
            file.WriteLine(lines);

            file.Close();
        }
        public string SaveAllData() //Fonction sur self : remplis la matrice de randoms
        {
            string lines = "";
            lines += "Matrice " + this.nameOfInstance + "; " + lignes + "lignes x " + colonnes + "colonnes \n\nµ";
            for (var i = 0; i < lignes; i++)
            {
                lines += "\n{";
                for (var j = 0; j < colonnes; j++)
                {
                    lines += " " + (mylist[i][j] as ElementMatrice).GetValue() + " " + ";";
                }
                lines += "}";
            }
            lines += "£\n\n";

            return lines;
            
        }

        public Matrice ReducMatrice(Matrice d, int ligneASupp, int colASupp)
        {
            d.mylist.RemoveAt(ligneASupp);
            for (var i = 0; i < d.lignes - 1; i++)
            {
                d.mylist[i].RemoveAt(colASupp);
            }
            d.lignes -= 1;
            d.colonnes -= 1;

            return d;
        }

    }
}

//TODO : gauss-jordan (normalisation, soustraction), mais rajouter "la solution", le déterminant, et la matrice identité
//règle des mineurs, cofacteurs, inversion, transposée, matrice réduite