using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Item
{
    public const int NORMAL_BULLET = 0;
    public const int FIRE_BULLET = 1;
    public const int ICE_BULLET = 2;
    public const int IEM_BULLET = 3;
    public const int BOMB_BULLET = 4;
    public const int WIND_BULLET = 5;

    public const int DIRECT_DAMAGE = 0;
    public const int FIRE_DAMAGE = 1;

    public const int FIRE_DAMAGE_VALUE = 5;
    public const float FIRE_DAMAGE_TICK = 5f;

    public const float FIRE_EXPLOSION_DELAY = 0.5f;

    public const float ICE_TOTAL_TIME = 20f;

    public const float ICE_FREEZE_TIME = 3f;

    public const float ICE_EXPLOSION_DELAY = 2f;

    public const float WIND_EXPLOSION_DELAY = 2f;

    public const float IEM_EXPLOSION_DELAY = 2;

    public const float IEM_DISABLE_TIME = 15;

    public const int BOMB_EXPLOSION_TIME = 8;

    public const float NORMAL_EXPLOSION_DELAY = 2;

    public const int NORMAL_BULLET_BOAT_DAMAGE = 150;

    public const int BOMB_BULLET_BOAT_DAMAGE = 400;

    private bool isShot = false;
    private bool hasDestroyed = false;

    [SerializeField]
    private int directDamage = 0;

    [SerializeField]
    private int type;

    protected bool isShootable = true;

    private Boat boat;

    public void SetBoat(Boat value){
        boat = value;
    }

    public void SetIsShot(bool value)
    {
        isShot = value;
    }

    public bool IsShootable(){
        return isShootable;
    }

    public void SetIsShootable(bool value){
        isShootable = value;
    }

    public void Load()
    {
        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.SetActive(false);
        currentPlayer = null;
        isShot = true;
    }

    public IEnumerator EnableCollider(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.GetComponent<Collider>().enabled = true;
    }

     public override void Enter(Player player)
    {
        
        if(player.GetCurrentItem() == null && isDropped && !isFake && !isShot)
        {
            PickUp(player); //ici seulement pour les futurs trucs je pense
        }
        
    }

    protected void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.GetComponent<Destroyable>() != null  && isShot && !hasDestroyed && other.gameObject.GetComponent<Destroyable>().GetBoat() != boat)
        {

            hasDestroyed = true;

            //Effet du boulet

            

            if(type == NORMAL_BULLET)
            {
                //DESTRUCTION TOTALE DE LA ZONE ATERRISSAGE
                //DESTRUCTION PARTIELLE AUTOUR ?

                //ATTENTION, IMPOSSIBLE DE DETRUIRE UN BATEAU EN 2 ? Check les transfors ? Tile en metal si avant dernier tile de la ligne detruite + securisation de 1 case de chaque cote dans le sens oppos� a la destructions
                //Tile floors : PDV pour detruire a deux forts un faible, quatres faibles

                //    y = 25
                //    x = 37.5
                
                other.gameObject.GetComponent<Destroyable>().Destroy(Destroyable.DESTRUCTION_LOURDE, true, 0);//Va agir sur les points de vie du floor, et le detruire si jamais

                List<Tile> tiles = TileUtils.GetClosestTiles(other.gameObject.GetComponent<Destroyable>().GetTile(), 1 , 'r');//Mettre a 1 

                //3.18 -> 27
                //26.4 -> -3.6
                //50.4 -> -3.22
                foreach (Tile t in tiles)
                {
                    t.gameObject.GetComponent<Destroyable>().Destroy(Destroyable.DESTRUCTION_LEGERE, false, NORMAL_EXPLOSION_DELAY);
                    //EFFET DE FEU
                }
            } else if(type == FIRE_BULLET)
            {
                //Degats a l'arriv�e a voir

                other.gameObject.GetComponent<Destroyable>().Destroy(Destroyable.DESTRUCTION_FEU, true, 0);//Va agir sur les points de vie du bateau en spawnant des fire
                //Effet de feu sur la tile et les adjacentes
                List<Tile> tiles = TileUtils.GetClosestTiles(other.gameObject.GetComponent<Destroyable>().GetTile(), 1 , 'r');//Mettre a 1 

                //3.18 -> 27
                //26.4 -> -3.6
                //50.4 -> -3.22
                foreach (Tile t in tiles)
                {
                    t.gameObject.GetComponent<Destroyable>().Destroy(Destroyable.DESTRUCTION_FEU, false, FIRE_EXPLOSION_DELAY);
                    //EFFET DE FEU
                }
            } else if (type == ICE_BULLET)
            {
                //Degats a l'arriv�e a voir
                //Effet de glace sur la tile et les adjacentes
                other.gameObject.GetComponent<Destroyable>().Destroy(Destroyable.DESTRUCTION_GLACE, true, 0);//Va spawn de la glace

                List<Tile> tiles = TileUtils.GetClosestTiles(other.gameObject.GetComponent<Destroyable>().GetTile(), 1, 'c');
               
                foreach (Tile t in tiles)
                {
                    t.gameObject.GetComponent<Destroyable>().Destroy(Destroyable.DESTRUCTION_GLACE, false, ICE_EXPLOSION_DELAY);
                    //EFFET DE glace
                } 

            } else if (type == IEM_BULLET){
                other.gameObject.GetComponent<Destroyable>().Destroy(Destroyable.DESTRUCTION_IEM, true, 0);//Va spawn de l'IEM
                List<Tile> tiles = TileUtils.GetClosestTiles(other.gameObject.GetComponent<Destroyable>().GetTile(), 1, 'c');
                foreach (Tile t in tiles)
                {
                    t.gameObject.GetComponent<Destroyable>().Destroy(Destroyable.DESTRUCTION_IEM, false, WIND_EXPLOSION_DELAY);
                    //EFFET DE VENT
                }
            } else if (type == BOMB_BULLET){
                other.gameObject.GetComponent<Destroyable>().Destroy(Destroyable.BOMB_SPAWN, true, 0);//Va spawn la bombe
            } else if (type == WIND_BULLET){
                other.gameObject.GetComponent<Destroyable>().Destroy(Destroyable.DESTRUCTION_WIND, true, 0);//Va spawn du vent
                List<Tile> tiles = TileUtils.GetClosestTiles(other.gameObject.GetComponent<Destroyable>().GetTile(), 1, 'c');
                foreach (Tile t in tiles)
                {
                    t.gameObject.GetComponent<Destroyable>().Destroy(Destroyable.DESTRUCTION_WIND, false, WIND_EXPLOSION_DELAY);
                    //EFFET DE VENT
                }
            }

            //Destruction du boulet
            Destroy(gameObject);

        }

        if(other.gameObject.tag == "Water")
        {
            Destroy(gameObject);
        }


    }

    public void InitiateCountDown()
    {
        StartCoroutine(DestroyBullet());
    }

    private IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }

    public int GetType()
    {
        return type;
    }

    public void SetType(int value)
    {
        type = value;
    }

    public Boat GetBoat(){
        return boat;
    }
}
