using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BrickBreaker
{
    public class Brick
    {
        public Texture2D tex;
        public Rectangle rect;
        public bool isAlive;
        public int health;
        public Color col;
        

        public Rectangle Pos
        {
            get { return rect; }
        }

        public Brick(Texture2D tex, Rectangle rect, Color col)
        {
            this.tex = tex;
            this.rect = rect;
            this.col = col;
            this.isAlive = true;
        }
        
        public void checkHealth()
        {
            if (health == 11)
            {
                col = Color.Gold;
            }
            if (health == 10)
            {
                col = Color.Silver;
            }
            if (health == 9)
            {
                col = Color.Indigo;
            }
            if (health == 8)
            {
                col = Color.BlueViolet;
            }
            if (health == 7)
            {
                col = Color.Blue;
            }
            if (health == 6)
            {
                col = Color.Green;
            }
            if (health == 5)
            {
                col = Color.GreenYellow;
            }
            if (health == 4)
            {
                col = Color.Yellow;
            }
            if (health == 3)
            {
                col = Color.Orange;
            }
            if (health == 2)
            {
                col = Color.OrangeRed;
            }
            if (health == 1)
            {
                col = Color.Red;
            }
            if (health <= 0)
            {
                isAlive = false;
                
             
            }
        }
       
        public void Draw(SpriteBatch spriteBatch)
        {
            if (isAlive)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(tex, rect, col);
                spriteBatch.End();
            }

            else if (!isAlive)
            {
                this.rect.Y = -100;
            }
        }
        public void CheckColor()
        {
            if (col == Color.Gold)
            {
                health = 11;
            }
            if (col == Color.Silver)
            {
                health = 10;
            }
            if (col == Color.Indigo)
            {
                health = 9;
            }
            if (col == Color.BlueViolet)
            {
                health = 8;
            }
            if (col == Color.Blue)
            {
                health = 7;
            }
            if (col == Color.Green)
            {
                health = 6;
            }
            if (col == Color.GreenYellow)
            {
                health = 5;
            }
            if (col == Color.Yellow)
            {
                health = 4;
            }
            if (col == Color.Orange)
            {
                health = 3;
            }
            if (col == Color.OrangeRed)
            {
                health = 2;
            }
            if (col == Color.Red)
            {
                health = 1;
            }
        }
    }
}
