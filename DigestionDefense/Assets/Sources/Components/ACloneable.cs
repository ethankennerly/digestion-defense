using System;

namespace FineGameDesign.Entitas
{
    public abstract class ACloneable : ICloneable
    {
        public virtual object Clone()
        {
            return MemberwiseClone();
        }
    }
}
