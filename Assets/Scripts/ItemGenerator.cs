using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    public GameObject carPrefab;
    public GameObject coinPrefab;
    public GameObject conePrefab;
    public GameObject Player;
    private float goalPos = 360;
    private float posRange = 3.4f;
    private float playerPosZ;//プレイヤーのZ座標
    private float generateRange = 30.0f;
    private float timeOut = 0.1f;
    private float timeElapsed;//合計
    // Start is called before the first frame update
    void Start()
    {
        


    }

    // Update is called once per frame
    void Update()
    {
        playerPosZ = Player.transform.position.z;
        if(playerPosZ > generateRange && playerPosZ < goalPos - 50.0f)
        {
            GenerateItem(generateRange);
            generateRange += 15.0f;
        }
    }
    void GenerateItem(float generateRange)
    {
        float i = generateRange + 50.0f;
        int num = Random.Range(1, 11);
        if (num <= 2)
        {
            for (float j = -1; j <= 1; j += 0.4f)
            {
                GameObject cone = Instantiate(conePrefab);
                cone.transform.position = new Vector3(4 * j, cone.transform.position.y, i);
            }
        }
        else
        {
            //レーンごとにアイテムを生成
            for (int j = -1; j <= 1; j++)
            {
                //アイテムの種類を決める
                int item = Random.Range(1, 11);
                //アイテムを置くZ座標のオフセットをランダムに設定
                int offsetZ = Random.Range(-5, 6);
                //60%コイン配置:30%車配置:10%何もなし
                if (1 <= item && item <= 6)
                {
                    //コインを生成
                    GameObject coin = Instantiate(coinPrefab);
                    coin.transform.position = new Vector3(posRange * j, coin.transform.position.y, i + offsetZ);
                }
                else if (7 <= item && item <= 9)
                {
                    //車を生成
                    GameObject car = Instantiate(carPrefab);
                    car.transform.position = new Vector3(posRange * j, car.transform.position.y, i + offsetZ);
                }
            }
        }
    }
}
