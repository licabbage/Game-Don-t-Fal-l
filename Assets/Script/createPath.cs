using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class createPath : MonoBehaviour {

    // Use this for initialization
    public GameObject roadPixel;
    public GameObject player;
    public int each_Size;
    public int width;   //路的行占几个格子
    public int length;  //显示的路的长度
    public int forwardSteps = 7; 
    public float deltTime;  //两行消失时间间隔

    public bool enableDispear = true;

    private int num = 0; //路的总长度
    private int currentDistance;    //当前走的路长
    private Queue<GameObject> paiQueue;
    private bool start = false;
    

    public class pai
    {
        private int width;
        private int []element;
        private int eachSize;
        public GameObject roadPixel;
        public GameObject PaiGameObject;
        

        //构造函数
        public pai(int _width,int _eachSize)
        {
            width = _width;
            eachSize = _eachSize;
            element = new int[width];
            RandomAllocation();
            roadPixel = GameObject.Find("path").GetComponent<createPath>().roadPixel;
        }

        public pai(int _width)
        {
            width = _width;
            eachSize = GameObject.Find("path").GetComponent<createPath>().each_Size;
            element = new int[width];
            RandomAllocation();
            roadPixel = GameObject.Find("path").GetComponent<createPath>().roadPixel;
        }

        //为每排元素分配随机数
        private void RandomAllocation()
        {
            for(int i=0; i<width;i++)
            {
                element[i] = Random.Range(0, 4);
            }
        }

        public int[] getElement()
        {
            return element;
        }
        //在unity中创建对象
        public GameObject show()  
        {
            PaiGameObject = new GameObject("pai");
            for(int i=0; i < width; i++)
            {
                switch (element[i])
                {//0和1时没有台阶，其他生成台阶
                    case 0:
                        break;
                    case 1:
                        break;
                    default:            
                        GameObject temp = Instantiate(roadPixel);
                        temp.transform.position = new Vector3(i * eachSize, 0, 0);
                        temp.transform.parent = PaiGameObject.transform;
                        break;

                }
                
            }
            return PaiGameObject;
        }
    }

    private void Start()
    {
        paiQueue = new Queue<GameObject>();
        //num = 0;
        //保证玩家一开始的位置是有板子的
        pai firstPai = addPai();
        int[] temp = firstPai.getElement();
        for (int i = 0; i < width; i++)
        {
            if (temp[i] != 0 && temp[i] != 1)
            {
                player.transform.position = new Vector3(each_Size * i, 8, 0);
                break;
            }
            if(i == width-1)
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        //加入剩下的length-1拍
        for (int i = 1; i < length; i++)
        {
            addPai();
        }
    }
	
	// Update is called once per frame
	void Update () {

        //等待键盘输入后开始，开始后开始不断掉落
        if ( !start&&  !player.GetComponent<moveController>().getHV().Equals(new Vector2(0, 0)))
        {
            start = true;
            Invoke("updateRoad", deltTime);
        }

        currentDistance = (int)player.transform.position.z / each_Size;
        if ( num -currentDistance <forwardSteps)
        {
            var temp = addPai().PaiGameObject;
            //Vector3 tragetPosition = temp.transform.position;
            //Vector3 startPosition = tragetPosition + new Vector3(0, 2, 0);
            //temp.transform.position = Vector3.Lerp(startPosition, tragetPosition, 0.1f);
            if(!enableDispear)//在不允许消失方块时为防止内存溢出，需要在每加上一排时删除最后一排
            {
                erasePai();
            }
        }
        //防止内存溢出，在路的长度过长时，消除后面的路
        if(paiQueue.Count > length)
        {
            erasePai();
        }
        
	}

    void updateRoad()
    {
        if (enableDispear)
        {
            erasePai();
            Invoke("updateRoad", deltTime);
        }
    }

    pai addPai()
    {
        pai tempPai = new pai(width);
        GameObject temp  = tempPai.show();
        temp.transform.position = new Vector3(0, 0, each_Size * num);
        temp.transform.parent = gameObject.transform;
        num++;
        paiQueue.Enqueue(temp);
        return tempPai;     
    }

    pai fallPai()
    {
        pai tempPai = new pai(width);
        GameObject temp = tempPai.show();
        Vector3 tragetPosition = new Vector3(0, 0, each_Size * num);
        Vector3 startPosition = tragetPosition + new Vector3(0, 3, 0);
        temp.transform.position = Vector3.Lerp(startPosition, tragetPosition, 0.1f);
        temp.transform.parent = gameObject.transform;
        num++;
        paiQueue.Enqueue(temp);
        return tempPai;
    }

    void erasePai()
    {
        Destroy(paiQueue.Dequeue());
    }

    public int getCurrentDistance()
    {
        return currentDistance;
    }
}
