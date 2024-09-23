using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafikProjekt1
{
    public class MoveUpDownBehaviour : Behaviour
    {
        private float moveSpeed = 1f;

        public MoveUpDownBehaviour(GameObject gameObject, Game window) : base(gameObject, window) { }

        public override void Update(FrameEventArgs args)
        {
            KeyboardState input = window.KeyboardState;
            if (input.IsKeyDown(Keys.W))
            {
                gameObject.transform.Position.Y += moveSpeed * (float)args.Time;
            }
            if (input.IsKeyDown(Keys.S))
            {
                gameObject.transform.Position.Y -= moveSpeed * (float)args.Time;
            }
        }
    }
}
