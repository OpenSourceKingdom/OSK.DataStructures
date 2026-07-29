namespace OSK.DataStructures;

public readonly record struct MapEntry<TLeft, TRight>(TLeft Left, TRight Right)
{
}
