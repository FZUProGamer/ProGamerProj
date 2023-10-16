using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 加载场景的管理类，外界传入参数调用方法进行转场景
/// 单例
/// </summary>
public class TransitionManager : Singleton<TransitionManager>
{
    //异步加载场景时，要显示的进度条内容
    [SerializeField]private GameObject progressPanel;
    [SerializeField]private  CanvasGroup fadeCanvasGroup;
    [SerializeField]private Text progressText;
    [SerializeField]private Slider progressSlide;

    //加载界面淡入淡出的速度
    [SerializeField]private float fadeDuration;

    //主要场景的名字，因为是根据场景名加载场景的，点开始游戏就会加载这个场景
    [SerializeField]private string gamePlayScene;

    //判断加载状态的布尔值，主要判断目前是否可以加载新场景
    public bool canTransition;
    public bool isFade;

    //捕获玩家和相机对象
    private GameObject player;
    private Camera camera;

    //注册和移除事件
    private void OnEnable()
    {
        EventHander.GameStateChangeEvent += OnGameStateChangeEvent;
    }

    private void OnDisable()
    {
        EventHander.GameStateChangeEvent -= OnGameStateChangeEvent;
    }

    //游戏一开始，就是在主界面的时候是可以进行场景切换的
    private void Start()
    {
        canTransition = true;
    }

    //利用事件委托的方法，当外界发生游戏状态变化时，就会判断当前状态能否进行场景切换
    private void OnGameStateChangeEvent(GameState gameState)
    {
        canTransition = gameState == GameState.GamePlay;
    }

    //点击开始按钮执行的方法
    public void StartGameTransition()
    {
        Transition(gamePlayScene);
    }

    //转移场景，若当前状态可以进行场景切换，则执行协程方法
    public void Transition(string sceneName)
    {
        if(canTransition && !isFade)
        {
            StartCoroutine(TransitionToScene(sceneName));
        }
    }

    /// <summary>
    /// 设置Camera的Follow目标为Player
    /// </summary>
    public void Camera()
    {
        camera = FindObjectOfType<Camera>();

        camera.GetComponent<CamController>().target = GameManager.Instance.player.transform;
    }

    /// <summary>
    /// 加载到其他场景，协程
    /// </summary>
    /// <param name="sceneName">场景的名字</param>
    /// <returns></returns>
    private IEnumerator TransitionToScene(string sceneName)
    {
        progressPanel.SetActive(true);
        //yield return Fade(1);
        
        //触发数据保存的事件
        EventHander.CallDataSaveEvent();
        
        //触发在禁用场景之前的事件，具体为记录当前场景的物品状态
        EventHander.CallBeforeSceneUnloadEvent();

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);//异步加载

        operation.allowSceneActivation = false;

        //具体进度条的实现
        while(!operation.isDone)
        {
            progressSlide.value = operation.progress;

            progressText.text = operation.progress * 100 + "%";

            if(operation.progress >= 0.9f)
            {
                progressSlide.value = 1;
                progressText.text = "100%";
                operation.allowSceneActivation = true;
            }

            yield return null;
        }

        //生成并获取玩家和摄像机
        if(sceneName == gamePlayScene)
        {
            GameManager.Instance.SetPlayer();
            Camera();
        }

        //触发在加载完新场景之后的事件，具体为设置场景中物体的状态
        EventHander.CallAfterSceneLoadedEvent();
        GameManager.Instance.SaveData();
        
        //yield return Fade(0);
        progressPanel.SetActive(false);
        yield return null;
    }

    /// <summary>
    /// 淡出淡出场景
    /// </summary>
    /// <param name="targetAlpha">1是黑，0是透明</param>
    /// <returns></returns>
    private IEnumerator Fade(float targetAlpha)
    {
        isFade = true;

        fadeCanvasGroup.blocksRaycasts = true;

        float speed = Mathf.Abs(fadeCanvasGroup.alpha - targetAlpha)/fadeDuration;
    
        while(!Mathf.Approximately(fadeCanvasGroup.alpha, targetAlpha))
        {
            fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, targetAlpha, speed * Time.deltaTime);
            yield return null;        
        }

        fadeCanvasGroup.blocksRaycasts = false;
        isFade = false;
    }
}
