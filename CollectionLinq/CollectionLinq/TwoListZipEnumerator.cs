using System;
using System.Collections;
using System.Collections.Generic;

namespace BusinessClassLibrary.Collections.Linq
{
	/// <summary>
	/// Перечислитель элементов, образованных применением функции к элементам сразу двух списков.
	/// </summary>
	/// <typeparam name="TFirst">Тип элементов первого списка.</typeparam>
	/// <typeparam name="TSecond">Тип элементов второго списка.</typeparam>
	/// <typeparam name="TResult">Тип элементов результирующего списка.</typeparam>
	internal class TwoListZipEnumerator<TFirst, TSecond, TResult> : IEnumerator<TResult>
	{
		private readonly IReadOnlyList<TFirst> _first;
		private readonly IReadOnlyList<TSecond> _second;
		private readonly Func<TFirst, TSecond, TResult> _selector;
		private TResult _current;
		private int _index;

		internal TwoListZipEnumerator (IReadOnlyList<TFirst> first, IReadOnlyList<TSecond> second, Func<TFirst, TSecond, TResult> selector)
		{
			_first = first;
			_second = second;
			_selector = selector;
			_index = -1;
			_current = default (TResult);
		}

		object IEnumerator.Current { get { return Current; } }
		public TResult Current
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
			if (_index == Math.Min (_first.Count, _second.Count))
			{
				_index = -2;
				_current = default (TResult);
				return false;
			}
			_current = _selector.Invoke (_first[_index], _second[_index]);
			return true;
		}

		public void Reset ()
		{
			_index = -1;
			_current = default (TResult);
		}

		public void Dispose ()
		{
			_index = -2;
			_current = default (TResult);
		}
	}
}
