namespace AlgosAndDataStructs.DataStructures.Types
{
    public class BinaryNode<TValue>
    {
        public BinaryNode()
        {
        }

        public BinaryNode(TValue value)
        {
            Value = value;
        }

        public required TValue? Value { get; set; }

        public BinaryNode<TValue>? Left { get; set; }

        public BinaryNode<TValue>? Right { get; set; }
    }
}
