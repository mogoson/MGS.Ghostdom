/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  InputFloat.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  10/28/2022
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine.UI;

namespace MGS.Ghostdoms
{
    public class InputFloat : InputText
    {
        protected override void OnInitInputField(InputField input)
        {
            base.OnInitInputField(input);
            input.contentType = InputField.ContentType.DecimalNumber;
        }

        protected override object OnInputValueChanged(string iptName, string text)
        {
            var newValue = 0f;
            if (string.IsNullOrEmpty(text) || text == MINUS) { }
            else
            {
                try
                {
                    newValue = float.Parse(text);
                }
                catch
                {
                    RefreshInput(0, newValue, inputs[0].interactable);
                }
            }
            return newValue;
        }
    }
}