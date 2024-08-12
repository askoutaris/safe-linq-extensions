using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace SafeLinqExtensions
{
	public static partial class LinqExtensions
	{
		public static void ForeachSafe<TSource>(this IEnumerable<TSource> source, Action<TSource> action, ILogger? logger = null)
		{
			foreach (var item in source)
			{
				try
				{
					action(item);
				}
				catch (Exception ex)
				{
					logger?.LogError(ex, item?.ToJson());
				}
			}
		}

		public static async Task ForeachSafe<TSource>(this IEnumerable<TSource> source, Func<TSource, Task> action, ILogger? logger = null)
		{
			foreach (var item in source)
			{
				try
				{
					await action(item);
				}
				catch (Exception ex)
				{
					logger?.LogError(ex, item?.ToJson());
				}
			}
		}
	}
}
