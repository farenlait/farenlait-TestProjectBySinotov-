using UnityEngine.UI;
using UnityEngine;

public class ButtonGoMenu : MonoBehaviour
{
    public GameObject mainObj;
    public GameObject lvlPanel;
    public GameObject spawnEnemy;
    public GameObject musicTap;

    int countLvl;

    Image thisImage;
    MainWorld mainObjScript;
    LVLPanel lvlPanelScript;
    SpawnEnemy spawnEnemyScript;
    AudioSource tap;

    private void Start()
    {
        tap = musicTap.GetComponent<AudioSource>();
        thisImage = GetComponent<Image>();
        mainObjScript = mainObj.GetComponent<MainWorld>();
        lvlPanelScript = lvlPanel.GetComponent<LVLPanel>();
        spawnEnemyScript = spawnEnemy.GetComponent<SpawnEnemy>();
    }

    private void OnMouseDown()
    {
        thisImage.color = new Color(0.5f,0.75f,1);
    }

    private void OnMouseUp()
    {
        tap.Play();
        thisImage.color = Color.white;
        if (this.name == "ButtonGoMenuWin")
        {
            if (spawnEnemyScript.killEnemy > lvlPanelScript.levelDate.level[mainObjScript.thisLevel].informationAsteroid)
            {
                lvlPanelScript.levelDate.level[mainObjScript.thisLevel].informationAsteroid = spawnEnemyScript.killEnemy;
            }
            if (mainObjScript.thisLevel < 2)
            {
                lvlPanelScript.levelDate.level[mainObjScript.thisLevel + 1].informationActiveLVL = true;
            }
            lvlPanelScript.levelDate.level[mainObjScript.thisLevel].informationProgress = 100;
            lvlPanelScript.startMenu();
            mainObjScript.startMenu();
        }
        else
        {
            if (spawnEnemyScript.killEnemy > lvlPanelScript.levelDate.level[mainObjScript.thisLevel].informationAsteroid)
            {
                lvlPanelScript.levelDate.level[mainObjScript.thisLevel].informationAsteroid = spawnEnemyScript.killEnemy;
            }
            if (mainObjScript.lvlProgressMath > lvlPanelScript.levelDate.level[mainObjScript.thisLevel].informationProgress)
            {
                lvlPanelScript.levelDate.level[mainObjScript.thisLevel].informationProgress = (int)mainObjScript.lvlProgressMath;
            }
            lvlPanelScript.startMenu();
            mainObjScript.startMenu();
        }
    }
}
