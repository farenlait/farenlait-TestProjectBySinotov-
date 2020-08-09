using UnityEngine;
using UnityEngine.UI;

public class LVLPanel : MonoBehaviour
{
    const int COUNTLVL = 3;

    [SerializeField] public InformationLvl levelDate;

    public GameObject[] lvlObject = new GameObject[COUNTLVL];
    public GameObject buttonNext;
    public GameObject buttonBack;
    public GameObject buttonStart;
    public GameObject mainObj;
    public GameObject lockPanel;

    MainWorld mainObjScript;

    int thisActivePanel = 0;

    public Text[] asteroidTxt = new Text[COUNTLVL];
    public Text[] progressTxt = new Text[COUNTLVL];

    private void Awake()
    {
        for (int i = 0; i < COUNTLVL; i++)
        {
            if (!PlayerPrefs.HasKey("progress" + i))
            {
                PlayerPrefs.SetInt("progress" + i, 0);
            }
            if (!PlayerPrefs.HasKey("asteroid" + i))
            {
                PlayerPrefs.SetInt("asteroid" + i, 0);
            }
            if (!PlayerPrefs.HasKey("active" + i))
            {
                PlayerPrefs.SetInt("active" + i, 0);
            }
        }
        loadDate();
    }

    void loadDate()
    {
        for (int i = 0; i < COUNTLVL; i++)
        {
            levelDate.level[i].informationProgress = PlayerPrefs.GetInt("progress" + i);
            levelDate.level[i].informationAsteroid = PlayerPrefs.GetInt("asteroid" + i);
            levelDate.level[i].informationActiveLVL = PlayerPrefs.GetInt("active" + i) == 0 ? false : true;
        }
    }

    public void saveDate()
    {
        for (int i = 0; i < COUNTLVL; i++)
        {
            PlayerPrefs.SetInt("progress" + i , levelDate.level[i].informationProgress);
            PlayerPrefs.SetInt("asteroid" + i, levelDate.level[i].informationAsteroid);
            PlayerPrefs.SetInt("active" + i, levelDate.level[i].informationActiveLVL ? 1 : 0);
        }
    }

    private void Start()
    {
        mainObjScript = mainObj.GetComponent<MainWorld>();
        levelDate.level[0].informationActiveLVL = true;
        startMenu();
    }

    public void startMenu()
    {
        for (int i = 0; i < COUNTLVL; i++)
        {
            lvlObject[i].SetActive(false);
        }
        for (int i = 0; i < COUNTLVL; i++)
        {
            asteroidTxt[i].text = levelDate.level[i].informationAsteroid.ToString();
            progressTxt[i].text = levelDate.level[i].informationProgress.ToString() + "%";
        }
        openActiveLevel();
        saveDate();
    }

    void openActiveLevel()
    {
        for (int i = COUNTLVL - 1; i>=0;i--)
        {
            if (levelDate.level[i].informationActiveLVL)
            {
                thisActivePanel = i;
                break;
            }
        }

        lvlObject[thisActivePanel].SetActive(true);

        if (thisActivePanel == 0)
        {
            buttonBack.SetActive(false);
            buttonNext.SetActive(true);
        }
        else if (thisActivePanel == 1)
        {
            buttonBack.SetActive(true);
            buttonNext.SetActive(true);
        }
        else if (thisActivePanel == 2)
        {
            buttonBack.SetActive(true);
            buttonNext.SetActive(false);
        }
        lockPanel.SetActive(false);
        buttonStart.SetActive(true);
    }

    public void nextPanel()
    {
        thisActivePanel++;
        lvlObject[thisActivePanel - 1].SetActive(false);
        lvlObject[thisActivePanel].SetActive(true);
        if (thisActivePanel>0)
        {
            buttonBack.SetActive(true);
        }
        if (thisActivePanel == COUNTLVL - 1)
        {
            buttonNext.SetActive(false);
        }
        chekActiveLVL();
    }

    public void backPanel()
    {
        thisActivePanel--;
        lvlObject[thisActivePanel + 1].SetActive(false);
        lvlObject[thisActivePanel].SetActive(true);
        if (thisActivePanel < COUNTLVL - 1)
        {
            buttonNext.SetActive(true);
        }
        if (thisActivePanel == 0)
        {
            buttonBack.SetActive(false);
        }
        chekActiveLVL();
    }

    void chekActiveLVL()
    {
        if (!levelDate.level[thisActivePanel].informationActiveLVL)
        {
            lockPanel.SetActive(true);
            buttonStart.SetActive(false);
        }
        else
        {
            lockPanel.SetActive(false);
            buttonStart.SetActive(true);
        }
    }

    public void startGame()
    {
        mainObjScript.thisLevel = thisActivePanel;
        mainObjScript.startGame();
        this.gameObject.SetActive(false);
    }

}
