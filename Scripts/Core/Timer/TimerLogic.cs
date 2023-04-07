using System;
using Microsoft.Xna.Framework;

namespace Core.Timer
{
    public class TimerLogic
    {
        public float Timer{get; private set;}
        
        public event Action OnTimerEnd;
        public TimerLogic(int time)
        {
            Timer = time;
        } 

        public void CountDown(GameTime gameTime)
        {
            Timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            if(Timer <= 0)
            {
                OnTimerEnd?.Invoke();
            }
        }

    }
}