using System;
using System.IO;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL;

namespace Proyecto_Final
{
	public class LoadTexture
	{
		public LoadTexture()
		{
		}
		public static DatosTextura LoadTestureFile(string archivo){
			int id = GL.GenTexture();
			GL.BindTexture(TextureTarget.Texture2D, id);
			
			if(!File.Exists(archivo)){
				throw new FileNotFoundException("El "+archivo+" no ha sido encontrado");
			}
			
			System.Drawing.Bitmap mapaDeBits = new System.Drawing.Bitmap(archivo);
			
			BitmapData datos = mapaDeBits.LockBits(new System.Drawing.Rectangle(0, 0, mapaDeBits.Width, mapaDeBits.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
			
			GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, datos.Width, datos.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgr, PixelType.UnsignedByte, datos.Scan0);
			
			mapaDeBits.UnlockBits(datos);
			
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Clamp);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Clamp);
			
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.NearestMipmapLinear);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
			
			return new DatosTextura(id, mapaDeBits.Width, mapaDeBits.Height);
		}
	}
}
