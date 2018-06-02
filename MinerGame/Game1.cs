﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace MinerGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public Rig player;
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;
        MouseState currentMouseState;
        MouseState previousMouseState;
        List<Wall> Walls = new List<Wall>();
        Cursor cursor;

        private Camera Camera;
        private List<Component> Components;
        public static int ScreenHeight;
        public static int ScreenWidth;

        Camera camera;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            SetWindowSize(graphics);
        }

        private void SetWindowSize(GraphicsDeviceManager graphics)
        {
            graphics.PreferredBackBufferWidth = 320;
            graphics.PreferredBackBufferHeight = 240;

            ScreenHeight = graphics.PreferredBackBufferHeight;
            ScreenWidth = graphics.PreferredBackBufferWidth;
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
            player = new Rig(Content.Load<Texture2D>("sprites/sHull_Base"));
            player.Position = new Vector2(0, 0);
            cursor = new Cursor();
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
            Vector2 rigPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height / 2);

            int wallCountWidth = graphics.PreferredBackBufferWidth / 16;
            int WallCountHeight = graphics.PreferredBackBufferHeight / 16;
            for (int i = 0; i < wallCountWidth; i ++)
            {
                for(int j = 0; j < WallCountHeight; j ++)
                {
                    Vector2 wallPosition = new Vector2(0 + (16 * i), 0 + (16 * j));
                    Wall wall = new Wall();
                    wall.Initialize(Content.Load<Texture2D>("sprites/sWall"), wallPosition);
                    Walls.Add(wall);
                }
            }

            Camera = new Camera();
            Components = new List<Component>()
            {
                player
            };
            cursor.Initialize(Content.Load<Texture2D>("sprites/sCursor"), new Vector2(0, 0));
            // TODO: use this.Content to load your game content here
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

            MouseState mousePos = Mouse.GetState();
            cursor.Update();
            // TODO: Add your update logic here
            currentKeyboardState = Keyboard.GetState();
            currentMouseState = Mouse.GetState();
            UpdateRig(gameTime);
            CollisionChecks();
            Camera.Follow(player);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(transformMatrix: Camera.Transform);
            foreach(Wall wall in Walls)
            {
                wall.Draw(spriteBatch);
            }
            player.Draw(gameTime, spriteBatch);
            cursor.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void UpdateRig(GameTime gameTime)
        {
            player.Move(currentKeyboardState);
        }

        private void CollisionChecks()
        {
            /*Rectangle PlayerHitMask_Body = new Rectangle((int)player.Position.X, (int)player.Position.Y, player.Width, player.Height);
            Rectangle MouseRectangle = new Rectangle(currentMouseState.X, currentMouseState.Y, cursor.Width, cursor.Height);
            if ( currentMouseState.LeftButton == ButtonState.Pressed) {
                if (PlayerHitMask_Body.Intersects(MouseRectangle))
                {
                    // player.Position.X += 32;
                }
            }

            foreach(Wall wall in Walls)
            {
                Rectangle wallHitBox = new Rectangle((int)wall.Position.X, (int)wall.Position.Y, wall.Width, wall.Height);
                if (wallHitBox.Intersects(PlayerHitMask_Body))
                {
                    //Walls.Remove(wall);
                    wall.Active = false;
                }
            }*/
        }
    }
}