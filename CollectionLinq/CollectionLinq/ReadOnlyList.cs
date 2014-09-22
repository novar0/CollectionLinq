using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace BusinessClassLibrary.Collections.Linq
{
	/// <summary>
	/// Методы расширения к спискам.
	/// </summary>
	[System.Diagnostics.CodeAnalysis.SuppressMessage ("Microsoft.Naming",
		"CA1711:IdentifiersShouldNotHaveIncorrectSuffix",
		Justification = "Analogous to System.Linq.Enumerable.")]
	public static class ReadOnlyList
	{
		/// <summary>
		/// Создаёт пустой список.
		/// </summary>
		/// <typeparam name="TResult">Тип элементов списка.</typeparam>
		/// <returns>Пустой список.</returns>
		public static IReadOnlyList<TResult> Empty<TResult> ()
		{
			return EmptyReadOnlyList<TResult>.GetInstance ();
		}

		/// <summary>
		/// Создаёт список целых чисел в заданном диапазоне.
		/// </summary>
		/// <param name="start">Значение первого целого числа списка.</param>
		/// <param name="count">Количество генерируемых последовательных целых чисел.</param>
		/// <returns>Список, содержащий диапазон последовательных целых чисел.</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage ("Microsoft.Usage",
			"CA2233:OperationsShouldNotOverflow",
			MessageId = "start+1",
			Justification = "Overflow checked."),
		System.Diagnostics.CodeAnalysis.SuppressMessage ("Microsoft.Usage",
			"CA2233:OperationsShouldNotOverflow",
			MessageId = "start+2",
			Justification = "Overflow checked.")]
		public static IReadOnlyList<int> Range (int start, int count)
		{
			if ((count < 0) || (((long)start + (long)count - 1L) > (long)int.MaxValue))
			{
				throw new ArgumentOutOfRangeException ("count");
			}
			Contract.EndContractBlock ();

			switch (count)
			{
				case 0:
					return EmptyReadOnlyList<int>.GetInstance ();
				case 1:
					return new int[] { start };
				case 2:
					return new int[] { start, start + 1 };
				case 3:
					return new int[] { start, start + 1, start + 2 };
				default:
					return new RangeReadOnlyList (start, count);
			}
		}

		/// <summary>
		/// Генерирует список, содержащий повторяющееся значение.
		/// </summary>
		/// <typeparam name="TResult">Тип значения, которое будет повторяться в результирующем списке.</typeparam>
		/// <param name="element">Повторяемое значение.</param>
		/// <param name="count">Требуемое число повторений значения в создаваемом списке.</param>
		/// <returns>Список, содержащий повторяющееся значение.</returns>
		public static IReadOnlyList<TResult> Repeat<TResult> (TResult element, int count)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException ("count");
			}
			Contract.EndContractBlock ();

			switch (count)
			{
				case 0:
					return EmptyReadOnlyList<TResult>.GetInstance ();
				case 1:
					return new TResult[] { element };
				case 2:
					return new TResult[] { element, element };
				case 3:
					return new TResult[] { element, element, element };
				default:
					return new RepeatReadOnlyList<TResult> (element, count);
			}
		}

		/// <summary>
		/// Возвращает первый элемент списка.
		/// </summary>
		/// <typeparam name="TSource">Тип элементов списка.</typeparam>
		/// <param name="source">Список, первый элемент которого требуется возвратить.</param>
		/// <returns>Первый элемент указанного списка.</returns>
		public static TSource First<TSource> (this IReadOnlyList<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException ("source");
			}
			Contract.EndContractBlock ();

			if (source.Count < 1)
			{
				throw new InvalidOperationException ("No elements in collection");
			}

			return source[0];
		}

		/// <summary>
		/// Возвращает первый элемент списка или значение по умолчанию, если список не содержит элементов.
		/// </summary>
		/// <typeparam name="TSource">Тип элементов списка.</typeparam>
		/// <param name="source">Список, первый элемент которого требуется возвратить.</param>
		/// <returns>Значение по умолчанию, если список пуст, в противном случае — первый элемент списка.</returns>
		public static TSource FirstOrDefault<TSource> (this IReadOnlyList<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException ("source");
			}
			Contract.EndContractBlock ();

			return (source.Count > 0) ? source[0] : default (TSource);
		}

		/// <summary>
		/// Возвращает последний элемент списка.
		/// </summary>
		/// <typeparam name="TSource">Тип элементов списка.</typeparam>
		/// <param name="source">Список, последний элемент которого требуется возвратить.</param>
		/// <returns>Последний элемент указанного списка.</returns>
		public static TSource Last<TSource> (this IReadOnlyList<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException ("source");
			}
			Contract.EndContractBlock ();

			if (source.Count < 1)
			{
				throw new InvalidOperationException ("No elements in collection");
			}

			return source[source.Count - 1];
		}

		/// <summary>
		/// Возвращает последний элемент списка или значение по умолчанию, если список не содержит элементов.
		/// </summary>
		/// <typeparam name="TSource">Тип элементов списка.</typeparam>
		/// <param name="source">Список, последний элемент которого требуется возвратить.</param>
		/// <returns>Значение по умолчанию, если список пуст, в противном случае — последний элемент списка.</returns>
		public static TSource LastOrDefault<TSource> (this IReadOnlyList<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException ("source");
			}
			Contract.EndContractBlock ();

			return (source.Count > 0) ? source[source.Count - 1] : default (TSource);
		}

		/// <summary>
		/// Возвращает элемент по указанному индексу в списке.
		/// </summary>
		/// <typeparam name="TSource">Тип элементов списка.</typeparam>
		/// <param name="source">Список, из которого требуется возвратить элемент.</param>
		/// <param name="index">Отсчитываемый от нуля индекс элемента, который требуется извлечь.</param>
		/// <returns>Элемент, находящийся в указанной позиции в списке.</returns>
		public static TSource ElementAt<TSource> (this IReadOnlyList<TSource> source, int index)
		{
			if (source == null)
			{
				throw new ArgumentNullException ("source");
			}
			if ((index < 0) || (index >= source.Count))
			{
				throw new ArgumentOutOfRangeException ("index");
			}
			Contract.EndContractBlock ();

			return source[index];
		}

		/// <summary>
		/// Возвращает элемент по указанному индексу в списке или
		/// значение по умолчанию, если индекс вне допустимого диапазона.
		/// </summary>
		/// <typeparam name="TSource">Тип элементов списка.</typeparam>
		/// <param name="source">Список, из которого требуется возвратить элемент.</param>
		/// <param name="index">Отсчитываемый от нуля индекс элемента, который требуется извлечь.</param>
		/// <returns>Значение по умолчанию, если индекс указывает позицию вне списка,
		/// в противном случае — элемент, находящийся в указанной позиции в списке.</returns>
		public static TSource ElementAtOrDefault<TSource> (this IReadOnlyList<TSource> source, int index)
		{
			if (source == null)
			{
				throw new ArgumentNullException ("source");
			}
			Contract.EndContractBlock ();

			if ((index < 0) || (index >= source.Count))
			{
				return default (TSource);
			}
			return source[index];
		}

		/// <summary>
		/// Возвращает элементы указанного списка или одноэлементный список,
		/// содержащий указанное значение, если список пуст.
		/// </summary>
		/// <typeparam name="TSource">Тип элементов списка.</typeparam>
		/// <param name="source">Список, для которого возвращается указанное значение, если он пуст.</param>
		/// <param name="defaultValue">Значение, возвращаемое в случае пустого списка.</param>
		/// <returns>Список, содержащий значение defaultValue, если список source пуст;
		/// в противном случае возвращается source.</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage ("Microsoft.Design",
			"CA1026:DefaultParametersShouldNotBeUsed",
			Justification = "Parameter have clear right 'default' value and there is no plausible reason why the default might need to change.")]
		public static IReadOnlyList<TSource> DefaultIfEmpty<TSource> (this IReadOnlyList<TSource> source, TSource defaultValue = default (TSource))
		{
			if (source == null)
			{
				throw new ArgumentNullException ("source");
			}
			Contract.EndContractBlock ();

			return (source.Count > 0) ? source : new TSource[] { defaultValue };
		}

		/// <summary>
		/// Пропускает заданное число элементов списка и возвращает список из остальных элементов.
		/// </summary>
		/// <typeparam name="TSource">Тип элементов списка.</typeparam>
		/// <param name="source">Список, из которого требуется возвратить элементы.</param>
		/// <param name="count">Число элементов, пропускаемых перед возвращением остальных элементов.</param>
		/// <returns>Список, содержащий элементы из входного списка, начиная с указанной позиции.</returns>
		public static IReadOnlyList<TSource> Skip<TSource> (this IReadOnlyList<TSource> source, int count)
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
				return EmptyReadOnlyList<TSource>.GetInstance ();
			}
			return new SkipReadOnlyList<TSource> (source, count);
		}

		/// <summary>
		/// Возвращает указанное число подряд идущих элементов с начала списка.
		/// </summary>
		/// <typeparam name="TSource">Тип элементов списка.</typeparam>
		/// <param name="source">Список, из которого требуется возвратить элементы.</param>
		/// <param name="count">Число возвращаемых элементов.</param>
		/// <returns>Список, содержащий заданное число элементов с начала входного списка.</returns>
		public static IReadOnlyList<TSource> Take<TSource> (this IReadOnlyList<TSource> source, int count)
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
				return EmptyReadOnlyList<TSource>.GetInstance ();
			}
			if (count >= source.Count)
			{
				return source;
			}
			return new TakeReadOnlyList<TSource> (source, count);
		}

		/// <summary>
		/// Проецирует каждый элемент списка в новую форму.
		/// </summary>
		/// <typeparam name="TSource">Тип элементов списка source.</typeparam>
		/// <typeparam name="TResult">Тип значения, возвращаемого функцией selector.</typeparam>
		/// <param name="source">Список значений, для которых вызывается функция преобразования.</param>
		/// <param name="selector">Функция преобразования, применяемая к каждому элементу.</param>
		/// <returns>Список, элементы которого получены в результате вызова функции преобразования
		/// для каждого элемента списка source.</returns>
		public static IReadOnlyList<TResult> Select<TSource, TResult> (this IReadOnlyList<TSource> source, Func<TSource, TResult> selector)
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
				(IReadOnlyList<TResult>)EmptyReadOnlyList<TResult>.GetInstance () :
				new SelectReadOnlyList<TSource, TResult> (source, selector);
		}

		/// <summary>
		/// Проецирует каждый элемент списка в новую форму, добавляя индекс элемента.
		/// </summary>
		/// <typeparam name="TSource">Тип элементов списка source.</typeparam>
		/// <typeparam name="TResult">Тип значения, возвращаемого функцией selector.</typeparam>
		/// <param name="source">Список значений, для которых вызывается функция преобразования.</param>
		/// <param name="selector">Функция преобразования, применяемая к каждому исходному элементу;
		/// второй параметр функции представляет индекс исходного элемента.</param>
		/// <returns>Список, элементы которого получены в результате вызова функции преобразования
		/// для каждого элемента списка source.</returns>
		public static IReadOnlyList<TResult> Select<TSource, TResult> (this IReadOnlyList<TSource> source, Func<TSource, int, TResult> selector)
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
				(IReadOnlyList<TResult>)EmptyReadOnlyList<TResult>.GetInstance () :
				new SelectIndexReadOnlyList<TSource, TResult> (source, selector);
		}

		/// <summary>
		/// Изменяет порядок элементов списка на противоположный.
		/// </summary>
		/// <typeparam name="TSource">Тип элементов списка.</typeparam>
		/// <param name="source">Список значений, которые следует расставить в обратном порядке.</param>
		/// <returns>Список, элементы которого соответствуют элементам входного списка,
		/// но следуют в противоположном порядке.</returns>
		public static IReadOnlyList<TSource> Reverse<TSource> (this IReadOnlyList<TSource> source)
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
			return new ReverseReadOnlyList<TSource> (source);
		}

		/// <summary>
		/// Сортирует элементы списка в порядке возрастания с использованием указанного компаратора.
		/// </summary>
		/// <typeparam name="TSource">Тип элементов списка.</typeparam>
		/// <typeparam name="TKey">Тип ключа, возвращаемого функцией keySelector.</typeparam>
		/// <param name="source">Список значений, которые следует упорядочить.</param>
		/// <param name="keySelector">Функция, извлекающая ключ из элемента.</param>
		/// <param name="comparer">Компаратор, используемый для сравнения ключей.</param>
		/// <returns>Список, элементы которого отсортированы по ключу.</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage ("Microsoft.Design",
			"CA1026:DefaultParametersShouldNotBeUsed",
			Justification = "Parameter have clear right 'default' value and there is no plausible reason why the default might need to change.")]
		public static IOrderedReadOnlyList<TSource> OrderBy<TSource, TKey> (
			this IReadOnlyList<TSource> source,
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

			return new OrderedReadOnlyList<TSource, TKey> (source, keySelector, comparer, false, null);
		}

		/// <summary>
		/// Сортирует элементы списка в порядке убывания с использованием указанного компаратора.
		/// </summary>
		/// <typeparam name="TSource">Тип элементов списка.</typeparam>
		/// <typeparam name="TKey">Тип ключа, возвращаемого функцией keySelector.</typeparam>
		/// <param name="source">Список значений, которые следует упорядочить.</param>
		/// <param name="keySelector">Функция, извлекающая ключ из элемента.</param>
		/// <param name="comparer">Компаратор, используемый для сравнения ключей.</param>
		/// <returns>Список, элементы которого отсортированы по ключу в порядке убывания.</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage ("Microsoft.Design",
			"CA1026:DefaultParametersShouldNotBeUsed",
			Justification = "Parameter have clear right 'default' value and there is no plausible reason why the default might need to change.")]
		public static IOrderedReadOnlyList<TSource> OrderByDescending<TSource, TKey> (
			this IReadOnlyList<TSource> source,
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

			return new OrderedReadOnlyList<TSource, TKey> (source, keySelector, comparer, true, null);
		}

		/// <summary>
		/// Выполняет дополнительное упорядочение элементов списка в порядке возрастания с использованием указанного компаратора.
		/// </summary>
		/// <typeparam name="TSource">Тип элементов списка.</typeparam>
		/// <typeparam name="TKey">Тип ключа, возвращаемого функцией keySelector.</typeparam>
		/// <param name="source">Список, содержащий сортируемые элементы.</param>
		/// <param name="keySelector">Функция, извлекающая ключ из каждого элемента.</param>
		/// <param name="comparer">Компаратор, используемый для сравнения ключей.</param>
		/// <returns>Список, элементы которого отсортированы по ключу.</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage ("Microsoft.Design",
			"CA1026:DefaultParametersShouldNotBeUsed",
			Justification = "Parameter have clear right 'default' value and there is no plausible reason why the default might need to change.")]
		public static IOrderedReadOnlyList<TSource> ThenBy<TSource, TKey> (
			this IOrderedReadOnlyList<TSource> source,
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
			return source.CreateOrderedReadOnlyList<TKey> (keySelector, comparer, false);
		}

		/// <summary>
		/// Выполняет дополнительное упорядочение элементов списка в порядке убывания с использованием указанного компаратора.
		/// </summary>
		/// <typeparam name="TSource">Тип элементов списка.</typeparam>
		/// <typeparam name="TKey">Тип ключа, возвращаемого функцией keySelector.</typeparam>
		/// <param name="source">Список, содержащий сортируемые элементы.</param>
		/// <param name="keySelector">Функция, извлекающая ключ из каждого элемента.</param>
		/// <param name="comparer">Компаратор, используемый для сравнения ключей.</param>
		/// <returns>Список, элементы которого отсортированы по ключу в порядке убывания.</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage ("Microsoft.Design",
			"CA1026:DefaultParametersShouldNotBeUsed",
			Justification = "Parameter have clear right 'default' value and there is no plausible reason why the default might need to change.")]
		public static IOrderedReadOnlyList<TSource> ThenByDescending<TSource, TKey> (
			this IOrderedReadOnlyList<TSource> source,
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
			return source.CreateOrderedReadOnlyList<TKey> (keySelector, comparer, true);
		}

		/// <summary>
		/// Объединяет два списка.
		/// </summary>
		/// <typeparam name="TSource">Тип элементов списков.</typeparam>
		/// <param name="first">Первый из объединяемых списков.</param>
		/// <param name="second">Список, объединяемый с первым списком.</param>
		/// <returns>Список, содержащий элементы двух входных списков.</returns>
		public static IReadOnlyList<TSource> Concat<TSource> (this IReadOnlyList<TSource> first, IReadOnlyList<TSource> second)
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
			return new ConcatReadOnlyList<TSource> (first, second);
		}

		/// <summary>
		/// Объединяет две списка, используя указанную функцию предиката.
		/// </summary>
		/// <typeparam name="TFirst">Тип элементов первого входного списка.</typeparam>
		/// <typeparam name="TSecond">Тип элементов второго входного списка.</typeparam>
		/// <typeparam name="TResult">Тип элементов результирующего списка.</typeparam>
		/// <param name="first">Первый список для объединения.</param>
		/// <param name="second">Второй список для объединения.</param>
		/// <param name="selector">Функция, которая определяет, как объединить элементы двух списков.</param>
		/// <returns>Список, содержащий объединенные элементы двух входных списков.</returns>
		public static IReadOnlyList<TResult> Zip<TFirst, TSecond, TResult> (this IReadOnlyList<TFirst> first, IReadOnlyList<TSecond> second, Func<TFirst, TSecond, TResult> selector)
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
				(IReadOnlyList<TResult>)EmptyReadOnlyList<TResult>.GetInstance () :
				new ZipReadOnlyList<TFirst, TSecond, TResult> (first, second, selector);
		}

		#region private generated collection classes

		internal static class EmptyReadOnlyList<T>
		{
			private static volatile EmptySetImplementation _instance;
			internal static EmptySetImplementation GetInstance ()
			{
				if (_instance == null)
				{
					_instance = new EmptySetImplementation ();
				}
				return _instance;
			}

			internal class EmptySetImplementation : IReadOnlyFiniteSet<T>, IReadOnlyList<T>
			{
				private readonly IEnumerable<T> _items = new T[0];
				public EmptySetImplementation () { }
				public bool Contains (T item) { return false; }
				public int Count { get { return 0; } }
				IEnumerator IEnumerable.GetEnumerator () { return _items.GetEnumerator (); }
				public IEnumerator<T> GetEnumerator () { return _items.GetEnumerator (); }
				public T this[int index] { get { throw new ArgumentOutOfRangeException ("index"); } }
			}
		}

		internal class RangeReadOnlyList : IReadOnlyList<int>, IReadOnlyFiniteSet<int>
		{
			private readonly int _start;
			private readonly int _count;

			internal RangeReadOnlyList (int start, int count)
			{
				_start = start;
				_count = count;
			}

			public int this[int index]
			{
				get
				{
					if ((index < 0) || (index >= this.Count))
					{
						throw new ArgumentOutOfRangeException ("index");
					}
					return _start + index;
				}
			}

			public bool Contains (int item)
			{
				return (item >= _start) && (item < (_start + _count));
			}

			public int Count { get { return _count; } }

			IEnumerator IEnumerable.GetEnumerator () { return GetEnumerator (); }

			public IEnumerator<int> GetEnumerator ()
			{
				for (int i = 0; i < _count; ++i)
				{
					yield return (_start + i);
				}
			}
		}

		private class RepeatReadOnlyList<TResult> : IReadOnlyList<TResult>
		{
			private readonly TResult _element;
			private readonly int _count;

			internal RepeatReadOnlyList (TResult element, int count)
			{
				_element = element;
				_count = count;
			}

			public TResult this[int index]
			{
				get
				{
					if ((index < 0) || (index >= this.Count))
					{
						throw new ArgumentOutOfRangeException ("index");
					}
					return _element;
				}
			}

			public int Count { get { return _count; } }

			IEnumerator IEnumerable.GetEnumerator () { return GetEnumerator (); }

			public IEnumerator<TResult> GetEnumerator ()
			{
				for (int i = 0; i < _count; ++i)
				{
					yield return _element;
				}
			}
		}

		private class SkipReadOnlyList<TSource> : IReadOnlyList<TSource>
		{
			private readonly IReadOnlyList<TSource> _source;
			private readonly int _skipCount;

			internal SkipReadOnlyList (IReadOnlyList<TSource> source, int skipCount)
			{
				_source = source;
				_skipCount = skipCount;
			}

			public int Count { get { return (_source.Count - _skipCount); } }

			public TSource this[int index]
			{
				get
				{
					if ((index < 0) || (index >= this.Count))
					{
						throw new ArgumentOutOfRangeException ("index");
					}
					return _source[_skipCount + index];
				}
			}

			IEnumerator IEnumerable.GetEnumerator () { return GetEnumerator (); }

			public IEnumerator<TSource> GetEnumerator ()
			{
				return new ListRangeEnumerator<TSource> (_source, _skipCount, _source.Count - _skipCount);
			}
		}

		private class TakeReadOnlyList<TSource> : IReadOnlyList<TSource>
		{
			private readonly IReadOnlyList<TSource> _source;
			private readonly int _count;

			internal TakeReadOnlyList (IReadOnlyList<TSource> source, int count)
			{
				_source = source;
				_count = count;
			}

			public int Count { get { return _count; } }

			public TSource this[int index]
			{
				get
				{
					if ((index < 0) || (index >= this.Count))
					{
						throw new ArgumentOutOfRangeException ("index");
					}
					return _source[index];
				}
			}

			IEnumerator IEnumerable.GetEnumerator () { return GetEnumerator (); }

			public IEnumerator<TSource> GetEnumerator ()
			{
				return new ListRangeEnumerator<TSource> (_source, 0, _count);
			}
		}

		private class SelectReadOnlyList<TSource, TResult> : IReadOnlyList<TResult>
		{
			private IReadOnlyList<TSource> _source;
			private Func<TSource, TResult> _selector;

			internal SelectReadOnlyList (IReadOnlyList<TSource> source, Func<TSource, TResult> selector)
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

			public TResult this[int index]
			{
				get
				{
					if ((index < 0) || (index >= this.Count))
					{
						throw new ArgumentOutOfRangeException ("index");
					}
					return _selector.Invoke (_source[index]);
				}
			}
		}

		private class SelectIndexReadOnlyList<TSource, TResult> : IReadOnlyList<TResult>
		{
			private IReadOnlyList<TSource> _source;
			private Func<TSource, int, TResult> _selector;

			internal SelectIndexReadOnlyList (IReadOnlyList<TSource> source, Func<TSource, int, TResult> selector)
			{
				_source = source;
				_selector = selector;
			}

			public int Count { get { return _source.Count; } }

			public TResult this[int index]
			{
				get
				{
					if ((index < 0) || (index >= this.Count))
					{
						throw new ArgumentOutOfRangeException ("index");
					}
					return _selector.Invoke (_source[index], index);
				}
			}

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

		private class ReverseReadOnlyList<TSource> : IReadOnlyList<TSource>
		{
			private readonly IReadOnlyList<TSource> _source;

			internal ReverseReadOnlyList (IReadOnlyList<TSource> source)
			{
				_source = source;
			}

			public int Count { get { return _source.Count; } }

			public TSource this[int index]
			{
				get
				{
					if ((index < 0) || (index >= this.Count))
					{
						throw new ArgumentOutOfRangeException ("index");
					}
					return _source[_source.Count - index - 1];
				}
			}

			IEnumerator IEnumerable.GetEnumerator () { return GetEnumerator (); }

			public IEnumerator<TSource> GetEnumerator ()
			{
				return new ReverseListEnumerator<TSource> (_source);
			}
		}

		private class ConcatReadOnlyList<TSource> : IReadOnlyList<TSource>
		{
			private readonly IReadOnlyList<TSource> _first;
			private readonly IReadOnlyList<TSource> _second;

			internal ConcatReadOnlyList (IReadOnlyList<TSource> first, IReadOnlyList<TSource> second)
			{
				_first = first;
				_second = second;
			}

			public int Count { get { return (_first.Count + _second.Count); } }

			public TSource this[int index]
			{
				get
				{
					if ((index < 0) || (index >= this.Count))
					{
						throw new ArgumentOutOfRangeException ("index");
					}
					return (index < _first.Count) ? _first[index] : _second[index - _first.Count];
				}
			}

			IEnumerator IEnumerable.GetEnumerator () { return GetEnumerator (); }

			public IEnumerator<TSource> GetEnumerator ()
			{
				return new TwoListConcatEnumerator<TSource> (_first, _second);
			}
		}

		private class ZipReadOnlyList<TFirst, TSecond, TResult> : IReadOnlyList<TResult>
		{
			private readonly IReadOnlyList<TFirst> _first;
			private readonly IReadOnlyList<TSecond> _second;
			private readonly Func<TFirst, TSecond, TResult> _selector;

			internal ZipReadOnlyList (IReadOnlyList<TFirst> first, IReadOnlyList<TSecond> second, Func<TFirst, TSecond, TResult> selector)
			{
				_first = first;
				_second = second;
				_selector = selector;
			}

			public int Count { get { return Math.Min (_first.Count, _second.Count); } }

			public TResult this[int index]
			{
				get
				{
					if ((index < 0) || (index >= this.Count))
					{
						throw new ArgumentOutOfRangeException ("index");
					}
					return _selector.Invoke (_first[index], _second[index]);
				}
			}

			IEnumerator IEnumerable.GetEnumerator () { return GetEnumerator (); }

			public IEnumerator<TResult> GetEnumerator ()
			{
				return new TwoListZipEnumerator<TFirst, TSecond, TResult> (_first, _second, _selector);
			}
		}

		private abstract class OrderedReadOnlyList<TElement> : IOrderedReadOnlyList<TElement>
		{
			protected readonly IReadOnlyList<TElement> _source;
			private int[] _indexMap = null;

			protected OrderedReadOnlyList (IReadOnlyList<TElement> source)
			{
				_source = source;
			}

			public TElement this[int index]
			{
				get
				{
					if (_indexMap == null)
					{
						var sorter = CreateSorter (null);
						_indexMap = sorter.CreateIndex ();
					}
					return _source[_indexMap[index]];
				}
			}

			public int Count { get { return _source.Count; } }

			IEnumerator IEnumerable.GetEnumerator () { return GetEnumerator (); }

			public IEnumerator<TElement> GetEnumerator ()
			{
				if (_source.Count > 0)
				{
					if (_indexMap == null)
					{
						var sorter = CreateSorter (null);
						_indexMap = sorter.CreateIndex ();
					}
					for (int i = 0; i < _source.Count; ++i)
					{
						yield return _source[_indexMap[i]];
					}
				}
			}

			internal abstract CollectionSorter<TElement> CreateSorter (CollectionSorter<TElement> next);

			System.Linq.IOrderedEnumerable<TElement> System.Linq.IOrderedEnumerable<TElement>.CreateOrderedEnumerable<TKey> (Func<TElement, TKey> keySelector, IComparer<TKey> comparer, bool descending)
			{
				return new OrderedReadOnlyList<TElement, TKey> (_source, keySelector, comparer, descending, this);
			}

			IOrderedReadOnlyCollection<TElement> IOrderedReadOnlyCollection<TElement>.CreateOrderedReadOnlyCollection<TKey> (Func<TElement, TKey> keySelector, IComparer<TKey> comparer, bool descending)
			{
				return new OrderedReadOnlyList<TElement, TKey> (_source, keySelector, comparer, descending, this);
			}

			IOrderedReadOnlyList<TElement> IOrderedReadOnlyList<TElement>.CreateOrderedReadOnlyList<TKey> (Func<TElement, TKey> keySelector, IComparer<TKey> comparer, bool descending)
			{
				return new OrderedReadOnlyList<TElement, TKey> (_source, keySelector, comparer, descending, this);
			}
		}

		private class OrderedReadOnlyList<TElement, TKey> : OrderedReadOnlyList<TElement>
		{
			private readonly OrderedReadOnlyList<TElement> _parent = null;
			private readonly Func<TElement, TKey> _keySelector;
			private readonly IComparer<TKey> _comparer;
			private readonly bool _descending;

			internal OrderedReadOnlyList (IReadOnlyList<TElement> source, Func<TElement, TKey> keySelector, IComparer<TKey> comparer, bool descending, OrderedReadOnlyList<TElement> parent)
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
