﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
public class AdsPopup : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textSecond;
    [SerializeField] Button clickAds;
    [SerializeField] Image fillImg;

    private int time;


    private void OnEnable()
    {

        time = 5;
        textSecond.text = time.ToString();

        StartCountDown();

        transform.localScale = new Vector2(0f, 0f);
        transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack).SetUpdate(true);
        fillImg.fillAmount = 1f;
        clickAds.onClick.AddListener(ShowAds);
    }

    public void StartCountDown()
    {
        StartCoroutine(CountDown());
        StartCoroutine(FillImage());
    }

    IEnumerator CountDown()
    {
        yield return new WaitForSecondsRealtime(1f);
        time--;
        textSecond.text = time.ToString();
        if(time>=1)
        {
            StartCoroutine(CountDown());
        }

        if (time == 0)
        {
            transform.DOKill();
            transform.DOScale(0f, 0.2f).SetEase(Ease.InCubic).SetUpdate(true).OnComplete(() => {
                UIManager.Instance.ShowEndGameInfomation();
            }) ;
        }
    }

    IEnumerator FillImage()
    {
        while(time>=0)
        {
            fillImg.fillAmount -= 1/5f * Time.unscaledDeltaTime;
            yield return null;
        }
    }


    void ShowAds()
    {
        Debug.Log("ADS ON");
    }
}