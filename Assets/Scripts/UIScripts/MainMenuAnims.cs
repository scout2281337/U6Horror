using UnityEngine;
using DG.Tweening;

public class MainMenuAnims : MonoBehaviour
{

    [Header("For mainmenu panel")]
    public Vector2 ScreenPos;
    public Vector2 OutOfScreenPos;
    private bool IsOnScreen = true;

    [Header("For settings menu panel")] 
    public Vector2 ScreenPos2;
    public Vector2 OutOfScreenPos2;

    [Header("Ease settings")]
    public Ease buttonEase = Ease.OutBack;
    public Ease panelEase = Ease.InOutExpo;
    public void AnimateButton(GameObject buttonObject) 
    {
        Transform buttonTransform = buttonObject.transform;
        buttonTransform.DOKill();
        buttonTransform.DOScale(1.1f, 0.15f)
            .SetEase(buttonEase)
            .SetLoops(2, LoopType.Yoyo);
    }
    public void AnimatePanel(GameObject panelObject) 
    {
        RectTransform _rectTransform = panelObject.GetComponent<RectTransform>();
        if (IsOnScreen)
        {
            _rectTransform.DOAnchorPos(OutOfScreenPos, 0.5f).
                SetEase(panelEase); //Ease.InOutElastic с небольшой тряской
            IsOnScreen = false;
            panelObject.transform.DOScale(0.3f, 0.5f);
        }
        else 
        {
            _rectTransform.DOAnchorPos(ScreenPos, 0.5f).
                SetEase(panelEase);
            panelObject.transform.DOScale(0.75f, 0.5f);
            IsOnScreen = true;
        }
    
    }
    public void AnimateSettingsPanel(GameObject panelObject) 
    {
        RectTransform _rectTransform = panelObject.GetComponent<RectTransform>();
        if (!IsOnScreen)
        {
            _rectTransform.DOAnchorPos(OutOfScreenPos2, 0.5f).
                SetEase(panelEase);
            panelObject.transform.DOScale(1f, 0.5f);

        }
        else
        {
            _rectTransform.DOAnchorPos(ScreenPos2, 0.5f).
                SetEase(panelEase);
            panelObject.transform.DOScale(0.75f, 0.5f);
        }
    }


}
