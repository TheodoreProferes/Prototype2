using System;
using System.Collections.Generic;
using System.Text;
using SFML.Graphics;
using SFML.Window;
using SFML.System;


namespace Platforming
{
    public abstract class GameLoop
    {
        public const int targetFps = 60;
        public const float timeUntilUpdate = 1f / targetFps;

        public RenderWindow Window
        {
            get;
            protected set;
        }

        public GameTime GameTime
        {
            get;
            protected set;
        }

        public Color WindowClearColour
        {
            get;
            protected set;
        }

        protected GameLoop(uint windowWidth, uint windowHeight, string windowTitle, Color windowClearColour)
        {
            this.WindowClearColour = windowClearColour;
            this.Window = new RenderWindow(new VideoMode(windowWidth, windowHeight), windowTitle);
            this.GameTime = new GameTime();
            Window.Closed += WindowClosed;
        }

        public void Run()
        {
            Load();
            Initialise();

            float totalTimeBeforeUpdate = 0f;
            float previousTimeElapsed = 0f;
            float deltaTime = 0f;
            float totalTimeElapsed = 0f;

            Clock clock = new Clock();

            while (Window.IsOpen)
            {
                Window.DispatchEvents();

                totalTimeElapsed = clock.ElapsedTime.AsSeconds();
                deltaTime = totalTimeElapsed - previousTimeElapsed;
                previousTimeElapsed = totalTimeElapsed;

                totalTimeBeforeUpdate += deltaTime;

                if (totalTimeBeforeUpdate >= timeUntilUpdate)
                {
                    GameTime.Update(totalTimeBeforeUpdate, clock.ElapsedTime.AsSeconds());
                    totalTimeBeforeUpdate = 0f;

                    Update(GameTime);

                    Window.Clear(WindowClearColour);
                    Render(GameTime);
                    Window.Display();

                }

            }

        }

        public abstract void Load();
        public abstract void Initialise();
        public abstract void Update(GameTime gameTime);
        public abstract void Render(GameTime gameTime);

        private void WindowClosed(object sender, EventArgs e)
        {
            Window.Close();
        }
    }
}