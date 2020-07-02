using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> enemies;
    private LevelManager levelManager;
    public LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        for (int i = 0; i < levelManager.whatLevel; i++)
        {
            // Spawn Enemy above ground
            int enemyNum = Random.Range(0, enemies.Count);
            Vector2 enemyPos = new Vector2(Random.Range(-5.0f, 7.0f), 4);
            RaycastHit2D hit = Physics2D.Raycast(enemyPos, Vector2.down, 10.0f);
            /* if (hit.collider == null)
            {
                i--;
                continue;   // redo spawn if enemy could not be placed
            } */
            enemyPos.y = hit.point.y;
            Instantiate(enemies[enemyNum], enemyPos + new Vector2(0, 1), Quaternion.identity);
        }
    }

}
