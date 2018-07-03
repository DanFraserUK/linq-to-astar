using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace LinqToAStar.Core
{
    static class IterativeDeepeningAStar
    {
        public static Node<TFactor, TStep> Run<TFactor, TStep>(HeuristicSearchBase<TFactor, TStep> source)
        {
            Debug.WriteLine("LINQ Expression Stack: {0}", source);

            return new IterativeDeepeningAStar<TFactor, TStep>(source).Run();
        }
    }

    class IterativeDeepeningAStar<TFactor, TStep>
    {
        #region Fields

        private readonly HeuristicSearchBase<TFactor, TStep> _source;
        private readonly int _max = 1024;

        #endregion

        #region Properties

        public int MaxNumberOfLoops => _max;

        #endregion

        #region Constructor

        internal IterativeDeepeningAStar(HeuristicSearchBase<TFactor, TStep> source)
        {
            _source = source;
        }

        #endregion

        #region Override

        public Node<TFactor, TStep> Run()
        {
            var counter = 0;
            var path = new Stack<Node<TFactor, TStep>>(_source.ConvertToNodes(_source.From, 0).OrderBy(n => n.Factor, _source.NodeComparer));
            var bound = path.Peek();

            while (counter <= _max)
            {
                var t = Search(path, bound, new HashSet<TStep>(_source.StepComparer));

                if (t.Flag == RecursionFlag.Found)
                    return t.Node;
                if (t.Flag == RecursionFlag.NotFound)
                    return null;

                // In Progress
                bound = t.Node;
                counter++;
            }
            return null;
        }

        #endregion

        #region Core

        private RecursionState<TFactor, TStep> Search(Stack<Node<TFactor, TStep>> path, Node<TFactor, TStep> bound, ISet<TStep> visited)
        {
            var current = path.Peek();

            if (_source.NodeComparer.Compare(current, bound) > 0)
                return new RecursionState<TFactor, TStep>(RecursionFlag.InProgress, current);

            if (_source.StepComparer.Equals(current.Step, _source.To))
                return new RecursionState<TFactor, TStep>(RecursionFlag.Found, current);

            var min = default(Node<TFactor, TStep>);
            var hasMin = false;
            var nexts = _source.Expands(current.Step, current.Level, visited.Add).ToArray();

            Array.Sort(nexts, _source.NodeComparer);

            foreach (var next in nexts)
            {
                Debug.WriteLine($"{current.Step}\t{current.Level} -> {next.Step}\t{next.Level}");

                next.Previous = current;
                path.Push(next);

                var t = Search(path, bound, visited);

                if (t.Flag == RecursionFlag.Found) return t;
                if (!hasMin || _source.NodeComparer.Compare(t.Node, min) < 0)
                {
                    min = t.Node;
                    hasMin = true;
                }
                path.Pop();
            }
            return new RecursionState<TFactor, TStep>(hasMin ? RecursionFlag.InProgress : RecursionFlag.NotFound, min);
        }

        #endregion 
    }

    internal struct IterativeDeepeningAStarAlgorithm : IAlgorithm
    {
        string IAlgorithm.AlgorithmName => nameof(IterativeDeepeningAStar);

        Node<TFactor, TStep> IAlgorithm.Run<TFactor, TStep>(HeuristicSearchBase<TFactor, TStep> source)
        {
            Debug.WriteLine("LINQ Expression Stack: {0}", source);

            return new IterativeDeepeningAStar<TFactor, TStep>(source).Run();
        }
    }
}