namespace QFramework.Example
{
    interface IEnemy
    {
        public void Hurt(float damage);
        void SetHpScale(float hPScale);
        void SetSpeedScale(float speedScale);
    }
}