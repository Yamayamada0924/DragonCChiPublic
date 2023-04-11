using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace yydlib.CompatibleDoozyUI
{
    [Serializable]
    public class CompatibleUIPopupContentReferences
    {
        public List<CompatibleUIButton> Buttons = new List<CompatibleUIButton>();
        
        public List<Image> Images = new List<Image>();
        
        public List<GameObject> Labels = new List<GameObject>();
    }
}