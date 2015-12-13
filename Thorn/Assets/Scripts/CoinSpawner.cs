using UnityEngine;
using System.Collections;

public class CoinSpawner : MonoBehaviour {

	public GameObject Coin;
	public GameObject Player;
	private GameObject lastCoin;

	private float player_pos_y;
	private float minPos;
	private float maxPos;

	void Start () {
		minPos = PlayerController.PLAYER_POS_LIMITS.Min;
		maxPos = PlayerController.PLAYER_POS_LIMITS.Max;

		float xPos = Random.Range (minPos, maxPos);
		lastCoin = (GameObject)Instantiate (Coin.gameObject, new Vector3(xPos, Player.transform.position.y, 0), Quaternion.identity);

		StartCoroutine (SpawnCoin());
	}

	void Update()
	{
		player_pos_y = Player.transform.position.y;
	}

	IEnumerator SpawnCoin()
	{
		while (true) {
			yield return new WaitForSeconds(3);

			float xPos = Random.Range (minPos, maxPos);
			lastCoin = (GameObject)Instantiate (Coin.gameObject, new Vector3(xPos, player_pos_y + 10, 0), Quaternion.identity);	
		}
	}

	public GameObject GetLastCoin()
	{
		return lastCoin;
	}
}
