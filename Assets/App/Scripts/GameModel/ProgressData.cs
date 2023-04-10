using yydlib;

namespace App.Scripts.GameModel
{
    public interface IProgressDataReadOnly
    {
        public int Day { get; }
    }
    
    public class ProgressData : IProgressDataReadOnly
    {
        /// <summary>
        /// 0 origin
        /// </summary>
        public int Day { get; private set; }
        
        public bool IsDragonNoName { get; private set; }

        public int Money { get; private set; }
        
        public bool IsWithJapanese { get; private set; }

        public ProgressData()
        {
            Reset(false);
        }

        public void Sleep()
        {
            Day += 1;
            if(IsDragonNoName)
            {
                // 2日目に名前を付ける
                IsDragonNoName = false;
            }
        }
        
        public void Reset(bool isWithJapanese)
        {
            Day = 0;
            IsDragonNoName = true;
            Money = AppUtil.GetMiscParams().DefaultMoney;
            IsWithJapanese = isWithJapanese;
        }

        public void DecreaseMoney(int num)
        {
            Money -= num;
            GameUtil.Assert(Money >= 0);
        }
#if DEBUG
        public void Debug_PrepareEnd1()
        {
            IsDragonNoName = true;
            Day = 2;
        }
        public void Debug_PrepareEnd2()
        {
            IsDragonNoName = true;
            Day = 3;
        }
        public void Debug_PrepareEnd3()
        {
            IsDragonNoName = true;
            Day = 3;
        }
#endif
    }
}