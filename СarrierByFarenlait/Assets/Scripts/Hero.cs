using UnityEngine;

public class Hero : MonoBehaviour
{
    public GameObject mainObj;
    public GameObject[] purticals = new GameObject[4];

    MainWorld mainObjScript;

    private void Start()
    {
        mainObjScript = mainObj.GetComponent<MainWorld>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            mainObjScript.healthDown();
            offHeals();
        }
    }

    void offHeals()
    {
        int key = Random.Range(0,4);
        while (!purticals[key].activeSelf)
        {
            key = Random.Range(0, 4);
        }
        purticals[key].SetActive(false);
    }

    public void startGame()
    {
        for (int i = 0; i < 4; i++)
        {
            purticals[i].SetActive(true);
        }
        transform.position = new Vector3(0, 2, -21.5f);
    }

    public void onLockPartical()
    {
        for (int i = 0; i < 4; i++)
        {
            purticals[i].SetActive(false);
        }
    } 

}
