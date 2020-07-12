using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PrayerUI : MonoBehaviour
{
    public Transform prayerParent;
    public PlayerStats player;
    List<Prayer> prayerList = new List<Prayer>();

    void Start()
    {
        prayerList = prayerParent.GetComponentsInChildren<Prayer>().ToList();

        foreach (var prayer in prayerList)
        {
            prayer.image.sprite = prayer.icon;

            if (prayer.lvlUnlock > player.maxPrayerPoints)
            {
                prayer.image.color = new Color(0.5f, 0.5f, 0.5f);
                prayer.button.enabled = false;
            }
        }
    }
}
