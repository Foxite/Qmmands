using System;
using System.Threading.Tasks;
using Qmmands;

namespace CommandTesting {
	public class Program {
		private static void Main(string[] args) {
			try {
				new Program().MainAsync().GetAwaiter().GetResult();
			} catch (Exception e) {
				Console.WriteLine("Program has crashed: " + e.ToString());
				Console.ReadKey();
			}
		}

		private async Task MainAsync() {
			var qmmands = new CommandService(new CommandServiceConfiguration() {
				CommandMap = new MultiWordCommandMap()
			});
			qmmands.AddModule<TestModule>();

			string input;
			do {
				Console.Write("Input: ");
				input = Console.ReadLine();
				IResult result = await qmmands.ExecuteAsync(input, new ConsoleCommandContext());
				string resultString = "Result: " + (result?.ToString() ?? "null");
				Console.WriteLine(resultString);
				Console.WriteLine(new string('-', resultString.Length));
			} while (input != "!quit");
		}
	}
}
