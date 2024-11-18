public interface ICharacter
{
    int HP { get; set; }
    float Speed { get; set; }
    void Move();
    void Idle();
    void Attack();
    void TakeDamage(int damage);
    bool IsAlive { get; }
}
