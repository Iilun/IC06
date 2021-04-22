using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUtils : MonoBehaviour
{
    public static ItemUtils instance;

    public GameObject normalBullet;
    public GameObject fireBullet;
    public GameObject iceBullet;

    public GameObject iemBullet;
    public GameObject windBullet;

    void Awake()
    {
        instance = this;
    }

    public static void CreateBullet(BulletInfo bullet, Vector3 position)
    {

        if (bullet != null)
        {
            if(bullet.GetType() == Bullet.NORMAL_BULLET)
            {

                Instantiate(instance.normalBullet, position, Quaternion.identity);
            }
            if (bullet.GetType() == Bullet.FIRE_BULLET)
            {
                Instantiate(instance.fireBullet, position, Quaternion.identity);
            }
            if (bullet.GetType() == Bullet.ICE_BULLET)
            {
                Instantiate(instance.iceBullet, position, Quaternion.identity);
            }
            if (bullet.GetType() == Bullet.IEM_BULLET)
            {
                Instantiate(instance.iemBullet, position, Quaternion.identity);
            }
            if (bullet.GetType() == Bullet.WIND_BULLET)
            {
                Instantiate(instance.windBullet, position, Quaternion.identity);
            }
            if (bullet.GetType() == Bullet.BOMB_BULLET)
            {
                GameObject bomb = Instantiate(DestroyableUtils.GetBomb(), position, Quaternion.identity);
                bomb.GetComponent<Bomb>().SetIsShootable(true);
            }


        }
    }

    public static BulletInfo CraftBullet(CraftStation craft)
    {
        HashSet<int> ingredientTypes = new HashSet<int>();

        ingredientTypes.Add(craft.GetIng(0).GetType());
        
        if (craft.GetIng(1) != null)
        {
            ingredientTypes.Add(craft.GetIng(1).GetType());

            if (craft.GetIng(2) != null)
            {
                ingredientTypes.Add(craft.GetIng(2).GetType());
                if (craft.GetIng(3) != null)
                {
                    ingredientTypes.Add(craft.GetIng(3).GetType());
                }
            }
        }
        if(ingredientTypes.Contains(Ingredient.TYPE_FER) && ingredientTypes.Contains(Ingredient.TYPE_DYNAMITE))
        {
            craft.Remove(Ingredient.TYPE_FER, true);
            craft.Remove(Ingredient.TYPE_DYNAMITE, true);           
            return new BulletInfo(Bullet.NORMAL_BULLET);
        } else if(ingredientTypes.Contains(Ingredient.TYPE_DYNAMITE) && ingredientTypes.Contains(Ingredient.TYPE_POUDRE))
        {
            craft.Remove(Ingredient.TYPE_DYNAMITE, true);
            craft.Remove(Ingredient.TYPE_POUDRE, true);;
            return new BulletInfo(Bullet.FIRE_BULLET);
        }  else if(ingredientTypes.Contains(Ingredient.TYPE_DYNAMITE) && ingredientTypes.Contains(Ingredient.TYPE_EAU))
        {
            craft.Remove(Ingredient.TYPE_DYNAMITE, true);
            craft.Remove(Ingredient.TYPE_EAU, true);;
            return new BulletInfo(Bullet.ICE_BULLET);
        } else if(ingredientTypes.Contains(Ingredient.TYPE_FER) && ingredientTypes.Contains(Ingredient.TYPE_EAU))
        {
            craft.Remove(Ingredient.TYPE_FER, true);
            craft.Remove(Ingredient.TYPE_EAU, true);;
            return new BulletInfo(Bullet.IEM_BULLET);
        } else if(ingredientTypes.Contains(Ingredient.TYPE_FER) && ingredientTypes.Contains(Ingredient.TYPE_POUDRE))
        {
            craft.Remove(Ingredient.TYPE_FER, true);
            craft.Remove(Ingredient.TYPE_POUDRE, true);;
            return new BulletInfo(Bullet.BOMB_BULLET);
        } else if(ingredientTypes.Contains(Ingredient.TYPE_POUDRE) && ingredientTypes.Contains(Ingredient.TYPE_EAU))
        {
            craft.Remove(Ingredient.TYPE_POUDRE, true);
            craft.Remove(Ingredient.TYPE_EAU, true);;
            return new BulletInfo(Bullet.WIND_BULLET);
        } 

        return null;
        
    }
}
