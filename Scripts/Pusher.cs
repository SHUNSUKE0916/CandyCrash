using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pusher : MonoBehaviour
{
    Vector3 startPosition;

    [SerializeField] float amplitude;//移動量プロパティ
    [SerializeField] float speed;//移動速度プロパティ

    private void Start()
    {
        startPosition = transform.localEulerAngles;
    }

    private void Update()
    {
        //変位を計算
        float z = amplitude * Mathf.Sin(Time.time * speed);//移動量の計算

        //zを変位させたポジションに再設定
        transform.localPosition = startPosition + new Vector3(0, 0, z);//ポジションの反映
    }
}
