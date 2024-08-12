using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SafeLinqExtensions
{
	internal class Program
	{
		static void Main(string[] args)
		{
			using var serviceProvider = new ServiceCollection()
				.AddLogging(configure => configure.AddConsole())
				.BuildServiceProvider();

			var logger = serviceProvider.GetRequiredService<ILogger<Program>>();

			var userInput = new List<string> { "25", "42", "invalid_age", "58" };

			var ages = userInput
				.SelectSafe(x => int.Parse(x), logger)
				.ToArray();

			foreach (var age in ages)
			{
				Console.WriteLine(age);  // Outputs 25, 42, 58
			}

			Console.ReadLine();
		}
	}
}
