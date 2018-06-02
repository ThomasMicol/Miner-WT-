﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinerGame
{
    class Wall
    {
        public Texture2D Sprite;
        public bool Active;
        public Vector2 Position;

        public int Width
        {
            get { return Sprite.Width; }
        }

        public int Height
        {
            get { return Sprite.Height; }
        }

        public void Initialize(Texture2D sprite, Vector2 position)
        {
            Sprite = sprite;
            Position = position;
            Active = true;
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, Position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}