using UnityEngine.UI;
using UnityEngine;

public class MainWorld : MonoBehaviour
{
    const float DELTAMOVEEARTH = 0.0028f;

    public Image[] healthImg = new Image[3];
    public Slider lvlProgress;
    public Text finalLvlProgress;

    public GameObject earthObj;
    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject joyObject;
    public GameObject headerPanel;
    public GameObject mainCamera;
    public GameObject menuObj;
    public GameObject headerGame;
    public GameObject heroObj;
    public GameObject spawnEnemy;
    public GameObject lvlPanel;
    public GameObject musicWin;
    public GameObject musicLose;

    SpawnEnemy spawnEnemyScript;
    LVLPanel lvlPanelScript;
    Hero heroScript;
    Joy joyScript;
    AudioSource winPlay;
    AudioSource losePlay;

    public bool flagStopGame = true;
    public int countHealth = 3;
    public int thisLevel = 0;
    public float lvlProgressMath;

    private void Start()
    {
        losePlay = musicLose.GetComponent<AudioSource>();
        winPlay = musicWin.GetComponent<AudioSource>();
        spawnEnemyScript = spawnEnemy.GetComponent<SpawnEnemy>();
        lvlPanelScript = lvlPanel.GetComponent<LVLPanel>();
        heroScript = heroObj.GetComponent<Hero>();
        joyScript = joyObject.GetComponent<Joy>();
    }

    private void FixedUpdate()
    {
        if (!flagStopGame)
        {
            lvlProgress.value++;
            earthObj.transform.position = new Vector3(0, 0, earthObj.transform.position.z - DELTAMOVEEARTH);
            if (lvlProgress.value == lvlProgress.maxValue)
            {
                gameWin();
            }
        }
    }

    public void startGame()
    {
        mainCamera.transform.position = new Vector3(0, 28, -9);
        mainCamera.transform.eulerAngles = new Vector3(90, 0, 0);
        earthObj.transform.position = Vector3.zero;

        menuObj.SetActive(false);
        joyObject.SetActive(true);
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        headerPanel.SetActive(true);

        finalLvlProgress.text = "";
        countHealth = 3;
        flagStopGame = false;
        lvlProgress.value = 0;

        heroScript.enabled = true;
        heroScript.startGame();
        spawnEnemyScript.startGame();
        joyScript.startGame();
        if (thisLevel == 0)
        {
            joyScript.speedHero = 6;
        }
        else if (thisLevel == 1)
        {
            joyScript.speedHero = 5;
        }
        else if (thisLevel == 2)
        {
            joyScript.speedHero = 3;
        }

        drawHealth();
    }

    public void startMenu()
    {
        mainCamera.transform.position = new Vector3(50, 28, -9);
        mainCamera.transform.eulerAngles = new Vector3(0, 0, 0);

        winPanel.SetActive(false);
        losePanel.SetActive(false);
        headerPanel.SetActive(false);
        menuObj.SetActive(true);
        lvlPanel.SetActive(true);
        lvlPanelScript.startMenu();
        heroScript.onLockPartical();
    }

    public void gameWin()
    {
        heroScript.enabled = false;
        winPlay.Play();
        flagStopGame = true;
        lvlProgress.value = 0;
        spawnEnemyScript.gameWinOrLose(true);
        joyObject.SetActive(false);
        winPanel.SetActive(true);
    }

    public void healthDown()
    {
        countHealth--;
        chekGame();
    }

    void chekGame()
    {
        if(countHealth <= -1)
        {
            gameLose();
        }
        else
        {
            drawHealth();
        }
    }

    public void gameLose()
    {
        heroScript.enabled = false;
        losePlay.Play();
        flagStopGame = true;
        spawnEnemyScript.gameWinOrLose(false);
        joyObject.SetActive(false);
        losePanel.SetActive(true);
        lvlProgressMath = Mathf.Round((lvlProgress.value / lvlProgress.maxValue * 100));
        finalLvlProgress.text = lvlProgressMath.ToString() + "%";
    }

    void drawHealth()
    {
        for (int i = 0; i < 3; i++)
        {
            healthImg[i].color = new Color(healthImg[i].color.r, healthImg[i].color.g, healthImg[i].color.b, 0.25f);
        }

        for (int i = 0; i < countHealth; i++)
        {
            healthImg[i].color = new Color(healthImg[i].color.r, healthImg[i].color.g, healthImg[i].color.b, 1);
        }
    }
}
