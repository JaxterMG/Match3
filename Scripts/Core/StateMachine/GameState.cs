using System;
using Core.Config;
using Core.Drawer;
using Core.Drawer.CellElement;
using Core.Drawer.Field;
using Core.Drawer.Timer;
using Core.Field;
using Core.Input.Detection;
using Core.Points;
using Core.Timer;
using Match3;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Core.StateMachine.Game
{
    public class GameState : State
    {
        private readonly Texture2D _cellTexture;
        private readonly GameField _gameField;
        private readonly InputController _inputController;
        private readonly ClickDetector _clickDetector;

        private readonly TimerDrawer _timerDrawer;
        private readonly FieldDrawer _fieldDrawer;
        private readonly ContainerDrawer _containerDrawer;

        private readonly TimerLogic _timerLogic;

        private SimpleMessageDrawer _simpleMessageDrawer;

        private bool _isInitalized = false;

        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager contentManager, InputController inputController) : base(game, graphicsDevice, contentManager, inputController)
        {
            PointsCounter.Points = 0;
            _fieldDrawer = new FieldDrawer();
            _containerDrawer = new ContainerDrawer();
            _inputController = new InputController();
            _simpleMessageDrawer = new SimpleMessageDrawer();
            
            GameConfig.CellSize = 32;
            GameConfig.GameFieldSize = 8;
            GameConfig.WindowSize = new Vector2(_graphicsDevice.Viewport.Width, _graphicsDevice.Viewport.Height);
            GameConfig.FieldContainerSize = new Vector2(400, 400);
            GameConfig.CellPadding = (int)GameConfig.FieldContainerSize.Length() / GameConfig.GameFieldSize;
            GameConfig.SelectSizeModifier = 2;

            _simpleMessageDrawer.Initialize(new Vector2(5, 8));
            _simpleMessageDrawer.LoadFont(_contentManager);

            _timerLogic = new TimerLogic(60);
            _timerLogic.OnTimerEnd += EndGame;
            _timerDrawer = new TimerDrawer();
            _timerDrawer.Initialize(new Vector2(GameConfig.WindowSize.X / 2, 8));

            _gameField = new GameField(GameConfig.GameFieldSize, GameConfig.GameFieldSize);
            _clickDetector = new ClickDetector(_gameField);
            _inputController.OnMouseClick += _clickDetector.CheckRectangle;

            _containerDrawer.SetupRectangle();

            _cellTexture = _contentManager.Load<Texture2D>("Sprites/Square");
            LoadContent();
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var padding = GameConfig.CellPadding;

            var cellSize = GameConfig.CellSize - padding;

            if (!_isInitalized)
            {
                _isInitalized = _fieldDrawer.InitializeScreenPos(_gameField, _containerDrawer.FieldContainerRect, cellSize);
                return;
            }


            spriteBatch.Begin();
            _simpleMessageDrawer.DrawText(spriteBatch, $"Score: {PointsCounter.Points}", Color.Black);
            _timerDrawer.DrawText(spriteBatch,((int)Math.Round(_timerLogic.Timer)).ToString(), Color.Black);

            _containerDrawer.Draw(spriteBatch, _cellTexture, new Vector2(_containerDrawer.FieldContainerRect.X, _containerDrawer.FieldContainerRect.Y), (int)GameConfig.FieldContainerSize.X);

            _fieldDrawer.Draw(_gameField, spriteBatch, _cellTexture);
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            _timerLogic.CountDown(gameTime);
            _inputController.CheckMouseInput();
        }

        public void EndGame()
        {
            _game.ChangeState(new LoseState(_game, _graphicsDevice, _contentManager, _inputController));
            _inputController.OnMouseClick -= _clickDetector.CheckRectangle;
        }

        public override void LoadContent()
        {
            _timerDrawer.LoadFont(_contentManager);
            
        }
    }
        
}