using System;
using System.IO;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Proyecto_Final
{
	class vec
	{
		private Vector3 v;
		private Vector2 vt;
		public vec()
		{
			v = new Vector3();
			vt = new Vector2();
		}
		public vec(Vector3 v, Vector2 vt)
		{
			this.v = v;
			this.vt = vt;
		}
		public Vector3 V {
			set{ v = value; }
			get{ return v; }
		}
		public Vector2 Vt {
			set{ vt = value; }
			get{ return vt; }
		}
	}
	public class Face
	{
		List<vec> vectices;
		int num_vec = 0;
		public Face()
		{
			vectices = new List<vec>();
		}
		public void set(Vector3 v, Vector2 vt)
		{
			vectices.Add(new vec(v, vt));
		}
		public int Num_V()
		{
			int cont = 0;
			foreach (vec element in vectices) {
				cont++;
			}
			return cont;
		}
		public void actualizacion(Vector3 translacion, float escalar, float angulo, int eje, int reflexion, int op)
		{
			Transformacion conversion = new Transformacion();
			foreach (vec element in vectices) {
				switch(op){
						case 1: element.V = conversion.Reflection(element.V,reflexion); break;
						case 2: element.V = conversion.Rotation(element.V,angulo,eje); break;
						case 3: element.V = conversion.Scaling(element.V,escalar); break;
						case 4: element.V = conversion.Translation(element.V,translacion); break;
				}
			}
		}
		public void draw()
		{
			num_vec = Num_V();
			if (num_vec == 3) {
				GL.Begin(PrimitiveType.Triangles);
			} else if (num_vec == 4) {
				GL.Begin(PrimitiveType.Quads);
			} else if (num_vec > 4) {
				GL.Begin(PrimitiveType.Polygon);
			}
			foreach (vec element in vectices) {
				GL.TexCoord2(element.Vt);
				GL.Vertex3(element.V);
			}
			GL.End();
		}
	}
}
