namespace WebApi136.Areas.HelpPage.SampleGeneration
{
    using System;

    public class TextSample
    {
        public TextSample(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException("text");
            }

            this.Text = text;
        }

        public string Text { get; private set; }

        public override bool Equals(object obj)
        {
            var other = obj as TextSample;
            return other != null && this.Text == other.Text;
        }

        public override int GetHashCode()
        {
            return this.Text.GetHashCode();
        }

        public override string ToString()
        {
            return this.Text;
        }
    }
}