using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafikProjekt1
{
    class CubeMesh : Mesh
    {
        float[] vertices = {
             -0.5f, -0.5f, -0.5f, 0.0f, 0.0f,
             0.5f, -0.5f, -0.5f, 1.0f, 0.0f,
             0.5f, 0.5f, -0.5f, 1.0f, 1.0f,
             0.5f, 0.5f, -0.5f, 1.0f, 1.0f,
             -0.5f, 0.5f, -0.5f, 0.0f, 1.0f,
             -0.5f, -0.5f, -0.5f, 0.0f, 0.0f,
             -0.5f, -0.5f, 0.5f, 0.0f, 0.0f,
             0.5f, -0.5f, 0.5f, 1.0f, 0.0f,
             0.5f, 0.5f, 0.5f, 1.0f, 1.0f,
             0.5f, 0.5f, 0.5f, 1.0f, 1.0f,
             -0.5f, 0.5f, 0.5f, 0.0f, 1.0f,
             -0.5f, -0.5f, 0.5f, 0.0f, 0.0f,
             -0.5f, 0.5f, 0.5f, 1.0f, 0.0f,
             -0.5f, 0.5f, -0.5f, 1.0f, 1.0f,
             -0.5f, -0.5f, -0.5f, 0.0f, 1.0f,
             -0.5f, -0.5f, -0.5f, 0.0f, 1.0f,
             -0.5f, -0.5f, 0.5f, 0.0f, 0.0f,
             -0.5f, 0.5f, 0.5f, 1.0f, 0.0f,
             0.5f, 0.5f, 0.5f, 1.0f, 0.0f,
             0.5f, 0.5f, -0.5f, 1.0f, 1.0f,
             0.5f, -0.5f, -0.5f, 0.0f, 1.0f,
             0.5f, -0.5f, -0.5f, 0.0f, 1.0f,
             0.5f, -0.5f, 0.5f, 0.0f, 0.0f,
             0.5f, 0.5f, 0.5f, 1.0f, 0.0f,
             -0.5f, -0.5f, -0.5f, 0.0f, 1.0f,
             0.5f, -0.5f, -0.5f, 1.0f, 1.0f,
             0.5f, -0.5f, 0.5f, 1.0f, 0.0f,
             0.5f, -0.5f, 0.5f, 1.0f, 0.0f,
             -0.5f, -0.5f, 0.5f, 0.0f, 0.0f,
             -0.5f, -0.5f, -0.5f, 0.0f, 1.0f,
             -0.5f, 0.5f, -0.5f, 0.0f, 1.0f,
             0.5f, 0.5f, -0.5f, 1.0f, 1.0f,
             0.5f, 0.5f, 0.5f, 1.0f, 0.0f,
             0.5f, 0.5f, 0.5f, 1.0f, 0.0f,
             -0.5f, 0.5f, 0.5f, 0.0f, 0.0f,
             -0.5f, 0.5f, -0.5f, 0.0f, 1.0f
            };

        private static bool buffersCreated;

        public override void Draw()
        {
            GL.BindVertexArray(vertexArrayObject);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 36);
        }

        protected override void GenerateBuffers()
        {
            if (buffersCreated == true)
            {
                return;
            }
            vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float),
            vertices, BufferUsageHint.StaticDraw);
            vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(vertexArrayObject);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);
            buffersCreated = true;
            

        }
    }
}
