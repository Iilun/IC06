using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bomb : Bullet
{
    [SerializeField]
    private Text countdown;

    private bool isDisarmed;

   
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        StartCoroutine(Explosion());
        isShootable = false;
    }

    private IEnumerator Explosion(){
        for (int i = Bullet.BOMB_EXPLOSION_TIME; i >= 0 ; i--){
            if( i!=0){
                countdown.text = i.ToString();
            }
            yield return new WaitForSeconds(1);
            
        }
        if (!isDisarmed){
            Destroy(gameObject);
            List<Tile> tiles = TileUtils.GetClosestTilesFromBullet(this, 1, 'c');

            Instantiate(DestroyableUtils.GetBigExplosion(), tiles[0].gameObject.transform.position + new Vector3(0f,4f,0f), Quaternion.identity);
            foreach(Tile t in tiles){
                t.Destroy(Destroyable.DESTRUCTION_TOTALE, false, 0);
            }
        }
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer == 3){
            isDisarmed = true;
        }
    }
}
