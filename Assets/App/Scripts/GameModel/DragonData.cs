using System;
using System.Collections.Generic;
using System.Linq;
using App.Scripts.AppDebug;
using App.Scripts.FixedData;
using App.Scripts.GameCommon;
using UniRx;
using UnityEngine;
using yydlib;

namespace App.Scripts.GameModel
{
    public interface IDragonDataReadOnly
    {
        public int Energy { get; }
        
        public int Toilet { get; }
        
        public int Hungry { get; }
        
        public int Injury { get; }
        
        public int WannaPlay { get; }
        
        public int Like { get; }
        
        public int ToyLike { get; }
        
        public int Language { get; }
    }
    
    public class DragonData : IDragonDataReadOnly
    {
        public int Energy { get; private set; }
        
        public int Toilet { get; private set; }
        
        public int Hungry { get; private set; }
        
        public int Injury { get; private set; }
        
        public int WannaPlay { get; private set; }
        
        public int Like { get; private set; }
        
        public int ToyLike { get; private set; }
        
        public int Language { get; private set; }
        
        public Word PreviousWord { get; private set; }
        
        public TalkParameter EmotionallyTalk { get; private set; }

        private List<Word> _saidWords;
        public IReadOnlyList<Word> SaidWords => _saidWords;
        
        private List<Word> _spokenWords;
        public IReadOnlyList<Word> SpokenWords => _spokenWords;

        public bool GaveStone { get; private set; }
        
        public bool WillGiveStone { get; private set; }

        private const int SafetyMaxValue = 1000;

        private readonly Subject<ItemType> _onBuyItem = new Subject<ItemType>();
        public IObservable<ItemType> OnBuyItem => this._onBuyItem;
        
        
        public DragonData()
        {
            Reset();
#if DEBUG
            DebugCommandManager.Instance.OnChangeDragonParam.Subscribe(onChangeDragonParamArg =>
            {
                int add = onChangeDragonParamArg.isUp ? 10 : -10;

                switch (onChangeDragonParamArg.paramType)
                {
                    case DragonParamType.Energy:
                        Energy = Mathf.Clamp(Energy + add, 0, SafetyMaxValue);
                        break;
                    case DragonParamType.Toilet:
                        Toilet = Mathf.Clamp(Toilet + add, 0, SafetyMaxValue);
                        break;
                    case DragonParamType.Hungry:
                        Hungry = Mathf.Clamp(Hungry + add, 0, AppUtil.GetMiscParams().MaxHungry);
                        break;
                    case DragonParamType.Injury:
                        Injury = Mathf.Clamp(Injury + add, 0, SafetyMaxValue);
                        break;
                    case DragonParamType.WannaPlay:
                        WannaPlay = Mathf.Clamp(WannaPlay + add, 0, SafetyMaxValue);
                        break;
                    case DragonParamType.Like:
                        Like = Mathf.Clamp(Like + add, 0, SafetyMaxValue);
                        break;
                    case DragonParamType.ToyLike:
                        ToyLike = Mathf.Clamp(ToyLike + add, 0, AppUtil.GetMiscParams().MaxToyLike);
                        break;
                    case DragonParamType.Language:
                        Language = Mathf.Clamp(Language + add, 0, AppUtil.GetMiscParams().MaxLanguage);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            });//AddToないけどDebug用途なので…
#endif
        }

        public void Reset()
        {
            Energy = AppUtil.GetMiscParams().StartEnergy;
            Toilet = 0;
            Hungry = AppUtil.GetMiscParams().StartHungry;
            Injury = AppUtil.GetMiscParams().StartInjury;
            WannaPlay = 0;
            Like = 0;
            ToyLike = 0;
            Language = 0;
            PreviousWord = Word.None;
            EmotionallyTalk = null;
            _saidWords = new List<Word>() { Word.ThankYou, Word.What, Word.Yeah, Word.Hungry };// ストーリー上必ず言う
            _spokenWords = new List<Word>();
            GaveStone = false;
            WillGiveStone = false;
        }

        public bool IsGoSleep()
        {
            if(Energy <= AppUtil.GetMiscParams().EnergyNightThreshold)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isForce"></param>
        /// <param name="day">0 origin</param>
        public void Sleep(bool isForce, int day)
        {
            Energy = AppUtil.GetMiscParams().SleepEnergy;
            if(Toilet >= AppUtil.GetMiscParams().StressToiletThreshold)
            {
                Like = Mathf.Clamp(Like - AppUtil.GetMiscParams().SleepWithToiletDecreaseLike, 0, SafetyMaxValue);
            }
            Toilet = 0;
            Hungry = Mathf.Min(Hungry + AppUtil.GetMiscParams().SleepIncreaseHungry, AppUtil.GetMiscParams().MaxHungry);
            Injury = Mathf.Max(Injury - AppUtil.GetMiscParams().SleepHealInjury, 0);
            PreviousWord = Word.None;
            if(isForce)
            {
                Like = Mathf.Clamp(Like - AppUtil.GetMiscParams().ForceSleepDecreaseLike, 0, SafetyMaxValue);
            }
            if(day == 1 && Like >= AppUtil.GetMiscParams().GiveStoneLikeThreshold)
            {
                // 石を次の日に渡す
                WillGiveStone = true;
                AddSaidWords(new List<Word> { Word.GiveYou });// 翌日に言うことが決まっているのでここで追加
            }
            if(day == 2)
            {
                // 石を渡す
                GaveStone = WillGiveStone;
                // 3日目は特別にお腹が空いている
                Hungry = AppUtil.GetMiscParams().MaxHungry;
                PreviousWord = Word.Hungry;
            }
            if(day == 3)
            {
                // エンディングに行かなかったときだけ言うが、判定が面倒なので追加しない
                //PreviousWord = Word.CanFly;
                //AddSaidWords(new List<Word>{ PreviousWord });
            }
        }
        public IReadOnlyList<Word> RefuseSleep()
        {
            Turn(AppUtil.GetMiscParams().RefuseSleepTurn);
            
            List<Word> ret = new List<Word>
            {
                AppUtil.GetRandomWord(new Word[]{Word.No, Word.None, Word.Unnecessary})
            };

            ApplySaidWord(ret);
            AddSaidWords(ret);

            return ret;
        }
        public IReadOnlyList<Word> PlaySports()
        {
            Turn(AppUtil.GetMiscParams().SportsTurn);
            
            List<Word> ret = new List<Word>();

            Like = Mathf.Clamp(Like + AppUtil.GetMiscParams().SportsIncreaseLike, 0, SafetyMaxValue);
            if (Injury > AppUtil.GetMiscParams().CanPlayInjury)
            {
                Like = Mathf.Clamp(Like - AppUtil.GetMiscParams().SportsWithInjuryDecreaseLike, 0, SafetyMaxValue);
                Injury = Mathf.Clamp(Injury + AppUtil.GetMiscParams().SportsWithInjuryIncreaseInjury, 0, SafetyMaxValue);
                ret.Add(AppUtil.GetRandomWord(new Word[]{Word.Tired, Word.DontLike, Word.DontLike}));
            }
            else
            {
                ret.Add(AppUtil.GetRandomWord(new Word[]{Word.Yeah, Word.ThankYou, Word.ThankYou}));
            }

            WannaPlay = 0;

            ApplySaidWord(ret);
            AddSaidWords(ret);
            
            return ret;
        }
        
        public bool IsGoToilet()
        {
            if(Toilet < AppUtil.GetMiscParams().ToiletGoThreshold)
            {
                return false;
            }
            return true;
        }

        public IReadOnlyList<Word> GoToilet()
        {
            Turn(AppUtil.GetMiscParams().GoToiletTurn);
            
            List<Word> ret = new List<Word>();
            if(Toilet >= AppUtil.GetMiscParams().ToiletLikeThreshold)
            {
                Like += AppUtil.GetMiscParams().LikeIncreaseByToilet;
                ret.Add(AppUtil.GetRandomWord(new Word[]{Word.ThankYou, Word.ThankYou, Word.Poo}));
            }
            else
            {
                ret.Add(Word.None);
            }
            Toilet = 0;

            ApplySaidWord(ret);
            AddSaidWords(ret);

            return ret;
        }
        
        public IReadOnlyList<Word> RefuseToilet()
        {
            Turn(AppUtil.GetMiscParams().RefuseToiletTurn);
            
            List<Word> ret = new List<Word>
            {
                AppUtil.GetRandomWord(new Word[]{Word.No, Word.None, Word.Unnecessary})
            };

            ApplySaidWord(ret);
            AddSaidWords(ret);

            return ret;
        }
        
        public IReadOnlyList<Word> Spoken(Word word)
        {
            Turn(AppUtil.GetMiscParams().SpokenTurn);

            List<Word> ret = new List<Word>() { Word.What };
            for (int i = 0; i < FixDataManager.Instance.TalkParameters.GetCount(); i++)
            {
                var talkData = FixDataManager.Instance.TalkParameters.GetTalkParameter(i);
                
                if(talkData.NextWord != Word.None &&
                   talkData.NextWord != word)
                {
                    // 言う言葉不一致
                    continue;
                }

                if(talkData.PreviousWord != Word.None &&
                   talkData.PreviousWord != PreviousWord)
                {
                    // 前の言葉が不一致
                    continue;
                }

                bool isConditionMatch = true;
                foreach (var conditionParameter in talkData.Conditions)
                {
                    if(!AppUtil.IsConditionTrue(conditionParameter, this))
                    {
                        isConditionMatch = false;
                        break;
                    }
                }
                if(!isConditionMatch)
                {
                    // 条件不一致
                    continue;
                }
                
                // 条件合致
                ret = talkData.Answer;
                ApplyDragonChange(talkData.DragonChangeParameter);
                // 記憶更新
                if(EmotionallyTalk == null ||
                   Mathf.Abs(EmotionallyTalk.DragonChangeParameter.Like) < Mathf.Abs(talkData.DragonChangeParameter.Like))
                {
                    if (talkData.PreviousWord != Word.None && talkData.NextWord != Word.None)
                    {
                        EmotionallyTalk = talkData;
                    }
                }
                break;
            }

            ApplySaidWord(ret);
            AddSaidWords(ret);
            AddSpokenWord(word);
            
            return ret;
        }
        
        public IReadOnlyList<Word> Listened()
        {
            Turn(AppUtil.GetMiscParams().ListenedTurn);
            
            List<Word> ret = new List<Word>() { Word.None };

            if (Toilet > AppUtil.GetMiscParams().ToiletLikeThreshold)
            {
                ret = new List<Word>
                {
                    AppUtil.GetRandomWord(new Word[]{Word.Toilet, Word.Toilet, Word.Toilet, Word.Poo})
                };
            }
            else if(Energy <= AppUtil.GetMiscParams().SleepyEnergy)
            {
                ret = new List<Word>
                {
                    AppUtil.GetRandomWord(new Word[]{Word.Sleepy, Word.Tired})
                };
            }
            else if(Hungry >= AppUtil.GetMiscParams().TellHungryThreshold)
            {
                ret = new List<Word>
                {
                    AppUtil.GetRandomWord(new Word[]{Word.Hungry, Word.Hungry, Word.Hungry, Word.Food})
                };
            }
            else if(WannaPlay >= AppUtil.GetMiscParams().TellWannaPlayThreshold)
            {
                ret = new List<Word>
                {
                    Word.Sports
                };
            }
            else if(Language < AppUtil.GetMiscParams().MaxLanguage)
            {
                ret = new List<Word>
                {
                    Word.Language,
                    Word.WannaKnow
                };
            }

            ApplySaidWord(ret);
            AddSaidWords(ret);
            
            return ret;
        }
        
        public IReadOnlyList<Word> Gave(ItemType itemType)
        {
            if(itemType == ItemType.None)
            {
                GameUtil.Assert(false);
                return new List<Word>();
            }

            var itemParameter = AppUtil.GetItemParameter(itemType);

            List<Word> ret = new List<Word>();
            switch (itemParameter.ItemKind)
            {
                case ItemKind.None:
                    break;
                case ItemKind.Food:
                    ret = GaveFood(itemParameter);
                    break;
                case ItemKind.Book:
                    ret = GaveBook(itemParameter);
                    break;
                case ItemKind.Toy:
                    ret = GaveToy(itemParameter);
                    break;
                default:
                    GameUtil.Assert(false);
                    break;
            }
            
            // 空腹度などを参照するので、返答を作ってから適用
            ApplyDragonChange(itemParameter.DragonChangeParameter);

            ApplySaidWord(ret);
            AddSaidWords(ret);

            _onBuyItem.OnNext(itemType);
            
            return ret;
        }
        public bool IsNeedSleep()
        {
            return Energy <= 0;
        }
        
        public bool SpeakLanguage()
        {
            return Language >= AppUtil.GetMiscParams().MaxLanguage;
        }
        
        private List<Word> GaveFood(ItemParameter itemParameter)
        {
            Turn(AppUtil.GetMiscParams().GaveFoodTurn);
            
            List<Word> ret = new List<Word>();
            bool isHungry = Hungry >= AppUtil.GetMiscParams().HungryThreshold;
            if(isHungry && itemParameter.DragonChangeParameter.Like >= AppUtil.GetMiscParams().FoodSayYeahThreshold)
            {
                ret.Add(AppUtil.GetRandomWord(new Word[]{Word.Yeah, Word.Yeah, Word.Delicious}));
            }
            else if(isHungry && itemParameter.DragonChangeParameter.Like >= AppUtil.GetMiscParams().FoodSayDeliciousThreshold)
            {
                ret.Add(Word.Delicious);
            }
            else if(itemParameter.DragonChangeParameter.Like == 0)
            {
                ret.Add(Word.Normal);
            }
            
            if (itemParameter.DragonChangeParameter.Like >= 0)
            {
                ret.Add(Word.ThankYou);
            }
            else
            {
                ret.Add(Word.Tasteless);
            }
            return ret;
        }

        private List<Word> GaveBook(ItemParameter itemParameter)
        {
            Turn(AppUtil.GetMiscParams().GaveBookTurn);
            
            List<Word> ret = new List<Word>();
            if(Language < AppUtil.GetMiscParams().MaxLanguage)
            {
                ret.Add(Word.This);
                ret.Add(Word.What);
                ret.Add(Word.ThankYou);
            }
            else
            {
                ret.Add(Word.Unnecessary);
                ret.Add(Word.ThankYou);
            }

            return ret;
        }
        
        private List<Word> GaveToy(ItemParameter itemParameter)
        {
            Turn(AppUtil.GetMiscParams().GaveToyTurn);

            List<Word> ret = new List<Word>();
            bool isMax = ToyLike >= AppUtil.GetMiscParams().MaxToyLike;
            if(itemParameter.DragonChangeParameter.ToyLike >= AppUtil.GetMiscParams().ToySayYeahThreshold)
            {
                ret.Add(AppUtil.GetRandomWord(new Word[]{Word.Yeah, Word.Hahaha}));
            }
            else if (isMax)
            {
                ret.Add(AppUtil.GetRandomWord(new Word[]{Word.None, Word.No}));
            }
            else if(itemParameter.DragonChangeParameter.ToyLike < 0)
            {
                ret.Add(AppUtil.GetRandomWord(new Word[]{Word.DontLike, Word.DontLike, Word.No}));
            }
            else
            {
                // 満タンじゃなければ喜ぶ
                ret.Add(AppUtil.GetRandomWord(new Word[]{Word.Yeah, Word.Hahaha}));
            }

            return ret;
        }
        private void ApplyDragonChange(DragonChangeParameter dragonChangeParameter)
        {
            Energy = Mathf.Clamp(Energy + dragonChangeParameter.Energy, 0, SafetyMaxValue);
            Toilet = Mathf.Clamp(Toilet + dragonChangeParameter.Toilet, 0, SafetyMaxValue);
            Hungry = Mathf.Clamp(Hungry + dragonChangeParameter.Hungry, 0, AppUtil.GetMiscParams().MaxHungry);
            if(Injury > 0)
            {
                // 怪我は治ったら変わらない
                Injury = Mathf.Clamp(Injury + dragonChangeParameter.Injury, 0, SafetyMaxValue);
            }

            if (Injury <= AppUtil.GetMiscParams().CanPlayInjury)
            {
                // 怪我が治りかけていると遊びたい
                WannaPlay = Mathf.Clamp(WannaPlay + dragonChangeParameter.WannaPlay, 0, SafetyMaxValue);
            }
            
            Like = Mathf.Clamp(Like + dragonChangeParameter.Like, 0, SafetyMaxValue);
            ToyLike = Mathf.Clamp(ToyLike + dragonChangeParameter.ToyLike, 0, AppUtil.GetMiscParams().MaxToyLike);
            Language = Mathf.Clamp(Language + dragonChangeParameter.Language, 0, AppUtil.GetMiscParams().MaxLanguage);
        }
        
        /// <summary>
        /// ターン経過、寝る以外何もしてもターンは消費する
        /// </summary>
        private void Turn(int turnNum)
        {
            bool stress = false;
            if(Hungry >= AppUtil.GetMiscParams().StressHungryThreshold)
            {
                stress = true;
                Like = Mathf.Clamp(Like - AppUtil.GetMiscParams().StressHungryDecreaseLike, 0, SafetyMaxValue);
            }
            if(Toilet >= AppUtil.GetMiscParams().StressToiletThreshold)
            {
                stress = true;
                Like = Mathf.Clamp(Like - AppUtil.GetMiscParams().StressToiletDecreaseLike, 0, SafetyMaxValue);
            }
            if(WannaPlay >= AppUtil.GetMiscParams().StressWannaPlayThreshold)
            {
                stress = true;
                Like = Mathf.Clamp(Like - AppUtil.GetMiscParams().StressWannaPlayDecreaseLike, 0, SafetyMaxValue);
            }
            if(!stress)
            {
                if(Injury > 0)
                {
                    Like = Mathf.Clamp(Like + AppUtil.GetMiscParams().InjuryNoStressIncreaseLike, 0, SafetyMaxValue);
                }
                else
                {
                    Like = Mathf.Clamp(Like + AppUtil.GetMiscParams().NoStressIncreaseLike, 0, SafetyMaxValue);
                }
            }
            
            Energy = Mathf.Min(Energy - AppUtil.GetMiscParams().TurnDecreaseEnergy * turnNum, SafetyMaxValue);
            Hungry = Mathf.Min(Hungry + AppUtil.GetMiscParams().TurnIncreaseHungry * turnNum, AppUtil.GetMiscParams().MaxHungry);
            Toilet = Mathf.Min(Toilet + AppUtil.GetMiscParams().TurnIncreaseToilet * turnNum, SafetyMaxValue);
            if (Injury <= AppUtil.GetMiscParams().CanPlayInjury)
            {
                WannaPlay = Mathf.Min(WannaPlay + AppUtil.GetMiscParams().TurnIncreaseWannaPlay * turnNum, SafetyMaxValue);
            }
        }
        
        private void ApplySaidWord(IReadOnlyList<Word> words)
        {
            if(words.Count > 0)
            {
                PreviousWord = words[^1];
            }
        }
        
        private void AddSaidWords(IReadOnlyList<Word> words)
        {
            foreach (var word in words)
            {
                if(!_saidWords.Contains(word))
                {
                    _saidWords.Add(word);
                }
            }
        }

        private void AddSpokenWord(Word word)
        {
            if(!_spokenWords.Contains(word))
            {
                _spokenWords.Add(word);
            }
        }

#if DEBUG
        public void Debug_PrepareEnd1()
        {
            Like = 0;
        }
        public void Debug_PrepareEnd2()
        {
            Like = 50;
        }
        public void Debug_PrepareEnd3()
        {
            GaveStone = true;
            Language = AppUtil.GetMiscParams().MaxLanguage;
            Like = 100;
        }
#endif
    }
}