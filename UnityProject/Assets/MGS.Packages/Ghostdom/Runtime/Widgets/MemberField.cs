
/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MemberField.cs
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
    public class MemberField : MonoBehaviour
    {
        protected enum InputType
        {
            InputBool,
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

        public Action<MemberInfo, object> OnValueChanged;
        protected MemberInfo member;

        protected virtual void Reset()
        {
            text = GetComponentInChildren<Text>(true);
            inputs = transform.parent.GetChild(0);
        }

        public void Refresh(MemberInfo member, object value)
        {
            this.member = member;
            text.text = member.Name;

            var interactable = false;
            var iptType = ResolveInput(member, out interactable);
            RefreshInput(iptType, value, interactable);
        }

        protected InputType ResolveInput(MemberInfo member, out bool interactable)
        {
            interactable = true;
            Type type = null;
            if (member.MemberType == MemberTypes.Field)
            {
                var field = (FieldInfo)member;
                interactable = field.IsPublic;
                type = field.FieldType;
            }
            else if (member.MemberType == MemberTypes.Property)
            {
                var property = (PropertyInfo)member;
                interactable = property.CanWrite;
                type = property.PropertyType;
            }

            var iptType = InputType.InputText;
            if (type == typeof(bool))
            {
                iptType = InputType.InputBool;
            }
            else if (type == typeof(string))
            {
                iptType = InputType.InputText;
            }
            else if (type == typeof(int))
            {
                iptType = InputType.InputInt;
            }
            else if (type == typeof(float) || type == typeof(double))
            {
                iptType = InputType.InputFloat;
            }
            else if (type == typeof(Vector2))
            {
                iptType = InputType.InputVector2;
            }
            else if (type == typeof(Vector3))
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

        protected InputBehaviour RefreshInput(InputType inputType)
        {
            var items = transform.childCount - reserved;
            while (items > 0)
            {
                var item = transform.GetChild(reserved).gameObject;
                DestroyImmediate(item);
                items--;
            }

            var index = (int)inputType;
            var prefab = inputs.GetChild(index).gameObject;
            return Instantiate(prefab, transform).GetComponent<InputBehaviour>();
        }

        protected void Input_OnValueChanged(object value)
        {
            if (OnValueChanged != null)
            {
                OnValueChanged(member, value);
            }
        }
    }
}