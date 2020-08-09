using UnityEngine;
using UnityEngine.UI;

public class ButtonNextBackLVL : MonoBehaviour
{
    public GameObject musicTAP;

    Image thisImage;
    LVLPanel lvlPanelScript;
    AudioSource tap;

    private void Start()
    {
        thisImage = GetComponent<Image>();
        lvlPanelScript = transform.parent.GetComponent<LVLPanel>();
        tap = musicTAP.GetComponent<AudioSource>();
    }

    private void OnMouseDown()
    {
        thisImage.color = new Color(0.5f, 0.75f, 1);
    }

    private void OnMouseUp()
    {
        tap.Play();
        thisImage.color = Color.white;
        if (this.name == "ButtonNextMission")
        {
            lvlPanelScript.nextPanel();
        }
        else if (this.name == "ButtonBackMission")
        {
            lvlPanelScript.backPanel();
        }
        else
        {
            lvlPanelScript.startGame();
        }
    }
}
