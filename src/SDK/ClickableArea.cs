using System;

namespace OneSpanSign.Sdk
{
    public class ClickableArea
    {
        public Nullable<Double> Width
        {
            get;
            set;
        }

        public Nullable<Double> Height
        {
            get;
            set;
        }

        public Nullable<ClickableAreaAlignment> Alignment
        {
            get;
            set;
        }
    }
}
