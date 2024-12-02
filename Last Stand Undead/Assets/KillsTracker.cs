using UnityEngine;
using TMPro;

public class KillsTracker : MonoBehaviour
{
    public TMP_Text textKillsCount;

    private int kills;
    private bool isKills = false;

    void Start()
    {
        isKills = true;
    }

    void DisplayKills()
    {
        textKillsCount.text = string.Format("{0}", kills);
    }

    void Update()
    {
        if(isKills)
        {
            GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyWaveTracker>().enemiesKilled = kills;
            DisplayKills();
        }
        
    }
}
