using UnityEngine;

public class FirstClick : MonoBehaviour
{
    public GameObject NameGame;
    public GameObject LVLPanel;
    public GameObject TAPmus;

    private void OnMouseUp()
    {
        TAPmus.GetComponent<AudioSource>().Play();
        LVLPanel.SetActive(true);
        Destroy(NameGame);
    }
}
