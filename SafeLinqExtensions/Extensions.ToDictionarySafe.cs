namespace SafeLinqExtensions
{
	public static partial class LinqExtensions
	{
		public static Dictionary<TKey, TElement> ToDictionarySafe<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector) where TKey : notnull
		{
			Dictionary<TKey, TElement> dictionary = new Dictionary<TKey, TElement>();

			foreach (var item in source)
				dictionary[keySelector(item)] = elementSelector(item);

			return dictionary;
		}

		public static Dictionary<TKey, TSource> ToDictionarySafe<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector) where TKey : notnull
		{
			Dictionary<TKey, TSource> dictionary = new Dictionary<TKey, TSource>();

			foreach (var item in source)
				dictionary[keySelector(item)] = item;

			return dictionary;
		}
	}
}
