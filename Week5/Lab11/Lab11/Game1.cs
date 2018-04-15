using ExplodingTeddies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Lab11
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public const int WindowWidth = 800;
        public const int WindowHeight = 600;

        TeddyBear teddybear;

        Explosion explosion;

        Random random = new Random();

        // keeping track of the previous state of each button
        ButtonState leftButton = ButtonState.Released;
        ButtonState rightButton = ButtonState.Released;
        ButtonState middleButton = ButtonState.Released;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = WindowWidth;
            graphics.PreferredBackBufferHeight = WindowHeight;

            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            teddybear = new TeddyBear(Content, WindowWidth, WindowHeight, @"graphics\teddybear", random.Next(0, WindowWidth), random.Next(0, WindowHeight), new Vector2((float)random.NextDouble(), (float)random.NextDouble()));

            explosion = new Explosion(Content, @"graphics\explosion");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // get mouse state
            MouseState mouse = Mouse.GetState();

            // click left button on the teddy bear to make it explode
            if (mouse.LeftButton == ButtonState.Pressed
                && leftButton == ButtonState.Released
                && teddybear.Active
                && teddybear.DrawRectangle.Contains(mouse.X, mouse.Y))
            {
                leftButton = ButtonState.Pressed;

                explosion.Play(teddybear.DrawRectangle.Center.X, teddybear.DrawRectangle.Center.Y);

                teddybear.Active = false;
            }

            // track the previous state
            if (mouse.LeftButton == ButtonState.Released)
            {
                leftButton = ButtonState.Released;
            }

            // click right button to replace the current teddy bear
            if (mouse.RightButton == ButtonState.Pressed && rightButton == ButtonState.Released)
            {
                rightButton = ButtonState.Pressed;

                teddybear = new TeddyBear(Content, WindowWidth, WindowHeight, @"graphics\teddybear", random.Next(0, WindowWidth), random.Next(0, WindowHeight), new Vector2((float)random.NextDouble(), (float)random.NextDouble()));
            }

            // track the previous state
            if (mouse.RightButton == ButtonState.Released)
            {
                rightButton = ButtonState.Released;
            }

            // click middle button to explode the curren teddy bear and replace to new one
            if (mouse.MiddleButton == ButtonState.Pressed && middleButton == ButtonState.Released)
            {
                middleButton = ButtonState.Pressed;

                if (teddybear.Active)
                {
                    explosion.Play(teddybear.DrawRectangle.Center.X, teddybear.DrawRectangle.Center.Y);
                }

                teddybear.Active = false;

                teddybear = new TeddyBear(Content, WindowWidth, WindowHeight, @"graphics\teddybear", random.Next(0, WindowWidth), random.Next(0, WindowHeight), new Vector2((float)random.NextDouble(), (float)random.NextDouble()));
            }

            // track the previous state
            if (mouse.MiddleButton == ButtonState.Released)
            {
                middleButton = ButtonState.Released;
            }

            teddybear.Update(gameTime);
            explosion.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            // draw teddy bears
            teddybear.Draw(spriteBatch);

            // draw the explosion
            explosion.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
