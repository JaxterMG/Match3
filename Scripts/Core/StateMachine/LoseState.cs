using Core.Config;
using Core.Drawer;
using Core.Drawer.CellElement;
using Core.Input.Detection;
using Core.StateMachine.Menu;
using Match3;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Core.StateMachine.Game
{
    public class LoseState : State
    {
        private readonly Texture2D _cellTexture;
        private SimpleMessageDrawer _gameOverMessageDrawer;
        private SimpleMessageDrawer _okMessageDrawer;
        private ButtonDrawer _buttonDrawer;

        public LoseState(Game1 game, GraphicsDevice graphicsDevice, ContentManager contentManager, InputController inputController) : base(game, graphicsDevice, contentManager, inputController)
        {
            _inputController.OnMouseClick += CheckIfButtonPressed;

            _buttonDrawer = new ButtonDrawer();
            _buttonDrawer.InitializeScreenPos(GameConfig.MiddlePoint, new Vector2(200, 100));

            _gameOverMessageDrawer = new SimpleMessageDrawer();
            _gameOverMessageDrawer.LoadFont(_contentManager);
            _gameOverMessageDrawer.Initialize
            (
                new Vector2(GameConfig.WindowSize.X / 2 - _gameOverMessageDrawer.MeasureText("Game Over").X / 2,
                GameConfig.WindowSize.Y / 3 - _gameOverMessageDrawer.MeasureText("Game Over").Y / 2)
             );

            _okMessageDrawer = new SimpleMessageDrawer();
            _okMessageDrawer.LoadFont(_contentManager);
            _okMessageDrawer.Initialize
            (
                new Vector2(_buttonDrawer.ButtonRectangle.Center.X - _okMessageDrawer.MeasureText("Ok").X / 2,
                _buttonDrawer.ButtonRectangle.Center.Y - _okMessageDrawer.MeasureText("Ok").Y / 2)
            );

            _cellTexture = _contentManager.Load<Texture2D>("Sprites/Square");
            LoadContent();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            _gameOverMessageDrawer.DrawText(spriteBatch, "Game Over", Color.Black);
            _buttonDrawer.Draw(spriteBatch, _cellTexture, new Vector2(_buttonDrawer.ButtonRectangle.X, _buttonDrawer.ButtonRectangle.Y), _buttonDrawer.ButtonRectangle.Width);
            _okMessageDrawer.DrawText(spriteBatch, "Ok", Color.Black);
            spriteBatch.End();
        }
        public void CheckIfButtonPressed()
        {
            MouseState mouseState = Mouse.GetState();
            if (_buttonDrawer.ButtonRectangle.Contains(mouseState.Position))
            {
                _game.ChangeState(new MenuState(_game, _graphicsDevice, _contentManager, _inputController));
                _inputController.OnMouseClick -= CheckIfButtonPressed;
            }
        }
        public async override void Update(GameTime gameTime)
        {
            _inputController.CheckMouseInput();
        }

        public override void LoadContent()
        {

        }
    }
}