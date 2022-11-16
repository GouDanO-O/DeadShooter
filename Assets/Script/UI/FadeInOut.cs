using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    private float fadeSpeed = 0.25f;             //渐隐渐现的速率
    private bool sceneStarting = true;          //场景是否开始
    private RawImage rawImage;

    private void Awake()
    {
        this.gameObject.SetActive(true);
    }

    void Start()
    {
        RectTransform rectTransform = this.GetComponent<RectTransform>();
        //使背景满屏
        rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
        rawImage = this.GetComponent<RawImage>();
        rawImage.uvRect = new Rect(0, 0, Screen.width, Screen.height);
        rawImage.enabled = true;
    }

    void Update()
    {
        if (sceneStarting)
        {
            StartScene();
        }
    }
    private void FadeToClear()
    {
        rawImage.color = Color.Lerp(rawImage.color, Color.clear, fadeSpeed * Time.deltaTime);
    }

    private void FadeToBlack()
    {
        rawImage.color = Color.Lerp(rawImage.color, Color.black, fadeSpeed * Time.deltaTime);
    }

    private void StartScene()
    {
        FadeToClear();
        if (rawImage.color.a <= 0.05f)
        {
            rawImage.color = Color.clear;
            rawImage.enabled = false;
            sceneStarting = false;
        }
    }

    public void EndScene()
    {
        rawImage.enabled = true;
        FadeToBlack();
        if (rawImage.color.a >= 0.95f)
        {
            SceneManager.LoadScene("GameSense_City01");
        }
    }
}

