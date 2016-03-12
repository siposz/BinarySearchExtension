namespace BinarySearchExtension
{
    internal class RangeIndex
    {
        public int FromIndex { get; private set; }
        public int ToIndex { get; private set; }
        public int Length { get { return ToIndex - FromIndex + 1; } }

        public static RangeIndex CreateEmpty()
        {
            return new RangeIndex(-1, -2);
        }

        public RangeIndex(int fromIndex, int toIndex)
        {
            FromIndex = fromIndex;
            ToIndex = toIndex;
        }
    }
}
