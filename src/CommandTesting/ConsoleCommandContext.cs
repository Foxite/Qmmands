using Qmmands;

namespace CommandTesting {
	public class ConsoleCommandContext : CommandContext {
		public ConsoleCommandContext() : base(null) { } // It will use DummyServiceProvider
	}
}