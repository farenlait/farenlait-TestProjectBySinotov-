using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    const int COUNTENEMY = 12;

    public GameObject[] enemy = new GameObject[COUNTENEMY];
    public GameObject mainObj;
    public Text txtEnemySpeed;
    public Text txtEnemyXP;
    public Text countKillEnemy;
    public Text finalCountKillWin;
    public Text finalCountKillLose;

    public int XPEnemy;
    public float speedTime;
    public int killEnemy;

    Enemy[] scriptEnemy = new Enemy[COUNTENEMY];
    Coroutine menegCorotineNewEnemy;
    MainWorld scriptMainObj;

    int lifeTime = 1;

    void Start()
    {
        scriptMainObj = mainObj.GetComponent<MainWorld>();
        for (int i = 0; i < COUNTENEMY; i++)
        {
            scriptEnemy[i] = enemy[i].GetComponent<Enemy>();
        }
    }

    public void startGame()
    {
        lifeTime = 1;
        enemy[0].SetActive(true);
        scriptEnemy[0].randomStart();
        menegCorotineNewEnemy = StartCoroutine(newEnemy());

        if (scriptMainObj.thisLevel == 0)
        {
            XPEnemy = 140;
            speedTime = -0.1f;
            txtEnemySpeed.text = "80%";
            txtEnemyXP.text = "80%";
        }
        else if (scriptMainObj.thisLevel == 1)
        {
            XPEnemy = 170;
            speedTime = 0f;
            txtEnemySpeed.text = "100%";
            txtEnemyXP.text = "100%";
        }
        else 
        {
            XPEnemy = 210;
            speedTime = 0.1f;
            txtEnemySpeed.text = "150%";
            txtEnemyXP.text = "150%";
        }

        killEnemy = 0;
        countKillEnemy.text = "0";
        finalCountKillWin.text = "";
        finalCountKillLose.text = "";
    }

    public void newKillEnemy()
    {
        killEnemy++;
        countKillEnemy.text = killEnemy.ToString();
    }

    public void gameWinOrLose(bool win)
    {
        for (int i = 0; i < COUNTENEMY; i++)
        {
            scriptEnemy[i].deleteEnemy();
        }
        if (win)
        {
            finalCountKillWin.text = killEnemy.ToString();
        }
        else
        {
            StopCoroutine(menegCorotineNewEnemy);
            for (int i = 0; i < enemy.Length; i++)
            {
                enemy[i].SetActive(false);
            }
            finalCountKillLose.text = killEnemy.ToString();
        }
    }

    IEnumerator newEnemy()
    {
        while (lifeTime != 11)
        {
            yield return new WaitForSeconds(11);
            ActiveEnemy();
            lifeTime++;
            if (lifeTime == 11)
            {
                ActiveEnemy();
                StopCoroutine(menegCorotineNewEnemy);
            }
        }
    }

    void ActiveEnemy()
    {
        enemy[lifeTime].SetActive(true);
        scriptEnemy[lifeTime].randomStart();
        speedTime += 0.1f + 0.1f * scriptMainObj.thisLevel;
        XPEnemy += 50 + 25 * scriptMainObj.thisLevel;

        if (scriptMainObj.thisLevel == 0)
        {
            txtEnemySpeed.text = (80 + speedTime * 100).ToString() + "%";
            txtEnemyXP.text = (80 + lifeTime * 35).ToString() + "%";
        }
        else if (scriptMainObj.thisLevel == 1)
        {
            txtEnemySpeed.text = (100 + speedTime * 100).ToString() + "%";
            txtEnemyXP.text = (100 + lifeTime * 40).ToString() + "%";
        }
        else
        {
            txtEnemySpeed.text = (150 + speedTime * 100).ToString() + "%";
            txtEnemyXP.text = (150 + lifeTime * 40).ToString() + "%";
        }
    }

    void noVisableEnemy()
    {
        for (int i = 1; i< enemy.Length; i++)
        {
            enemy[i].SetActive(false);
        }
    }


}
