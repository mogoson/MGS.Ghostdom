/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  InputBool.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  10/29/2022
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using UnityEngine;
using UnityEngine.UI;

namespace MGS.Ghostdoms
{
    public class InputBool : InputBehaviour<Toggle, bool>
    {
        protected override void OnInitInputField(Toggle input, Action<string, bool> callback)
        {
            input.onValueChanged.AddListener((value) => callback.Invoke(input.name, value));
        }

        protected override void RefreshInput(int index, object value, bool interactable = true, bool ignoreChange = true)
        {
            base.RefreshInput(index, value, interactable, ignoreChange);
            inputs[index].graphic.color = interactable ? Color.white : Color.gray;
        }

        protected override void RefreshInput(Toggle input, object value)
        {
            input.isOn = (bool)value;
        }
    }
}