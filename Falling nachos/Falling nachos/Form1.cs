namespace Falling_nachos
{
    public partial class Form1 : Form
    {

        //Zadeklarovanie premennych

        bool    movementLeft, movementRight;
        int     skore, lives = 5, movementSpeed = 10, nachoFallSpeed = 5, missingNachos = 1, cislo = 1, scaleNumber = 5;
        int     scaleNumberPlus = 5;

        Label PrehraLabel = new Label()
        {
            Name = "PrehraLabel1",
            Text = "Prehral si!!!! \n pre restart hry stlac tlacidlo 'R'.",
            Location = new Point(116, 94),
            TextAlign = ContentAlignment.MiddleCenter,
            Font = new System.Drawing.Font("Showcard Gothic", 14.25F),
            AutoSize = true
        };

    //Generovanie 'nahodnych' cisel 

    Random randomY = new Random();
        Random randomX = new Random();


        List<PictureBox> nachos = new List<PictureBox>();

        private void makeNachos()
        {
            PictureBox nacho = new PictureBox()
            {
                Name = "nacho" + cislo.ToString(),
                Size = new Size(70, 60),
                Tag = "enemyNacho",
                BackColor = Color.Transparent,
                Image = Properties.Resources.nacho1,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Location = new Point(randomX.Next(0,(ClientSize.Width - 60)),randomY.Next(randomY.Next(1, 250)) * -1)
            };
            nachos.Add(nacho);
            this.Controls.Add(nacho);
        }
        public Form1()
        {
            InitializeComponent();
            restartGame();
            makeNachos();
            this.Controls.Add(PrehraLabel);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void keyIsDown(object sender, KeyEventArgs e)
        {
            
            // Kontrolovanie tlacidla ktore bolo stlacene

            if (e.KeyCode == Keys.A)
            {
                movementLeft = true;
                
            }
            if (e.KeyCode == Keys.D)
            {
                movementRight = true;
                
            }

        }

        private void keyIsUp(object sender, KeyEventArgs e)
        {

            // Kontrolovanie tlacidla ktore bolo pustene

            if (e.KeyCode == Keys.A)
            {
                movementLeft = false;
            }
            if (e.KeyCode == Keys.D)
            {
                movementRight = false;
            }
            if(e.KeyCode == Keys.R)
            {
                restartGame();
            }
            }

            private void gameTimerTick(object sender, EventArgs e)
            {
                skoreLabel.Text = "skore: " + skore;
                livesLabel.Text = "Zivoty: " + lives;
                
                while(missingNachos != 1)
            {
                makeNachos();
                missingNachos -= 1;
                cislo += 1;
            }

            if(skore >= scaleNumber)
            {
                nachoFallSpeed += 1;
                missingNachos += 1;
                scaleNumber += scaleNumberPlus;
            }

                foreach (Control x in this.Controls)
                {
                    if(x is PictureBox && (string)x.Tag == "enemyNacho")
                    {
                        x.Top += nachoFallSpeed;
                    if (Player.Bounds.IntersectsWith(x.Bounds))
                        {
                            lives -= 1;
                            this.Controls.Remove(x);
                            missingNachos += 1;
                        }
                    
                    }
                    if(x.Top >= 740)
                {    
                    x.Top = randomY.Next( 1, 250) * -1;     
                    skore += 1;
                    scaleNumberPlus += 1;
                    this.Controls.Remove(x);
                    missingNachos += 1;

                }
                    
                }
            {
            }

                if(lives == 0)
                {
                    endgame();
                }


                if(movementLeft == true &&  Player.Left > 0 )
                    {
                        Player.Image = Properties.Resources.player_left2;
                        Player.Left -= movementSpeed;
                    }
                if (movementRight == true && (Player.Left + Player.Width) < ClientSize.Width)
                    {
                        Player.Image = Properties.Resources.player_right2;
                        Player.Left += movementSpeed;
                    }
            }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    private void endgame()
    {
        gameTimer.Stop();
            PrehraLabel.Visible = true;
    }
        private void restartGame()
        {
            PrehraLabel.Visible = false;
            nachos.Clear();
            
            nachoFallSpeed = 5;
            missingNachos = 1;
            scaleNumber = 5;
            skore = 0;
            lives = 5;
            scaleNumberPlus = 5;

            Player.Left = (ClientSize.Width / 2) - (Player.Width / 2);

            foreach (Control x in Controls)
            {
                if (x is PictureBox && (string)x.Tag == "enemyNacho")
                {
                    x.Top = randomY.Next(1 , 250) * -1;
                    x.Left = randomX.Next(0, (ClientSize.Width - Player.Width));
                }
            }

            gameTimer.Enabled = true;
           
        }
    }
}