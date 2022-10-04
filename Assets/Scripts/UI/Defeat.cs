using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
namespace UI
{
    public class Defeat:Window
    {
        [SerializeField] private Button menu;
        public override void Init()
        {
            base.Init();
            menu.onClick.AddListener(() =>
            {
                Close();
                WindowManager.instance.OpenWindow("Menu");
            });
            exitButton.onClick.AddListener(Game.instance.RestartGame);
            
        }
    }
}