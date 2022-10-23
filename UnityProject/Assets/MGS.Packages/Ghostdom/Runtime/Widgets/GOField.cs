/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  GOField.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  10/23/2022
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using UnityEngine;
using UnityEngine.UI;

namespace MGS.Ghostdoms
{
    public class GOField : MonoBehaviour
    {
        [SerializeField]
        protected Button foldBtn;
        protected bool isFold = true;

        [SerializeField]
        protected Toggle goToggle;

        public Collector collector;
        protected GameObject go;

        public Action<GameObject, bool> OnSelected;

        protected virtual void Reset()
        {
            foldBtn = GetComponentInChildren<Button>(true);
            goToggle = GetComponentInChildren<Toggle>(true);
            collector = GetComponentInChildren<Collector>(true);
        }

        protected virtual void Awake()
        {
            foldBtn.onClick.AddListener(FoldBtn_OnClick);
            goToggle.onValueChanged.AddListener(GoToggle_OnValueChanged);
        }

        protected void FoldBtn_OnClick()
        {
            ToggleChildren(!isFold);

            if (!isFold)
            {
                collector.RequireItems(go.transform.childCount);
                for (int i = 0; i < go.transform.childCount; i++)
                {
                    var goField = collector.GetItem<GOField>(i);
                    goField.collector.prefab = collector.prefab;

                    var child = go.transform.GetChild(i).gameObject;
                    goField.Refresh(child);
                    goField.gameObject.name = child.name;

                    goField.OnSelected = Child_OnSelected;
                    goField.gameObject.SetActive(true);
                }
            }
        }

        protected void Child_OnSelected(GameObject go, bool isSelect)
        {
            if (OnSelected != null)
            {
                OnSelected.Invoke(go, isSelect);
            }
        }

        protected void GoToggle_OnValueChanged(bool isOn)
        {
            if (OnSelected != null)
            {
                OnSelected.Invoke(go, isOn);
            }
        }

        public void Refresh(GameObject go)
        {
            this.go = go;
            var isShowFold = go.transform.childCount > 0;
            foldBtn.gameObject.SetActive(isShowFold);
            foldBtn.transform.parent.GetComponent<LayoutGroup>().padding.left = isShowFold ? 0 : 18;
            goToggle.GetComponentInChildren<Text>().text = go.name;
            ToggleChildren(true);
        }

        protected void ToggleChildren(bool isFold)
        {
            foldBtn.GetComponentInChildren<Text>().text = isFold ? "+" : "-";
            collector.gameObject.SetActive(!isFold);
            this.isFold = isFold;
        }
    }
}