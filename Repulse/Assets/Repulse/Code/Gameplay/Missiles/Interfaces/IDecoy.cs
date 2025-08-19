using System;

public interface IDecoy : IExplosion
{
    public event Action OnDecoyExploded;

    public void DecoyExplosiveAttack();
}
