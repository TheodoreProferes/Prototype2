using System;
using System.Collections.Generic;
using System.Text;
using SFML.Audio;
using SFML.System;
using SFML.Graphics;
using SFML.Window;

namespace Platforming
{
    public class Player
    {
        private Texture texture;
        private Sprite player;
        private Vector2f Velocity = new Vector2f(0, 0);
        private float maxspeed = 500;
        private float movespeed = 5;
        private FloatRect PlayerBounds;
        private FloatRect Dimensions;
        private bool sloped = false;
        private bool jumpable = false;
        private bool PreviousJumpable = false;
        private float radiansTilt;
        private float ConstDist;
        private float tan;
        private float sin;
        private float cos;
        private Vector2f Centre;
        private float HalfLength;
        private float internalAngle;
        private float angleDifference;
        private float Displacement;
        public float height
        {
            get;
            private set;
        }
        public float width
        {
            get;
            private set;
        }
        public Player(string imgPath)
        {
            texture = new Texture(imgPath) { Smooth = true };
            player = new Sprite(texture);
            Dimensions = player.GetGlobalBounds();
            height = Dimensions.Height;
            width = Dimensions.Width;
            Centre = new Vector2f(width / 2, height / 2);
            HalfLength = (float)Math.Sqrt(Math.Pow(Centre.X, 2) + Math.Pow(Centre.Y, 2));
        }

        public void DetectCollision(FloatRect Floor, float friction)
        {
            if (PlayerBounds.Intersects(Floor))
            {
                PreviousJumpable = jumpable;
                player.Rotation = 0;
            if ((PlayerBounds.Top + PlayerBounds.Height <= Floor.Top + 50) && (PlayerBounds.Top + PlayerBounds.Height >= Floor.Top))
            {
                if ((PlayerBounds.Left > Floor.Left && PlayerBounds.Left < Floor.Left + Floor.Width) || (PlayerBounds.Left + PlayerBounds.Width > Floor.Left && PlayerBounds.Left + PlayerBounds.Width < Floor.Left + Floor.Width))
                {
                    if (Velocity.Y > 0)
                    {
                        jumpable = true;
                        Velocity.Y = 0;
                        player.Position = new Vector2f(player.Position.X, Floor.Top - PlayerBounds.Height);
                    }
                    else if ((Velocity.Y < 0) && (jumpable = true) && (PreviousJumpable == true))
                    {
                        Velocity.Y = 0;
                        player.Position = new Vector2f(player.Position.X, Floor.Top + Floor.Height);
                    }
                    Velocity.X -= friction * Math.Sign(Velocity.X);

                }
            }

            else if ((PlayerBounds.Top > Floor.Top && PlayerBounds.Top < Floor.Top + Floor.Height) || (PlayerBounds.Top + PlayerBounds.Height > Floor.Top && PlayerBounds.Top + PlayerBounds.Width < Floor.Top + Floor.Height))
            {
                if (Velocity.X > 0)
                {
                    Velocity.X = 0;
                    player.Position = new Vector2f(Floor.Left - PlayerBounds.Width, player.Position.Y);
                }
                else if (Velocity.X < 0)
                {
                    Velocity.X = 0;
                    player.Position = new Vector2f(Floor.Left + Floor.Width, player.Position.Y);
                }
                Velocity.Y -= friction * Math.Sign(Velocity.Y);
            }

        }
   

            }
        
        public void DetectCollision(Slope slope)
        {
            if (PlayerBounds.Intersects(slope.Bounds)){
                if (sloped == false)
                {
                    sloped = true;
                    player.Rotation = slope.Tilt;
                    radiansTilt = (float)(Math.Abs(slope.Tilt) * Math.PI / 180);
                    tan = (float)Math.Tan(radiansTilt);
                    sin = (float)Math.Sin(radiansTilt);
                    cos = (float)Math.Cos(radiansTilt);
                    ConstDist = height * cos;
                    internalAngle = (float)Math.Acos(((Math.Pow(HalfLength, 2) * 2) - Math.Pow(height, 2)) / (Math.Pow(HalfLength, 2) * 2));
                    angleDifference = (float)(Math.PI / 2 - radiansTilt - ((Math.PI - internalAngle) / 2));
                    Displacement = (float)(Math.Cos(angleDifference) * HalfLength);
                    player.Position -= new Vector2f(Displacement-(cos*width), 0);
                    
                }
                if (player.Position.Y >= ((float)(slope.LeftCorner.Y - (tan * (player.GetGlobalBounds().Left + (sin * height) - slope.LeftCorner.X)) - ConstDist)) && (player.Position.Y<=  ((float)(slope.LeftCorner.Y - (tan * (player.GetGlobalBounds().Left + (sin * height) - slope.LeftCorner.X)) - ConstDist)+(slope.Dimensions.Height/cos))))
                {
                    player.Position = new Vector2f(player.Position.X, (float)(slope.LeftCorner.Y - (tan * (player.GetGlobalBounds().Left + (sin * height) - slope.LeftCorner.X)) - ConstDist));
                    Velocity.X -= 3 * cos;
                    Velocity.X -= slope.Friction * Math.Sign(Velocity.X);
                    if (Keyboard.IsKeyPressed(Keyboard.Key.Up)) // jump
                    {
                            Velocity.Y = -300 * cos;
                        Velocity.X -= 300 * sin;
                        

                    }
                    else
                    {
                        Velocity.Y = 500;
                    }
                }
            }
            else
            {
                sloped = false;
            }
        }

        public void DetectCollision(Slope slope, bool down)
        {
            if (PlayerBounds.Intersects(slope.Bounds))
            {
                if (sloped == false)
                {
                    sloped = true;
                    player.Rotation = slope.Tilt;
                    radiansTilt = (float)(Math.Abs(slope.Tilt) * Math.PI / 180);
                    tan = (float)Math.Tan(radiansTilt);
                    sin = (float)Math.Sin(radiansTilt);
                    cos = (float)Math.Cos(radiansTilt);
                    ConstDist = height / cos;

                }
                player.Position = new Vector2f(player.Position.X, (float)(slope.LeftCorner.Y + (tan * (player.GetGlobalBounds().Left + (sin * height) - slope.LeftCorner.X)) - ConstDist));
            }
        }
            public void Update()
        {
            player.Position += Velocity / 60f;
            PlayerBounds = player.GetGlobalBounds();
            Velocity.Y += 10; //gravity

            if (Keyboard.IsKeyPressed(Keyboard.Key.Left)) // move left
            {
                if (Velocity.X > -maxspeed)
                {
                    Velocity.X -= movespeed;
                }
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Right)) // move right
            {
                if (Velocity.X < maxspeed)
                {
                    Velocity.X += movespeed;
                }
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Left)) // move left
            {
                if (Velocity.X > -maxspeed)
                {
                    Velocity.X -= movespeed;
                }
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Up)) // jump
            {
                if (jumpable == true && PreviousJumpable == true)
                {
                    jumpable = false;
                    Velocity.Y -= 300;
                }

            }
        }
        public void Render(GameLoop gameLoop)
        {
            gameLoop.Window.Draw(player);
        }
    }
}
