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

    GameObject Thing { get; set; }

    const int NbCoefficients = 4;
    int incrementSxPair { get; set; } = 0;
    int incrementSxImpair { get; set; } = 0;
    double puissance { get; set; }
    double coefficient { get; set; }
    float n { get; set; }

    int nbSx;             //nb fonctions piecewise
    int bornesExclues;    // nb de points sans le premier et dernier point
    int colonneCount;
    int testFinal;
    int ligneCount;
    int nbVerificationTest1;                                               // commence a 1 
    int nbVerificationTest2;
    int nbVerificationTest3;
    int verificationPair;
    int verficationImpair;
    int verificationBonPoint; //1,2,3
    int iterationPoint; // 0,1,2  
    public Spline(List<Vector3> listePoints)
    {
        ListePoints = listePoints;
    }
    public Spline(List<Vector3> listePoints, GameObject thing)
    {
        ListePoints = listePoints;
        Thing = thing;
    }

    /*
      Pour créer la Matrice A, jéai fait 4 for() pour chaque tests, commenéant du nombre de vérifications des tests précédents additionnés
      et allant jusquéaux nombres de vérifications du test courant additionné aux test précédents. Pour chaque double for() une méthode est
      lé pour chaque test . Gréce au i de la premiére boucle, je sais avec quelle ligne on travaille et donc avec quel point on devrait travailler
      pour le test(méthode)  en dedans. Chaque test est recopié é chaque ligne de la matrice. 
   */
    private double[,] CréerMatriceA()
    {
        nbSx = (ListePoints.Count - 1);             //nb fonctions piecewise
        bornesExclues = (ListePoints.Count - 2);    // nb de points sans le premier et dernier point
        colonneCount = nbSx * NbCoefficients;
        int testFinal = 2;
        //                test         test f(x)'     test f(x)''    test endpoint(pt initial et final)
        ligneCount = (nbSx * 2) + bornesExclues + bornesExclues + testFinal;
        var matA = new double[ligneCount, colonneCount];

        //var B = Matrix<double>.Build;
        //B.Dense(ligneCount, colonneCount);

        for (int i = 0; i < nbSx * 2; ++i)
        {
            var t1 = Formertest1(colonneCount, i); // on vérifie la ligne pour choisir le bon test et le bon point de listePoints, on remplace le point dans le test
            for (int j = 0; j < t1.Count; ++j)
                matA[i, j] = t1[j]; // on recopie ce qu'on a trouvé
        }


        for (int i = nbSx * 2; i < nbSx * 2 + bornesExclues; ++i)
        {
            var t2 = Formerdxdt1(colonneCount, i);
            for (int j = 0; j < t2.Count; ++j)
                matA[i, j] = t2[j];
        }

        for (int i = nbSx * 2 + bornesExclues; i < nbSx * 2 + 2 * bornesExclues; i++)
        {
            var t3 = Formerdxdt2(colonneCount, i);
            for (int j = 0; j < t3.Count; ++j)
                matA[i, j] = t3[j];
        }
        for (int i = nbSx * 2 + 2 * bornesExclues; i < nbSx * 2 + 2 * bornesExclues + testFinal; i++)
        {
            var t4 = FormerEndpoint(colonneCount, i, ligneCount);
            for (int j = 0; j < t4.Count; ++j)
                matA[i, j] = t4[j];
        }
        return matA;
    }

    //Cette méthode vérifie que si Si(xi)=yi, alors Si(x_i+1)=y_i+1?, 
    private List<double> Formertest1(int colonneLenght, int noLigne)
    {

        // Algorithme qui choisit l'index de quel point de la liste il faut utiliser en fonction de : Si(xi)=yi ou Si(x_i+1)=y_i+1?
        verificationPair = noLigne / 2;
        verficationImpair = (noLigne + 1) / 2;


        var a = new List<double>(colonneLenght);
        if (noLigne % 2 == 0) // Si(xi)=yi ou Si(x_i+1)=y_i+1?
        {
            écrireTest1(verificationPair, colonneLenght, noLigne, a);
            incrementSxPair++;   // a chaque fois qu'on augmente de ligne :2,4,6,8,10,12...
        }
        else
        {
            écrireTest1(verficationImpair, colonneLenght, noLigne, a);
            incrementSxImpair++; // a chaque fois qu'on augmente de ligne : 1,3,5,7,9...
        }
        return a;
    }

    private void écrireTest1(int verification, int colonneLenght, int noLigne, List<double> a)
    {
        int increment;
        increment = (noLigne % 2 == 0 ? incrementSxPair : incrementSxImpair);

        double x = ListePoints[verification].x;
        double puissance = 3;
        for (int k = 0; k < colonneLenght; k++)
        {
            if (k >= (increment * NbCoefficients) && k < (increment * NbCoefficients) + NbCoefficients)   // Fonction piecewise: S0(x),S1(x),S2(x),S3(x)... 
                                                                                                          // On vérifie é quelle colonne chaque nombre devrait étre écrit par le nombre de fois quéon a fait l'oppération : increment++. 
            {                                                                                             // Aller regarder la photo de la matrice dans mon deuxiéme rapport individuel pour comprendre l'emplacement des nombres dans la matrice
                a.Add(Math.Pow(x, puissance));
                puissance--;
            }
            else
            {
                a.Add(0);
            }

        }

    }

    // Cette méthode vérifie si chaque point entre deux fonctions appartient aux deux fonctions en méme temps (excluant le premier et le dernier point) é léaide la premiére différentielle

    private List<double> Formerdxdt1(int colonneLenght, int noLigne)
    {
        // Détermine le point é utiliser, avec le noLigne de la matrice A --> aprés avoir effectué le test1

        nbVerificationTest1 = (ListePoints.Count - 1) * 2;  // commence é 0
        verificationBonPoint = noLigne - nbVerificationTest1 + 1; // 1,2,3,4...
        iterationPoint = noLigne - nbVerificationTest1; // 0,1,2...


        var a = new List<double>(colonneLenght);
        double x = ListePoints[verificationBonPoint].x;
        double puissance = 2;
        double coefficient = 3;
        int signe = 1;   //négatif ou positif

        // (3a_n-2 * x ^ 2_n-1) + (2b_n-2 * x_n-1) + C_n-2 - (3a_n-1 * x ^ 2_n-1) - (2b_n-1 * x_n-1) - C_n-1 = 0.

        for (int i = 0; i < colonneLenght; i++)
        {
            if (i >= (iterationPoint * NbCoefficients) && i < (iterationPoint * NbCoefficients + 2 * NbCoefficients)) // on veut 8 nombres 
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

    // Cette méthode vérifie si chaque point entre deux fonctions appartient aux deux fonctions en méme temps (excluant le premier et le dernier point) é léaide la deuxiéme différentielle
    private List<double> Formerdxdt2(int columnLenght, int row)
    {
        bornesExclues = ListePoints.Count - 2;
        nbVerificationTest2 = ((ListePoints.Count - 1) * 2) + bornesExclues;
        verificationBonPoint = (row - nbVerificationTest2) + 1; //1,2,3
        iterationPoint = row - nbVerificationTest2; // 0,1,2     


        var a = new List<double>(columnLenght);
        double x = ListePoints[verificationBonPoint].x;
        //(6a_n-2 * x_n-1 )+ 2b_n-2 -( 6a_n-1 * x_n-1) - 2b_n-1 = 0.


        double puissance = 1;
        double coefficient = 6;
        int signe = 1;
        float n = 1;
        int rec = 0; // indicateur pour changer le signe {6,2,0,0,-6,-2,0,0}
        for (int i = 0; i < columnLenght; i++)
        {
            if (i >= (iterationPoint * NbCoefficients) && i < (iterationPoint * NbCoefficients + 2 * NbCoefficients))
            {
                coefficient = 6 - 4 * (n - 1); //{6,2,0,0,-6,-2,0,0} 
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
                    //lorsque  n = 5/2;
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
    //Cette méthode vérifie si le premier et le dernier point sont =0 é léaide de la 2e différentielle? f'(x)=y, f"(x)=0
    private List<double> FormerEndpoint(int columnLenght, int row, int rowcount)
    {
        nbSx = (ListePoints.Count - 1);             //nb fonctions piecewise
        bornesExclues = (ListePoints.Count - 2);    // nb de points sans le premier et dernier point
        colonneCount = nbSx * NbCoefficients;
        testFinal = 2;
        ligneCount = (nbSx * 2) + bornesExclues + bornesExclues + testFinal;
        nbVerificationTest1 = (nbSx * 2);                                               // commence a 1 
        nbVerificationTest2 = nbVerificationTest1 + bornesExclues;
        nbVerificationTest3 = nbVerificationTest2 + bornesExclues;

        double xdeb = ListePoints[0].x;
        double xfin = ListePoints[ListePoints.Count - 1].x;
        puissance = 1;
        coefficient = 6;
        n = 1f;


        var a = new List<double>(columnLenght);

        if (row == rowcount - 1)  // 6a_n-1 * x_n + 2b_n-1 = 0.
        {
            for (int k = 0; k < columnLenght; k++)
            {
                // coefs * avnt dernier nb de tests
                if (k >= nbVerificationTest3 - 2) // si k est plus grand, on rajoute le coefficients an
                {
                    CalculerEndPoint(a, xfin);
                }
                else
                {
                    a.Add(0);
                }

            }
        }
        else // 6a1 * x1 + 2b1 = 0.
        {
            for (int k = 0; k < columnLenght; k++)
            {

                if (k < NbCoefficients * nbVerificationTest1)
                {
                    CalculerEndPoint(a, xdeb);
                }
                else
                {
                    a.Add(0);
                }
            }

        }
        return a;
    }
    private void CalculerEndPoint(List<double> a, double x)
    {
        coefficient = 6 - 4 * (n - 1);
        a.Add(coefficient * Math.Pow(x, puissance));
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
    // Cette méthode construit la matrice B : ce sont les composantes en z de ListePoints
    private Matrix<double> CréerMatriceB()
    {
        nbSx = (ListePoints.Count - 1);
        bornesExclues = (ListePoints.Count - 2);
        //        test         test f(x)'     test f(x)''    test endpoint(pt initial et final)
        ligneCount = (nbSx * 2) + bornesExclues + bornesExclues + 2;


        double[] x = new double[ligneCount];
        for (int i = 0; i < ligneCount; ++i)
        {
            verificationPair = i / 2;                             // La méme méthode d'index est utilisé que lors de FormerTest1(), car les composantes en z des points de ListePoints[index].z 
            verficationImpair = (i + 1) / 2;                      // doivent étre placés de la méme faéon que l'orde de vérification de FormerTest1().Le reste sont des 0. Exemple : [11,22,22,33,33,44, 0,0,0,0,0,0]
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
        return B.Dense(ligneCount, 1, x); // x est recopié dans B 

    }
    private double[,] TrouverMatriceX(List<Vector3> listePoints)
    {
        var c = Matrix<double>.Build;
        var M = CréerMatriceA();
        var b = CréerMatriceB();
        var a = c.DenseOfArray(M);
        Matrix<double> x = a.Inverse() * b;
        var SxCoefficients = ArrangerCoefficients(x, listePoints);

        return SxCoefficients;
    }

    //  Chaque groupe de 4 coefficients est placé selon leur ordre dans dans un tableau[,]
    private double[,] ArrangerCoefficients(Matrix<double> x, List<Vector3> listePoints)
    {
        nbSx = (listePoints.Count - 1);             //nb fonctions piecewise
        bornesExclues = (listePoints.Count - 2);    // nb de points sans le premier et dernier point
        colonneCount = nbSx * NbCoefficients;
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

    // Création des points intermédiaires entre chacun des points de ListePoints
    public List<Vector3> TrouverPointsSpline()
    {
        double[,] coefficients = TrouverMatriceX(ListePoints);
        var pointsSpline = new List<Vector3>();

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

        foreach (Vector3 points in listePointsSpline)
        {
            Instantiate(Thing, points, Quaternion.identity);
        }
    }



}
