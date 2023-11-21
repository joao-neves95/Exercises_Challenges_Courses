using AlgosAndDataStructs.DataStructures;
using AlgosAndDataStructs.DataStructures.Options;

using FluentAssertions;

namespace AlgosAndDataStructs.Tests.DataStructures
{
    public class BinarySearchTreeTests
    {
        [Fact]
        public void Create_Passes()
        {
            var tree1 = new BinarySearchTree<int>();
            var tree2 = new BinarySearchTree<int>(new[] { 0, 1, 2, 3, 4, 5 });

            tree1.Count.Should().Be(0);
            tree1.Root?.Value.Should().Be(null);

            tree2.Count.Should().Be(6);
            tree2.Root?.Value.Should().Be(0);
        }

        [Fact]
        public void Insert_OneItem_Passes()
        {
            var tree = new BinarySearchTree<int>();
            tree.Insert(9);

            tree.Count.Should().Be(1);
            tree.Root?.Value.Should().Be(9);
        }

        [Fact]
        public void Insert_MultipleItems_CreatesATree()
        {
            var tree1 = new BinarySearchTree<int>();
            tree1.Insert(9);
            tree1.Insert(4);
            tree1.Insert(6);
            tree1.Insert(20);
            tree1.Insert(170);
            tree1.Insert(15);
            tree1.Insert(1);

            var tree2 = new BinarySearchTree<int>(new[] { 9, 4, 6, 20, 170, 15, 1 });

            tree1.Count.Should().Be(7);
            tree2.Count.Should().Be(7);

            /*
                   9
               4      20
              1 6   15  170
            */
            const string expectedJson = "{\"Value\":9,\"Left\":{\"Value\":4,\"Left\":{\"Value\":1,\"Left\":null,\"Right\":null},\"Right\":{\"Value\":6,\"Left\":null,\"Right\":null}},\"Right\":{\"Value\":20,\"Left\":{\"Value\":15,\"Left\":null,\"Right\":null},\"Right\":{\"Value\":170,\"Left\":null,\"Right\":null}}}";

            TestUtils.ToJson(tree1, JsonIdentation.None).Should().Be(expectedJson);
            TestUtils.ToJson(tree2, JsonIdentation.None).Should().Be(expectedJson);
        }
    }
}
