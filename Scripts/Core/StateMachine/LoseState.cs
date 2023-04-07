using System;
using Core.Config;
using Core.Drawer.CellElement;
using Core.Drawer.Field;
using Core.Drawer.Timer;
using Core.Field;
using Core.Input.Detection;
using Core.Timer;
using Match3;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Core.StateMachine.Game
{
    public class LoseState : State
    {
        private readonly Texture2D _cellTexture;
        private readonly GameField _gameField;

        private readonly TimerDrawer _timerDrawer;
        private readonly FieldDrawer _fieldDrawer;
        private readonly ContainerDrawer _containerDrawer;

        private readonly TimerLogic _timerLogic;

        public LoseState(Game1 game, GraphicsDevice graphicsDevice, ContentManager contentManager) : base(game, graphicsDevice, contentManager)
        {
            _graphicsDevice = graphicsDevice;
            _contentManager = contentManager;
            _game = game;

            _cellTexture = _contentManager.Load<Texture2D>("Sprites/Square");
            LoadContent();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            _timerDrawer.DrawText(spriteBatch,((int)Math.Round(_timerLogic.Timer)).ToString(), Color.Black);

            _containerDrawer.Draw(spriteBatch, _cellTexture, new Vector2(_containerDrawer.FieldContainerRect.X, _containerDrawer.FieldContainerRect.Y), (int)GameConfig.FieldContainerSize.X);

            _fieldDrawer.Draw(_gameField, spriteBatch, _cellTexture);
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void LoadContent()
        {
            _timerDrawer.LoadFont(_contentManager);
            
        }
    }
}