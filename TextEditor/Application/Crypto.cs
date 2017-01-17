using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace Sedecal.CryptoText
{
	internal class Crypto
	{
		//OJO.. este es un sistema sencillo de encriptacion de datos
		// por este motivo las claves de encriptacion son fijas
		// o por contra pueden meterse en configuración y encriptar el app.config para ocultar esta informacion
		byte[] Key;
		byte[] IV;

		private ICryptoTransform cryptoTransform;
		private ICryptoTransform decryptoTransform;

		/// <summary>
		///  La clase utilizará la clave y vector pasados en el constructor
		/// </summary>
		internal Crypto() : this("1234567890123456789", "1234567890123456789")
		{
		}

		/// <summary>
		/// La clase utilizará la clave y vector pasados en el constructor
		/// </summary>
		/// <param name="clave">La clave de encriptación</param>
		/// <param name="vector">El vector de encriptación</param>
		internal Crypto(string clave, string vector)
		{
			Key = MakeKeyByteArray(clave);
			IV = MakeKeyByteArray(vector);

			Rijndael rijndael = Rijndael.Create();
			cryptoTransform = rijndael.CreateEncryptor(Key, IV);
			decryptoTransform = rijndael.CreateDecryptor(Key, IV);
		}


		//-----------------------------------------------------------------------------------------------------------

		private byte[] MakeKeyByteArray(string stringKey)
		{
			if (stringKey.Length < 16)
				// de ser así, completamos la cadena hasta esos 16 bytes.
				stringKey = stringKey.PadRight(16);
			else if (stringKey.Length > 16)// si la longitud es mayor a 16 bytes,
				// truncamos la cadena dejándola en 8 bytes.
				stringKey = stringKey.Substring(0, 16);
			// utilizando los métodos del namespace System.Text, 
			// convertimos la cadena de caracteres en un arreglo de bytes
			// mediante el método GetBytes() del sistema de codificación UTF.
			return Encoding.UTF8.GetBytes(stringKey);
		}

		private byte[] FormatText(string textoplano)
		{
			//			if (textoplano.Length < 16)
			//			{
			// de ser así, completamos la cadena hasta esos 16 bytes.
			//				textoplano = textoplano.PadRight(16);
			//			}
			return Encoding.UTF8.GetBytes(textoplano);
		}

		internal string EncryptText(string cadenaplana)
		{
			if (string.IsNullOrEmpty(cadenaplana))
			{
				return string.Empty;
			}

			MemoryStream memStream = null;
			// Create a new Rijndael object
			byte[] textoPlano = FormatText(cadenaplana);
			memStream = new System.IO.MemoryStream(textoPlano.Length * 2);
			// Create a CryptoStream using the FileStream 
			// and the passed key and initialization vector (IV).
			CryptoStream cStream = new CryptoStream(memStream, cryptoTransform, CryptoStreamMode.Write);
			// creamos el flujo
			cStream.Write(textoPlano, 0, textoPlano.Length);
			cStream.Close();
			byte[] result = memStream.ToArray();
			return Convert.ToBase64String(result);
		}


		internal string DecryptText(string cadenacifrada)
		{
			MemoryStream memStream = null;
			// creamos el flujo
			memStream = new MemoryStream(cadenacifrada.Length);
			byte[] textoCifrado = Convert.FromBase64String(cadenacifrada);

			// Create a CryptoStream using the FileStream 
			// and the passed key and initialization vector (IV).
			CryptoStream cStream = new CryptoStream(memStream, decryptoTransform, CryptoStreamMode.Write);

			cStream.Write(textoCifrado, 0, textoCifrado.Length); // ciframos
			cStream.Close();
			byte[] result = memStream.ToArray();
			return Encoding.UTF8.GetString(memStream.ToArray());
		}


		//-----------------------------------------------------------------------------------------------------------

		internal Stream EncryptBytes(Stream StreamIn, Stream StreamOut)
		{

			//Creamos el algoritmo de encriptado
			Rijndael RijndaelAlg = Rijndael.Create();
			CryptoStream cStream = new CryptoStream(StreamIn,
				RijndaelAlg.CreateEncryptor(Key, IV),
				CryptoStreamMode.Read);

			try
			{
				// creamos el flujo
				Byte[] buffer = new byte[16384];
				int leido = cStream.Read(buffer, 0, 16384);
				while (leido > 0)
				{
					StreamOut.Write(buffer, 0, leido);
					leido = cStream.Read(buffer, 0, 16384);
				}
				return StreamOut;
			}
			catch (Exception ex)
			{
				// GESTIONAR ERROR
				return null;
			}
			finally
			{
				cStream.Close();
				//los flujos de salida y entrada se cierran desde fuera
			}
		}


		//-----------------------------------------------------------------------------------------------------------

		internal Stream DecryptBytes(Stream StreamIn, Stream StreamOut)
		{

			//Creamos el algoritmo de encriptado
			Rijndael RijndaelAlg = Rijndael.Create();
			CryptoStream cStream = new CryptoStream(StreamIn,
				RijndaelAlg.CreateDecryptor(Key, IV),
				CryptoStreamMode.Read);

			try
			{
				// creamos el flujo
				Byte[] buffer = new byte[16384];
				int leido = cStream.Read(buffer, 0, 16384);
				while (leido > 0)
				{
					StreamOut.Write(buffer, 0, leido);
					leido = cStream.Read(buffer, 0, 16384);
				}
				return StreamOut;
			}
			catch (Exception ex)
			{
				// GESTIONAR ERROR
				return null;
			}
			finally
			{
				cStream.Close();
				//los flujos de salida y entrada se cierran desde fuera
			}
		}
	}

}
