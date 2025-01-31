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

public class attackUnit {
    public int x;
    public int y;
    public int z;
    public towertype type;

    public attackUnit(int x, int y, int z, towertype type) {
        this.x = x;
        this.y = y;
        this.z = z;
        this.type = type;
    }

    public void moveForward() {
        this.x++;
    }
}

public class place : MonoBehaviour
{

    public TileBase towerTile;
    public Sprite projectile;

    public int round = 0;

    public int userMoney = 100;
    public int enemyMoney = 100;


    private List<tower> towers = new List<tower>();
    private List<attackUnit> attackUnits = new List<attackUnit>();

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("place");
        towertype towerType = new towertype("test", 100, 10, 10, 10, new towerTile("test", towerTile), projectile);
        tower tower = new tower(0, 0, 0, towerType);
        towers.Add(tower);

        Debug.Log(towers[0].type.name);
    }

    private List<towertype> GenerateRandomAttackUnits(int count = 3)
    {
        List<towertype> units = new List<towertype>();
        string[] unitNames = { "Gyors", "Erős", "Távolsági" };
        
        for (int i = 0; i < count; i++)
        {
            int cost = Random.Range(20, 81);  // 20-80 közötti költség
            int range = Random.Range(3, 13);  // 3-12 közötti hatótáv
            int damage = Random.Range(5, 31); // 5-30 közötti sebzés
            int speed = Random.Range(5, 16);  // 5-15 közötti sebesség
            
            towertype unit = new towertype(
                unitNames[i],
                cost,
                range,
                damage,
                speed,
                new towerTile(unitNames[i], towerTile),
                projectile
            );
            
            units.Add(unit);
        }
        
        return units;
    }

    public void startRound()
    {
        attackUnits.Clear();
        if(round == 0)
        {
            userMoney = 100;
            enemyMoney = 100;
            
            // Generáljuk a random egységeket
            List<towertype> randomUnits = GenerateRandomAttackUnits();
            
            // Debug.Log a generált egységek tulajdonságainak megjelenítéséhez
            foreach (var unit in randomUnits)
            {
                Debug.Log($"Egység: {unit.name}, Költség: {unit.cost}, " +
                         $"Hatótáv: {unit.range}, Sebzés: {unit.damage}, " +
                         $"Sebesség: {unit.speed}");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
