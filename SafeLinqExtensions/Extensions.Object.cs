using Newtonsoft.Json;

namespace SafeLinqExtensions
{
	public static class ObjectExtensions
	{
		private readonly static JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
		{
			ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
		};

		public static string ToJson(this object obj, JsonSerializerSettings serializerSettings)
			=> JsonConvert.SerializeObject(obj, serializerSettings);

		public static string ToJson(this object obj)
			=> JsonConvert.SerializeObject(obj);
	}
}
