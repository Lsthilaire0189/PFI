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
    public class EntréeEnter
    {
        public string Nom { get; private set; }
        public int Temps { get; set; }
        public int ArgentCollecté { get; set; }

        public EntréeEnter(string nom, int temps, int argentCollecté)
        {
            Nom = nom;
            Temps = temps;
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

        //if (!scoreJoueurs.Contains(entrée))
        {
            //return 0;
        }
        //else
        index = scoreJoueurs.FindIndex(x => (x.ArgentCollecté == entrée.ArgentCollecté) && (x.Nom == entrée.Nom) && (x.Temps == entrée.Temps));

        switch (typeScore)
        {
            case "Temps":
                return scoreJoueurs[index].Temps;

            case "Argent Collecté":
                return scoreJoueurs[index].ArgentCollecté;

            default:
                break;

        }

        throw new TypeScoreIntrouvableException("Type de score mal écrit ");

    }

    public static void SetTypeScore(EntréeEnter entrée, int valeur,string typeScore)
    {
        InitialisationScoreJoueurs();

        changementScore++;

        switch (typeScore)
        {
            case "Temps":
                entrée.Temps= valeur;
                break;

            case "Argent Collecté":
                entrée.ArgentCollecté = valeur;
                break;
        }
    }
    
    public static EntréeEnter SetJoueur(string nom, int temps, int argentCollecte) // Sert a instancier
    {
        InitialisationScoreJoueurs();

        changementScore++;

        var entrée = new EntréeEnter(nom, temps, argentCollecte);
        scoreJoueurs.Add(entrée);
        return entrée;
    }
    public static void ChangerTypeScore(EntréeEnter entrée, int addition,string typeScore)
    {

        InitialisationScoreJoueurs();

        int scoreCourrant = GetTypeScore(entrée, $"{typeScore}");
        SetTypeScore(entrée, scoreCourrant + addition, typeScore);
    }
   

    public static List<EntréeEnter> GetNoms() // on recoit les noms tries selon leur score
    {
        InitialisationScoreJoueurs();
        return scoreJoueurs.OrderByDescending(x => x.Temps).ToList();
    }

    public static int GetchangementScore() => changementScore;

}
