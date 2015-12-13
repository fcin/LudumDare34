using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class PlayerController : MonoBehaviour {


    public static Range PLAYER_POS_LIMITS = new Range(-8, 8);

    public GameObject Child;
    public GameObject Spawner;
	public Animator anim;

    private GameObject lastChild;
    private Vector3 lastPlayerPos;
	private CoinSpawner coinSpawner;

    private float player_rot_speed = 1f;
    private float player_speed = 5f;
    private float step = 0.25f;
    private float player_acceleration = 0.01f;

    private bool isPlayerDead = false;

	public static int score = 0;

	private GameObject lastCoin;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Obstacle")
        {
            isPlayerDead = true;
        }
		else if (col.gameObject.tag == "Coin") {
			score++;
			Destroy (col.gameObject);
		}
    }

    void Start()
    {
        WorldSettings.IsGameRunning = false;

        Vector3 childStartPos = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y - 0.1f,
                            this.gameObject.transform.position.z);

        lastChild = Instantiate(Child.gameObject, childStartPos, this.gameObject.transform.rotation) as GameObject;
        lastPlayerPos = this.gameObject.transform.position;

		coinSpawner = Spawner.GetComponent<CoinSpawner> ();
		anim = GetComponent<Animator> ();
		RunEatingAnimation ();
    }

    private void SpawnChild()
    {
        float x = Mathf.Abs(this.gameObject.transform.position.x - lastChild.transform.position.x);
        float y = Mathf.Abs(this.gameObject.transform.position.y - lastChild.transform.position.y);
        float r = Mathf.Sqrt((x * x) + (y * y));

        if (r > 0.75f)
        {
            lastChild = Instantiate(Child.gameObject, lastChild.transform.position, this.gameObject.transform.rotation) as GameObject;
            lastPlayerPos = this.gameObject.transform.position;

            if (player_speed <= 8)
            {
                player_speed += player_acceleration;
            }

        }
    }

    private void InputChecker()
    {
        if (Input.GetButton("a"))
        {
            this.transform.Rotate(new Vector3(0, 0, player_rot_speed));
        }
        else if (Input.GetButton("d"))
        {
            this.transform.Rotate(new Vector3(0, 0, -player_rot_speed));
        }
    }

    private void MovePlayer()
    {
        this.gameObject.transform.Translate(Vector3.up * Time.deltaTime * player_speed);
    }

    private void CheckLimits()
    {
        if (this.gameObject.transform.position.x > PLAYER_POS_LIMITS.Max)
        {
            this.gameObject.transform.position = new Vector3(PLAYER_POS_LIMITS.Max, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
            this.gameObject.transform.Rotate(new Vector3(0, 0, 0.35f));
        }
        else if (this.gameObject.transform.position.x < PLAYER_POS_LIMITS.Min)
        {
            this.gameObject.transform.position = new Vector3(PLAYER_POS_LIMITS.Min, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
            this.gameObject.transform.Rotate(new Vector3(0, 0, -0.35f));
        }

        if (this.gameObject.transform.rotation.z > 0.35f)
        {
            this.gameObject.transform.rotation = new Quaternion(0, 0, 0.35f, 1);
        }
        else if (this.gameObject.transform.rotation.z < -0.35f)
        {
            this.gameObject.transform.rotation = new Quaternion(0, 0, -0.35f, 1);
        }
    }
	
	void Update () {
        
        if (!isPlayerDead && WorldSettings.IsGameRunning)
        {
            InputChecker();
            MovePlayer();
            CheckLimits();
            SpawnChild();

            lastChild.transform.position = Vector3.MoveTowards(lastChild.transform.position, lastPlayerPos, step);
        }
        else
        {
            Spawner.GetComponent<WorldSettings>().ToggleStartButton(false);
        }
    }

	private void RunEatingAnimation()
	{
		anim.SetBool ("isEating", true);
	}

    public void SetPlayerState(bool isDead)
    {
        this.isPlayerDead = isDead;
    }
}
