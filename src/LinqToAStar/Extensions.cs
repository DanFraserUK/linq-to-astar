﻿using System;
using System.Collections.Generic;

namespace LinqToAStar
{
    /// <summary>
    /// Provide a set of LINQ clauses to <see cref="HeuristicSearchBase{TFactor, TStep}"/> class.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Select the factor used to evaluate with heuristic functions.
        /// </summary>
        /// <typeparam name="TSource">The source type of factor used to evaluate with heuristic function.</typeparam>
        /// <typeparam name="TFactor">The type of factor used to evaluate with heuristic function.</typeparam>
        /// <typeparam name="TStep">The type of step of the problem.</typeparam>
        /// <param name="source">The current instance.</param>
        /// <param name="selector">The selector to select factor from current instance.</param>
        /// <returns>An instance with type <typeparamref name="TFactor"/> as factor.</returns>
        public static HeuristicSearchBase<TFactor, TStep> Select<TSource, TFactor, TStep>(this HeuristicSearchBase<TSource, TStep> source, Func<TSource, TFactor> selector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return new HeuristicSearchSelect<TSource, TFactor, TStep>(source, (s, i) => selector(s));
        }

        /// <summary>
        /// Select the factor used to evaluate with heuristic functions.
        /// </summary>
        /// <typeparam name="TSource">The source type of factor used to evaluate with heuristic function.</typeparam>
        /// <typeparam name="TFactor">The type of factor used to evaluate with heuristic function.</typeparam>
        /// <typeparam name="TStep">The type of step of the problem.</typeparam>
        /// <param name="source">The current instance.</param>
        /// <param name="selector">The selector with index argument to select factor from current instance.</param>
        /// <returns>An instance with type <typeparamref name="TFactor"/> as factor.</returns>
        public static HeuristicSearchBase<TFactor, TStep> Select<TSource, TFactor, TStep>(this HeuristicSearchBase<TSource, TStep> source, Func<TSource, int, TFactor> selector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return new HeuristicSearchSelect<TSource, TFactor, TStep>(source, selector);
        }

        /// <summary>
        /// Select one or more factors used to evaluate with heuristic functions.
        /// </summary>
        /// <typeparam name="TSource">The source type of factor used to evaluate with heuristic function.</typeparam>
        /// <typeparam name="TFactor">The type of factor used to evaluate with heuristic function.</typeparam>
        /// <typeparam name="TStep">The type of step of the problem.</typeparam>
        /// <param name="source">The current instance.</param>
        /// <param name="selector">The selector to select factor from current instance.</param>
        /// <returns>An instance with type <typeparamref name="TFactor"/> as factor.</returns>
        public static HeuristicSearchBase<TFactor, TStep> SelectMany<TSource, TFactor, TStep>(this HeuristicSearchBase<TSource, TStep> source, Func<TSource, IEnumerable<TFactor>> selector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return new HeuristicSearchSelectMany<TSource, TFactor, TStep>(source, (s, i) => selector(s));
        }

        /// <summary>
        /// Select one or more factors used to evaluate with heuristic functions.
        /// </summary>
        /// <typeparam name="TSource">The source type of factor used to evaluate with heuristic function.</typeparam>
        /// <typeparam name="TFactor">The type of factor used to evaluate with heuristic function.</typeparam>
        /// <typeparam name="TStep">The type of step of the problem.</typeparam>
        /// <param name="source">The current instance.</param>
        /// <param name="selector">The selector with index argument to select factor from current instance.</param>
        /// <returns>An instance with type <typeparamref name="TFactor"/> as factor.</returns>        
        public static HeuristicSearchBase<TFactor, TStep> SelectMany<TSource, TFactor, TStep>(this HeuristicSearchBase<TSource, TStep> source, Func<TSource, int, IEnumerable<TFactor>> selector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return new HeuristicSearchSelectMany<TSource, TFactor, TStep>(source, selector);
        }

        public static HeuristicSearchBase<TFactor, TStep> SelectMany<TSource, TCollection, TFactor, TStep>(this HeuristicSearchBase<TSource, TStep> source, Func<TSource, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TFactor> factorSelector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (collectionSelector == null) throw new ArgumentNullException(nameof(collectionSelector));
            if (factorSelector == null) throw new ArgumentNullException(nameof(factorSelector));

            return new HeuristicSearchSelectMany<TSource, TCollection, TFactor, TStep>(source, (s, i) => collectionSelector(s), factorSelector);
        }

        public static HeuristicSearchBase<TFactor, TStep> SelectMany<TSource, TCollection, TFactor, TStep>(this HeuristicSearchBase<TSource, TStep> source, Func<TSource, int, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TFactor> factorSelector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (collectionSelector == null) throw new ArgumentNullException(nameof(collectionSelector));
            if (factorSelector == null) throw new ArgumentNullException(nameof(factorSelector));

            return new HeuristicSearchSelectMany<TSource, TCollection, TFactor, TStep>(source, collectionSelector, factorSelector);
        }

        public static HeuristicSearchBase<TFactor, TStep> Where<TFactor, TStep>(this HeuristicSearchBase<TFactor, TStep> source, Func<TFactor, bool> predicate)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            return new HeuristicSearchWhere<TFactor, TStep>(source, (r, i) => predicate(r));
        }

        public static HeuristicSearchBase<TFactor, TStep> Where<TFactor, TStep>(this HeuristicSearchBase<TFactor, TStep> source, Func<TFactor, int, bool> predicate)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            return new HeuristicSearchWhere<TFactor, TStep>(source, predicate);
        }

        public static HeuristicSearchBase<TFactor, TStep> Except<TFactor, TStep>(this HeuristicSearchBase<TFactor, TStep> source, IEnumerable<TFactor> collection)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            return new HeuristicSearchExcept<TFactor, TStep>(source, collection, null);
        }

        public static HeuristicSearchBase<TFactor, TStep> Except<TFactor, TStep>(this HeuristicSearchBase<TFactor, TStep> source, IEnumerable<TFactor> collection, IEqualityComparer<TFactor> comparer)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            return new HeuristicSearchExcept<TFactor, TStep>(source, collection, comparer);
        }

        public static HeuristicSearchBase<TFactor, TStep> Contains<TFactor, TStep>(this HeuristicSearchBase<TFactor, TStep> source, IEnumerable<TFactor> collection)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            return new HeuristicSearchContains<TFactor, TStep>(source, collection, null);
        }

        public static HeuristicSearchBase<TFactor, TStep> Contains<TFactor, TStep>(this HeuristicSearchBase<TFactor, TStep> source, IEnumerable<TFactor> collection, IEqualityComparer<TFactor> comparer)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            return new HeuristicSearchContains<TFactor, TStep>(source, collection, comparer);
        }

        public static HeuristicSearchBase<TFactor, TStep> Reverse<TFactor, TStep>(this HeuristicSearchBase<TFactor, TStep> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            source.IsReversed = !source.IsReversed;

            return source;
        }
    }
}