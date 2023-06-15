using UnityEngine;
using UnityEngine.UI;


public class Raw_Image : MonoBehaviour
{
    //ת���ܳ���ʱ��
    private float fadeTime = 1f;

    //����UI��
    //[SerializeField] private FadeImage m_FadeImage;
    private RawImage m_RawImage;
    //��ʼ����ʱ
    public bool timestart = false;
    //��������ʱ
    public bool timeend = false;
    //��������ִ��
    public bool FadeIn = false;
    //��������ִ��
    public bool FadeOut = false;

    public string Sts = "0";

    public float times1 = 0f;

    void Start()
    {
        //��ȡRawimageʵ��
        m_RawImage = GetComponent<RawImage>();
        //��ͼƬ��С����Ϊ��Ļ��С
        m_RawImage.uvRect = new Rect(0, 0, Screen.width, Screen.height);
        m_RawImage.CrossFadeAlpha(0f, 0f, false);
    }

    void Update()
    {

        //��ʱ����ɵ���ʱ�Ĺ���
        if (timestart)
        {

            times1 -= Time.deltaTime;
            if (times1 <= 0f)
            {
                if (Sts == "1")
                //if (timestart && !timeend)
                {
                    //�ж�����ʱ����ʱ����ʲô
                    FadeToBlack(fadeTime);
                    Sts = "2";

                    Debug.Log("timestart OK");
                    Debug.Log(Sts);
                }
            }
        }

    }

    //��Ļ����Ч������     
    public void FadeToClear(float fadeSpeeds)
    {
        //rawImage.color = Color.Lerp(rawImage.color, Color.clear, fadeSpeed * Time.deltaTime);
        m_RawImage.CrossFadeAlpha(1f, fadeSpeeds / 2, false);
        fadeTime = fadeSpeeds;
        times1 = (fadeSpeeds / 2) + 0.1f;
        Sts = "1";
        timestart = true;
        Debug.Log("FadeToClear OK");
        Debug.Log(Sts);
    }
    //��Ļ����Ч������     
    public void FadeToBlack(float fadeSpeeds)
    {


        m_RawImage.CrossFadeAlpha(0f, fadeSpeeds / 2, false);

        //times1 = (fadeSpeeds/2) + 0.2f;
        //Sts = "3";
        Debug.Log("FadeToBlack OK");
        Debug.Log(Sts);

        timestart = false;
    }
    /*
        public IEnumerator ChangeTime()
        {
            while (time > 0)
            {
                yield return new WaitForSeconds(1);// ÿ�� �Լ�1���ȴ� 1 ��
                time--;
                //GetComponent<Text>().text = "����ʱ:" + time;
                Debug.Log("����ʱ:" + time);
            }
            FadeToBlack();

        }*/

}