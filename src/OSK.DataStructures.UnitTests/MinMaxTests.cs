using System;
using System.Collections.Generic;
using System.Text;

namespace OSK.DataStructures.UnitTests;

public class MinMaxTests
{
    #region Constructor

    [Fact]
    public void Constructor_SetsMinAndMax()
    {
        var r = new MinMax<int>(1, 5);

        Assert.Equal(1, r.Min);
        Assert.Equal(5, r.Max);
    }

    [Fact]
    public void Constructor_AllowsEqualBounds()
    {
        var r = new MinMax<int>(3, 3);

        Assert.Equal(3, r.Min);
        Assert.Equal(3, r.Max);
    }

    [Fact]
    public void Constructor_SingleParam_CreatesDegenerateRange()
    {
        var r = new MinMax<int>(7);

        Assert.Equal(7, r.Min);
        Assert.Equal(7, r.Max);
    }

    [Fact]
    public void Constructor_ThrowsWhenMaxLessThanMin()
    {
        Assert.Throws<ArgumentException>(() => new MinMax<int>(5, 1));
    }

    #endregion

    #region Clamp

    [Theory]
    [InlineData(-5, 0)]   // below range -> Min
    [InlineData(0, 0)]    // at Min
    [InlineData(5, 5)]    // inside
    [InlineData(10, 10)]  // at Max
    [InlineData(15, 10)]  // above range -> Max
    public void Clamp_ReturnsValueWithinBounds(int input, int expected)
    {
        var r = new MinMax<int>(0, 10);

        Assert.Equal(expected, r.Clamp(input));
    }

    #endregion

    #region Contains

    [Theory]
    [InlineData(-1, false)]
    [InlineData(0, true)]
    [InlineData(5, true)]
    [InlineData(10, true)]
    [InlineData(11, false)]
    public void Contains_IsInclusiveOfBounds(int input, bool expected)
    {
        var r = new MinMax<int>(0, 10);

        Assert.Equal(expected, r.Contains(input));
    }

    #endregion

    #region Equality

    [Fact]
    public void Equality_IsByValue()
    {
        var a = new MinMax<int>(1, 5);
        var b = new MinMax<int>(1, 5);
        var c = new MinMax<int>(1, 6);

        Assert.Equal(a, b);
        Assert.True(a == b);
        Assert.NotEqual(a, c);
        Assert.True(a != c);
        Assert.Equal(a.GetHashCode(), b.GetHashCode());
    }

    #endregion
}