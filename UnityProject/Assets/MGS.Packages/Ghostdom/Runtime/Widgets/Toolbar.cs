/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Toolbar.cs
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
    public class Toolbar : MonoBehaviour
    {
        public event Action<string> OnButtonClick;
        public event Action<string, string> OnInputValueChanged;

        protected virtual void Awake()
        {
            var btns = GetComponentsInChildren<Button>(true);
            foreach (var btn in btns)
            {
                btn.onClick.AddListener(() =>
                {
                    if (OnButtonClick != null)
                    {
                        OnButtonClick.Invoke(btn.name);
                    }
                });
            }

            var ipts = GetComponentsInChildren<InputField>(true);
            foreach (var ipt in ipts)
            {
                ipt.onValueChanged.AddListener((value) =>
                {
                    if (OnInputValueChanged != null)
                    {
                        OnInputValueChanged.Invoke(ipt.name, value);
                    }
                });
            }
        }
    }
}