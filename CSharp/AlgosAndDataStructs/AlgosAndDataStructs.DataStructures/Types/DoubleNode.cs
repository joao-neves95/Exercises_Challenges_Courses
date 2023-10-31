namespace AlgosAndDataStructs.DataStructures.Types
{
    public class DoubleNode<TValue>
    {
        public DoubleNode()
        {
        }

        public DoubleNode(TValue value)
        {
            Value = value;
        }

        public required TValue Value { get; set; }

        public DoubleNode<TValue>? PreviousNode { get; set; }

        public DoubleNode<TValue>? NextNode { get; set; }
    }
}
