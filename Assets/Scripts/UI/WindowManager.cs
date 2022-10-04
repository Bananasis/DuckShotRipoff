using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    public static WindowManager instance;
    [SerializeField] private List<Window> windows;
    private Stack<Window> openWindows = new Stack<Window>();

    private void Awake()
    {
        instance = this;
    }

    public void CloseTopWindow()
    {
        if (openWindows.Count == 0) throw new Utils.GameException("No open windows");
        var wd = openWindows.Pop();
        if (!wd.gameObject.activeSelf) Debug.LogError($"Window already closed {wd.windowName} ");
        wd.gameObject.SetActive(false);
        if (openWindows.Count == 0) return;
        openWindows.Peek().gameObject.SetActive(true);
    }

    public void OpenWindow(string name)
    {
        var wd = windows.FirstOrDefault((s) => s.windowName == name);
        if (wd is null) throw new Utils.GameException($"No window with name {name}");
        if (openWindows.Count > 0)
            openWindows.Peek().gameObject.SetActive(false);
        wd.gameObject.SetActive(true);
        openWindows.Push(wd);
    }
}