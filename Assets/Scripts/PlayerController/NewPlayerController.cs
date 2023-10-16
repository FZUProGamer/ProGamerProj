using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家控制角色的脚本，控制角色的移动、旋转、互动和动画的播放
/// </summary>
public class NewPlayerController : MonoBehaviour
{
    private bool PlayerMovementIsActivated = true;

    private bool isWalking;
    private bool isTalking;
    private bool isChopping;

    private bool canControl = true;
    private bool canChop => GameManager.Instance.saveData.jianZi_Completed;
    private bool interact;

    [Header("物体检测范围")]
    [SerializeField]private float radius;
    private TargetType targetType;

    [Header("运动速度")]
    public float WalkSpeed = 2f;
    public float turnSpeed = 20f;
    [Space]
    public GameObject tool;
    private GameObject target;
    
    [Header("相机")]
    public Transform FollowedCamera;

    [Header("导航")]
    public GameObject arowPoint;

    [Header("互动UI")]
    public GameObject interactUI;
    private SpriteRenderer interactBtn;

    [Header("文本文件")]
    public TextAsset chopCompleted;
    public TextAsset cantChop;

    public List<Sprite> spList;

    Animator m_Animator;
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;
    CharacterController m_Ctrl;

    Vector3 mDir = Vector3.zero;

    private void Awake()
    {
        FollowedCamera = FindObjectOfType<Camera>().transform;
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent<AudioSource>();
        interactBtn = interactUI.GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        EventHander.GameStateChangeEvent += OnGameStateChangeEvent;
    }

    private void OnDisable()
    {
        EventHander.GameStateChangeEvent -= OnGameStateChangeEvent;
    }

    private void OnGameStateChangeEvent(GameState gameState)
    {
        canControl = gameState != GameState.Pause;
    }

    void Update()
    {
        PlayerMove();
        PlayerAnimation();

        targetType = FoundTarget();

        if (isWalking)
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else
        {
            m_AudioSource.Stop();
        }

        if(Input.GetButton("Interact") && target is not null)
        {
            if(transform.IsFacingTarget(target.transform))
            {
                if(targetType is TargetType.Tree)
                {
                    Debug.Log("我按了");
                }
                else if(targetType is TargetType.Character)
                {
                    var character = target.GetComponent<Character>();
                    character?.TalkWith();
                    isTalking = true;
                    m_Animator.SetBool("IsTalking", isTalking);
                    StartCoroutine(Idle());
                }
                else if(targetType is TargetType.Teleport)
                {
                    var teleport = target.GetComponent<Teleport>();
                    teleport?.TeleportToScene();
                }
            }
        }
    }

    private void PlayerMove()
    {
        float horizontal;
        float vertical;

        if (PlayerMovementIsActivated && canControl)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
        }
        else
        {
            horizontal = 0;
            vertical = 0;
        }

        Vector3 A = Vector3.zero;
        A.Set(FollowedCamera.transform.forward.x, 0f, FollowedCamera.transform.forward.z);
        float angle = Vector3.Angle(Vector3.forward, A);
        A.Set(Mathf.Sin(angle), 0, Mathf.Cos(angle));

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);

        if (hasHorizontalInput || hasVerticalInput)
        {
            m_Movement.Set(horizontal, 0f, vertical);
            m_Movement.Normalize();
            m_Movement.Set
                ((Quaternion.Euler(Quaternion.LookRotation(FollowedCamera.transform.forward).eulerAngles + Quaternion.LookRotation(m_Movement).eulerAngles) * Vector3.forward).normalized.x
                ,
                0f
                ,
                 (Quaternion.Euler(Quaternion.LookRotation(FollowedCamera.transform.forward).eulerAngles + Quaternion.LookRotation(m_Movement).eulerAngles) * Vector3.forward).normalized.z
                );
        }

        isWalking = hasHorizontalInput || hasVerticalInput;

        if(isWalking)
        {
            Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
            
            m_Rotation = Quaternion.LookRotation(desiredForward);

            EventHander.CallPlayerAudioEvent("walking");
        }
        else
        {
            EventHander.CallStopAudioPlayEvent("playerSource");
        }
    }

    private void PlayerAnimation()
    {
        m_Animator.SetBool("IsWalking", isWalking);
    }

    void OnAnimatorMove()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + WalkSpeed * m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation(m_Rotation);
    }

    /// <summary>
    /// 用Physics.OverlapSphere去检测半径为radius的球形范围内可互动的碰撞体
    /// </summary>
    /// <returns></returns>
    private TargetType FoundTarget()
    {
        //创建检测，返回值为Collider的数组
        var colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (var target in colliders)
        {
            if(target.CompareTag("Tree"))
            {
                interactBtn.sprite = spList[0];
                interactUI.SetActive(true);

                if(Input.GetButton("Interact"))
                {
                    if(canChop)
                    {
                        StartCoroutine(Chopping(target.gameObject));
                    }
                    else
                    {
                        EventHander.CallDialogEvent(cantChop);
                    }

                }               

                return TargetType.Tree;
            }
            else if(target.CompareTag("Teleport"))
            {
                this.target = target.gameObject;

                switch(target.name)
                {
                    case "ZhiMo_Teleport":
                        interactBtn.sprite = spList[2];
                        interactUI.SetActive(true);

                        return TargetType.Teleport;
                    case "FanZi_Teleport":
                        interactBtn.sprite = spList[3];
                        interactUI.SetActive(true);

                        return TargetType.Teleport;
                    case "KeZi_Teleport":
                        interactBtn.sprite = spList[4];
                        interactUI.SetActive(true);

                        return TargetType.Teleport;
                }
            }
            else if(target.CompareTag("Character"))
            {
                this.target = target.gameObject;

                return TargetType.Character;
            }
        }
        interactUI.SetActive(false);
        return TargetType.Non;
    }


    IEnumerator Chopping(GameObject tree)
    {        
        //触发数据保存的事件
        EventHander.CallDataSaveEvent();
        
        //触发在禁用场景之前的事件，具体为记录当前场景的物品状态
        EventHander.CallBeforeSceneUnloadEvent();

        isChopping = true;
        tool.SetActive(isChopping);
        m_Animator.SetBool("IsChopping", isChopping);
        EventHander.CallPlayerAudioEvent("chopping");
        EventHander.CallGameStateChangeEvent(GameState.Pause);

        yield return new WaitForSeconds(5f);

        Animator trees = tree.GetComponent<Animator>();
        trees.Play("Falling");
        EventHander.CallGameStateChangeEvent(GameState.GamePlay);
        isChopping = false;
        m_Animator.SetBool("IsChopping", isChopping);
        tool.SetActive(isChopping);
        ObjectManager.Instance.isCompleteDict["Chopping"] = true;
        GameManager.Instance.SetChoppingData(true);
        EventHander.CallDialogEvent(chopCompleted);
        EventHander.CallStopAudioPlayEvent("playerSource");

        GameManager.Instance.SetArowPoint(2);

        //具体为设置场景中物体的状态
        EventHander.CallAfterSceneLoadedEvent();
        GameManager.Instance.SaveData();
    }

    IEnumerator Idle()
    {
        yield return new WaitForSeconds(7f);
        isTalking = false;
        m_Animator.SetBool("IsTalking", isTalking);
    }
}
