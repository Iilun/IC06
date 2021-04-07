using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUtils : MonoBehaviour
{
    public static ItemUtils instance;

    public GameObject normalBullet;

    public GameObject poisonBullet;

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
                Instantiate(instance.poisonBullet, position, Quaternion.identity);
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
        if(ingredientTypes.Contains(Ingredient.TYPE_BETON) && ingredientTypes.Contains(Ingredient.TYPE_POUDRE))
        {
            craft.Remove(Ingredient.TYPE_BETON, true);
            craft.Remove(Ingredient.TYPE_POUDRE, true);           
            return new BulletInfo(Bullet.NORMAL_BULLET);
        } else if(ingredientTypes.Contains(Ingredient.TYPE_POISON) && ingredientTypes.Contains(Ingredient.TYPE_POUDRE))
        {
            craft.Remove(Ingredient.TYPE_POISON, true);
            craft.Remove(Ingredient.TYPE_POUDRE, true);;
            return new BulletInfo(Bullet.FIRE_BULLET);
        }

        return null;
        
    }
}
