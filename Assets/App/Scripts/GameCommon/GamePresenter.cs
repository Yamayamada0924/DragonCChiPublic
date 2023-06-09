using System;
using System.Collections.Generic;
using App.Scripts.AppDebug;
using App.Scripts.Fungus;
using App.Scripts.GameView;
using App.Scripts.Sound;
using App.Scripts.UI;
#if FEATURE_MASTER_AUDIO
using DarkTonic.MasterAudio;
#else
using MasterAudio = yydlib.CompatibleMasterAudio.CompatibleMasterAudio;
#endif
using Fungus;
using UnityEngine;
using VContainer.Unity;
using yydlib;
using UniRx;

namespace App.Scripts.GameCommon
{
    public class GamePresenter : IStartable, ITickable, System.IDisposable
    {
        private readonly GameModel.GameModel _gameModel;
        private readonly FlowchartController _flowchartController;
        private readonly TitleBehavior _titleBehavior;
        private readonly MainMenuBehavior _mainMenuBehavior;
        private readonly SpotLightChangeView _spotLightChangeView;
        private readonly GameSoundView _gameSoundView;
        private readonly SkipViewBehavior _skipViewBehavior;

        private readonly CompositeDisposable _disposable = new CompositeDisposable();
#if false
        // 自分でInjectするものがあればこうするというメモ
        [Inject]
        InjectTest injectTest { get; set; }
#endif

        public GamePresenter(
            GameModel.GameModel gameModel,
            FlowchartController flowchartController,
            TitleBehavior titleBehavior,
            MainMenuBehavior mainMenuBehavior,
            SpotLightChangeView spotLightChangeView,
            GameSoundView gameSoundView,
            SkipViewBehavior skipViewBehavior
        )
        {
            this._gameModel = gameModel;
            this._flowchartController = flowchartController;
            this._titleBehavior = titleBehavior;
            this._mainMenuBehavior = mainMenuBehavior;
            this._spotLightChangeView = spotLightChangeView;
            this._gameSoundView = gameSoundView;
            this._skipViewBehavior = skipViewBehavior;
        }
        public void Dispose()
        {
            _disposable.Dispose();
        }

        void IStartable.Start()
        {
            _titleBehavior.OnGameStart.Subscribe(_ =>
            {
                _gameSoundView.PlayDecide();
                _gameModel.ResetGame(_titleBehavior.IsWithJapanese);
                _mainMenuBehavior.SetSpeakViewText(_gameModel.IsWithJapanese());
                _flowchartController.SetVolume(MasterAudio.MasterVolumeLevel);
                _flowchartController.StartOpeningBlock(_gameModel.IsWithJapanese());
            }).AddTo(_disposable);

            _mainMenuBehavior.OnGameAction.Subscribe(gameAction =>
            {
                if(_flowchartController.IsRunning())
                {
                    return;
                }
                
                IReadOnlyList<Word> words;
                switch (gameAction)
                {
                    case GameAction.Listen:
                        _gameSoundView.PlayDecide();
                        words = _gameModel.DragonListened();
                        _flowchartController.SayListenAnswer(words, _gameModel.IsDragonNoName(), _gameModel.IsWithJapanese());
                        OnNormalLightChange();
                        break;
                    
                    case GameAction.Play:
                        _gameSoundView.PlayDecide();
                        words = _gameModel.DragonPlaySports();
                        _flowchartController.SaySportsAnswer(words, _gameModel.IsDragonNoName(),
                            _gameModel.IsDayNight(), _gameModel.IsWithJapanese());
                        OnNormalLightChange();
                        break;
                    
                    case GameAction.Toilet:
                        _gameSoundView.PlayDecide();
                        if(_gameModel.DragonIsGoToilet())
                        {
                            words = _gameModel.DragonGoToilet();
                            _flowchartController.SayToiletAnswer(words, _gameModel.IsDragonNoName(),
                                _gameModel.IsDayNight(), _gameModel.IsWithJapanese());
                            OnNormalLightChange();
                        }
                        else
                        {
                            words = _gameModel.DragonRefuseToilet();
                            _flowchartController.SayRefuseToiletAnswer(words, _gameModel.IsDragonNoName(), _gameModel.IsWithJapanese());
                            OnNormalLightChange();
                        }
                        break;
                    
                    case GameAction.Sleep:
                        _gameSoundView.PlayDecide();
                        if (_gameModel.DragonIsGoSleep())
                        {
                            _gameModel.DragonSleep(false);
                            _flowchartController.Sleep(_gameModel.GetSleepParam(false));
                        }
                        else
                        {
                            words = _gameModel.DragonRefuseSleep();
                            _flowchartController.SayRefuseSleepAnswer(words, _gameModel.IsDragonNoName(), _gameModel.IsWithJapanese());
                            OnNormalLightChange();
                        }

                        break;
                    
                    case GameAction.Give:
                    case GameAction.Speak:
                    default:
                        GameUtil.Assert(false);
                        break;
                }
            }).AddTo(_disposable);
            
            _mainMenuBehavior.OnSpeak.Subscribe(word =>
            {
                if(_flowchartController.IsRunning())
                {
                    return;
                }
                _gameSoundView.PlayDecide();
                IReadOnlyList<Word> words = _gameModel.DragonSpoken(word);
                _flowchartController.SaySpeakAnswer(words, _gameModel.IsDragonNoName(), word, _gameModel.IsWithJapanese());
                OnNormalLightChange();
            }).AddTo(_disposable);
            
            _mainMenuBehavior.OnGiveItem.Subscribe(itemType =>
            {
                if(_flowchartController.IsRunning())
                {
                    return;
                }
                _gameSoundView.PlayDecide();
                var itemParameter = AppUtil.GetItemParameter(itemType);
                IReadOnlyList<Word> words = _gameModel.DragonGave(itemType);
                _flowchartController.SayGiveAnswer(words, _gameModel.IsDragonNoName(), itemParameter.ItemKind, _gameModel.IsWithJapanese());
                OnNormalLightChange();
            }).AddTo(_disposable);
            
            _mainMenuBehavior.OnActionButtonEnter.Subscribe(gameAction =>
            {
                if(_flowchartController.IsRunning())
                {
                    return;
                }
                _flowchartController.ShowExplain(AppUtil.GetExplainFromGameAction(gameAction));
            }).AddTo(_disposable);
            
            _mainMenuBehavior.OnActionButtonExit.Subscribe(_ =>
            {
                if(_flowchartController.IsRunning())
                {
                    return;
                }
                _flowchartController.HideExplain();
            }).AddTo(_disposable);
            
            _mainMenuBehavior.OnMoneyEnter.Subscribe(_ =>
            {
                if(_flowchartController.IsRunning())
                {
                    return;
                }
                _flowchartController.ShowExplain(Explain.Money);
            }).AddTo(_disposable);
            
            _mainMenuBehavior.OnMoneyExit.Subscribe(_ =>
            {
                if(_flowchartController.IsRunning())
                {
                    return;
                }
                _flowchartController.HideExplain();
            }).AddTo(_disposable);
            
            _mainMenuBehavior.OnSpeakViewShown.Subscribe(_ =>
            {
                if(_flowchartController.IsRunning())
                {
                    return;
                }
                _flowchartController.ShowExplainSpeakView();
            }).AddTo(_disposable);
            
            _mainMenuBehavior.OnSpeakViewHidden.Subscribe(_ =>
            {
                if(_flowchartController.IsRunning())
                {
                    return;
                }
                _flowchartController.HideExplain();
            }).AddTo(_disposable);
            
            _mainMenuBehavior.OnGiveViewShown.Subscribe(_ =>
            {
                if(_flowchartController.IsRunning())
                {
                    return;
                }
                _flowchartController.ShowExplainGiveView();
            }).AddTo(_disposable);
            
            _mainMenuBehavior.OnGiveViewHidden.Subscribe(_ =>
            {
                if(_flowchartController.IsRunning())
                {
                    return;
                }
                _flowchartController.HideExplain();
            }).AddTo(_disposable);
            
            _mainMenuBehavior.OnSpeakViewShow.Subscribe(speakViewButtonSetter =>
            {
                if(_flowchartController.IsRunning())
                {
                    return;
                }
                _gameSoundView.PlayPopupOpen();
                speakViewButtonSetter.Show();
            }).AddTo(_disposable);
            
            _mainMenuBehavior.OnGiveViewShow.Subscribe(giveViewButtonSetter =>
            {
                if(_flowchartController.IsRunning())
                {
                    return;
                }
                _gameSoundView.PlayPopupOpen();
                giveViewButtonSetter.Show();
                giveViewButtonSetter.SetButtonEnable(_gameModel.Money);
            }).AddTo(_disposable);
            
            _mainMenuBehavior.OnSpeakViewClose.Subscribe(_ =>
            {
                _gameSoundView.PlayPopupClose();
            }).AddTo(_disposable);
            
            _mainMenuBehavior.OnGiveViewClose.Subscribe(_ =>
            {
                _gameSoundView.PlayPopupClose();
            }).AddTo(_disposable);
            
            _flowchartController.OnApplyLight.Subscribe(_ =>
            {
                _spotLightChangeView.ChangeLight(_gameModel.GetLightRate(),
                    AppUtil.GetMiscParams().LightChangeTimeSleep);
            }).AddTo(_disposable);

            _skipViewBehavior.OnSkip.Subscribe(_ =>
            {
                _flowchartController.Skip();
            }).AddTo(_disposable);

            _gameModel.OnTrueEndingStart.Subscribe(_ =>
            {
                _titleBehavior.EnableClearedButton();
            }).AddTo(_disposable);
        }

        void ITickable.Tick()
        {
            // 強制睡眠チェック.
            if(_gameModel.IsDragonNeedSleep() && !_flowchartController.IsRunning())
            {
                _gameModel.DragonSleep(true);
                _flowchartController.Sleep(_gameModel.GetSleepParam(true));
            }
            
            CommonParameters.IViewParamReadOnly viewParam = _gameModel.GetViewParam();
            
            _mainMenuBehavior.UpdateGame(viewParam);

            _flowchartController.UpdateGame();
#if DEBUG
            DebugDragonParamViewer.Instance.Debug_SetDragonParam(_gameModel.Debug_GetDragonParam());
#endif
        }

        private void OnNormalLightChange()
        {
            _spotLightChangeView.ChangeLight(_gameModel.GetLightRate(),
                AppUtil.GetMiscParams().LightChangeTime);
        }


    }
}
