using System;
using System.Collections.Generic;
using App.Scripts.Sound;
#if FEATURE_MASTER_AUDIO
using DarkTonic.MasterAudio;
#endif
#if FEATURE_DOOZY
using Doozy.Engine.UI;
#endif
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using yydlib;

namespace App.Scripts.UI
{
    public class TitleBehavior : MonoBehaviour, IDisposable
    {
        private enum TitleButton
        {
            Start,
            License,
            Tweet,
            Twitter0,
            Twitter1,
            DragonLanguage,
            Ending,
        }

        private enum TitleSlider
        {
            Sound,
        }
        public enum TitlePopup
        {
            License,
        }

        private static float _soundVolume = 1.0f;

#if FEATURE_DOOZY
        private UIView _uiView;

        [SerializeField] private List<UIButton> buttons;
#endif
    
        [SerializeField]  private List<Slider> sliders;
        [SerializeField]  private List<SliderPointerUp> sliderPointerUps;

        [SerializeField] private TextMeshProUGUI languageButtonText;
        

        [SerializeField] private LicenceBehavior licencePopup;

        [SerializeField] private EndingYesNoBehavior endingYesNoBehavior;
        
        [SerializeField] private UnityEngine.Playables.PlayableDirector endingPlayableDirector;

        private bool _clearedTrueEnding;

        private readonly Subject<Unit> _onGameStart = new Subject<Unit>();
        public IObservable<Unit> OnGameStart => this._onGameStart;
        
        public bool IsWithJapanese { get; private set; }
        

        // Start is called before the first frame update
        private void Awake()
        {
#if FEATURE_DOOZY
            _uiView = gameObject.GetComponent<UIView>();

            buttons[(int)TitleButton.Start].OnClick.OnTrigger.Action += _ =>
            {
                _uiView.Hide();
                _onGameStart.OnNext(Unit.Default);
            };
            buttons[(int)TitleButton.License].OnClick.OnTrigger.Action += _ =>
            {
                licencePopup.Show();
            };
            buttons[(int)TitleButton.Tweet].OnClick.OnTrigger.Action += _ =>
            {
                if(_clearedTrueEnding)
                {
#if FEATURE_UNITYROOM_TWEET
                    naichilab.UnityRoomTweet.Tweet("dummy", "私とドラをプレイしてトゥルーエンドを見たよ！", "unityroom", "unity1week");
#endif
                }
                else
                {
#if FEATURE_UNITYROOM_TWEET
                    naichilab.UnityRoomTweet.Tweet("dummy", "私とドラをプレイしたよ！", "unityroom", "unity1week");
#endif
                }
            };
            buttons[(int)TitleButton.Twitter0].OnClick.OnTrigger.Action += _ =>
            {
                Application.OpenURL("https://twitter.com/yamayamadagames");
            };
            buttons[(int)TitleButton.Twitter1].OnClick.OnTrigger.Action += _ =>
            {
                Application.OpenURL("https://twitter.com/yamayamada0_0");
            };
            buttons[(int)TitleButton.DragonLanguage].OnClick.OnTrigger.Action += _ =>
            {
                IsWithJapanese = !IsWithJapanese;
                languageButtonText.text = $"翻訳:{(IsWithJapanese ? "ON" : "OFF")}";
            };
            buttons[(int)TitleButton.Ending].OnClick.OnTrigger.Action += _ =>
            {
                endingYesNoBehavior.Show();
            };
#endif
            endingYesNoBehavior.OnEndingStart.Subscribe(_ =>
            {
                endingYesNoBehavior.gameObject.SetActive(false);
                endingPlayableDirector.Play();
            });
            
            sliders[(int)TitleSlider.Sound].value = _soundVolume;
            sliders[(int)TitleSlider.Sound].onValueChanged.AddListener(value =>
            {
                _soundVolume = value;
#if FEATURE_MASTER_AUDIO
                MasterAudio.MasterVolumeLevel = _soundVolume;
                MasterAudio.PlaylistMasterVolume = _soundVolume;
#endif
            });
            sliderPointerUps[(int)TitleSlider.Sound].OnValueChanged.AddListener(_ =>
            {
                SoundManagerForMasterAudio.Instance.PlaySe(SeId.Decide);
            });
        }

        public void Dispose()
        {
            _onGameStart?.Dispose();
        }

        // Update is called once per frame
        private void Update()
        {
#if DEBUG
#if FEATURE_MASTER_AUDIO
            if (Input.GetKeyDown(KeyCode.Alpha1)) {
                MasterAudio.PlaySound("speedt_mainmenu_button");
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                //再生
                MasterAudio.StartPlaylistOnClip("Default", "speedt_music1");
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                //停止
                MasterAudio.StopAllPlaylists();
            }
#endif
#endif
        }

        public void EnableClearedButton()
        {
#if FEATURE_DOOZY
            buttons[(int)TitleButton.DragonLanguage].gameObject.SetActive(true);
            buttons[(int)TitleButton.Ending].gameObject.SetActive(true);
#endif
            _clearedTrueEnding = true;
        }

    }
}
