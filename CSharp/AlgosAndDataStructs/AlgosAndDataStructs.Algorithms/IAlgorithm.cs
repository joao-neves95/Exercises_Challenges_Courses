namespace AlgosAndDataStructs.Algorithms
{
    public interface IAlgorithm<in TIn, out TOut>
    {
        public static abstract TOut? BruteForce(TIn input);

        public static abstract TOut? Optimized(TIn input);
    }

    public interface IAlgorithm<in TIn1, in TIn2, out TOut>
    {
        public static abstract TOut? BruteForce(TIn1 input1, TIn2 input2);

        public static abstract TOut? Optimized(TIn1 input1, TIn2 input2);
    }
}
