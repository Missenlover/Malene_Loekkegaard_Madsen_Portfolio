using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafikProjekt1
{
    public class MoveLeftRightBehaviour(GameObject gameObject, Game window) : Behaviour(gameObject, window)
    {
        private readonly float moveSpeed = 1f;

        public override void Update(FrameEventArgs args)
        {
            KeyboardState input = window.KeyboardState;
            if (input.IsKeyDown(Keys.A))
            {
                gameObject.transform.Position.X -= moveSpeed * (float)args.Time;
            }
            if (input.IsKeyDown(Keys.D))
            {
                gameObject.transform.Position.X += moveSpeed * (float)args.Time;
            }
        }
    }
}