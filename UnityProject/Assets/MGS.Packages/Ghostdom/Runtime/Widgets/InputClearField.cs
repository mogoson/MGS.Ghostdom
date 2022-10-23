/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  InputClearField.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  10/23/2022
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;
using UnityEngine.UI;

namespace MGS.Ghostdoms
{
    public class InputClearField : MonoBehaviour
    {
        protected virtual void Awake()
        {
            var inputField = GetComponentInChildren<InputField>(true);
            var clearBtn = GetComponentInChildren<Button>(true);

            inputField.onValueChanged.AddListener((text) =>
            {
                var isActiveBtn = !string.IsNullOrEmpty(text);
                clearBtn.gameObject.SetActive(isActiveBtn);
            });

            clearBtn.onClick.AddListener(() =>
            {
                inputField.text = string.Empty;
            });
        }
    }
}