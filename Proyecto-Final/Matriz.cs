using System;

namespace Proyecto_Final
{
public class Matriz
	{
		int dim;
		float[,] matriz;
		
		public Matriz(float[,] a)
		{
			dim = 4;
			matriz = new float[dim, dim];
			for (int i = 0; i < dim; i++) {
				for (int j = 0; j < dim; j++) {
					matriz[i, j] = a[i, j];
				}
			}
		}
		
		public Matriz()
		{
			dim = 4;
			matriz = new float[dim, dim];
		}
		
		public void Mostrar()
		{
			for (int i = 0; i < dim; i++) {
				for (int j = 0; j < dim; j++) {
					Console.Write(matriz[i, j] + " ");
				}
				Console.WriteLine();
			}
		}
		
		public float this[int i, int j] {
			set{ matriz[i, j] = value; }
			get{ return matriz[i, j]; }
		}
		
		public int Dim {
			set{ dim = value; }
			get{ return dim; }
		}
		
		public static Matriz operator+(Matriz a, Matriz b) //Suma de matrices
		{
			Matriz resultado = new Matriz();
			
			for (int i = 0; i < a.dim; i++) {
				for (int j = 0; j < a.dim; j++) {
					resultado[i, j] = a[i, j] + b[i, j];
				}
			}
			
			return resultado;
		}
		
		public static Matriz operator*(Matriz a, Matriz b) //Multiplicacion de matrices
		{
			Matriz resultado = new Matriz();
			
			for (int i = 0; i < a.dim; i++) {
				for (int j = 0; j < a.dim; j++) {
					for (int k = 0; k < b.dim; k++) {
						resultado[i, j] += (a[i, k] * b[k, j]);
					}
				}
			}
			
			return resultado;
		}
		public static Matriz operator*(Matriz a, float e) //Multiplicacion de escalar por matriz
		{
			Matriz resultado = new Matriz();
			
			for (int i = 0; i < a.dim; i++) {
				for (int j = 0; j < a.dim; j++) {
					resultado[i, j] = a[i, j] * e;
				}
			}
			
			return resultado;
		}
	}
}
