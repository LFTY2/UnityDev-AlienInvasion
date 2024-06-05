using System;

namespace _PROJECT.Scripts
{
    public interface IRadius
    {
        public event Action<float> OnRadiusChange;
        public float Radius { get; }
    }
}