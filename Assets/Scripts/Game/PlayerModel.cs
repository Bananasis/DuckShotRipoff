using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public class PlayerModel
    {
        public int starsCount;
        public int bestRun;
        public List<bool> availableBalls = new List<bool>();
        public int currentBall;
        public bool mute;
        public bool nightMode;
        
        public void SaveModel()
        {
            var json = JsonUtility.ToJson(this);
            PlayerPrefs.SetString("pd", json);
            PlayerPrefs.Save();
            
        }
        
        public void LoadModel()
        {
            var json = PlayerPrefs.GetString("pd");
            JsonUtility.FromJsonOverwrite(json,this);
        }
    }
    
  
}