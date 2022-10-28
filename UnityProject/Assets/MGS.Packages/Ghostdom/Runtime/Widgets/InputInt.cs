/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  InputInt.cs
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
    public class InputInt : InputText
    {
        protected override void OnInitInputField(InputField input)
        {
            base.OnInitInputField(input);
            input.contentType = InputField.ContentType.IntegerNumber;
        }

        protected override object OnInputValueChanged(string iptName, string text)
        {
            var newValue = 0;
            if (string.IsNullOrEmpty(text) || text == MINUS) { }
            else
            {
                try
                {
                    newValue = int.Parse(text);
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