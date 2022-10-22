/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  LRToolbar.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  10/22/2022
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using UnityEngine;
using UnityEngine.UI;

namespace MGS.Ghostdoms
{
    public class LRToolbar : MonoBehaviour
    {
        public event Action<string> OnButtonClick;

        protected virtual void Awake()
        {
            var btns = GetComponentsInChildren<Button>(true);
            foreach (var btn in btns)
            {
                btn.onClick.AddListener(() => OnButtonClick.Invoke(btn.name));
            }
        }
    }
}