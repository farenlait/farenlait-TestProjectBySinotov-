using UnityEngine;

public class Joy : MonoBehaviour
{
    float minPosX = -5.9f;
    float minPosY = -21.5f;
    float maxPosX = 5.9f;
    float maxPosY = 0;

    public Transform transformHero;
    public Transform keyStartBullet;
    public GameObject objectBullet;
    public GameObject bulletMusic;

    Vector3 mousePosition;
    Vector3 newPosition;
    AudioSource music;

    bool flagMoveHero = false;
    bool flagPossibilityBullet = true;

    public float speedHero = 6;

    float distance = 25;
    float speedAttack = 0.5f;

    private void Start()
    {
        music = bulletMusic.GetComponent<AudioSource>();
        newPosition = new Vector3(1.665f, 2, -22f);
        if ((float)Screen.width / Screen.height == (float)9 /16)
        {
            minPosX = -6.9f;
            maxPosX = 6.9f;
        }
    }

    public void startGame()
    {
        flagMoveHero = false;
        flagPossibilityBullet = true;
        newPosition = new Vector3(1.665f, 2, -22f);
    }

    private void OnMouseDown()
    {
        flagMoveHero = true;
    }

    void OnMouseDrag()
    {
        mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        newPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        newPosition = new Vector3(newPosition.x, 2, newPosition.z);
        Shot();
    }

    void Shot()
    {
        if (flagPossibilityBullet)
        {
            music.Play();
            Instantiate(objectBullet, keyStartBullet.position, Quaternion.identity);
            flagPossibilityBullet = false;
            Invoke("keyFlagPossibilityBullet", speedAttack);
        }
    }

    void keyFlagPossibilityBullet()
    {
        flagPossibilityBullet = true;
    }

    private void Update()
    {
        if (flagMoveHero)
        {
            if (transformHero.position != newPosition)
            {
                transformHero.position = new Vector3(Mathf.Clamp(transformHero.position.x + ((newPosition.x - transformHero.position.x) * Time.deltaTime * speedHero), minPosX, maxPosX), 2,
                                                      Mathf.Clamp(transformHero.position.z + ((newPosition.z - transformHero.position.z) * Time.deltaTime * speedHero), minPosY, maxPosY));
            }
            else
            {
                flagMoveHero = false;
            }
        }
    }
}


