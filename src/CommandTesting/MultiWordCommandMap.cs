using System.Collections.Generic;
using Qmmands;

namespace CommandTesting {
	internal class MultiWordCommandMap : ICommandMap {
		private readonly string m_Separator;
		private readonly List<Command> m_Commands;

		// I've not actually tested this with any separator other than a space but it should totally work.
		public MultiWordCommandMap(string separator = " ") {
			m_Commands = new List<Command>();
			m_Separator = separator;
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
						string trimmedRemainingInput = remainingInput.TrimStart(m_Separator);
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
							if (remainingInput.Length == 0 || remainingInput.StartsWith(m_Separator)) {
								matches.Add(new CommandMatch(command, alias, path.AsReadOnly(), remainingInput.TrimStart(m_Separator)));
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
			foreach (Module sub in module.Submodules) {
				MapModule(sub);
			}
		}

		public void UnmapModule(Module module) {
			m_Commands.RemoveAll(command => command.Module == module);
			foreach (Module sub in module.Submodules) {
				UnmapModule(sub);
			}
		}
	}
}