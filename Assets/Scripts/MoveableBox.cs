using System.Threading;
using UnityEngine;
using DG.Tweening;
public class MoveableBox : MonoBehaviour
{
    
    void Start()
    {
        transform.DOMoveX(3f, 2f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine); ;
    }

    void Update()
    {
        
         //transform.DOMoveX(3f, 2f).SetLoops(-1, LoopType.Yoyo);     
        
    }
}
