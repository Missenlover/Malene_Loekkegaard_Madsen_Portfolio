using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafikProjekt1
{
    public class Mesh
    {
        protected int vertexArrayObject;
        protected int elementBufferObject;
        protected int vertexBufferObject;
  
        public Mesh()
        {
            GenerateBuffers(); //setup based on vertices, indices, etc
        }
        public virtual void Draw()
        {
            
        }

        protected virtual void GenerateBuffers()
        {

        }
    }
}
