using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    Spawnpoint[] spawnpoints;

    PhotonView pv;

    [SerializeField]
    public GameObject items;

    void Awake()
    {
        instance = this;
        pv = GetComponent<PhotonView>();
        spawnpoints = GetComponentsInChildren<Spawnpoint>();
    }

    void Start()
    {
        Debug.Log("spawnpoint 數量 : " + spawnpoints.Length);

        GunGenerator();
        
        AmmoGenerator();

        KitGenerator();
    }


    void KitGenerator() 
    {
        bool flag = true;

        int count = 0;
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("一共有 " + spawnpoints.Length + " 個重生點");
            while (flag)
            {
                if (count < 20)
                {
                    CreateKit(count);
                    CreateKit(count);
                    count++;
                }

                if (count == 20)
                {
                    flag = false;
                }
            }
        }
        Debug.Log("一共產生 " + count + " 把槍");
    }

    void GunGenerator()
    {
        Weapon[] weapons = new Weapon[4];
        weapons[0] = new Weapon("PistolItem", 6);
        weapons[1] = new Weapon("RifleItem", 8);
        weapons[2] = new Weapon("ShotgunItem", 5);
        weapons[3] = new Weapon("SniperItem", 1);

        bool flag = true;

        int count = 0;
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("一共有 " + spawnpoints.Length + " 個重生點");
            while (flag)
            {
                int num1 = Random.Range(0, 1000) % 4;
                if (weapons[num1].getAmount() > 0)
                {
                    CreateGun(count, weapons[num1].getWeaponName());
                    weapons[num1].decreaceAmount();
                    count++;
                }

                if (count == 20)
                {
                    flag = false;
                }
            }
        }
        Debug.Log("一共產生 " + count + " 把槍");
    }

    void AmmoGenerator()
    {
        Ammo[] ammos = new Ammo[3];
        ammos[0] = new Ammo("PistolAmmo", 20);
        ammos[1] = new Ammo("RifleAmmo", 20);
        ammos[2] = new Ammo("ShotgunAmmo", 20);
        bool flag = true;

        int count = 0;
        int loop = 0;
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("一共有 " + spawnpoints.Length + " 個重生點");
            while (flag)
            {
                int num1 = Random.Range(0, 1000) % 3;

                if (ammos[num1].getAmount() > 0)
                {
                    CreateAmmo(count, ammos[num1].getAmmoName());
                    ammos[num1].decreaceAmount();
                    count++;
                }

                if (count == 20)
                {
                    loop += 1;
                    count = 0;
                }

                if (loop == 3)
                {
                    flag = false;
                }
            }
        }
        Debug.Log("一共產生 " + count + " 彈藥");
    }

    void CreateGun(int index, string str)
    {
        Vector3 pos = spawnpoints[index].transform.position;
        pos.y += 520;
        spawnpoints[index].transform.position = pos;
        Transform spawnpoint = spawnpoints[index].transform;
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs/Gun", str), spawnpoint.position, spawnpoint.rotation, 0, new object[] { pv.ViewID });
    }

    void CreateAmmo(int index, string str)
    {
        int z = Random.Range(-1000, 1000) % 20;
        int x = Random.Range(-1000, 1000) % 20;
        Vector3 pos = spawnpoints[index].transform.position;
        pos.y += 5;
        pos.z += z;
        pos.x += x;
        spawnpoints[index].transform.position = pos;
        Transform spawnpoint = spawnpoints[index].transform;
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs/Kits", str), spawnpoint.position, spawnpoint.rotation, 0, new object[] { pv.ViewID });
    }

    void CreateKit(int index)
    {
        Vector3 pos = spawnpoints[index].transform.position;
        pos.y += 2;
        spawnpoints[index].transform.position = pos;
        Transform spawnpoint = spawnpoints[index].transform;
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs/Kits", "Burger"), spawnpoint.position, spawnpoint.rotation, 0, new object[] { pv.ViewID });
    }

    public Spawnpoint[] GetSpawnpoint()
    {
        return spawnpoints;
        // spawnpoints[Random.Range(0, spawnpoints.Length)].transform
    }
}
