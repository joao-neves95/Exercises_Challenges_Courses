namespace AlgosAndDataStructs.DataStructures.Types
{
    public struct KeyValuePair<TKey, TValue>
    {
        public KeyValuePair(TKey key, TValue value) : this()
        {
            Key = key;
            Value = value;
        }

        public TKey Key { get; set; }

        public TValue Value { get; set; }
    }
}
