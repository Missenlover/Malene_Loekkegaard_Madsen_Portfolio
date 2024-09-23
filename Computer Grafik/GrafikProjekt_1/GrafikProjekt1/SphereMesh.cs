using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafikProjekt1
{
    public class SphereMesh : Mesh
    {
        private static int vertexArrayObject;
        private static int elementBufferObject;
        private static int vertexBufferObject;
        private bool bufferGenerated = false;

        public static float[] vertices = GenerateSphereVertices(Sphere.Radius, Sphere.Sectors, Sphere.Stacks);

        public static uint[] indices = GenerateSphereIndices(Sphere.Radius, Sphere.Sectors, Sphere.Stacks);

        public override void Draw()
        {
            // Bind the VAO
            GL.BindVertexArray(vertexArrayObject);
            // Draw the mesh
            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
            //Unbind the VAO
            GL.BindVertexArray(0);
        }

        protected override void GenerateBuffers()
        {
            if (bufferGenerated)
            {
                return;
            }

            // Generate the VBO
            vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            // Generate the VAO
            vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(vertexArrayObject);

            // Generate the EBO
            elementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);


            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);

            bufferGenerated = true;
        }

        private static float[] GenerateSphereVertices(float radius, int latitudeSegments, int longitudeSegments)
        {
            List<float> vertices = new List<float>();

            for (int lat = 0; lat <= latitudeSegments; lat++)
            {
                float theta = (float)(lat * Math.PI / latitudeSegments);
                float sinTheta = (float)Math.Sin(theta);
                float cosTheta = (float)Math.Cos(theta);

                for (int lon = 0; lon <= longitudeSegments; lon++)
                {
                    float phi = (float)(lon * 2 * Math.PI / longitudeSegments);
                    float sinPhi = (float)Math.Sin(phi);
                    float cosPhi = (float)Math.Cos(phi);

                    float x = (float)(cosPhi * sinTheta);
                    float y = (float)cosTheta;
                    float z = (float)(sinPhi * sinTheta);

                    vertices.Add(x * radius);
                    vertices.Add(y * radius);
                    vertices.Add(z * radius);
                }
            }
            return vertices.ToArray();
        }

        private static uint[] GenerateSphereIndices(float radius, int latitudeSegments, int longitudeSegments)
        {
            List<uint> indices = new List<uint>();

            for (int lat = 0; lat < latitudeSegments; lat++)
            {
                for (int lon = 0; lon < longitudeSegments; lon++)
                {
                    uint first = (uint)((lat * (longitudeSegments + 1)) + lon);
                    uint second = first + (uint)(longitudeSegments + 1);
                    indices.Add(first);
                    indices.Add(second);
                    indices.Add(first + 1);

                    indices.Add(second);
                    indices.Add(second + 1);
                    indices.Add(first + 1);
                }
            }

            return indices.ToArray();
        }

    }
}
