using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//ʺһ���Ĵ��룬�ǵø�
//�Ĳ�����д����ѡ������
public class TrackData : MonoBehaviour
{
    [Header("���ʱ�̱��ļ�")]
    public TrackTimerLists_Dic[] trackTimerLists_Dic;

    public Queue<PointGameObject> UpTrackQueue, DownTrackQueue, LeftTrackQueue, RightTrackQueue, CUpTrackQueue, CDownTrackQueue, CLeftTrackQueue, CRightTrackQueue;
    //û�뵽���õķ�������ʱά��8��ͨ���Ķ������ж�
    bool SWKey = true;

    public GameObject[] Ship;

    public AudioSource[] bgm;

    public int bgmNum = 0;

    public int Perfect = 0;
    public int Miss = 0;

    public AudioSource hitAudio;

    public GameObject pointPre;

    private List<PointGameObject> tempTrackList = new List<PointGameObject>();

    public GameObject PointSave;


    private void Start()
    {
        UpTrackQueue = new Queue<PointGameObject>();
        DownTrackQueue = new Queue<PointGameObject>();
        LeftTrackQueue = new Queue<PointGameObject>();
        RightTrackQueue = new Queue<PointGameObject>();
        CUpTrackQueue = new Queue<PointGameObject>();
        CDownTrackQueue = new Queue<PointGameObject>();
        CLeftTrackQueue = new Queue<PointGameObject>();
        CRightTrackQueue = new Queue<PointGameObject>();
   
    }

    private void Update()
    {       
            foreach (var point in tempTrackList)
            {
                //(400 * point.p, (this.bgm.currentTime - (point.t / 1000)) * -1500, 0);//1500
                point.gameObject.transform.position = new Vector3(point.trackIdx, point.trackIdy, (bgm[bgmNum].time - point.timer) * -15);//20  1.00
            }

        SWBgmKey();
        InputKey();
        DeQueuePoint();
        PointCounter.Perfect = Perfect;
        PointCounter.Miss = Miss;
    }
     private void AddPoint(float trackIdx, float trackIdy, float currentTime, int hitPos)
     {
            PointGameObject pNode = new PointGameObject();
            pNode.timer = currentTime;
            pNode.trackIdx = trackIdx;
            pNode.trackIdy = trackIdy;
            pNode.hitPos = hitPos;
            pNode.gameObject = Instantiate(pointPre,PointSave.transform);
            EnTrackQueue(pNode, hitPos);
            tempTrackList.Add(pNode);

     }
     
    public void EnTrackQueue(PointGameObject pNode,int hitPos)
    {
        switch (hitPos)
        {
            case 0:
                UpTrackQueue.Enqueue(pNode);
                break;
            case 1:
                DownTrackQueue.Enqueue(pNode);
                break;
            case 2:
                LeftTrackQueue.Enqueue(pNode);
                break;
            case 3:
                RightTrackQueue.Enqueue(pNode);
                break;
            case 4:
                CUpTrackQueue.Enqueue(pNode);
                break;
            case 5:
                CDownTrackQueue.Enqueue(pNode);
                break;
            case 6:
                CLeftTrackQueue.Enqueue(pNode);
                break;
            case 7:
                CRightTrackQueue.Enqueue(pNode);
                break;
        }
    }
        
    public void CreateNecks()
    {
        if (trackTimerLists_Dic[bgmNum])
        {
            foreach (var item in trackTimerLists_Dic[bgmNum].trackTimerLists)
            {
                AddPoint(item.trackIdx,item.trackIdy, item.timer,item.hitPos);
            }
            
        }
    }
   

  

    void SWBgmKey()//�л�bgm����
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            SWBgm(0);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            SWBgm(1);
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            SWBgm(2);
        }
    }

    public void SWBgm(int s)
    {
        bgm[bgmNum].Pause();
        if (s == 0 && bgmNum < bgm.Length - 1)
        {
            DelLastTrackNote();
            bgmNum += 1;
            
        }
        if (s == 1 && bgmNum > 0)
        {
            DelLastTrackNote();
            bgmNum -= 1;
            
        }
        if(s == 2)
        {
            Perfect = 0;
            Miss = 0;
            bgm[bgmNum].time = 0;
            bgm[bgmNum].Play();      
            CreateNecks();
      }
    }

    void InputKey()//��������
    {
        if (SWKey)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && UpTrackQueue.Count != 0)
            {
                JudgeTime(0);
                KeyColorChange(0);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) && DownTrackQueue.Count != 0)
            {
                JudgeTime(1);
                KeyColorChange(1);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && LeftTrackQueue.Count != 0)
            {
                JudgeTime(2);
                KeyColorChange(2);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) && RightTrackQueue.Count != 0)
            {
                JudgeTime(3);
                KeyColorChange(3);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SWKey = false;
            }
        }else
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && CUpTrackQueue.Count != 0)
            {
                JudgeTime(4);
                KeyColorChange(4);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) && CDownTrackQueue.Count != 0)
            {
                JudgeTime(5);
                KeyColorChange(5);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && CLeftTrackQueue.Count != 0)
            {
                JudgeTime(6);
                KeyColorChange(6);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) && CRightTrackQueue.Count != 0)
            {
                JudgeTime(7);
                KeyColorChange(7);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SWKey = true;
            }
        }

    }

    public void InputTouch(int sender)//�ֻ�����
    {
        if (SWKey)
        {
            if (sender == 0 && UpTrackQueue.Count != 0)
            {
                JudgeTime(0);
                KeyColorChange(0);
            }
            if (sender == 1 && DownTrackQueue.Count != 0)
            {
                JudgeTime(1);
                KeyColorChange(1);
            }
            if (sender == 2 && LeftTrackQueue.Count != 0)
            {
                JudgeTime(2);
                KeyColorChange(2);
            }
            if (sender == 3 && RightTrackQueue.Count != 0)
            {
                JudgeTime(3);
                KeyColorChange(3);
            }
            if (sender == 4)
            {
                SWKey = false;
            }
        }
        else
        {
            if (sender == 0 && CUpTrackQueue.Count != 0)
            {
                JudgeTime(4);
                KeyColorChange(4);
            }
            if (sender == 1 && CDownTrackQueue.Count != 0)
            {
                JudgeTime(5);
                KeyColorChange(5);
            }
            if (sender == 2 && CLeftTrackQueue.Count != 0)
            {
                JudgeTime(6);
                KeyColorChange(6);
            }
            if (sender == 3 && CRightTrackQueue.Count != 0)
            {
                JudgeTime(7);
                KeyColorChange(7);
            }
            if (sender == 4)
            {
                SWKey = true;
            }
        }

    }
    void DeQueuePoint()//����δ�����˳�����
    {
        if (UpTrackQueue.Count != 0 && UpTrackQueue.Peek().gameObject.transform.position.z < -6)
        {
            Miss += 1;
            UpTrackQueue.Dequeue();
            //Debug.LogError("working");
        }
        if (DownTrackQueue.Count != 0 && DownTrackQueue.Peek().gameObject.transform.position.z < -6)
        {
            Miss += 1;
            DownTrackQueue.Dequeue();
        }
        if (LeftTrackQueue.Count != 0 && LeftTrackQueue.Peek().gameObject.transform.position.z < -6)
        {
            Miss += 1;
            LeftTrackQueue.Dequeue();
        }
        if (RightTrackQueue.Count != 0 && RightTrackQueue.Peek().gameObject.transform.position.z < -6)
        {
            Miss += 1;
            RightTrackQueue.Dequeue();
        }
        if (CUpTrackQueue.Count != 0 && CUpTrackQueue.Peek().gameObject.transform.position.z < -6)
        {
            Miss += 1;
            CUpTrackQueue.Dequeue();
        }
        if (CDownTrackQueue.Count != 0 && CDownTrackQueue.Peek().gameObject.transform.position.z < -6)
        {
            Miss += 1;
            CDownTrackQueue.Dequeue();
        }
        if (CLeftTrackQueue.Count != 0 && CLeftTrackQueue.Peek().gameObject.transform.position.z < -6)
        {
            Miss += 1;
            CLeftTrackQueue.Dequeue();
        }
        if (CRightTrackQueue.Count != 0 && CRightTrackQueue.Peek().gameObject.transform.position.z < -6)
        {
            Miss += 1;
            CRightTrackQueue.Dequeue();
        }
    }

    void QueueClear()//��ո������ж�����
    {
        UpTrackQueue.Clear();
        DownTrackQueue.Clear();
        LeftTrackQueue.Clear();
        RightTrackQueue.Clear();
        CUpTrackQueue.Clear();
        CDownTrackQueue.Clear();
        CLeftTrackQueue.Clear();
        CRightTrackQueue.Clear();
    }

    void DelLastTrackNote()//�л�bgm��ɾ����һ�׵�ʣ��ڵ�
    {
        var g = GameObject.FindGameObjectsWithTag("Point");
        foreach(var point in g)
        {
            Destroy(point);
        }
        QueueClear();
        tempTrackList.Clear();
    }
    public void JudgeTime(int Arrow)//����Z��λ���ж��Ƿ����
    {

        switch (Arrow)
        {
            case 0:
                var temp = UpTrackQueue.Peek();
                if (temp.gameObject.transform.position.z <= 0 && temp.gameObject.transform.position.z >= -6)
                {
                    Perfect += 1;
                    Debug.LogError("hitSucces0");
                    hitAudio.Play();
                    temp = UpTrackQueue.Dequeue();
                }
                break;
            case 1:
                var temp1 = DownTrackQueue.Peek();
                if (temp1.gameObject.transform.position.z <= 0 && temp1.gameObject.transform.position.z >= -6)
                {
                    Perfect += 1;
                    Debug.LogError("hitSucces1");
                    hitAudio.Play();
                    temp1 = DownTrackQueue.Dequeue();
                }
                break;
            case 2:
                var temp2 = LeftTrackQueue.Peek();
                if (temp2.gameObject.transform.position.z <= 0 && temp2.gameObject.transform.position.z >= -6)
                {
                    Perfect += 1;
                    Debug.LogError("hitSucces2");
                    hitAudio.Play();
                    temp2 = LeftTrackQueue.Dequeue();
                }
                break;
            case 3:
                var temp3 = RightTrackQueue.Peek();
                if (temp3.gameObject.transform.position.z <= 0 && temp3.gameObject.transform.position.z >= -6)
                {
                    Perfect += 1;
                    Debug.LogError("hitSucces3");
                    hitAudio.Play();
                    temp3 = RightTrackQueue.Dequeue();
                }
                break;
            case 4:
                var temp4 = CUpTrackQueue.Peek();
                if (temp4.gameObject.transform.position.z <= 0 && temp4.gameObject.transform.position.z >= -6)
                {
                    Perfect += 1;
                    Debug.LogError("hitSucces4");
                    hitAudio.Play();
                    temp4 = CUpTrackQueue.Dequeue();
                }
                break;
            case 5:
                var temp5 = CDownTrackQueue.Peek();
                if (temp5.gameObject.transform.position.z <=0 && temp5.gameObject.transform.position.z >= -6)
                {
                    Perfect += 1;
                    Debug.LogError("hitSucces5");
                    hitAudio.Play();
                    temp5 = CDownTrackQueue.Dequeue();
                }
                break;
            case 6:
                var temp6 = CLeftTrackQueue.Peek();
                if (temp6.gameObject.transform.position.z <= 0 && temp6.gameObject.transform.position.z >= -6)
                {
                    Perfect += 1;
                    Debug.LogError("hitSucces6");
                    hitAudio.Play();
                    temp6 = CLeftTrackQueue.Dequeue();
                }
                break;
            case 7:
                var temp7 = CRightTrackQueue.Peek();
                if (temp7.gameObject.transform.position.z <= 0 && temp7.gameObject.transform.position.z >= -6)
                {
                    Perfect += 1;
                    Debug.LogError("hitSucces7");
                    hitAudio.Play();
                    temp7 = CRightTrackQueue.Dequeue();
                }
                break;
        }
    }

    void KeyColorChange(int pos)//ShipHitλ����ɫ�仯����
    {
        Ship[pos].GetComponent<SpriteRenderer>().color = Color.blue;
        StartCoroutine(ChangeWhite(Ship[pos]));
    }
    IEnumerator ChangeWhite(GameObject ship)
    {
        yield return new WaitForSeconds(0.2f);
        ship.GetComponent<SpriteRenderer>().color = Color.white;
    }


}
