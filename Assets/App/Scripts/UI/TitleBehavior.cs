using System;
using System.Collections.Generic;
using App.Scripts.Sound;
#if FEATURE_MASTER_AUDIO
using DarkTonic.MasterAudio;
#else
using MasterAudio = yydlib.CompatibleMasterAudio.CompatibleMasterAudio;
#endif
#if FEATURE_DOOZY
using Doozy.Engine.UI;
#else
using yydlib.CompatibleDoozyUI;
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
#else
        private CompatibleUIView _uiView;
#endif
#if FEATURE_DOOZY
        [SerializeField] private List<UIButton> buttons;
#else
        [SerializeField] private List<CompatibleUIButton> compatibleButtons;
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
#else
            _uiView = gameObject.GetComponent<CompatibleUIView>();
            var buttons = compatibleButtons;
#endif

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
            endingYesNoBehavior.OnEndingStart.Subscribe(_ =>
            {
                endingYesNoBehavior.gameObject.SetActive(false);
                endingPlayableDirector.Play();
            });
            
            sliders[(int)TitleSlider.Sound].value = _soundVolume;
            sliders[(int)TitleSlider.Sound].onValueChanged.AddListener(value =>
            {
                _soundVolume = value;
                MasterAudio.MasterVolumeLevel = _soundVolume;
                MasterAudio.PlaylistMasterVolume = _soundVolume;
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
            if (Input.GetKeyDown(KeyCode.Alpha1)) {
                MasterAudio.PlaySound("Decide");
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                //再生
                MasterAudio.StartPlaylistOnClip("Default", "NormalBGM");
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                //停止
                MasterAudio.StopAllPlaylists();
            }
#endif
        }

        public void EnableClearedButton()
        {
#if !FEATURE_DOOZY
            var buttons = compatibleButtons;
#endif
            buttons[(int)TitleButton.DragonLanguage].gameObject.SetActive(true);
            buttons[(int)TitleButton.Ending].gameObject.SetActive(true);
            _clearedTrueEnding = true;
        }

    }
}
