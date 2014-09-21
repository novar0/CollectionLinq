using System;
using System.Collections;
using System.Collections.Generic;

namespace BusinessClassLibrary.Collections.Linq
{
	/// <summary>
	/// Перечислитель элементов двух списков друг за другом.
	/// </summary>
	/// <typeparam name="TSource">Тип элементов списков.</typeparam>
	internal class TwoListConcatEnumerator<TSource> : IEnumerator<TSource>
	{
		private readonly IReadOnlyList<TSource> _first;
		private readonly IReadOnlyList<TSource> _second;
		private TSource _current;
		private int _index;

		internal TwoListConcatEnumerator (IReadOnlyList<TSource> first, IReadOnlyList<TSource> second)
		{
			_first = first;
			_second = second;
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
			if (_index == (_first.Count + _second.Count))
			{
				_index = -2;
				_current = default (TSource);
				return false;
			}
			_current = (_index < _first.Count) ? _first[_index] : _second[_index - _first.Count];
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
