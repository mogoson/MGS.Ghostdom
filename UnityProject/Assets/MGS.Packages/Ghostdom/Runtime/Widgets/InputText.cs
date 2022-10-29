/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  InputText.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  10/27/2022
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using UnityEngine.UI;

namespace MGS.Ghostdoms
{
    public class InputText : InputBehaviour<InputField, string>
    {
        protected const string MINUS = "-";

        protected override void OnInitInputField(InputField input, Action<string, string> callback)
        {
            OnInitInputField(input);
            input.onValueChanged.AddListener((value) => callback.Invoke(input.name, value));
        }

        protected virtual void OnInitInputField(InputField input)
        {
            input.contentType = InputField.ContentType.Standard;
        }

        protected override void RefreshInput(InputField input, object value)
        {
            input.text = value == null ? "null" : value.ToString();
        }
    }
}