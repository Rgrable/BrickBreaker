using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace BrickBreaker
{
    
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteFont font;
        SpriteBatch spriteBatch;
        Player player_1 = new Player();
        ball ball_1 = new ball();
        GUI GUI_1 = new GUI();
        float ballSpeed;
        bool playing;
        bool gameOver;
        // Bricks
        Texture2D bricksTex;
        const int BricksWide = 9;
        const int BricksHigh = 11;

        public int finishedAmount = 0;
        const int FinishAmount = 594;
        int healthScore = 10000;
        
        Brick[,] bricks;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 650;
            graphics.PreferredBackBufferHeight = 700;
            graphics.ApplyChanges();

            ballSpeed = 5;
            GUI_1.Lives = 3;
            GUI_1.Level = 1;
            
            base.Initialize();
        }
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load Player 1
            player_1.tex = Content.Load<Texture2D>("Paddle");
            player_1.pos = new Vector2(0, 675);
            player_1.sprite = new GameObject(player_1.tex, player_1.pos);
            player_1.rect = new Rectangle((int)player_1.pos.X, (int)player_1.pos.Y, player_1.tex.Width, player_1.tex.Height);

            // Load  ball
            ball_1.tex = Content.Load<Texture2D>("Ball");
            ball_1.pos = new Vector2(100, 600);
            ball_1.sprite = new GameObject(ball_1.tex, ball_1.pos,new Vector2(ballSpeed,-ballSpeed));
            ball_1.rect = new Rectangle((int)ball_1.pos.X, (int)ball_1.pos.Y, ball_1.tex.Width, ball_1.tex.Height);

            // Load Font
            font = Content.Load<SpriteFont>("Font");

            // Load GUI
            GUI_1.tex = Content.Load<Texture2D>("SideBar");
            GUI_1.pos = new Vector2(450, 0);


            // Load the Bricks image in the game
            bricksTex = Content.Load<Texture2D>("Brick");
            StartBricks();

        }

        public void NextLevelGame()
        {
            
            finishedAmount = 0;
            ballSpeed += 2;
            GUI_1.Level++;
            playing = false;
            
            LoadContent();
            StartBricks();
        }
        protected void GameOver()
        {
            if (gameOver)
            {
                
                GUI_1.Level = 1;
                finishedAmount = 0;
                playing = false;
                ballSpeed = 5;
                healthScore = 10000;
                LoadContent();
                StartBricks();
                gameOver = false;
            }
        }
        protected void ResetGame()
        {
            playing = false;
            ball_1.sprite.Velocity *= -1;
            GUI_1.Lives--;
        }
        private void StartBricks()
        {
            bricks = new Brick[BricksWide, BricksHigh];
            for (int y = 0; y < BricksHigh; y++)
            {
                Color col = Color.White;
                switch (y)
                {
                    case 0:
                        {
                            col = Color.Gold;
                            break;
                        }
                    case 1:
                        {
                            col = Color.Silver;
                            break;
                        }
                    case 2:
                        {
                            col = Color.Indigo;
                            break;
                        }
                    case 3:
                        {
                            col = Color.BlueViolet;
                            break;
                        }
                    case 4:
                        {
                            col = Color.Blue;
                            break;
                        }
                    case 5:
                        {
                            col = Color.Green;
                            break;
                        }
                    case 6:
                        {
                            col = Color.GreenYellow;
                            break;
                        }
                    case 7:
                        {
                            col = Color.Yellow;
                            break;
                        }
                    case 8:
                        {
                            col = Color.Orange;
                            break;
                        }
                    case 9:
                        {
                            col = Color.OrangeRed;
                            break;
                        }
                    case 10:
                        {
                            col = Color.Red;
                            break;
                        }
                }
                for (int x = 0; x < BricksWide; x++)
                {
                    bricks[x, y] = new Brick(bricksTex, new Rectangle(x * bricksTex.Width,
                                             y * bricksTex.Height, bricksTex.Width, bricksTex.Height),col);
                    bricks[x, y].CheckColor();
                }
                
            }
        }
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            BallMovement(gameTime);
            PlayerMovement(gameTime);
            CheckLives(gameTime);
            base.Update(gameTime);
        }

        protected void DrawText(GameTime gameTime)
        {
            
            spriteBatch.DrawString(font, "BRICK BREAK!", new Vector2(455, 5), Color.White);
            spriteBatch.DrawString(font, "SCORE: " + GUI_1.Score.ToString(), new Vector2(455, 35), Color.White);
            spriteBatch.DrawString(font, "LIVES: " + GUI_1.Lives.ToString(), new Vector2(455, 65), Color.White);
            spriteBatch.DrawString(font, "LEVEL: " + GUI_1.Level.ToString(), new Vector2(455, 95), Color.White);
            spriteBatch.DrawString(font, "Created By:\nRichard Grable\nrichardgrable.com", new Vector2(455, 620), Color.Orange);

            //Debug strings
           // spriteBatch.DrawString(font, "FINISHED: " + (finishedAmount / (FinishAmount / )).ToString() + "%", new Vector2(455, 125), Color.White);
            //spriteBatch.DrawString(font, "SPEED: " + ballSpeed.ToString(), new Vector2(455, 155), Color.White);


            if (!playing)
                spriteBatch.DrawString(font, "PRESS SPACE TO BEGIN", new Vector2(120, 350), Color.White);

            if (gameOver)
                spriteBatch.DrawString(font, "GAME OVER", new Vector2(210, 350), Color.White);
        }
        protected override void Draw(GameTime gameTime)
        {
            
            GraphicsDevice.Clear(Color.Black);

            // Draw Player 1
            spriteBatch.Begin();
            player_1.sprite.Draw(spriteBatch);
            spriteBatch.End();

            // Draw Player 1 Ball
            spriteBatch.Begin();
            ball_1.sprite.Draw(spriteBatch);
            spriteBatch.End();

            // Draw GUI
            spriteBatch.Begin();
            spriteBatch.Draw(GUI_1.tex, GUI_1.pos, Color.White);
            spriteBatch.End();

            spriteBatch.Begin();
            DrawText(gameTime);
            spriteBatch.End();
            

            // Draw all the bricks in game
            foreach (Brick brick in bricks)
            {
                brick.Draw(spriteBatch);
            }

                base.Draw(gameTime);
        }

        protected virtual void PlayerMovement(GameTime gameTime)
        {
            KeyboardState newKey = Keyboard.GetState();
            // Player 1
            // Auto Controller
            //player_1.sprite.Position.X = ball_1.sprite.Position.X;
            if (newKey.IsKeyDown(Keys.Left))
            {
                player_1.sprite.Position.X -= 8;
            }
            if (newKey.IsKeyDown(Keys.Right))
            {
                player_1.sprite.Position.X += 8;
            }

            if (player_1.sprite.Position.X < 0)
            {
                player_1.sprite.Position.X += 8;
            }
            if (player_1.sprite.Position.X >= 450 - player_1.tex.Width)
            {
                player_1.sprite.Position.X -= 8;
            }

            if (newKey.IsKeyDown(Keys.Space))
            {
                playing = true;
            }
            else if (newKey.IsKeyDown(Keys.Escape))
            {
                playing = false;
                
            }
        } 
        protected void BallMovement(GameTime gameTime)
        {
            if (playing)
            {
                ball_1.sprite.Position += ball_1.sprite.Velocity;
            }
            else
            {
                ball_1.sprite.Position = ball_1.pos;
            }
            // Ball 1
            if (ball_1.sprite.Position.Y < 0)
            {
                ball_1.sprite.Velocity.Y *= -1;
            }
            if (ball_1.sprite.Position.X < 0)
            {
                ball_1.sprite.Velocity.X *= -1;
            }
            if (ball_1.sprite.Position.X >= 450 - ball_1.tex.Width)
            {
                ball_1.sprite.Velocity.X *= -1;
            }
            if (ball_1.sprite.Position.Y >= GraphicsDevice.Viewport.Height - ball_1.tex.Height)
            {
                ResetGame();
            }
            foreach (Brick brick in bricks)
            {
                if (ball_1.sprite.BoundingBox.Intersects(brick.Pos))
                {
                    ball_1.sprite.Velocity.Y *= -1;
                    GUI_1.Score += 50;
                    brick.health -= 1;
                    brick.checkHealth();
                    finishedAmount++;
                    CheckForFinish();
                }
            }

            

                // Paddle Collisions
            if (ball_1.sprite.BoundingBox.Intersects(player_1.sprite.BoundingBox))
            {
                ball_1.sprite.Velocity.Y *= -1;
                
            }
        }

       protected virtual void CheckForFinish()
        {
            if (finishedAmount >= FinishAmount)
            {
                NextLevelGame();
            }
        }

        protected virtual void CheckLives(GameTime gameTime)
       {
           if (GUI_1.Lives < 1)
            {
                gameOver = true;
                GameOver();
            }

           if (GUI_1.Score >= healthScore)
           {
               GUI_1.Lives++;
               healthScore = healthScore + 10000;
           }
       }

        
    }

    struct Player
    {
        public GameObject sprite;
        public Vector2 pos;
        public Texture2D tex;
        public Rectangle rect;
    };

    struct ball
    {
        public GameObject sprite;
        public Vector2 pos;
        public Texture2D tex;
        public Rectangle rect;
    };

    struct GUI
    {
        public Texture2D tex;
        public Vector2 pos;
        public int Level;
        public int Score;
        public int Lives;

    };
}
