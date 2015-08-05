namespace WebApi136.Areas.HelpPage.SampleGeneration
{
    using System;

    public class ImageSample
    {
        public ImageSample(string src)
        {
            if (src == null)
            {
                throw new ArgumentNullException("src");
            }

            this.Src = src;
        }

        public string Src { get; private set; }

        public override bool Equals(object obj)
        {
            var other = obj as ImageSample;
            return other != null && this.Src == other.Src;
        }

        public override int GetHashCode()
        {
            return this.Src.GetHashCode();
        }

        public override string ToString()
        {
            return this.Src;
        }
    }
}