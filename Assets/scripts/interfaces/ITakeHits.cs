public interface ITakeHits
{
    void TakeHit(IDamage hitBy);
    bool Alive { get; }
}
