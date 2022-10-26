/*************************************************************************
 *  Copyright © #COPYRIGHTYEAR# Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  PropertyField.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  #CREATEDATE#
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
        [SerializeField]
        protected Text nameTxt;

        [SerializeField]
        protected RectTransform input1;

        [SerializeField]
        protected RectTransform input2;

        [SerializeField]
        protected RectTransform input3;

        public Action<PropertyInfo, object> OnValueChanged;
        protected PropertyInfo property;

        protected virtual void Awake()
        {
            var inputs = GetComponentsInChildren<InputField>();
            foreach (var input in inputs)
            {
                input.onValueChanged.AddListener((value) => Input_OnValueChanged(input.name, value));
            }
        }

        protected void Input_OnValueChanged(string iptName, string value)
        {
            if (OnValueChanged != null)
            {
                OnValueChanged.Invoke(property, value);
            }
        }

        public void Refresh(PropertyInfo property)
        {
            this.property = property;
            nameTxt.text = property.Name;
        }
    }
}