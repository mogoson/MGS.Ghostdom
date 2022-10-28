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
using UnityEngine;
using UnityEngine.UI;

namespace MGS.Ghostdoms
{
    public class InputText : MonoBehaviour
    {
        [SerializeField]
        protected InputField[] inputs;
        protected const string MINUS = "-";

        public event Action<object> OnValueChanged;
        protected object value;
        protected bool ignoreChange = false;

        protected virtual void Reset()
        {
            inputs = GetComponentsInChildren<InputField>(true);
        }

        protected virtual void Awake()
        {
            foreach (var ipt in inputs)
            {
                OnInitInputField(ipt);
            }
        }

        protected virtual void OnInitInputField(InputField input)
        {
            input.onValueChanged.AddListener((value) =>
            {
                if (ignoreChange) { return; }
                Input_OnValueChanged(input.name, value);
            });
        }

        protected void Input_OnValueChanged(string iptName, string text)
        {
            value = OnInputValueChanged(iptName, text);
            if (OnValueChanged != null)
            {
                OnValueChanged.Invoke(value);
            }
        }

        protected virtual object OnInputValueChanged(string iptName, string text)
        {
            return text;
        }

        public void Refresh(object value, bool interactable = true, bool ignoreChange = true)
        {
            this.value = value;
            OnRefresh(value, interactable, ignoreChange);
        }

        protected virtual void OnRefresh(object value, bool interactable = true, bool ignoreChange = true)
        {
            RefreshInput(0, value, interactable, ignoreChange);
        }

        protected void RefreshInput(int index, object value, bool interactable = true, bool ignoreChange = true)
        {
            this.ignoreChange = ignoreChange;
            inputs[index].text = value == null ? "null" : value.ToString();
            this.ignoreChange = false;

            inputs[index].interactable = interactable;
            var graphics = inputs[index].GetComponentsInChildren<Graphic>(true);
            foreach (var graphic in graphics)
            {
                graphic.raycastTarget = interactable;
            }
        }
    }
}