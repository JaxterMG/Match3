using Core.Input.Detection;
using Match3;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Core.StateMachine
{
    public abstract class State
    {
        protected GraphicsDevice _graphicsDevice;
        protected Game1 _game;
        protected ContentManager _contentManager;
        protected InputController _inputController;

        public State(Game1 game, GraphicsDevice graphicsDevice, ContentManager contentManager, InputController inputController)
        {
            _game = game;
            _graphicsDevice = graphicsDevice;
            _contentManager = contentManager;
            _inputController = inputController;
        }
        public abstract void Update(GameTime gameTime);
        public abstract void LoadContent();
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}