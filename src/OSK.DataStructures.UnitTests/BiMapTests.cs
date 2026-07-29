using System.Collections.Generic;

namespace OSK.DataStructures.UnitTests;

public class BiMapTests
{
    #region Variables

    private readonly BiMap<string, int> _biMap;

    #endregion

    #region Constructors

    public BiMapTests()
    {
        _biMap = new BiMap<string, int>();
    }

    [Fact]
    public void DefaultConstructor_CreatesEmptyBiMap_CountIsZero()
    {
        // Arrange
        var bimap = new BiMap<string, int>();

        // Act

        // Assert
        Assert.Equal(0, bimap.Count);
        Assert.Empty(bimap.Left);
        Assert.Empty(bimap.Right);
    }

    [Fact]
    public void Constructor_WithEntries_PopulatesForward()
    {
        // Arrange
        var entries = new[]
        {
            new MapEntry<string, int>("one", 1),
            new MapEntry<string, int>("two", 2),
            new MapEntry<string, int>("three", 3),
        };

        // Act
        var bimap = new BiMap<string, int>(entries);

        // Assert
        Assert.Equal(3, bimap.Count);
        Assert.Contains("one", bimap.Left);
        Assert.Contains("two", bimap.Left);
        Assert.Contains("three", bimap.Left);
        Assert.Contains(1, bimap.Right);
        Assert.Contains(2, bimap.Right);
        Assert.Contains(3, bimap.Right);
        Assert.Equal(1, bimap["one"]);
        Assert.Equal(2, bimap["two"]);
        Assert.Equal(3, bimap["three"]);
        Assert.Equal("one", bimap[1]);
        Assert.Equal("two", bimap[2]);
        Assert.Equal("three", bimap[3]);
    }

    [Fact]
    public void Constructor_WithReversedEntries_PopulatesCorrectly()
    {
        // Arrange
        var entries = new[]
        {
            new MapEntry<int, string>(1, "one"),
            new MapEntry<int, string>(2, "two"),
        };

        // Act
        var bimap = new BiMap<string, int>(entries);

        // Assert
        Assert.Equal(2, bimap.Count);
        Assert.Equal("one", bimap[1]);
        Assert.Equal("two", bimap[2]);
        Assert.Equal(1, bimap["one"]);
        Assert.Equal(2, bimap["two"]);
    }

    #endregion

    #region Count

    [Fact]
    public void Count_EmptyBiMap_ReturnsZero()
    {
        // Arrange
        var bimap = new BiMap<string, int>();

        // Act

        // Assert
        Assert.Equal(0, bimap.Count);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(100)]
    public void Count_AfterAdditions_ReturnsCorrectCount(int count)
    {
        // Arrange
        var bimap = new BiMap<string, int>();

        // Act
        for (int i = 0; i < count; i++)
        {
            bimap.Add($"key{i}", i);
        }

        // Assert
        Assert.Equal(count, bimap.Count);
    }

    #endregion

    #region Left

    [Fact]
    public void Left_ReturnsAllKeys()
    {
        // Arrange
        var bimap = new BiMap<string, int>
        {
            ["a"] = 1,
            ["b"] = 2,
            ["c"] = 3,
        };

        // Act
        var left = new List<string>(bimap.Left);

        // Assert
        Assert.Equal(3, left.Count);
        Assert.Contains("a", left);
        Assert.Contains("b", left);
        Assert.Contains("c", left);
    }

    #endregion

    #region Right

    [Fact]
    public void Right_ReturnsAllValues()
    {
        // Arrange
        var bimap = new BiMap<string, int>
        {
            ["a"] = 1,
            ["b"] = 2,
            ["c"] = 3,
        };

        // Act
        var right = new List<int>(bimap.Right);

        // Assert
        Assert.Equal(3, right.Count);
        Assert.Contains(1, right);
        Assert.Contains(2, right);
        Assert.Contains(3, right);
    }

    #endregion

    #region Indexer

    [Fact]
    public void Indexer_LeftKeyGetter_ReturnsValue()
    {
        // Arrange
        var bimap = new BiMap<string, int>
        {
            ["key"] = 42,
        };

        // Act
        var result = bimap["key"];

        // Assert
        Assert.Equal(42, result);
    }

    [Fact]
    public void Indexer_LeftKeySetter_AddsEntry()
    {
        // Arrange
        var bimap = new BiMap<string, int>();

        // Act
        bimap["key"] = 10;

        // Assert
        Assert.Equal(1, bimap.Count);
        Assert.Equal(10, bimap["key"]);
        Assert.Equal("key", bimap[10]);
    }

    [Fact]
    public void Indexer_RightKeyGetter_ReturnsKey()
    {
        // Arrange
        var bimap = new BiMap<string, int>
        {
            ["name"] = 99,
        };

        // Act
        var result = bimap[99];

        // Assert
        Assert.Equal("name", result);
    }

    [Fact]
    public void Indexer_RightKeySetter_AddsEntry()
    {
        // Arrange
        var bimap = new BiMap<string, int>();

        // Act
        bimap[50] = "value";

        // Assert
        Assert.Equal(1, bimap.Count);
        Assert.Equal("value", bimap[50]);
        Assert.Equal(50, bimap["value"]);
    }

    #endregion

    #region Add

    [Fact]
    public void Add_LeftFirst_AddsEntryBidirectionally()
    {
        // Arrange
        var bimap = new BiMap<string, int>();

        // Act
        bimap.Add("left", 1);

        // Assert
        Assert.Equal(1, bimap.Count);
        Assert.Equal(1, bimap["left"]);
        Assert.Equal("left", bimap[1]);
    }

    [Fact]
    public void Add_RightFirst_AddsEntryBidirectionally()
    {
        // Arrange
        var bimap = new BiMap<string, int>();

        // Act
        bimap.Add(1, "left");

        // Assert
        Assert.Equal(1, bimap.Count);
        Assert.Equal(1, bimap["left"]);
        Assert.Equal("left", bimap[1]);
    }

    #endregion

    #region Remove

    [Fact]
    public void Remove_LeftKey_RemovesBidirectionally()
    {
        // Arrange
        var bimap = new BiMap<string, int>
        {
            ["a"] = 1,
            ["b"] = 2,
        };

        // Act
        var removed = bimap.Remove("a");

        // Assert
        Assert.True(removed);
        Assert.Equal(1, bimap.Count);
        Assert.DoesNotContain("a", bimap.Left);
        Assert.DoesNotContain(1, bimap.Right);
    }

    [Fact]
    public void Remove_RightKey_RemovesBidirectionally()
    {
        // Arrange
        var bimap = new BiMap<string, int>
        {
            ["a"] = 1,
            ["b"] = 2,
        };

        // Act
        var removed = bimap.Remove(1);

        // Assert
        Assert.True(removed);
        Assert.Equal(1, bimap.Count);
        Assert.DoesNotContain("a", bimap.Left);
        Assert.DoesNotContain(1, bimap.Right);
    }

    [Fact]
    public void Remove_NotFoundKey_Left_ReturnsFalse()
    {
        // Arrange
        var bimap = new BiMap<string, int>
        {
            ["a"] = 1,
        };

        // Act
        var removed = bimap.Remove("missing");

        // Assert
        Assert.False(removed);
        Assert.Equal(1, bimap.Count);
    }

    [Fact]
    public void Remove_NotFoundKey_Right_ReturnsFalse()
    {
        // Arrange
        var bimap = new BiMap<string, int>
        {
            ["a"] = 1,
        };

        // Act
        var removed = bimap.Remove(99);

        // Assert
        Assert.False(removed);
        Assert.Equal(1, bimap.Count);
    }

    #endregion

    #region TryGetEntry

    [Fact]
    public void TryGetEntry_FoundKey_ReturnsTrueWithRightValue()
    {
        // Arrange
        var bimap = new BiMap<string, int>
        {
            ["key"] = 42,
        };

        // Act
        var found = bimap.TryGetEntry("key", out var right);

        // Assert
        Assert.True(found);
        Assert.Equal(42, right);
    }

    [Fact]
    public void TryGetEntry_NotFoundKey_ReturnsFalseWithDefault()
    {
        // Arrange
        var bimap = new BiMap<string, int>();

        // Act
        var found = bimap.TryGetEntry("missing", out var right);

        // Assert
        Assert.False(found);
        Assert.Equal(default(int), right);
    }

    #endregion

    #region TryGetValue

    [Fact]
    public void TryGetValue_FoundKey_ReturnsTrueWithLeftValue()
    {
        // Arrange
        var bimap = new BiMap<string, int>
        {
            ["key"] = 42,
        };

        // Act
        var found = bimap.TryGetValue(42, out var left);

        // Assert
        Assert.True(found);
        Assert.Equal("key", left);
    }

    [Fact]
    public void TryGetValue_NotFoundKey_ReturnsFalseWithDefault()
    {
        // Arrange
        var bimap = new BiMap<string, int>();

        // Act
        var found = bimap.TryGetValue(99, out var left);

        // Assert
        Assert.False(found);
        Assert.Equal(default(string), left);
    }

    #endregion

    #region Contains

    [Fact]
    public void Contains_LeftKey_Existing_ReturnsTrue()
    {
        // Arrange
        var bimap = new BiMap<string, int>
        {
            ["key"] = 1,
        };

        // Act
        var result = bimap.Contains("key");

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Contains_LeftKey_NotExisting_ReturnsFalse()
    {
        // Arrange
        var bimap = new BiMap<string, int>();

        // Act
        var result = bimap.Contains("missing");

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Contains_RightKey_Existing_ReturnsTrue()
    {
        // Arrange
        var bimap = new BiMap<string, int>
        {
            ["key"] = 1,
        };

        // Act
        var result = bimap.Contains(1);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Contains_RightKey_NotExisting_ReturnsFalse()
    {
        // Arrange
        var bimap = new BiMap<string, int>();

        // Act
        var result = bimap.Contains(99);

        // Assert
        Assert.False(result);
    }

    #endregion

    #region Clear

    [Fact]
    public void Clear_EmptyBiMap_DoesNothing()
    {
        // Arrange
        var bimap = new BiMap<string, int>();

        // Act
        bimap.Clear();

        // Assert
        Assert.Equal(0, bimap.Count);
    }

    [Fact]
    public void Clear_PopulatedBiMap_EmptiesAllEntries()
    {
        // Arrange
        var bimap = new BiMap<string, int>
        {
            ["a"] = 1,
            ["b"] = 2,
            ["c"] = 3,
        };

        // Act
        bimap.Clear();

        // Assert
        Assert.Equal(0, bimap.Count);
        Assert.Empty(bimap.Left);
        Assert.Empty(bimap.Right);
    }

    #endregion

    #region GetEnumerator

    [Fact]
    public void GetEnumerator_ReturnsAllEntries()
    {
        // Arrange
        var bimap = new BiMap<string, int>
        {
            ["a"] = 1,
            ["b"] = 2,
            ["c"] = 3,
        };

        // Act
        var entries = bimap.ToList();

        // Assert
        Assert.Equal(3, entries.Count);
        Assert.Contains(entries, e => e.Left == "a" && e.Right == 1);
        Assert.Contains(entries, e => e.Left == "b" && e.Right == 2);
        Assert.Contains(entries, e => e.Left == "c" && e.Right == 3);
    }

    [Fact]
    public void GetEnumerator_EmptyBiMap_ReturnsEmpty()
    {
        // Arrange
        var bimap = new BiMap<string, int>();

        // Act
        var entries = bimap.ToList();

        // Assert
        Assert.Empty(entries);
    }

    #endregion

    #region AddRangeConstructor

    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    public void AddRangeConstructor_WithMultipleEntries_PopulatesCorrectly(int count)
    {
        // Arrange
        var entries = new MapEntry<string, int>[count];
        for (int i = 0; i < count; i++)
        {
            entries[i] = new MapEntry<string, int>($"key{i}", i);
        }

        // Act
        var bimap = new BiMap<string, int>(entries);

        // Assert
        Assert.Equal(count, bimap.Count);
        for (int i = 0; i < count; i++)
        {
            Assert.Equal(i, bimap[$"key{i}"]);
            Assert.Equal($"key{i}", bimap[i]);
        }
    }

    #endregion
}
