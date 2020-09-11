using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    const int MaxShotPower = 5;
    const int RecoverSeconds = 3;

    int shotPower = MaxShotPower;
    AudioSource shotSound;

    [SerializeField] GameObject[] candyPrefabs;
    [SerializeField] Transform candyParentTransform;
    [SerializeField] CandyManager candyManager;
    [SerializeField] float shotForce;
    [SerializeField] float ShotTorque;
    [SerializeField] float baseWidth;

    private void Start()
    {
        shotSound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shot();
        }

    }

    //キャンディのプレハブのからランダムに一つ選ぶ
    GameObject SampleCandy()
    {
        int index = Random.Range(0, candyPrefabs.Length);
        return candyPrefabs[index];
    }

    Vector3 GetInstantiatePosition()
    {
        //画面のサイズとInputの割合からキャンディ生成のポジションを計算
        float x = baseWidth *
            (Input.mousePosition.x / Screen.width) - (baseWidth / 2);
        return transform.position + new Vector3(x, 0, 0);
    }


    public void Shot()
    {
        //キャンディを生成できる条件外ならばShotしない
        if (candyManager.GetCandyAmount() <= 0)
        {
            return;
        }

        if(shotPower <= 0)
        {
            return;
        }
        //プレハブからオブジェクトを生成
        GameObject candy = (GameObject)Instantiate(SampleCandy(),
            GetInstantiatePosition(),
            Quaternion.identity);

        //生成したCandyオブジェクトの親をcandyParentTransformに設定する
        candy.transform.parent = candyParentTransform;

        //CandyオブジェクトのRigidbodyを取得し、力と回転を加える
        Rigidbody candyRigidbody = candy.GetComponent<Rigidbody>();
        candyRigidbody.AddForce(transform.forward * shotForce);
        candyRigidbody.AddTorque(new Vector3(0, ShotTorque, 0));

        //Candyのストックを消費
        candyManager.ConsumeCandy();

        //ShotPowerを消費
        ConsumePower();

        //サウンドを再生
        shotSound.Play();
    }

    private void OnGUI()
    {
        GUI.color = Color.black;

        //ShOtPowerの残数を＋の数で表示
        string label = "";

        for(int i = 0; i < shotPower; i++)
        {
            label = label + "+";
        }

        GUI.Label(new Rect(50, 65, 100, 30), label);
    }

    void ConsumePower()
    {
        //ShotPowerを消費すると同時に回復のカウントをスタート
        shotPower--;

        StartCoroutine(RecoverPower());
    }

    IEnumerator RecoverPower()
    {
        //一定秒数待ったあとにShotPowerを回復
        yield return new WaitForSeconds(RecoverSeconds);
        shotPower++;
    }
}
