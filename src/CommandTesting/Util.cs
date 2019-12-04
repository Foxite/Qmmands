﻿namespace CommandTesting {
	public static class Util {
		// Stolen from https://stackoverflow.com/a/4335913
		public static string TrimStart(this string target, string trimString) {
			if (string.IsNullOrEmpty(trimString)) return target;

			string result = target;
			while (result.StartsWith(trimString)) {
				result = result.Substring(trimString.Length);
			}

			return result;
		}

		public static string TrimEnd(this string target, string trimString) {
			if (string.IsNullOrEmpty(trimString)) return target;

			string result = target;
			while (result.EndsWith(trimString)) {
				result = result.Substring(0, result.Length - trimString.Length);
			}

			return result;
		}
	}
}