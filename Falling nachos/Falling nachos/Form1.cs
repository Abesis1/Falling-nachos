using System.Collections.Generic;

namespace Falling_nachos
{
    public partial class Form1 : Form
    {

        //Zadeklarovanie premennych

        bool    movementLeft, movementRight;
        int     skore, lives = 5, movementSpeed = 10, nachoFallSpeed = 5, missingNachos = 1, cislo = 1, scaleNumber = 5;
        int     scaleNumberPlus = 5, onionFallSpeed = 5;

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
        Random chance = new Random();


        List<PictureBox> nachos = new List<PictureBox>();
        List<PictureBox> onions = new List<PictureBox>();
        
        // Function to make new nachos

        private void makeOnion()
        {
            // Basic info and parameters for Heart

            PictureBox onionSprite = new PictureBox()
            {
                Name = "nacho" + (cislo * cislo).ToString(),
                Size = new Size(70, 60),
                Tag = "friendlyOnion",
                BackColor = Color.Transparent,
                Image = Properties.Resources.onion,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Location = new Point(randomX.Next(0, (ClientSize.Width - 60)), randomY.Next(randomY.Next(1, 250)) * -1)
            };


           
            onions.Add(onionSprite);
            this.Controls.Add(onionSprite);

            
             
        }
        
        
        
        private void makeNachos()
        {

            // Basic info and parameters of nacho

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
            
            // Adding nacho to the controls (game)
            
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
            
            // Controlling which key was pressed

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

            // Controlling which key was released

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
            
            // Updating score and lives counters

            {
                skoreLabel.Text = "skore: " + skore;
                livesLabel.Text = "Zivoty: " + lives;
                
                while(missingNachos != 1)
            {
                makeNachos();
                missingNachos -= 1;
                cislo += 1;
            }

                // If score is equal to number nacho fall speed is raised and also more nacho spawns after
                // Upon adding extra nacho, game have 50% chance to spawn onion which restores player lives

            if(skore >= scaleNumber)
            {
                nachoFallSpeed += 1;
                missingNachos += 1;
                scaleNumber += scaleNumberPlus;
                if(chance.Next(0,101) <= 50)
                {
                    makeOnion();
                }
                
            }

            // Controlling whether player touches falling nacho and if then nacho despawns and live is reduced

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

                

                // Controlling whether nacho is below game zone and if it is game will spawn new nacho at top

                    if(x.Top >= 740)
                {    
                    x.Top = randomY.Next( 1, 250) * -1;     
                    skore += 1;
                    scaleNumberPlus += 1;
                    this.Controls.Remove(x);
                    missingNachos += 1;

                }
                
            }

            // Controlling whether player touches onion and if he does then his lives rester to default value

            foreach (Control y in this.Controls)
            {
                if (y is PictureBox && (string)y.Tag == "friendlyOnion")
                {
                    y.Top += onionFallSpeed;
                    if (Player.Bounds.IntersectsWith(y.Bounds))
                    {
                        lives = 5;
                        this.Controls.Remove(y);
                    }
                }
            }
            {
            }

            // Controlling if lives == 0 so basically you died and if then game calls endgame function and stops

                if(lives == 0)
                {
                    endgame();
                }

                // Changing if player sprite is to be rotated to lef or righr depending on the button pressed


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
    
        // Endgame function
        
        private void endgame()
    {
        
            // Stopping everything and showing lose label with instructionst on how to restart game
            
            gameTimer.Stop();
            PrehraLabel.Visible = true;
    }
        private void restartGame()
        {
            
            // Setting values to default 

            PrehraLabel.Visible = false;
            nachos.Clear();
            
            nachoFallSpeed = 5;
            missingNachos = 1;
            scaleNumber = 5;
            skore = 0;
            lives = 5;
            scaleNumberPlus = 5;

            Player.Left = (ClientSize.Width / 2) - (Player.Width / 2);

            // Spawning first nacho in random position 

            foreach (Control x in Controls)
            {
                if (x is PictureBox && (string)x.Tag == "enemyNacho")
                {
                    x.Top = randomY.Next(1 , 250) * -1;
                    x.Left = randomX.Next(0, (ClientSize.Width - Player.Width));
                }
            }

            // Eneabling game timer which gets everything moving lul

            gameTimer.Enabled = true;
           
        }
    }
}