namespace GameProject.Characters
{
    /// <summary>
    /// 캐릭터의 기본 동작을 정의하는 인터페이스입니다.
    /// </summary>
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
}
