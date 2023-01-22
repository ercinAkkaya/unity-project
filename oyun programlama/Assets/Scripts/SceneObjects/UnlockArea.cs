using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnlockArea : MonoBehaviour
{
    public UnlockData unlockableData;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI PriceText;
    public Image fillBar;
    public List<GameObject> ObjectsToUnlock = new List<GameObject>();
    

    private void OnEnable()
    {
        CheckUnlocked();
        fillBar.fillAmount = (float)unlockableData.CollectedPrice / (float)unlockableData.price;
    }

    void Start()
    { 
        ObjectsToUnlock.ForEach((x) => x.SetActive(false));
        //ObjectToUnlock.SetActive(false);
        NameText.text = "UNLOCK " + unlockableData.unlockableName.ToUpper();
        PriceText.text = unlockableData.RemainingPrice.ToString();  
    }

    public void Pay(Stashable stashable)
    {
        if (unlockableData.RemainingPrice <= 0)
            return;

        unlockableData.CollectedPrice++;
        fillBar.fillAmount = (float)unlockableData.CollectedPrice / (float)unlockableData.price;
        stashable.PayStashable(transform, PaymentCompleted);
         
    }

    private void PaymentCompleted()
    {
        PriceText.text = unlockableData.RemainingPrice.ToString();
        CheckUnlocked();
    }

    private void CheckUnlocked()
    {
        if (unlockableData.RemainingPrice > 0) return;
        ObjectsToUnlock.ForEach((x) =>
        {
            x.SetActive(true);
        });
        gameObject.SetActive(false);
    }
}
