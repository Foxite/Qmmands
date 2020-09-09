using System;
using System.Collections.Generic;
using System.Text;

namespace Qmmands {
	public class PrimitiveTypeParserResult : ITypeParserResult {
		public string Reason { get; }
		public bool HasValue => false;
		public bool IsSuccessful => false;
		
		public PrimitiveTypeParserResult(string reason) {
			Reason = reason;
		}
	}
}
