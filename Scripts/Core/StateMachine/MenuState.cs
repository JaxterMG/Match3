using System.Threading.Tasks;
using Core.Drawer;
using Core.Drawer.CellElement;
using Core.Input.Detection;
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
        private SimpleMessageDrawer _startMessageDrawer;
        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager contentManager, InputController inputController) : base(game, graphicsDevice, contentManager, inputController)
        {
            
            _inputController.OnMouseClick += CheckIfButtonPressed;

            _cellTexture = contentManager.Load<Texture2D>("Sprites/Square");
            _buttonDrawer = new ButtonDrawer();
            _buttonDrawer.InitializeScreenPos(new Vector2(_graphicsDevice.Viewport.Width / 2, _graphicsDevice.Viewport.Height / 2), new Vector2(200, 100));
            _startMessageDrawer = new SimpleMessageDrawer();
            _startMessageDrawer.LoadFont(_contentManager);
            _startMessageDrawer.Initialize
            (
                new Vector2(_buttonDrawer.ButtonRectangle.Center.X - _startMessageDrawer.MeasureText("Start").X / 2,
                _buttonDrawer.ButtonRectangle.Center.Y - _startMessageDrawer.MeasureText("Start").Y / 2)
            );     
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            _buttonDrawer.Draw(spriteBatch, _cellTexture, new Vector2(_buttonDrawer.ButtonRectangle.X, _buttonDrawer.ButtonRectangle.Y), _buttonDrawer.ButtonRectangle.Width);
            _startMessageDrawer.DrawText(spriteBatch, "Start", Color.Black);
            spriteBatch.End();
        }

        public override void LoadContent(){}

        public void CheckIfButtonPressed()
        {
            MouseState mouseState = Mouse.GetState();
            if (_buttonDrawer.ButtonRectangle.Contains(mouseState.Position))
            {
                _game.ChangeState(new GameState(_game, _graphicsDevice, _contentManager, _inputController));
            }
        }

        public override void Update(GameTime gameTime)
        {
            _inputController.CheckMouseInput();
        }

        public override void OnExit()
        {
            _inputController.OnMouseClick -= CheckIfButtonPressed;
        }
    }
}
