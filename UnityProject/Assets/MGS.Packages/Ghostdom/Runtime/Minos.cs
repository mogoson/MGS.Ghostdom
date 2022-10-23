/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Minos.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  10/22/2022
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MGS.Ghostdoms
{
    public class Minos : LRTPanel
    {
        #region
        protected class MTBtnNames : LRTBtnNames
        {
            public const string Clear = "Clear";
        }

        protected struct Log
        {
            public string level;
            public string message;

            public Log(string type, string message)
            {
                this.level = type;
                this.message = message;
            }
        }

        protected struct LogLevel
        {
            public const string Log = "Log";
            public const string Warning = "Warning";
            public const string Error = "Error";
        }
        #endregion

        #region
        [SerializeField]
        protected Collector collector;

        [SerializeField]
        protected InputField searchField;
        protected string keyword = string.Empty;

        protected List<Log> logs = new List<Log>();
        protected List<string> levels = new List<string>
        {
            LogLevel.Log, LogLevel.Warning, LogLevel.Error
        };

        protected override void Awake()
        {
            base.Awake();

            Application.logMessageReceived += Application_logMessageReceived;
            searchField.onValueChanged.AddListener(SearchField_OnValueChanged);
        }

        protected override void Toolbar_OnButtonClick(string btnName)
        {
            switch (btnName)
            {
                case MTBtnNames.Clear:
                    ClearLog();
                    break;

                case LogLevel.Log:
                case LogLevel.Warning:
                case LogLevel.Error:
                    FilterLevel(btnName);
                    break;

                default:
                    base.Toolbar_OnButtonClick(btnName);
                    break;
            }
        }

        protected void Application_logMessageReceived(string condition, string stackTrace, LogType type)
        {
            AppendLog(type.ToString(), string.Format("{0}\r\n{1}", condition, stackTrace.TrimEnd()));
        }

        protected void SearchField_OnValueChanged(string text)
        {
            keyword = text;
            FilterLog(levels, keyword);
        }

        protected void AppendLog(string level, string message)
        {
            var log = new Log(level, message);
            logs.Add(log);

            if (levels.Contains(log.level) && message.Contains(keyword))
            {
                var txt = collector.CreateItem<Text>();
                RefreshItem(txt, log);
            }
        }

        protected void FilterLevel(string level)
        {
            if (levels.Contains(level))
            {
                levels.Remove(level);
            }
            else
            {
                levels.Add(level);
            }
            FilterLog(levels, keyword);
        }

        protected void FilterLog(List<string> levels, string keyword)
        {
            var selects = new List<Log>();
            foreach (var log in logs)
            {
                if (levels.Contains(log.level) && log.message.Contains(keyword))
                {
                    selects.Add(log);
                }
            }
            Refresh(selects);
        }

        protected void ClearLog()
        {
            logs.Clear();
            collector.RequireItems(logs.Count);
        }

        protected void Refresh(List<Log> logs)
        {
            while (logs.Count > 1000)
            {
                logs.RemoveAt(0);
            }
            collector.RequireItems(logs.Count);

            var i = 0;
            foreach (var log in logs)
            {
                var txt = collector.GetItem<Text>(i);
                RefreshItem(txt, log);
                i++;
            }
        }

        protected void RefreshItem(Text text, Log log)
        {
            text.text = log.message;
            text.color = GetColor(log.level);
            text.gameObject.SetActive(true);
        }

        protected Color GetColor(string type)
        {
            var color = Color.white;
            switch (type)
            {
                case LogLevel.Warning:
                    color = Color.yellow;
                    break;

                case LogLevel.Error:
                    color = Color.red;
                    break;
            }
            return color;
        }
        #endregion

        #region
        protected override void InitializeSize()
        {
            var parentHeight = (transform.parent as RectTransform).rect.height;
            minSize = parentHeight * 0.05f;
            maxSize = parentHeight * 0.45f;
            stepSize = parentHeight * 0.05f;
        }

        protected override void AddSize()
        {
            var height = (transform as RectTransform).rect.height;
            if (height + stepSize <= maxSize)
            {
                SetSize(RectTransform.Axis.Vertical, height + stepSize);
            }
        }

        protected override void ReduceSize()
        {
            var height = (transform as RectTransform).rect.height;
            if (height - stepSize >= minSize)
            {
                SetSize(RectTransform.Axis.Vertical, height - stepSize);
            }
        }
        #endregion
    }
}