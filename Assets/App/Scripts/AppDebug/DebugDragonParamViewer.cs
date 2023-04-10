using UnityEngine;
using yydlib;

namespace App.Scripts.AppDebug
{
    public class DebugDragonParamViewer : SingletonMonoBehaviour<DebugDragonParamViewer>
    {
        [TextArea(10, 20), SerializeField] private string _dragonParam;

        public void Debug_SetDragonParam(string param)
        {
            _dragonParam = param;
        }

        protected override bool DontDestroyOnLoad => true;
        protected override void Init()
        {
        }
    }
}