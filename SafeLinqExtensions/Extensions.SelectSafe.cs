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
		public static IEnumerable<TResult> SelectSafe<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector, ILogger? logger = null)
		{
			foreach (var item in source)
			{
				TResult result;

				try
				{
					result = selector(item);
				}
				catch (Exception ex)
				{
					logger?.LogError(ex, item?.ToJson());

					continue;
				}

				yield return result;
			}
		}

		public static async IAsyncEnumerable<TResult> SelectSafe<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, Task<TResult>> selector, ILogger? logger = null)
		{
			foreach (var item in source)
			{
				TResult result;

				try
				{
					result = await selector(item);
				}
				catch (Exception ex)
				{
					logger?.LogError(ex, item?.ToJson());

					continue;
				}

				yield return result;
			}
		}
	}
}
