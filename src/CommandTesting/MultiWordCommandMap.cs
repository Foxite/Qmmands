using System.Collections.Generic;
using Qmmands;

namespace CommandTesting {
	internal class MultiWordCommandMap : ICommandMap {
		private List<Command> m_Commands;

		public MultiWordCommandMap() {
			m_Commands = new List<Command>();
		}

		public IReadOnlyList<CommandMatch> FindCommands(string input) {
			var matches = new List<CommandMatch>();
			// This can be seriously optimized but it does the job for the proof of concept.
			// For starters we can keep a list of modules that we've already checked, and skip re-checking those.
			foreach (Command command in m_Commands) {
				var path = new List<string>();
				var checkModules = new Stack<Module>();
				string remainingInput = input;
				checkModules.Push(command.Module);
				while (checkModules.Peek().Parent != null) {
					checkModules.Push(checkModules.Peek().Parent);
				}
				char separatorChar = ' '; // TODO take custom separator
				bool match = false;
				while (checkModules.TryPop(out Module check)) {
					foreach (string alias in check.Aliases) {
						if (remainingInput.StartsWith(alias)) {
							remainingInput = remainingInput.Substring(alias.Length);
							path.Add(alias);
							match = true;
							break;
						}
					}

					if (match) {
						string trimmedRemainingInput = remainingInput.TrimStart(separatorChar);
						if (trimmedRemainingInput != remainingInput) {
							remainingInput = trimmedRemainingInput;
						} else {
							match = false;
							break;
						}
					}
				}
				if (match) {
					foreach (string alias in command.Aliases) {
						if (remainingInput.StartsWith(alias)) {
							remainingInput = remainingInput.Substring(alias.Length);
							if (remainingInput.Length == 0 || remainingInput.StartsWith(separatorChar)) {
								matches.Add(new CommandMatch(command, alias, path.AsReadOnly(), remainingInput.TrimStart(separatorChar)));
								break;
							}
						}
					}
				}
			}
			return matches.AsReadOnly();
		}

		public void MapModule(Module module) {
			m_Commands.AddRange(module.Commands);
		}

		public void UnmapModule(Module module) {
			m_Commands.RemoveAll(command => command.Module == module);
		}
	}
}