using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : Destroyable
{

    
    public int tile_type;
    private Boat boat;

    private GameObject fire;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Destroy(int i)
    {
        if (i == Destroyable.DESTRUCTION_TOTALE)
        {
            GetComponent<Collider>().enabled = false;
        } else if (i == DESTRUCTION_FEU)
        {
            SetFire(true);
        }
    }

    public override Boat GetBoat()
    {
        return boat;
    }

    public void SetBoat(Boat value)
    {
        boat = value;
    }

    private IEnumerator FireDamage()
    {
        boat.InflictFireDamage();
        yield return new WaitForSeconds(Bullet.FIRE_DAMAGE_TICK);
        if (fire != null)
        {
            StartCoroutine(FireDamage());
        }
    }

    private void ActivateFire()
    {
        fire = Instantiate(DestroyableUtils.GetFire(), transform.position + new Vector3(0, 3f, 0), Quaternion.identity);
        fire.transform.GetChild(0).GetComponent<Fire>().SetParent(this);
        StartCoroutine(FireDamage());
    }

    public void SetFire(bool value)
    {
        if (value)
        {
            if(fire == null)
            {
                ActivateFire();
            }
        } else
        {
            GameObject temp = fire;
            fire = null;
            Destroy(temp);
        }
    }

    public bool GetFire()
    {
        return fire != null;
    }

    //public override bool Equals(Object obj)
    //{
    //    return transform.position.Equals(obj.transform.position)
    //}


}
