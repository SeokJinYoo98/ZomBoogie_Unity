using System;

namespace Common.Interface
{
    public interface IRotateable
    {
        void Rotate(float angle);

    }
    public interface IFlipable
    {
        void FlipX(bool flip);
        void FlipY(bool flip);
    }
}
