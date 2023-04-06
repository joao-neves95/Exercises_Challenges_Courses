using AlgosAndDataStructs.DataStructures;

using FluentAssertions;

namespace AlgosAndDataStructs.Tests.DataStructures
{
    public class DynamicArrayTests
    {
        [Fact]
        public void Create_Passes()
        {
            var newArray = new DynamicArray<int>(40);

            newArray.Length.Should().Be(0);
        }

        [Fact]
        public void Get_OverflowReturnsNull_Passes()
        {
            const string value = "ABC";
            const int size = 10;
            var newArray = new DynamicArray<string>(size).Fill(value);

            newArray.Length.Should().Be(size);
            newArray.Size.Should().Be(size);
            newArray.Get(-1).Should().Be(default);
            newArray.Get(0).Should().Be(value);
            newArray.Get(size - 1).Should().Be(value);
            newArray.Get(size).Should().Be(default);
        }

        [Fact]
        public void Fill_Passes()
        {
            const string value = "Hello";
            const int size = 10;
            var newArray = new DynamicArray<string>(size).Fill(value);

            newArray.Length.Should().Be(size);
            newArray.Size.Should().Be(size);
            newArray.Get((int)Math.Floor(size / 2d)).Should().Be(value);
        }

        [Fact]
        public void Add_Passes()
        {
            var newArray = new DynamicArray<int>();
            newArray.Add(123);

            newArray.Length.Should().Be(1);
        }

        [Fact]
        public void Add_MoreThanSize_Passes()
        {
            const int size = 10;

            var newArray = new DynamicArray<int>(size);
            for (int i = 0; i < size + 1; ++i)
            {
                newArray.Add(i);
            }

            newArray.Length.Should().Be(size + 1);
            newArray.Size.Should().Be(size * 2);
        }

        [Fact]
        public void Add_LessThanSize_Passes()
        {
            const int size = 10;

            var newArray = new DynamicArray<int>(size);
            for (int i = 0; i < size - 1; ++i)
            {
                newArray.Add(i);
            }

            newArray.Length.Should().Be(size - 1);
            newArray.Size.Should().Be(size);
        }

        [Fact]
        public void Delete_Passes()
        {
            const int size = 10;

            var newArray = new DynamicArray<int>(size).Fill(1).Delete(2);

            newArray.Length.Should().Be(size - 1);
            newArray.Size.Should().Be(size);
        }

        [Fact]
        public void Pop_Passes()
        {
            const int addedElems = 2;
            const int initialSize = 10 - addedElems;
            const int finalLength = (initialSize - 1) + addedElems;
            const int finalSize = initialSize * 2;
            const int diffElem = 200;

            var newArray = new DynamicArray<int>(initialSize)
                .Fill(123)
                .Add(diffElem)
                .Add(1)
                .Pop();

            newArray.Peek().Should().Be(diffElem);
            newArray.Length.Should().Be(finalLength);
            newArray.Size.Should().Be(finalSize);
        }
    }
}
