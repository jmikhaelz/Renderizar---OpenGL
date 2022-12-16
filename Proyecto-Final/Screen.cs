using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Proyecto_Final
{
	public class Screen : GameWindow
	{
		//Visualizacion
		Vector3 camara = new Vector3(8,8, 5);
		Vector3 objetivo = new Vector3(0, 0, 0);
		Vector3 orientacion = new Vector3(0, 1, 0);
		//Modelo
		Modelo objeto = new Modelo();
		//Control del movimiento de la figura
		Vector3 posicion = new Vector3(0, 0, 0);
		float escala = 1;
		float angulo = 0;
		int eje = -1;
		int reflexion = 0;
		bool mov = true;
		float frecuencia = 0.01f;
		public Screen(int ancho, int alto)
			: base(ancho, alto)
		{
			Title = "//***Proyecto Final***//";
		}
		protected override void OnLoad(System.EventArgs e)
		{
			GL.ClearColor(Color.Black);
			GL.Enable(EnableCap.CullFace);
			GL.Enable(EnableCap.Texture2D);
			GL.DepthFunc(DepthFunction.Less);
			
			
			objeto.CargarArch("angel.obj", "angel.jfif");
			Menu();
		}
		protected override void OnUpdateFrame(FrameEventArgs e)
		{
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
			Matrix4 PuntoDeVision = Matrix4.LookAt(camara, objetivo, orientacion);
			GL.MatrixMode(MatrixMode.Modelview);
			GL.LoadMatrix(ref PuntoDeVision);
			
			//Manejo de la figura
			angulo = (angulo > 360) ? 0 : angulo += 0.001f;
			objeto.Posicion(posicion, escala, angulo, eje, reflexion);
		}
		protected override void OnResize(EventArgs e)
		{
			GL.Viewport(0, 0, Width, Height);
			float RelacionAspecto = (float)Width / (float)Height;
			Matrix4 campoVision = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver6, RelacionAspecto, 1, 60);
			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadMatrix(ref campoVision);
		}
		protected override void OnRenderFrame(FrameEventArgs e)
		{
			Grilla();
			objeto.Draw();
			SwapBuffers();
		}
		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
			if (e.KeyChar == '|') {
				Exit();
			}
			//Cambio de vista
			if (e.KeyChar == 'm' || e.KeyChar == 'M') {
				mov = (mov) ? false : true;
			}
			//Eje X
			if (e.KeyChar == 'q' || e.KeyChar == 'Q') {
				if (!mov) {
					camara.X += frecuencia;
				} else {
					posicion.X += frecuencia;
				}
			}
			if (e.KeyChar == 'w' || e.KeyChar == 'W') {
				if (!mov) {
					camara.X -= frecuencia;
				} else {
					posicion.X -= frecuencia;
				}
			}
			//Eje Y
			if (e.KeyChar == 'a' || e.KeyChar == 'A') {
				if (!mov) {
					camara.Y += frecuencia;
				} else {
					posicion.Y += frecuencia;
				}
			}
			if (e.KeyChar == 's' || e.KeyChar == 'S') {
				if (!mov) {
					camara.Y -= frecuencia;
				} else {
					posicion.Y -= frecuencia;
				}
			}
			//Eje Z
			if (e.KeyChar == 'z' || e.KeyChar == 'Z') {
				if (!mov) {
					camara.Z += frecuencia;
				} else {
					posicion.Z += frecuencia;
				}
			}
			if (e.KeyChar == 'x' || e.KeyChar == 'X') {
				if (!mov) {
					camara.Z -= frecuencia;
				} else {
					posicion.Z -= frecuencia;
				}
			}
			//Rotacion
			if (e.KeyChar == 'v' || e.KeyChar == 'V') {
				if (eje >= 1) {
					eje--;angulo=0;
				}
			}
			if (e.KeyChar == 'c' || e.KeyChar == 'C') {
				if (eje <= 2) {
					eje++;angulo=0;
				}
			}
			//Escalar
			if (e.KeyChar == 'y' || e.KeyChar == 'Y') {
				if (escala <= 2) {
					escala += frecuencia;
				}
			}
			if (e.KeyChar == 'u' || e.KeyChar == 'U') {
				if (escala >= 0.5f) {
					escala -= frecuencia;
				}
			}
			//Reflexion
			if (e.KeyChar == 'o' || e.KeyChar == 'O') {
				if (reflexion <= 3) {
					reflexion++;
				}
			}
			if (e.KeyChar == 'p' || e.KeyChar == 'P') {
				if (reflexion >= 1) {
					reflexion--;
				}				
				
			}
		}
		public void Grilla()
		{
			GL.Begin(PrimitiveType.Lines);
			GL.Color3(0.5f, 0.5f, 0.5f);
			for (int i = -10; i < 10; i++) {
				for (int j = -10; j < 10; j++) {
					GL.Vertex3(-10, j, 0);
					GL.Vertex3(10, j, 0);
					GL.Vertex3(j, -10, 0);
					GL.Vertex3(j, 10, 0);
				}
			}
			GL.End();
		}
		public void Menu()
		{
			Console.WriteLine("\tM\tE\tN\tU");
			Console.WriteLine("\tOpcion a Realizar \tTecla");
			Console.WriteLine("\tSalir del Programa \t[|]");
			Console.WriteLine("\tAumentar Objeto \t[Y]+");
			Console.WriteLine("\tDecrementar Objeto \t[U]-");
			Console.WriteLine("\tEje de Rotacion \t[C]+ [V]-");
			Console.WriteLine("\tReflexion del Objeto \t[O]+ [P]-");
			Console.WriteLine("\tCambio de Movimiento \t[M]\t De Objeto a Camara");
			Console.WriteLine("\tEje X \t[Q]+ [W]-");
			Console.WriteLine("\tEje Y \t[A]+ [S]-");
			Console.WriteLine("\tEje Z \t[Z]+ [X]-");
		}
	}
}
