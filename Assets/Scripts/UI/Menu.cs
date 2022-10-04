using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Menu:Window
    {
        [SerializeField] private Button play;
        [SerializeField] private Button settings;
        [SerializeField] private Button customize;
        [SerializeField] private Button exitGame;
        
        public override void Init()
        {
            base.Init();
            play.onClick.AddListener(() =>
            {
                Close();
                Game.instance.RestartGame();
            });
            settings.onClick.AddListener(() =>
            {
                WindowManager.instance.OpenWindow("Settings");
            });
            exitGame.onClick.AddListener(Application.Quit);
        }
    }
}