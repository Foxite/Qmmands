namespace Qmmands {
	/// <summary>
	///		A non-generic interface for <see cref="TypeParserResult{T}"/>. Does not contain <see cref="TypeParserResult{T}.Value"/>.
	/// </summary>
	public interface ITypeParserResult : IResult {
		/// <summary>
		///     Gets the error reason. <see langword="null"/> if <see cref="IResult.IsSuccessful"/> is <see langword="true"/>.
		/// </summary>
		bool HasValue { get; }
		
		/// <summary>
		///     Gets whether this result has a parsed value or not.
		/// </summary>
		string Reason { get; }
	}
}
