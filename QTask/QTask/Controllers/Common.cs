using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QTask.Models;
using QTaskDataLayer.Repository;
using System.Security.Cryptography;
using System.Text;
using System.Data;

namespace QTask.Controllers
{
	public static class Common
	{

		private static String magic = "$1$";
		private static String itoa64 = "./0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

		public static IConfiguration config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
		public static string encrypt(string Tex)
		{
			byte[] hashedBytes = null;
			try
			{
				SHA1CryptoServiceProvider hasher = new SHA1CryptoServiceProvider();
				byte[] textWithSaltBytes = Encoding.UTF8.GetBytes(string.Concat(Tex));
				hashedBytes = hasher.ComputeHash(textWithSaltBytes);
				hasher.Clear();

			}
			catch (Exception ex)
			{
				CommonRepository objComm = new CommonRepository(config);
				string UserName = string.Empty;

				objComm.SaveErrorLog("Common", "encrypt", ex.Message, UserName);
			}
			return Convert.ToBase64String(hashedBytes);
		}

		public static string PasswordEncrypt(string text)
		{
			string EncryptString = string.Empty;
			try
			{
				MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
				byte[] array = Encoding.UTF8.GetBytes(text);
				array = md5.ComputeHash(array);
				StringBuilder sb = new StringBuilder();
				foreach (byte ba in array)
				{
					sb.Append(ba.ToString("x2").ToLower());
				}

				EncryptString = sb.ToString();
			}
			catch (Exception ex)
			{
				CommonRepository objComm = new CommonRepository(config);
				string UserName = string.Empty;

				objComm.SaveErrorLog("Common", "PasswordEncrypt", ex.Message, UserName);
			}
			return EncryptString;
		}


		public static string Salting(string text, string saltKey)
		{

			string SaltedText = string.Empty;
			int saltEnd;
			int len;
			int value;
			int i;
			byte[] final;
			byte[] passwordBytes;
			byte[] saltBytes;
			byte[] ctx;
			try
			{
				StringBuilder result;
				HashAlgorithm x_hash_alg = HashAlgorithm.Create("MD5");
				// Skip magic if it exists
				if (saltKey.StartsWith(magic))
				{
					saltKey = saltKey.Substring(magic.Length);
				}

				// Remove password hash if present
				if ((saltEnd = saltKey.LastIndexOf('$')) != -1)
				{
					saltKey = saltKey.Substring(0, saltEnd);
				}

				// Shorten salt to 8 characters if it is longer
				if (saltKey.Length > 8)
				{
					saltKey = saltKey.Substring(0, 8);
				}
				ctx = Encoding.ASCII.GetBytes((text + magic + saltKey));
				final = x_hash_alg.ComputeHash(Encoding.ASCII.GetBytes((text + saltKey + text)));

				// Add as many characters of ctx1 to ctx
				byte[] hashM;// = new byte[15];
				for (len = text.Length; len > 0; len -= 16)
				{
					if (len > 16)
					{
						ctx = Concat(ctx, final);
					}
					else
					{
						ctx = PartialConcat(ctx, final, len);

					}

					//System.Buffer.BlockCopy(final, 0, hash16, ctx.Length, len);
					//System.Buffer.BlockCopy(ctx, 0, hash16, 0, ctx.Length);

				}
				//ctx = hashM;

				// Then something really weird...
				passwordBytes = Encoding.ASCII.GetBytes(text);

				for (i = text.Length; i > 0; i >>= 1)
				{
					if ((i & 1) == 1)
					{
						ctx = Concat(ctx, new byte[] { 0 });
					}
					else
					{
						ctx = Concat(ctx, new byte[] { passwordBytes[0] });
					}
				}

				final = x_hash_alg.ComputeHash(ctx);

				byte[] ctx1;

				// Do additional mutations
				saltBytes = Encoding.ASCII.GetBytes(saltKey);//.getBytes();
				for (i = 0; i < 1000; i++)
				{
					ctx1 = new byte[] { };
					if ((i & 1) == 1)
					{
						ctx1 = Concat(ctx1, passwordBytes);
					}
					else
					{
						ctx1 = Concat(ctx1, final);
					}
					if (i % 3 != 0)
					{
						ctx1 = Concat(ctx1, saltBytes);
					}
					if (i % 7 != 0)
					{
						ctx1 = Concat(ctx1, passwordBytes);
					}
					if ((i & 1) != 0)
					{
						ctx1 = Concat(ctx1, final);
					}
					else
					{
						ctx1 = Concat(ctx1, passwordBytes);
					}
					final = x_hash_alg.ComputeHash(ctx1);

				}
				result = new StringBuilder();
				// Add the password hash to the result string
				value = ((final[0] & 0xff) << 16) | ((final[6] & 0xff) << 8)
						| (final[12] & 0xff);
				result.Append(to64(value, 4));
				value = ((final[1] & 0xff) << 16) | ((final[7] & 0xff) << 8)
						| (final[13] & 0xff);
				result.Append(to64(value, 4));
				value = ((final[2] & 0xff) << 16) | ((final[8] & 0xff) << 8)
						| (final[14] & 0xff);
				result.Append(to64(value, 4));
				value = ((final[3] & 0xff) << 16) | ((final[9] & 0xff) << 8)
						| (final[15] & 0xff);
				result.Append(to64(value, 4));
				value = ((final[4] & 0xff) << 16) | ((final[10] & 0xff) << 8)
						| (final[5] & 0xff);
				result.Append(to64(value, 4));
				value = final[11] & 0xff;
				result.Append(to64(value, 2));

				// Return result string
				return magic + saltKey + "$" + result.ToString();
			}
			catch (Exception ex)
			{
				CommonRepository objComm = new CommonRepository(config);
				string UserName = string.Empty;

				objComm.SaveErrorLog("Common", "Salting", ex.Message, UserName);
			}
			return SaltedText;
		}
		private static byte[] Concat(byte[] array1, byte[] array2)
		{
			byte[] concat = new byte[array1.Length + array2.Length];
			System.Buffer.BlockCopy(array1, 0, concat, 0, array1.Length);
			System.Buffer.BlockCopy(array2, 0, concat, array1.Length, array2.Length);
			return concat;
		}

		private static byte[] PartialConcat(byte[] array1, byte[] array2, int max)
		{
			byte[] concat = new byte[array1.Length + max];
			System.Buffer.BlockCopy(array1, 0, concat, 0, array1.Length);
			System.Buffer.BlockCopy(array2, 0, concat, array1.Length, max);
			return concat;
		}

		private static String to64(int value, int length)
		{
			StringBuilder result;

			result = new StringBuilder();
			while (--length >= 0)
			{
				result.Append(itoa64.Substring(value & 0x3f, 1));
				value >>= 6;
			}
			return (result.ToString());
		}

		public static bool ValidateSession(HttpContext context)
		{
			bool status = false;
			try
			{
				if (context.Session?.GetString("UserSession") != null && context.Session?.GetString("UserSession") != string.Empty)
				{
					// var AppData = Global.Application.get(context.Session?.GetString("UserSession"));

					//if (AppData != null && AppData.Name == context.Session.Id)
					//    status= true;
					//else
					//    status= false;

					status = true;
				}
				else
				{
					status = false;
				}
			}
			catch (Exception ex)
			{
				CommonRepository objComm = new CommonRepository(config);
				string UserName = string.Empty;
				if (context.Session?.GetString("UserSession") != null && context.Session?.GetString("UserSession") != string.Empty)
				{
					UserName = context.Session?.GetString("UserSession");
				}
				objComm.SaveErrorLog("Common", "ValidateSession", ex.Message, UserName);
			}
			return status;
		}

		public static bool CheckConcurrentSession(string key)
		{
			bool status = false;
			try
			{
				if (Global.Application.get("app-" + key) != null)
				{
					status = true;
				}
				else
				{
					status = false;
				}
			}
			catch (Exception ex)
			{
				CommonRepository objComm = new CommonRepository(config);
				string UserName = string.Empty;
				objComm.SaveErrorLog("Common", "CheckConcurrentSession", ex.Message, UserName);
			}
			return status;
		}

		public static UserAccessModel CheckAuthorization(HttpContext context)
		{
			UserAccessModel objUserAcc = null;
			try
			{
				var test = context.Session.GetString("UserAccess");
				List<UserAccessModel> objlstAccessModal = new List<UserAccessModel>();
				objlstAccessModal = JsonConvert.DeserializeObject<List<UserAccessModel>>(test);

				string requestedURL = context.Request.Path.ToString();
				requestedURL = requestedURL.Substring(requestedURL.LastIndexOf("/") + 1).Trim();
				if (requestedURL.IndexOf("?") > -1)
				{
					requestedURL = requestedURL.Remove(requestedURL.IndexOf("?"));
				}

				//objUserAcc = objlstAccessModal.Where(x => x.PageUrl == requestedURL).FirstOrDefault();
			}
			catch (Exception ex)
			{
				CommonRepository objComm = new CommonRepository(config);
				string UserName = string.Empty;
				if (context.Session?.GetString("UserSession") != null && context.Session?.GetString("UserSession") != string.Empty)
				{
					UserName = context.Session?.GetString("UserSession");
				}
				objComm.SaveErrorLog("Common", "CheckAuthorization", ex.Message, UserName);
			}

			return objUserAcc;
		}

		public static string QEncrypt(string PlainText)
		{
			string EncryptText = string.Empty;
			string Key = "Dd_gje4@85d$sg";
			try
			{
				string EncryptionKey = Key;
				byte[] clearBytes = Encoding.Unicode.GetBytes(PlainText);
				using (Aes encryptor = Aes.Create())
				{
					Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
					encryptor.Key = pdb.GetBytes(32);
					encryptor.IV = pdb.GetBytes(16);
					using (MemoryStream ms = new MemoryStream())
					{
						using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
						{
							cs.Write(clearBytes, 0, clearBytes.Length);
							cs.Close();
						}
						EncryptText = Convert.ToBase64String(ms.ToArray());
					}
				}
			}
			catch (Exception ex)
			{
				CommonRepository objComm = new CommonRepository(config);
				string UserName = string.Empty;

				objComm.SaveErrorLog("Common", "QEncrypt", ex.Message, UserName);
			}
			EncryptText = EncryptText.Replace("+", "A990A");
			return EncryptText;
		}

		public static string QDecrypt(string EncryptText)
		{
			string DecryptValue = string.Empty;
			string Key = "Dd_gje4@85d$sg";
			try
			{

				//cipherText = cipherText.Trim().Replace("A990A", "+");
				EncryptText = EncryptText.Replace("A990A", "+").Trim();
				string EncryptionKey = Key;
				EncryptText = EncryptText.Replace(" ", "+");
				byte[] cipherBytes = Convert.FromBase64String(EncryptText);

				using (Aes encryptor = Aes.Create())
				{
					Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
					encryptor.Key = pdb.GetBytes(32);
					encryptor.IV = pdb.GetBytes(16);
					using (MemoryStream ms = new MemoryStream())
					{
						using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
						{
							cs.Write(cipherBytes, 0, cipherBytes.Length);
							cs.Close();
						}
						DecryptValue = Encoding.Unicode.GetString(ms.ToArray());
					}
				}

			}
			catch (Exception ex)
			{
				CommonRepository objComm = new CommonRepository(config);
				string UserName = string.Empty;

				objComm.SaveErrorLog("Common", "QDecrypt", ex.Message, UserName);
			}
			return DecryptValue;
		}


		public static bool ValidatePassword(string Password, string Salting)
		{
			bool result = false;
			try
			{
				if (!Salting.Trim().StartsWith('$') && Salting.Length == 32)
				{
					// Old way - just md5 password
					if (Password.ToLower() == Salting)
						result = true;
				}
				else
				{
					Password = Common.Salting(Password.ToLower(), Salting);
					if (Password == Salting)
						result = true;
				}

			}
			catch (Exception ex)
			{
				CommonRepository objComm = new CommonRepository(config);

				string UserName = string.Empty;
				//if (objLog != null)
				//    UserName = objLog.UserName;

				objComm.SaveErrorLog("LoginAPIController", "ValidatePassword", ex.Message, UserName);
			}

			return result;
		}

	}
}
