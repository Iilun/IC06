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

    private int fireDamageNumber;

    public const float FIRE_DAMAGE_PIPELINE_DELAY = Bullet.FIRE_EXPLOSION_DELAY;

    // Start is called before the first frame update
    void Start()
    {
        GetChildTiles(transform);
        foreach (GameObject g in tiles)
        {
            //Debug.Log("addtile");
            g.GetComponent<Tile>().SetBoat(this);
        }
        healthBar.SetMaxHealth(maxHealthValue);
        fireDamageNumber = 0;
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
            
        }
    }

    public int GetHealth()
    {
        return healthBar.GetHealth();
    }
    

    public void SetHealth(int health)
    {
        if(health > 0){
            healthBar.SetHealth(health);
        } else {
            healthBar.SetHealth(0);
            GameTime.Stop();
        }
    }

    public int GetId()
    {
        return boatId;
    }

    public void InflictDamage(int dmg, int damageType)
    {
        int healthValue = GetHealth() - dmg;
        SetHealth(healthValue);
        if (healthValue > 0){
            healthBar.AddInfo(dmg, damageType);
        }
       

    }

    public void InflictFireDamage()
    {
        fireDamageNumber++;
        
        if (fireDamage)
        {
            fireDamage = false;
            StartCoroutine(CanFireDamage());
        }
    }

    private IEnumerator CanFireDamage()
    {
        yield return new WaitForSeconds(FIRE_DAMAGE_PIPELINE_DELAY);
        InflictDamage(fireDamageNumber * Bullet.FIRE_DAMAGE_VALUE, Bullet.FIRE_DAMAGE);
        fireDamageNumber = 0; 
        fireDamage = true;
    }
}
