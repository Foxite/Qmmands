using System;
using System.Collections.Generic;
using System.Text;

namespace Qmmands {
	public class PrimitiveTypeParserResult : ITypeParserResult {
		public string Reason { get; }
		public bool HasValue => false;
		public bool IsSuccessful => false;

		object ITypeParserResult.Value => throw new NotImplementedException();

		public PrimitiveTypeParserResult(string reason) {
			Reason = reason;
		}
	}
}
