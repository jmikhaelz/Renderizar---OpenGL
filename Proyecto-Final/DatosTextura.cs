using System;

namespace Proyecto_Final
{
	public class DatosTextura
	{
		readonly int Identificador;
		readonly int Ancho;
		readonly int Alto;
		
		public DatosTextura(int id, int ancho, int alto)
		{
			Identificador = id;
			Ancho = ancho;
			Alto = alto;
			
		}
		
		public int ID {
			get {
				return Identificador;
			}
		}
		
		public int WIDTH {
			get {
				return Ancho;
			}
		}
		
		public int HEIGHT {
			get {
				return Alto;
			}
		}
	}
}
