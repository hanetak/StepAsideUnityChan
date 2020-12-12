using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityChanController : MonoBehaviour
{
    private Animator myAnimator;
    private Rigidbody myRigidbody;
    private float velocityZ = 16f;
    private float velocityX = 10f;
    private float velocityY = 10f;
    private float movableRange = 3.4f;
    private float coefficient = 0.99f;//動きを減速させる係数
    private bool isEnd = false;
    private GameObject stateText;//GameOverのときに表示するテキスト
    private GameObject scoreText;//スコアを表示するテキスト
    private int score = 0;//得点
    private bool isLButtonDown = false;
    private bool isRButtonDown = false;
    private bool isJButtonDown = false;
    // Start is called before the first frame update
    void Start()
    {
        this.myAnimator = GetComponent<Animator>();
        this.myAnimator.SetFloat("Speed", 1.0f);//走るアニメーションを開始
        this.myRigidbody = GetComponent<Rigidbody>();
        this.stateText = GameObject.Find("GameResultText");
        this.scoreText = GameObject.Find("ScoreText");
    }

    // Update is called once per frame
    void Update()
    {
        //ゲーム終了なら減速する
        if (this.isEnd)
        {
            this.velocityZ *= this.coefficient;
            this.velocityX *= this.coefficient;
            this.velocityY *= this.coefficient;
            this.myAnimator.speed *= this.coefficient;
        }
        float inputVelocityX = 0;
        float inputVelocityY = 0;
        if ((Input.GetKey(KeyCode.LeftArrow)||this.isLButtonDown)&& -this.movableRange<this.transform.position.x)
        {
            inputVelocityX = -this.velocityX;
        }
        else if((Input.GetKey(KeyCode.RightArrow) || this.isRButtonDown) &&this.transform.position.x < this.movableRange)
        {
            inputVelocityX = this.velocityX;
        }
        if((Input.GetKeyDown(KeyCode.Space )|| this.isJButtonDown) &&this.transform.position.y < 0.5f)
        {
            this.myAnimator.SetBool("Jump", true);
            inputVelocityY = this.velocityY;
        }
        else
        {
            inputVelocityY = this.myRigidbody.velocity.y;
        }
        if (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            this.myAnimator.SetBool("Jump", false);
        }
        this.myRigidbody.velocity = new Vector3(inputVelocityX, inputVelocityY, this.velocityZ);
    }

    //接触時処理
    private void OnTriggerEnter(Collider other)
    {
        //障害物に接触した場合
        if(other.gameObject.tag =="CarTag"||other.gameObject.tag == "TrafficConeTag")
        {
            this.isEnd = true;
            this.stateText.GetComponent<Text>().text = "GAME OVER";
        }
        //ゴール地点に到達した場合
        if(other.gameObject.tag == "GoalTag")
        {
            this.isEnd = true;
            this.stateText.GetComponent<Text>().text = "CLEAR!!";
        }
        //コインに衝突した場合
        if (other.gameObject.tag == "CoinTag")
        {
            this.score += 10;
            this.scoreText.GetComponent<Text>().text = "Score" + this.score + "pt";
            GetComponent<ParticleSystem>().Play();
            Destroy(other.gameObject);
        }
    }

    public void GetMyJumpButtonDown()
    {
        this.isJButtonDown = true;
    }
    public void GetMyJumpButtonUp()
    {
        this.isJButtonDown = false;
    }
    public void GetMyLeftButtonDown()
    {
        this.isLButtonDown = true;
    }
    public void GetMyLeftButtonUp()
    {
        this.isLButtonDown = false;
    }
    public void GetMyRightButtonDown()
    {
        this.isRButtonDown = true;
    }
    public void GetMyRightButtonUp()
    {
        this.isRButtonDown = false;
    }
}
