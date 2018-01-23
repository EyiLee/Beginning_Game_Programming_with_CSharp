using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ExplodingTeddies;

namespace Lab10
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        const int WindowWidth = 800;
        const int WindowHeight = 600;

        TeddyBear bear0;
        TeddyBear bear1;

        Explosion explosion;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // setting the window size
            graphics.PreferredBackBufferWidth = WindowWidth;
            graphics.PreferredBackBufferHeight = WindowHeight;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // load two teddybears
            bear0 = new TeddyBear(Content, WindowWidth, WindowHeight, @"graphics\teddybear0", 0, 100, new Vector2(1, 0));
            bear1 = new TeddyBear(Content, WindowWidth, WindowHeight, @"graphics\teddybear1", 800, 100, new Vector2(-1, 0));

            // load the explosion
            explosion = new Explosion(Content, @"graphics\explosion");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // move the teddybears
            bear0.Update(gameTime);
            bear1.Update(gameTime);

            // playing the explosion
            explosion.Update(gameTime);

            // detect the collision and play the explosion
            if (bear0.Active && bear1.Active && bear0.DrawRectangle.Intersects(bear1.DrawRectangle)) {
                bear0.Active = false;
                bear1.Active = false;

                // get the collision point of two teddybears
                Rectangle collisionRectangle = Rectangle.Intersect(bear0.DrawRectangle, bear1.DrawRectangle);
                explosion.Play(collisionRectangle.Center.X, collisionRectangle.Center.Y);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            // draw the two teddybears
            bear0.Draw(spriteBatch);
            bear1.Draw(spriteBatch);

            // draw the explosion
            explosion.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
