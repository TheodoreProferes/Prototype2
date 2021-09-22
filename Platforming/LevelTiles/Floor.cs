using System;
using System.Collections.Generic;
using System.Text;
using SFML.Audio;
using SFML.System;
using SFML.Graphics;
using SFML.Window;

namespace Platforming
{
    public class Floor : LevelTile
    {
        public Floor(string imagePath, float friction, Vector2f LeftCorner) : base(imagePath, friction, LeftCorner)
        {
            
        }

        public override void Initialise()
        {
            Sprite = new Sprite(Texture);
            Centre = new Vector2f ( Sprite.GetGlobalBounds().Width / 2, Sprite.GetGlobalBounds().Height / 2 );
            Sprite.Origin = Centre;
            Sprite.Position = LeftCorner;
            
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
