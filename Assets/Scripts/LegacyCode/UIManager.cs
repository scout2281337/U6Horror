using DG.Tweening;
using TMPro;
using UnityEngine;


public class UIManager : MonoBehaviour
{
    public Vector2 ScreenPos;
    public Vector2 ScreenSize;
    public Vector2 OutScreenPos;
    public Vector2 OutScreenSize;
    private bool IsOnScreen = false;
    private RectTransform RT;
    
    [SerializeField] private TextMeshProUGUI pressFtoPlay; 
    private RectTransform TRt;


    [SerializeField]private TextMeshProUGUI mainT;
    

    [Header("Color Pulse")]
    [SerializeField] private Color baseColor = Color.white;
    [SerializeField] private Color glowColor = new Color(0.2f, 1f, 1f); // бирюзовый

    [Header("Alpha Flicker")]
    [SerializeField] private float minAlpha = 0.5f;
    [SerializeField] private float flickerDuration = 1f;

    [Header("Scale Pulse")]
    [SerializeField] private float scaleAmount = 1.1f;
    [SerializeField] private float scaleDuration = 1f;

    void Start()
    {
        RT = GetComponent<RectTransform>();
        TRt = pressFtoPlay.GetComponent<RectTransform>();
        StartTextAnims();
        // Устанавливаем начальные значения
        mainT.color = baseColor;
        mainT.alpha = 1f;
        mainT.transform.localScale = Vector3.one;

        // 1. Цвет — Перелив
        mainT.DOColor(glowColor, 2f)
              .SetLoops(-1, LoopType.Yoyo)
              .SetEase(Ease.InOutSine);

        // 2. Прозрачность — Мигание/свечение
        mainT.DOFade(minAlpha, flickerDuration)
              .SetLoops(-1, LoopType.Yoyo)
              .SetEase(Ease.InOutQuad);

        // 3. Масштаб — Пульс
        mainT.transform.DOScale(scaleAmount, scaleDuration)
              .SetLoops(-1, LoopType.Yoyo)
              .SetEase(Ease.InOutSine);
    }
    void StartTextAnims() 
    {
        Sequence s = DOTween.Sequence();
        s.Append(TRt.DOScale(0.75f, 1f).SetLoops(2, LoopType.Yoyo));
        s.Append(TRt.DORotate(new Vector3(0, 0, 180), 2f).SetLoops(2, LoopType.Yoyo));
        //TRt.DOScale(0.75f, 1f).SetLoops(-1, LoopType.Yoyo);
        //TRt.DORotate(new Vector3(0, 0, 180), 2f).SetLoops(-1, LoopType.Yoyo);
    }
    void MoveAnimsForText() 
    {
        if (!IsOnScreen)
        {
            TRt.DOAnchorPos(OutScreenPos, 1f).SetEase(Ease.InOutExpo);
        }
        else
        {
            TRt.DOAnchorPos(ScreenPos, 1f).SetEase(Ease.InOutQuad);
        }
    }
    void AnimationForMenu() 
    {
        if (!IsOnScreen)
        {
            RT.DOAnchorPos(ScreenPos, 1f).SetEase(Ease.InOutQuad);
            RT.DOSizeDelta(ScreenSize, 1.5f).SetEase(Ease.InSine);
            MoveAnimsForText();
            IsOnScreen = true;
        }
        else 
        {
            RT.DOAnchorPos(OutScreenPos, 1f).SetEase(Ease.InOutExpo);
            RT.DOSizeDelta(OutScreenSize, 1f).SetEase(Ease.InSine);
            MoveAnimsForText();
            IsOnScreen = false;
        }
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E)) { AnimationForMenu(); }    
    }
}
