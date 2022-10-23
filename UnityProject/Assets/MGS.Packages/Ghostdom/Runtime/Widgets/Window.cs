/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Window.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  10/22/2022
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using UnityEngine;

namespace MGS.Ghostdoms
{
    public class Window : MonoBehaviour
    {
        public enum SizeMode { Min, Moderate, Max }

        protected class ToolNames
        {
            public const string Add = "Add";
            public const string Reduce = "Reduce";
            public const string Close = "Close";
        }

        public event Action<RectTransform.Axis, float> OnSizeChanged;

        protected float minSize;
        protected float maxSize;
        protected float stepSize;

        protected virtual void Awake()
        {
            var toolbar = transform.Find("Toolbar").GetComponent<Toolbar>();
            toolbar.OnButtonClick += Toolbar_OnButtonClick;
            toolbar.OnInputValueChanged += Toolbar_OnInputValueChanged;

            InitializeSize();
        }

        protected virtual void Toolbar_OnButtonClick(string btnName)
        {
            switch (btnName)
            {
                case ToolNames.Add:
                    AddSize();
                    break;

                case ToolNames.Reduce:
                    ReduceSize();
                    break;

                case ToolNames.Close:
                    Close();
                    break;
            }
        }

        protected virtual void Toolbar_OnInputValueChanged(string iptName, string value) { }

        protected virtual void InitializeSize()
        {
            var parentWidth = (transform.parent as RectTransform).rect.width;
            minSize = 200;
            maxSize = Mathf.Max(parentWidth * 0.35f, minSize);
            stepSize = parentWidth * 0.05f;
            SetSize(RectTransform.Axis.Horizontal, SizeMode.Moderate);
        }

        protected virtual void AddSize()
        {
            var width = (transform as RectTransform).rect.width;
            if (width + stepSize <= maxSize)
            {
                SetSize(RectTransform.Axis.Horizontal, width + stepSize);
            }
        }

        protected virtual void ReduceSize()
        {
            var width = (transform as RectTransform).rect.width;
            if (width - stepSize >= minSize)
            {
                SetSize(RectTransform.Axis.Horizontal, width - stepSize);
            }
        }

        public void SetSize(RectTransform.Axis axis, SizeMode mode)
        {
            var size = 0f;
            switch (mode)
            {
                case SizeMode.Min:
                    size = minSize;
                    break;

                case SizeMode.Moderate:
                    size = (minSize + maxSize) * 0.5f;
                    break;

                case SizeMode.Max:
                    size = maxSize;
                    break;
            }
            SetSize(axis, size);
        }

        public void SetSize(RectTransform.Axis axis, float size)
        {
            (transform as RectTransform).SetSizeWithCurrentAnchors(axis, size);
            if (OnSizeChanged != null)
            {
                OnSizeChanged.Invoke(axis, size);
            }
        }

        protected void Close()
        {
            gameObject.SetActive(false);
        }
    }
}