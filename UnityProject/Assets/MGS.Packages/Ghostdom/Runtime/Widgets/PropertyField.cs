
/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  PropertyField.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  10/27/2022
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace MGS.Ghostdoms
{
    public class PropertyField : MonoBehaviour
    {
        protected enum InputType
        {
            InputText,
            InputInt,
            InputFloat,
            InputVector2,
            InputVector3
        }

        [SerializeField]
        protected Text text;

        [SerializeField]
        protected Transform inputs;

        [SerializeField]
        protected int reserved = 1;

        public Action<PropertyInfo, object> OnValueChanged;
        protected PropertyInfo property;

        protected virtual void Reset()
        {
            text = GetComponentInChildren<Text>(true);
            inputs = transform.parent.GetChild(0);
        }

        public void Refresh(PropertyInfo property, object value)
        {
            this.property = property;
            text.text = property.Name;

            var interactable = false;
            var iptType = ResolveInput(property, out interactable);
            RefreshInput(iptType, value, interactable);
        }

        protected InputType ResolveInput(PropertyInfo property, out bool interactable)
        {
            interactable = property.CanWrite;

            var iptType = InputType.InputText;
            if (property.PropertyType == typeof(string))
            { }
            else if (property.PropertyType == typeof(int))
            {
                iptType = InputType.InputInt;
            }
            else if (property.PropertyType == typeof(float) || property.PropertyType == typeof(double))
            {
                iptType = InputType.InputFloat;
            }
            else if (property.PropertyType == typeof(Vector2))
            {
                iptType = InputType.InputVector2;
            }
            else if (property.PropertyType == typeof(Vector3))
            {
                iptType = InputType.InputVector3;
            }
            else
            {
                interactable = false;
            }
            return iptType;
        }

        protected void RefreshInput(InputType inputType, object value, bool interactable)
        {
            var input = RefreshInput(inputType);
            input.OnValueChanged += Input_OnValueChanged;
            input.Refresh(value, interactable);
            input.gameObject.SetActive(true);
        }

        protected InputText RefreshInput(InputType inputType)
        {
            var items = transform.childCount - reserved;
            while (items > 0)
            {
                Destroy(transform.GetChild(items).gameObject);
                items--;
            }

            var index = (int)inputType;
            var prefab = inputs.GetChild(index).gameObject;
            return Instantiate(prefab, transform).GetComponent<InputText>();
        }

        protected void Input_OnValueChanged(object value)
        {
            if (OnValueChanged != null)
            {
                OnValueChanged(property, value);
            }
        }
    }
}