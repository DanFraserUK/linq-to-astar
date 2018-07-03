namespace Heuristic.Linq.Algorithms
{
    internal struct RecursionState<TFactor, TStep>
    {
        public RecursionFlag Flag
        {
            get; private set;
        }

        public Node<TFactor, TStep> Node
        {
            get; private set;
        }

        public RecursionState(RecursionFlag flag, Node<TFactor, TStep> node)
        {
            Flag = flag;
            Node = node;
        }

        public override string ToString()
        {
            return $"{Node.Step} -> {Node.Factor} ({Flag})";
        }
    }

    internal enum RecursionFlag
    {
        Found,

        InProgress,

        NotFound,
    }
}