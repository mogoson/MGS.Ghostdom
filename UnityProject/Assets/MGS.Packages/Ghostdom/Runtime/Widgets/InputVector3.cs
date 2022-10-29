/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  InputVector3.cs
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
    public class InputVector3 : InputVector2
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
                    else if (iptName == inputs[2].name)
                    {
                        index = 2;
                    }
                    RefreshInput(index, newValue, inputs[index].interactable);
                }
            }

            var vector3 = (Vector3)value;
            if (iptName == inputs[0].name)
            {
                vector3.x = newValue;
            }
            else if (iptName == inputs[1].name)
            {
                vector3.y = newValue;
            }
            else if (iptName == inputs[2].name)
            {
                vector3.z = newValue;
            }
            return vector3;
        }

        protected override void OnRefresh(object value, bool interactable = true, bool ignoreChange = true)
        {
            var vector3 = (Vector3)value;
            RefreshInput(0, vector3.x, interactable, ignoreChange);
            RefreshInput(1, vector3.y, interactable, ignoreChange);
            RefreshInput(2, vector3.z, interactable, ignoreChange);
        }
    }
}