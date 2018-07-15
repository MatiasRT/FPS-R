using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    [SerializeField] GameObject player;
    [SerializeField] GameObject enemy;
    [SerializeField] float sTime = 3f;
    [SerializeField] int sCant = 40;
    [SerializeField] Transform[] sPoints;
    private PlayerHealth playerHealth;

	void Start () {
        Invoke("Spawn", sTime);
        playerHealth = player.GetComponent<PlayerHealth>();
	}
	
	void Spawn()
    {
        if (playerHealth.CurrentHealth <= 0f || sCant == 0)
            return;
        int spawnPointIndex = Random.Range(0, sPoints.Length);
        Instantiate(enemy, sPoints[spawnPointIndex].position, sPoints[spawnPointIndex].rotation);
        sCant--;
        if (sCant > sCant / 2)
            Invoke("Spawn", sTime);
        else
            Invoke("Spawn", sTime / 2);
    }
}
