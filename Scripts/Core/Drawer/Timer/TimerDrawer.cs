using System;
using System.Net.Mime;
using Core.Field;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Core.Drawer.Timer
{
    public class TimerDrawer : TextDrawer
    {
        private string _fontName;
        public override void Initialize(Vector2 screenPos)
        {
            base.Initialize(screenPos);
        }
        public override void LoadFont(ContentManager contentManager, string fontName = "Fonts/Arial")
        {
            if(String.IsNullOrEmpty(fontName))
            {
                _fontName = fontName;
            }
            base.LoadFont(contentManager, fontName);
        }


        public override void DrawText(SpriteBatch spriteBatch, string text, Color color)
        {

            base.DrawText(spriteBatch,text, color);

        }

        
    }
}