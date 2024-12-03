using UnityEngine;
using TMPro;

public class KillsTracker : MonoBehaviour
{
    public TMP_Text textKillsCount;

    public int EnemiesKilledCount;

    void Start()
    {
        EnemiesKilledCount = 0;
    }


    void Update()
    {
        DisplayKills();
        
    }

        void DisplayKills()
    {
        textKillsCount.text = string.Format("{0}", EnemiesKilledCount);
    }
}
