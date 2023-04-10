using UnityEngine;

namespace App.Scripts.FixedData
{
	[CreateAssetMenu(menuName = "MyScriptable/Create MiscParameters")]
	public class MiscParameters : ScriptableObject
	{
		[SerializeField] private int defaultMoney;
		public int DefaultMoney => this.defaultMoney;
		
		
		[SerializeField] private int noStressIncreaseLike;
		public int NoStressIncreaseLike => this.noStressIncreaseLike;

		[SerializeField] private int injuryNoStressIncreaseLike;
		public int InjuryNoStressIncreaseLike => this.injuryNoStressIncreaseLike;

		[Header("体力関連")]
		[SerializeField] private int startEnergy;
		public int StartEnergy => this.startEnergy;

		[SerializeField] private int sleepEnergy;
		public int SleepEnergy => this.sleepEnergy;

		[SerializeField] private int sleepyEnergy;
		public int SleepyEnergy => this.sleepyEnergy;
		
		[SerializeField] private int turnDecreaseEnergy;
		public int TurnDecreaseEnergy => this.turnDecreaseEnergy;
		
		[SerializeField] private int forceSleepDecreaseLike;
		public int ForceSleepDecreaseLike => this.forceSleepDecreaseLike;

		[SerializeField] private int energyNightThreshold;
		public int EnergyNightThreshold => this.energyNightThreshold;
		
		
		[Header("怪我関連")]
		[SerializeField] private int startInjury;
		public int StartInjury => this.startInjury;

		[SerializeField] private int canPlayInjury;
		public int CanPlayInjury => this.canPlayInjury;
		

		[Tooltip("1睡眠ごとに回復する怪我"), SerializeField] private int sleepHealInjury;
		public int SleepHealInjury => this.sleepHealInjury;
		

		[Header("空腹関連")]
		[SerializeField] private int startHungry;
		public int StartHungry => this.startHungry;

		[Tooltip("1睡眠ごとに増える空腹"), SerializeField] private int sleepIncreaseHungry;
		public int SleepIncreaseHungry => this.sleepIncreaseHungry;

		[SerializeField] private int turnIncreaseHungry;
		public int TurnIncreaseHungry => this.turnIncreaseHungry;
		
		[SerializeField] private int maxHungry;
		public int MaxHungry => this.maxHungry;

		[SerializeField] private int tellHungryThreshold;
		public int TellHungryThreshold => this.tellHungryThreshold;
		

		[Header("トイレ関連")]
		
		[SerializeField] private int toiletLikeThreshold;
		public int ToiletLikeThreshold => this.toiletLikeThreshold;

		[SerializeField] private int toiletGoThreshold;
		public int ToiletGoThreshold => this.toiletGoThreshold;
		
		[SerializeField] private int turnIncreaseToilet;
		public int TurnIncreaseToilet => this.turnIncreaseToilet;

		[SerializeField] private int likeIncreaseByToilet;
		public int LikeIncreaseByToilet => this.likeIncreaseByToilet;

		[SerializeField] private int stressToiletThreshold;
		public int StressToiletThreshold => this.stressToiletThreshold;

		[SerializeField] private int stressToiletDecreaseLike;
		public int StressToiletDecreaseLike => this.stressToiletDecreaseLike;
		
		[SerializeField] private int sleepWithToiletDecreaseLike;
		public int SleepWithToiletDecreaseLike => this.sleepWithToiletDecreaseLike;
		
		[Header("Language関連")] 
		[SerializeField] private int maxLanguage;
		public int MaxLanguage => this.maxLanguage;
		

		[Header("Food関連")]
		[SerializeField] private int foodSayYeahThreshold;
		public int FoodSayYeahThreshold => this.foodSayYeahThreshold;
		
		[SerializeField]private int foodSayDeliciousThreshold;
		public int FoodSayDeliciousThreshold => this.foodSayDeliciousThreshold;
		
		[SerializeField] private int hungryThreshold;
		public int HungryThreshold => this.hungryThreshold;

		[SerializeField] private int stressHungryThreshold;
		public int StressHungryThreshold => this.stressHungryThreshold;

		[SerializeField] private int stressHungryDecreaseLike;
		public int StressHungryDecreaseLike => this.stressHungryDecreaseLike;
		
		
		[Header("Toy関連")]
		[SerializeField] private int maxToyLike;
		public int MaxToyLike => this.maxToyLike;
		
		[SerializeField] private int toySayYeahThreshold;
		public int ToySayYeahThreshold => this.toySayYeahThreshold;
		
		
		[Header("運動関連")]
		[SerializeField] private int tellWannaPlayThreshold;
		public int TellWannaPlayThreshold => this.tellWannaPlayThreshold;
		
		[SerializeField] private int turnIncreaseWannaPlay;
		public int TurnIncreaseWannaPlay => this.turnIncreaseWannaPlay;
		
		[SerializeField] private int stressWannaPlayThreshold;
		public int StressWannaPlayThreshold => this.stressWannaPlayThreshold;

		[SerializeField] private int stressWannaPlayDecreaseLike;
		public int StressWannaPlayDecreaseLike => this.stressWannaPlayDecreaseLike;
		
		[SerializeField] private int sportsWithInjuryDecreaseLike;
		public int SportsWithInjuryDecreaseLike => this.sportsWithInjuryDecreaseLike;
		
		[SerializeField] private int sportsWithInjuryIncreaseInjury;
		public int SportsWithInjuryIncreaseInjury => this.sportsWithInjuryIncreaseInjury;
		
		[SerializeField] private int sportsIncreaseLike;
		public int SportsIncreaseLike => this.sportsIncreaseLike;		
		
		[Header("ターン消費関連")] 
		[SerializeField] private int spokenTurn;
		public int SpokenTurn => this.spokenTurn;
		
		[SerializeField] private int listenedTurn;
		public int ListenedTurn => this.listenedTurn;
		
		[SerializeField] private int goToiletTurn;
		public int GoToiletTurn => this.goToiletTurn;
		
		[SerializeField] private int refuseToiletTurn;
		public int RefuseToiletTurn => this.refuseToiletTurn;
		
		[SerializeField] private int sportsTurn;
		public int SportsTurn => this.sportsTurn;
		
		[SerializeField] private int gaveFoodTurn;
		public int GaveFoodTurn => this.gaveFoodTurn;
		
		[SerializeField] private int gaveBookTurn;
		public int GaveBookTurn => this.gaveBookTurn;

		[SerializeField] private int gaveToyTurn;
		public int GaveToyTurn => this.gaveToyTurn;
		
		[SerializeField] private int refuseSleepTurn;
		public int RefuseSleepTurn => this.refuseSleepTurn;

		[Header("エンディング関連")]
		[SerializeField] private int day4EndingLikeThreshold;
		public int Day4EndingLikeThreshold => this.day4EndingLikeThreshold;
		
		[SerializeField] private int trueEndLikeThreshold;
		public int TrueEndLikeThreshold => this.trueEndLikeThreshold;

		[SerializeField] private int giveStoneLikeThreshold;
		public int GiveStoneLikeThreshold => this.giveStoneLikeThreshold;

		[SerializeField] private int goodEmotionTalkLikeThreshold;
		public int GoodEmotionTalkLikeThreshold => this.goodEmotionTalkLikeThreshold;
		
		[SerializeField] private int badEmotionTalkLikeThreshold;
		public int BadEmotionTalkLikeThreshold => this.badEmotionTalkLikeThreshold;

		[Header("見た目")] 
		[SerializeField] private AnimationCurve energyToLightIntensityCurve;
		public AnimationCurve EnergyToLightIntensityCurve => this.energyToLightIntensityCurve;

		[SerializeField] private float lightChangeTime;
		public float LightChangeTime => this.lightChangeTime;

		[SerializeField] private float lightChangeTimeSleep;
		public float LightChangeTimeSleep => this.lightChangeTimeSleep;
		
		
		public AnimationCurve testCurve;// Evaluate(rate); で使う.
	}
}
