using System;

namespace Qmmands
{
    /// <summary>
    ///     Represents a type parse failure.
    /// </summary>
    public sealed class TypeParseFailedResult : FailedResult
    {
        /// <summary>
        ///     Gets the reason of this failed result.
        /// </summary>
        public override string Reason => TypeParserResult.Reason;

        /// <summary>
        ///     Gets the <see cref="Qmmands.Parameter"/> the parse failed for.
        /// </summary>
        public Parameter Parameter { get; }

        /// <summary>
        ///     Gets the value passed to the type parser.
        /// </summary>
        public string Value { get; }

        /// <summary>
        ///     The <see cref="TypeParserResult{T}"/> that caused this failure.
        /// </summary>
        public ITypeParserResult TypeParserResult { get; }

        internal TypeParseFailedResult(Parameter parameter, string value, ITypeParserResult innerResult)
        {
            Parameter = parameter;
            Value = value;

            TypeParserResult = innerResult;
        }
    }
}
