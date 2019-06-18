using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CanvasGroup))]
public class PanelChanger : MonoBehaviour
{
    public enum FadeMode
    {
        None,
        Fade
    }

    public enum FadeDirection
    {
        Top,
        Right,
        Left,
        Bottom,
        None
    }

    private enum PlayingState
    {
        Stop,
        FadeIn,
        FadeOut
    }

    private RectTransform rtf;
    private GameObject parent;
    private string panel_name;
    private bool isPlaying = false;
    private float progress = 0.0f;
    private PlayingState nowState = PlayingState.Stop;
    private Coroutine nowCoroutine = null;
    private int directionX = 0, directionY = 0;
    private CanvasGroup canvasGroup;

    public FadeMode fadeInMode = FadeMode.None;
    public FadeDirection fadeIn = FadeDirection.Top;
    public float fadeInTime = 1.0f;
    public float fadeInDelay = 0.0f;
    public float fadeInDistance = 800.0f;
    public bool fadeInAlpha = true;
    public AnimationCurve fadeInAnimationCurve = AnimationCurve.Linear(0, 0, 1, 1);
    public UnityEvent onStart;
    public UnityEvent onFinishFadeIn;

    public FadeMode fadeOutMode = FadeMode.None;
    public FadeDirection fadeOut = FadeDirection.Bottom;
    public float fadeOutTime = 1.0f;
    public float fadeOutDelay = 0.0f;
    public float fadeOutDistance = 800.0f;
    public bool fadeOutAlpha = true;
    public AnimationCurve fadeOutAnimationCurve = AnimationCurve.Linear(0, 0, 1, 1);
    public UnityEvent onStartFadeOut;
    public UnityEvent onFinish;

    void Awake()
    {
        rtf = this.gameObject.GetComponent<RectTransform>();
        parent = transform.parent.gameObject;
        canvasGroup = this.gameObject.GetComponent<CanvasGroup>();
    }

    void OnEnable()
    {
        StartChange();
    }

    void OnDisable()
    {
        Stop();
    }

    void Update()
    {
        if (!isPlaying) { return; }
        switch (nowState)
        {
            case PlayingState.Stop: return;
            case PlayingState.FadeIn:
                {
                    float curve = fadeInAnimationCurve.Evaluate(progress);
                    rtf.anchoredPosition = new Vector2(directionX * fadeInDistance * (1.0f - curve), directionY * fadeInDistance * (1.0f - curve));
                    if (fadeInAlpha) { canvasGroup.alpha = curve; }
                    break;
                }
            case PlayingState.FadeOut:
                {
                    float curve = fadeInAnimationCurve.Evaluate(progress);
                    rtf.anchoredPosition = new Vector2(directionX * fadeInDistance * curve, directionY * fadeInDistance * curve);
                    if (fadeOutAlpha) { canvasGroup.alpha = (1.0f - curve); }
                    break;
                }
        }
    }

    //-- パネルの出現
    public void PanelAppear()
    {
        this.gameObject.SetActive(true);
        StartChange();
    }

    //-- パネルを切り替える(_name : 切り替え先のパネルオブジェクト名)
    public void PanelChange(string _name)
    {
        panel_name = _name;
        if (fadeOutMode == FadeMode.None) { FinishOutChange(); }
        else { FadeOutPanel(); }
    }

    //-- パネルを閉じる
    public void PanelClose()
    {
        panel_name = "";
        if (fadeOutMode == FadeMode.None) { FinishOutChange(); }
        else { FadeOutPanel(); }
    }

    public void changeFadeIn(FadeDirection _direction)
    {
        fadeIn = _direction;
    }

    public void changeFadeOut(FadeDirection _direction)
    {
        fadeOut = _direction;
    }

    public void Stop()
    {
        if (nowCoroutine != null) StopCoroutine(nowCoroutine);
        isPlaying = false;
        nowState = PlayingState.Stop;
        nowCoroutine = null;
    }

    private void StartChange()
    {
        if (onStart != null) onStart.Invoke();
        if (fadeInMode == FadeMode.None) { FinishInChange(); }
        else { FadeInPanel(); }
    }

    private void FinishInChange()
    {
        canvasGroup.alpha = 1.0f;
        canvasGroup.interactable = true;
        rtf.anchoredPosition = new Vector2(0, 0);
    }

    private void FinishOutChange()
    {
        canvasGroup.alpha = 0.0f;
        if (panel_name != "")
        {
            parent.transform.Find(panel_name).gameObject.SetActive(true);
        }
        if (onFinish != null) onFinish.Invoke();
        if (panel_name == "")
        {
            parent.GetComponent<PanelChangerRoot>().PanelClose();
        }
        this.gameObject.SetActive(false);
    }

    private void FadeInPanel()
    {
        progress = 0.0f;
        canvasGroup.alpha = 0.0f;
        canvasGroup.interactable = false;
        setDirection(fadeIn);
        nowCoroutine = StartCoroutine(PlayFadeInCoroutine());
    }

    private void FadeOutPanel()
    {
        progress = 0.0f;
        canvasGroup.alpha = 1.0f;
        canvasGroup.interactable = false;
        setDirection(fadeOut);
        nowCoroutine = StartCoroutine(PlayFadeOutCoroutine());
    }

    private void setDirection(FadeDirection _direction)
    {
        switch (_direction)
        {
            case FadeDirection.Top: directionX = 0; directionY = 1; break;
            case FadeDirection.Right: directionX = 1; directionY = 0; break;
            case FadeDirection.Left: directionX = -1; directionY = 0; break;
            case FadeDirection.Bottom: directionX = 0; directionY = -1; break;
        }
    }

    IEnumerator PlayFadeInCoroutine()
    {
        if (fadeInDelay > 0) { yield return new WaitForSeconds(fadeInDelay); }
        if (isPlaying) { yield break; }
        nowState = PlayingState.FadeIn;
        isPlaying = true;

        while (progress < 1.0f)
        {
            progress += Time.deltaTime / fadeInTime;
            yield return null;
        }

        isPlaying = false;
        nowState = PlayingState.Stop;
        progress = 1.0f;
        if (onFinishFadeIn != null) onFinishFadeIn.Invoke();
        FinishInChange();
    }

    IEnumerator PlayFadeOutCoroutine()
    {
        if (fadeOutDelay > 0) { yield return new WaitForSeconds(fadeOutDelay); }
        if (isPlaying) { yield break; }
        nowState = PlayingState.FadeOut;
        isPlaying = true;
        if (onStartFadeOut != null) onStartFadeOut.Invoke();

        while (progress < 1.0f)
        {
            progress += Time.deltaTime / fadeOutTime;
            yield return null;
        }

        isPlaying = false;
        nowState = PlayingState.Stop;
        progress = 1.0f;
        FinishOutChange();
    }
}