using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class towerTile {
    public string name;
    public TileBase tile;

    public towerTile(string name, TileBase tile) {
        this.name = name;
        this.tile = tile;
    }
}

public class towertype {
    public string name;
    public int cost;
    public int range;
    public int damage;
    public int speed;
    public towerTile tile;
    public Sprite projectile;

    public towertype(string name, int cost, int range, int damage, int speed, towerTile tile, Sprite projectile) {
        this.name = name;
        this.cost = cost;
        this.range = range;
        this.damage = damage;
        this.speed = speed;
        this.tile = tile;
        this.projectile = projectile;
    }
}

public class tower {
    public int x;
    public int y;
    public int z;

    public towertype type;

    public tower(int x, int y, int z, towertype type) {
        this.x = x;
        this.y = y;
        this.z = z;
        this.type = type;

    }
}



public class place : MonoBehaviour
{

    public TileBase towerTile;

    public towertype towerType = new towertype("test", 100, 10, 10, 10, new towerTile("test", towerTile), new Sprite());

    private List<tower> towers = new List<tower>();

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("place");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
