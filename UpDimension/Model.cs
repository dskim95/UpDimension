using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Single;
using MathNet.Numerics.LinearAlgebra.Factorization;

namespace UpDimension
{
    public class Model
    {
        public List<Scene> scene;
        public List<Vertex3D> point;
        public List<Face> face;
        public float center;

        public Model()
        {
            scene = new List<Scene>();
            point = new List<Vertex3D>();
            face = new List<Face>();
            center = 0;
        }

        public void create3DModel()
        {
            int i, j, k;
            int count;
            int n = 0; // point number
            int m = 0; // image number
            int maxPointNumber = 0;
            int var = 0; // number of variables
            int eq = 0; // number of equations

            for (i = 0; i < scene.Count; i++)
            {
                if (scene[i].vertexList.Count - 1 > maxPointNumber)
                    maxPointNumber = scene[i].vertexList.Count - 1;
            }
            Console.WriteLine("maxPointNumber: {0}", maxPointNumber);

            for (i = 0; i <= maxPointNumber; i++)
            {
                count = 0;

                for (j = 0; j < scene.Count; j++)
                {
                    if (i < scene[j].vertexList.Count)
                        if (scene[j].vertexList[i].coord.X != -1)
                            count++;
                }

                if (count >= 2)
                    n++;
            }

            int[] indexToPointNumber = new int[n];

            k = 0;
            for (i = 0; i <= maxPointNumber; i++)
            {
                count = 0;

                for (j = 0; j < scene.Count; j++)
                {
                    if (i < scene[j].vertexList.Count)
                        if (scene[j].vertexList[i].coord.X != -1)
                            count++;
                }

                if (count >= 2)
                    indexToPointNumber[k++] = i;
            }
            
            for (i = 0; i < scene.Count; i++)
            {
                count = 0;

                for (j = 0; j < n; j++)
                {
                    if (indexToPointNumber[j] < scene[i].vertexList.Count)
                    {
                        if (scene[i].vertexList[indexToPointNumber[j]].coord.X != -1)
                            count++;
                    }
                }

                if (count >= 2)
                    m++;
            }
            

            int[] indexToImageNumber = new int[n];

            k = 0;
            for (i = 0; i < scene.Count; i++)
            {
                count = 0;

                for (j = 0; j < n; j++)
                {
                    if (indexToPointNumber[j] < scene[i].vertexList.Count)
                        if (scene[i].vertexList[indexToPointNumber[j]].coord.X != -1)
                            count++;
                }

                if (count >= 2)
                    indexToImageNumber[k++] = i;
            }

            for (i = 0; i < n; i++)
            {
                for (j = 0; j < m; j++)
                {
                    if (indexToPointNumber[i] < scene[indexToImageNumber[j]].vertexList.Count)
                        if (scene[indexToImageNumber[j]].vertexList[indexToPointNumber[i]].coord.X != -1)
                            eq++;
                }
            }
            
            var = 3 * n - 3 + 3 * m - 3;
            eq = 2 * eq - 2;

            Console.WriteLine("{0}, {1}, {2}, {3}", n, m, var, eq);

            Matrix<float> A = Matrix<float>.Build.Dense(eq, var);
            Vector<float> b = Vector<float>.Build.Dense(eq);
            float[] t = new float[3];

            count = 0;
            for (i = 0; i < n; i++)
            {
                for (j = 0; j < m; j++)
                {
                    
                    if (i == 0 && j == 0)
                    {
                        PointF point = scene[indexToImageNumber[j]].vertexList[indexToPointNumber[i]].coord;
                        t[0] = point.X;
                        t[1] = point.Y;
                        t[2] = 1;
                        continue;
                    }

                    if (indexToPointNumber[i] < scene[indexToImageNumber[j]].vertexList.Count)
                        if (scene[indexToImageNumber[j]].vertexList[indexToPointNumber[i]].coord.X != -1)
                        {
                            PointF point = scene[indexToImageNumber[j]].vertexList[indexToPointNumber[i]].coord;
                            Matrix<float> R = DenseMatrix.OfArray(new float[,]
                            {
                                {1, 1, 1 },
                                {1, 1, 1 },
                                {1, 1, 1 }
                            });
                            if (i != 0)
                            {
                                A[count, 3 * i - 3] = point.X * R[2, 0] - R[0, 0];
                                A[count, 3 * i - 2] = point.X * R[2, 1] - R[0, 1];
                                A[count, 3 * i - 1] = point.X * R[2, 2] - R[0, 2];
                                A[count + 1, 3 * i - 3] = point.Y * R[2, 0] - R[1, 0];
                                A[count + 1, 3 * i - 2] = point.Y * R[2, 1] - R[1, 1];
                                A[count + 1, 3 * i - 1] = point.Y * R[2, 2] - R[1, 2];
                            }
                            if (j != 0)
                            {
                                A[count, 3 * n - 3 + 3 * j - 3] = -1;
                                A[count, 3 * n - 3 + 3 * j - 1] = point.X;
                                A[count + 1, 3 * n - 3 + 3 * j - 2] = -1;
                                A[count + 1, 3 * n - 3 + 3 * j - 1] = point.Y;
                            }
                            else
                            {
                                b[count] = -t[0] + point.X * t[2];
                                b[count + 1] = -t[1] + point.Y * t[2];
                            }
                            Console.WriteLine("dd");
                            count = count + 2;
                        }
                }
            }

            QR<float> qr = A.QR(QRMethod.Full);
            var x = qr.Solve(b);

            printMatrix(A);
            
            Console.WriteLine(x);
        }

        void printMatrix(Matrix<float> matrix)
        {
            for (int i = 0; i < matrix.RowCount; i++)
            {
                for (int j = 0; j < matrix.ColumnCount; j++)
                {
                    Console.Write(matrix[i,j] + " ");
                }
                Console.Write("\n");
            }
        }
    }
}
