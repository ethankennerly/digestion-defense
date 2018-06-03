using System;

public abstract class ACloneable : ICloneable
{
    public virtual object Clone()
    {
        return MemberwiseClone();
    }
}
