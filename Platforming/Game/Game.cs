using System;
using System.Collections.Generic;
using System.Text;
using SFML.Audio;
using SFML.System;
using SFML.Graphics;
using SFML.Window;
namespace Platforming
{
    class Game : GameLoop
    {
        public const uint defaultWindowHeight = 1080;
        public const uint defaultWindowWidth = 1920;
        public const string defaultWindowTitle = "platforming";
        Floor floor;
        Slope slope;
        Player player;
        public Game() : base(defaultWindowWidth, defaultWindowHeight, defaultWindowTitle, Color.White)
        {

        }

        public override void Load()
        {
            floor = new Floor("./img/floor.png", 3,new Vector2f(0,522));
            slope = new Slope("./img/floor.png", 2, new Vector2f(640, 500), -30);
            player = new Player("./img/player.png");
        }

        public override void Initialise()
        {
            floor.Initialise();
            slope.Initialise();
        }

        public override void Update(GameTime gameTime)
        {
            
            player.Update();
            player.DetectCollision(floor.Sprite.GetGlobalBounds(), floor.Friction);
            player.DetectCollision(slope);

        }

        public override void Render(GameTime gameTime)
        {
            player.Render(this);
            floor.Render(this);
            slope.Render(this);
            
        }

    }
}
