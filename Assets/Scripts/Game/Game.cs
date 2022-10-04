using System;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using Random = System.Random;

public class Game : MonoBehaviour
{
    [SerializeField] private Ball ball;
    [SerializeField] private Net net;
    [SerializeField] private Net slingshot;
    [SerializeField] private Camera cam;
    [SerializeField] private StarPool sp;
    [SerializeField] private Prediction prediction;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Slider slider;
    [SerializeField] private TextManager tm;
    [SerializeField] private WindowManager wm;
    [SerializeField] private TextMeshProUGUI scoreLabel;
    [SerializeField] private TextMeshProUGUI recordLabel;
    [SerializeField] private TextMeshProUGUI starCounter;
    [SerializeField] private ThemeResolver tr;
    private Random rand = new Random((int) DateTime.Now.Ticks);
    public GameModel gm;
    public PlayerModel pm;
    public static Game instance;

    [HideInInspector] public bool perfect = true;
    [HideInInspector] public bool bounce;
    [HideInInspector] public bool record;

    private int combo;
    private int _score;

    public void Loose()
    {
        WindowManager.instance.OpenWindow("Defeat");
        if (record) return;
        recordLabel.text = $"Best score\n{pm.bestRun}";
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        pm.SaveModel();
    }
    private void OnApplicationQuit()
    {
        pm.SaveModel();
    }

    public void SetNightMode(bool mode)
    {
        pm.nightMode = mode;
        tr.Swith(mode);
    }

    public void SetMute(bool mode)
    {
        pm.mute = mode;
        SoundManager.instance.mute = mode;
    }

    private int score
    {
        get => _score;
        set
        {
            _score = value;
            scoreLabel.text = $"{_score}";
            if (_score <= pm.bestRun) return;
            pm.bestRun = _score;
            recordLabel.text = $"New record\n{_score}";
            if (record) return;
            record = true;
            SoundManager.instance.Play("Congratulations");
        }
    }

    public void AddStar()
    {
        pm.starsCount += 1;
        starCounter.text = $"{pm.starsCount}";
    }

    public void Pause()
    {
        WindowManager.instance.OpenWindow("Pause");
        Time.timeScale = 0;
    }
    public void ResumeGame ()
    {
        Time.timeScale = 1;
    }

    private void Awake()
    {
        
        instance = this;
        pm.LoadModel();

        gm.camWorldSize.SetMinMax(cam.ViewportToWorldPoint(new Vector3()), cam.ViewportToWorldPoint(new Vector3(1, 1)));
    }

    private void Start()
    {
        SetNightMode(pm.nightMode);
        SetMute(pm.mute);
        WindowManager.instance.OpenWindow("Menu");
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        pm.SaveModel();
        Time.timeScale = 1;
        starCounter.text = $"{pm.starsCount}";
        score = 0;
        perfect = true;
        bounce = false;
        recordLabel.text = $"";
        var transform1 = slingshot.transform;
        transform1.position = spawnPoint.position;
        transform1.rotation = Quaternion.identity;
        ball.Spawn(slingshot.slingPoint);
        slider.SetToStart();
        RespawnNet();
    }

    public void Pull(Vector2 start, Vector2 end)
    {
        var wStart = cam.ScreenToWorldPoint(start);
        var wEnd = cam.ScreenToWorldPoint(end);
        var pullVector = wEnd - wStart;
        if (pullVector == Vector3.zero) return;
        var vec = pullVector.normalized;
        var speed = pullVector.magnitude;

        var rotation = Quaternion.LookRotation(Vector3.forward, -vec);
        var stretchScaled = 1 - 1 / (1 + speed);
        slingshot.Pull(stretchScaled, rotation);


        if (speed > gm.maxSpeed) speed = gm.maxSpeed;
        if (speed < gm.minSpeed)
        {
            prediction.HideLine();
            return;
        }

        prediction.UpdateLine(ball.transform.position, -vec * speed * gm.speedC, speed / gm.maxSpeed);
    }

    public void Magnetize(Net catcher)
    {
        SoundManager.instance.Play("Catch");
        ball.Magnetize(catcher.slingPoint);
        if (catcher == slingshot) return;
        net = slingshot;
        slingshot = catcher;
        if (perfect)
        {
            tm.Spawnperfect(ball.transform.position);
            combo += 1;
        }
        else
        {
            combo = 1;
        }

        if (bounce) tm.SpawnBounce(ball.transform.position);
        RespawnNet();
        var points = bounce ? combo * 2 : combo;
        score += points;
        perfect = true;
        bounce = false;
        tm.SpawnplusOne(ball.transform.position, points);
    }


    private void RespawnNet()
    {
        net.transform.rotation = Quaternion.LookRotation(Vector3.forward,
            Vector3.up + Vector3.left * (float) (rand.NextDouble() - 0.5));
        float range = gm.camWorldSize.size.x - 2 * gm.basketWallDistance;
        float newX = gm.camWorldSize.min.x + gm.basketWallDistance + (float) (rand.NextDouble() * range);
        if (Mathf.Abs(newX - slingshot.transform.position.x) < gm.slingshotNeMinDistance)
        {
            if (newX > slingshot.transform.position.x)
                newX = (newX + gm.slingshotNeMinDistance < gm.camWorldSize.max.x - gm.basketWallDistance)
                    ? newX + gm.slingshotNeMinDistance
                    : slingshot.transform.position.x - gm.slingshotNeMinDistance;
            else
                newX = (newX - gm.slingshotNeMinDistance > gm.camWorldSize.min.x + gm.basketWallDistance)
                    ? newX - gm.slingshotNeMinDistance
                    : slingshot.transform.position.x + gm.slingshotNeMinDistance;
        }
        
        net.transform.rotation = Quaternion.LookRotation(Vector3.forward,
            Vector3.up + Vector3.left * ((float) (rand.NextDouble() - 0.5) + ( newX -slingshot.transform.position.x  )/8));
        net.transform.position = new Vector3(
            newX,
            (float) (rand.NextDouble() * 1) + gm.basketSpawnOffset + slingshot.transform.position.y,
            0);

        ;
        if (rand.NextDouble() < gm.starChance)
        {
            sp.Spawn(net.transform.position + net.transform.up * gm.starOffset);
        }
    }

    public void Shoot(Vector2 start, Vector2 end)
    {
        slingshot.Unstretch();
        prediction.HideLine();
        var wStart = cam.ScreenToWorldPoint(start);
        var wEnd = cam.ScreenToWorldPoint(end);
        var pullVector = wEnd - wStart;
        var vec = pullVector.normalized;
        var speed = pullVector.magnitude;
        if (speed < gm.minSpeed)
            return;
        if (speed > gm.maxSpeed) speed = gm.maxSpeed;

        ball.MakeShot(-vec * speed * gm.speedC);
    }
}