using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace BusinessClassLibrary.Collections.Linq
{
	/// <summary>
	/// Методы расширения к структурно сравниваемым объектам.
	/// </summary>
	public static class StructuralEquatable
	{
		/// <summary>
		/// Определяет, совпадают ли две коллекции, используя для сравнения элементов указанный компаратор.
		/// </summary>
		/// <typeparam name="TSource">Тип элементов входных коллекций.</typeparam>
		/// <param name="first">Объект, сравниваемый с коллекцией second.</param>
		/// <param name="second">Объект, сравниваемый с последовательностью first.</param>
		/// <param name="comparer">Компаратор, используемый для сравнения элементов.</param>
		/// <returns>True, если у двух указанных коллекций одинаковая длина и соответствующие элементы совпадают
		/// согласно компаратору comparer, в противном случае — False.</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage ("Microsoft.Design",
			"CA1026:DefaultParametersShouldNotBeUsed",
			Justification = "Parameter have clear right 'default' value and there is no plausible reason why the default might need to change.")]
		public static bool SequenceEqual<TSource> (
			this IStructuralEquatable first,
			IEnumerable<TSource> second,
			IEqualityComparer comparer = null)
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

			return first.Equals (second, comparer ?? EqualityComparer<TSource>.Default);
		}
	}
}
