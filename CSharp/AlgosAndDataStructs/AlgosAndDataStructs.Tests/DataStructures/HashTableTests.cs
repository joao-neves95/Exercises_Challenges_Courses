using AlgosAndDataStructs.DataStructures;

using FluentAssertions;

namespace AlgosAndDataStructs.Tests.DataStructures
{
    public class HashTableTests
    {
        [Fact]
        public void Create_Passes()
        {
            var table = new HashTable<int>(40);

            var table2 = new HashTable<int>(new[]
            {
                new KeyValuePair<string, int>("1", 123),
                new KeyValuePair<string, int>("2", 456),
                new KeyValuePair<string, int>("3", 789),
            },
            40);

            table.Count.Should().Be(0);
            table2.Count.Should().Be(3);
            table.Size.Should().Be(40);
        }

        [Fact]
        public void Add_WithoutResize_Passes()
        {
            var table = new HashTable<int>(6);
            table.Add("1", 123);

            table.Count.Should().Be(1);
            table.Size.Should().Be(6);

            var table2 = new HashTable<int>(
                new[]
                {
                    new KeyValuePair < string, int >("1", 123),
                },
                6);
            table2.Add("2", 456);

            table2.Count.Should().Be(2);
            table2.Size.Should().Be(6);
        }

        [Fact]
        public void Add_WithResize_Passes()
        {
            var table = new HashTable<int>(3);
            table.Add("1", 123);
            table.Add("2", 456);
            table.Add("3", 789);
            table.Add("4", 321);

            var table2 = new HashTable<int>(new[]
            {
                new KeyValuePair < string, int >("1", 123),
                new KeyValuePair < string, int >("2", 456),
                new KeyValuePair < string, int >("3", 789),
            });

            table2.Add("4", 321);

            table.Count.Should().Be(4);
            table2.Count.Should().Be(4);
            table.Size.Should().Be(6);
            table2.Size.Should().Be(6);
        }

        [Fact]
        public void Remove_WithoutResize_Passes()
        {
            var table = new HashTable<int>(6);
            table.Add("1", 123);
            table.Add("2", 456);
            table.Add("3", 789);
            table.Remove("1");

            table.Count.Should().Be(2);
            table.Size.Should().Be(6);

            table.Keys().Should().BeEquivalentTo(new[] { "2", "3" });
            table.Values().Should().BeEquivalentTo(new[] { 456, 789 });
        }

        [Fact]
        public void Remove_WithResize_Passes()
        {
            var table = new HashTable<int>(new[]
            {
                new KeyValuePair < string, int >("1", 123),
                new KeyValuePair < string, int >("2", 456),
                new KeyValuePair < string, int >("3", 789),
            },
            10);

            table.Remove("2");

            table.Count.Should().Be(2);
            table.Size.Should().Be(5);
        }
    }
}
