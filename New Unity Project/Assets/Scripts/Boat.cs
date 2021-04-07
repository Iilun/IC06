using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Boat : MonoBehaviour
{

    public int boatId;
    private List<GameObject> tiles = new List<GameObject>();

    [SerializeField]
    private HealthBar healthBar;

    [SerializeField]
    private int maxHealthValue;

    private bool fireDamage = true;

    // Start is called before the first frame update
    void Start()
    {
        GetChildTiles(transform);
        foreach (GameObject g in tiles)
        {
            g.GetComponent<Tile>().SetBoat(this);
        }
        healthBar.SetMaxHealth(maxHealthValue);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetChildTiles(Transform parent)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            if (child.tag == "Tile")
            {
                tiles.Add(child.gameObject);
            }
            if (child.childCount > 0)
            {
                GetChildTiles(child);
            }
        }
    }

    public int GetHealth()
    {
        return healthBar.GetHealth();
    }
    

    public void SetHealth(int health)
    {
        healthBar.SetHealth(health);
    }

    public int GetId()
    {
        return boatId;
    }

    public void InflictDamage(int dmg, int damageType)
    {
        int healthValue = GetHealth() - dmg;
        if (healthValue > 0)
        {
            SetHealth(healthValue);
            healthBar.AddInfo(dmg, damageType);
        }
    }

    public void InflictFireDamage()
    {
        if (fireDamage)
        {
            fireDamage = false;
            StartCoroutine(CanFireDamage());
            InflictDamage(Bullet.FIRE_DAMAGE_VALUE, Bullet.FIRE_DAMAGE);
        }
    }

    private IEnumerator CanFireDamage()
    {
        yield return new WaitForSeconds(Bullet.FIRE_DAMAGE_TICK);
        fireDamage = true;
    }
}
