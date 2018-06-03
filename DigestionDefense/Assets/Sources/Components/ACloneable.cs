using System;

namespace Finegamedesign.Entitas
{
    public abstract class ACloneable : ICloneable
    {
        public virtual object Clone()
        {
            return MemberwiseClone();
        }
    }
}
