using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NotEnoughPointsException : Exception
{
    public NotEnoughPointsException()
    {
    }

    public NotEnoughPointsException(string message)
        : base(message)
    {
    }

    public NotEnoughPointsException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
public class Spline : MonoBehaviour
{
    [SerializeField] int NbIntervalles = 10;

    List<Vector3> listePoints = new List<Vector3>();
    List<Vector3> ListePoints 

    {
        get => listePoints;

        set
        {
            if (value.Count < 2) 
            {
                throw new NotEnoughPointsException("Il faut donner en argument une liste contenant au minimum 2 points");
            }
            listePoints = value;
        }
    }

    GameObject Thing{ get; set; }

    const int NbCoefficients = 4;
    int incrementSxPair { get; set; } = 0;
    int incrementSxImpair { get; set; } = 0;
   

    public Spline(List<Vector3> listePoints)
    {
        ListePoints = listePoints;
        //TrouverPointsInitiaux();
        // var matA = CréerMatriceA();
        //var coefficients = TrouverMatriceX(ListePoints);
    }
    public Spline(List<Vector3> listePoints, GameObject thing)
    {
        ListePoints = listePoints;
        Thing = thing;
        //TrouverPointsInitiaux();
        // var matA = CréerMatriceA();
        //var coefficients = TrouverMatriceX(ListePoints);
    }

  
    private double[,] CréerMatriceA()
    {
        int nbSx = (ListePoints.Count - 1);             //nb fonctions piecewise
        int bornesExclues = (ListePoints.Count - 2);    // nb de points sans le premier et dernier point
        int columnCount = nbSx * NbCoefficients;
        int testFinal = 2;
        //        test         test f(x)'     test f(x)''    test endpoint(pt initial et final)
        int rowCount = (nbSx * 2) + bornesExclues + bornesExclues + testFinal;
        var matA = new double[rowCount, columnCount];

        var B = Matrix<double>.Build;
        B.Dense(rowCount, columnCount);

        for (int i = 0; i < nbSx * 2; ++i)
        {
            var t1 = Formertest1(columnCount, i); // on vérifie la ligne pour choisir le bon test et le bon point des pointsInitiauxTries, on remplace le point dans le tes
            for (int j = 0; j < t1.Count; ++j)
                matA[i, j] = t1[j]; // on recopie ce qu'on a trouvé
        }


        for (int i = nbSx * 2; i < nbSx * 2 + bornesExclues; ++i)
        {
            var t2 = Formerdxdt1(columnCount, i);
            for (int j = 0; j < t2.Count; ++j)
                matA[i, j] = t2[j];
        }

        for (int i = nbSx * 2 + bornesExclues; i < nbSx * 2 + 2 * bornesExclues; i++)
        {
            var t3 = Formerdxdt2(columnCount, i);
            for (int j = 0; j < t3.Count; ++j)
                matA[i, j] = t3[j];
        }
        for (int i = nbSx * 2 + 2 * bornesExclues; i < nbSx * 2 + 2 * bornesExclues + testFinal; i++)
        {
            var t4 = FormerEndpoint(columnCount, i, rowCount);
            for (int j = 0; j < t4.Count; ++j)
                matA[i, j] = t4[j];
        }
        return matA;
    }

    private List<double> Formertest1(int columnLenght, int row)
    {

        // Algo qui fait un lien entre la liste de points initiaux tries et la matrice A
        int verificationPair = row / 2;
        int verficationImpair = (row + 1) / 2;


        var a = new List<double>(columnLenght);
        if (row % 2 == 0) // vérifie le test 
        {


            double x = ListePoints[verificationPair].x;
            double puissance = 3;
            for (int k = 0; k < columnLenght; k++)
            {
                if (k >= (incrementSxPair * NbCoefficients) && k < (incrementSxPair * NbCoefficients) + NbCoefficients) // 
                {                                                     // Fonction piecewise: S0(x),S1(x),S2(x),S3(x)... 
                                                                      // a chaque 4 lignes on a a1,a2,a3
                    a.Add(Math.Pow(x, puissance));
                    puissance--;
                }
                else
                {
                    a.Add(0);
                }

            }
            incrementSxPair++;   // a chaque fois qu<on augmente de row :2,4,6,8,10,12
        }
        else
        {
            double x = ListePoints[verficationImpair].x;
            double puissance = 3;

            for (int k = 0; k < columnLenght; k++)
            {
                if (k >= (incrementSxImpair * NbCoefficients) && k < (incrementSxImpair * NbCoefficients) + NbCoefficients)
                // row matrice : 1,3,5,7,9... Fonction piecewise : S0(x),S1(x),S2(x),S3(x)... 
                {                                           // a chaque 4 lignes on a a1,a2,a3
                    a.Add(Math.Pow(x, puissance));
                    puissance--;
                }
                else
                {
                    a.Add(0);

                }

            }
            incrementSxImpair++; // a chaque fois qu<on augmente de row : 1,3,5,7,9
        }
        return a;
    }

    private List<double> Formerdxdt1(int columnLenght, int row)
    {
        // Algo qui fait un lien entre la liste de points initiaux tries et la matrice A
        //                                 nb de v/rifications du test1        commence a 0
        int verificationBonPoint = row - ((ListePoints.Count - 1) * 2) + 1; // 1,2,3,4...
        int iterationPoint = row - ((ListePoints.Count - 1) * 2); // 0,1,2...     

        var a = new List<double>(columnLenght);
        double x = ListePoints[verificationBonPoint].x;
        double puissance = 2;
        double coefficient = 3;
        int signe = 1;                  //   3_an-2 *x^2_n-1 + 2b_n-2* xn_-1+ C_n-2 - 3_an-1 *x^2_n-1 - 2b_n-1* xn_-1- C_n-1=0.

        for (int i = 0; i < columnLenght; i++)
        {
            if (i >= (iterationPoint * NbCoefficients) && i < (iterationPoint * NbCoefficients + 2 * NbCoefficients)) // on veut 8 coefficients
            {
                a.Add(signe * coefficient * Math.Pow(x, puissance));

                if (puissance == 0 && coefficient == 0)
                {
                    puissance = 2;
                    coefficient = 3;
                    signe = signe - 2;
                    continue;
                }
                if (coefficient > 0 && puissance > 0)
                {
                    puissance--;
                    coefficient--;
                }
                else if (puissance == 0 && coefficient != 0)  // coefficient diminue encore lorsque puissance =0 
                {
                    coefficient--;
                }


            }
            else
            {
                a.Add(0.0);
            }

        }

        return a;
    }


    private List<double> Formerdxdt2(int columnLenght, int row)
    {
        int bornesExclues = ListePoints.Count - 2;
        // nb de verfifcations test 1 + nb de verfifcations test 2 +2
        int verificationBonPoint = row - (((ListePoints.Count - 1) * 2) + bornesExclues) + 1; //1,2,3
        int iterationPoint = row - (((ListePoints.Count - 1) * 2) + bornesExclues); // 0,1,2     


        var a = new List<double>(columnLenght);
        double x = ListePoints[verificationBonPoint].x;
        double puissance = 1;
        double coefficient = 6;
        int signe = 1;
        float n = 1;
        int rec = 0;
        for (int i = 0; i < columnLenght; i++)
        {
            if (i >= (iterationPoint * NbCoefficients) && i < (iterationPoint * NbCoefficients + 2 * NbCoefficients))
            {
                coefficient = 6 - 4 * (n - 1); //{6,2,0,0,-6,-2,0,0} de -2 a 5 
                a.Add(signe * coefficient * Math.Pow(x, puissance));

                if (puissance == 0 && rec == 1)
                {
                    puissance = 1;
                    n = 1;
                    signe = signe - 2;
                    rec = 0;
                    continue;


                }
                if (coefficient > 0 && puissance > 0) //1
                {
                    puissance--;
                    n++;
                }
                else if (puissance == 0 && coefficient == 2)
                {
                    n = n + 0.5f;
                }
                else
                {
                    // n = 10 / 4;
                    rec++;
                }


            }
            else
            {
                a.Add(0.0);
            }

        }
        return a;
    }
    private List<double> FormerEndpoint(int columnLenght, int row, int rowcount)
    {
        int nbSx = (ListePoints.Count - 1);             //nb fonctions piecewise
        int bornesExclues = (ListePoints.Count - 2);    // nb de points sans le premier et dernier point
        int columnCount = nbSx * NbCoefficients;
        int testFinal = 2;
        int rowCount = (nbSx * 2) + bornesExclues + bornesExclues + testFinal;
        int nbVerificationTest1 = (nbSx * 2);                                               // commence a 1 
        int nbVerificationTest2 = nbVerificationTest1 + bornesExclues;
        int nbVerificationTest3 = nbVerificationTest2 + bornesExclues;
        int nbVerificationTest4 = nbVerificationTest3 + 2;


        var a = new List<double>(columnLenght);
        double xdeb = ListePoints[0].x;
        double xfin = ListePoints[ListePoints.Count - 1].x;
        double puissance = 1;
        double coefficient = 6;
        float n = 1f;
        if (row == rowcount - 1)
        {
            for (int k = 0; k < columnLenght; k++)
            {
                // coefs * avnt dernier nb de tests
                if (k >= nbVerificationTest3 - 2) // si k est plus grand, on rajoute le coefficients an
                {

                    coefficient = 6 - 4 * (n - 1);
                    a.Add(coefficient * Math.Pow(xfin, puissance));
                    if (coefficient > 0 && puissance > 0)
                    {
                        puissance--;
                        n++;
                    }
                    else if (puissance == 0 && coefficient == 2)
                    {
                        n = n + 0.5f;
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

                if (k < NbCoefficients * nbVerificationTest1) // si k est plus grand, on rajoute le coefficients an 
                {
                    coefficient = 6 - 4 * (n - 1);
                    a.Add(coefficient * Math.Pow(xdeb, puissance));
                    if (coefficient > 0 && puissance > 0)
                    {
                        puissance--;
                        n++;
                    }
                    else if (puissance == 0 && coefficient == 2)
                    {
                        n = n + 0.5f;
                    }

                }
                else
                {
                    a.Add(0);
                }

            }
        }
        return a;
    }
    private Matrix<double> CréerMatriceB()
    {
        int nbSx = (ListePoints.Count - 1);
        int bornesExclues = (ListePoints.Count - 2);
        int columnCount = nbSx * 4;
        //        test         test f(x)'     test f(x)''    test endpoint(pt initial et final)
        int rowCount = (nbSx * 2) + bornesExclues + bornesExclues + 2;


        double[] x = new double[rowCount];
        for (int i = 0; i < rowCount; ++i)
        {
            int verificationPair = i / 2;
            int verficationImpair = (i + 1) / 2;
            if (i < nbSx * 2)
            {
                if (i % 2 == 0)
                {
                    x[i] = ListePoints[verificationPair].z;
                }
                else
                {
                    x[i] = ListePoints[verficationImpair].z;
                }
            }
            else
            {
                x[i] = 0.0;
            }
        }
        var B = Matrix<double>.Build;
        return B.Dense(rowCount, 1, x);

    }
    private double[,] TrouverMatriceX(List<Vector3> listePoints)
    {
        //var listv = new List<Vector3>() { new Vector3(0, 1, 0), new Vector3(1, 3, 0), new Vector3(2, 2, 0) };
        var c = Matrix<double>.Build;
        var M = CréerMatriceA();
        var b = CréerMatriceB();
        var a = c.DenseOfArray(M);
        Matrix<double> x = a.Inverse() * b;
        var SxCoefficients = ArrangerCoefficients(x, listePoints);

        return SxCoefficients;
    }
    private double[,] ArrangerCoefficients(Matrix<double> x, List<Vector3> listePoints)
    {
        int nbSx = (listePoints.Count - 1);             //nb fonctions piecewise
        int bornesExclues = (listePoints.Count - 2);    // nb de points sans le premier et dernier point
        int columnCount = nbSx * NbCoefficients;
        int k = 0;
        var SxCoefficients = new double[nbSx, NbCoefficients]; // ListePoints.Count * NbCoefficients
        for (int i = 0; i < nbSx; i++)
        {

            for (int j = 0; j < NbCoefficients; j++)             // il y a 4 coefficients, chaque paire de 4 est dans une liste
                                                                 // matrice nX1 
            {
                SxCoefficients[i, j] = (x[k, 0]);
                k++;
            }
        }
        return SxCoefficients;
    }


    public List<Vector3> TrouverPointsSpline()
    {
        double[,] coefficients = TrouverMatriceX(ListePoints);
        var pointsSpline = new List<Vector3>();
        //nbSx
        for (int i = 0; i < ListePoints.Count - 1; i++)
        {
            float deltaX = (ListePoints[i + 1].x - ListePoints[i].x) / NbIntervalles;

            for (int j = 0; j <= NbIntervalles; j++) //nbpoints=nbintervalles+1
            {
                if (i != 0 && j == 0)
                    continue;

                float x = ListePoints[i].x + j * deltaX;
                float z = (float)(coefficients[i, 0] * Math.Pow(x, 3) + coefficients[i, 1] * Math.Pow(x, 2) + coefficients[i, 2] * Math.Pow(x, 1) + coefficients[i, 3] * Math.Pow(x, 0));
                pointsSpline.Add(new Vector3(x, ListePoints[i].y, z));
              
            }
        }
        foreach (var point in pointsSpline)
            Debug.Log(point);

        return pointsSpline;
    }
    public void InstancierObjets() 
    {
        var listePointsSpline = TrouverPointsSpline();

        foreach(Vector3 points in listePointsSpline) 
        {
            Instantiate(Thing, points, Quaternion.identity);
        }
    }

   

}
