using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeMatrixes
{
    class StockDeMatrices : Panel  //Donc c'est un panel qui contient d'autres panel. Le but est de différencier le panel "de sauvegarde" et le panel "créatif"
    {
        public int maximumWidth=1800; //TODO : obtenir maxwidth du form p-e
        public void Display() //TODO : Fix bug, la première matrice est overlapped. Je crois aussi que y a un souci plus général (les "changements" semblent arriver une matrice trop tard)
        {
            int maxWidth = 1500; //TODO : linker à maxwidth du form et retirer le creator de matrices p-e

            int lineHeight = 0; 
            int currentLeft = 15;
            int currentTop = 15;
            foreach(Matrice proute in stock)
            {
                
                if ((currentLeft + proute.Width +10 ) > maxWidth) //si "currentLeft" est trop large, bah on reset tout et on passe à la ligne suivante
                {
                    currentLeft = 15;
                    currentTop += lineHeight +30;
                    lineHeight = 30;
                }
                if (stock.IndexOf(proute)==1) //TODO : correctif temporaire
                {
                    currentLeft += proute.Width/2;
                }
                proute.Left = currentLeft;
                currentLeft += proute.Width + 10; //on ajoute au "currentLeft" la largeur de la matrice et 10
                if (lineHeight < proute.Height) //on compare la hauteur de ligne à la hauteur de la nouvelle matrice
                {
                    lineHeight = proute.Height;
                }
                proute.Top = currentTop;
                
            }
        }
        class CreatorOfMatrixPanel : Panel //Contient les quelques boutons minimums requis pour créer une nouvelle matrice vierge
        {
            public enum MatrixNames
            {
                A = 1, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z
            }
            int number = 0;
            int DefaultCreationWidth=55;
            public string getkeyname()
            {
                number++;
                return ((MatrixNames)number).ToString();
            }

            public Matrice returnMatrice(string input)
            {
                int index = stock.FindIndex(Matrice => Matrice.nameOfInstance.Equals(input, StringComparison.Ordinal));

                return stock[index];
            }
            
            public class listsofformula
            {
                List<Matrice> listeMatrice;
                List<string> listeOperators;


                public listsofformula(List<Matrice> listeMatrice2, List<string> listeOperators2)
                {
                    this.listeMatrice = listeMatrice2;
                    this.listeOperators = listeOperators2;
                }
                public Matrice SimplifyFormula(listsofformula listes, int etape)
                {
                    int step = etape; //sépare entre multi/add/sub
                    var testx = false; //dès qu'on a fait une multi/add/sub, ça passe à true

                    if (listes.listeOperators.Count == 0)
                    {
                        return listes.listeMatrice[0]; //fonction récursive, le but c'est de renvoyer ça
                    }
                    else
                    {

                        List<Matrice> listMatrice2 = new List<Matrice>(); //liste qu'on renverra récursivement
                        List<string> operatorsAsStrings2 = new List<string>(); //liste qu'on renverra récursivement
                        foreach (string opera in listes.listeOperators)
                        {
                            switch (step)
                            {
                                case 1: //MULTIPLICATION
                                    if (testx==false && opera.Contains('*'))
                                    {
                                        testx = true;
                                        var IIndex = listeOperators.IndexOf(opera);
                                        listMatrice2.Add((listes.listeMatrice[IIndex] as Matrice) * (listes.listeMatrice[IIndex + 1] as Matrice));
                                    }
                                    else
                                    {
                                        var IIndex = listeOperators.IndexOf(opera);
                                        listMatrice2.Add(listes.listeMatrice[IIndex]);
                                        operatorsAsStrings2.Add(listes.listeOperators[IIndex]);
                                    }
                                    break;
                                case 2: //ADDITION
                                    if (testx == false && opera.Contains('+'))
                                    {
                                        testx = true;
                                        var IIndex = listeOperators.IndexOf(opera);
                                        listMatrice2.Add((listes.listeMatrice[IIndex] as Matrice) + (listes.listeMatrice[IIndex + 1] as Matrice));
                                    }
                                    else
                                    {
                                        var IIndex = listeOperators.IndexOf(opera);
                                        listMatrice2.Add(listes.listeMatrice[IIndex]);
                                        operatorsAsStrings2.Add(listes.listeOperators[IIndex]);
                                    }
                                    break;
                                case 3: //SOUSTRACTION
                                    if (testx == false && opera.Contains('-'))
                                    {
                                        testx = true;
                                        var IIndex = listeOperators.IndexOf(opera);
                                        listMatrice2.Add((listes.listeMatrice[IIndex] as Matrice) - (listes.listeMatrice[IIndex + 1] as Matrice));

                                    }
                                    else
                                    {
                                        var IIndex = listeOperators.IndexOf(opera);
                                        listMatrice2.Add(listes.listeMatrice[IIndex]);
                                        operatorsAsStrings2.Add(listes.listeOperators[IIndex]);
                                    }
                                    break;
                            }                            
                        }
                        if (testx==false)
                        {
                            step++;
                        }
                        listsofformula listes2 = new listsofformula(listMatrice2, operatorsAsStrings2);
                        return(SimplifyFormula(listes2, step));
                    }
                }
                

            }
            public Matrice EvaluateFormula(string input)
            {

                List<Matrice> tampon = new List<Matrice>();
                


                string[] operands = input.Split('+', '-', '*', '/', '(', ')');
                string[] operatorsasstring = input.Split(operands,StringSplitOptions.RemoveEmptyEntries);

                List<String> operators = operatorsasstring.ToList();

                foreach (string matriceEnDevenir in operands)
                {
                    tampon.Add(returnMatrice(matriceEnDevenir));
                }

                listsofformula listToSimplify = new listsofformula(tampon, operators);

                return listToSimplify.SimplifyFormula(listToSimplify,1); ;
            }


            public CreatorOfMatrixPanel() // Constructeur 
            {
                this.BackColor = Color.FromArgb(255, 255, 255);
                this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                this.AutoSize = true;
                this.AutoSizeMode = AutoSizeMode.GrowAndShrink;

                CheckBox ZeroMatrix = new CheckBox();
                ZeroMatrix.Checked = true;
                ZeroMatrix.Top = 10;
                ZeroMatrix.Left = 10;
                ZeroMatrix.AutoSize = true;
                ZeroMatrix.Text = "Full 0:";

                CheckBox IdentityMatrix = new CheckBox();
                IdentityMatrix.Checked = false;
                IdentityMatrix.Top = 30;
                IdentityMatrix.Left = 10;
                IdentityMatrix.AutoSize = true;
                IdentityMatrix.Text = "Identité :";
                Controls.Add(IdentityMatrix);

                CheckBox RandomMatrix = new CheckBox();
                RandomMatrix.Checked = false;
                RandomMatrix.Top = 50;
                RandomMatrix.Left = 10;
                RandomMatrix.AutoSize = true;
                RandomMatrix.Text = "Random :";
                Controls.Add(RandomMatrix);

                CheckBox RandomMatrixNeg = new CheckBox();
                RandomMatrixNeg.Checked = false;
                RandomMatrixNeg.Top = 50;
                RandomMatrixNeg.Left = 90;
                RandomMatrixNeg.AutoSize = true;
                RandomMatrixNeg.Text = "Neg";
                Controls.Add(RandomMatrixNeg);

                CheckBox RandomMatrixInt = new CheckBox();
                RandomMatrixInt.Checked = true;
                RandomMatrixInt.Top = 50;
                RandomMatrixInt.Left = 140;
                RandomMatrixInt.AutoSize = true;
                RandomMatrixInt.Text = "Int";
                Controls.Add(RandomMatrixInt);

                TextBox RandomMatrixRange = new TextBox();
                RandomMatrixRange.Text = "10";
                RandomMatrixRange.Top = 50;
                RandomMatrixRange.Left = 190;
                RandomMatrixRange.Width = 60;
                Controls.Add(RandomMatrixRange);

                CheckBox EquationMatrix = new CheckBox();
                EquationMatrix.Checked = false;
                EquationMatrix.AutoSize = true;
                EquationMatrix.Top = 90;
                EquationMatrix.Left = 10;
                EquationMatrix.AutoSize = true;
                EquationMatrix.Text = "A partir d'une équation :";
                Controls.Add(EquationMatrix);

                Button btnOpen = new Button();
                btnOpen.Top = 90;
                btnOpen.Left = 200;
                btnOpen.Text = "Import data";
                btnOpen.Click += (sender, e) =>
                {
                    var fileContent = string.Empty;
                    var filePath = string.Empty;
                    using (OpenFileDialog openFileDialog = new OpenFileDialog())
                    {
                        openFileDialog.InitialDirectory = "C:\\Users\\ASUS ROG\\Documents\\Programmation - cours\\";
                        openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                        openFileDialog.FilterIndex = 2;
                        openFileDialog.RestoreDirectory = true;

                        if (openFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            //Get the path of specified file
                            filePath = openFileDialog.FileName;

                            //Read the contents of the file into a stream
                            var fileStream = openFileDialog.OpenFile();

                            using (StreamReader reader = new StreamReader(fileStream))
                            {
                                fileContent = reader.ReadToEnd();
                            }
                            string[] input = fileContent.Split('M');

                            List<string> differentesmatrices = input.ToList();
                            foreach (string entrant in differentesmatrices)
                            {
                                MessageBox.Show(entrant);
                                Matrice proute = new Matrice(entrant, this.DefaultCreationWidth);
                                Controls.Add(proute);

                            }

                        }
                    }

                    MessageBox.Show(fileContent, "File Content at path: " + filePath, MessageBoxButtons.OK);
                };
                Controls.Add(btnOpen);


                TextBox Equation = new TextBox();
                Equation.Text = "text";
                Equation.Top = 115;
                Equation.Left = 10;
                Equation.ReadOnly = true;
                Equation.AutoSize = true;
                Controls.Add(Equation);

                NumericUpDown updownSize = new NumericUpDown();

                updownSize.Location = new System.Drawing.Point(200, 240);
                updownSize.Name = "updownSize";
                updownSize.Size = new System.Drawing.Size(120, 20);
                updownSize.TabIndex = 6;
                updownSize.Value = 55;
                updownSize.Minimum = 30;
                updownSize.Increment = 5;
                updownSize.Maximum = 120;

                updownSize.ValueChanged += (sender, e) =>
                {
                    foreach (Matrice proute in stock)
                    {
                        proute.RevealMatrice(Convert.ToInt32((sender as NumericUpDown).Value));
                    }
                    (this.Parent as StockDeMatrices).Display();
                    this.DefaultCreationWidth = Convert.ToInt32((sender as NumericUpDown).Value);
                };
                Controls.Add(updownSize);

                /*NumericUpDown updownSizeFont = new NumericUpDown();
                updownSizeFont.Location = new System.Drawing.Point(200, 270);
                updownSizeFont.Name = "updownSizeFont";
                updownSizeFont.Size = new System.Drawing.Size(120, 20);
                updownSizeFont.TabIndex = 7;
                //updownSizeFont.ValueChanged += new System.EventHandler(this.UpdownSizeFont_ValueChanged);
                Controls.Add(updownSizeFont);*/

                Button saveAll = new Button();
                saveAll.Location = new System.Drawing.Point(200, 270);
                saveAll.Text = "Sauver data";
                saveAll.Click += (sender, e) =>
                {
                    List<String> lines = new List<String>();
                    foreach (Matrice proute in stock)
                    {
                        lines.Add(proute.SaveAllData());

                    }

                    System.IO.StreamWriter file = new System.IO.StreamWriter("C:\\Users\\ASUS ROG\\Documents\\Programmation - cours\\testalldata.txt");
                    foreach (string proute in lines)
                    {
                        file.WriteLine(proute);
                    }
                    

                    file.Close();
                };
                Controls.Add(saveAll);

                /*
                CheckBox blankMatrix = new CheckBox;
                blankMatrix.Checked = true;
                blankMatrix.Top = 10;
                blankMatrix.Left = 10;
                blankMatrix.Text = "Vierge :";
                blankMatrix.Click += (sender, e) =>
                {

                }
                Controls.Add(blankMatrix);*/


                ZeroMatrix.Click += (sender, e) =>
                {
                    ZeroMatrix.Checked = true;
                    IdentityMatrix.Checked = false;
                    RandomMatrix.Checked = false;
                    EquationMatrix.Checked = false;
                };
                Controls.Add(ZeroMatrix);

                IdentityMatrix.Click += (sender, e) =>
                {
                    ZeroMatrix.Checked = false;
                    IdentityMatrix.Checked = true;
                    RandomMatrix.Checked = false;
                    EquationMatrix.Checked = false;
                };

                RandomMatrix.Click += (sender, e) =>
                {
                    ZeroMatrix.Checked = false;
                    IdentityMatrix.Checked = false;
                    RandomMatrix.Checked = true;
                    EquationMatrix.Checked = false;
                };

                EquationMatrix.Click += (sender, e) =>
                {
                    ZeroMatrix.Checked = false;
                    IdentityMatrix.Checked = false;
                    RandomMatrix.Checked = false;
                    EquationMatrix.Checked = true;
                };
                EquationMatrix.CheckedChanged += (sender, e) =>
                {
                    if (EquationMatrix.Checked == true)
                    {
                        Equation.ReadOnly = false;
                    }
                    else
                    {
                        Equation.ReadOnly = true;
                    }
                       
                };
                



                NumericUpDown nUDLine = new NumericUpDown(); //deux numupdown qui contiennent les valeurs ligne et colonne de la future matrice
                nUDLine.Value = 5;
                nUDLine.Minimum = 1;
                nUDLine.Maximum = 20;
                nUDLine.Top = 10;
                nUDLine.Left = 120;
                nUDLine.Width = 35;
                Controls.Add(nUDLine);

                Label LignesXCol = new Label();
                LignesXCol.Text = "Lignes x \nColonnes";
                LignesXCol.AutoSize = true;
                LignesXCol.BackColor = Color.FromArgb(255, 255,255);
                LignesXCol.Top = 8;
                LignesXCol.Left = 180;
                LignesXCol.Width = 120;
                Controls.Add(LignesXCol);

                NumericUpDown nUDCol = new NumericUpDown();
                nUDCol.Value = 5;
                nUDCol.Minimum = 1;
                nUDCol.Maximum = 20;
                nUDCol.Top = 10;
                nUDCol.Left = 250;
                nUDCol.Width = 35;
                Controls.Add(nUDCol);


                Button btnNewMatrix = new Button(); //crée une nouvelle matrice en prenant les variables lignes et colonnes des num updown
                btnNewMatrix.Text = "New";
                btnNewMatrix.Width = 80;
                btnNewMatrix.Top = 150;
                btnNewMatrix.Left = 0;
                btnNewMatrix.BackColor = Color.FromArgb(50, 205, 50);
                btnNewMatrix.Click += (sender, e) => //TODO : fix defaultwidth qui se fait pas
                {
                    //proute.RandomizeMatrix(); //Test
                    if (EquationMatrix.Checked == true)
                    {
                        Matrice proute = new Matrice(this.DefaultCreationWidth, EvaluateFormula(Equation.Text));
                        Controls.Add(proute);
                        (this.Parent as StockDeMatrices).addStock(proute);
                        (this.Parent as StockDeMatrices).Display();
                        proute.Parent = this.Parent;
                    }
                    else
                    {
                        Matrice proute = new Matrice((int)nUDLine.Value, (int)nUDCol.Value, getkeyname(), this.DefaultCreationWidth);
                        (this.Parent as StockDeMatrices).addStock(proute);
                        (this.Parent as StockDeMatrices).Display();

                        Controls.Add(proute);
                        proute.Parent = this.Parent;

                        if (ZeroMatrix.Checked == true)
                        {
                            proute.FullZero();
                        }
                        if (IdentityMatrix.Checked == true)
                        {
                            proute.IdentityMatrice();
                        }
                        if (RandomMatrix.Checked == true)
                        {
                            Int32.TryParse(RandomMatrixRange.Text, out var range);
                            proute.RandomizeMatrix(RandomMatrixNeg.Checked, RandomMatrixInt.Checked, range);
                        }
                    }


                };
                Controls.Add(btnNewMatrix);

                /*Button btnAddition = new Button();
                btnAddition.Text = "MAT1 + MAT2";
                btnAddition.Width = 150;
                btnAddition.Top = 150;
                btnAddition.Left = 0;
                btnAddition.BackColor = Color.FromArgb(50, 205, 50);
                /*btnAddition.Click += (sender,e)=>
                {
                    string mat1 = '';
                    string mat2 = '';
                    foreach (Matrice proute in stock)
                    {
                        proute.Click += (sender,e)=>
                        {
                            if (string.IsNullOrEmpty(mat1))
                            {
                                mat1 = (sender as Matrice).name;
                            }
                            else
                            {
                                mat2 = (sender as Matrice).name;

                            }
                        }
                    }
                }

                Controls.Add(btnAddition);*/



            }

        }
        public static List<Matrice> stock = new List<Matrice>(); // liste de matrices à laquelle on rajoute ou supprime les matrices
        public void addStock(Matrice matrice) // fonction d'ajout à la liste de matrices
        {
            stock.Insert(0,matrice);
            Display();
        }

        //TODO : créer une fonction de suppression de matrices dans stock, et un "réorganisateur" des matrices dans la liste pour bootstraper le truc
        public StockDeMatrices() // Constructeur 
        {
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.BackColor = Color.FromArgb(30, 144, 224);
            

            /*Matrice First = new Matrice(4, 4,"B",55);
            First.RandomizeMatrix();
            addStock(First);
            Controls.Add(First);

            Matrice Seconde = new Matrice(4, 4, "A" ,55);
            Seconde.RandomizeMatrix();
            addStock(Seconde);
            Controls.Add(Seconde);

            Matrice Third = new Matrice(55, First * Seconde);
            addStock(Third);
            Controls.Add(Third);*/

            CreatorOfMatrixPanel pirouette = new CreatorOfMatrixPanel();

            Controls.Add(pirouette);
            pirouette.Left = maximumWidth - pirouette.Width;
            pirouette.Top = 15;

            Display();
        }
    }
}
