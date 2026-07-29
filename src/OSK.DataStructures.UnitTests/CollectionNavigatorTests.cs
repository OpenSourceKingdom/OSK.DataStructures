using System.Collections.Generic;

namespace OSK.DataStructures.UnitTests;

public class CollectionNavigatorTests
{
    #region Variables

    private static readonly IReadOnlyList<string> _items = ["a", "b", "c", "d", "e"];

    #endregion

    #region Constructors

    [Fact]
    public void Constructor_WithValidItems_SetsCountCorrectly()
    {
        // Arrange
        var items = new[] { "x", "y", "z" };

        // Act
        var navigator = new CollectionNavigator<string>(items);

        // Assert
        Assert.Equal(3, navigator.Count);
    }

    [Fact]
    public void Constructor_WithNullItems_ThrowsArgumentNullException()
    {
        // Arrange
        IEnumerable<string> items = null!;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new CollectionNavigator<string>(items));
    }

    [Fact]
    public void Constructor_DefaultWrapNavigation_SetsWrapNavigationToFalse()
    {
        // Arrange
        var items = new[] { "a", "b" };

        // Act
        var navigator = new CollectionNavigator<string>(items);

        // Assert
        Assert.False(navigator.WrapNavigation);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void Constructor_WithWrapNavigation_SetsWrapNavigationCorrectly(bool wrap)
    {
        // Arrange
        var items = new[] { "a", "b" };

        // Act
        var navigator = new CollectionNavigator<string>(items, wrap);

        // Assert
        Assert.Equal(wrap, navigator.WrapNavigation);
        Assert.Equal(2, navigator.Count);
        Assert.Equal("a", navigator.Current);
        Assert.Equal(0, navigator.CurrentIndex);
        Assert.Equal(wrap, navigator.HasPrevious);
        Assert.True(navigator.HasNext);
    }

    #endregion

    #region Count

    [Fact]
    public void Count_EmptyCollection_ReturnsZero()
    {
        // Arrange
        var navigator = new CollectionNavigator<string>(Array.Empty<string>());

        // Act & Assert
        Assert.Equal(0, navigator.Count);
    }

    [Fact]
    public void Count_PopulatedCollection_ReturnsItemCount()
    {
        // Arrange
        var items = new[] { "a", "b", "c" };

        // Act
        var navigator = new CollectionNavigator<string>(items);

        // Assert
        Assert.Equal(3, navigator.Count);
    }

    #endregion

    #region Current

    [Fact]
    public void Current_IndexZero_ReturnsFirstItem()
    {
        // Arrange
        var items = new[] { "first", "second", "third" };

        // Act
        var navigator = new CollectionNavigator<string>(items);

        // Assert
        Assert.Equal("first", navigator.Current);
    }

    [Fact]
    public void Current_AfterNext_ReturnsNextItem()
    {
        // Arrange
        var items = new[] { "a", "b", "c" };

        // Act
        var navigator = new CollectionNavigator<string>(items);
        navigator.Next();

        // Assert
        Assert.Equal("b", navigator.Current);
    }

    [Fact]
    public void Current_AfterPrevious_ReturnsPreviousItem()
    {
        // Arrange
        var items = new[] { "a", "b", "c" };

        // Act
        var navigator = new CollectionNavigator<string>(items);
        navigator.Next();
        navigator.Next();
        navigator.Previous();

        // Assert
        Assert.Equal("b", navigator.Current);
    }

    [Fact]
    public void Current_AfterNavigateToIndex_ReturnsItemAtIndex()
    {
        // Arrange
        var items = new[] { "a", "b", "c", "d" };

        // Act
        var navigator = new CollectionNavigator<string>(items);
        navigator.TryNavigate(2);

        // Assert
        Assert.Equal("c", navigator.Current);
    }

    #endregion

    #region CurrentIndex

    [Fact]
    public void CurrentIndex_InitialValue_IsZero()
    {
        // Arrange
        var navigator = new CollectionNavigator<string>(_items);

        // Act & Assert
        Assert.Equal(0, navigator.CurrentIndex);
    }

    [Fact]
    public void CurrentIndex_AfterNext_IncrementsByOne()
    {
        // Arrange
        var navigator = new CollectionNavigator<string>(_items);

        // Act
        navigator.Next();

        // Assert
        Assert.Equal(1, navigator.CurrentIndex);
    }

    [Fact]
    public void CurrentIndex_AfterPrevious_DecrementsByOne()
    {
        // Arrange
        var navigator = new CollectionNavigator<string>(_items);
        navigator.Next();
        navigator.Next();

        // Act
        navigator.Previous();

        // Assert
        Assert.Equal(1, navigator.CurrentIndex);
    }

    [Fact]
    public void CurrentIndex_AfterNavigateToIndex_SetsToProvidedIndex()
    {
        // Arrange
        var navigator = new CollectionNavigator<string>(_items);

        // Act
        navigator.TryNavigate(3);

        // Assert
        Assert.Equal(3, navigator.CurrentIndex);
    }

    #endregion

    #region HasNext

    [Fact]
    public void HasNext_AtStart_ReturnsTrue()
    {
        // Arrange
        var navigator = new CollectionNavigator<string>(_items);

        // Act & Assert
        Assert.True(navigator.HasNext);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void HasNext_AtEnd_ReturnsExpected(bool wrap)
    {
        // Arrange
        var navigator = new CollectionNavigator<string>(_items, wrap);
        for (int i = 0; i < _items.Count; i++)
        {
            navigator.Next();
        }

        // Act & Assert
        if (wrap)
        {
            Assert.True(navigator.HasNext);
        }
        else
        {
            Assert.False(navigator.HasNext);
        }
    }

    #endregion

    #region HasPrevious

    [Fact]
    public void HasPrevious_AtStart_ReturnsFalse()
    {
        // Arrange
        var navigator = new CollectionNavigator<string>(_items);

        // Act & Assert
        Assert.False(navigator.HasPrevious);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void HasPrevious_AtStart_ReturnsExpected(bool wrap)
    {
        // Arrange
        var navigator = new CollectionNavigator<string>(_items, wrap);

        // Act & Assert
        if (wrap)
        {
            Assert.True(navigator.HasPrevious);
        }
        else
        {
            Assert.False(navigator.HasPrevious);
        }
    }

    #endregion

    #region Next

    [Fact]
    public void Next_FromStart_AdvancesToSecondItem()
    {
        // Arrange
        var navigator = new CollectionNavigator<string>(_items);

        // Act
        var result = navigator.Next();

        // Assert
        Assert.True(result);
        Assert.Equal("b", navigator.Current);
        Assert.Equal(1, navigator.CurrentIndex);
    }

    [Fact]
    public void Next_FromMiddle_AdvancesCorrectly()
    {
        // Arrange
        var navigator = new CollectionNavigator<string>(_items);
        navigator.TryNavigate(2);

        // Act
        var result = navigator.Next();

        // Assert
        Assert.True(result);
        Assert.Equal("d", navigator.Current);
        Assert.Equal(3, navigator.CurrentIndex);
    }

    [Fact]
    public void Next_AtEndWithoutWrap_ReturnsFalseAndStaysInPlace()
    {
        // Arrange
        var navigator = new CollectionNavigator<string>(_items, false);
        for (int i = 0; i < _items.Count; i++)
        {
            navigator.Next();
        }

        // Act
        var result = navigator.Next();

        // Assert
        Assert.False(result);
        Assert.Equal(_items[_items.Count - 1], navigator.Current);
    }

    [Fact]
    public void Next_AtEndWithWrap_WrapsToBeginning()
    {
        // Arrange
        var navigator = new CollectionNavigator<string>(_items, true);
        for (int i = 0; i < _items.Count - 1; i++)
        {
            navigator.Next();
        }

        // Act
        var result = navigator.Next();

        // Assert
        Assert.True(result);
        Assert.Equal("a", navigator.Current);
        Assert.Equal(0, navigator.CurrentIndex);
    }

    [Fact]
    public void Next_SingleElementCollection_StaysInPlaceWithWrap()
    {
        // Arrange
        var navigator = new CollectionNavigator<string>(["only"], true);

        // Act
        var result = navigator.Next();

        // Assert
        Assert.True(result);
        Assert.Equal("only", navigator.Current);
        Assert.Equal(0, navigator.CurrentIndex);
    }

    [Fact]
    public void Next_SingleElementCollectionWithoutWrap_ReturnsFalse()
    {
        // Arrange
        var navigator = new CollectionNavigator<string>(["only"], false);

        // Act
        var result = navigator.Next();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Next_EmptyCollection_ReturnsFalse()
    {
        // Arrange
        var navigator = new CollectionNavigator<string>(Array.Empty<string>());

        // Act
        var result = navigator.Next();

        // Assert
        Assert.False(result);
    }

    #endregion

    #region Previous

    [Fact]
    public void Previous_FromEnd_RetreatsToSecondLastItem()
    {
        // Arrange
        var navigator = new CollectionNavigator<string>(_items);
        for (int i = 0; i < _items.Count - 1; i++)
        {
            navigator.Next();
        }

        // Act
        var result = navigator.Previous();

        // Assert
        Assert.True(result);
        Assert.Equal(_items[_items.Count - 2], navigator.Current);
    }

    [Fact]
    public void Previous_FromMiddle_RetreatsCorrectly()
    {
        // Arrange
        var navigator = new CollectionNavigator<string>(_items);
        navigator.TryNavigate(3);

        // Act
        var result = navigator.Previous();

        // Assert
        Assert.True(result);
        Assert.Equal("c", navigator.Current);
        Assert.Equal(2, navigator.CurrentIndex);
    }

    [Fact]
    public void Previous_AtStartWithoutWrap_ReturnsFalseAndStaysInPlace()
    {
        // Arrange
        var navigator = new CollectionNavigator<string>(_items, false);

        // Act
        var result = navigator.Previous();

        // Assert
        Assert.False(result);
        Assert.Equal("a", navigator.Current);
    }

    [Fact]
    public void Previous_AtStartWithWrap_WrapsToEnd()
    {
        // Arrange
        var navigator = new CollectionNavigator<string>(_items, true);

        // Act
        var result = navigator.Previous();

        // Assert
        Assert.True(result);
        Assert.Equal("e", navigator.Current);
        Assert.Equal(4, navigator.CurrentIndex);
    }

    [Fact]
    public void Previous_SingleElementCollection_StaysInPlaceWithWrap()
    {
        // Arrange
        var navigator = new CollectionNavigator<string>(["only"], true);

        // Act
        var result = navigator.Previous();

        // Assert
        Assert.True(result);
        Assert.Equal("only", navigator.Current);
        Assert.Equal(0, navigator.CurrentIndex);
    }

    [Fact]
    public void Previous_SingleElementCollectionWithoutWrap_ReturnsFalse()
    {
        // Arrange
        var navigator = new CollectionNavigator<string>(["only"], false);

        // Act
        var result = navigator.Previous();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Previous_EmptyCollection_ReturnsFalse()
    {
        // Arrange
        var navigator = new CollectionNavigator<string>(Array.Empty<string>());

        // Act
        var result = navigator.Previous();

        // Assert
        Assert.False(result);
    }

    #endregion

    #region TryNavigate

    [Theory]
    [InlineData(0)]
    [InlineData(2)]
    [InlineData(4)]
    public void TryNavigate_ValidIndex_NavigatesSuccessfully(int index)
    {
        // Arrange
        var navigator = new CollectionNavigator<string>(_items);

        // Act
        var result = navigator.TryNavigate(index);

        // Assert
        Assert.True(result);
        Assert.Equal(_items[index], navigator.Current);
        Assert.Equal(index, navigator.CurrentIndex);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-5)]
    public void TryNavigate_NegativeIndex_ReturnsFalse(int index)
    {
        // Arrange
        var navigator = new CollectionNavigator<string>(_items);

        // Act
        var result = navigator.TryNavigate(index);

        // Assert
        Assert.False(result);
    }

    [Theory]
    [InlineData(5)]
    [InlineData(10)]
    public void TryNavigate_OutOfBoundsIndex_ReturnsFalse(int index)
    {
        // Arrange
        var navigator = new CollectionNavigator<string>(_items);

        // Act
        var result = navigator.TryNavigate(index);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void TryNavigate_EmptyCollection_ReturnsFalse()
    {
        // Arrange
        var navigator = new CollectionNavigator<string>(Array.Empty<string>());

        // Act
        var result = navigator.TryNavigate(0);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void TryNavigate_ZeroIndex_NavigatesToStart()
    {
        // Arrange
        var navigator = new CollectionNavigator<string>(_items);
        for (int i = 0; i < _items.Count; i++)
        {
            navigator.Next();
        }

        // Act
        navigator.TryNavigate(0);

        // Assert
        Assert.Equal("a", navigator.Current);
        Assert.Equal(0, navigator.CurrentIndex);
    }

    #endregion

    #region WrapNavigation

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void WrapNavigation_Getter_ReturnsConstructorValue(bool wrap)
    {
        // Arrange
        var items = new[] { "a", "b" };

        // Act
        var navigator = new CollectionNavigator<string>(items, wrap);

        // Assert
        Assert.Equal(wrap, navigator.WrapNavigation);
    }

    #endregion
}
