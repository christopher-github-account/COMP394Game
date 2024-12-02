using UnityEngine;
using TMPro;

public class RoundsTracker : MonoBehaviour
{
    public TMP_Text textRoundsCount;

    private int rounds;
    private bool isRounds = false;

    void Start()
    {
        isRounds = true;
    }

    void DisplayRounds()
    {
        textRoundsCount.text = string.Format("{0}", rounds);
    }

    void Update()
    {
        if(isRounds)
        {
            GameObject.FindGameObjectWithTag("WaveSpawner").GetComponent<WaveSpawner>().currWave = rounds;
            DisplayRounds();
        }
        
    }
}
