namespace MarkLight
{
    /// <summary>
    /// Enum indicating if size should be adjusted to text.
    /// </summary>
    public enum AdjustToText
    {
        /// <summary>
        /// Neither width nor height should be adjusted to text.
        /// </summary>
        None = 0,

        /// <summary>
        /// Adjust width to text.
        /// </summary>
        Width = 1,

        /// <summary>
        /// Adjust height to text.
        /// </summary>
        Height = 2,

        /// <summary>
        /// Adjust width and height to text.
        /// </summary>
        WidthAndHeight = 3
    }
}
