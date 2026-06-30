namespace QFramework.Example
{
    public interface IEnemy
    {
        public void Hurt(float damage,bool force = false,bool critical = false);
        void SetHpScale(float hPScale);
        void SetSpeedScale(float speedScale);
    }
}