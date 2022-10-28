/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  InputVector2.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  10/27/2022
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.Ghostdoms
{
    public class InputVector2 : InputFloat
    {
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
                    var index = 0;
                    if (iptName == inputs[1].name)
                    {
                        index = 1;
                    }
                    RefreshInput(index, newValue, inputs[index].interactable);
                }
            }

            var vector2 = (Vector2)value;
            if (iptName == inputs[0].name)
            {
                vector2.x = newValue;
            }
            else if (iptName == inputs[1].name)
            {
                vector2.y = newValue;
            }
            return vector2;
        }

        protected override void OnRefresh(object value, bool interactable = true, bool ignoreChange = true)
        {
            var vector2 = (Vector2)value;
            RefreshInput(0, vector2.x, interactable, ignoreChange);
            RefreshInput(1, vector2.y, interactable, ignoreChange);
        }
    }
}