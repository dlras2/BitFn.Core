using Humanizer;

namespace BitFn.Core
{
	/// <summary>
	///     A class this library wants to expose.
	/// </summary>
	public class Class1
	{
		private readonly IInterface1 _interface1;

		public Class1(IInterface1 interface1)
		{
			_interface1 = interface1;
		}

		/// <summary>
		///     A method echoing its arguments.
		/// </summary>
		/// <param name="argument">The string to echo.</param>
		/// <returns>The argument string, preceded by its argument name.</returns>
		public string Echo(string argument)
		{
			return $"{nameof(argument).Humanize()}: {argument}";
		}
	}
}
