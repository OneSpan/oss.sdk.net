using System;

namespace OneSpanSign.Sdk.Builder
{
    /// <summary>
    /// ClickableAreaBuilder is a convenient class used to define a field's clickable area.
    /// </summary>
    public class ClickableAreaBuilder
    {
        private Nullable<Double> width;
        private Nullable<Double> height;
        private Nullable<ClickableAreaAlignment> alignment;

        private ClickableAreaBuilder()
        {
        }

        /// <summary>
        /// Creates a clickable area builder.
        /// </summary>
        /// <returns>a clickable area builder</returns>
        public static ClickableAreaBuilder NewClickableArea()
        {
            return new ClickableAreaBuilder();
        }

        /// <summary>
        /// Sets the size, in pixel, of the clickable area.
        /// </summary>
        /// <param name="width">the width of the clickable area min="0"</param>
        /// <param name="height">the height of the clickable area min="0"</param>
        /// <returns>the clickable area builder itself</returns>
        public ClickableAreaBuilder WithSize(double width, double height)
        {
            if (width < 0 || height < 0)
            {
                throw new BuilderException("Clickable area width and height must not be negative.");
            }
            this.width = width;
            this.height = height;
            return this;
        }

        /// <summary>
        /// Sets the anchor position of the field's mark within the clickable area.
        /// </summary>
        /// <param name="alignment">the alignment</param>
        /// <returns>the clickable area builder itself</returns>
        public ClickableAreaBuilder WithAlignment(ClickableAreaAlignment alignment)
        {
            this.alignment = alignment;
            return this;
        }

        /// <summary>
        /// Builds the actual ClickableArea with the values specified.
        /// </summary>
        /// <returns>the built ClickableArea</returns>
        public ClickableArea Build()
        {
            ClickableArea result = new ClickableArea();
            result.Width = width;
            result.Height = height;
            result.Alignment = alignment;
            return result;
        }
    }
}
