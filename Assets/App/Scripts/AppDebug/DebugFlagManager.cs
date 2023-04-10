using System;
using UnityEngine;
using yydlib;

namespace App.Scripts.AppDebug
{
#if DEBUG
    public class DebugFlagManager: SingletonMonoBehaviour<DebugFlagManager>
    {
        public bool JapaneseMode { get; private set; }
        protected override bool DontDestroyOnLoad => true;
        protected override void Init()
        {
        }

        private void Update()
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetKeyDown(KeyCode.J))
                {
                    JapaneseMode = !JapaneseMode;
                }
            }
        }
    }
#endif
}