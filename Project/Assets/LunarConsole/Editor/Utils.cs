﻿using UnityEngine;
using UnityEditor;

using System;
using System.Collections.Generic;
using System.Reflection;

namespace LunarConsoleInternal
{
    struct DialogButton
    {
        public readonly string title;
        public readonly Action<string> action;
        
        public DialogButton(string title, Action action)
        {
            this.title = title;
            this.action = action != null ? (Action<string>)(delegate(string obj) { action(); }) : null;
        }
        
        public DialogButton(string title, Action<string> action = null)
        {
            this.title = title;
            this.action = action;
        }
        
        internal void PerformAction()
        {
            try
            {
                if (action != null)
                {
                    action(title);
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }

    static class Utils
    {
        #region Dialogs
        
        internal static bool ShowDialog(string title, string message)
        {
            return EditorUtility.DisplayDialog(title, message, "Ok", "Cancel");
        }
        
        internal static void ShowDialog(string title, string message, DialogButton buttonOk)
        {
            if (EditorUtility.DisplayDialog(title, message, buttonOk.title))
            {
                buttonOk.PerformAction();
            }
        }
        
        internal static void ShowDialog(string title, string message, DialogButton buttonOk, DialogButton buttonCancel)
        {
            if (EditorUtility.DisplayDialog(title, message, buttonOk.title, buttonCancel.title))
            {
                buttonOk.PerformAction();
            }
            else
            {
                buttonCancel.PerformAction();
            }
        }
        
        internal static void ShowDialog(string title, string message, DialogButton buttonOk, DialogButton buttonCancel, DialogButton buttonAlt)
        {
            int choice = EditorUtility.DisplayDialogComplex(title, message, buttonOk.title, buttonCancel.title, buttonAlt.title);
            switch (choice)
            {
                case 0:
                    buttonOk.PerformAction();
                    break;
                case 1:
                    buttonCancel.PerformAction();
                    break;
                case 2:
                    buttonAlt.PerformAction();
                    break;
            }
        }
        
        internal static void ShowMessageDialog(string title, string message)
        {
            EditorUtility.DisplayDialog(title, message, "OK");
        }
        
        #endregion

        #region Dispatcher
        
        private static Queue<Action> s_dispatchQueue = new Queue<Action>();
        
        public static void DispatchOnMainThread(Action action)
        {
            lock (s_dispatchQueue)
            {
                s_dispatchQueue.Enqueue(action);
                if (s_dispatchQueue.Count == 1)
                {
                    EditorApplication.update += RunDispatch;
                }
            }
        }
        
        private static void RunDispatch()
        {
            lock (s_dispatchQueue)
            {
                while (s_dispatchQueue.Count > 0)
                {
                    Action action = s_dispatchQueue.Dequeue();
                    action();
                }
                
                EditorApplication.update -= RunDispatch;
            }
        }
        
        #endregion
    }
}