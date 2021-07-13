using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage
{
    public string name;

    public List<Transform> door;

    public PolygonCollider2D mapCollider;

    public List<Transform> enemy;

    public Stage()
    {
        this.door = new List<Transform>();
        this.enemy = new List<Transform>();
    }

    public Stage(string name, List<Transform> door, PolygonCollider2D mapCollider, List<Transform> enemy)
    {
        this.name = name;
        this.door = door;
        this.mapCollider = mapCollider;
        this.enemy = enemy;
    }

}
