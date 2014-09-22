using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace BusinessClassLibrary.Collections.Linq
{
	/// <summary>
	/// Методы расширения к конечным множествам.
	/// </summary>
	[System.Diagnostics.CodeAnalysis.SuppressMessage ("Microsoft.Naming",
		"CA1711:IdentifiersShouldNotHaveIncorrectSuffix",
		Justification = "Analogous to System.Linq.Enumerable.")]
	public static class ReadOnlyFiniteSet
	{
		/// <summary>
		/// Создаёт пустое множество.
		/// </summary>
		/// <typeparam name="TResult">Тип элементов множества.</typeparam>
		/// <returns>Пустое множество.</returns>
		public static IReadOnlyFiniteSet<TResult> Empty<TResult> ()
		{
			return ReadOnlyList.EmptyReadOnlyList<TResult>.GetInstance ();
		}

		/// <summary>
		/// Создаёт множество целых чисел в заданном диапазоне.
		/// </summary>
		/// <param name="start">Значение первого целого числа множества.</param>
		/// <param name="count">Количество генерируемых последовательных целых чисел.</param>
		/// <returns>Множество, содержащее диапазон последовательных целых чисел.</returns>
		public static IReadOnlyFiniteSet<int> Range (int start, int count)
		{
			if ((count < 0) || (((long)start + (long)count - 1L) > (long)int.MaxValue))
			{
				throw new ArgumentOutOfRangeException ("count");
			}
			Contract.EndContractBlock ();

			if (count < 1)
			{
				return ReadOnlyList.EmptyReadOnlyList<int>.GetInstance ();
			}
			return new ReadOnlyList.RangeReadOnlyList (start, count);
		}

		/// <summary>
		/// Возвращает указанное конечное множество или одноэлементное множество,
		/// содержащее указанное значение, если указанное множество пустое.
		/// </summary>
		/// <typeparam name="TSource">Тип элементов множества.</typeparam>
		/// <param name="source">Множество, для которого возвращается указанное значение, если оно пустое.</param>
		/// <param name="defaultValue">Значение, возвращаемое в случае пустого множества.</param>
		/// <returns>Множество, содержащее значение defaultValue, если множество source пустое;
		/// в противном случае возвращается source.</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage ("Microsoft.Design",
			"CA1026:DefaultParametersShouldNotBeUsed",
			Justification = "Parameter have clear right 'default' value and there is no plausible reason why the default might need to change.")]
		public static IReadOnlyFiniteSet<TSource> DefaultIfEmpty<TSource> (this IReadOnlyFiniteSet<TSource> source, TSource defaultValue = default (TSource))
		{
			if (source == null)
			{
				throw new ArgumentNullException ("source");
			}
			Contract.EndContractBlock ();

			return (source.Count > 0) ? source : new OneItemReadOnlyFiniteSet<TSource> (defaultValue, null);
		}

		/// <summary>
		/// Определяет, содержится ли указанный элемент в указанном конечном множестве.
		/// </summary>
		/// <typeparam name="TSource">Тип элементов множества.</typeparam>
		/// <param name="source">Множество, в котором требуется найти данное значение.</param>
		/// <param name="value">Значение, которое требуется найти в множестве.</param>
		/// <param name="notUsed">Не используется.</param>
		/// <returns>True, если множество содержит элемент с указанным значением, в противном случае — False.</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage ("Microsoft.Usage",
			"CA1801:ReviewUnusedParameters",
			MessageId = "notUsed",
			Justification = "Parameters must be compatible with System.Linq.Enumerable.Contains()."),
		System.Diagnostics.CodeAnalysis.SuppressMessage ("Microsoft.Design",
			"CA1026:DefaultParametersShouldNotBeUsed",
			Justification = "Parameter have clear right 'default' value and there is no plausible reason why the default might need to change.")]
		public static bool Contains<TSource> (
			this IReadOnlyFiniteSet<TSource> source,
			TSource value,
			IEqualityComparer<TSource> notUsed = null)
		{
			if (source == null)
			{
				throw new ArgumentNullException ("source");
			}
			Contract.EndContractBlock ();

			return source.Contains (value);
		}

		/// <summary>
		/// Изменяет порядок элементов указанного конечного множества на противоположный.
		/// </summary>
		/// <typeparam name="TSource">Тип элементов множества.</typeparam>
		/// <param name="source">Множество, элементы которого следует расставить в обратном порядке.</param>
		/// <returns>Множество, элементы которого содержат те же элементы, но следуют в противоположном порядке.</returns>
		public static IReadOnlyFiniteSet<TSource> Reverse<TSource> (this IReadOnlyFiniteSet<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException ("source");
			}
			Contract.EndContractBlock ();

			if (source.Count < 2)
			{
				return source;
			}
			return new ReverseReadOnlyFiniteSet<TSource> (source);
		}

		/// <summary>
		/// Возвращает указанное множество.
		/// </summary>
		/// <typeparam name="TSource">Тип элементов множества.</typeparam>
		/// <param name="source">Конечное множество</param>
		/// <param name="notUsed">Не используется.</param>
		/// <returns>Указанное множество.</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage ("Microsoft.Usage", "CA1801:ReviewUnusedParameters",
			MessageId = "notUsed",
			Justification = "Parameters must be compatible with System.Linq.Enumerable.Distinct()."),
		System.Diagnostics.CodeAnalysis.SuppressMessage ("Microsoft.Design",
			"CA1026:DefaultParametersShouldNotBeUsed",
			Justification = "Parameter have clear right 'default' value and there is no plausible reason why the default might need to change.")]
		public static IReadOnlyFiniteSet<TSource> Distinct<TSource> (
			this IReadOnlyFiniteSet<TSource> source,
			IEqualityComparer<TSource> notUsed = null)
		{
			if (source == null)
			{
				throw new ArgumentNullException ("source");
			}
			Contract.EndContractBlock ();

			return source;
		}

		/// <summary>
		/// Получает разность (дополнение) указанных конечных множеств.
		/// </summary>
		/// <typeparam name="TSource">Тип элементов множеств.</typeparam>
		/// <param name="first">Множество, из которого будут получена разность с множеством second.</param>
		/// <param name="second">Множество, которое будет использовано для получения разности с множеством first.</param>
		/// <param name="notUsed">Не используется.</param>
		/// <returns>Множество, представляющее собой разность двух множеств.</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage ("Microsoft.Design",
			"CA1026:DefaultParametersShouldNotBeUsed",
			Justification = "Parameter have clear right 'default' value and there is no plausible reason why the default might need to change."),
		System.Diagnostics.CodeAnalysis.SuppressMessage ("Microsoft.Usage",
			"CA1801:ReviewUnusedParameters",
			MessageId = "notUsed",
			Justification = "Parameters must be compatible with System.Linq.Enumerable.Except().")]
		public static IReadOnlyFiniteSet<TSource> Except<TSource> (
			this IReadOnlyFiniteSet<TSource> first,
			IReadOnlyFiniteSet<TSource> second,
			IEqualityComparer<TSource> notUsed = null)
		{
			if (first == null)
			{
				throw new ArgumentNullException ("first");
			}
			if (second == null)
			{
				throw new ArgumentNullException ("second");
			}
			Contract.EndContractBlock ();

			if ((first.Count < 1) || (second.Count < 1))
			{
				return first;
			}
			if (first == second)
			{
				return ReadOnlyList.EmptyReadOnlyList<TSource>.GetInstance ();
			}

			// заранее считаем количество элементов
			int count = first.Count;
			if (first.Count < second.Count)
			{
				foreach (var item in first)
				{
					if (second.Contains (item))
					{
						count--;
					}
				}
			}
			else
			{
				foreach (var item in second)
				{
					if (first.Contains (item))
					{
						count--;
					}
				}
			}

			return new ExceptReadOnlyFiniteSet<TSource> (first, second, count);
		}

		/// <summary>
		/// Получает симметрическую разность указанных конечных множеств.
		/// </summary>
		/// <typeparam name="TSource">Тип элементов множеств.</typeparam>
		/// <param name="first">Первое множество для вычисления симметрической разности.</param>
		/// <param name="second">Второе множество для вычисления симметрической разности.</param>
		/// <param name="notUsed">Не используется.</param>
		/// <returns>Множество, представляющее собой симметрическую разность двух множеств.</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage ("Microsoft.Design",
			"CA1026:DefaultParametersShouldNotBeUsed",
			Justification = "Parameter have clear right 'default' value and there is no plausible reason why the default might need to change."),
		System.Diagnostics.CodeAnalysis.SuppressMessage ("Microsoft.Usage",
			"CA1801:ReviewUnusedParameters",
			MessageId = "notUsed",
			Justification = "Parameters must be compatible with System.Linq.Enumerable.Except().")]
		public static IReadOnlyFiniteSet<TSource> SymmetricExcept<TSource> (
			this IReadOnlyFiniteSet<TSource> first,
			IReadOnlyFiniteSet<TSource> second,
			IEqualityComparer<TSource> notUsed = null)
		{
			if (first == null)
			{
				throw new ArgumentNullException ("first");
			}
			if (second == null)
			{
				throw new ArgumentNullException ("second");
			}
			Contract.EndContractBlock ();

			if (first.Count < 1)
			{
				return second;
			}
			if (second.Count < 1)
			{
				return first;
			}
			if (first == second)
			{
				return ReadOnlyList.EmptyReadOnlyList<TSource>.GetInstance ();
			}

			// заранее считаем количество элементов
			int count = first.Count + second.Count;
			foreach (var item in first)
			{
				if (second.Contains (item))
				{
					count--;
				}
			}
			foreach (var item in second)
			{
				if (first.Contains (item))
				{
					count--;
				}
			}

			return new SymmetricExceptReadOnlyFiniteSet<TSource> (first, second, count);
		}

		/// <summary>
		/// Получает пересечение указанных конечных множеств.
		/// </summary>
		/// <typeparam name="TSource">Тип элементов множеств.</typeparam>
		/// <param name="first">Первое множество для вычисления пересечения.</param>
		/// <param name="second">Второе множество для вычисления пересечения.</param>
		/// <param name="notUsed">Не используется.</param>
		/// <returns>Множество, представляющее собой пересечение двух множеств.</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage ("Microsoft.Design",
			"CA1026:DefaultParametersShouldNotBeUsed",
			Justification = "Parameter have clear right 'default' value and there is no plausible reason why the default might need to change."),
		System.Diagnostics.CodeAnalysis.SuppressMessage ("Microsoft.Usage",
			"CA1801:ReviewUnusedParameters",
			MessageId = "notUsed",
			Justification = "Parameters must be compatible with System.Linq.Enumerable.Intersect().")]
		public static IReadOnlyFiniteSet<TSource> Intersect<TSource> (
			this IReadOnlyFiniteSet<TSource> first,
			IReadOnlyFiniteSet<TSource> second,
			IEqualityComparer<TSource> notUsed = null)
		{
			if (first == null)
			{
				throw new ArgumentNullException ("first");
			}
			if (second == null)
			{
				throw new ArgumentNullException ("second");
			}
			Contract.EndContractBlock ();

			if ((first.Count < 1) || (first == second))
			{
				return first;
			}
			if (second.Count < 1)
			{
				return second;
			}

			// делаем первым тот, в котором больше элементов
			if (first.Count < second.Count)
			{
				var ttt = first;
				first = second;
				second = ttt;
			}

			// заранее считаем количество элементов
			int count = 0;
			foreach (var item in second)
			{
				if (first.Contains (item))
				{
					count++;
				}
			}

			return new IntersectReadOnlyFiniteSet<TSource> (first, second, count);
		}

		/// <summary>
		/// Получает объединение указанных конечных множеств.
		/// </summary>
		/// <typeparam name="TSource">Тип элементов множеств.</typeparam>
		/// <param name="first">Первое множество для вычисления объединения.</param>
		/// <param name="second">Второе множество для вычисления объединения.</param>
		/// <param name="notUsed">Не используется.</param>
		/// <returns>Множество, представляющее собой объединение двух множеств.</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage ("Microsoft.Design",
			"CA1026:DefaultParametersShouldNotBeUsed",
			Justification = "Parameter have clear right 'default' value and there is no plausible reason why the default might need to change."),
		System.Diagnostics.CodeAnalysis.SuppressMessage ("Microsoft.Usage",
			"CA1801:ReviewUnusedParameters",
			MessageId = "notUsed",
			Justification = "Parameters must be compatible with System.Linq.Enumerable.Union().")]
		public static IReadOnlyFiniteSet<TSource> Union<TSource> (
			this IReadOnlyFiniteSet<TSource> first,
			IReadOnlyFiniteSet<TSource> second,
			IEqualityComparer<TSource> notUsed = null)
		{
			if (first == null)
			{
				throw new ArgumentNullException ("first");
			}
			if (second == null)
			{
				throw new ArgumentNullException ("second");
			}
			Contract.EndContractBlock ();

			if (first.Count < 1)
			{
				return second;
			}
			if ((second.Count < 1) || (first == second))
			{
				return first;
			}

			// делаем первым тот, в котором больше элементов
			if (first.Count < second.Count)
			{
				var ttt = first;
				first = second;
				second = ttt;
			}

			// заранее считаем количество элементов
			int count = first.Count;
			foreach (var item in second)
			{
				if (!first.Contains (item))
				{
					count++;
				}
			}

			return new UnionReadOnlyFiniteSet<TSource> (first, second, count);
		}

		#region private generated collection classes

		private class ReverseReadOnlyFiniteSet<TSource> : IReadOnlyFiniteSet<TSource>
		{
			private readonly IReadOnlyFiniteSet<TSource> _source;
			private TSource[] _buffer = null;

			public int Count { get { return _source.Count; } }

			internal ReverseReadOnlyFiniteSet (IReadOnlyFiniteSet<TSource> source)
			{
				_source = source;
			}

			public bool Contains (TSource item)
			{
				return _source.Contains (item);
			}

			IEnumerator IEnumerable.GetEnumerator () { return GetEnumerator (); }
			public IEnumerator<TSource> GetEnumerator ()
			{
				if (_buffer == null)
				{
					var buf = ReadOnlyCollection.ToArray (_source);
					Array.Reverse (buf);
					_buffer = buf;
				}
				return ((IEnumerable<TSource>)_buffer).GetEnumerator ();
			}
		}

		private class OneItemReadOnlyFiniteSet<T> : IReadOnlyFiniteSet<T>
		{
			private readonly T[] _item = new T[1];
			private readonly IEqualityComparer<T> _comparer;

			public int Count { get { return 1; } }

			internal OneItemReadOnlyFiniteSet (T item, IEqualityComparer<T> comparer)
			{
				_item[0] = item;
				_comparer = comparer ?? EqualityComparer<T>.Default;
			}

			public bool Contains (T item)
			{
				return _comparer.Equals (_item[0], item);
			}

			IEnumerator IEnumerable.GetEnumerator () { return GetEnumerator (); }
			public IEnumerator<T> GetEnumerator ()
			{
				return ((IEnumerable<T>)_item).GetEnumerator ();
			}
		}

		private class ExceptReadOnlyFiniteSet<TSource> : IReadOnlyFiniteSet<TSource>
		{
			private readonly IReadOnlyFiniteSet<TSource> _first;
			private readonly IReadOnlyFiniteSet<TSource> _second;
			private readonly int _count;

			internal ExceptReadOnlyFiniteSet (
				IReadOnlyFiniteSet<TSource> first,
				IReadOnlyFiniteSet<TSource> second,
				int count)
			{
				_first = first;
				_second = second;
				_count = count;
			}

			public bool Contains (TSource item)
			{
				return (_first.Contains (item) && !_second.Contains (item));
			}

			public int Count { get { return _count; } }

			IEnumerator IEnumerable.GetEnumerator () { return GetEnumerator (); }
			public IEnumerator<TSource> GetEnumerator ()
			{
				foreach (var item in _first)
				{
					if (!_second.Contains (item))
					{
						yield return item;
					}
				}
			}
		}

		private class SymmetricExceptReadOnlyFiniteSet<TSource> : IReadOnlyFiniteSet<TSource>
		{
			private readonly IReadOnlyFiniteSet<TSource> _first;
			private readonly IReadOnlyFiniteSet<TSource> _second;
			private readonly int _count;

			internal SymmetricExceptReadOnlyFiniteSet (
				IReadOnlyFiniteSet<TSource> first,
				IReadOnlyFiniteSet<TSource> second,
				int count)
			{
				_first = first;
				_second = second;
				_count = count;
			}

			public bool Contains (TSource item)
			{
				return (_first.Contains (item) ^ _second.Contains (item));
			}

			public int Count { get { return _count; } }

			IEnumerator IEnumerable.GetEnumerator () { return GetEnumerator (); }
			public IEnumerator<TSource> GetEnumerator ()
			{
				foreach (var item in _first)
				{
					if (!_second.Contains (item))
					{
						yield return item;
					}
				}
				foreach (var item in _second)
				{
					if (!_first.Contains (item))
					{
						yield return item;
					}
				}
			}
		}

		private class IntersectReadOnlyFiniteSet<TSource> : IReadOnlyFiniteSet<TSource>
		{
			private readonly IReadOnlyFiniteSet<TSource> _first;
			private readonly IReadOnlyFiniteSet<TSource> _second;
			private readonly int _count;

			internal IntersectReadOnlyFiniteSet (
				IReadOnlyFiniteSet<TSource> first,
				IReadOnlyFiniteSet<TSource> second,
				int count)
			{
				_first = first;
				_second = second;
				_count = count;
			}

			public bool Contains (TSource item)
			{
				return (_first.Contains (item) && _second.Contains (item));
			}

			public int Count { get { return _count; } }

			IEnumerator IEnumerable.GetEnumerator () { return GetEnumerator (); }
			public IEnumerator<TSource> GetEnumerator ()
			{
				foreach (var item in _second)
				{
					if (_first.Contains (item))
					{
						yield return item;
					}
				}
			}
		}

		private class UnionReadOnlyFiniteSet<TSource> : IReadOnlyFiniteSet<TSource>
		{
			private readonly IReadOnlyFiniteSet<TSource> _first;
			private readonly IReadOnlyFiniteSet<TSource> _second;
			private readonly int _count;

			internal UnionReadOnlyFiniteSet (
				IReadOnlyFiniteSet<TSource> first,
				IReadOnlyFiniteSet<TSource> second,
				int count)
			{
				_first = first;
				_second = second;
				_count = count;
			}

			public bool Contains (TSource item)
			{
				return (_first.Contains (item) || _second.Contains (item));
			}

			public int Count { get { return _count; } }

			IEnumerator IEnumerable.GetEnumerator () { return GetEnumerator (); }
			public IEnumerator<TSource> GetEnumerator ()
			{
				foreach (var item in _first)
				{
					yield return item;
				}
				foreach (var item in _second)
				{
					if (!_first.Contains (item))
					{
						yield return item;
					}
				}
			}
		}

		#endregion
	}
}
