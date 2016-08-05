using UnityEngine;

public class DummyTarget : MonoBehaviour
{
    //public GameObject incomingShot;
    public int shieldHP;
    public int hullHP;

    public void TakeDamage(int shield, int hull)
    {
        shieldHP -= shield;
        hullHP -= hull;
    }

    // Update is called once per frame
    void Update ()
    {
        //TakeDamage(shieldHP, hullHP);

        if (shieldHP <= 0)
            Debug.Log("Target's Shields are down!");

            if (hullHP <= 0)
        {
            Debug.Log("Target Destroyed!");
            Destroy(gameObject);
        }
        Debug.Log(shieldHP);
        Debug.Log(hullHP);
	}
}
