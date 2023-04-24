using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
public class Circuit : MonoBehaviour
{
 //    [SerializeField] int NbIntervalles = 10;
//    public RoadHelper roadHelper;
    List<Vector3> pointsInitiauxTries { get; set; }
    List<Vector3> pointsIntervallesTries { get; set; }


    
    private void ExecuterMatriceA() 
    {
        var listv = new List<Vector3>() { new Vector3(0, 1, 0), new Vector3(1, 3, 0), new Vector3(2, 2, 0) };
        var a = CréerMatriceATest(listv);
    }
    public Circuit()
    {
        ExecuterMatriceA();
    }

    
    private List<Vector3> convertirEnVector3(List<Vector3Int> listeRoute)
    {
        List<Vector3> nvListeRoute = new List<Vector3>(listeRoute.Count);
        foreach (Vector3Int route in listeRoute)
        {
            nvListeRoute.Add((Vector3)route);
        }
        return nvListeRoute;
    }
    
    private double[,] CréerMatriceATest(List<Vector3> pointstTest)
    {

        int nbSx = (pointstTest.Count - 1);
        int bornesExclues = (pointstTest.Count - 2);
        int columnCount = nbSx * 4;
        //        test         test f(x)'     test f(x)''    test endpoint(pt initial et final)
        int rowCount = (nbSx * 2) + bornesExclues + bornesExclues + 2;

        var matA = new double[rowCount, columnCount];


        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                if (i < (nbSx * 2)) //borne1
                {
                    var t = Formertest11(columnCount, i, pointstTest); // on vérifie la ligne pour choisir le bon test et le bon point des pointsInitiauxTries, on remplace le point dans le test
                    matA[i, j] = t[j]; // on recopie ce qu'on a trouvé
                }

                else if ((i >= nbSx * 2) && (i < (nbSx * 2) + bornesExclues)) //borne 2
                {
                    var t = Formerdxdt11(columnCount, i, pointstTest); // on vérifie la ligne pour choisir le bon test et le bon point des pointsInitiauxTries, on remplace le point dans le test
                    matA[i, j] = t[j]; // on recopie ce qu'on a trouvé
                }
                else if ((i >= nbSx * 2) && (i < (nbSx * 2) + bornesExclues)) //borne 3
                {
                    var t = Formerdxdt22(columnCount, i, pointstTest); // on vérifie la ligne pour choisir le bon test et le bon point des pointsInitiauxTries, on remplace le point dans le test
                    matA[i, j] = t[j];
                }
                else
                {
                    var t = FormerEndpoint1(columnCount, i, rowCount, pointstTest);
                    matA[i, j] = t[j];
                }
                // rechercher else if 
            }
        }
        return matA;
    }
   
    private List<double> Formertest11(int columnLenght, int row, List<Vector3> pointstTest)
    {
        // Algo qui fait un lien entre la liste de points initiaux tries et la matrice A
        int verificationPair = row / 2;
        int verficationImpair = (row + 1) / 2;

        var a = new List<double>(columnLenght);
        if (row % 2 == 0) // vérifie le test 
        {
            double x = pointstTest[verificationPair].x;
            double puissance = 3;
            for (int k = 0; k < columnLenght; k++)
            {
                if (k + puissance < 4)
                {
                    a.Add(Math.Pow(x, puissance));
                    puissance--;
                }
                else
                {
                    a.Add(0);
                }

            }

        }
        else
        {
            double x = pointstTest[verficationImpair].x;
            double puissance = 3;

            for (int k = 0; k < columnLenght; k++)
            {
                if (k + puissance < 4)
                {
                    a.Add(0);
                }
                else
                {
                    a.Add(Math.Pow(x, puissance));
                    puissance--;
                }

            }

        }
        return a;
    }
    
    private List<double> Formerdxdt11(int columnLenght, int row, List<Vector3> pointstTest)
    {
        // Algo qui fait un lien entre la liste de points initiaux tries et la matrice A
        //                            nb de v/rifications du test1        commence a 0
        int verificationBonPoint = row - (((pointstTest.Count - 1) * 2) - 1);

        var a = new List<double>(columnLenght);
        double x = pointstTest[verificationBonPoint].x;
        double puissance = 2;
        double coefficient = 3;
        for (int i = 0; i < columnLenght; i++)
        {
            a.Add(coefficient * Math.Pow(x, puissance));
            puissance--;
            coefficient--;
            if (puissance == 0 && coefficient == 0)
            {
                puissance = 2;
                coefficient = 3;
            }
        }
        return a;
    }

    private List<double> Formerdxdt22(int columnLenght, int row, List<Vector3> pointstTest)
    {
        int bornesExclues = pointstTest.Count - 2;
        int verificationBonPoint = row - ((((pointstTest.Count - 1) * 2) - 1) + bornesExclues);
        var a = new List<double>(columnLenght);
        double x = pointstTest[verificationBonPoint].x;
        double puissance = 1;
        double coefficient = 6;
        int n = 1;
        for (int i = 0; i < columnLenght; i++)
        {
            coefficient = 6 - 4 * (n - 1);
            a.Add(coefficient * Math.Pow(x, puissance));
            if (coefficient > 0 && puissance > 0)
            {
                puissance--;
                n++;
            }
            if (puissance == 0 && coefficient == 0 && i == columnLenght / 2)
            {
                puissance = 1;
                coefficient = 6;
            }
        }
        return a;
    }

    
    private List<double> FormerEndpoint1(int columnLenght, int row, int rowcount, List<Vector3> pointstTest)
    {
        var a = new List<double>(columnLenght);
        double xdeb = pointstTest[0].x;
        double xfin = pointstTest[pointstTest.Count - 1].x;
        double puissance = 1;
        double coefficient = 6;
        int n = 1;
        if (row == rowcount - 1)
        {
            for (int k = 0; k < columnLenght; k++)
            {

                if (k < columnLenght / 2)
                {

                    coefficient = 6 - 4 * (n - 1);
                    a.Add(coefficient * Math.Pow(xfin, puissance));
                    if (coefficient > 0 && puissance > 0)
                    {
                        puissance--;
                        n++;
                    }
                }
                else
                {
                    a.Add(0);
                }

            }
        }
        else
        {
            for (int k = 0; k < columnLenght; k++)
            {

                if (k < columnLenght / 2)
                {
                    a.Add(0);
                }
                else
                {
                    coefficient = 6 - 4 * (n - 1);
                    a.Add(coefficient * Math.Pow(xfin, puissance));
                    if (coefficient > 0 && puissance > 0)
                    {
                        puissance--;
                        n++;
                    }
                }

            }
        }
        return a;
    }
    private void CréerIntervalles()
    {
        for (int i = 0; i < pointsInitiauxTries.Count; ++i)
        {
            if (i % 2 != 0)
            {

            }
        }
    }
    //private void TrouverCoefficients()
    //{
    //    var listv = new List<Vector3>() { new Vector3(0, 1, 0), new Vector3(1, 3, 0), new Vector3(2, 2, 0) };
    //    var M = Matrix<double>.Build;
    //    var a = CréerMatriceATest(listv);
    //    Debug.Log(a.ToString());
    //    M.DenseOfArray(a);

    //}

}
