using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessClassLibrary.Collections.Linq.Test
{
	[TestClass]
	public class CollectionTests
	{
		private static IReadOnlyList<T> ToArray<T> (IEnumerable<T> enumerable)
		{
			return System.Linq.Enumerable.ToArray (enumerable);
		}

		[TestMethod]
		[TestCategory ("Collections.Linq.Collection")]
		public void Collection_Any ()
		{
			Assert.IsFalse (Collection.Any (new int[0]));
			Assert.IsTrue (Collection.Any (new int[] { 3 }));
			Assert.IsTrue (Collection.Any (new string[] { "one", "two", "three" }));
		}

		[TestMethod]
		[TestCategory ("Collections.Linq.Collection")]
		public void Collection_Count ()
		{
			Assert.AreEqual (0, Collection.Count (new int[0]));
			Assert.AreEqual (1, Collection.Count (new int[] { 3 }));
			Assert.AreEqual (3, Collection.Count (new string[] { "one", "two", "three" }));
			Assert.AreEqual (0L, Collection.LongCount (new int[0]));
			Assert.AreEqual (1L, Collection.LongCount (new int[] { 3 }));
			Assert.AreEqual (3L, Collection.LongCount (new string[] { "one", "two", "three" }));
		}

		[TestMethod]
		[TestCategory ("Collections.Linq.Collection")]
		public void Collection_DefaultIfEmpty ()
		{
			var collection = ToArray (Collection.DefaultIfEmpty (new int[0], 999));
			Assert.AreEqual (1, collection.Count);
			Assert.AreEqual (999, collection[0]);
			collection = ToArray (Collection.DefaultIfEmpty (new int[] { 9, 3, 1 }, 999));
			Assert.AreEqual (3, collection.Count);
			Assert.AreEqual (9, collection[0]);
			Assert.AreEqual (3, collection[1]);
			Assert.AreEqual (1, collection[2]);
			var collection2 = ToArray (Collection.DefaultIfEmpty (new string[0], "999"));
			Assert.AreEqual (1, collection2.Count);
			Assert.AreEqual ("999", collection2[0]);
			collection2 = ToArray (Collection.DefaultIfEmpty (new string[] { "three", "two", "one" }, "999"));
			Assert.AreEqual (3, collection2.Count);
			Assert.AreEqual ("three", collection2[0]);
			Assert.AreEqual ("two", collection2[1]);
			Assert.AreEqual ("one", collection2[2]);
		}

		[TestMethod]
		[TestCategory ("Collections.Linq.Collection")]
		public void Collection_Skip ()
		{
			var collection = ToArray (Collection.Skip (new int[0], 3));
			Assert.AreEqual (0, collection.Count);
			collection = ToArray (Collection.Skip (new int[] { 9, 3, 1 }, 0));
			Assert.AreEqual (3, collection.Count);
			Assert.AreEqual (9, collection[0]);
			Assert.AreEqual (3, collection[1]);
			Assert.AreEqual (1, collection[2]);
			collection = ToArray (Collection.Skip (new int[] { 9, 3, 1 }, 2));
			Assert.AreEqual (1, collection.Count);
			Assert.AreEqual (1, collection[0]);

			var collection2 = ToArray (Collection.Skip (new string[0], 3));
			Assert.AreEqual (0, collection2.Count);
			collection2 = ToArray (Collection.Skip (new string[] { "three", "two", "one" }, 0));
			Assert.AreEqual (3, collection2.Count);
			Assert.AreEqual ("three", collection2[0]);
			Assert.AreEqual ("two", collection2[1]);
			Assert.AreEqual ("one", collection2[2]);
			collection2 = ToArray (Collection.Skip (new string[] { "three", "two", "one" }, 2));
			Assert.AreEqual (1, collection2.Count);
			Assert.AreEqual ("one", collection2[0]);
		}

		[TestMethod]
		[TestCategory ("Collections.Linq.Collection")]
		public void Collection_Take ()
		{
			var collection = ToArray (Collection.Take (new int[0], 3));
			Assert.AreEqual (0, collection.Count);
			collection = ToArray (Collection.Take (new int[] { 9, 3, 1 }, 0));
			Assert.AreEqual (0, collection.Count);
			collection = ToArray (Collection.Take (new int[] { 9, 3, 1 }, 5));
			Assert.AreEqual (3, collection.Count);
			Assert.AreEqual (9, collection[0]);
			Assert.AreEqual (3, collection[1]);
			Assert.AreEqual (1, collection[2]);
			collection = ToArray (Collection.Take (new int[] { 9, 3, 1 }, 1));
			Assert.AreEqual (1, collection.Count);
			Assert.AreEqual (9, collection[0]);

			var collection2 = ToArray (Collection.Take (new string[0], 3));
			Assert.AreEqual (0, collection2.Count);
			collection2 = ToArray (Collection.Take (new string[] { "three", "two", "one" }, 0));
			Assert.AreEqual (0, collection2.Count);
			collection2 = ToArray (Collection.Take (new string[] { "three", "two", "one" }, 4));
			Assert.AreEqual (3, collection2.Count);
			Assert.AreEqual ("three", collection2[0]);
			Assert.AreEqual ("two", collection2[1]);
			Assert.AreEqual ("one", collection2[2]);
			collection2 = ToArray (Collection.Take (new string[] { "three", "two", "one" }, 1));
			Assert.AreEqual (1, collection2.Count);
			Assert.AreEqual ("three", collection2[0]);
		}

		[TestMethod]
		[TestCategory ("Collections.Linq.Collection")]
		public void Collection_Select ()
		{
			var collection = ToArray (Collection.Select (new int[0], item => item * 2));
			Assert.AreEqual (0, collection.Count);
			collection = ToArray (Collection.Select (new int[] { 9, 3, 1 }, item => item + 3));
			Assert.AreEqual (3, collection.Count);
			Assert.AreEqual (12, collection[0]);
			Assert.AreEqual (6, collection[1]);
			Assert.AreEqual (4, collection[2]);

			var collection2 = ToArray (Collection.Select (new string[0], item => item + "_"));
			Assert.AreEqual (0, collection2.Count);
			collection2 = ToArray (Collection.Select (new string[] { "three", "two", "one" }, item => item + "_"));
			Assert.AreEqual (3, collection2.Count);
			Assert.AreEqual ("three_", collection2[0]);
			Assert.AreEqual ("two_", collection2[1]);
			Assert.AreEqual ("one_", collection2[2]);
		}

		[TestMethod]
		[TestCategory ("Collections.Linq.Collection")]
		public void Collection_SelectIdx ()
		{
			var collection = ToArray (Collection.Select (new int[0], (item, idx) => item * idx));
			Assert.AreEqual (0, collection.Count);
			collection = ToArray (Collection.Select (new int[] { 9, 3, 1 }, (item, idx) => item * idx));
			Assert.AreEqual (3, collection.Count);
			Assert.AreEqual (0, collection[0]);
			Assert.AreEqual (3, collection[1]);
			Assert.AreEqual (2, collection[2]);

			var collection2 = ToArray (Collection.Select (new string[0], (item, idx) => item + idx.ToString (System.Globalization.CultureInfo.InvariantCulture)));
			Assert.AreEqual (0, collection2.Count);
			collection2 = ToArray (Collection.Select (new string[] { "three", "two", "one" }, (item, idx) => item + idx.ToString (System.Globalization.CultureInfo.InvariantCulture)));
			Assert.AreEqual (3, collection2.Count);
			Assert.AreEqual ("three0", collection2[0]);
			Assert.AreEqual ("two1", collection2[1]);
			Assert.AreEqual ("one2", collection2[2]);
		}

		[TestMethod]
		[TestCategory ("Collections.Linq.Collection")]
		public void Collection_Reverse ()
		{
			var collection = ToArray (Collection.Reverse (new int[0]));
			Assert.AreEqual (0, collection.Count);
			collection = ToArray (Collection.Reverse (new int[] { 3 }));
			Assert.AreEqual (1, collection.Count);
			Assert.AreEqual (3, collection[0]);
			collection = ToArray (Collection.Reverse (new int[] { 9, 3, 1 }));
			Assert.AreEqual (3, collection.Count);
			Assert.AreEqual (1, collection[0]);
			Assert.AreEqual (3, collection[1]);
			Assert.AreEqual (9, collection[2]);

			var collection2 = ToArray (Collection.Reverse (new string[0]));
			Assert.AreEqual (0, collection2.Count);
			collection2 = ToArray (Collection.Reverse (new string[] { "two" }));
			Assert.AreEqual (1, collection2.Count);
			Assert.AreEqual ("two", collection2[0]);
			collection2 = ToArray (Collection.Reverse (new string[] { "three", "two", "one" }));
			Assert.AreEqual (3, collection2.Count);
			Assert.AreEqual ("one", collection2[0]);
			Assert.AreEqual ("two", collection2[1]);
			Assert.AreEqual ("three", collection2[2]);
		}

		private class StringLenComparer : IComparer<string>
		{
			public int Compare (string x, string y)
			{
				if (x == null) return (y == null) ? 0 : -1;
				if (y == null) return 1;
				return x.Length.CompareTo (y.Length);
			}
		}
		[TestMethod]
		[TestCategory ("Collections.Linq.Collection")]
		public void Collection_Ordering ()
		{
			var collection = ToArray (Collection.OrderBy (new int[0], item => item * 3));
			Assert.AreEqual (0, collection.Count);
			var source = new int[] { 9, 1, -3 };
			collection = ToArray (Collection.OrderBy (source, item => item));
			Assert.AreEqual (3, collection.Count);
			Assert.AreEqual (-3, collection[0]);
			Assert.AreEqual (1, collection[1]);
			Assert.AreEqual (9, collection[2]);
			collection = ToArray (Collection.OrderBy (source, item => System.Math.Abs (item)));
			Assert.AreEqual (3, collection.Count);
			Assert.AreEqual (1, collection[0]);
			Assert.AreEqual (-3, collection[1]);
			Assert.AreEqual (9, collection[2]);

			collection = ToArray (Collection.OrderByDescending (new int[0], item => item * 3));
			Assert.AreEqual (0, collection.Count);
			collection = ToArray (Collection.OrderByDescending (source, item => item));
			Assert.AreEqual (3, collection.Count);
			Assert.AreEqual (9, collection[0]);
			Assert.AreEqual (1, collection[1]);
			Assert.AreEqual (-3, collection[2]);
			collection = ToArray (Collection.OrderByDescending (source, item => System.Math.Abs (item)));
			Assert.AreEqual (3, collection.Count);
			Assert.AreEqual (9, collection[0]);
			Assert.AreEqual (-3, collection[1]);
			Assert.AreEqual (1, collection[2]);

			var sizeComparer = new StringLenComparer ();
			var col = new string[] { "333", "222", "111", "11", "1111", null, "aaa", "a", "" };
			var collection2 = ToArray (Collection.ThenBy (Collection.OrderBy (col, item => item, sizeComparer), item => item, null));
			Assert.AreEqual (9, collection2.Count);
			Assert.AreEqual (null, collection2[0]);
			Assert.AreEqual ("", collection2[1]);
			Assert.AreEqual ("a", collection2[2]);
			Assert.AreEqual ("11", collection2[3]);
			Assert.AreEqual ("111", collection2[4]);
			Assert.AreEqual ("222", collection2[5]);
			Assert.AreEqual ("333", collection2[6]);
			Assert.AreEqual ("aaa", collection2[7]);
			Assert.AreEqual ("1111", collection2[8]);

			collection2 = ToArray (Collection.ThenBy (Collection.OrderByDescending (col, item => item, sizeComparer), item => item, null));
			Assert.AreEqual (9, collection2.Count);
			Assert.AreEqual ("1111", collection2[0]);
			Assert.AreEqual ("111", collection2[1]);
			Assert.AreEqual ("222", collection2[2]);
			Assert.AreEqual ("333", collection2[3]);
			Assert.AreEqual ("aaa", collection2[4]);
			Assert.AreEqual ("11", collection2[5]);
			Assert.AreEqual ("a", collection2[6]);
			Assert.AreEqual ("", collection2[7]);
			Assert.AreEqual (null, collection2[8]);

			collection2 = ToArray (Collection.ThenByDescending (Collection.OrderBy (col, item => item, sizeComparer), item => item, null));
			Assert.AreEqual (9, collection2.Count);
			Assert.AreEqual (null, collection2[0]);
			Assert.AreEqual ("", collection2[1]);
			Assert.AreEqual ("a", collection2[2]);
			Assert.AreEqual ("11", collection2[3]);
			Assert.AreEqual ("aaa", collection2[4]);
			Assert.AreEqual ("333", collection2[5]);
			Assert.AreEqual ("222", collection2[6]);
			Assert.AreEqual ("111", collection2[7]);
			Assert.AreEqual ("1111", collection2[8]);
		}

		[TestMethod]
		[TestCategory ("Collections.Linq.Collection")]
		public void Collection_Concat ()
		{
			var collection = ToArray (Collection.Concat (new int[0], new int[0]));
			Assert.AreEqual (0, collection.Count);
			collection = ToArray (Collection.Concat (new int[0], new int[] { 9, 3, 1 }));
			Assert.AreEqual (3, collection.Count);
			Assert.AreEqual (9, collection[0]);
			Assert.AreEqual (3, collection[1]);
			Assert.AreEqual (1, collection[2]);
			collection = ToArray (Collection.Concat (new int[] { 9, 3, 1 }, new int[0]));
			Assert.AreEqual (3, collection.Count);
			Assert.AreEqual (9, collection[0]);
			Assert.AreEqual (3, collection[1]);
			Assert.AreEqual (1, collection[2]);
			collection = ToArray (Collection.Concat (new int[] { 9, 3, 1 }, new int[] { 2, 4 }));
			Assert.AreEqual (5, collection.Count);
			Assert.AreEqual (9, collection[0]);
			Assert.AreEqual (3, collection[1]);
			Assert.AreEqual (1, collection[2]);
			Assert.AreEqual (2, collection[3]);
			Assert.AreEqual (4, collection[4]);
		}

		[TestMethod]
		[TestCategory ("Collections.Linq.Collection")]
		public void Collection_Zip ()
		{
			var collection = ToArray (Collection.Zip (new int[0], new decimal[0], (i1, i2) => i1.ToString (System.Globalization.CultureInfo.InvariantCulture) + i2.ToString (System.Globalization.CultureInfo.InvariantCulture)));
			Assert.AreEqual (0, collection.Count);
			collection = ToArray (Collection.Zip (new int[] { 9, 3, 1 }, new decimal[0], (i1, i2) => i1.ToString (System.Globalization.CultureInfo.InvariantCulture) + i2.ToString (System.Globalization.CultureInfo.InvariantCulture)));
			Assert.AreEqual (0, collection.Count);
			collection = ToArray (Collection.Zip (new int[0], new decimal[] { 555.1m, 10m, 2.2m }, (i1, i2) => i1.ToString (System.Globalization.CultureInfo.InvariantCulture) + i2.ToString (System.Globalization.CultureInfo.InvariantCulture)));
			Assert.AreEqual (0, collection.Count);
			collection = ToArray (Collection.Zip (new int[] { 9, 3, 1, -2 }, new decimal[] { 555.1m, 10m, 2.2m }, (i1, i2) => i1.ToString (System.Globalization.CultureInfo.InvariantCulture) + i2.ToString (System.Globalization.CultureInfo.InvariantCulture)));
			Assert.AreEqual (3, collection.Count);
			Assert.AreEqual ("9555.1", collection[0]);
			Assert.AreEqual ("310", collection[1]);
			Assert.AreEqual ("12.2", collection[2]);
		}

		[TestMethod]
		[TestCategory ("Collections.Linq.Collection")]
		public void Collection_ToArray ()
		{
			var collection0 = Collection.ToArray (new System.DateTime[0]);
			Assert.AreEqual (0, collection0.Length);
			var collection1 = Collection.ToArray (new int[] { 9, 3, 1 });
			Assert.AreEqual (3, collection1.Length);
			Assert.AreEqual (9, collection1[0]);
			Assert.AreEqual (3, collection1[1]);
			Assert.AreEqual (1, collection1[2]);
			var collection2 = Collection.ToArray (new string[] { "three", "two", "one", "zero" });
			Assert.AreEqual (4, collection2.Length);
			Assert.AreEqual ("three", collection2[0]);
			Assert.AreEqual ("two", collection2[1]);
			Assert.AreEqual ("one", collection2[2]);
			Assert.AreEqual ("zero", collection2[3]);
		}
	}
}
