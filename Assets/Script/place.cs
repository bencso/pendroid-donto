using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using System.Linq;

public class towerTile
{
    public string name;
    public TileBase tile;

    public towerTile(string name, TileBase tile)
    {
        this.name = name;
        this.tile = tile;
    }
}

public class towertype
{
    public string name;


    public int cost;
    public int hp;
    public int range;
    public int damage;
    public int attackSpeed;
    public int speed;
    public towerTile tile;
    public Sprite projectile;

    public towertype(string name, int cost, int hp, int range, int damage, int attackSpeed, int speed, towerTile tile, Sprite projectile)
    {
        this.name = name;
        this.cost = cost;
        this.hp = hp;
        this.range = range;
        this.damage = damage;
        this.attackSpeed = attackSpeed;
        this.speed = speed;
        this.tile = tile;
        this.projectile = projectile;
        
    }
}

public class tower
{
    public int x;
    public int y;
    public int z;

    public towertype type;

    public tower(int x, int y, int z, towertype type)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.type = type;

    }

}

public class attackUnit
{
    public int x;
    public int y;
    public int z;
    public towertype type;

    public attackUnit(int x, int y, int z, towertype type)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.type = type;
    }

    public void moveForward()
    {
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

    public int rowLength = 14;

    public List<towertype> attackTowers = new List<towertype>();

    private List<tower> towers = new List<tower>();
    private List<attackUnit> attackUnits = new List<attackUnit>();

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("place");
        // towertype towerType = new towertype("test", 100, 110, 10, 10, 1, 1, new towerTile("test", towerTile), projectile);
        attackTowers.Add(new towertype("ranged", 100, 110, 4, 4, 6, 4, new towerTile("test", towerTile), projectile));
        attackTowers.Add(new towertype("fast", 100, 150, 1, 5, 1, 5, new towerTile("test", towerTile), projectile));
        attackTowers.Add(new towertype("strong", 100, 200, 10, 3, 1, 1, new towerTile("test", towerTile), projectile));
        tower tower = new tower(0, 0, 0, attackTowers[Random.Range(0, attackTowers.Count)]);
        towers.Add(tower);

        Debug.Log(towers[0].type.name);
        startRound();
        List<int> usedrows = attackUnits.Select(unit => unit.y).Distinct().ToList();
        foreach (var row in usedrows)
        {
            var defenseTowers = GenerateDefenseTowers(attackUnits.Where(unit => unit.y == row).ToList());
            foreach (var defenseTower in defenseTowers)
            {
                {
                    var towersinrow = towers.Where(t => t.y == row);
                    if (towersinrow.Count() == 0)
                    {
                        towers.Add(new tower(rowLength - 1, row, 0, defenseTower));
                    }
                    else
                    {
                        towers.Add(new tower(rowLength - (towersinrow.Count() + 1), row, 0, defenseTower));
                    }
            }
        }
        }

        foreach (var defenseTower in towers)
        {
            Debug.Log($"Tower: {defenseTower.x}, {defenseTower.y}, {defenseTower.z} | Type: {defenseTower.type.name}");
        }
        Debug.Log(towers.Count);
    }

    public List<towertype> GenerateRandomAttackUnits(int count = 3)
    {
        List<towertype> units = new List<towertype>();
        string[] unitNames = { "Gyors", "Erős", "Távolsági" };

        for (int i = 0; i < count; i++)
        {
            int cost = Random.Range(20, 81);  // 20-80 közötti költség
            int range = Random.Range(1, 4);  // 3-12 közötti hatótáv
            int damage = Random.Range(5, 21); // 5-30 közötti sebzés
            int speed = Random.Range(5, 10);  // 5-15 közötti sebesség
            int attackSpeed = Random.Range(1, 5);  // 1-5 közötti támadási sebesség
            int hp = Random.Range(100, 201);  // 100-200 közötti HP

            towertype unit = new towertype(
                unitNames[i],
                cost,
                hp,
                range,
                damage,
                attackSpeed,
                speed,
                new towerTile(unitNames[i], towerTile),
                projectile
            );

            units.Add(unit);
        }

        return units;
    }

    private List<towertype> GenerateDefenseTowers(List<attackUnit> enemyUnits)
    {
        List<towertype> defenseTowers = new List<towertype>();

        // Statisztikák számítása az ellenséges egységekről
        float avgDamage = 0;
        float avgSpeed = 0;
        float avgRange = 0;
        float avgAttackSpeed = 0;
        float avgHp = 0;

        foreach (var unit in enemyUnits)
        {
            avgDamage += unit.type.damage;
            avgSpeed += unit.type.speed;
            avgRange += unit.type.range;
            avgAttackSpeed += unit.type.attackSpeed;
            avgHp += unit.type.hp;
        }

        avgDamage /= (enemyUnits.Count / 2);
        avgSpeed /= enemyUnits.Count;
        avgRange /= (enemyUnits.Count * 2.5f);
        avgAttackSpeed /= enemyUnits.Count;
        avgHp /= enemyUnits.Count;

        List<string> sortedStats = sortStats(new Dictionary<string, int> {
            {"speed", Mathf.RoundToInt(avgSpeed)},
            {"damage", Mathf.RoundToInt(avgDamage)},
            {"range", Mathf.RoundToInt(avgRange)},
            {"attackSpeed", Mathf.RoundToInt(avgAttackSpeed)}

        });

        for(int i = 0; i < enemyUnits.Count; i++) {
            switch (sortedStats[0])
        {
            case "speed":
                defenseTowers.Add(new towertype("Gyors Védő", 100, Mathf.RoundToInt(avgHp * 0.7f), Mathf.RoundToInt(avgRange * 0.7f), Mathf.RoundToInt(avgDamage * 0.7f), Mathf.RoundToInt(avgSpeed * 1.5f), Mathf.RoundToInt(avgAttackSpeed * 1.5f), new towerTile("Gyors Védő", towerTile), projectile));
                break;
            case "damage":
                defenseTowers.Add(new towertype("Erős Védő", 100, Mathf.RoundToInt(avgHp * 0.6f), Mathf.RoundToInt(avgRange * 0.6f), Mathf.RoundToInt(avgDamage * 1.5f), Mathf.RoundToInt(avgSpeed * 0.7f), Mathf.RoundToInt(avgAttackSpeed * 1.5f), new towerTile("Erős Védő", towerTile), projectile));
                break;
            case "range":
                defenseTowers.Add(new towertype("Távolsági Védő", 100, Mathf.RoundToInt(avgHp * 0.8f), Mathf.RoundToInt(avgRange * 1.4f), Mathf.RoundToInt(avgDamage * 0.8f), Mathf.RoundToInt(avgSpeed * 0.8f), Mathf.RoundToInt(avgAttackSpeed * 1.5f), new towerTile("Távolsági Védő", towerTile), projectile));
                break;
        }
        }

        // Tornyok generálása az ellenséges statisztikák alapján
        // 1. Gyors támadó torony a gyors egységek ellen




        return defenseTowers;
    }

    public List<string> sortStats(Dictionary<string, int> stats)
    {
        List<string> sortedStats = new List<string>();
        List<int> sortedValues = new List<int>();
        foreach (var stat in stats)
        {
            sortedStats.Add(stat.Key);
            sortedValues.Add(stat.Value);
        }
        for (int i = 0; i < sortedValues.Count; i++)
        {
            for (int j = 0; j < sortedValues.Count; j++)
            {
                if (sortedValues[i] < sortedValues[j])
                {
                    sortedStats[i] = sortedStats[j];
                    
                }
            }
        }
        return sortedStats;

    }

    public void startRound()
    {
        attackUnits.Clear();
        int attackUnitCount = 0;
        if (round == 0)
        {
            attackUnitCount = 3;
        }
        else
        {
            attackUnitCount = 3 + round;
        }


        // Generáljuk a random egységeket
        List<towertype> randomUnits = GenerateRandomAttackUnits(attackUnitCount);
        Dictionary<int, int> unitCount = new Dictionary<int, int>();
        unitCount.Add(1, 0);
        unitCount.Add(2, 0);
        unitCount.Add(3, 0);
        unitCount.Add(4, 0);
        foreach (var unit in randomUnits)
        {
            int random = Random.Range(1, 5);
            unitCount[random]++;
            attackUnits.Add(new attackUnit(unitCount[random], random, 0, unit));
        }

        // Debug.Log a generált egységek tulajdonságainak megjelenítéséhez
        foreach (var unit in randomUnits)
        {
            Debug.Log($"Egység: {unit.name}, Költség: {unit.cost}, " +
                     $"HP: {unit.hp}, Hatótáv: {unit.range}, Sebzés: {unit.damage}, " +
                     $"Sebesség: {unit.speed}, Támadási sebesség: {unit.attackSpeed}");
        }

        foreach(var unit in attackUnits) {
            Debug.Log($"Egység: {unit.x}, {unit.y}, {unit.z} | Type: {unit.type.name}");
        }
        Debug.Log("--------------------------------");

        round++;

    }


    // Update is called once per frame
    void Update()
    {
    }
}
