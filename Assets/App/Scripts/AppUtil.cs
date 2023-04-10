//#define ASSERT_FORCE_OFF

using System;
using System.Collections;
using App.Scripts.FixedData;
using App.Scripts.GameCommon;
using App.Scripts.GameModel;
using App.Scripts.UI;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using yydlib;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace App.Scripts
{


    public static class AppUtil
    {
        static readonly float DestroyDelayTime = 0.5f;

        public static MiscParameters GetMiscParams()
        {
            return FixDataManager.Instance.MiscParameters;
        }
        
        public static void StartCoroutineByGameManager(IEnumerator enumerator)
	    {
            GameUtil.Assert(GameManager.gameManager != null);
            GameManager.gameManager.StartCoroutine(enumerator);
        }

	    public static void DestroyAction(GameObject gameObject)
	    {
            Object.Destroy(gameObject, DestroyDelayTime);
        }

        public static Explain GetExplainFromGameAction(GameAction gameAction)
        {
            Explain ret = Explain.Speak;
            switch (gameAction)
            {
                case GameAction.Speak:
                    ret = Explain.Speak;
                    break;
                case GameAction.Listen:
                    ret = Explain.Listen;
                    break;
                case GameAction.Play:
                    ret = Explain.Play;
                    break;
                case GameAction.Give:
                    ret = Explain.Give;
                    break;
                case GameAction.Toilet:
                    ret = Explain.Toilet;
                    break;
                case GameAction.Sleep:
                    ret = Explain.Sleep;
                    break;
                default:
                    GameUtil.Assert(false);
                    break;
            }

            return ret;
        }
        
        public static ItemParameter GetItemParameter(ItemType itemType)
        {
            return FixDataManager.Instance.ItemParameters.GetItemParameter(itemType);
        }
        public static WordParameter GetWordParameter(Word word)
        {
            return FixDataManager.Instance.WordParameters.GetWordParameter(word);
        }
        public static bool IsConditionTrue(TalkParameter.ConditionParameter conditionParameter, DragonData dragonData)
        {
            int value = 0;
            switch (conditionParameter.dragonParamType)
            {
                case DragonParamType.Energy:
                    value = dragonData.Energy;
                    break;
                case DragonParamType.Toilet:
                    value = dragonData.Toilet;
                    break;
                case DragonParamType.Hungry:
                    value = dragonData.Hungry;
                    break;
                case DragonParamType.Injury:
                    value = dragonData.Injury;
                    break;
                case DragonParamType.WannaPlay:
                    value = dragonData.WannaPlay;
                    break;
                case DragonParamType.Like:
                    value = dragonData.Like;
                    break;
                case DragonParamType.ToyLike:
                    value = dragonData.ToyLike;
                    break;
                case DragonParamType.Language:
                    value = dragonData.Language;
                    break;
                default:
                    GameUtil.Assert(false);
                    break;
            }

            return conditionParameter.IsConditionTrue(value);
        }
        
        public static Word GetRandomWord(Word[] words)
        {
            return words[Random.Range(0, words.Length)];
        }

        public static Vector2 GetResizedPosition(Vector2 position, float scaleFactor)
	    {
            var offset = new Vector2
            {
                x = 0.0f,
                y = 0.0f
            };
            
            if ((float)Screen.width > 0.0f && (float)Screen.height > 0.0f)
            {
                if ((float)Screen.width / (float)Screen.height > StaticParameters.DefaultScreenWidth / StaticParameters.DefaultScreenHeight)
                {
                    offset.x = ((float)Screen.width - ((float)Screen.height * (StaticParameters.DefaultScreenWidth / StaticParameters.DefaultScreenHeight))) * 0.5f;
                }
                if ((float)Screen.width / (float)Screen.height < StaticParameters.DefaultScreenWidth / StaticParameters.DefaultScreenHeight)
                {
                    offset.y = ((float)Screen.height - ((float)Screen.width * (StaticParameters.DefaultScreenHeight / StaticParameters.DefaultScreenWidth))) * 0.5f;
                }
            }

            var ret = new Vector2
            {
                x = position.x * scaleFactor + offset.x,
                y = position.y * scaleFactor + offset.y
            };

            return ret;
        }
        
        public static void ChangeTrackVolume(string audioTrackName, PlayableDirector playableDirector, float volume)
        {
            var tracks = (playableDirector.playableAsset as TimelineAsset)?.GetOutputTracks();
            foreach (var track in tracks)
            {
                if (track.name == audioTrackName)
                {
                    var audioTrack = track as AudioTrack;
                    if (audioTrack != null)
                    {
                        audioTrack.CreateCurves("nameOfAnimationClip");
                        audioTrack.curves.SetCurve(string.Empty,
                            typeof(AudioTrack),
                            "volume",
                            AnimationCurve.Linear(volume, volume,volume, volume));
                    }
                }
            }
            playableDirector.RebuildGraph();
        }

    }

}