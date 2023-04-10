using System;
using System.Collections.Generic;
using App.Scripts.AppDebug;
using App.Scripts.FixedData;
using App.Scripts.GameCommon;
using UniRx;
using UnityEngine;
using yydlib;

namespace App.Scripts.GameModel
{
	public class GameModel : System.IDisposable
	{

		private readonly DragonData _dragonData;
		public IDragonDataReadOnly DragonData => _dragonData;

		private readonly ProgressData _progressData;
		public IProgressDataReadOnly ProgressData => _progressData;
		public int Money => _progressData.Money;

		private readonly Subject<Unit> _onTrueEndingStart = new Subject<Unit>();
		public IObservable<Unit> OnTrueEndingStart => this._onTrueEndingStart;
		

		public GameModel()
		{
			_dragonData = new DragonData();
			_progressData = new ProgressData();

			_dragonData.OnBuyItem.Subscribe(itemType =>
			{
				var itemParameter = AppUtil.GetItemParameter(itemType);
				_progressData.DecreaseMoney(itemParameter.Money);
			});

#if DEBUG
			DebugCommandManager.Instance.OnForceEnd1.Subscribe(_ =>
			{
				_progressData.Debug_PrepareEnd1();
				_dragonData.Debug_PrepareEnd1();
			});
			DebugCommandManager.Instance.OnForceEnd2.Subscribe(_ =>
			{
				_progressData.Debug_PrepareEnd2();
				_dragonData.Debug_PrepareEnd2();
			});
			DebugCommandManager.Instance.OnForceEnd3.Subscribe(_ =>
			{
				_progressData.Debug_PrepareEnd3();
				_dragonData.Debug_PrepareEnd3();
			});
#endif
			
			ResetGame(false);
		}

		public void Dispose()
		{
		}

		public void ResetGame(bool isWithJapanese)
		{
			_dragonData.Reset();
			_progressData.Reset(isWithJapanese);
		}
		

		public CommonParameters.ViewParam GetViewParam()
		{
			var ret = new CommonParameters.ViewParam
			{
				Money = _progressData.Money
			};
			return ret;
		}

		public IReadOnlyList<Word> DragonSpoken(Word word)
		{
			return _dragonData.Spoken(word);
		}
		public IReadOnlyList<Word> DragonListened()
		{
			return _dragonData.Listened();
		}
		
		public IReadOnlyList<Word> DragonPlaySports()
		{
			return _dragonData.PlaySports();
		}
		public IReadOnlyList<Word> DragonGave(ItemType itemType)
		{
			return _dragonData.Gave(itemType);
		}
		
		public bool DragonIsGoToilet()
		{
			return _dragonData.IsGoToilet();
		}
		public bool DragonIsGoSleep()
		{
			return _dragonData.IsGoSleep();
		}		
		public IReadOnlyList<Word> DragonGoToilet()
		{
			return _dragonData.GoToilet();
		}
		public IReadOnlyList<Word> DragonRefuseToilet()
		{
			return _dragonData.RefuseToilet();
		}

		public void DragonSleep(bool isForce)
		{
			_progressData.Sleep();
			_dragonData.Sleep(isForce, _progressData.Day);
			if(IsTrueEnd())
			{
				_onTrueEndingStart.OnNext(Unit.Default);
			}
		}
		public IReadOnlyList<Word> DragonRefuseSleep()
		{
			return _dragonData.RefuseSleep();
		}
		public bool IsDragonNeedSleep()
		{
			return _dragonData.IsNeedSleep();
		}		
		public bool IsDragonNoName()
		{
			return _progressData.IsDragonNoName;
		}
		
		public int GetDay()
		{
			return _progressData.Day;
		}
		
		public bool IsDayNight()
		{
			return _dragonData.Energy > AppUtil.GetMiscParams().EnergyNightThreshold;
		}

		public float GetLightRate()
		{
			return AppUtil.GetMiscParams().EnergyToLightIntensityCurve.Evaluate(_dragonData.Energy);
		}

		public SleepParam GetSleepParam(bool isForce)
		{
			var talkParameter = GetEmotionallyTalk();
			var spokenWord = GetEndingSpokenWord();
			var saidWord = GetEndingSaidWord();
			var additionalWord = GetEndingAdditionalWord();
			var spokenWordParameter = AppUtil.GetWordParameter(spokenWord);
			var saidWordParameter = AppUtil.GetWordParameter(saidWord);
			var additionalWordParameter = AppUtil.GetWordParameter(additionalWord);

			var talk0 = "";
			var talk1 = "";
			var talk0Jp = "";
			var talk1Jp = "";
			var goodEmotion = false;
			var badEmotion = false;
			if(talkParameter != null)
			{
				var talk0WordParameter = AppUtil.GetWordParameter(talkParameter.PreviousWord);
				var talk1WordParameter = AppUtil.GetWordParameter(talkParameter.NextWord);
				talk0 = talk0WordParameter.GetDragonLanguage();
				talk0Jp = talk0WordParameter.GetJapanese();
				talk1 = talk1WordParameter.GetDragonLanguage();
				talk1Jp = talk1WordParameter.GetJapanese();
				goodEmotion = talkParameter.DragonChangeParameter.Like >= AppUtil.GetMiscParams().GoodEmotionTalkLikeThreshold;
				badEmotion = talkParameter.DragonChangeParameter.Like <= AppUtil.GetMiscParams().BadEmotionTalkLikeThreshold;
			}

			return new SleepParam(
				GetDay() + 1,
				IsDragonNoName(),
				isForce,
				IsDragonWillGiveStone(),
				IsDragonSpeakLanguage(),
				IsDay4Ending(),
				IsTrueEnd(),
				talk0,
				talk1,
				talk0Jp,
				talk1Jp,
				goodEmotion,
				badEmotion,
				spokenWordParameter.GetDragonLanguage(true),
				spokenWordParameter.GetJapanese(true),
				saidWordParameter.GetDragonLanguage(true),
				saidWordParameter.GetJapanese(true),
				additionalWordParameter.GetDragonLanguage(true),
				additionalWordParameter.GetJapanese(true)
			);
		}

		public bool IsWithJapanese()
		{
			return _progressData.IsWithJapanese;
		}
		
		private bool IsDragonWillGiveStone()
		{
			return _dragonData.WillGiveStone;
		}
		private bool IsDragonSpeakLanguage()
		{
			return _dragonData.SpeakLanguage();
		}

		private bool IsDay4Ending()
		{
			return _dragonData.Like + _dragonData.ToyLike < AppUtil.GetMiscParams().Day4EndingLikeThreshold;
		}

		private bool IsTrueEnd()
		{
			return (_dragonData.Like + _dragonData.ToyLike >= AppUtil.GetMiscParams().TrueEndLikeThreshold) &&
			       _dragonData.SpeakLanguage() && _dragonData.GaveStone;
		}
		private TalkParameter GetEmotionallyTalk()
		{
			return _dragonData.EmotionallyTalk;
		}

		private Word GetEndingSpokenWord()
		{
			int count = _dragonData.SpokenWords.Count; 
			if(count == 0)
			{
				return Word.None;
			}
			
			int choice = FixOneRand.Range(
				_dragonData.Like + _dragonData.Hungry + _dragonData.Toilet,/*seed*/
				0, count);
			return _dragonData.SpokenWords[choice];
		}

		private Word GetEndingSaidWord()
		{
			int count = _dragonData.SaidWords.Count; 
			if(count == 0)
			{
				GameUtil.Assert(false);// 絶対にあるはず
				return Word.None;
			}

			int choice = FixOneRand.Range(
				_dragonData.Like + _dragonData.Hungry + _dragonData.Toilet,/*seed*/
				0, count);
			return _dragonData.SaidWords[choice];
		}

		private Word GetEndingAdditionalWord()
		{
			var talkParameter = GetEmotionallyTalk();
			var endingSpokenWord = talkParameter == null ? GetEndingSpokenWord() : Word.None;
			var endingSaidWord = talkParameter == null && endingSpokenWord == Word.None ? GetEndingSaidWord() : Word.None;
			
			// まず話した言葉から選ぼうとする
			foreach (var spokenWord in _dragonData.SpokenWords)
			{
				if(talkParameter != null &&
				   (spokenWord == talkParameter.PreviousWord || spokenWord == talkParameter.NextWord))
				{
					continue;
				}
				if(spokenWord == endingSpokenWord)
				{
					continue;
				}
				return spokenWord;
			}

			// 全部の言葉からランダムに選ぶ
			int wordsMax = GameUtil.GetEnumCount(typeof(Word));
			int start = FixOneRand.Range(
				_dragonData.Like + _dragonData.Hungry + _dragonData.Toilet,/*seed*/
				0, wordsMax);
			for (int i = 0 ; i < wordsMax ; i++)
			{
				int index = (start + i) % wordsMax;
				var word = (Word)index;
				if(word == Word.None)
				{
					continue;
				}
				if(talkParameter != null &&
				   (word == talkParameter.PreviousWord || word == talkParameter.NextWord))
				{
					continue;
				}
				if(word == endingSpokenWord)
				{
					continue;
				}
				if(word == endingSaidWord)
				{
					continue;
				}

				return word;
			}

			// safety
			return Word.CanFly;
		}
#if DEBUG
		public string Debug_GetDragonParam()
		{
			string ret = "";
			ret += $"Energy = {_dragonData.Energy}\n";
			ret += $"Toilet = {_dragonData.Toilet}\n";
			ret += $"Hungry = {_dragonData.Hungry}\n";
			ret += $"Injury = {_dragonData.Injury}\n";
			ret += $"WannaPlay = {_dragonData.WannaPlay}\n";
			ret += $"Like = {_dragonData.Like}\n";
			ret += $"ToyLike = {_dragonData.ToyLike}\n";
			ret += $"Language = {_dragonData.Language}\n";
			ret += $"PreviousWord = {_dragonData.PreviousWord}\n";
			ret += $"EmotionallyTalk = {(_dragonData.EmotionallyTalk != null ? _dragonData.EmotionallyTalk.name : "なし")}\n";
			ret += "SpokenWords = "; 
			foreach (var spokenWord in _dragonData.SpokenWords)
			{
				ret += $"{spokenWord},";
			}
			ret += "\n"; 
			ret += "SaidWords = "; 
			foreach (var saidWord in _dragonData.SaidWords)
			{
				ret += $"{saidWord},";
			}
			ret += "\n"; 
			return ret;
		}
#endif
		
	}
}
