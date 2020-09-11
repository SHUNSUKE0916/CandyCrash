using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyDestroy : MonoBehaviour
{
    [SerializeField] CandyManager candyManager;
    [SerializeField] int reward;
    [SerializeField] GameObject effectPrefab;
    [SerializeField] Vector3 effectRotation;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Candy")
        {
            //指定数だけcandyのストックを増やす
            candyManager.AddCandy(reward);

            //オブジェクトを削除
            Destroy(other.gameObject);

            if(effectPrefab != null)
            {
                //candyのポジションにエフェクトを生成
                Instantiate(
                    effectPrefab,
                    other.transform.position,
                    Quaternion.Euler(effectRotation));//Quaternionで回転数
            }
        }
    }
}
