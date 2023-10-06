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
    private bool isDropping;
    private Vector3 originalPosition;
    private bool isFading;

    public Button start;
    private bool isBrowsing;
    private Vector3 storedPosition;
    private bool isFlying;

    public void Start()
    {

        originalPosition = image1.transform.position;
    }

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

    public void Drop()
    {
        if (!isDropping)
        {
            Vector3 targetPosition = originalPosition - Vector3.down * 30f;
            image1.transform.DOScale(0, 0.3f);
            image1.transform.DOMove(targetPosition, 0.3f).OnComplete(() => isDropping = true);
            image1.DOFade(0f, 0.3f).OnComplete(() => isFading = true);
        }
        else
        {
            image1.transform.DOMove(originalPosition, 0.3f).OnComplete(() => isDropping = false);
            image1.transform.DOScale(3.6239f, 0.3f);
            image1.DOFade(1f, 0.3f).OnComplete(() => isFading = false);
        }
    }

    public void Browse()
    {
        if (!isBrowsing)
        {
            storedPosition = image1.transform.position;
            Vector3 targetPosition = originalPosition - Vector3.right * 20f;
            Vector3 leftTargetPosition = originalPosition - Vector3.left * 18f;

            image1.transform.DOMove(targetPosition, 0.2f).SetEase(Ease.Linear).OnComplete(() =>
            {
                image1.transform.DOMove(leftTargetPosition, 0.18f).SetEase(Ease.Linear).OnComplete(() =>
                {
                    image1.DOFade(0f, 0.2f).OnComplete(() => isBrowsing = true);
                });
            });
        }
        else
        {
            image1.transform.DOMove(storedPosition, 0.3f).SetEase(Ease.InOutQuad);
            image1.transform.DOScale(0, 0.01f);
            image1.transform.DOScale(3.6239f, 0.3f);
            image1.DOFade(1f, 0.3f).OnComplete(() => isBrowsing = false);
        }
    }

    public void Fly()
    {
        if (!isFlying)
        {
            Vector3 targetPosition = originalPosition;
            targetPosition.x -= 150f;

            image1.transform.DOMove(targetPosition, .3f).SetEase(Ease.InSine).OnComplete(() => isFlying = true);
        }
        else
        {
            image1.transform.DOMove(originalPosition, .8f).SetEase(Ease.OutBounce).OnComplete(() => isFlying = false);
        }
    }


}

