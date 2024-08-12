using Microsoft.Extensions.Logging;

namespace SafeLinqExtensions
{
	public static partial class LinqExtensions
	{
		public static async Task ForeachSafe<TSource>(this IEnumerable<TSource> source, ILogger logger, Func<TSource, Task> action)
		{
			foreach (var item in source)
			{
				try
				{
					await action(item);
				}
				catch (Exception ex)
				{
					logger.LogError(ex, item?.ToJson());
				}
			}
		}

		public static async Task<TResult[]> ToArrayAsync<TResult>(this IEnumerable<Task<TResult>> tasks) where TResult : class
		{
			var results = new List<TResult>();

			foreach (var task in tasks)
			{
				var result = await task;

				results.Add(result);
			}

			return [.. results];
		}

		public static async Task<TResult[]> ToArrayAsync<TResult>(this IAsyncEnumerable<TResult> taskResults) where TResult : class
		{
			var results = new List<TResult>();

			await foreach (var result in taskResults)
				results.Add(result);

			return [.. results];
		}

		public static async Task<TSource[]> ToArrayNotNullAsync<TSource>(this IAsyncEnumerable<TSource?> tasks)
		{
			var results = new List<TSource>();

			await foreach (var result in tasks)
			{
				if (result is not null)
					results.Add(result);
			}

			return [.. results];
		}

		public static async Task<TSource[]> ToArrayNotNullAsync<TSource>(this IEnumerable<Task<TSource?>> tasks)
		{
			var results = new List<TSource>(tasks.Count());

			foreach (var task in tasks)
			{
				var result = await task;

				if (result is not null)
					results.Add(result);
			}

			return [.. results];
		}
	}
}
