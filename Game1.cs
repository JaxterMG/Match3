using Core.Input.Detection;
using Core.StateMachine;
using Core.StateMachine.Menu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Match3
{
    public class Game1 : Game
    {
        private State _currentState;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private InputController _inputController;

        public void ChangeState(State state)
        {
            _currentState = state;
        }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;
            base.Initialize();
        }
        protected override void LoadContent()
        {
            _inputController = new InputController();
            _currentState = new MenuState(this, _graphics.GraphicsDevice, Content, _inputController);
            _currentState.LoadContent();
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _currentState.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _currentState.Draw(gameTime, _spriteBatch);
            base.Draw(gameTime);
        }
    }
}