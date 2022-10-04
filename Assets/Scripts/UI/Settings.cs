using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
namespace UI
{
    
    public class Settings:Window
    {
        [SerializeField] private Toggle nightMode;
        [SerializeField] private Toggle sound;
        
        public override void Init()
        {
            base.Init();
            nightMode.isOn = Game.instance.pm.nightMode;
            sound.isOn = !Game.instance.pm.mute;
            nightMode.onValueChanged.AddListener(Game.instance.SetNightMode);
            sound.onValueChanged.AddListener((m) => Game.instance.SetMute(!m));
        }
    }
}