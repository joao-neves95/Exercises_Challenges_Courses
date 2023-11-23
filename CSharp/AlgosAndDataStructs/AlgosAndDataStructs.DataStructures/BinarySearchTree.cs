using AlgosAndDataStructs.DataStructures.Traits;
using AlgosAndDataStructs.DataStructures.Types;

namespace AlgosAndDataStructs.DataStructures
{
    public class BinarySearchTree<T> : ICountable, IJsonSerializable
        where T : IComparable<T>
    {
        public BinaryNode<T>? Root { get; private set; }

        public int Count { get; private set; }

        public BinarySearchTree()
        {
        }

        public BinarySearchTree(T value)
        {
            Insert(value);
        }

        /// <summary>
        /// O(1)
        ///
        /// </summary>
        int ICountable.Count()
        {
            return Count;
        }

        /// <summary>
        /// Creates the tree by inserting each item in the same order as the input Enumerable. <br />
        /// O(n)
        ///
        /// </summary>
        public BinarySearchTree(IEnumerable<T> initialValues)
        {
            foreach (var value in initialValues.ToArray())
            {
                Insert(value);
            }
        }

        /// <summary>
        /// O(log n) or O(n)
        ///
        /// </summary>
        public BinarySearchTree<T> Insert(T value)
        {
            var newNode = new BinaryNode<T>() { Value = value };

            if (Root == null)
            {
                Root = newNode;
                ++Count;

                return this;
            }

            BinaryNode<T>? currentNode = Root;

            while (currentNode != null)
            {
                if (value.CompareTo(currentNode.Value) < 0)
                {
                    if (currentNode.Left == null)
                    {
                        currentNode.Left = newNode;

                        break;
                    }
                    else
                    {
                        currentNode = currentNode.Left;
                    }
                }
                else
                {
                    if (currentNode.Right == null)
                    {
                        currentNode.Right = newNode;

                        break;
                    }
                    else
                    {
                        currentNode = currentNode.Right;
                    }
                }
            }

            ++Count;

            return this;
        }

        /// <summary>
        /// O(log n) or O(n)
        ///
        /// </summary>
        public BinaryNode<T>? Lookup(T value)
        {
            if (Root == null)
            {
                return null;
            }
            else if (Count == 1)
            {
                return Root;
            }

            var currentNode = Root;

            while (currentNode != null)
            {
                if (value.CompareTo(currentNode.Value) == 0)
                {
                    return currentNode;
                }
                else if (value.CompareTo(currentNode.Value) < 0)
                {
                    currentNode = currentNode.Left;
                }
                else
                {
                    currentNode = currentNode.Right;
                }
            }

            return null;
        }

        public object GetJsonObject()
        {
            return Root;
        }
    }
}
