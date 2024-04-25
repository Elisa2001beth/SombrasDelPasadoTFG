using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Character_enemy : CharacterBase
{
    public int Level{get; set;}
    public CharacterBase Base {get; set;}

    public int HP {get; set;}
    public List<Move> Moves {get; set;}

    public Character_enemy(CharacterBase cBase, int cLevel)
    {
        Base = cBase;
        Level = cLevel;

        HP = MaxHp;
        //Init();
        Moves = new List<Move>();

        foreach (var move in Base.LearnableMoves)
        {
            if (move.Level <= Level)
                Moves.Add(new Move(move.Base));

            if (Moves.Count >= 4)
                break;
        }
        
    }

    


    public int Attack
    {
        get { return Mathf.FloorToInt((base.Attack * Level) / 100f) + 5; }
    }

    public int Defense
    {
        get { return Mathf.FloorToInt((base.Defense * Level) / 100f) + 5; }
    }

    public int Hability
    {
        get { return Mathf.FloorToInt((base.Hability * Level) / 100f) + 5; }
    }

    public int Speed
    {
        get { return Mathf.FloorToInt((base.Speed * Level) / 100f) + 5; }
    }

    public int MaxHp
    {
        get { return Mathf.FloorToInt((base.MaxHp * Level) / 100f) + 10; }
    }


    /* public void Init()
    {
        // Generate Moves
        Moves = new List<Move>();
        foreach (var move in Base.LearnableMoves)
        {
            if (move.Level <= Level)
                Moves.Add(new Move(move.Base));

            if (Moves.Count >= CharacterBase.MaxNumOfMoves)
                break;
        }

        //Exp = Base.GetExpForLevel(Level);

        //CalculateStats();
        HP = MaxHp;

        //StatusChanges = new Queue<string>();
        //ResetStatBoost();
        //Status = null;
        //VolatileStatus = null;
    } */
}
