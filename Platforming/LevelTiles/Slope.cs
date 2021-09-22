using System;
using System.Collections.Generic;
using System.Text;
using SFML.Audio;
using SFML.System;
using SFML.Graphics;
using SFML.Window;

namespace Platforming
{
    public class Slope : LevelTile
    {
        public float Tilt
        {
            get;
            private set;
        }

        
        public FloatRect Dimensions
        {
            get;
            private set;
        }
        public FloatRect Bounds
        {
            get;
            private set;
        }
        public Slope(string imagePath, float friction, Vector2f LeftCorner, float tilt) : base(imagePath, friction, LeftCorner)
        {
            Tilt = tilt;
            
        }

        public override void Initialise()
        {
            Sprite = new Sprite(Texture){Position = LeftCorner};
            Dimensions = Sprite.GetGlobalBounds();
            Sprite.Rotation = Tilt;
            Bounds = Sprite.GetGlobalBounds();
            Centre = new Vector2f ((Bounds.Width / 2) + Bounds.Left, (Bounds.Height / 2)+Bounds.Top );
            
        }
        public override void Render(GameLoop gameLoop)
        {
            gameLoop.Window.Draw(Sprite);
        }

        public override void Update()
        {

        }
    }
}

