using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileFloor : Destroyable
{
        private float health = TileUtils.TILE_FLOOR_MAX_HEALTH;

        private Tile motherTile;
        
        [SerializeField]
        private bool isDestroyable;



        void Start(){
            //Si indestructible changer le mesh
        }
        public bool IsDestroyed(){
            return health <= 0;
        }

        public bool IsDestroyable(){
            return isDestroyable;
        }

        public void Damage(int damageType, bool isBaseTile){
            if (damageType == Destroyable.DESTRUCTION_LEGERE){
                health -= 25;
            } else if (damageType == Destroyable.DESTRUCTION_LOURDE){
                health -= 37.5f;
            } else if (damageType == Destroyable.DESTRUCTION_TOTALE){
                health -= TileUtils.TILE_FLOOR_MAX_HEALTH;
            }

            if(health < 0){
                health = 0;
            }

            motherTile.GetBoat().InflictDamage((int)(TileUtils.TILE_FLOOR_MAX_HEALTH - health), Bullet.DIRECT_DAMAGE);
            //ICI RECALCULER LE MESH DE CA ET CEUX AUTOURS

            if (IsDestroyed()){
               // LA FAUDRA FAIRE LE SYSTEME
               Debug.Log(transform.position);
               Instantiate(DestroyableUtils.GetTileDestroyAlone(), motherTile.transform.position, Quaternion.identity);
               Destroy(gameObject);
            }
        }

        public void SetTile(Tile value){
            motherTile = value;
        }

        public override void Destroy(int i, bool isBaseTile, float delay)
        {
            motherTile.Destroy(i,isBaseTile,delay);
        }

    public override Boat GetBoat()
    {
        return motherTile.GetBoat();
    }

    public override Tile GetTile()
    {
        return motherTile;
    }

}
