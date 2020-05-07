using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoopSubtitles : MonoBehaviour
{
    [SerializeField] private RectTransform background;
    [SerializeField] private RectTransform text;
    [SerializeField] private float loopSpeed = 10;
    private ContentSizeFitter contentSizeFitter;
    private RectTransform text2;
    private float textPosY;
    private bool showText1 = true;
    private bool showText2 = false;
    float backgroundWidth;
    float textWidth;

    public void UpdateSubtitileContent(string data)
    {
        text.GetComponent<Text>().text = data;
        StartCoroutine(ResetTextWidth());
    }

    IEnumerator ResetTextWidth()
    {
        contentSizeFitter.enabled = true;
        yield return null;
        contentSizeFitter.enabled = false;
        yield return null;
        Initialize();
    }

    private void Initialize()
    {
        //With ContentSizeFitter component, can not get the correct with of the text
        textWidth = text.sizeDelta.x;

        //If the text width is short, then we don't need loop.
        if (textWidth > backgroundWidth)
        {
            if (text2 != null)
                Destroy(text2.gameObject);
            text2 = Instantiate(text.gameObject, background.transform).GetComponent<RectTransform>();
            text2.anchoredPosition = new Vector2(background.sizeDelta.x, textPosY);

            StartCoroutine(MoveText1());
        }
    }

    private void Start()
    {
        contentSizeFitter = text.GetComponent<ContentSizeFitter>();
        backgroundWidth = background.sizeDelta.x;
        textPosY = text.anchoredPosition.y;
        StartCoroutine(ResetTextWidth());
    }

    private IEnumerator MoveText1()
    {
        text2.anchoredPosition = new Vector2(backgroundWidth, textPosY);

        while (showText1)
        {
            if (text.anchoredPosition.x > backgroundWidth - textWidth)
            {
                text.anchoredPosition = new Vector2(text.anchoredPosition.x - loopSpeed * Time.deltaTime, textPosY);
            }
            else if(text2.anchoredPosition.x > 0)
            {
                text.anchoredPosition = new Vector2(text.anchoredPosition.x - loopSpeed * Time.deltaTime, textPosY);
                text2.anchoredPosition = new Vector2(text2.anchoredPosition.x - loopSpeed * Time.deltaTime, textPosY);
            }
            else if(text2.anchoredPosition.x <= 0)
            {
                showText1 = false;
                showText2 = true;
                StartCoroutine(MoveText2());
            }
            yield return null;
        }
        yield return null;
    }

    private IEnumerator MoveText2()
    {
        text.anchoredPosition = new Vector2(backgroundWidth, textPosY);

        while (showText2)
        {
            if (text2.anchoredPosition.x > backgroundWidth - textWidth)
            {
                text2.anchoredPosition = new Vector2(text2.anchoredPosition.x - loopSpeed * Time.deltaTime, textPosY);
            }
            else if (text.anchoredPosition.x > 0)
            {
                text.anchoredPosition = new Vector2(text.anchoredPosition.x - loopSpeed * Time.deltaTime, textPosY);
                text2.anchoredPosition = new Vector2(text2.anchoredPosition.x - loopSpeed * Time.deltaTime, textPosY);
            }
            else if (text.anchoredPosition.x <= 0)
            {
                showText1 = true;
                showText2 = false;
                StartCoroutine(MoveText1());
            }
            yield return null;
        }
        yield return null;
    }
}
