using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using TeddyMineExplosion;

namespace ProgrammingAssignment5
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

        // mine support
        Texture2D mineSprite;
        List<Mine> mines = new List<Mine>();

        // teddy support
        Texture2D teddybearSprite;
        List<TeddyBear> teddybears = new List<TeddyBear>();

        // explosion support
        Texture2D explosionSprite;
        List<Explosion> explosions = new List<Explosion>();

        // random support
        Random random = new Random();

        // spawn delay
        int elapsedTotalMilliseconds = 1000;
        int elapsedCooldownMilliseconds = 0;

        // click processing
        bool leftClickStart = false;
        bool leftButtonReleased = true;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // set window resolution
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

            // load mine sprite
            mineSprite = Content.Load<Texture2D>(@"graphics\mine");

            // load teddybeat sprite
            teddybearSprite = Content.Load<Texture2D>(@"graphics\teddybear");

            // explosion sprite
            explosionSprite = Content.Load<Texture2D>(@"graphics\explosion");
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

            MouseState mouse = Mouse.GetState();
            if (mouse.LeftButton == ButtonState.Pressed &&
                leftButtonReleased)
            {
                leftClickStart = true;
                leftButtonReleased = false;
            }
            else if (mouse.LeftButton == ButtonState.Released)
            {
                leftButtonReleased = true;

                if (leftClickStart)
                {
                    leftClickStart = false;

                    mines.Add(new Mine(mineSprite, mouse.X, mouse.Y));
                }
            }

            elapsedCooldownMilliseconds += gameTime.ElapsedGameTime.Milliseconds;
            if (elapsedCooldownMilliseconds > elapsedTotalMilliseconds)
            {
                elapsedCooldownMilliseconds = 0;
                elapsedTotalMilliseconds = random.Next(1000, 3000);

                // spawn new teddybear
                teddybears.Add(new TeddyBear(teddybearSprite, new Vector2((float)random.NextDouble(), (float)random.NextDouble()), WindowWidth, WindowHeight));
            }

            foreach (TeddyBear teddybear in teddybears)
            {
                foreach (Mine mine in mines)
                {
                    // check if teddybear collides with a mine
                    if (teddybear.CollisionRectangle.Intersects(mine.CollisionRectangle) &&
                        teddybear.Active &&
                        mine.Active)
                    {
                        teddybear.Active = false;
                        mine.Active = false;

                        // add an explosion when a collision occurs
                        Rectangle collisionRectangle = Rectangle.Intersect(teddybear.CollisionRectangle, mine.CollisionRectangle);
                        explosions.Add(new Explosion(explosionSprite, collisionRectangle.Center.X, collisionRectangle.Center.Y));
                    }
                }
            }

            // update and remove teddybears
            for (int i = teddybears.Count - 1; i >= 0; i--)
            {
                if (!teddybears[i].Active)
                {
                    teddybears.RemoveAt(i);
                }
                else
                {
                    teddybears[i].Update(gameTime);
                }
            }

            // remove mines
            for (int i = mines.Count - 1; i >= 0; i--)
            {
                if (!mines[i].Active)
                {
                    mines.RemoveAt(i);
                }
            }

            // update and remove explosions
            for (int i = explosions.Count - 1; i >= 0; i--)
            {
                if (!explosions[i].Playing)
                {
                    explosions.RemoveAt(i);
                }
                else
                {
                    explosions[i].Update(gameTime);
                }
            }

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
            foreach (Mine mine in mines)
            {
                mine.Draw(spriteBatch);
            }
            foreach (TeddyBear teddybear in teddybears)
            {
                teddybear.Draw(spriteBatch);
            }
            foreach (Explosion explosoin in explosions)
            {
                explosoin.Draw(spriteBatch);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
