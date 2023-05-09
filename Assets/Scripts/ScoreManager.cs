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
    public class EntreeEnter
    {
        public string Nom { get; private set; }
        public int Temps { get; set; }
        public int ArgentCollecte { get; set; }

        public EntreeEnter(string nom, int temps, int argentCollecte)
        {
            Nom = nom;
            Temps = temps;
            ArgentCollecte = argentCollecte;
        }

    }

    public static List<EntreeEnter> scoreJoueurs;
    private static int changementScore = 0;


    public static void InitialisationScoreJoueurs()
    {
        if (scoreJoueurs == null)
            scoreJoueurs = new List<EntreeEnter>();
    }
    public static int GetTypeScore(EntreeEnter entree, string typeScore)
    {
        InitialisationScoreJoueurs();
        int index;

        //if (!scoreJoueurs.Contains(entree))
        {
            //return 0;
        }
        //else
        index = scoreJoueurs.FindIndex(x => (x.ArgentCollecte == entree.ArgentCollecte) && (x.Nom == entree.Nom) && (x.Temps == entree.Temps));

        switch (typeScore)
        {
            case "Temps":
                return scoreJoueurs[index].Temps;

            case "Argent Collecte":
                return scoreJoueurs[index].ArgentCollecte;

            default:
                break;

        }

        throw new TypeScoreIntrouvableException("Type de score mal ecrit");

    }

    public static void SetTypeScore(EntreeEnter entree, int valeur,string typeScore)
    {
        InitialisationScoreJoueurs();

        changementScore++;

        switch (typeScore)
        {
            case "Temps":
                entree.Temps= valeur;
                break;

            case "Argent Collecte":
                entree.ArgentCollecte = valeur;
                break;
        }
    }
    
    public static EntreeEnter SetJoueur(string nom, int temps, int argentCollecte) // Sert a instancier
    {
        InitialisationScoreJoueurs();

        changementScore++;

        var entree = new EntreeEnter(nom, temps, argentCollecte);
        scoreJoueurs.Add(entree);
        return entree;
    }
    public static void ChangerTypeScore(EntreeEnter entree, int addition,string typeScore)
    {

        InitialisationScoreJoueurs();

        int scoreCourrant = GetTypeScore(entree, $"{typeScore}");
        SetTypeScore(entree, scoreCourrant + addition, typeScore);
    }
   

    public static List<EntreeEnter> GetNoms() // on recoit les noms tries selon leur score
    {
        InitialisationScoreJoueurs();
        return scoreJoueurs.OrderByDescending(x => x.Temps).ToList();
    }

    public static int GetchangementScore() => changementScore;

}
