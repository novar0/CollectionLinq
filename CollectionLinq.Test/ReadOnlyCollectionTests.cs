using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessClassLibrary.Collections.Linq.Test
{
	[TestClass]
	public class ReadOnlyCollectionTests
	{
		private static IReadOnlyList<T> ToArray<T> (IEnumerable<T> enumerable)
		{
			return System.Linq.Enumerable.ToArray (enumerable);
		}

		[TestMethod]
		[TestCategory ("Collections.Linq.ReadOnlyCollection")]
		public void Collection_Any ()
		{
			Assert.IsFalse (ReadOnlyCollection.Any (new int[0]));
			Assert.IsTrue (ReadOnlyCollection.Any (new int[] { 3 }));
			Assert.IsTrue (ReadOnlyCollection.Any (new string[] { "one", "two", "three" }));
		}

		[TestMethod]
		[TestCategory ("Collections.Linq.ReadOnlyCollection")]
		public void Collection_Count ()
		{
			Assert.AreEqual (0, ReadOnlyCollection.Count (new int[0]));
			Assert.AreEqual (1, ReadOnlyCollection.Count (new int[] { 3 }));
			Assert.AreEqual (3, ReadOnlyCollection.Count (new string[] { "one", "two", "three" }));
			Assert.AreEqual (0L, ReadOnlyCollection.LongCount (new int[0]));
			Assert.AreEqual (1L, ReadOnlyCollection.LongCount (new int[] { 3 }));
			Assert.AreEqual (3L, ReadOnlyCollection.LongCount (new string[] { "one", "two", "three" }));
		}

		[TestMethod]
		[TestCategory ("Collections.Linq.ReadOnlyCollection")]
		public void Collection_DefaultIfEmpty ()
		{
			var collection = ToArray (ReadOnlyCollection.DefaultIfEmpty (new int[0], 999));
			Assert.AreEqual (1, collection.Count);
			Assert.AreEqual (999, collection[0]);
			collection = ToArray (ReadOnlyCollection.DefaultIfEmpty (new int[] { 9, 3, 1 }, 999));
			Assert.AreEqual (3, collection.Count);
			Assert.AreEqual (9, collection[0]);
			Assert.AreEqual (3, collection[1]);
			Assert.AreEqual (1, collection[2]);
			var collection2 = ToArray (ReadOnlyCollection.DefaultIfEmpty (new string[0], "999"));
			Assert.AreEqual (1, collection2.Count);
			Assert.AreEqual ("999", collection2[0]);
			collection2 = ToArray (ReadOnlyCollection.DefaultIfEmpty (new string[] { "three", "two", "one" }, "999"));
			Assert.AreEqual (3, collection2.Count);
			Assert.AreEqual ("three", collection2[0]);
			Assert.AreEqual ("two", collection2[1]);
			Assert.AreEqual ("one", collection2[2]);
		}

		[TestMethod]
		[TestCategory ("Collections.Linq.ReadOnlyCollection")]
		public void Collection_Skip ()
		{
			var collection = ToArray (ReadOnlyCollection.Skip (new int[0], 3));
			Assert.AreEqual (0, collection.Count);
			collection = ToArray (ReadOnlyCollection.Skip (new int[] { 9, 3, 1 }, 0));
			Assert.AreEqual (3, collection.Count);
			Assert.AreEqual (9, collection[0]);
			Assert.AreEqual (3, collection[1]);
			Assert.AreEqual (1, collection[2]);
			collection = ToArray (ReadOnlyCollection.Skip (new int[] { 9, 3, 1 }, 2));
			Assert.AreEqual (1, collection.Count);
			Assert.AreEqual (1, collection[0]);

			var collection2 = ToArray (ReadOnlyCollection.Skip (new string[0], 3));
			Assert.AreEqual (0, collection2.Count);
			collection2 = ToArray (ReadOnlyCollection.Skip (new string[] { "three", "two", "one" }, 0));
			Assert.AreEqual (3, collection2.Count);
			Assert.AreEqual ("three", collection2[0]);
			Assert.AreEqual ("two", collection2[1]);
			Assert.AreEqual ("one", collection2[2]);
			collection2 = ToArray (ReadOnlyCollection.Skip (new string[] { "three", "two", "one" }, 2));
			Assert.AreEqual (1, collection2.Count);
			Assert.AreEqual ("one", collection2[0]);
		}

		[TestMethod]
		[TestCategory ("Collections.Linq.ReadOnlyCollection")]
		public void Collection_Take ()
		{
			var collection = ToArray (ReadOnlyCollection.Take (new int[0], 3));
			Assert.AreEqual (0, collection.Count);
			collection = ToArray (ReadOnlyCollection.Take (new int[] { 9, 3, 1 }, 0));
			Assert.AreEqual (0, collection.Count);
			collection = ToArray (ReadOnlyCollection.Take (new int[] { 9, 3, 1 }, 5));
			Assert.AreEqual (3, collection.Count);
			Assert.AreEqual (9, collection[0]);
			Assert.AreEqual (3, collection[1]);
			Assert.AreEqual (1, collection[2]);
			collection = ToArray (ReadOnlyCollection.Take (new int[] { 9, 3, 1 }, 1));
			Assert.AreEqual (1, collection.Count);
			Assert.AreEqual (9, collection[0]);

			var collection2 = ToArray (ReadOnlyCollection.Take (new string[0], 3));
			Assert.AreEqual (0, collection2.Count);
			collection2 = ToArray (ReadOnlyCollection.Take (new string[] { "three", "two", "one" }, 0));
			Assert.AreEqual (0, collection2.Count);
			collection2 = ToArray (ReadOnlyCollection.Take (new string[] { "three", "two", "one" }, 4));
			Assert.AreEqual (3, collection2.Count);
			Assert.AreEqual ("three", collection2[0]);
			Assert.AreEqual ("two", collection2[1]);
			Assert.AreEqual ("one", collection2[2]);
			collection2 = ToArray (ReadOnlyCollection.Take (new string[] { "three", "two", "one" }, 1));
			Assert.AreEqual (1, collection2.Count);
			Assert.AreEqual ("three", collection2[0]);
		}

		[TestMethod]
		[TestCategory ("Collections.Linq.ReadOnlyCollection")]
		public void Collection_Select ()
		{
			var collection = ToArray (ReadOnlyCollection.Select (new int[0], item => (item * 2).ToString ()));
			Assert.AreEqual (0, collection.Count);
			collection = ToArray (ReadOnlyCollection.Select (new int[] { 9, 3, 1 }, item => (item + 3).ToString (System.Globalization.DateTimeFormatInfo.InvariantInfo)));
			Assert.AreEqual (3, collection.Count);
			Assert.AreEqual ("12", collection[0]);
			Assert.AreEqual ("6", collection[1]);
			Assert.AreEqual ("4", collection[2]);

			var collection2 = ToArray (ReadOnlyCollection.Select (new string[0], item => item + "_"));
			Assert.AreEqual (0, collection2.Count);
			collection2 = ToArray (ReadOnlyCollection.Select (new string[] { "three", "two", "one" }, item => item + "_"));
			Assert.AreEqual (3, collection2.Count);
			Assert.AreEqual ("three_", collection2[0]);
			Assert.AreEqual ("two_", collection2[1]);
			Assert.AreEqual ("one_", collection2[2]);
		}

		[TestMethod]
		[TestCategory ("Collections.Linq.ReadOnlyCollection")]
		public void Collection_SelectIdx ()
		{
			var collection = ToArray (ReadOnlyCollection.Select (new int[0], (item, idx) => (item * idx).ToString ()));
			Assert.AreEqual (0, collection.Count);
			collection = ToArray (ReadOnlyCollection.Select (new int[] { 9, 3, 1 }, (item, idx) => (item * idx).ToString (System.Globalization.DateTimeFormatInfo.InvariantInfo)));
			Assert.AreEqual (3, collection.Count);
			Assert.AreEqual ("0", collection[0]);
			Assert.AreEqual ("3", collection[1]);
			Assert.AreEqual ("2", collection[2]);

			var collection2 = ToArray (ReadOnlyCollection.Select (new string[0], (item, idx) => item + idx.ToString (System.Globalization.CultureInfo.InvariantCulture)));
			Assert.AreEqual (0, collection2.Count);
			collection2 = ToArray (ReadOnlyCollection.Select (new string[] { "three", "two", "one" }, (item, idx) => item + idx.ToString (System.Globalization.CultureInfo.InvariantCulture)));
			Assert.AreEqual (3, collection2.Count);
			Assert.AreEqual ("three0", collection2[0]);
			Assert.AreEqual ("two1", collection2[1]);
			Assert.AreEqual ("one2", collection2[2]);
		}

		[TestMethod]
		[TestCategory ("Collections.Linq.ReadOnlyCollection")]
		public void Collection_Reverse ()
		{
			var collection = ToArray (ReadOnlyCollection.Reverse (new int[0]));
			Assert.AreEqual (0, collection.Count);
			collection = ToArray (ReadOnlyCollection.Reverse (new int[] { 3 }));
			Assert.AreEqual (1, collection.Count);
			Assert.AreEqual (3, collection[0]);
			collection = ToArray (ReadOnlyCollection.Reverse (new int[] { 9, 3, 1 }));
			Assert.AreEqual (3, collection.Count);
			Assert.AreEqual (1, collection[0]);
			Assert.AreEqual (3, collection[1]);
			Assert.AreEqual (9, collection[2]);

			var collection2 = ToArray (ReadOnlyCollection.Reverse (new string[0]));
			Assert.AreEqual (0, collection2.Count);
			collection2 = ToArray (ReadOnlyCollection.Reverse (new string[] { "two" }));
			Assert.AreEqual (1, collection2.Count);
			Assert.AreEqual ("two", collection2[0]);
			collection2 = ToArray (ReadOnlyCollection.Reverse (new string[] { "three", "two", "one" }));
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
		[TestCategory ("Collections.Linq.ReadOnlyCollection")]
		public void Collection_Ordering ()
		{
			var collection = ToArray (ReadOnlyCollection.OrderBy (new int[0], item => item * 3));
			Assert.AreEqual (0, collection.Count);
			var source = new int[] { 9, 1, -3 };
			collection = ToArray (ReadOnlyCollection.OrderBy (source, item => item));
			Assert.AreEqual (3, collection.Count);
			Assert.AreEqual (-3, collection[0]);
			Assert.AreEqual (1, collection[1]);
			Assert.AreEqual (9, collection[2]);
			collection = ToArray (ReadOnlyCollection.OrderBy (source, item => System.Math.Abs (item)));
			Assert.AreEqual (3, collection.Count);
			Assert.AreEqual (1, collection[0]);
			Assert.AreEqual (-3, collection[1]);
			Assert.AreEqual (9, collection[2]);

			collection = ToArray (ReadOnlyCollection.OrderByDescending (new int[0], item => item * 3));
			Assert.AreEqual (0, collection.Count);
			collection = ToArray (ReadOnlyCollection.OrderByDescending (source, item => item));
			Assert.AreEqual (3, collection.Count);
			Assert.AreEqual (9, collection[0]);
			Assert.AreEqual (1, collection[1]);
			Assert.AreEqual (-3, collection[2]);
			collection = ToArray (ReadOnlyCollection.OrderByDescending (source, item => System.Math.Abs (item)));
			Assert.AreEqual (3, collection.Count);
			Assert.AreEqual (9, collection[0]);
			Assert.AreEqual (-3, collection[1]);
			Assert.AreEqual (1, collection[2]);

			var sizeComparer = new StringLenComparer ();
			var col = new string[] { "333", "222", "111", "11", "1111", null, "aaa", "a", "" };
			var collection2 = ToArray (ReadOnlyCollection.ThenBy (ReadOnlyCollection.OrderBy (col, item => item, sizeComparer), item => item, null));
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

			collection2 = ToArray (ReadOnlyCollection.ThenBy (ReadOnlyCollection.OrderByDescending (col, item => item, sizeComparer), item => item, null));
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

			collection2 = ToArray (ReadOnlyCollection.ThenByDescending (ReadOnlyCollection.OrderBy (col, item => item, sizeComparer), item => item, null));
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
		[TestCategory ("Collections.Linq.ReadOnlyCollection")]
		public void Collection_Concat ()
		{
			var collection = ToArray (ReadOnlyCollection.Concat (new int[0], new int[0]));
			Assert.AreEqual (0, collection.Count);
			collection = ToArray (ReadOnlyCollection.Concat (new int[0], new int[] { 9, 3, 1 }));
			Assert.AreEqual (3, collection.Count);
			Assert.AreEqual (9, collection[0]);
			Assert.AreEqual (3, collection[1]);
			Assert.AreEqual (1, collection[2]);
			collection = ToArray (ReadOnlyCollection.Concat (new int[] { 9, 3, 1 }, new int[0]));
			Assert.AreEqual (3, collection.Count);
			Assert.AreEqual (9, collection[0]);
			Assert.AreEqual (3, collection[1]);
			Assert.AreEqual (1, collection[2]);
			collection = ToArray (ReadOnlyCollection.Concat (new int[] { 9, 3, 1 }, new int[] { 2, 4 }));
			Assert.AreEqual (5, collection.Count);
			Assert.AreEqual (9, collection[0]);
			Assert.AreEqual (3, collection[1]);
			Assert.AreEqual (1, collection[2]);
			Assert.AreEqual (2, collection[3]);
			Assert.AreEqual (4, collection[4]);
		}

		[TestMethod]
		[TestCategory ("Collections.Linq.ReadOnlyCollection")]
		public void Collection_Zip ()
		{
			var collection = ToArray (ReadOnlyCollection.Zip (new int[0], new decimal[0], (i1, i2) => i1.ToString (System.Globalization.CultureInfo.InvariantCulture) + i2.ToString (System.Globalization.CultureInfo.InvariantCulture)));
			Assert.AreEqual (0, collection.Count);
			collection = ToArray (ReadOnlyCollection.Zip (new int[] { 9, 3, 1 }, new decimal[0], (i1, i2) => i1.ToString (System.Globalization.CultureInfo.InvariantCulture) + i2.ToString (System.Globalization.CultureInfo.InvariantCulture)));
			Assert.AreEqual (0, collection.Count);
			collection = ToArray (ReadOnlyCollection.Zip (new int[0], new decimal[] { 555.1m, 10m, 2.2m }, (i1, i2) => i1.ToString (System.Globalization.CultureInfo.InvariantCulture) + i2.ToString (System.Globalization.CultureInfo.InvariantCulture)));
			Assert.AreEqual (0, collection.Count);
			collection = ToArray (ReadOnlyCollection.Zip (new int[] { 9, 3, 1, -2 }, new decimal[] { 555.1m, 10m, 2.2m }, (i1, i2) => i1.ToString (System.Globalization.CultureInfo.InvariantCulture) + i2.ToString (System.Globalization.CultureInfo.InvariantCulture)));
			Assert.AreEqual (3, collection.Count);
			Assert.AreEqual ("9555.1", collection[0]);
			Assert.AreEqual ("310", collection[1]);
			Assert.AreEqual ("12.2", collection[2]);
		}

		[TestMethod]
		[TestCategory ("Collections.Linq.ReadOnlyCollection")]
		public void Collection_ToArray ()
		{
			var collection0 = ReadOnlyCollection.ToArray (new System.DateTime[0]);
			Assert.AreEqual (0, collection0.Length);
			var collection1 = ReadOnlyCollection.ToArray (new int[] { 9, 3, 1 });
			Assert.AreEqual (3, collection1.Length);
			Assert.AreEqual (9, collection1[0]);
			Assert.AreEqual (3, collection1[1]);
			Assert.AreEqual (1, collection1[2]);
			var collection2 = ReadOnlyCollection.ToArray (new string[] { "three", "two", "one", "zero" });
			Assert.AreEqual (4, collection2.Length);
			Assert.AreEqual ("three", collection2[0]);
			Assert.AreEqual ("two", collection2[1]);
			Assert.AreEqual ("one", collection2[2]);
			Assert.AreEqual ("zero", collection2[3]);
		}
	}
}
