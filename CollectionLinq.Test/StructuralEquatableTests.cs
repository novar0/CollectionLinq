using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessClassLibrary.Collections.Linq.Test
{
	[TestClass]
	public class StructuralEquatableTests
	{
		[TestMethod]
		[TestCategory ("Collections.Linq.StructuralEquatable")]
		public void StructuralEquatable_SequenceEqual ()
		{
			var a0 = new byte[0];
			var a1 = new byte[0];
			var a2 = new byte[] { 1, 2, 3 };
			var a3 = new byte[] { 1, 3, 3 };
			var a4 = new byte[] { 1, 2, 3, 4 };
			Assert.IsTrue (StructuralEquatable.SequenceEqual (a0, a1));
			Assert.IsFalse (StructuralEquatable.SequenceEqual (a1, a2));
			Assert.IsFalse (StructuralEquatable.SequenceEqual (a2, a3));
			a3[1] = 2;
			Assert.IsTrue (StructuralEquatable.SequenceEqual (a2, a3));
			Assert.IsFalse (StructuralEquatable.SequenceEqual (a2, a4));
		}
	}
}
