using UnityEngine;
using DG.Tweening;

public class MainMenuAnims : MonoBehaviour
{
    public Vector2 ScreenPos;
    public Vector2 OutOfScreenPos;
    private bool IsOnScreen = true;

    public void AnimateButton(GameObject buttonObject) 
    {
        Transform buttonTransform = buttonObject.transform;
        buttonTransform.DOKill();
        buttonTransform.DOScale(1.1f, 0.15f)
            .SetEase(Ease.OutBack)
            .SetLoops(2, LoopType.Yoyo);
    }
    public void AnimatePanel(GameObject panelObject) 
    {
        RectTransform _rectTransform = panelObject.GetComponent<RectTransform>();
        if (IsOnScreen)
        {
            _rectTransform.DOAnchorPos(OutOfScreenPos, 0.5f).
                SetEase(Ease.InOutElastic);
            IsOnScreen = false;
        }
        else 
        {
            _rectTransform.DOAnchorPos(ScreenPos, 0.5f).
                SetEase(Ease.InOutElastic);
            IsOnScreen = true;
        }
    
    }


}
