using AlgosAndDataStructs.DataStructures;
using AlgosAndDataStructs.DataStructures.Options;
using AlgosAndDataStructs.DataStructures.Types;

using FluentAssertions;

namespace AlgosAndDataStructs.Tests.DataStructures
{
    public class BinarySearchTreeTests
    {
        [Fact]
        public void Create_Passes()
        {
            var tree1 = new BinarySearchTree<int>();
            tree1.Count.Should().Be(0);
            tree1.Root?.Value.Should().Be(null);

            var tree2 = CreateMockTree();
            tree2.Root?.Value.Should().Be(9);
        }

        [Fact]
        public void Insert_OneItem_Passes()
        {
            var tree = new BinarySearchTree<int>()
                .Insert(9);

            tree.Count.Should().Be(1);
            tree.Root?.Value.Should().Be(9);
        }

        [Fact]
        public void Insert_MultipleItems_CreatesATree()
        {
            var tree1 = new BinarySearchTree<int>()
                .Insert(9)
                .Insert(4)
                .Insert(6)
                .Insert(20)
                .Insert(170)
                .Insert(15)
                .Insert(1);

            tree1.Count.Should().Be(7);

            var tree2 = CreateMockTree();

            /*
                   9
               4      20
              1 6   15  170
            */
            const string expectedJson = "{\"Value\":9,\"Left\":{\"Value\":4,\"Left\":{\"Value\":1,\"Left\":null,\"Right\":null},\"Right\":{\"Value\":6,\"Left\":null,\"Right\":null}},\"Right\":{\"Value\":20,\"Left\":{\"Value\":15,\"Left\":null,\"Right\":null},\"Right\":{\"Value\":170,\"Left\":null,\"Right\":null}}}";

            TestUtils.ToJson(tree1, JsonIdentation.None).Should().Be(expectedJson);
            TestUtils.ToJson(tree2, JsonIdentation.None).Should().Be(expectedJson);
        }

        [Fact]
        public void Lookup_ReturnTheCorrectNode()
        {
            var tree1 = CreateMockTree();

            var expected_6 = new BinaryNode<int>() { Value = 6 };
            var expected_170 = new BinaryNode<int>() { Value = 170 };

            tree1.Lookup(-100).Should().Be(null);
            tree1.Lookup(6).Should().BeEquivalentTo(expected_6);
            tree1.Lookup(170).Should().BeEquivalentTo(expected_170);
        }

        private static BinarySearchTree<int> CreateMockTree()
        {
            var newTree = new BinarySearchTree<int>(new[] { 9, 4, 6, 20, 170, 15, 1 });
            newTree.Count.Should().Be(7);

            return newTree;
        }
    }
}
