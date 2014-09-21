using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessClassLibrary.Collections.Linq.Test
{
	[TestClass]
	public class ListTests
	{
		[TestMethod]
		[TestCategory ("Collections.Linq.List")]
		public void List_Empty ()
		{
			var list = List.Empty<int> ();
			Assert.AreEqual (0, list.Count);
		}

		[TestMethod]
		[TestCategory ("Collections.Linq.List")]
		public void List_Range ()
		{
			var list = List.Range (0, 0);
			Assert.AreEqual (0, list.Count);
			list = List.Range (0, 1);
			Assert.AreEqual (1, list.Count);
			Assert.AreEqual (0, list[0]);
			list = List.Range (-2, 5);
			Assert.AreEqual (5, list.Count);
			Assert.AreEqual (-2, list[0]);
			Assert.AreEqual (-1, list[1]);
			Assert.AreEqual (0, list[2]);
			Assert.AreEqual (1, list[3]);
			Assert.AreEqual (2, list[4]);
		}

		[TestMethod]
		[TestCategory ("Collections.Linq.List")]
		public void List_Repeat ()
		{
			var list = List.Repeat<int> (0, 0);
			Assert.AreEqual (0, list.Count);
			list = List.Repeat<int> (1, 1);
			Assert.AreEqual (1, list.Count);
			Assert.AreEqual (1, list[0]);
			string tmpl = "one";
			var list2 = List.Repeat<string> (tmpl, 5);
			Assert.AreEqual (5, list2.Count);
			Assert.AreEqual (tmpl, list2[0]);
			Assert.AreEqual (tmpl, list2[1]);
			Assert.AreEqual (tmpl, list2[2]);
			Assert.AreEqual (tmpl, list2[3]);
			Assert.AreEqual (tmpl, list2[4]);
		}

		[TestMethod]
		[TestCategory ("Collections.Linq.List")]
		public void List_First ()
		{
			var list = new int[] { 3 };
			Assert.AreEqual (3, List.First (list));
			list = new int[] { 9, 3, 1 };
			Assert.AreEqual (9, List.First (list));
		}

		[TestMethod]
		[TestCategory ("Collections.Linq.List")]
		public void List_FirstOrDefault ()
		{
			var list = new int[] { };
			Assert.AreEqual (0, List.FirstOrDefault (list));
			list = new int[] { 3 };
			Assert.AreEqual (3, List.FirstOrDefault (list));
			list = new int[] { 9, 3, 1 };
			Assert.AreEqual (9, List.FirstOrDefault (list));
			var list2 = new string[] { };
			Assert.AreEqual ((string)null, List.FirstOrDefault (list2));
			list2 = new string[] { "one" };
			Assert.AreEqual ("one", List.FirstOrDefault (list2));
			list2 = new string[] { "three", "two", "one" };
			Assert.AreEqual ("three", List.FirstOrDefault (list2));
		}

		[TestMethod]
		[TestCategory ("Collections.Linq.List")]
		public void List_Last ()
		{
			var list = new int[] { 3 };
			Assert.AreEqual (3, List.Last (list));
			list = new int[] { 9, 3, 1 };
			Assert.AreEqual (1, List.Last (list));
		}

		[TestMethod]
		[TestCategory ("Collections.Linq.List")]
		public void List_LastOrDefault ()
		{
			var list = new int[] { };
			Assert.AreEqual (0, List.LastOrDefault (list));
			list = new int[] { 3 };
			Assert.AreEqual (3, List.LastOrDefault (list));
			list = new int[] { 9, 3, 1 };
			Assert.AreEqual (1, List.LastOrDefault (list));
			var list2 = new string[] { };
			Assert.AreEqual ((string)null, List.LastOrDefault (list2));
			list2 = new string[] { "one" };
			Assert.AreEqual ("one", List.LastOrDefault (list2));
			list2 = new string[] { "three", "two", "one" };
			Assert.AreEqual ("one", List.LastOrDefault (list2));
		}

		[TestMethod]
		[TestCategory ("Collections.Linq.List")]
		public void List_ElementAt ()
		{
			var list = new int[] { 3 };
			Assert.AreEqual (3, List.ElementAt (list, 0));
			list = new int[] { 9, 3, 1 };
			Assert.AreEqual (3, List.ElementAt (list, 1));
		}

		[TestMethod]
		[TestCategory ("Collections.Linq.List")]
		public void List_ElementAtOrDefault ()
		{
			var list = new int[0];
			Assert.AreEqual (0, List.ElementAtOrDefault (list, 3));
			list = new int[] { 3 };
			Assert.AreEqual (3, List.ElementAtOrDefault (list, 0));
			Assert.AreEqual (0, List.ElementAtOrDefault (list, 1));
			list = new int[] { 9, 3, 1 };
			Assert.AreEqual (1, List.ElementAtOrDefault (list, 2));
			var list2 = new string[] { };
			Assert.AreEqual ((string)null, List.ElementAtOrDefault (list2, 0));
			list2 = new string[] { "one" };
			Assert.AreEqual ("one", List.ElementAtOrDefault (list2, 0));
			list2 = new string[] { "three", "two", "one" };
			Assert.AreEqual ("two", List.ElementAtOrDefault (list2, 1));
			Assert.AreEqual ((string)null, List.ElementAtOrDefault (list2, 3));
		}

		[TestMethod]
		[TestCategory ("Collections.Linq.List")]
		public void List_DefaultIfEmpty ()
		{
			var list = List.DefaultIfEmpty (new int[0], 999);
			Assert.AreEqual (1, list.Count);
			Assert.AreEqual (999, list[0]);
			list = List.DefaultIfEmpty (new int[] { 9, 3, 1 }, 999);
			Assert.AreEqual (3, list.Count);
			Assert.AreEqual (9, list[0]);
			Assert.AreEqual (3, list[1]);
			Assert.AreEqual (1, list[2]);
			var list2 = List.DefaultIfEmpty (new string[0], "999");
			Assert.AreEqual (1, list2.Count);
			Assert.AreEqual ("999", list2[0]);
			list2 = List.DefaultIfEmpty (new string[] { "three", "two", "one" }, "999");
			Assert.AreEqual (3, list2.Count);
			Assert.AreEqual ("three", list2[0]);
			Assert.AreEqual ("two", list2[1]);
			Assert.AreEqual ("one", list2[2]);
		}

		[TestMethod]
		[TestCategory ("Collections.Linq.List")]
		public void List_Skip ()
		{
			var list = List.Skip (new int[0], 3);
			Assert.AreEqual (0, list.Count);
			list = List.Skip (new int[] { 9, 3, 1 }, 0);
			Assert.AreEqual (3, list.Count);
			Assert.AreEqual (9, list[0]);
			Assert.AreEqual (3, list[1]);
			Assert.AreEqual (1, list[2]);
			list = List.Skip (new int[] { 9, 3, 1 }, 2);
			Assert.AreEqual (1, list.Count);
			Assert.AreEqual (1, list[0]);

			var list2 = List.Skip (new string[0], 3);
			Assert.AreEqual (0, list2.Count);
			list2 = List.Skip (new string[] { "three", "two", "one" }, 0);
			Assert.AreEqual (3, list2.Count);
			Assert.AreEqual ("three", list2[0]);
			Assert.AreEqual ("two", list2[1]);
			Assert.AreEqual ("one", list2[2]);
			list2 = List.Skip (new string[] { "three", "two", "one" }, 2);
			Assert.AreEqual (1, list2.Count);
			Assert.AreEqual ("one", list2[0]);
		}

		[TestMethod]
		[TestCategory ("Collections.Linq.List")]
		public void List_Take ()
		{
			var list = List.Take (new int[0], 3);
			Assert.AreEqual (0, list.Count);
			list = List.Take (new int[] { 9, 3, 1 }, 0);
			Assert.AreEqual (0, list.Count);
			list = List.Take (new int[] { 9, 3, 1 }, 5);
			Assert.AreEqual (3, list.Count);
			Assert.AreEqual (9, list[0]);
			Assert.AreEqual (3, list[1]);
			Assert.AreEqual (1, list[2]);
			list = List.Take (new int[] { 9, 3, 1 }, 1);
			Assert.AreEqual (1, list.Count);
			Assert.AreEqual (9, list[0]);

			var list2 = List.Take (new string[0], 3);
			Assert.AreEqual (0, list2.Count);
			list2 = List.Take (new string[] { "three", "two", "one" }, 0);
			Assert.AreEqual (0, list2.Count);
			list2 = List.Take (new string[] { "three", "two", "one" }, 4);
			Assert.AreEqual (3, list2.Count);
			Assert.AreEqual ("three", list2[0]);
			Assert.AreEqual ("two", list2[1]);
			Assert.AreEqual ("one", list2[2]);
			list2 = List.Take (new string[] { "three", "two", "one" }, 1);
			Assert.AreEqual (1, list2.Count);
			Assert.AreEqual ("three", list2[0]);
		}

		[TestMethod]
		[TestCategory ("Collections.Linq.List")]
		public void List_Select ()
		{
			var list = List.Select (new int[0], item => item * 2);
			Assert.AreEqual (0, list.Count);
			list = List.Select (new int[] { 9, 3, 1 }, item => item + 3);
			Assert.AreEqual (3, list.Count);
			Assert.AreEqual (12, list[0]);
			Assert.AreEqual (6, list[1]);
			Assert.AreEqual (4, list[2]);

			var list2 = List.Select (new string[0], item => item + "_");
			Assert.AreEqual (0, list2.Count);
			list2 = List.Select (new string[] { "three", "two", "one" }, item => item + "_");
			Assert.AreEqual (3, list2.Count);
			Assert.AreEqual ("three_", list2[0]);
			Assert.AreEqual ("two_", list2[1]);
			Assert.AreEqual ("one_", list2[2]);
		}

		[TestMethod]
		[TestCategory ("Collections.Linq.List")]
		public void List_SelectIdx ()
		{
			var list = List.Select (new int[0], (item, idx) => item * idx);
			Assert.AreEqual (0, list.Count);
			list = List.Select (new int[] { 9, 3, 1 }, (item, idx) => item * idx);
			Assert.AreEqual (3, list.Count);
			Assert.AreEqual (0, list[0]);
			Assert.AreEqual (3, list[1]);
			Assert.AreEqual (2, list[2]);

			var list2 = List.Select (new string[0], (item, idx) => item + idx.ToString (System.Globalization.CultureInfo.InvariantCulture));
			Assert.AreEqual (0, list2.Count);
			list2 = List.Select (new string[] { "three", "two", "one" }, (item, idx) => item + idx.ToString (System.Globalization.CultureInfo.InvariantCulture));
			Assert.AreEqual (3, list2.Count);
			Assert.AreEqual ("three0", list2[0]);
			Assert.AreEqual ("two1", list2[1]);
			Assert.AreEqual ("one2", list2[2]);
		}

		[TestMethod]
		[TestCategory ("Collections.Linq.List")]
		public void List_Reverse ()
		{
			var list = List.Reverse (new int[0]);
			Assert.AreEqual (0, list.Count);
			list = List.Reverse (new int[] { 3 });
			Assert.AreEqual (1, list.Count);
			Assert.AreEqual (3, list[0]);
			list = List.Reverse (new int[] { 9, 3, 1 });
			Assert.AreEqual (3, list.Count);
			Assert.AreEqual (1, list[0]);
			Assert.AreEqual (3, list[1]);
			Assert.AreEqual (9, list[2]);

			var list2 = List.Reverse (new string[0]);
			Assert.AreEqual (0, list2.Count);
			list2 = List.Reverse (new string[] { "two" });
			Assert.AreEqual (1, list2.Count);
			Assert.AreEqual ("two", list2[0]);
			list2 = List.Reverse (new string[] { "three", "two", "one" });
			Assert.AreEqual (3, list2.Count);
			Assert.AreEqual ("one", list2[0]);
			Assert.AreEqual ("two", list2[1]);
			Assert.AreEqual ("three", list2[2]);
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
		[TestCategory ("Collections.Linq.List")]
		public void List_Ordering ()
		{
			var list = List.OrderBy (new int[0], item => item * 3);
			Assert.AreEqual (0, list.Count);
			var source = new int[] { 9, 1, -3 };
			list = List.OrderBy (source, item => item);
			Assert.AreEqual (3, list.Count);
			Assert.AreEqual (-3, list[0]);
			Assert.AreEqual (1, list[1]);
			Assert.AreEqual (9, list[2]);
			list = List.OrderBy (source, item => System.Math.Abs (item));
			Assert.AreEqual (3, list.Count);
			Assert.AreEqual (1, list[0]);
			Assert.AreEqual (-3, list[1]);
			Assert.AreEqual (9, list[2]);

			list = List.OrderByDescending (new int[0], item => item * 3);
			Assert.AreEqual (0, list.Count);
			list = List.OrderByDescending (source, item => item);
			Assert.AreEqual (3, list.Count);
			Assert.AreEqual (9, list[0]);
			Assert.AreEqual (1, list[1]);
			Assert.AreEqual (-3, list[2]);
			list = List.OrderByDescending (source, item => System.Math.Abs (item));
			Assert.AreEqual (3, list.Count);
			Assert.AreEqual (9, list[0]);
			Assert.AreEqual (-3, list[1]);
			Assert.AreEqual (1, list[2]);

			var sizeComparer = new StringLenComparer ();
			var src = new string[] { "333", "222", "111", "11", "1111", null, "aaa", "a", "" };
			var list2 = List.ThenBy (List.OrderBy (src, item => item, sizeComparer), item => item, null);
			Assert.AreEqual (9, list2.Count);
			Assert.AreEqual (null, list2[0]);
			Assert.AreEqual ("", list2[1]);
			Assert.AreEqual ("a", list2[2]);
			Assert.AreEqual ("11", list2[3]);
			Assert.AreEqual ("111", list2[4]);
			Assert.AreEqual ("222", list2[5]);
			Assert.AreEqual ("333", list2[6]);
			Assert.AreEqual ("aaa", list2[7]);
			Assert.AreEqual ("1111", list2[8]);

			list2 = List.ThenBy (List.OrderByDescending (src, item => item, sizeComparer), item => item, null);
			Assert.AreEqual (9, list2.Count);
			Assert.AreEqual ("1111", list2[0]);
			Assert.AreEqual ("111", list2[1]);
			Assert.AreEqual ("222", list2[2]);
			Assert.AreEqual ("333", list2[3]);
			Assert.AreEqual ("aaa", list2[4]);
			Assert.AreEqual ("11", list2[5]);
			Assert.AreEqual ("a", list2[6]);
			Assert.AreEqual ("", list2[7]);
			Assert.AreEqual (null, list2[8]);

			list2 = List.ThenByDescending (List.OrderBy (src, item => item, sizeComparer), item => item, null);
			Assert.AreEqual (9, list2.Count);
			Assert.AreEqual (null, list2[0]);
			Assert.AreEqual ("", list2[1]);
			Assert.AreEqual ("a", list2[2]);
			Assert.AreEqual ("11", list2[3]);
			Assert.AreEqual ("aaa", list2[4]);
			Assert.AreEqual ("333", list2[5]);
			Assert.AreEqual ("222", list2[6]);
			Assert.AreEqual ("111", list2[7]);
			Assert.AreEqual ("1111", list2[8]);
		}

		[TestMethod]
		[TestCategory ("Collections.Linq.List")]
		public void List_Concat ()
		{
			var list = List.Concat (new int[0], new int[0]);
			Assert.AreEqual (0, list.Count);
			list = List.Concat (new int[0], new int[] { 9, 3, 1 });
			Assert.AreEqual (3, list.Count);
			Assert.AreEqual (9, list[0]);
			Assert.AreEqual (3, list[1]);
			Assert.AreEqual (1, list[2]);
			list = List.Concat (new int[] { 9, 3, 1 }, new int[0]);
			Assert.AreEqual (3, list.Count);
			Assert.AreEqual (9, list[0]);
			Assert.AreEqual (3, list[1]);
			Assert.AreEqual (1, list[2]);
			list = List.Concat (new int[] { 9, 3, 1 }, new int[] { 2, 4 });
			Assert.AreEqual (5, list.Count);
			Assert.AreEqual (9, list[0]);
			Assert.AreEqual (3, list[1]);
			Assert.AreEqual (1, list[2]);
			Assert.AreEqual (2, list[3]);
			Assert.AreEqual (4, list[4]);
		}

		[TestMethod]
		[TestCategory ("Collections.Linq.List")]
		public void List_Zip ()
		{
			var list = List.Zip (new int[0], new decimal[0], (i1, i2) => i1.ToString (System.Globalization.CultureInfo.InvariantCulture) + i2.ToString (System.Globalization.CultureInfo.InvariantCulture));
			Assert.AreEqual (0, list.Count);
			list = List.Zip (new int[] { 9, 3, 1 }, new decimal[0], (i1, i2) => i1.ToString (System.Globalization.CultureInfo.InvariantCulture) + i2.ToString (System.Globalization.CultureInfo.InvariantCulture));
			Assert.AreEqual (0, list.Count);
			list = List.Zip (new int[0], new decimal[] {555.1m, 10m, 2.2m }, (i1, i2) => i1.ToString (System.Globalization.CultureInfo.InvariantCulture) + i2.ToString (System.Globalization.CultureInfo.InvariantCulture));
			Assert.AreEqual (0, list.Count);
			list = List.Zip (new int[] { 9, 3, 1, -2 }, new decimal[] { 555.1m, 10m, 2.2m }, (i1, i2) => i1.ToString (System.Globalization.CultureInfo.InvariantCulture) + i2.ToString (System.Globalization.CultureInfo.InvariantCulture));
			Assert.AreEqual (3, list.Count);
			Assert.AreEqual ("9555.1", list[0]);
			Assert.AreEqual ("310", list[1]);
			Assert.AreEqual ("12.2", list[2]);
		}
	}
}
