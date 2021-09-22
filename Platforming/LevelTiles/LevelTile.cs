using System;
using System.Collections.Generic;
using System.Text;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
namespace Platforming
{
    public abstract class LevelTile
    {
        public Vector2f Centre
        {
            get;
            protected set;
        }
        public Texture Texture
        {
            get;
            protected set;
        }
        public float Friction
        {
            get;
            protected set;
        }
        public Vector2f LeftCorner
        {
            get;
            protected set;
        }

        public Sprite Sprite
        {
            get;
            protected set;
        }

        protected LevelTile(string imagePath, float friction, Vector2f leftCorner)
        {
            Texture = new Texture(imagePath) { Smooth = true };
            Friction = friction;
            LeftCorner = leftCorner;
            
        }
        public abstract void Initialise();
        public abstract void Update();
        public abstract void Render(GameLoop game);
    }
}
