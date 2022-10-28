/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  ObjectField.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  10/27/2022
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace MGS.Ghostdoms
{
    public class ObjectField : MonoBehaviour
    {
        [SerializeField]
        protected Text text;

        [SerializeField]
        protected Collector collector;
        protected object obj;

        public void Refresh(object obj)
        {
            this.obj = obj;
            text.text = obj.GetType().Name;
            Refresh(obj.GetType().GetMembers());
        }

        protected void Refresh(ICollection<MemberInfo> members)
        {
            var selects = new List<MemberInfo>();
            var values = new List<object>();
            foreach (var member in members)
            {
                object value = null;
                try
                {
                    if (member.MemberType == MemberTypes.Field)
                    {
                        var field = (FieldInfo)member;
                        if (field.FieldType == typeof(Matrix4x4))
                        {
                            continue;
                        }
                        value = field.GetValue(obj);
                    }
                    else if (member.MemberType == MemberTypes.Property)
                    {
                        var property = (PropertyInfo)member;
                        if (property.PropertyType == typeof(Matrix4x4))
                        {
                            continue;
                        }
                        value = property.GetValue(obj, null);
                    }
                    else { continue; }
                }
                catch { continue; }

                selects.Add(member);
                values.Add(value);
            }

            collector.RequireItems(selects.Count);

            var i = 0;
            foreach (var member in selects)
            {
                var item = collector.GetItem<MemberField>(i);
                item.Refresh(member, values[i]);
                item.OnValueChanged = Member_OnValueChanged;
                item.gameObject.SetActive(true);
                i++;
            }
        }

        protected void Member_OnValueChanged(MemberInfo member, object value)
        {
            try
            {
                if (member.MemberType == MemberTypes.Field)
                {
                    var field = (FieldInfo)member;
                    field.SetValue(obj, value);
                }
                else if (member.MemberType == MemberTypes.Property)
                {
                    var property = (PropertyInfo)member;
                    property.SetValue(obj, value, null);
                }
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }
    }
}