using System;
using App.Scripts.FixedData;
using UniRx;
using UnityEngine;
using yydlib;

namespace App.Scripts.AppDebug
{
#if DEBUG
    public struct OnChangeDragonParamArg
    {
        public readonly bool isUp;
        public readonly DragonParamType paramType;

        public OnChangeDragonParamArg(bool isUp, DragonParamType paramType)
        {
            this.isUp = isUp;
            this.paramType = paramType;
        }
    }
    
    public class DebugCommandManager: SingletonMonoBehaviour<DebugCommandManager>
    {

        private readonly Subject<OnChangeDragonParamArg> _onChangeDragonParam = new Subject<OnChangeDragonParamArg>();
        public IObservable<OnChangeDragonParamArg> OnChangeDragonParam => this._onChangeDragonParam;

        private readonly Subject<Unit> _onForceEnd1 = new Subject<Unit>();
        public IObservable<Unit> OnForceEnd1 => this._onForceEnd1;

        private readonly Subject<Unit> _onForceEnd2 = new Subject<Unit>();
        public IObservable<Unit> OnForceEnd2 => this._onForceEnd2;

        private readonly Subject<Unit> _onForceEnd3 = new Subject<Unit>();
        public IObservable<Unit> OnForceEnd3 => this._onForceEnd3;

        
        private readonly (KeyCode, DragonParamType)[] _changeDragonParamPatterns =
        {
            (KeyCode.E, DragonParamType.Energy),
            (KeyCode.T, DragonParamType.Toilet),
            (KeyCode.H, DragonParamType.Hungry),
            (KeyCode.I, DragonParamType.Injury),
            (KeyCode.P, DragonParamType.WannaPlay),
            (KeyCode.L, DragonParamType.Like),
            (KeyCode.Y, DragonParamType.ToyLike),
            (KeyCode.Semicolon, DragonParamType.Language),
        };
        

        protected override bool DontDestroyOnLoad => true;
        protected override void Init()
        {
        }

        private void Update()
        {
            if (Input.anyKeyDown)
            {
                foreach (var changeDragonParamPattern in _changeDragonParamPatterns)
                {
                    if(Input.GetKey(changeDragonParamPattern.Item1))
                    {
                        if(Input.GetKeyDown(KeyCode.UpArrow))
                        {
                            _onChangeDragonParam.OnNext(new OnChangeDragonParamArg(true, changeDragonParamPattern.Item2));
                        }
                        if(Input.GetKeyDown(KeyCode.DownArrow))
                        {
                            _onChangeDragonParam.OnNext(new OnChangeDragonParamArg(false, changeDragonParamPattern.Item2));
                        }
                    }
                }
                
                if(Input.GetKeyDown(KeyCode.Keypad1))
                {
                    _onForceEnd1.OnNext(Unit.Default);
                }
                if(Input.GetKeyDown(KeyCode.Keypad2))
                {
                    _onForceEnd2.OnNext(Unit.Default);
                }
                if(Input.GetKeyDown(KeyCode.Keypad3))
                {
                    _onForceEnd3.OnNext(Unit.Default);
                }
            }
        }
        
    }
#endif
}