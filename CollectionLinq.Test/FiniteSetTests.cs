using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessClassLibrary.Collections.Linq.Test
{
	[TestClass]
	public class FiniteSetTests
	{
		private static IReadOnlyList<T> ToArray<T> (IEnumerable<T> enumerable)
		{
			return System.Linq.Enumerable.ToArray (enumerable);
		}

		[TestMethod]
		[TestCategory ("Collections.Linq.FiniteSet")]
		public void FiniteSet_Empty ()
		{
			var set = FiniteSet.Empty<int> ();
			Assert.AreEqual (0, set.Count);
			Assert.IsFalse (set.Contains (0));
			Assert.IsFalse (set.Contains (1));
		}

		[TestMethod]
		[TestCategory ("Collections.Linq.FiniteSet")]
		public void FiniteSet_Range ()
		{
			var set = FiniteSet.Range (0, 0);
			Assert.AreEqual (0, set.Count);
			Assert.IsFalse (set.Contains (0));
			Assert.IsFalse (set.Contains (1));
			set = FiniteSet.Range (0, 1);
			Assert.AreEqual (1, set.Count);
			Assert.IsTrue (set.Contains (0));
			Assert.IsFalse (set.Contains (-1));
			Assert.IsFalse (set.Contains (1));
			set = FiniteSet.Range (-2, 5);
			Assert.AreEqual (5, set.Count);
			var list = ToArray (set);
			Assert.AreEqual (-2, list[0]);
			Assert.AreEqual (-1, list[1]);
			Assert.AreEqual (0, list[2]);
			Assert.AreEqual (1, list[3]);
			Assert.AreEqual (2, list[4]);
			Assert.IsTrue (set.Contains (-2));
			Assert.IsTrue (set.Contains (-1));
			Assert.IsTrue (set.Contains (0));
			Assert.IsTrue (set.Contains (1));
			Assert.IsTrue (set.Contains (2));
			Assert.IsFalse (set.Contains (-3));
			Assert.IsFalse (set.Contains (3));
		}

		internal class TestSet<T> : HashSet<T>, IReadOnlyFiniteSet<T>
		{
			internal TestSet ()
				: base ()
			{
			}

			internal TestSet (IEnumerable<T> collection)
				: base (collection)
			{
			}
		}
		[TestMethod]
		[TestCategory ("Collections.Linq.FiniteSet")]
		public void FiniteSet_DefaultIfEmpty ()
		{
			var set = FiniteSet.DefaultIfEmpty (new TestSet<int> (), 999);
			Assert.AreEqual (1, set.Count);
			Assert.IsTrue (set.Contains (999));
			Assert.IsFalse (set.Contains (-1));
			Assert.IsFalse (set.Contains (1));
			set = FiniteSet.DefaultIfEmpty (new TestSet<int> () { 9, 3, 1 }, 999);
			Assert.AreEqual (3, set.Count);
			Assert.IsTrue (set.Contains (9));
			Assert.IsTrue (set.Contains (3));
			Assert.IsTrue (set.Contains (1));
			Assert.IsFalse (set.Contains (-1));
			Assert.IsFalse (set.Contains (2));
			var set2 = FiniteSet.DefaultIfEmpty (new TestSet<string> (), "999");
			Assert.AreEqual (1, set2.Count);
			Assert.IsTrue (set2.Contains ("999"));
			Assert.IsFalse (set2.Contains (""));
			Assert.IsFalse (set2.Contains (null));
			set2 = FiniteSet.DefaultIfEmpty (new TestSet<string> () { "three", "two", "one" }, "999");
			Assert.AreEqual (3, set2.Count);
			Assert.IsTrue (set2.Contains ("three"));
			Assert.IsTrue (set2.Contains ("two"));
			Assert.IsTrue (set2.Contains ("one"));
			Assert.IsFalse (set2.Contains (""));
			Assert.IsFalse (set2.Contains (null));
		}

		[TestMethod]
		[TestCategory ("Collections.Linq.FiniteSet")]
		public void FiniteSet_Contains ()
		{
			var set = new TestSet<int> ();
			Assert.IsFalse (FiniteSet.Contains (set, 0));
			Assert.IsFalse (FiniteSet.Contains (set, -1));
			set = new TestSet<int> () { 9, 3, 1 };
			Assert.IsTrue (FiniteSet.Contains (set, 3));
			Assert.IsTrue (FiniteSet.Contains (set, 1));
			Assert.IsTrue (FiniteSet.Contains (set, 9));
			Assert.IsFalse (FiniteSet.Contains (set, 0));
			Assert.IsFalse (FiniteSet.Contains (set, -1));
			var set2 = new TestSet<string> () { "three", "two", "one" };
			Assert.IsTrue (FiniteSet.Contains (set2, "three"));
			Assert.IsTrue (FiniteSet.Contains (set2, "two"));
			Assert.IsTrue (FiniteSet.Contains (set2, "one"));
			Assert.IsFalse (FiniteSet.Contains (set2, ""));
			Assert.IsFalse (FiniteSet.Contains (set2, null));
		}

		[TestMethod]
		[TestCategory ("Collections.Linq.FiniteSet")]
		public void FiniteSet_Reverse ()
		{
			var set = FiniteSet.Reverse (new TestSet<int> ());
			Assert.AreEqual (0, set.Count);
			set = FiniteSet.Reverse (new TestSet<int> () { 3 });
			Assert.AreEqual (1, set.Count);
			Assert.IsTrue (set.Contains (3));
			Assert.IsFalse (set.Contains (-1));
			Assert.IsFalse (set.Contains (1));
			set = FiniteSet.Reverse (new TestSet<int> () { 9, 3, 1 });
			var list = ToArray (set);
			Assert.AreEqual (3, list.Count);
			Assert.AreEqual (1, list[0]);
			Assert.AreEqual (3, list[1]);
			Assert.AreEqual (9, list[2]);
			Assert.IsTrue (set.Contains (9));
			Assert.IsTrue (set.Contains (3));
			Assert.IsTrue (set.Contains (1));
			Assert.IsFalse (set.Contains (-1));
			Assert.IsFalse (set.Contains (0));

			var set2 = FiniteSet.Reverse (new TestSet<string> ());
			Assert.AreEqual (0, set2.Count);
			set2 = FiniteSet.Reverse (new TestSet<string> () { "two" });
			Assert.AreEqual (1, set2.Count);
			Assert.IsTrue (set2.Contains ("two"));
			Assert.IsFalse (set2.Contains (""));
			Assert.IsFalse (set2.Contains (null));
			set2 = FiniteSet.Reverse (new TestSet<string> () { "three", "two", "one" });
			var list2 = ToArray (set2);
			Assert.AreEqual (3, list2.Count);
			Assert.AreEqual ("one", list2[0]);
			Assert.AreEqual ("two", list2[1]);
			Assert.AreEqual ("three", list2[2]);
			Assert.IsTrue (set2.Contains ("three"));
			Assert.IsTrue (set2.Contains ("two"));
			Assert.IsTrue (set2.Contains ("one"));
			Assert.IsFalse (set2.Contains (""));
			Assert.IsFalse (set2.Contains (null));
		}

		[TestMethod]
		[TestCategory ("Collections.Linq.FiniteSet")]
		public void FiniteSet_Distinct ()
		{
			var set = FiniteSet.Distinct (new TestSet<int> ());
			Assert.AreEqual (0, set.Count);
			set = FiniteSet.Distinct (new TestSet<int> () { 3 });
			Assert.AreEqual (1, set.Count);
			Assert.IsTrue (set.Contains (3));
			Assert.IsFalse (set.Contains (-1));
			Assert.IsFalse (set.Contains (1));
			set = FiniteSet.Distinct (new TestSet<int> () { 9, 3, 1 });
			var list = ToArray (set);
			Assert.AreEqual (3, list.Count);
			Assert.AreEqual (9, list[0]);
			Assert.AreEqual (3, list[1]);
			Assert.AreEqual (1, list[2]);
			Assert.IsTrue (set.Contains (9));
			Assert.IsTrue (set.Contains (3));
			Assert.IsTrue (set.Contains (1));
			Assert.IsFalse (set.Contains (-1));
			Assert.IsFalse (set.Contains (0));

			var set2 = FiniteSet.Distinct (new TestSet<string> ());
			Assert.AreEqual (0, set2.Count);
			set2 = FiniteSet.Distinct (new TestSet<string> () { "two" });
			Assert.AreEqual (1, set2.Count);
			Assert.IsTrue (set2.Contains ("two"));
			Assert.IsFalse (set2.Contains (""));
			Assert.IsFalse (set2.Contains (null));
			set2 = FiniteSet.Distinct (new TestSet<string> () { "three", "two", "one" });
			var list2 = ToArray (set2);
			Assert.AreEqual (3, list2.Count);
			Assert.AreEqual ("three", list2[0]);
			Assert.AreEqual ("two", list2[1]);
			Assert.AreEqual ("one", list2[2]);
			Assert.IsTrue (set2.Contains ("three"));
			Assert.IsTrue (set2.Contains ("two"));
			Assert.IsTrue (set2.Contains ("one"));
			Assert.IsFalse (set2.Contains (""));
			Assert.IsFalse (set2.Contains (null));
		}

		[TestMethod]
		[TestCategory ("Collections.Linq.FiniteSet")]
		public void FiniteSet_Except ()
		{
			var set = FiniteSet.Except (new TestSet<int> (), new TestSet<int> () { 9, 3, 1 });
			Assert.AreEqual (0, set.Count);
			Assert.IsFalse (set.Contains (1));
			Assert.IsFalse (set.Contains (-1));
			set = FiniteSet.Except (new TestSet<int> () { 9, 3, 1 }, new TestSet<int> ());
			Assert.AreEqual (3, set.Count);
			Assert.IsTrue (set.Contains (9));
			Assert.IsTrue (set.Contains (3));
			Assert.IsTrue (set.Contains (1));
			Assert.IsFalse (set.Contains (-1));
			Assert.IsFalse (set.Contains (0));
			set = FiniteSet.Except (new TestSet<int> () { 9, 3, 1 }, new TestSet<int> () { 8, 3, 0 });
			Assert.AreEqual (2, set.Count);
			Assert.IsTrue (set.Contains (9));
			Assert.IsTrue (set.Contains (1));
			Assert.IsFalse (set.Contains (3));
			Assert.IsFalse (set.Contains (0));
			set = FiniteSet.Except (new TestSet<int> () { 9, 3, 1 }, new TestSet<int> () { 1, 9, 3 });
			Assert.AreEqual (0, set.Count);
			Assert.IsFalse (set.Contains (1));
			Assert.IsFalse (set.Contains (3));
			Assert.IsFalse (set.Contains (9));
		}

		[TestMethod]
		[TestCategory ("Collections.Linq.FiniteSet")]
		public void FiniteSet_SymmetricExcept ()
		{
			var set = FiniteSet.SymmetricExcept (new TestSet<int> (), new TestSet<int> () { 9, 3, 1 });
			Assert.AreEqual (3, set.Count);
			Assert.IsTrue (set.Contains (9));
			Assert.IsTrue (set.Contains (3));
			Assert.IsTrue (set.Contains (1));
			Assert.IsFalse (set.Contains (-1));
			Assert.IsFalse (set.Contains (0));
			set = FiniteSet.SymmetricExcept (new TestSet<int> () { 9, 3, 1 }, new TestSet<int> ());
			Assert.AreEqual (3, set.Count);
			Assert.IsTrue (set.Contains (9));
			Assert.IsTrue (set.Contains (3));
			Assert.IsTrue (set.Contains (1));
			Assert.IsFalse (set.Contains (-1));
			Assert.IsFalse (set.Contains (0));
			set = FiniteSet.SymmetricExcept (new TestSet<int> () { 9, 3, 1 }, new TestSet<int> () { 8, 3, 0 });
			Assert.AreEqual (4, set.Count);
			Assert.IsTrue (set.Contains (9));
			Assert.IsTrue (set.Contains (1));
			Assert.IsTrue (set.Contains (8));
			Assert.IsTrue (set.Contains (0));
			Assert.IsFalse (set.Contains (-1));
			Assert.IsFalse (set.Contains (3));
			set = FiniteSet.SymmetricExcept (new TestSet<int> () { 9, 3, 1 }, new TestSet<int> () { 1, 9, 3 });
			Assert.AreEqual (0, set.Count);
			Assert.IsFalse (set.Contains (1));
			Assert.IsFalse (set.Contains (3));
			Assert.IsFalse (set.Contains (9));
		}

		[TestMethod]
		[TestCategory ("Collections.Linq.FiniteSet")]
		public void FiniteSet_Intersect ()
		{
			var set = FiniteSet.Intersect (new TestSet<int> (), new TestSet<int> () { 9, 3, 1 });
			Assert.AreEqual (0, set.Count);
			Assert.IsFalse (set.Contains (9));
			Assert.IsFalse (set.Contains (3));
			Assert.IsFalse (set.Contains (1));
			set = FiniteSet.Intersect (new TestSet<int> () { 9, 3, 1 }, new TestSet<int> ());
			Assert.AreEqual (0, set.Count);
			Assert.IsFalse (set.Contains (9));
			Assert.IsFalse (set.Contains (3));
			Assert.IsFalse (set.Contains (1));
			set = FiniteSet.Intersect (new TestSet<int> () { 9, 3, 1 }, new TestSet<int> () { 8, 3, 0 });
			Assert.AreEqual (1, set.Count);
			Assert.IsTrue (set.Contains (3));
			Assert.IsFalse (set.Contains (9));
			Assert.IsFalse (set.Contains (1));
			Assert.IsFalse (set.Contains (8));
			Assert.IsFalse (set.Contains (0));
			Assert.IsFalse (set.Contains (-1));
			set = FiniteSet.Intersect (new TestSet<int> () { 9, 3, 1 }, new TestSet<int> () { 1, 9, 3 });
			Assert.AreEqual (3, set.Count);
			Assert.IsTrue (set.Contains (9));
			Assert.IsTrue (set.Contains (3));
			Assert.IsTrue (set.Contains (1));
			Assert.IsFalse (set.Contains (-1));
			Assert.IsFalse (set.Contains (0));
		}

		[TestMethod]
		[TestCategory ("Collections.Linq.FiniteSet")]
		public void FiniteSet_Union ()
		{
			var set = FiniteSet.Union (new TestSet<int> (), new TestSet<int> () { 9, 3, 1 });
			Assert.AreEqual (3, set.Count);
			Assert.IsTrue (set.Contains (9));
			Assert.IsTrue (set.Contains (3));
			Assert.IsTrue (set.Contains (1));
			Assert.IsFalse (set.Contains (-1));
			Assert.IsFalse (set.Contains (0));
			set = FiniteSet.Union (new TestSet<int> () { 9, 3, 1 }, new TestSet<int> ());
			Assert.AreEqual (3, set.Count);
			Assert.IsTrue (set.Contains (9));
			Assert.IsTrue (set.Contains (3));
			Assert.IsTrue (set.Contains (1));
			Assert.IsFalse (set.Contains (-1));
			Assert.IsFalse (set.Contains (0));
			set = FiniteSet.Union (new TestSet<int> () { 9, 3, 1 }, new TestSet<int> () { 8, 3, 0 });
			Assert.AreEqual (5, set.Count);
			Assert.IsTrue (set.Contains (9));
			Assert.IsTrue (set.Contains (3));
			Assert.IsTrue (set.Contains (1));
			Assert.IsTrue (set.Contains (8));
			Assert.IsTrue (set.Contains (0));
			Assert.IsFalse (set.Contains (-1));
			Assert.IsFalse (set.Contains (10));
			set = FiniteSet.Union (new TestSet<int> () { 9, 3, 1 }, new TestSet<int> () { 1, 9, 3 });
			Assert.AreEqual (3, set.Count);
			Assert.IsTrue (set.Contains (9));
			Assert.IsTrue (set.Contains (3));
			Assert.IsTrue (set.Contains (1));
			Assert.IsFalse (set.Contains (-1));
			Assert.IsFalse (set.Contains (0));
		}
	}
}
