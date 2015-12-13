using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class ObstacleSpawner : MonoBehaviour {

    public GameObject Player;
    public GameObject[] obstacles = new GameObject[3];
    public GameObject[] additionalObstacles = new GameObject[2];
    private Range range = new Range(0, 2);
    private float player_pos_y;
    private Vector3 lastObstaclePos;
    private int obst_spawn_offset = 15;

    private int minRange = PlayerController.PLAYER_POS_LIMITS.Min;
    private int maxRange = PlayerController.PLAYER_POS_LIMITS.Max;

    void Start () {
        lastObstaclePos = Vector3.zero;

        StartCoroutine(RandomEvent());
    }
	
	void Update () {

        player_pos_y = Player.transform.position.y;

        SpawnObstacle();
    }

    IEnumerator RandomEvent()
    {
        while (true)
        {
            int randomTime = Random.Range(3, 7);
            int randomObstacleIndex = Random.Range(0, additionalObstacles.Length);

            yield return new WaitForSeconds(randomTime);

			int direction = (int)Random.value;

			int obsPos_x = direction == 0 ? maxRange : minRange;

			Vector3 obsPos = new Vector3(obsPos_x, player_pos_y + 15, 0);
            Instantiate(additionalObstacles[randomObstacleIndex].gameObject, obsPos,
                additionalObstacles[randomObstacleIndex].transform.rotation);
        }
    }

    private void SpawnObstacle()
    {
        float delta = Player.gameObject.transform.position.y - lastObstaclePos.y;

        if (delta > 0)
        {
            int index = Random.Range(range.Min, range.Max);
            Vector2 obstPos = new Vector2(Random.Range(minRange, maxRange), Player.transform.position.y + obst_spawn_offset);
            Instantiate(obstacles[index].gameObject, new Vector3(obstPos.x, obstPos.y, 0), Quaternion.identity);
            lastObstaclePos = obstPos;
        }
    }
}