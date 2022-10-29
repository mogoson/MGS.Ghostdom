/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  InputBehaviour.cs
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
    public abstract class InputBehaviour : MonoBehaviour
    {
        public abstract event Action<object> OnValueChanged;

        public abstract void Refresh(object value, bool interactable = true, bool ignoreChange = true);
    }

    public abstract class InputBehaviour<T, K> : InputBehaviour where T : Selectable
    {
        [SerializeField]
        protected T[] inputs;

        public override event Action<object> OnValueChanged;
        protected object value;
        protected bool ignoreChange = false;

        protected virtual void Reset()
        {
            inputs = GetComponentsInChildren<T>(true);
        }

        protected virtual void Awake()
        {
            foreach (var ipt in inputs)
            {
                OnInitInputField(ipt, Input_OnValueChanged);
            }
        }

        protected abstract void OnInitInputField(T input, Action<string, K> callback);

        protected void Input_OnValueChanged(string iptName, K value)
        {
            if (ignoreChange)
            {
                return;
            }

            this.value = OnInputValueChanged(iptName, value);
            if (OnValueChanged != null)
            {
                OnValueChanged.Invoke(this.value);
            }
        }

        protected virtual object OnInputValueChanged(string iptName, K value)
        {
            return value;
        }

        public override void Refresh(object value, bool interactable = true, bool ignoreChange = true)
        {
            this.value = value;
            OnRefresh(value, interactable, ignoreChange);
        }

        protected virtual void OnRefresh(object value, bool interactable = true, bool ignoreChange = true)
        {
            RefreshInput(0, value, interactable, ignoreChange);
        }

        protected virtual void RefreshInput(int index, object value, bool interactable = true, bool ignoreChange = true)
        {
            inputs[index].interactable = interactable;
            var graphics = inputs[index].GetComponentsInChildren<Graphic>(true);
            foreach (var graphic in graphics)
            {
                graphic.raycastTarget = interactable;
            }

            this.ignoreChange = ignoreChange;
            RefreshInput(inputs[index], value);
            this.ignoreChange = false;
        }

        protected abstract void RefreshInput(T input, object value);
    }
}