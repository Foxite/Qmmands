using System;
using Qmmands;

namespace CommandTesting {
	//[Group("bbb")]
	public class TestModule : ModuleBase<ConsoleCommandContext> {
		[Command("test thing")]
		public void TestCommand(string thing) {
			Console.WriteLine("You wrote: " + thing);
		}

		[Command("int")]
		public void ParserTest(int thing) {
			Console.WriteLine("Int value: " + thing.ToString());
		}
	}
}