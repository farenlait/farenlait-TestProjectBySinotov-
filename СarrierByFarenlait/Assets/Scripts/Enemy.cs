using UnityEngine;

public class Enemy : MonoBehaviour
{
    const int COUNTVERTICALPOINT = 5;

    public GameObject musicEnemy;
    public ParticleSystem partical;
    public float randomSpeed;

    int XP;
    int randomRotate;

    SpawnEnemy spawnScript;
    Vector3[] verticalLeftPoints = new Vector3[COUNTVERTICALPOINT];
    Vector3[] verticalRightPoints = new Vector3[COUNTVERTICALPOINT];
    Vector3 startPoint;
    Vector3 finishPoint;
    AudioSource boomMusic;

    private void Awake()
    {
        spawnScript = transform.parent.GetComponent<SpawnEnemy>();
    }

    void Start()
    {
        boomMusic = musicEnemy.GetComponent<AudioSource>();

        for (int i = 0; i < 5; i++)
        {
            verticalLeftPoints[i] = new Vector3(-6.6f + 2.2f * i, 1.5f, 9);
            verticalRightPoints[i] = new Vector3(-6.6f + 2.2f * i, 1.5f, -26);
        }

        randomStart();
    }

    public void randomStart()
    {
        XP = spawnScript.XPEnemy;
        randomRotate = Random.Range(0, 3);
        randomSpeed = Random.Range(0.4f, 2f) + spawnScript.speedTime;
        transform.localScale = new Vector3(Random.Range(0.8f, 1.5f), Random.Range(0.8f, 1.5f), Random.Range(0.8f, 1.5f));
        newStartPoint();
        transform.position = startPoint;
    }

    public void deleteEnemy()
    {
        partical.Play();
        Invoke("setActiveObj", 0.5f);
    }

    void setActiveObj()
    {
        gameObject.SetActive(false);
    }

    void newStartPoint()
    {
        startPoint = verticalLeftPoints[Random.Range(0, verticalLeftPoints.Length)];
        finishPoint = verticalRightPoints[Random.Range(0, verticalRightPoints.Length)];
    }

    void Update()
    {
        if (transform.position != finishPoint)
        {
            if (transform.position.x != finishPoint.x)
            {
                transform.position = new Vector3(transform.position.x + ((finishPoint.x - transform.position.x) * Time.deltaTime * randomSpeed/20),
                                                transform.position.y, transform.position.z - Time.deltaTime * randomSpeed);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - Time.deltaTime * randomSpeed);
            }
        }
        if (randomRotate == 0)
        {
            transform.Rotate(1,0,0);
        }
        else if (randomRotate == 0)
        {
            transform.Rotate(0, 1, 0);
        }
        else
        {
            transform.Rotate(0, 0, 1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            boomMusic.Play();
            spawnScript.newKillEnemy();
            partical.Play();
            Invoke("randomStart", 0.05f);
        }
        else if (other.tag == "Bullet")
        {
            Destroy(other.gameObject);
            XP -= Random.Range(80,101);
            if (XP <= 0)
            {
                boomMusic.Play();
                spawnScript.newKillEnemy();
                partical.Play();
                Invoke("randomStart", 0.05f);
            }
        }
        else if (other.tag == "Respawn")
        {
            Invoke("randomStart",0.05f);
        }
    }
}
