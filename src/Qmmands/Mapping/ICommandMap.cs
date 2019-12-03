using System.Collections.Generic;

namespace Qmmands {
	/// <summary>
	///		This keeps track of all <see cref="Module"/>s and their <see cref="Command"/>s that a <see cref="CommandService"/> can access.
	///		You can implement this interface to customize how a <see cref="CommandService"/> finds commands.
	///		You should not be interacting with an object of this type directly, instead you should construct your own implementation and add it to <see cref="CommandServiceConfiguration.CommandMap"/>.
	/// </summary>
	public interface ICommandMap {
		/// <summary>
		///		Given an <paramref name="input"/>, this will find all potential <see cref="Command"/>s matching the input.
		/// </summary>
		IReadOnlyList<CommandMatch> FindCommands(string input);

		/// <summary>
		///		Adds a <see cref="Module"/> to the map.
		/// </summary>
		void MapModule(Module module);

		/// <summary>
		///		Removes a <see cref="Module"/> from the map.
		/// </summary>
		void UnmapModule(Module module);
	}
}