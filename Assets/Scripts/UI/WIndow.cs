using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class Window : MonoBehaviour
{
    public string windowName;
    [SerializeField] protected Button exitButton;
    
    private bool closing;

    public virtual void Init()
    {
        if (exitButton == null)
        {
            Debug.LogWarning($"No exit Button on {windowName}");
            return;
        }

        exitButton.onClick.AddListener(Close);
    }

    public void Close()
    {
        if (closing) return;
        closing = true;
        WindowManager.instance.CloseTopWindow();
    }

    private void Start()
    {
        Init();
    }

    private void OnEnable()
    {
        closing = false;
    }
}