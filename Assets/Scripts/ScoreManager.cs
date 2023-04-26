using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class TypeScoreIntrouvableException : Exception
{
    public TypeScoreIntrouvableException()
    {
    }

    public TypeScoreIntrouvableException(string message)
        : base(message)
    {
    }

    public TypeScoreIntrouvableException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
public static class ScoreManager
{
    public class EntréeEnter
    {
        public string Nom { get; private set; }
        public int Score { get; set; }
        public int ArgentCollecté { get; set; }

        public EntréeEnter(string nom, int score, int argentCollecté)
        {
            Nom = nom;
            Score = score;
            ArgentCollecté = argentCollecté;
        }

    }

    public static List<EntréeEnter> scoreJoueurs;
    private static int changementScore = 0;


    public static void InitialisationScoreJoueurs()
    {
        if (scoreJoueurs == null)
            scoreJoueurs = new List<EntréeEnter>();
    }
    public static int GetTypeScore(EntréeEnter entrée, string typeScore)
    {
        InitialisationScoreJoueurs();
        int index;

        if (!scoreJoueurs.Contains(entrée))
        {
            return 0;
        }
        else
        {
            index = scoreJoueurs.FindIndex(x => (x.ArgentCollecté == entrée.ArgentCollecté) && (x.Nom == entrée.Nom) && (x.Score == entrée.Score));
        }

        switch (typeScore)
        {
            case "Score":
                return scoreJoueurs[index].Score;

            case "Argent Collecté":
                return scoreJoueurs[index].ArgentCollecté;

            default:
                break;

        }

        throw new TypeScoreIntrouvableException("Type de score mal écrit ");

    }

    //public static int GetArgentCollecté(EntréeEnter entrée)
    //{
    //    InitialisationScoreJoueurs();
    //    int index;

    //    if (!scoreJoueurs.Contains(entrée))
    //    {
    //        return 0;
    //    }
    //    else
    //    {
    //        index = scoreJoueurs.FindIndex(x => (x.ArgentCollecté == entrée.ArgentCollecté) && (x.Nom == entrée.Nom) && (x.Score == entrée.Score));
    //    }


    //    return scoreJoueurs[index].ArgentCollecté;
    //}
    public static void SetTypeScore(EntréeEnter entrée, int valeur,string typeScore)
    {
        InitialisationScoreJoueurs();

        changementScore++;

        switch (typeScore)
        {
            case "Score":
                entrée.Score= valeur;
                break;

            case "Argent Collecté":
                entrée.Score = valeur;
                break;
        }


   
    }
    //public static void SetArgentCollecté(EntréeEnter entrée, int valeur)
    //{
    //    InitialisationScoreJoueurs();

    //    changementScore++;
    //    entrée.ArgentCollecté = valeur;
    //}
    public static void SetJoueur(string nom, int score, int arentCollecte) // Sert a instancier
    {
        InitialisationScoreJoueurs();

        changementScore++;

        var entrée = new EntréeEnter(nom, score, arentCollecte);
        scoreJoueurs.Add(entrée);
    }
    public static void ChangerTypeScore(EntréeEnter entrée, int addition,string typeScore)
    {

        InitialisationScoreJoueurs();

        int scoreCourrant = GetTypeScore(entrée, $"{typeScore}");
        SetTypeScore(entrée, scoreCourrant + addition, typeScore);


    }
    //public static void ChangerArgentCollecté(EntréeEnter entrée, int addition)
    //{

    //    InitialisationScoreJoueurs();

    //    int scoreCourrant = GetArgentCollecté(entrée);
    //    SetArgentCollecté(entrée, scoreCourrant + addition);

    //}

    public static List<EntréeEnter> GetNoms() // on recoit les noms tries selon leur score
    {
        InitialisationScoreJoueurs();
        return scoreJoueurs.OrderByDescending(x => x.Score).ToList();
    }

    public static int GetchangementScore() => changementScore;



}
