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
    [Serializable]
    public class Entr�eEnter
    {
        public string Nom { get; private set; }
        public int Temps { get; set; }
        public int ArgentCollect� { get; set; }

        public Entr�eEnter(string nom, int temps, int argentCollect�)
        {
            Nom = nom;
            Temps = temps;
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

        //if (!scoreJoueurs.Contains(entr�e))
        {
            //return 0;
        }
        //else
        index = scoreJoueurs.FindIndex(x => (x.ArgentCollect� == entr�e.ArgentCollect�) && (x.Nom == entr�e.Nom) && (x.Temps == entr�e.Temps));

        switch (typeScore)
        {
            case "Temps":
                return scoreJoueurs[index].Temps;

            case "Argent Collect�":
                return scoreJoueurs[index].ArgentCollect�;

            default:
                break;

        }

        throw new TypeScoreIntrouvableException("Type de score mal �crit ");

    }

    public static void SetTypeScore(Entr�eEnter entr�e, int valeur,string typeScore)
    {
        InitialisationScoreJoueurs();

        changementScore++;

        switch (typeScore)
        {
            case "Temps":
                entr�e.Temps= valeur;
                break;

            case "Argent Collect�":
                entr�e.ArgentCollect� = valeur;
                break;
        }
    }
    
    public static Entr�eEnter SetJoueur(string nom, int temps, int argentCollecte) // Sert a instancier
    {
        InitialisationScoreJoueurs();

        changementScore++;

        var entr�e = new Entr�eEnter(nom, temps, argentCollecte);
        scoreJoueurs.Add(entr�e);
        return entr�e;
    }
    public static void ChangerTypeScore(Entr�eEnter entr�e, int addition,string typeScore)
    {

        InitialisationScoreJoueurs();

        int scoreCourrant = GetTypeScore(entr�e, $"{typeScore}");
        SetTypeScore(entr�e, scoreCourrant + addition, typeScore);
    }
   

    public static List<Entr�eEnter> GetNoms() // on recoit les noms tries selon leur score
    {
        InitialisationScoreJoueurs();
        return scoreJoueurs.OrderByDescending(x => x.Temps).ToList();
    }

    public static int GetchangementScore() => changementScore;

}
