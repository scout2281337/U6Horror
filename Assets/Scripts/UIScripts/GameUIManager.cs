using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private flashlight_S flashlight_S;
    
    public List<Image> FLImages = new List<Image>();
    private Color  yellowC = new Color(255f / 255f, 230f / 255f, 13f / 255f);

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ChangeSliderValue();
    }
    void ChangeSliderValue()
    {
        float batteryCharge = flashlight_S.FLcharge;
        int activeCount = 0;

        if (batteryCharge > 100) activeCount = 5;
        else if (batteryCharge > 75) activeCount = 4;
        else if (batteryCharge > 50) activeCount = 3;
        else if (batteryCharge > 25) activeCount = 2;
        else if (batteryCharge > 0) activeCount = 1;

        for (int i = 0; i < FLImages.Count; i++)
        {
            FLImages[i].color = i < activeCount ? yellowC : Color.white;
        }
    }
}
