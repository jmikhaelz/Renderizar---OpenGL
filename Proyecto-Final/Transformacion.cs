using OpenTK;
using System;

namespace Proyecto_Final
{
	public class Transformacion
	{
		public Transformacion()
		{
		}
		public Vector3 Scaling(Vector3 v, float e)
		{
			float[,] Mv = {
				{ v.X, 0, 0, 0 },
				{ 0, v.Y, 0, 0 },
				{ 0, 0, v.Z, 0 },
				{ 0, 0, 0, 1 }
			};
			Matriz M = new Matriz(Mv);
			float[,] E = {
				{ e, 0, 0, 0 },
				{ 0, e, 0, 0 },
				{ 0, 0, e, 0 },
				{ 0, 0, 0, 1 }
			};
			Matriz S = new Matriz(E);
			M = S * M;
			return new Vector3(M[0, 0], M[1, 1], M[2, 2]);
		}
		public Vector3 Reflection(Vector3 v, int opc)
		{
			float[,] Mv = {
				{ v.X, 0, 0, 0 },
				{ 0, v.Y, 0, 0 },
				{ 0, 0, v.Z, 0 },
				{ 0, 0, 0, 1 }
			};
			Matriz M = new Matriz(Mv);
			switch (opc) {
				case 1:
					float[,] Rx = {
						{ -1, 0, 0, 0 },
						{ 0, 1, 0, 0 },
						{ 0, 0, 1, 0 },
						{ 0, 0, 0, 1 }
					};
					Matriz X = new Matriz(Rx);
					M = M * X;
					break;
				case 2:
					float[,] Ry = {
						{ 1, 0, 0, 0 },
						{ 0, -1, 0, 0 },
						{ 0, 0, 1, 0 },
						{ 0, 0, 0, 1 }
					};
					Matriz Y = new Matriz(Ry);
					M = M * Y;
					break;
				case 3:
					float[,] Rz = {
						{ 1, 0, 0, 0 },
						{ 0, 1, 0, 0 },
						{ 0, 0, -1, 0 },
						{ 0, 0, 0, 1 }
					};
					Matriz Z = new Matriz(Rz);
					M = M * Z;
					break;
				case 4:
					float[,] R = {
						{ -1, 0, 0, 0 },
						{ 0, -1, 0, 0 },
						{ 0, 0, -1, 0 },
						{ 0, 0, 0, 1 }
					};
					Matriz Rt = new Matriz(R);
					M = M * Rt;
					break;
			}
			return new Vector3(M[0, 0], M[1, 1], M[2, 2]);
		}
		public Vector3 Translation(Vector3 v, Vector3 t)
		{
			float[,] Mv = {
				{ v.X, 0, 0, 0 },
				{ 0, v.Y, 0, 0 },
				{ 0, 0, v.Z, 0 },
				{ 0, 0, 0, 1 }
			};
			Matriz M = new Matriz(Mv);
			float[,] T = {
				{ 1, 0, 0, t.X },
				{ 0, 1, 0, t.Y },
				{ 0, 0, 1, t.Z },
				{ 0, 0, 0, 1 }
			};
			Matriz Tm = new Matriz(T);
			M = Tm * M;
			return new Vector3((M[0, 0] + M[0, 3]), (M[1, 1] + M[1, 3]), (M[2, 2] + M[2, 3]));
		}
		public Vector3 Rotation(Vector3 v, float ax, int eje)
		{
			ax = (float)(ax / Math.PI * 2);
			float[,] Mv = {
				{ v.X, 0, 0, 0 },
				{ 0, v.Y, 0, 0 },
				{ 0, 0, v.Z, 0 },
				{ 0, 0, 0, 1 }
			};
			Matriz M = new Matriz(Mv);
			Vector3 resultado = v;
			float cos = (float)Math.Cos(ax);
			float sin = (float)Math.Sin(ax);
			switch (eje) {
				case 0: //Eje X
					float[,] Rx = {
						{ 1, 0, 0, 0 },
						{ 0, cos, sin, 0 },
						{ 0, -sin, cos, 0 },
						{ 0, 0, 0, 1 }
					};
					Matriz X = new Matriz(Rx);
					X = X * M;
					resultado = new Vector3(X[0, 0], (X[1, 1] + X[1, 2]), (X[2, 1] + X[2, 2]));
					break;
				case 1: //Eje Y
					float[,] Ry = {
						{ cos, 0, -sin, 0 },
						{ 0, 1, 0, 0 },
						{ sin, 0, cos, 0 },
						{ 0, 0, 0, 1 }
					};
					Matriz Y = new Matriz(Ry);
					Y = Y * M;
					resultado = new Vector3((Y[0, 0] + Y[0, 2]), Y[1, 1], (Y[2, 0] + Y[2, 2]));
					break;
				case 2: //Eje Z
					float[,] Rz = {
						{ cos, -sin, 0, 0 },
						{ sin, cos, 0, 0 },
						{ 0, 0, 1, 0 },
						{ 0, 0, 0, 1 }
					};
					Matriz Z = new Matriz(Rz);
					Z = Z * M;
					resultado = new Vector3((Z[0, 0] + Z[0, 1]), (Z[1, 0] + Z[1, 1]), Z[2, 2]);
					break;
			}
			return resultado;
		}
	}
}
