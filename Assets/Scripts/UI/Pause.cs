using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
namespace UI
{
    public class Pause:Window
    {
        [SerializeField] private Button menu;
        
        public override void Init()
        {
            base.Init();
            
            exitButton.onClick.AddListener(Game.instance.ResumeGame);
            
            menu.onClick.AddListener(() =>
            {
                Close();
                WindowManager.instance.OpenWindow("Menu");
            });
        }
    }
}