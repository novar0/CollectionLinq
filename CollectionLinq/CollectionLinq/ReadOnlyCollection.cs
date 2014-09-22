using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace BusinessClassLibrary.Collections.Linq
{
	/// <summary>
	/// Методы расширения к коллекциям.
	/// </summary>
	[System.Diagnostics.CodeAnalysis.SuppressMessage ("Microsoft.Naming",
		"CA1711:IdentifiersShouldNotHaveIncorrectSuffix",
		Justification = "Analogous to System.Linq.Enumerable.")]
	public static class ReadOnlyCollection
	{
		/// <summary>
		/// Проверяет, содержит ли коллекция какие-либо элементы.
		/// </summary>
		/// <typeparam name="TSource">Тип элементов коллекции.</typeparam>
		/// <param name="source">Коллекция, проверяемая на наличие элементов.</param>
		/// <returns>True, если исходная коллекция содержит какие-либо элементы, в противном случае — False.</returns>
		public static bool Any<TSource> (this IReadOnlyCollection<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException ("source");
			}
			Contract.EndContractBlock ();

			return (source.Count > 0);
		}

		/// <summary>
		/// Возвращает количество элементов в коллекции.
		/// </summary>
		/// <typeparam name="TSource">Тип элементов коллекции.</typeparam>
		/// <param name="source">Коллекция, элементы которой требуется подсчитать.</param>
		/// <returns>Число элементов в коллекции.</returns>
		public static int Count<TSource> (this IReadOnlyCollection<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException ("source");
			}
			Contract.EndContractBlock ();

			return source.Count;
		}

		/// <summary>
		/// Возвращает значение типа Int64, представляющее общее число элементов в коллекции.
		/// </summary>
		/// <typeparam name="TSource">Тип элементов коллекции.</typeparam>
		/// <param name="source">Коллекция, элементы которой требуется подсчитать.</param>
		/// <returns>Число элементов в коллекции.</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage ("Microsoft.Naming",
			"CA1720:IdentifiersShouldNotContainTypeNames",
			MessageId = "long",
			Justification = "Analogous to System.Linq.Enumerable.LongCount().")]
		public static long LongCount<TSource> (this IReadOnlyCollection<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException ("source");
			}
			Contract.EndContractBlock ();

			return source.Count;
		}

		/// <summary>
		/// Возвращает элементы указанной коллекции или одноэлементную коллекцию,
		/// содержащую указанное значение, если коллекция пуста.
		/// </summary>
		/// <typeparam name="TSource">Тип элементов коллекции.</typeparam>
		/// <param name="source">Коллекция, для которой возвращается указанное значение, если она пуста.</param>
		/// <param name="defaultValue">Значение, возвращаемое в случае пустой коллекции.</param>
		/// <returns>Коллекция, содержащая значение defaultValue, если коллекция source пуста;
		/// в противном случае возвращается source.</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage ("Microsoft.Design",
			"CA1026:DefaultParametersShouldNotBeUsed",
			Justification = "Parameter have clear right 'default' value and there is no plausible reason why the default might need to change.")]
		public static IReadOnlyCollection<TSource> DefaultIfEmpty<TSource> (this IReadOnlyCollection<TSource> source, TSource defaultValue = default (TSource))
		{
			if (source == null)
			{
				throw new ArgumentNullException ("source");
			}
			Contract.EndContractBlock ();

			return (source.Count > 0) ? source : new TSource[] { defaultValue };
		}

		/// <summary>
		/// Пропускает заданное число элементов коллекции и возвращает коллекцию из остальных элементов.
		/// </summary>
		/// <typeparam name="TSource">Тип элементов коллекции.</typeparam>
		/// <param name="source">Коллекция, из которой требуется возвратить элементы.</param>
		/// <param name="count">Число элементов, пропускаемых перед возвращением остальных элементов.</param>
		/// <returns>Коллекция, содержащая элементы из входной коллекции, начиная с указанной позиции.</returns>
		public static IReadOnlyCollection<TSource> Skip<TSource> (this IReadOnlyCollection<TSource> source, int count)
		{
			if (source == null)
			{
				throw new ArgumentNullException ("source");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException ("count");
			}
			Contract.EndContractBlock ();

			if (source.Count < 1)
			{
				return source;
			}
			if (count >= source.Count)
			{
				return ReadOnlyList.Empty<TSource> ();
			}
			return new SkipReadOnlyCollection<TSource> (source, count);
		}

		/// <summary>
		/// Возвращает указанное число подряд идущих элементов с начала коллекции.
		/// </summary>
		/// <typeparam name="TSource">Тип элементов коллекции.</typeparam>
		/// <param name="source">Коллекция, из которой требуется возвратить элементы.</param>
		/// <param name="count">Число возвращаемых элементов.</param>
		/// <returns>Коллекция, содержащая заданное число элементов с начала входной коллекции.</returns>
		public static IReadOnlyCollection<TSource> Take<TSource> (this IReadOnlyCollection<TSource> source, int count)
		{
			if (source == null)
			{
				throw new ArgumentNullException ("source");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException ("count");
			}
			Contract.EndContractBlock ();

			if ((source.Count < 1) || (count < 1))
			{
				return ReadOnlyList.Empty<TSource> ();
			}
			if (count >= source.Count)
			{
				return source;
			}
			return new TakeReadOnlyCollection<TSource> (source, count);
		}

		/// <summary>
		/// Проецирует каждый элемент коллекции в новую форму.
		/// </summary>
		/// <typeparam name="TSource">Тип элементов коллекции source.</typeparam>
		/// <typeparam name="TResult">Тип значения, возвращаемого функцией selector.</typeparam>
		/// <param name="source">Коллекция значений, для которых вызывается функция преобразования.</param>
		/// <param name="selector">Функция преобразования, применяемая к каждому элементу.</param>
		/// <returns>Коллекция, элементы которого получены в результате вызова функции преобразования
		/// для каждого элемента коллекции source.</returns>
		public static IReadOnlyCollection<TResult> Select<TSource, TResult> (this IReadOnlyCollection<TSource> source, Func<TSource, TResult> selector)
		{
			if (source == null)
			{
				throw new ArgumentNullException ("source");
			}
			if (selector == null)
			{
				throw new ArgumentNullException ("selector");
			}
			Contract.EndContractBlock ();

			return (source.Count < 1) ?
				(IReadOnlyCollection<TResult>)ReadOnlyList.Empty<TSource> () :
				new SelectReadOnlyCollection<TSource, TResult> (source, selector);
		}

		/// <summary>
		/// Проецирует каждый элемент коллекции в новую форму, добавляя индекс элемента.
		/// </summary>
		/// <typeparam name="TSource">Тип элементов коллекции source.</typeparam>
		/// <typeparam name="TResult">Тип значения, возвращаемого функцией selector.</typeparam>
		/// <param name="source">Коллекция значений, для которых вызывается функция преобразования.</param>
		/// <param name="selector">Функция преобразования, применяемая к каждому исходному элементу;
		/// второй параметр функции представляет индекс исходного элемента.</param>
		/// <returns>Коллекция, элементы которого получены в результате вызова функции преобразования
		/// для каждого элемента коллекции source.</returns>
		public static IReadOnlyCollection<TResult> Select<TSource, TResult> (this IReadOnlyCollection<TSource> source, Func<TSource, int, TResult> selector)
		{
			if (source == null)
			{
				throw new ArgumentNullException ("source");
			}
			if (selector == null)
			{
				throw new ArgumentNullException ("selector");
			}
			Contract.EndContractBlock ();

			return (source.Count < 1) ?
				(IReadOnlyCollection<TResult>)ReadOnlyList.Empty<TSource> () :
				new SelectIndexReadOnlyCollection<TSource, TResult> (source, selector);
		}

		/// <summary>
		/// Изменяет порядок элементов коллекции на противоположный.
		/// </summary>
		/// <typeparam name="TSource">Тип элементов коллекции.</typeparam>
		/// <param name="source">Коллекция значений, которые следует расставить в обратном порядке.</param>
		/// <returns>Коллекция, элементы которой соответствуют элементам входной коллекции,
		/// но следуют в противоположном порядке.</returns>
		public static IReadOnlyCollection<TSource> Reverse<TSource> (this IReadOnlyCollection<TSource> source)
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
			return new ReverseReadOnlyCollection<TSource> (source);
		}

		/// <summary>
		/// Сортирует элементы коллекции в порядке возрастания с использованием указанного компаратора.
		/// </summary>
		/// <typeparam name="TSource">Тип элементов коллекции.</typeparam>
		/// <typeparam name="TKey">Тип ключа, возвращаемого функцией keySelector.</typeparam>
		/// <param name="source">Коллекция значений, которые следует упорядочить.</param>
		/// <param name="keySelector">Функция, извлекающая ключ из элемента.</param>
		/// <param name="comparer">Компаратор, используемый для сравнения ключей.</param>
		/// <returns>Коллекция, элементы которого отсортированы по ключу.</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage ("Microsoft.Design",
			"CA1026:DefaultParametersShouldNotBeUsed",
			Justification = "Parameter have clear right 'default' value and there is no plausible reason why the default might need to change.")]
		public static IOrderedReadOnlyCollection<TSource> OrderBy<TSource, TKey> (
			this IReadOnlyCollection<TSource> source,
			Func<TSource, TKey> keySelector,
			IComparer<TKey> comparer = null)
		{
			if (source == null)
			{
				throw new ArgumentNullException ("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException ("keySelector");
			}
			Contract.EndContractBlock ();

			return new OrderedReadOnlyCollection<TSource, TKey> (source, keySelector, comparer, false, null);
		}

		/// <summary>
		/// Сортирует элементы коллекции в порядке убывания с использованием указанного компаратора.
		/// </summary>
		/// <typeparam name="TSource">Тип элементов коллекции.</typeparam>
		/// <typeparam name="TKey">Тип ключа, возвращаемого функцией keySelector.</typeparam>
		/// <param name="source">Коллекция значений, которые следует упорядочить.</param>
		/// <param name="keySelector">Функция, извлекающая ключ из элемента.</param>
		/// <param name="comparer">Компаратор, используемый для сравнения ключей.</param>
		/// <returns>Коллекция, элементы которого отсортированы по ключу в порядке убывания.</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage ("Microsoft.Design",
			"CA1026:DefaultParametersShouldNotBeUsed",
			Justification = "Parameter have clear right 'default' value and there is no plausible reason why the default might need to change.")]
		public static IOrderedReadOnlyCollection<TSource> OrderByDescending<TSource, TKey> (
			this IReadOnlyCollection<TSource> source,
			Func<TSource, TKey> keySelector,
			IComparer<TKey> comparer = null)
		{
			if (source == null)
			{
				throw new ArgumentNullException ("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException ("keySelector");
			}
			Contract.EndContractBlock ();

			return new OrderedReadOnlyCollection<TSource, TKey> (source, keySelector, comparer, true, null);
		}

		/// <summary>
		/// Выполняет дополнительное упорядочение элементов коллекции в порядке возрастания с использованием указанного компаратора.
		/// </summary>
		/// <typeparam name="TSource">Тип элементов коллекции.</typeparam>
		/// <typeparam name="TKey">Тип ключа, возвращаемого функцией keySelector.</typeparam>
		/// <param name="source">Коллекция, содержащая сортируемые элементы.</param>
		/// <param name="keySelector">Функция, извлекающая ключ из каждого элемента.</param>
		/// <param name="comparer">Компаратор, используемый для сравнения ключей.</param>
		/// <returns>Коллекция, элементы которой отсортированы по ключу.</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage ("Microsoft.Design",
			"CA1026:DefaultParametersShouldNotBeUsed",
			Justification = "Parameter have clear right 'default' value and there is no plausible reason why the default might need to change.")]
		public static IOrderedReadOnlyCollection<TSource> ThenBy<TSource, TKey> (
			this IOrderedReadOnlyCollection<TSource> source,
			Func<TSource, TKey> keySelector,
			IComparer<TKey> comparer = null)
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
			return source.CreateOrderedReadOnlyCollection<TKey> (keySelector, comparer, false);
		}

		/// <summary>
		/// Выполняет дополнительное упорядочение элементов коллекции в порядке убывания с использованием указанного компаратора.
		/// </summary>
		/// <typeparam name="TSource">Тип элементов коллекции.</typeparam>
		/// <typeparam name="TKey">Тип ключа, возвращаемого функцией keySelector.</typeparam>
		/// <param name="source">Коллекция, содержащая сортируемые элементы.</param>
		/// <param name="keySelector">Функция, извлекающая ключ из каждого элемента.</param>
		/// <param name="comparer">Компаратор, используемый для сравнения ключей.</param>
		/// <returns>Коллекция, элементы которой отсортированы по ключу в порядке убывания.</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage ("Microsoft.Design",
			"CA1026:DefaultParametersShouldNotBeUsed",
			Justification = "Parameter have clear right 'default' value and there is no plausible reason why the default might need to change.")]
		public static IOrderedReadOnlyCollection<TSource> ThenByDescending<TSource, TKey> (
			this IOrderedReadOnlyCollection<TSource> source,
			Func<TSource, TKey> keySelector,
			IComparer<TKey> comparer = null)
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
			return source.CreateOrderedReadOnlyCollection<TKey> (keySelector, comparer, true);
		}

		/// <summary>
		/// Объединяет две коллекции.
		/// </summary>
		/// <typeparam name="TSource">Тип элементов коллекций.</typeparam>
		/// <param name="first">Первая из объединяемых коллекций.</param>
		/// <param name="second">Коллекция, объединяемая с первой коллекций.</param>
		/// <returns>Коллекция, содержащая элементы двух входных коллекций.</returns>
		public static IReadOnlyCollection<TSource> Concat<TSource> (
			this IReadOnlyCollection<TSource> first,
			IReadOnlyCollection<TSource> second)
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
			return new ConcatReadOnlyCollection<TSource> (first, second);
		}

		/// <summary>
		/// Объединяет две коллекции, используя указанную функцию предиката.
		/// </summary>
		/// <typeparam name="TFirst">Тип элементов первой входной коллекции.</typeparam>
		/// <typeparam name="TSecond">Тип элементов второй входной коллекции.</typeparam>
		/// <typeparam name="TResult">Тип элементов результирующей коллекции.</typeparam>
		/// <param name="first">Первая коллекция для объединения.</param>
		/// <param name="second">Вторая коллекция для объединения.</param>
		/// <param name="selector">Функция, которая определяет, как объединить элементы двух коллекций.</param>
		/// <returns>Коллекция, содержащий объединенные элементы двух входных коллекций.</returns>
		public static IReadOnlyCollection<TResult> Zip<TFirst, TSecond, TResult> (
			this IReadOnlyCollection<TFirst> first,
			IReadOnlyCollection<TSecond> second,
			Func<TFirst, TSecond, TResult> selector)
		{
			if (first == null)
			{
				throw new ArgumentNullException ("first");
			}
			if (second == null)
			{
				throw new ArgumentNullException ("second");
			}
			if (selector == null)
			{
				throw new ArgumentNullException ("selector");
			}
			Contract.EndContractBlock ();

			return ((first.Count < 1) || (second.Count < 1)) ?
				(IReadOnlyCollection<TResult>)ReadOnlyList.Empty<TResult> () :
				new ZipReadOnlyCollection<TFirst, TSecond, TResult> (first, second, selector);
		}

		/// <summary>
		/// Создаёт массив, содержащий все элементы указанной коллекции.
		/// </summary>
		/// <typeparam name="TSource">Тип элементов коллекции.</typeparam>
		/// <param name="source">Коллекция, на основе которой создаётся массив.</param>
		/// <returns>Массив, содержащий элементы из входной коллекции.</returns>
		public static TSource[] ToArray<TSource> (this IReadOnlyCollection<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException ("first");
			}
			Contract.EndContractBlock ();

			var array = new TSource[source.Count];
			if (source.Count > 0)
			{
				int idx = 0;
				foreach (var item in source)
				{
					array[idx++] = item;
				}
			}
			return array;
		}

		#region private generated collection classes

		private class SkipReadOnlyCollection<TSource> : IReadOnlyCollection<TSource>
		{
			private readonly IReadOnlyCollection<TSource> _source;
			private readonly int _skipCount;

			internal SkipReadOnlyCollection (IReadOnlyCollection<TSource> source, int skipCount)
			{
				_source = source;
				_skipCount = skipCount;
			}

			public int Count { get { return (_source.Count - _skipCount); } }

			IEnumerator IEnumerable.GetEnumerator () { return GetEnumerator (); }

			public IEnumerator<TSource> GetEnumerator ()
			{
				using (var enumerator = _source.GetEnumerator ())
				{
					var count = _skipCount;
					while ((count > 0) && enumerator.MoveNext ())
					{
						--count;
					}
					if (count <= 0)
					{
						while (enumerator.MoveNext ())
						{
							yield return enumerator.Current;
						}
					}
				}
			}
		}

		private class TakeReadOnlyCollection<TSource> : IReadOnlyCollection<TSource>
		{
			private readonly IReadOnlyCollection<TSource> _source;
			private readonly int _count;

			internal TakeReadOnlyCollection (IReadOnlyCollection<TSource> source, int count)
			{
				_source = source;
				_count = count;
			}

			public int Count { get { return _count; } }

			IEnumerator IEnumerable.GetEnumerator () { return GetEnumerator (); }

			public IEnumerator<TSource> GetEnumerator ()
			{
				var count = _count;
				foreach (var item in _source)
				{
					yield return item;
					if (--count == 0)
					{
						break;
					}
				}
			}
		}

		private class SelectReadOnlyCollection<TSource, TResult> : IReadOnlyCollection<TResult>
		{
			private IReadOnlyCollection<TSource> _source;
			private Func<TSource, TResult> _selector;

			internal SelectReadOnlyCollection (IReadOnlyCollection<TSource> source, Func<TSource, TResult> selector)
			{
				_source = source;
				_selector = selector;
			}

			public int Count { get { return _source.Count; } }

			IEnumerator IEnumerable.GetEnumerator () { return GetEnumerator (); }

			public IEnumerator<TResult> GetEnumerator ()
			{
				foreach (var item in _source)
				{
					yield return _selector.Invoke (item);
				}
			}
		}

		private class SelectIndexReadOnlyCollection<TSource, TResult> : IReadOnlyCollection<TResult>
		{
			private IReadOnlyCollection<TSource> _source;
			private Func<TSource, int, TResult> _selector;

			internal SelectIndexReadOnlyCollection (IReadOnlyCollection<TSource> source, Func<TSource, int, TResult> selector)
			{
				_source = source;
				_selector = selector;
			}

			public int Count { get { return _source.Count; } }

			IEnumerator IEnumerable.GetEnumerator () { return GetEnumerator (); }

			public IEnumerator<TResult> GetEnumerator ()
			{
				int idx = 0;
				foreach (var item in _source)
				{
					yield return _selector.Invoke (item, idx++);
				}
			}
		}

		private class ReverseReadOnlyCollection<TSource> : IReadOnlyCollection<TSource>
		{
			private readonly IReadOnlyCollection<TSource> _source;
			private TSource[] _buffer = null;

			internal ReverseReadOnlyCollection (IReadOnlyCollection<TSource> source)
			{
				_source = source;
			}

			public int Count { get { return _source.Count; } }

			IEnumerator IEnumerable.GetEnumerator () { return GetEnumerator (); }

			public IEnumerator<TSource> GetEnumerator ()
			{
				if (_buffer == null)
				{
					var buf = ToArray (_source);
					Array.Reverse (buf);
					_buffer = buf;
				}
				return ((IEnumerable<TSource>)_buffer).GetEnumerator ();
			}
		}

		private class ConcatReadOnlyCollection<TSource> : IReadOnlyCollection<TSource>
		{
			private readonly IReadOnlyCollection<TSource> _first;
			private readonly IReadOnlyCollection<TSource> _second;

			internal ConcatReadOnlyCollection (IReadOnlyCollection<TSource> first, IReadOnlyCollection<TSource> second)
			{
				_first = first;
				_second = second;
			}

			public int Count { get { return (_first.Count + _second.Count); } }

			IEnumerator IEnumerable.GetEnumerator () { return GetEnumerator (); }

			public IEnumerator<TSource> GetEnumerator ()
			{
				foreach (var item in _first)
				{
					yield return item;
				}
				foreach (var item in _second)
				{
					yield return item;
				}
			}
		}

		private class ZipReadOnlyCollection<TFirst, TSecond, TResult> : IReadOnlyCollection<TResult>
		{
			private readonly IReadOnlyCollection<TFirst> _first;
			private readonly IReadOnlyCollection<TSecond> _second;
			private readonly Func<TFirst, TSecond, TResult> _selector;

			internal ZipReadOnlyCollection (IReadOnlyCollection<TFirst> first, IReadOnlyCollection<TSecond> second, Func<TFirst, TSecond, TResult> selector)
			{
				_first = first;
				_second = second;
				_selector = selector;
			}

			public int Count { get { return Math.Min (_first.Count, _second.Count); } }

			IEnumerator IEnumerable.GetEnumerator () { return GetEnumerator (); }

			public IEnumerator<TResult> GetEnumerator ()
			{
				using (var enumerator1 = _first.GetEnumerator ())
				{
					using (var enumerator2 = _second.GetEnumerator ())
					{
						while (enumerator1.MoveNext () && enumerator2.MoveNext ())
						{
							yield return _selector.Invoke (enumerator1.Current, enumerator2.Current);
						}
					}
				}
			}
		}

		private abstract class OrderedReadOnlyCollection<TElement> : IOrderedReadOnlyCollection<TElement>
		{
			protected readonly IReadOnlyCollection<TElement> _source;

			protected OrderedReadOnlyCollection (IReadOnlyCollection<TElement> source)
			{
				_source = source;
			}

			public int Count { get { return _source.Count; } }

			IEnumerator IEnumerable.GetEnumerator () { return GetEnumerator (); }

			public IEnumerator<TElement> GetEnumerator ()
			{
				if (_source.Count > 0)
				{
					var buffer = (_source as IReadOnlyList<TElement>) ?? ToArray (_source);
					var sorter = CreateSorter (null);
					var map = sorter.CreateIndex ();
					for (int i = 0; i < buffer.Count; ++i)
					{
						yield return buffer[map[i]];
					}
				}
			}

			internal abstract CollectionSorter<TElement> CreateSorter (CollectionSorter<TElement> next);

			System.Linq.IOrderedEnumerable<TElement> System.Linq.IOrderedEnumerable<TElement>.CreateOrderedEnumerable<TKey> (Func<TElement, TKey> keySelector, IComparer<TKey> comparer, bool descending)
			{
				return new OrderedReadOnlyCollection<TElement, TKey> (_source, keySelector, comparer, descending, this);
			}

			IOrderedReadOnlyCollection<TElement> IOrderedReadOnlyCollection<TElement>.CreateOrderedReadOnlyCollection<TKey> (Func<TElement, TKey> keySelector, IComparer<TKey> comparer, bool descending)
			{
				return new OrderedReadOnlyCollection<TElement, TKey> (_source, keySelector, comparer, descending, this);
			}
		}

		private class OrderedReadOnlyCollection<TElement, TKey> : OrderedReadOnlyCollection<TElement>
		{
			private readonly OrderedReadOnlyCollection<TElement> _parent = null;
			private readonly Func<TElement, TKey> _keySelector;
			private readonly IComparer<TKey> _comparer;
			private readonly bool _descending;

			internal OrderedReadOnlyCollection (IReadOnlyCollection<TElement> source, Func<TElement, TKey> keySelector, IComparer<TKey> comparer, bool descending, OrderedReadOnlyCollection<TElement> parent)
				: base (source)
			{
				if (source == null)
				{
					throw new ArgumentNullException ("source");
				}
				if (keySelector == null)
				{
					throw new ArgumentNullException ("keySelector");
				}

				_keySelector = keySelector;
				_comparer = comparer ?? Comparer<TKey>.Default;
				_descending = descending;
				_parent = parent;
			}

			internal override CollectionSorter<TElement> CreateSorter (CollectionSorter<TElement> next)
			{
				CollectionSorter<TElement> sorter = new CollectionSorter<TElement, TKey> (_source, _keySelector, _comparer, _descending, next);
				if (_parent != null)
				{
					sorter = _parent.CreateSorter (sorter);
				}
				return sorter;
			}
		}

		#endregion
	}
}
