using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    public Image image1;
    public float dur;
    public float Fadedur;
    public Vector3 targetrotation;
    public float TargetSize;
    public float TargetTime;
    public Vector3 TargetPos;
    public float MoveSpeed;

    public void Fade()
    {
        image1.DOFade(0, Fadedur);
        /* NOTES
         - Color (R,G,B,A(transparency)
         - 
        */
    }

    public void transitionz()
    {
        Sequence sequence = DOTween.Sequence();

        //first task
        sequence.Append(image1.transform.DOLocalMoveX(-10, 3f)); //image will move
        //delay
        sequence.Append(image1.DOFade(0, Fadedur));

    }

    public void FadeUp()
    {
        Sequence sequence = DOTween.Sequence();

        //first task
        sequence.Append(image1.transform.DOLocalMoveY(10, 3f)); //image will move
        //delay
        sequence.Append(image1.DOFade(0, Fadedur));

    }

    public void Rotate()
    {
        //image1.transform.DOMove(TargetPos, MoveSpeed);
        image1.transform.DOLocalRotate(targetrotation, dur).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);

        /* NOTES
        - "ButtonManager">set z and duration >go to button and change "OnClick"
        - SetLoops(-1{sets rotation to infinite},LoopType."Incremental"{does full rotation} "Yoyo"{gpes back and forth})
        */
    }

    public void ResizeDown()
    {
        image1.transform.DOScale(TargetSize, TargetTime).SetEase(Ease.InOutElastic).OnComplete(() => Destroy(image1));
    }

    public void ChangePosition()
    {
        image1.transform.DOMove(TargetPos, MoveSpeed).SetEase(Ease.OutExpo).OnComplete(() => ResizeDown());
    }




}

