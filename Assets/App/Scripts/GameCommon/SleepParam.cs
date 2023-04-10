namespace App.Scripts.GameCommon
{
    public struct SleepParam
    {
        /// <summary>
        /// 1 origin
        /// </summary>
        public readonly int day;
        public readonly bool noName;
        public readonly bool isForce;
        public readonly bool stone;
        public readonly bool language;
        public readonly bool ending;
        public readonly bool trueEnd;
        public readonly string talk0;
        public readonly string talk1;
        public readonly string talk0Jp;
        public readonly string talk1Jp;
        public readonly bool talkGoodEmotion;
        public readonly bool talkBadEmotion;
        public readonly string spokenWord;
        public readonly string spokenWordJp;
        public readonly string saidWord;
        public readonly string saidWordJp;
        public readonly string additionalWord;
        public readonly string additionalWordJp;

        public SleepParam(int day, bool noName, bool isForce, bool stone, bool language, bool ending, bool trueEnd, string talk0, string talk1, string talk0Jp, string talk1Jp, bool talkGoodEmotion, bool talkBadEmotion, string spokenWord, string spokenWordJp, string saidWord, string saidWordJp, string additionalWord, string additionalWordJp)
        {
            this.day = day;
            this.noName = noName;
            this.isForce = isForce;
            this.stone = stone;
            this.language = language;
            this.ending = ending;
            this.trueEnd = trueEnd;
            this.talk0 = talk0;
            this.talk1 = talk1;
            this.talk0Jp = talk0Jp;
            this.talk1Jp = talk1Jp;
            this.talkGoodEmotion = talkGoodEmotion;
            this.talkBadEmotion = talkBadEmotion;
            this.spokenWord = spokenWord;
            this.spokenWordJp = spokenWordJp;
            this.saidWord = saidWord;
            this.saidWordJp = saidWordJp;
            this.additionalWord = additionalWord;
            this.additionalWordJp = additionalWordJp;
        }
    }
}