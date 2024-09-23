using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafikProjekt1
{
    public class CylinderMesh : Mesh
    {

        private static int vertexArrayObject;
        private static int elementBufferObject;
        private static int vertexBufferObject;

        // Predefined cylinder attributes
        static int sectors = 8;
        static int stacks = 1;
        static float radius = 0.5f;
        static float height = 1.0f;

        public static float[] vertices = GenerateCylinderVertices(sectors, stacks, radius, height);
        public static uint[] indices = GenerateCylinderIndices(sectors, stacks);

        private bool bufferGenerated;


        public override void Draw()
        {

            GL.BindVertexArray(vertexArrayObject);
            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);

        }

        protected override void GenerateBuffers()
        {
            if (bufferGenerated)
            {
                return;
            }

            // Vertex Buffer Object - VBO
            vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            // Vertex Array Object - VAO
            vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(vertexArrayObject);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);

            // Element Buffer Object - Generate the EBO
            elementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

            bufferGenerated = true;
        }

        private static float[] GenerateCylinderVertices(int sectors, int stacks, float radius, float height)
        {
            List<float> vertices = new List<float>();

            float sectorStep = 2 * (float)Math.PI / sectors;
            float stackStep = height / stacks;
            float sectorAngle, stackPosition;

            // Generate vertices for the side of the cylinder
            for (int i = 0; i <= stacks; i++)
            {
                stackPosition = -0.5f * height + i * stackStep;
                for (int j = 0; j <= sectors; j++)
                {
                    sectorAngle = j * sectorStep;
                    vertices.Add(radius * (float)Math.Cos(sectorAngle));
                    vertices.Add(stackPosition);
                    vertices.Add(radius * (float)Math.Sin(sectorAngle));
                    vertices.Add((float)j / sectors);
                    vertices.Add((float)i / stacks);
                }
            }

            // Generate vertices for the top face
            float topPosition = 0.5f * height;
            for (int j = 0; j <= sectors; j++)
            {
                sectorAngle = j * sectorStep;
                vertices.Add(radius * (float)Math.Cos(sectorAngle));
                vertices.Add(topPosition);
                vertices.Add(radius * (float)Math.Sin(sectorAngle));
                vertices.Add((float)j / sectors);
                vertices.Add(1.0f); // Texture coordinate for top face
            }

            // Generate vertices for the bottom face
            float bottomPosition = -0.5f * height;
            for (int j = 0; j <= sectors; j++)
            {
                sectorAngle = j * sectorStep;
                vertices.Add(radius * (float)Math.Cos(sectorAngle));
                vertices.Add(bottomPosition);
                vertices.Add(radius * (float)Math.Sin(sectorAngle));
                vertices.Add((float)j / sectors);
                vertices.Add(0.0f); // Texture coordinate for bottom face
            }

            return vertices.ToArray();
        }

        private static uint[] GenerateCylinderIndices(int sectors, int stacks)
        {
            List<uint> indices = new List<uint>();

            // Generate indices for the side of the cylinder
            for (int i = 0; i < stacks; i++)
            {
                for (int j = 0; j < sectors; j++)
                {
                    uint idx0 = (uint)(i * (sectors + 1) + j);
                    uint idx1 = (uint)(idx0 + sectors + 1);
                    uint idx2 = (uint)(idx0 + 1);
                    uint idx3 = (uint)(idx1 + 1);

                    indices.Add(idx0);
                    indices.Add(idx1);
                    indices.Add(idx2);

                    indices.Add(idx2);
                    indices.Add(idx1);
                    indices.Add(idx3);
                }
            }

            // Generate indices for the top face
            uint baseIndex = (uint)(sectors * (stacks + 1));
            uint centerIndex = (uint)(baseIndex + sectors);

            // Add the center vertex for the top face
            for (int j = 0; j < sectors; j++)
            {
                indices.Add(centerIndex);
                indices.Add((uint)(baseIndex + j));
                indices.Add((uint)(baseIndex + (j + 1) % sectors));
            }

            // Add the center vertex for the bottom face
            baseIndex += (uint)(sectors + 1); // Update the base index for the bottom face
            centerIndex = (uint)(baseIndex + sectors); // Index of the center vertex for the bottom face

            for (int j = 0; j < sectors; j++)
            {
                indices.Add(centerIndex);
                // Connect to the next vertex, wrapping around to the start if necessary
                indices.Add((uint)(baseIndex + (j + 1) % sectors * 2));
                indices.Add((uint)(baseIndex + j));
            }

            return indices.ToArray();
        }
    }
}
