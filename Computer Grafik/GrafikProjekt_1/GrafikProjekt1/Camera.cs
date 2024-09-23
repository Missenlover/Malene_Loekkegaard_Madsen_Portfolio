using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafikProjekt1
{
    public class Camera : Behaviour
    {
        private Vector3 front = new Vector3(0.0f, 0.0f, -1.0f);
        private Vector3 up = new Vector3(0.0f, 1.0f, 0.0f);
        private float speed = 1.0f;
        private float sensitivity = 0.05f;
        private float fov;
        private float aspectX;
        private float aspectY;
        private float near;
        private float far;

        private float yaw;
        private float pitch;
        private bool firstMove = true;
        private Vector2 lastPos;

        public Camera(GameObject gameObject, Game window, float fov, float aspectX, float aspectY, float near, float far)
            : base(gameObject, window)
        {
            gameObject.transform.Position = new Vector3(0.0f, 0.0f, 3.0f);
            this.fov = fov;
            this.aspectX = aspectX;
            this.aspectY = aspectY;
            this.near = near;
            this.far = far;
        }

        public override void Update(FrameEventArgs args)
        {
            HandleKeyboardInput(args);
            HandleMouseInput(args);
        }

        private void HandleKeyboardInput(FrameEventArgs args)
        {
            KeyboardState input = window.KeyboardState;

            if (input.IsKeyDown(Keys.Up))
            {
                gameObject.transform.Position += front * speed * (float)args.Time; // Forward
            }
            if (input.IsKeyDown(Keys.Down))
            {
                gameObject.transform.Position -= front * speed * (float)args.Time; // Backwards
            }
            if (input.IsKeyDown(Keys.Left))
            {
                gameObject.transform.Position -= Vector3.Normalize(Vector3.Cross(front, up)) * speed * (float)args.Time; // Left
            }
            if (input.IsKeyDown(Keys.Right))
            {
                gameObject.transform.Position += Vector3.Normalize(Vector3.Cross(front, up)) * speed * (float)args.Time; // Right
            }
            if (input.IsKeyDown(Keys.Space))
            {
                gameObject.transform.Position += up * speed * (float)args.Time; // Up 
            }
            if (input.IsKeyDown(Keys.LeftShift))
            {
                gameObject.transform.Position -= up * speed * (float)args.Time; // Down
            }
        }

        private void HandleMouseInput(FrameEventArgs args)
        {
            MouseState mouse = window.MouseState;


            if (window.IsFocused)
            {
                if (firstMove)
                {
                    lastPos = new Vector2(mouse.X, mouse.Y);
                    firstMove = false;
                }
                else
                {
                    float deltaX = mouse.X - lastPos.X;
                    float deltaY = mouse.Y - lastPos.Y;
                    lastPos = new Vector2(mouse.X, mouse.Y);

                    yaw += deltaX * sensitivity;
                    pitch -= deltaY * sensitivity;

                    // Clamp pitch to avoid camera flipping
                    if (pitch > 89.0f)
                    {
                        pitch = 89.0f;
                    }
                    else if (pitch < -89.0f)
                    {
                        pitch = -89.0f;
                    }

                }

                // Recalculate front vector based on new yaw and pitch
                front.X = (float)Math.Cos(MathHelper.DegreesToRadians(pitch)) * (float)Math.Sin(MathHelper.DegreesToRadians(yaw));
                front.Z = -(float)Math.Cos(MathHelper.DegreesToRadians(pitch)) * (float)Math.Cos(MathHelper.DegreesToRadians(yaw));
                front.Y = (float)Math.Sin(MathHelper.DegreesToRadians(pitch));
                front = Vector3.Normalize(front);
            }
        }

        public Matrix4 GetViewProjection()
        {
            //Matrix4 view2;
            //Matrix4.Invert(gameObject.transform.CalculateModel(), out view2);
            //Matrix4 view = view2
            Matrix4 view = Matrix4.LookAt(gameObject.transform.Position, gameObject.transform.Position + front, up);
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(fov), aspectX / aspectY, near, far);
            return view * projection;
        }
    }
}
