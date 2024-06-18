using System.Runtime.Caching;
using QTask.Controllers;
using System.Runtime.Caching;
using QTask.Models;

namespace QTask.Global
{
	public static class Token
	{
		public static List<TokenData> lstToken = new List<TokenData>();

		public static TokenData get(string key)
		{
			ObjectCache cache = MemoryCache.Default;
			TokenData objToken = (TokenData)cache.Get(key);
			if (objToken != null)
				objToken.TokenLastAccessDate = DateTime.Now;

#pragma warning disable CS8603 // Possible null reference return.
			return objToken;
#pragma warning restore CS8603 // Possible null reference return.
		}

		public static void remove(string key, string name)
		{
			if (!String.IsNullOrEmpty(key))
			{
				ObjectCache cache = MemoryCache.Default;

				TokenData objData = (TokenData)cache.Get(key);

				if (objData != null && objData.TokenName == name)
					cache.Remove(key);
			}
		}

		public static void removeAll(string key)
		{
			if (!String.IsNullOrEmpty(key))
			{
				ObjectCache cache = MemoryCache.Default;

				TokenData objData = (TokenData)cache.Get(key);

				if (objData != null)
					cache.Remove(key);
			}
		}

		public static void set(string TokenId, string value, string SessionId, List<UserAccessModel> lstUsr, bool IsAdmin, string BrowserDetails, string IPAddress)
		{
			ObjectCache cache = MemoryCache.Default;

			TokenData obj = new TokenData();
			obj.TokenId = TokenId;
			obj.TokenName = value;
			obj.SessionId = SessionId;
			obj.BrowserDetails = BrowserDetails;
			obj.IpAddress = IPAddress;
			obj.TokenLastAccessDate = DateTime.Now;
			obj.CreatedDateTime = DateTime.Now;
			obj.IsAdmin = IsAdmin;
			obj.lstUserAcceessModel = lstUsr;

			CacheItemPolicy objPolicy = new CacheItemPolicy();
			objPolicy.AbsoluteExpiration = DateTimeOffset.MaxValue;
			objPolicy.SlidingExpiration = TimeSpan.FromMinutes(30);

			cache.Set(TokenId, obj, objPolicy);

		}

		public static bool ValidateToken(string key, string SessionId, string BrowserDetails, string IPAddress)
		{
			bool Result = false;
#pragma warning disable CS0168 // Variable is declared but never used
			try
			{

				if (!String.IsNullOrEmpty(key))
				{
					ObjectCache cache = MemoryCache.Default;

					TokenData objData = (TokenData)cache.Get(key);

					if (objData != null)
					{
						if (objData.SessionId == SessionId && objData.BrowserDetails == BrowserDetails && objData.IpAddress == IPAddress)
						{
							Result = true;
						}
					}
				}
			}
			catch (Exception ex)
			{

			}
#pragma warning restore CS0168 // Variable is declared but never used
			return Result;
		}

		public static string GenerateNewTokenId(string TokenId, string SessionId)
		{
			string NewKey = string.Empty;
			if (!String.IsNullOrEmpty(TokenId))
			{
				ObjectCache cache = MemoryCache.Default;

				TokenData objData = (TokenData)cache.Get(TokenId);

				if (objData != null)
				{
					string id = objData.TokenName;
					string TimeStamp = DateTime.Now.ToString("ddMMyyyyhhmmss");

					NewKey = Common.QEncrypt(id + "|#sep#|" + TimeStamp + "|#sep#|" + SessionId);

					CacheItemPolicy objPolicy = new CacheItemPolicy();
					objPolicy.AbsoluteExpiration = DateTimeOffset.MaxValue;
					objPolicy.SlidingExpiration = TimeSpan.FromMinutes(30);
				}

			}
			return NewKey;
		}
	}

	public class TokenData
	{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public string TokenId { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public string TokenName { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public string SessionId { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public string BrowserDetails { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public string IpAddress { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public DateTime CreatedDateTime { get; set; }
		public DateTime TokenLastAccessDate { get; set; }
		public bool IsAdmin { get; set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public List<UserAccessModel> lstUserAcceessModel { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	}
}
