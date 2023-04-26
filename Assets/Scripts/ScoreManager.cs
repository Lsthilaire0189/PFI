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
    public class Entr�eEnter
    {
        public string Nom { get; private set; }
        public int Score { get; set; }
        public int ArgentCollect� { get; set; }

        public Entr�eEnter(string nom, int score, int argentCollect�)
        {
            Nom = nom;
            Score = score;
            ArgentCollect� = argentCollect�;
        }

    }

    public static List<Entr�eEnter> scoreJoueurs;
    private static int changementScore = 0;


    public static void InitialisationScoreJoueurs()
    {
        if (scoreJoueurs == null)
            scoreJoueurs = new List<Entr�eEnter>();
    }
    public static int GetTypeScore(Entr�eEnter entr�e, string typeScore)
    {
        InitialisationScoreJoueurs();
        int index;

        if (!scoreJoueurs.Contains(entr�e))
        {
            return 0;
        }
        else
        {
            index = scoreJoueurs.FindIndex(x => (x.ArgentCollect� == entr�e.ArgentCollect�) && (x.Nom == entr�e.Nom) && (x.Score == entr�e.Score));
        }

        switch (typeScore)
        {
            case "Score":
                return scoreJoueurs[index].Score;

            case "Argent Collect�":
                return scoreJoueurs[index].ArgentCollect�;

            default:
                break;

        }

        throw new TypeScoreIntrouvableException("Type de score mal �crit ");

    }

    //public static int GetArgentCollect�(Entr�eEnter entr�e)
    //{
    //    InitialisationScoreJoueurs();
    //    int index;

    //    if (!scoreJoueurs.Contains(entr�e))
    //    {
    //        return 0;
    //    }
    //    else
    //    {
    //        index = scoreJoueurs.FindIndex(x => (x.ArgentCollect� == entr�e.ArgentCollect�) && (x.Nom == entr�e.Nom) && (x.Score == entr�e.Score));
    //    }


    //    return scoreJoueurs[index].ArgentCollect�;
    //}
    public static void SetTypeScore(Entr�eEnter entr�e, int valeur,string typeScore)
    {
        InitialisationScoreJoueurs();

        changementScore++;

        switch (typeScore)
        {
            case "Score":
                entr�e.Score= valeur;
                break;

            case "Argent Collect�":
                entr�e.Score = valeur;
                break;
        }


   
    }
    //public static void SetArgentCollect�(Entr�eEnter entr�e, int valeur)
    //{
    //    InitialisationScoreJoueurs();

    //    changementScore++;
    //    entr�e.ArgentCollect� = valeur;
    //}
    public static void SetJoueur(string nom, int score, int arentCollecte) // Sert a instancier
    {
        InitialisationScoreJoueurs();

        changementScore++;

        var entr�e = new Entr�eEnter(nom, score, arentCollecte);
        scoreJoueurs.Add(entr�e);
    }
    public static void ChangerTypeScore(Entr�eEnter entr�e, int addition,string typeScore)
    {

        InitialisationScoreJoueurs();

        int scoreCourrant = GetTypeScore(entr�e, $"{typeScore}");
        SetTypeScore(entr�e, scoreCourrant + addition, typeScore);


    }
    //public static void ChangerArgentCollect�(Entr�eEnter entr�e, int addition)
    //{

    //    InitialisationScoreJoueurs();

    //    int scoreCourrant = GetArgentCollect�(entr�e);
    //    SetArgentCollect�(entr�e, scoreCourrant + addition);

    //}

    public static List<Entr�eEnter> GetNoms() // on recoit les noms tries selon leur score
    {
        InitialisationScoreJoueurs();
        return scoreJoueurs.OrderByDescending(x => x.Score).ToList();
    }

    public static int GetchangementScore() => changementScore;



}
