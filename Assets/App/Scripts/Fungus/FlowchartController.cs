using System;
using System.Collections.Generic;
using App.Scripts.FixedData;
using App.Scripts.GameCommon;
using Fungus;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;
using yydlib;

namespace App.Scripts.Fungus
{
    public class FlowchartController : MonoBehaviour
    {
        [SerializeField] private BlockReference openingBlock;

        [SerializeField] private BlockReference openingSkipBlock;

        [SerializeField] private List<BlockReference> explainBlocks;

        [SerializeField] private BlockReference explainSpeakViewBlock;
        
        [SerializeField] private BlockReference explainGiveViewBlock;

        [SerializeField] private BlockReference explainStopBlock;

        [SerializeField] private Flowchart answerFlowchart;

        [SerializeField] private BlockReference speakAnswer;

        [SerializeField] private BlockReference listenAnswer;
        
        [SerializeField] private BlockReference sportsAnswer;
        
        [SerializeField] private BlockReference toiletAnswer;

        [SerializeField] private BlockReference toiletRefuseAnswer;
        
        [SerializeField] private List<BlockReference> giveBlocks;
        
        [SerializeField] private BlockReference sleepRefuseAnswer;
        
        [SerializeField] private Flowchart eventFlowchart;

        [SerializeField] private BlockReference sleepActionBlock;

        [SerializeField] private BlockReference forceSleepBlock;

        [SerializeField] private List<VolumeVariableWriterAudio> writerAudios;
        

        private readonly Subject<Unit> _onApplyLight = new Subject<Unit>();
        public IObservable<Unit> OnApplyLight => this._onApplyLight;
        

        private const string WithJapaneseVariable = "WithJapanese";

        private const string NoNameVariable = "NoName";

        private static readonly string[] AnswerVariable = new string[] {
            "answer1",
            "answer2",
            "answer3",
        };

        private const string DayNightVariable = "DayNight";
        private const string SpeakWordVariable = "SpeakWord";
        
        private const string DayVariable = "Day";
        private const string StoneVariable = "Stone";
        private const string LanguageVariable = "Language";
        private const string EndingVariable = "Ending";
        private const string TrueEndVariable = "TrueEnd";
        private const string Talk0Variable = "Talk0";
        private const string Talk1Variable = "Talk1";
        private const string Talk0JpVariable = "Talk0JP";
        private const string Talk1JpVariable = "Talk1JP";
        private const string TalkGoodEmotionVariable = "TalkGoodEmotion";
        private const string TalkBadEmotionVariable = "TalkBadEmotion";
        private const string SpokenWordVariable = "SpokenWord";
        private const string SpokenWordJpVariable = "SpokenWordJP";
        private const string SaidWordVariable = "SaidWord";
        private const string SaidWordJpVariable = "SaidWordJP";
        private const string AdditionalWordVariable = "AdditionalWord";
        private const string AdditionalWordJpVariable = "AdditionalWordJP";
        
        public void SetVolume(float volume)
        {
            foreach (var writerAudio in writerAudios)
            {
                writerAudio.SetVolume(volume);
            }
        }
        
        public void UpdateGame()
        {
            if(eventFlowchart.GetBooleanVariable(ApplyLight.Key))
            {
                eventFlowchart.SetBooleanVariable(ApplyLight.Key, false);
                _onApplyLight.OnNext(Unit.Default);
            }
        }
        
        public void StartOpeningBlock(bool withJapanese)
        {
            openingBlock.block.GetFlowchart().SetBooleanVariable(WithJapaneseVariable, withJapanese);
            eventFlowchart.SetBooleanVariable(WithJapaneseVariable, withJapanese);
            openingBlock.Execute();
        }
        
        
        public void Skip()
        {
            var flowchart = openingBlock.block.GetFlowchart();
            if(flowchart != null)
            {
                flowchart.StopAllBlocks();
                openingSkipBlock.Execute();
            }
        }
        
        public void ShowExplain(Explain explain)
        {
            if ((int)explain < explainBlocks.Count)
            {
                explainBlocks[(int)explain].Execute();
            }
            else
            {
                GameUtil.Assert(false);
            }
        }
        public void ShowExplainSpeakView()
        {
            explainSpeakViewBlock.Execute();
        }       
        public void ShowExplainGiveView()
        {
            explainGiveViewBlock.Execute();
        }       
        public void HideExplain()
        {
            explainStopBlock.Execute();
        }
        
        public void SaySpeakAnswer(IReadOnlyList<Word> words, bool noName, Word speakWord, bool withJapanese)
        {
            HideExplain();
            PrepareAnswer(words, noName, withJapanese);
            answerFlowchart.SetStringVariable(SpeakWordVariable, AppUtil.GetWordParameter(speakWord).GetTranslationWord(withJapanese));
            speakAnswer.Execute();
        }

        public void SayListenAnswer(IReadOnlyList<Word> words, bool noName, bool withJapanese)
        {
            HideExplain();
            PrepareAnswer(words, noName, withJapanese);
            listenAnswer.Execute();
        }
        
        public void SayGiveAnswer(IReadOnlyList<Word> words, bool noName, ItemKind itemKind, bool withJapanese)
        {
            GameUtil.Assert((int)itemKind - 1 < giveBlocks.Count);
            HideExplain();
            PrepareAnswer(words, noName, withJapanese);
            giveBlocks[(int)itemKind - 1].Execute();
        }
        
        public void SaySportsAnswer(IReadOnlyList<Word> words, bool noName, bool dayNight, bool withJapanese)
        {
            HideExplain();
            PrepareAnswer(words, noName, withJapanese);
            answerFlowchart.SetBooleanVariable(DayNightVariable, dayNight);
            sportsAnswer.Execute();
        }
        
        public void SayToiletAnswer(IReadOnlyList<Word> words, bool noName, bool dayNight, bool withJapanese)
        {
            HideExplain();
            PrepareAnswer(words, noName, withJapanese);
            answerFlowchart.SetBooleanVariable(DayNightVariable, dayNight);
            toiletAnswer.Execute();
        }
        public void SayRefuseToiletAnswer(IReadOnlyList<Word> words, bool noName, bool withJapanese)
        {
            HideExplain();
            PrepareAnswer(words, noName, withJapanese);
            toiletRefuseAnswer.Execute();
        }
        
        public void Sleep(SleepParam sleepParam)
        {
            eventFlowchart.SetIntegerVariable(DayVariable, sleepParam.day);
            eventFlowchart.SetBooleanVariable(StoneVariable, sleepParam.stone);
            eventFlowchart.SetBooleanVariable(LanguageVariable, sleepParam.language);
            eventFlowchart.SetBooleanVariable(EndingVariable, sleepParam.ending);
            eventFlowchart.SetBooleanVariable(TrueEndVariable, sleepParam.trueEnd);
            eventFlowchart.SetStringVariable(Talk0Variable, sleepParam.talk0);
            eventFlowchart.SetStringVariable(Talk1Variable, sleepParam.talk1);
            eventFlowchart.SetStringVariable(Talk0JpVariable, sleepParam.talk0Jp);
            eventFlowchart.SetStringVariable(Talk1JpVariable, sleepParam.talk1Jp);
            eventFlowchart.SetBooleanVariable(TalkGoodEmotionVariable, sleepParam.talkGoodEmotion);
            eventFlowchart.SetBooleanVariable(TalkBadEmotionVariable, sleepParam.talkBadEmotion);
            eventFlowchart.SetStringVariable(SpokenWordVariable, sleepParam.spokenWord);
            eventFlowchart.SetStringVariable(SpokenWordJpVariable, sleepParam.spokenWordJp);
            eventFlowchart.SetStringVariable(SaidWordVariable, sleepParam.saidWord);
            eventFlowchart.SetStringVariable(SaidWordJpVariable, sleepParam.saidWordJp);
            eventFlowchart.SetStringVariable(AdditionalWordVariable, sleepParam.additionalWord);
            eventFlowchart.SetStringVariable(AdditionalWordJpVariable, sleepParam.additionalWordJp);
            HideExplain();
            if (sleepParam.isForce)
            {
                forceSleepBlock.Execute();
            }
            else
            {
                sleepActionBlock.Execute();
            }
        }
        
        public void SayRefuseSleepAnswer(IReadOnlyList<Word> words, bool noName, bool withJapanese)
        {
            HideExplain();
            PrepareAnswer(words, noName, withJapanese);
            sleepRefuseAnswer.Execute();
        }
        
        public bool IsRunning()
        {
            return answerFlowchart.HasExecutingBlocks() || eventFlowchart.HasExecutingBlocks();
        }
        
        private void PrepareAnswer(IReadOnlyList<Word> words, bool noName, bool withJapanese)
        {
            answerFlowchart.SetBooleanVariable(NoNameVariable, noName);
            
            PrepareWords(words, answerFlowchart, withJapanese);
        }
        
        private void PrepareWords(IReadOnlyList<Word> words, Flowchart flowchart, bool withJapanese)
        {
            foreach (var key in AnswerVariable)
            {
                flowchart.SetStringVariable(key, "");
            }

            int answer = 0;
            for (int i = 0; i < words.Count; i++)
            {
                if(i >= AnswerVariable.Length)
                {
                    GameUtil.Assert(false);
                    break;
                }

                if(words[i] == Word.This && i + 1 < words.Count)
                {
                    // Thisの特殊処理、1個後と結合する
#if false
                    flowchart.SetStringVariable(AnswerVariable[answer++], 
                        AppUtil.GetWordParameter(words[i]).GetTranslationWord(withJapanese) + AppUtil.GetWordParameter(words[i+1]).GetTranslationWord(withJapanese));
#else
                    // なぜか上のコードだとFungus側で[]内の文字がなくなるので無理やり対応...
                    if(withJapanese)
                    {
                        flowchart.SetStringVariable(AnswerVariable[answer++], 
                            $"{AppUtil.GetWordParameter(words[i]).GetDragonLanguage()}{AppUtil.GetWordParameter(words[i+1]).GetDragonLanguage()}[{AppUtil.GetWordParameter(words[i]).GetJapanese()}{AppUtil.GetWordParameter(words[i+1]).GetJapanese()}]");
                    }
                    else
                    {
                        flowchart.SetStringVariable(AnswerVariable[answer++], 
                            AppUtil.GetWordParameter(words[i]).GetDragonLanguage() + AppUtil.GetWordParameter(words[i+1]).GetDragonLanguage());
                    }
#endif
                    i++;
                }
                else
                {
                    flowchart.SetStringVariable(AnswerVariable[answer++], AppUtil.GetWordParameter(words[i]).GetTranslationWord(withJapanese));
                }
            }
        }

    }
}