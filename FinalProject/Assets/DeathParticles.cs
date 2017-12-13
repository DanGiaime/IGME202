using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathParticles : MonoBehaviour {

    [SerializeField]
    GameObject particle;

    [SerializeField]
    Sprite[] pLarge, pMedium, pSmall;

    public int Type;

    [SerializeField]
    float forceMax, rotMax, particleLife;

	// Use this for initialization
	void Start () {
        // Spawn one large, 3 medium, and 5 small particles.
        SpawnObject(pLarge[Type]);

        for(int i = 0; i < 4; i++) {
            SpawnObject(pMedium[Random.Range(Type * 3, Type * 3 + 3)]);
        }

        for(int i = 0; i < 6; i++) {
            SpawnObject(pSmall[Random.Range(Type * 2, Type * 2 + 2)]);
        }
	}

    /// <summary>
    /// Spawn a particle
    /// </summary>
    /// <param name="spr">Sprite of the particle to spawn.</param>
    void SpawnObject(Sprite spr) {
        GameObject l = Instantiate(particle, transform.position, transform.rotation);
        l.GetComponent<SpriteRenderer>().sprite = spr;
        l.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-forceMax, forceMax), Random.Range(-forceMax, forceMax)));
        l.GetComponent<Rigidbody2D>().angularVelocity = Random.Range(-rotMax, rotMax);
        l.GetComponent<Destroyer>().SetTime(Random.Range(1f, particleLife));
    }
}
