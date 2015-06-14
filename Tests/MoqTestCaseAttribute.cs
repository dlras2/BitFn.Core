using System;
using System.Collections.Generic;
using System.Reflection;

namespace BitFn.Core.Tests
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public class MoqTestCaseAttribute : MoqDataAttribute
	{
		private readonly object[] _arguments;

		public MoqTestCaseAttribute(params object[] arguments)
		{
			_arguments = arguments ?? new object[] {null};
		}

		public override IEnumerable<object[]> GetData(MethodInfo method)
		{
			var testCaseData = base.GetData(method);

			foreach (var caseValues in testCaseData)
			{
				if (_arguments.Length > caseValues.Length)
				{
					throw new InvalidOperationException("Number of parameters provided is more than expected parameter count");
				}
				Array.Copy(_arguments, caseValues, _arguments.Length);

				yield return caseValues;
			}
		}
	}
}
