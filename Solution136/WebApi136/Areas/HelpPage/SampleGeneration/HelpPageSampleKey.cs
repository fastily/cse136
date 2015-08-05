namespace WebApi136.Areas.HelpPage.SampleGeneration
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Net.Http.Headers;

    public class HelpPageSampleKey
    {
        public HelpPageSampleKey(MediaTypeHeaderValue mediaType, Type type)
        {
            if (mediaType == null)
            {
                throw new ArgumentNullException("mediaType");
            }

            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            this.ControllerName = string.Empty;
            this.ActionName = string.Empty;
            this.ParameterNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            this.ParameterType = type;
            this.MediaType = mediaType;
        }

        public HelpPageSampleKey(SampleDirection sampleDirection, string controllerName, string actionName, IEnumerable<string> parameterNames)
        {
            if (!Enum.IsDefined(typeof(SampleDirection), sampleDirection))
            {
                throw new InvalidEnumArgumentException("sampleDirection", (int)sampleDirection, typeof(SampleDirection));
            }

            if (controllerName == null)
            {
                throw new ArgumentNullException("controllerName");
            }

            if (actionName == null)
            {
                throw new ArgumentNullException("actionName");
            }

            if (parameterNames == null)
            {
                throw new ArgumentNullException("parameterNames");
            }

            this.ControllerName = controllerName;
            this.ActionName = actionName;
            this.ParameterNames = new HashSet<string>(parameterNames, StringComparer.OrdinalIgnoreCase);
            this.SampleDirection = sampleDirection;
        }

        public HelpPageSampleKey(MediaTypeHeaderValue mediaType, SampleDirection sampleDirection, string controllerName, string actionName, IEnumerable<string> parameterNames)
        {
            if (mediaType == null)
            {
                throw new ArgumentNullException("mediaType");
            }

            if (!Enum.IsDefined(typeof(SampleDirection), sampleDirection))
            {
                throw new InvalidEnumArgumentException("sampleDirection", (int)sampleDirection, typeof(SampleDirection));
            }

            if (controllerName == null)
            {
                throw new ArgumentNullException("controllerName");
            }

            if (actionName == null)
            {
                throw new ArgumentNullException("actionName");
            }

            if (parameterNames == null)
            {
                throw new ArgumentNullException("parameterNames");
            }

            this.ControllerName = controllerName;
            this.ActionName = actionName;
            this.MediaType = mediaType;
            this.ParameterNames = new HashSet<string>(parameterNames, StringComparer.OrdinalIgnoreCase);
            this.SampleDirection = sampleDirection;
        }

        public string ControllerName { get; private set; }

        public string ActionName { get; private set; }

        public MediaTypeHeaderValue MediaType { get; private set; }

        public HashSet<string> ParameterNames { get; private set; }

        public Type ParameterType { get; private set; }

        public SampleDirection? SampleDirection { get; private set; }

        public override bool Equals(object obj)
        {
            var otherKey = obj as HelpPageSampleKey;
            if (otherKey == null)
            {
                return false;
            }

            return string.Equals(this.ControllerName, otherKey.ControllerName, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(this.ActionName, otherKey.ActionName, StringComparison.OrdinalIgnoreCase) &&
                (this.MediaType == otherKey.MediaType || (this.MediaType != null && this.MediaType.Equals(otherKey.MediaType))) &&
                this.ParameterType == otherKey.ParameterType &&
                this.SampleDirection == otherKey.SampleDirection &&
                this.ParameterNames.SetEquals(otherKey.ParameterNames);
        }

        public override int GetHashCode()
        {
            var hashCode = this.ControllerName.ToUpperInvariant().GetHashCode() ^ this.ActionName.ToUpperInvariant().GetHashCode();
            if (this.MediaType != null)
            {
                hashCode ^= this.MediaType.GetHashCode();
            }

            if (this.SampleDirection != null)
            {
                hashCode ^= this.SampleDirection.GetHashCode();
            }

            if (this.ParameterType != null)
            {
                hashCode ^= this.ParameterType.GetHashCode();
            }

            foreach (var parameterName in this.ParameterNames)
            {
                hashCode ^= parameterName.ToUpperInvariant().GetHashCode();
            }

            return hashCode;
        }
    }
}
