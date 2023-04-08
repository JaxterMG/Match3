using Core.Config;
using Microsoft.Xna.Framework.Input;

namespace Core.Input.Detection
{
    public class InputController
    {
        bool _clicked = false;
        public event System.Action OnMouseClick;
        public void CheckMouseInput()
        {
            if(GameConfig.BlockInput)
                return;
                
            var mouseCurrentState = Mouse.GetState();

            if (mouseCurrentState.LeftButton == ButtonState.Pressed && !_clicked)
            {
                OnMouseClick?.Invoke();
                _clicked = true;
            }
            else if (mouseCurrentState.LeftButton == ButtonState.Released)
            {
                _clicked = false;
            }
        }
    }
}
