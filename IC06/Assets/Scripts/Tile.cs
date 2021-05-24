using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : Destroyable
{
    protected Boat boat;

    private GameObject fire;

    private GameObject ice;

    private TileFloor tileFloor;

    private InteractableDestroyable interactable;




    // Start is called before the first frame update
    void Start()
    {
        SetTiles(this.transform);

    }

    public TileFloor GetTileFloor(){
        return tileFloor;
    }

    public void SetTiles(Transform parent)
    {
        for (int i = 0; i < parent.childCount; i++)
        {

            Transform child = parent.GetChild(i);
            if (child.GetComponent<TileFloor>() != null)
            {
                tileFloor = child.GetComponent<TileFloor>();
                tileFloor.SetTile(this);
            } else if (child.GetComponent<InteractableDestroyable>() != null){//ICI CA MARCHE PAS CAR PAS RECURSIF IL FAUT ALLER UN NIVEAU PLUS LOIN
                interactable = child.GetComponent<InteractableDestroyable>();
                interactable.SetTile(this);
            }
            if(child.transform.childCount > 0){
                SetTiles(child);
            }
            
        }
    }

    public bool IsDestroyed(){
            return tileFloor.IsDestroyed();
    }

    public override void Destroy(int i, bool isBaseTile, float delay)
    {
        StartCoroutine(DestroyWithDelay(i,isBaseTile,delay));
    }

    private IEnumerator DestroyWithDelay(int i, bool isBaseTile, float delay){
        yield return new WaitForSeconds(delay);
        if (i == DESTRUCTION_FEU)
        {
            SetFire(true);
            if (isBaseTile){
                Instantiate(DestroyableUtils.GetFireExplosion(), transform.position + new Vector3(0f,4f,0f), Quaternion.identity);
            }
        } else if (i == DESTRUCTION_GLACE){
            SetIce(true);
            if (isBaseTile){
                Instantiate(DestroyableUtils.GetIceExplosion(), transform.position + new Vector3(0f,4f,0f), Quaternion.identity);
            }
            
        } else if (i == DESTRUCTION_WIND){
            ActivateWind();
            if (isBaseTile){
                Instantiate(DestroyableUtils.GetWindExplosion(), transform.position + new Vector3(0f,4f,0f), Quaternion.identity);
            }
        } else if(i == DESTRUCTION_IEM){
            ActivateIEM();
            if (isBaseTile){
                Instantiate(DestroyableUtils.GetIEMExplosion(), transform.position + new Vector3(0f,4f,0f), Quaternion.identity);
            }
        } else if(i == BOMB_SPAWN){
            ActivateBomb();
        } else if (i == DESTRUCTION_LOURDE){
            Instantiate(DestroyableUtils.GetNormalExplosion(), transform.position, Quaternion.identity);
            GetBoat().InflictDamage(Bullet.NORMAL_BULLET_BOAT_DAMAGE, Bullet.DIRECT_DAMAGE);
        }
        
        if (tileFloor.IsDestroyable()){
            DamageTileFloor(i, isBaseTile);
        }

    }

    private void DamageTileFloor(int damageType, bool isBaseTile)
    {
        tileFloor.Damage(damageType, isBaseTile);
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

    private void ActivateBomb()
    {
       //Instantiate bomb
       GameObject bomb = Instantiate(DestroyableUtils.GetBomb(), transform.position + new Vector3(0, 3f, 0), Quaternion.Euler(-90,0,0));
       bomb.GetComponent<Bomb>().SetBoat(boat);
    }

    private void ActivateIEM()
    {
        if (interactable != null){
            Interactable interactableHere = interactable.gameObject.GetComponent<Interactable>();
            if (interactableHere != null){
                interactableHere.SetDisabled(true);
                StartCoroutine(ReAbleCoroutine());
            }
        }
    }

    private IEnumerator ReAbleCoroutine(){
        yield return new WaitForSeconds(Bullet.IEM_DISABLE_TIME);
      
        if (interactable != null){
            Interactable interactableHere = interactable.gameObject.GetComponent<Interactable>();
            if (interactableHere != null){
                interactableHere.SetDisabled(false);
            }
        }
    }

    public void SetFire(bool value)
    {
        if (value)
        {
            if(fire == null && tileFloor.IsDestroyable())
            {
                ActivateFire();
            }
            SetIce(false);
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

    public void SetIce(bool value)
    {
        if (value)
        {
            if(ice == null)
            {
                ActivateIce();
            }
            SetFire(false);
        } else
        {
            GameObject temp = ice;
            ice = null;
            Destroy(temp);
        }
    }

     private void ActivateIce()
    {
        
        ice = Instantiate(DestroyableUtils.GetIce(), new Vector3(-1000,  -1000, -1000), Quaternion.Euler(-90,0,0));
        float base_tile_height = tileFloor.GetComponent<Collider>().bounds.size.y;
        float ice_height = ice.GetComponent<Collider>().bounds.size.y;
        float final_height_offset = (0.5f * base_tile_height) + (0.5f * ice_height);
        ice.transform.position = transform.position + new Vector3(0,final_height_offset,0);
        StartCoroutine(IceCountdown());
    }

    private IEnumerator IceCountdown()
    {
        yield return new WaitForSeconds(Bullet.ICE_TOTAL_TIME);
        SetIce(false);
    }

    private void ActivateWind(){
        if (fire != null){
            List<Tile> tiles = TileUtils.GetClosestTiles(this, 1 , 'r');
            foreach (Tile t in tiles)
            {
                t.gameObject.GetComponent<Destroyable>().Destroy(Destroyable.DESTRUCTION_FEU, true, Bullet.FIRE_EXPLOSION_DELAY);
                //EFFET DE FEU
            }
        }
    }

    public override Tile GetTile()
    {
        return this;
    }
    
}
