using System;
using System.IO;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Proyecto_Final
{
	public class Modelo
	{
		DatosTextura Imagen;
		float angulo;
		int eje;
		float escalar;
		int reflexion;
		Vector3 translacion;
		
		List<Face> obj;
		
		public Modelo()
		{
			angulo = 0;
			eje = -1;
			escalar = 1;
			translacion = new Vector3(0, 0, 0);
			reflexion = 0;
			obj = new List<Face>();
		}
		public void CargarArch(string modelo, string textura)
		{
			//Leer textura
			Imagen = LoadTexture.LoadTestureFile(textura);
			
			char[] caracteres = { '\n', '#', ' ', '/', '\t' };
			string contenido = File.ReadAllText(modelo);
			string[] lineas = contenido.Split(caracteres);
			float x = 0, y = 0, z = 0;
			int v_leer = 0, num_v = 0, num_vt = 0;
			
			for (int i = 0; i < lineas.Length; i++) {
				if (lineas[i] == "v") {
					num_v++;
				}
				if (lineas[i] == "vt") {
					num_vt++;
				}
				if (lineas[i] == "f") {
					for (int j = (i + 1); j < lineas.Length; j++) {
						if (lineas[j] != "f") {
							v_leer++;
						} else {
							j = lineas.Length;
						}
					}
				}
			}
			
			Vector3[] v = new Vector3[num_v + 1];
			Vector2[] vt = new Vector2[num_vt + 1];
			List<Face> listado = new List<Face>();
			v_leer = v_leer - (v_leer / 3);
			int[] sucesion = new int[v_leer + 1];
			
			num_v = num_vt = 1;
			int cont = 1, id_f = 1, aux = 0;
			
			for (int i = 0; i < lineas.Length; i++) {
				if (lineas[i] == "v") {
					x = float.Parse(lineas[i + 1], CultureInfo.InvariantCulture.NumberFormat);
					y = float.Parse(lineas[i + 2], CultureInfo.InvariantCulture.NumberFormat);
					z = float.Parse(lineas[i + 3], CultureInfo.InvariantCulture.NumberFormat);
					v[num_v] = new Vector3(x, y, z);
					num_v++;
				}
				if (lineas[i] == "vt") {
					x = float.Parse(lineas[i + 1], CultureInfo.InvariantCulture.NumberFormat);
					y = float.Parse(lineas[i + 2], CultureInfo.InvariantCulture.NumberFormat);
					vt[num_vt] = new Vector2(x, y);
					num_vt++;
				}
				if (lineas[i] == "f") {
					for (int j = (i + 1); j < lineas.Length; j++) {
						if (lineas[j] != "f") {
							if (cont != 3) {
								if ((int.TryParse(lineas[j], out aux)) && (lineas[j] != "0")) {
									sucesion[id_f] = Int32.Parse(lineas[j]);
								}
								id_f++;
								cont++;
							} else {
								cont = 1;
							}
						} else {
							j = lineas.Length;
							id_f = 1;
						}
					}
					Face lectura = new Face();
					int c = 1, v1 = 0, v2 = 0;
					foreach (int element in sucesion) {
						if ((element != 0) && (c < sucesion.Length)) {
							if ((sucesion[c] != 0) && (sucesion[(c + 1)] != 0)) {
									v1 = sucesion[c];
									v2 = sucesion[(c + 1)];
								if (v1 < v.Length && v2 < vt.Length) {
									lectura.set(v[v1], vt[v2]);
									c += 2;
								}
							}
						}
					}
					listado.Add(lectura);
				}
			}
			obj = listado;
			GL.BindTexture(TextureTarget.Texture2D, Imagen.ID);
		}
		public void Posicion(Vector3 t, float es, float ag, int ej, int refl)
		{
			int op = 0;

			if (reflexion != refl) {
				op = 1;
			}
			
			if ((angulo != ag) || (eje != ej) ) {
				op = 2;
			}
			if (escalar != es) {
				op = 3;
			}
			if (translacion != t) {
				op = 4;
			}
			angulo = ag;
			eje = ej;
			escalar = es;
			translacion = t;
			reflexion = refl;
			
			foreach (Face element in obj) {
				element.actualizacion(translacion, escalar, angulo, eje, reflexion, op);
			}
		}
		public void Draw()
		{
			foreach (Face element in obj) {
				element.draw();
			}
		}
	}
}
