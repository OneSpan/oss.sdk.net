namespace OneSpanSign.Sdk.Builder
{
    public class HandoverBuilder
    {
        private string language;
        private string href;
        private string text;
        private string title;

        private HandoverBuilder(string language)
        {
            this.language = language;
        }

        public static HandoverBuilder NewHandover(string language)
        {
            if (language == null)
            {
                throw new BuilderException("Language cannot be null.");
            }

            return new HandoverBuilder(language);
        }

        public HandoverBuilder WithHref(string href)
        {
            this.href = href;
            return this;
        }

        public HandoverBuilder WithText(string text)
        {
            this.text = text;
            return this;
        }

        public HandoverBuilder WithTitle(string title)
        {
            this.title = title;
            return this;
        }

        public Handover Build()
        {
            Handover handover = new Handover();
            handover.Language = language;
            handover.Href = href;
            handover.Text = text;
            handover.Title = title;
            return handover;
        }
    }
}