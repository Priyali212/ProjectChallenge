using System.Runtime.Caching;

namespace QTask.Global
{
	public static class Application
	{
		public static List<AppData> AppStore = new List<AppData>();

		public static AppData get(string key)
		{
			ObjectCache cache = MemoryCache.Default;
			AppData objData = (AppData)cache.Get(key);

			if (objData != null)
			{
				objData.LastAccessedDate = DateTime.Now;
			}


#pragma warning disable CS8603 // Possible null reference return.
			return objData;
#pragma warning restore CS8603 // Possible null reference return.
		}

		public static void remove(string key, string name)
		{
			if (!String.IsNullOrEmpty(key))
			{
				ObjectCache cache = MemoryCache.Default;

				AppData objData = (AppData)cache.Get(key);

				if (objData != null && objData.Name == name)
					cache.Remove(key);
			}
			//var Result = AppStore.FirstOrDefault(x => x.SessionKey == key && x.Name==name);
			//if (Result != null)
			//{
			//    AppStore.Remove(Result);
			//}
		}

		public static void removeAll(string key)
		{
			if (!String.IsNullOrEmpty(key))
			{
				ObjectCache cache = MemoryCache.Default;

				AppData objData = (AppData)cache.Get(key);

				if (objData != null)
					cache.Remove(key);
			}
		}

		public static void set(string key, string value)
		{
			ObjectCache cache = MemoryCache.Default;

			AppData obj = new AppData();
			obj.SessionKey = key;
			obj.Name = value;
			obj.LastAccessedDate = DateTime.Now;

			CacheItemPolicy objPolicy = new CacheItemPolicy();
			objPolicy.AbsoluteExpiration = DateTimeOffset.MaxValue;
			objPolicy.SlidingExpiration = TimeSpan.FromMinutes(30);

			cache.Set(key, obj, objPolicy);

			//var Result = AppStore.FirstOrDefault(x => x.SessionKey == key);

			//if (Result == null)
			//{
			//    AppData obj = new AppData();
			//    obj.SessionKey = key;
			//    obj.Name = value;
			//    obj.LastAccessedDate = DateTime.Now;
			//    AppStore.Add(obj);
			//}
			//else
			//{
			//    Result.LastAccessedDate= DateTime.Now;
			//}
		}
	}

	public class AppData
	{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public string SessionKey { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public string Name { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public DateTime LastAccessedDate { get; set; }
	}
}
