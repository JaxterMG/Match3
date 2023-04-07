using Core.Drawer.CellElement;
using Core.StateMachine.Game;
using Match3;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Core.StateMachine.Menu
{
    public class MenuState : State
    {
        private readonly Texture2D _cellTexture;
        ButtonDrawer _buttonDrawer;
        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager contentManager) : base(game, graphicsDevice, contentManager)
        {
            _game = game;
            _graphicsDevice = graphicsDevice;
            _contentManager = contentManager;

            _cellTexture = contentManager.Load<Texture2D>("Sprites/Square");
            _buttonDrawer = new ButtonDrawer();
            _buttonDrawer.InitializeScreenPos(new Vector2(_graphicsDevice.Viewport.Width / 2, _graphicsDevice.Viewport.Height / 2), 200);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            _buttonDrawer.Draw(spriteBatch, _cellTexture, new Vector2(_buttonDrawer.ButtonRectangle.X, _buttonDrawer.ButtonRectangle.Y), _buttonDrawer.ButtonRectangle.Width);
            spriteBatch.End();
        }

        public override void LoadContent(){}

        public override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed && _buttonDrawer.ButtonRectangle.Contains(mouseState.Position))
            {
                _game.ChangeState(new GameState(_game, _graphicsDevice, _contentManager));
            }
        }
    }
}
