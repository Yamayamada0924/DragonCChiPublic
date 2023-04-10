//#define ASSERT_FORCE_OFF

using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Rendering;

namespace yydlib
{
    public static class SettingsUtil
    {
        public static void ApplyFPS(int fpsUpdate, int fpsRender = -1)
        {
            if(fpsRender == -1)
            {
                fpsRender = fpsUpdate;
            }
            GameUtil.Assert(fpsUpdate <= fpsRender);
            GameUtil.Assert(fpsUpdate % fpsRender == 0);
            Application.targetFrameRate = fpsUpdate;
            OnDemandRendering.renderFrameInterval = fpsUpdate / fpsRender;
        }

    }
}
