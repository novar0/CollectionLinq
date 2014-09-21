using System;
using System.Collections.Generic;

namespace BusinessClassLibrary.Collections.Linq
{
	/// <summary>
	/// Создатель сортированного индекса для коллекций.
	/// </summary>
	/// <typeparam name="TElement">Тип элементов коллекции.</typeparam>
	internal abstract class CollectionSorter<TElement> : IComparer<int>
	{
		protected readonly IReadOnlyCollection<TElement> _items;

		protected CollectionSorter (IReadOnlyCollection<TElement> items)
		{
			_items = items;
		}

		internal abstract void Initialize ();

		public abstract int Compare (int index1, int index2);

		internal int[] CreateIndex ()
		{
			Initialize ();
			var indexMap = new int[_items.Count];
			for (int index = 0; index < _items.Count; index++)
			{
				indexMap[index] = index;
			}
			Array.Sort<int> (indexMap, this);
			return indexMap;
		}
	}

	/// <summary>
	/// Создатель сортированного индекса для коллекций на основе сравнения ключей элементов, выбранных указанной функцией.
	/// </summary>
	/// <typeparam name="TElement">Тип элементов коллекции.</typeparam>
	/// <typeparam name="TKey">Тип сортирующего ключа коллекции.</typeparam>
	internal class CollectionSorter<TElement, TKey> : CollectionSorter<TElement>
	{
		private readonly Func<TElement, TKey> _keySelector;
		private readonly IComparer<TKey> _comparer;
		private readonly bool _reverseOrder;
		private readonly CollectionSorter<TElement> _childSorter;
		private TKey[] _keys;

		internal CollectionSorter (
			IReadOnlyCollection<TElement> items,
			Func<TElement, TKey> keySelector,
			IComparer<TKey> comparer,
			bool reverseOrder,
			CollectionSorter<TElement> childSorter)
			: base (items)
		{
			_keySelector = keySelector;
			_comparer = comparer;
			_reverseOrder = reverseOrder;
			_keys = new TKey[_items.Count];
			_childSorter = childSorter;
		}

		internal override void Initialize ()
		{
			int index = 0;
			foreach (var item in _items)
			{
				_keys[index++] = _keySelector.Invoke (item);
			}
			if (_childSorter != null)
			{
				_childSorter.Initialize ();
			}
		}

		public override int Compare (int index1, int index2)
		{
			var num = _comparer.Compare (_keys[index1], _keys[index2]);
			return (num == 0) ?
				(_childSorter == null) ? (index1 - index2) : _childSorter.Compare (index1, index2) :
				_reverseOrder ? -num : num;
		}
	}
}
