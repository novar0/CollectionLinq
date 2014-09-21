using System;
using System.Collections.Generic;

namespace BusinessClassLibrary.Collections.Linq
{
	/// <summary>
	/// Представляет отсортированный список.
	/// </summary>
	/// <typeparam name="TElement">Тип элементов списка.</typeparam>
	[System.Diagnostics.CodeAnalysis.SuppressMessage ("Microsoft.Naming",
		"CA1710:IdentifiersShouldHaveCorrectSuffix",
		Justification = "Implemented interfaces has no association with class name.")]
	public interface IOrderedReadOnlyList<TElement> :
		IOrderedReadOnlyCollection<TElement>,
		IReadOnlyList<TElement>
	{
		/// <summary>
		/// Выполняет дополнительное упорядочение элементов объекта IOrderedReadOnlyList<TElement> по ключу.
		/// </summary>
		/// <typeparam name="TKey">Тип ключа, созданного функцией keySelector.</typeparam>
		/// <param name="keySelector">Функция, используемая для извлечения ключа для каждого элемента.</param>
		/// <param name="comparer">Компаратор, используемый для сравнения ключей при формировании возвращаемого списка.</param>
		/// <param name="descending">True, если элементы требуется сортировать в порядке убывания; False, чтобы сортировать элементы в порядке возрастания.</param>
		/// <returns>Список, элементы которой отсортированы по ключу.</returns>
		IOrderedReadOnlyList<TElement> CreateOrderedReadOnlyList<TKey> (Func<TElement, TKey> keySelector, IComparer<TKey> comparer, bool descending);
	}
}
