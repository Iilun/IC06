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
            if (!isDestroyable){
                
                GetComponent<MeshFilter>().mesh = DestroyableUtils.GetIndestructibleTileMesh(); 
                Material[] mats =  GetComponent<Renderer>().materials;
                mats[0] = DestroyableUtils.GetIndestructibleTileMaterial();
                mats[1] = mats[1];
                GetComponent<Renderer>().materials = mats;
            }
        }
        public bool IsDestroyed(){
            return health <= 0;
        }

        public bool IsDestroyable(){
            return isDestroyable;
        }

        public void Damage(int damageType, bool isBaseTile){
            float damage = 0;
            if (damageType == Destroyable.DESTRUCTION_LEGERE){
                damage = 25;
            } else if (damageType == Destroyable.DESTRUCTION_LOURDE){
                damage = 37.5f;
            } else if (damageType == Destroyable.DESTRUCTION_TOTALE){
                damage = TileUtils.TILE_FLOOR_MAX_HEALTH;
            }
            health -= damage;
            if(health < 0){
                health = 0;
            }

            CalculateBrokenMesh();
            //ICI RECALCULER LE MESH DE CA ET CEUX AUTOURS

            if (IsDestroyed()){
               // LA FAUDRA FAIRE LE SYSTEME
               Debug.Log(transform.position);
               Instantiate(DestroyableUtils.GetTileDestroyAlone(), motherTile.transform.position, Quaternion.identity);
               Destroy(gameObject);
            }
        }

        private void CalculateBrokenMesh(){
            if (health <= 75 && health > 25){
                GetComponent<MeshFilter>().mesh = DestroyableUtils.GetHalfBrokenTileMesh();
            }
            else if (health <= 25){
                GetComponent<MeshFilter>().mesh = DestroyableUtils.GetHeavyBrokenTileMesh();
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
