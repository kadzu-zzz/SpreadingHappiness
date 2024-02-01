using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Access with Pools.Instance().pool_type.Func()
 * probably CreatePoolable() or DestroyPoolable(type)
 * */
public class Pools 
{
    static Pools instance;

    public static Pools Instance()
    {
        if (instance == null)
        {
            instance = new Pools();

            //cache the resource.load instead of searching every Instantiation.
            instance.obj_bobabit = Resources.Load<GameObject>("Prefabs/BobaBit 1");
            instance.obj_enemyproj = Resources.Load<GameObject>("Prefabs/EnemyBullet");

            //Create & Setup the object pools
            instance.bobaBitPool = new();
            instance.bobaBitPool.Initialize(128, () => { return GameObject.Instantiate(instance.obj_bobabit).GetComponent<BobaBitController>(); });
            instance.enemyBulletPool = new();
            instance.enemyBulletPool.Initialize(128, () => { return GameObject.Instantiate(instance.obj_enemyproj).GetComponent<EnemyBulletController>(); });
        }
        return instance;
    }

    public static void Clean()
    {
        if (instance != null)
        {
            instance.bobaBitPool.Destroy();
            instance.enemyBulletPool.Destroy();
            instance = null;
        }
    }

    GameObject obj_bobabit;
    GameObject obj_enemyproj;

    //Pools for objects here.
    public ObjectPool<BobaBitController> bobaBitPool;
    public ObjectPool<EnemyBulletController> enemyBulletPool;

}
