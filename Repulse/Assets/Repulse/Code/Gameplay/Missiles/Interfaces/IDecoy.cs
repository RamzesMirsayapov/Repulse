using System;

public interface IDecoy
{
    public event Action OnDecoyExploded;

    public void DecoyExplosiveAttack();
}