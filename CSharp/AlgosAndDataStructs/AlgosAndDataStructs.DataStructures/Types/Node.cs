namespace AlgosAndDataStructs.DataStructures.Types
{
    public class Node<TValue>
    {
        public Node()
        {
        }

        public Node(TValue value)
        {
            Value = value;
        }

        public required TValue Value { get; set; }

        public Node<TValue>? NextNode { get; set; }
    }
}
