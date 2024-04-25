using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "Character_Enemy", menuName = "Character/Create new character")]
public class CharacterBase : ScriptableObject {
    
    [SerializeField] string name;
    [SerializeField] Sprite sprite;


    //BASE STATES
    [SerializeField] int maxHP;
    [SerializeField] int attack;
    [SerializeField] int defense;
    [SerializeField] int hability;
    [SerializeField] int speed;

    [SerializeField] List<LearnableMove> learnableMoves;

    public string Name{
        get{return name;}
    }

    public Sprite Imagen{
       get{return sprite;}
    }
    

    public int MaxHp{
        get{return maxHP;}
    }

    public int Attack{
        get{return attack;}
    }
    public int Defense{
        get{return defense;}
    }

    public int Hability{
        get{return hability;}
    }

    public int Speed{
        get{return speed;}
    }

    public List<LearnableMove> LearnableMoves {
        get { return learnableMoves; }
    }

    public static int MaxNumOfMoves { get; set; } = 4;

} 

[System.Serializable]
public class LearnableMove
{
    [SerializeField] MoveBase moveBase;
    [SerializeField] int level;

    public MoveBase Base {
        get { return moveBase; }
    }

    public int Level {
        get { return level; }
    }
}

