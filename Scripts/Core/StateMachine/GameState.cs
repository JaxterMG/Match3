using Core.Config;
using Core.Drawer.CellElement;
using Core.Drawer.Field;
using Core.Field;
using Core.Input.Detection;
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

        private bool _isInitalized = false;

        private readonly InputController _inputController;
        private readonly ClickDetector _clickDetector;

        private readonly FieldDrawer _fieldDrawer;
        private readonly ContainerDrawer _containerDrawer;

        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager contentManager) : base(game, graphicsDevice, contentManager)
        {
            _fieldDrawer = new FieldDrawer();
            _containerDrawer = new ContainerDrawer();
            _inputController = new InputController();
            _graphicsDevice = graphicsDevice;
            _contentManager = contentManager;
            _game = game;

            GameConfig.CellSize = 32;
            GameConfig.GameFieldSize = 8;
            GameConfig.WindowSize = new Vector2(_graphicsDevice.Viewport.Width, _graphicsDevice.Viewport.Height);
            GameConfig.FieldContainerSize = new Vector2(400, 400);
            GameConfig.CellPadding = (int)GameConfig.FieldContainerSize.Length() / GameConfig.GameFieldSize;
            GameConfig.SelectSizeModifier = 2;

            _gameField = new GameField(GameConfig.GameFieldSize, GameConfig.GameFieldSize);
            _clickDetector = new ClickDetector(_gameField);
            _inputController.OnMouseClick += _clickDetector.CheckRectangle;

            _containerDrawer.SetupRectangle();

            _cellTexture = _contentManager.Load<Texture2D>("Sprites/Square");
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
            _containerDrawer.Draw(spriteBatch, _cellTexture, new Vector2(_containerDrawer.FieldContainerRect.X, _containerDrawer.FieldContainerRect.Y), (int)GameConfig.FieldContainerSize.X);

            _fieldDrawer.Draw(_gameField, spriteBatch, _cellTexture);
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            _inputController.CheckMouseInput();
        }
    }
}