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

    public const int DIRECT_DAMAGE = 0;
    public const int FIRE_DAMAGE = 1;

    public const int FIRE_DAMAGE_VALUE = 5;
    public const float FIRE_DAMAGE_TICK = 5f;

    private bool isShot = false;
    private bool hasDestroyed = false;

    [SerializeField]
    private int directDamage = 0;

    [SerializeField]
    private int type;

    //TEST
    public Material testMaterial; 

    public void SetIsShot(bool value)
    {
        isShot = value;
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

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.GetComponent<Destroyable>() != null && isShot && !hasDestroyed)
        {
            hasDestroyed = true;

            //Effet du boulet

            if(type == NORMAL_BULLET)
            {
                //DESTRUCTION TOTALE DE LA ZONE ATERRISSAGE
                //DESTRUCTION PARTIELLE AUTOUR ?

                //ATTENTION, IMPOSSIBLE DE DETRUIRE UN BATEAU EN 2 ? Check les transfors ? Tile en metal si avant dernier tile de la ligne detruite + securisation de 1 case de chaque cote dans le sens opposé a la destructions
                //Tile floors : PDV pour detruire a deux forts un faible, quatres faibles

                //    y = 25
                //    x = 37.5
                
                other.gameObject.GetComponent<Destroyable>().Destroy(Destroyable.DESTRUCTION_LOURDE);//Va agir sur les points de vie du floor, et le detruire si jamais

                //TEST
                List<Tile> tiles = TileUtils.GetClosestTiles(other.gameObject.GetComponent<Destroyable>(), 4);
                foreach (Tile t in tiles)
                {
                    t.gameObject.GetComponent<MeshRenderer>().material = testMaterial;
                    //Destructions faibles sur celles ci
                }
            } else if(type == FIRE_BULLET)
            {
                //Degats a l'arrivée a voir

                other.gameObject.GetComponent<Destroyable>().Destroy(Destroyable.DESTRUCTION_FEU);//Va agir sur les points de vie du bateau en spawnant des fire
                //Effet de feu sur la tile et les adjacentes
                List<Tile> tiles = TileUtils.GetClosestTiles(other.gameObject.GetComponent<Destroyable>(), 4);
                foreach (Tile t in tiles)
                {
                    t.gameObject.GetComponent<Destroyable>().Destroy(Destroyable.DESTRUCTION_FEU);
                    //EFFET DE FEU
                }
            } else if (type == ICE_BULLET)
            {
                //Degats a l'arrivée a voir
                //Effet de glace sur la tile et les adjacentes
                other.gameObject.GetComponent<Destroyable>().Destroy(Destroyable.DESTRUCTION_GLACE);//Va agir sur les points de vie du floor, et le detruire si jamais
                List<Tile> tiles = TileUtils.GetClosestTiles(other.gameObject.GetComponent<Destroyable>(), 4);
                foreach (Tile t in tiles)
                {
                    t.gameObject.GetComponent<Destroyable>().Destroy(Destroyable.DESTRUCTION_GLACE);
                    //EFFET DE glace
                }

            }


            //Infliction des dommages 
            other.gameObject.GetComponent<Destroyable>().GetBoat().InflictDamage(directDamage, Bullet.DIRECT_DAMAGE);// A voir si type de dmg ou boulets 


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

    IEnumerator DestroyBullet()
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
}
