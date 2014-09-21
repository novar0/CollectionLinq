using System;
using System.Collections;
using System.Collections.Generic;

namespace BusinessClassLibrary.Collections.Linq
{
	/// <summary>
	/// Перечислитель диапазона элементов списка.
	/// </summary>
	/// <typeparam name="TSource">Тип элементов списка.</typeparam>
	internal class ListRangeEnumerator<TSource> : IEnumerator<TSource>
	{
		private readonly IReadOnlyList<TSource> _source;
		private readonly int _offset;
		private readonly int _count;
		private TSource _current;
		private int _index;

		internal ListRangeEnumerator (IReadOnlyList<TSource> source, int offset, int count)
		{
			_source = source;
			_offset = offset;
			_count = count;
			_index = -1;
			_current = default (TSource);
		}

		object IEnumerator.Current { get { return Current; } }
		public TSource Current
		{
			get
			{
				if (_index == -1)
				{
					throw new InvalidOperationException ("Can not get current element of enumeration because it not started.");
				}
				if (_index == -2)
				{
					throw new InvalidOperationException ("Can not get current element of enumeration because it already ended.");
				}
				return _current;
			}
		}

		public bool MoveNext ()
		{
			if (_index == -2)
			{
				return false;
			}

			_index++;
			if (_index == _count)
			{
				_index = -2;
				_current = default (TSource);
				return false;
			}
			_current = _source[_offset + _index];
			return true;
		}

		public void Reset ()
		{
			_index = -1;
			_current = default (TSource);
		}

		public void Dispose ()
		{
			_index = -2;
			_current = default (TSource);
		}
	}
}
