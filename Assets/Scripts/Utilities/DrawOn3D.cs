
using UnityEngine;

/// <summary>
/// ��3Dģ����Ϳѻ
/// </summary>
public class DrawOn3D : MonoBehaviour
{
    /// <summary>
    /// ���Ƶ�Ŀ��ͼƬ
    /// </summary>
    public RenderTexture rt;
    /// <summary>
    /// ��ˢ
    /// </summary>
    public Texture brushTexture;

    /// <summary>
    /// �հ�ͼ
    /// </summary>
    public Texture blankTexture;

    /// <summary>
    /// �������
    /// </summary>
    public Camera cam;

    float time;

    /// <summary>
    /// ģ��
    /// </summary>
    public Transform modelTransform;

    void Start()
    {
        cam = Camera.main;
        DrawBlank();
    }

    /// <summary>
    /// ��ʼ��RenderTexture
    /// </summary>
    private void DrawBlank()
    {
        // ����rt
        RenderTexture.active = rt;
        // ���浱ǰ״̬
        GL.PushMatrix();
        // ���þ���
        GL.LoadPixelMatrix(0, rt.width, rt.height, 0);


        // ������ͼ
        Rect rect = new Rect(0, 0, rt.width, rt.height);
        Graphics.DrawTexture(rect, blankTexture);

        // �����ı�
        GL.PopMatrix();

        RenderTexture.active = null;
    }

    /// <summary>
    /// ��RenderTexture��(x,y)���괦����ˢͼ��
    /// </summary>
    private void Draw(int x, int y)
    {
        // ����rt
        RenderTexture.active = rt;
        // ���浱ǰ״̬
        GL.PushMatrix();
        // ���þ���
        GL.LoadPixelMatrix(0, rt.width, rt.height, 0);


        // ������ͼ
        x -= (int)(brushTexture.width * 0.5f);
        y -= (int)(brushTexture.height * 0.5f);
        Rect rect = new Rect(x, y, brushTexture.width, brushTexture.height);
        Graphics.DrawTexture(rect, brushTexture);

        // �����ı�
        GL.PopMatrix();

        RenderTexture.active = null;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // hit.textureCoord����ײ���uvֵ��uvֵ�Ǵ�0��1�ģ�����Ҫ���Կ��߲��ܵõ����������
                var x = (int)(hit.textureCoord.x * rt.width);
                // ע�⣬uv����ϵ��Graphics����ϵ��y�᷽���෴
                var y = (int)(rt.height - hit.textureCoord.y * rt.height);
                Draw(x, y);

                if(time >= 1f)
                {
                    EventHander.CallPlayerAudioEvent("daMo");
                    time = 0f;                
                }
            }
        }
        else
        {
            EventHander.CallStopAudioPlayEvent("playerSource");
        }
        time += Time.deltaTime;
        // �����ҷ��������תģ��
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            modelTransform.Rotate(0, 360 * Time.deltaTime, 0);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            modelTransform.Rotate(0, -360 * Time.deltaTime, 0);
        }
    }
}
