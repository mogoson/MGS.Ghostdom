/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  LRTPanel.cs
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
    public class LRTPanel : MonoBehaviour
    {
        protected class LRTBtnNames
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
            var toolbar = transform.Find("Toolbar").GetComponent<LRToolbar>();
            toolbar.OnButtonClick += Toolbar_OnButtonClick;

            InitializeSize();
        }

        protected virtual void Toolbar_OnButtonClick(string btnName)
        {
            switch (btnName)
            {
                case LRTBtnNames.Add:
                    AddSize();
                    break;

                case LRTBtnNames.Reduce:
                    ReduceSize();
                    break;

                case LRTBtnNames.Close:
                    ClosePanel();
                    break;
            }
        }

        protected virtual void InitializeSize()
        {
            var parentWidth = (transform.parent as RectTransform).rect.width;
            minSize = parentWidth * 0.05f;
            maxSize = parentWidth * 0.35f;
            stepSize = parentWidth * 0.05f;
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

        public void SetSize(RectTransform.Axis axis, float size)
        {
            (transform as RectTransform).SetSizeWithCurrentAnchors(axis, size);
            if (OnSizeChanged != null)
            {
                OnSizeChanged.Invoke(axis, size);
            }
        }

        protected virtual void ClosePanel()
        {
            gameObject.SetActive(false);
        }
    }
}