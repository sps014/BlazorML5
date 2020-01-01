using System;
using System.Collections.Generic;
using System.Text;

namespace ML5
{
    public class Matrix
    {
        private static Random random = new Random(DateTime.Now.Millisecond);

        private List<List<double>> data;
        public double this[int i, int j]
        {
            get
            {
                if (i > rows || j > columns)
                    throw new Exception($"Matrix Index out of bound [{i},{j}] for size [{rows},{columns}]");

                return data[i][j];
            }
            set
            {

                if (i > rows || j > columns)
                    throw new Exception($"Matrix Index out of bound [{i},{j}] for size [{rows},{columns}]");



                DataChanged?.Invoke(this, new DataChangeArgs()
                {
                    NewValue = value,
                    OldValue = data[i][j],
                    IndexCol = (uint)j,
                    IndexRow = (uint)i
                });

                data[i][j] = value;
            }
        }

        private uint rows;
        private uint columns;

        public uint Rows
        {
            get
            {
                return rows;
            }
            set
            {
                RowChanged?.Invoke(this, rows);
                rows = value;
                RecreateMatrix();
            }
        }
        public uint Columns
        {
            get
            {
                return columns;
            }
            set
            {
                ColumnChanged?.Invoke(this, columns);
                columns = value;
                RecreateMatrix();
            }
        }

        public Matrix()
        {
            data = new List<List<double>>();
        }

        public Matrix(int rows, int columns)
        {
            data = new List<List<double>>();
            Rows = (uint)rows;
            Columns = (uint)columns;
        }
        public Matrix(uint rows, uint columns)
        {
            data = new List<List<double>>();
            Rows = rows;
            Columns = columns;

        }

        public void RandomizeMatrixValue()
        {
            RecreateMatrixWithRandom();
        }


        private void RecreateMatrix()
        {
            data.Clear();
            for (int i = 0; i < rows; i++)
            {
                data.Add(new List<double>());
                for (int j = 0; j < columns; j++)
                {
                    data[i].Add(0);
                }
            }
        }
        private void RecreateMatrixWithRandom()
        {
            data.Clear();
            for (int i = 0; i < rows; i++)
            {
                data.Add(new List<double>());
                for (int j = 0; j < columns; j++)
                {
                    double a = random.NextDouble();
                    data[i].Add(a);
                }
            }
        }


        public Matrix Add(Matrix B)
        {
            if (B.Columns != Columns || B.Rows != Rows)
                throw new Exception("For Addition dimension of both matrix should be same -> " + $"A:{Rows}x{Columns} and B:{B.Rows}x{B.Columns}");


            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    this[i, j] += B[i, j];
                }
            }


            return this;
        }
        public static Matrix Add(Matrix A, Matrix B)
        {
            if (B.Columns != A.Columns || B.Rows != A.Rows)
                throw new Exception("For Addition dimension of both matrix should be same -> " + $"A:{A.Rows}x{A.Columns} and B:{B.Rows}x{B.Columns}");

            Matrix C = new Matrix(A.Rows, B.Columns);

            for (int i = 0; i < A.Rows; i++)
            {
                for (int j = 0; j < A.Columns; j++)
                {
                    C[i, j] = A[i, j] + B[i, j];
                }
            }

            return C;
        }
        public static Matrix operator +(Matrix A, Matrix B)
        {
            return Add(A, B);
        }
        public double[] ToArray()
        {
            List<double> no = new List<double>();
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    no.Add(this[i, j]);
                }
            }
            return no.ToArray();
        }
        public static double[] ToArray(Matrix matrix)
        {
            List<double> no = new List<double>();
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    no.Add(matrix[i, j]);
                }
            }
            return no.ToArray();
        }
        public static Matrix FromArray(double[] array,int r,int col)
        {
            Matrix wtMatrix = new Matrix(r,col);
            int c = 0;
            for (int i = 0; i < r; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    wtMatrix[i, j] = array[c++];
                }
            }
            return wtMatrix;
        }


        public Matrix Subtract(Matrix B)
        {
            if (B.Columns != Columns || B.Rows != Rows)
                throw new Exception("For Subtraction dimension of both matrix should be same -> " + $"A:{Rows}x{Columns} and B:{B.Rows}x{B.Columns}");


            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    this[i, j] -= B[i, j];
                }
            }


            return this;
        }
        public static Matrix Subtract(Matrix A, Matrix B)
        {
            if (B.Columns != A.Columns || B.Rows != A.Rows)
                throw new Exception("For Subtraction dimension of both matrix should be same -> " + $"A:{A.Rows}x{A.Columns} and B:{B.Rows}x{B.Columns}");

            Matrix C = new Matrix(A.Rows, B.Columns);

            for (int i = 0; i < B.Rows; i++)
            {
                for (int j = 0; j < B.Columns; j++)
                {
                    C[i, j] = A[i, j] - B[i, j];
                }
            }

            return C;
        }
        public static Matrix operator -(Matrix A, Matrix B)
        {
            return Matrix.Subtract(A, B);
        }


        public Matrix Multiply(Matrix B)
        {
            if (Columns != B.Rows)
                throw new Exception("Can't Multiply with unequal number of columns and rows of the first and second matrix respectively ->" + $"A:{Rows}x{Columns} and B:{B.Rows}x{B.Columns}");

            Matrix C = new Matrix(Rows, B.Columns);

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < B.Columns; j++)
                {
                    for (int k = 0; k < Columns; k++)
                    {
                        C[i, j] += this[i, k] * B[k, j];
                    }
                }
            }

            C.CopyTo(this);

            return C;
        }
        public static Matrix Multiply(Matrix A, Matrix B)
        {
            if (A.Columns != B.Rows)
                throw new Exception("Can't Multiply with unequal number of columns and rows of the first and second matrix respectively -> " + $"A:{A.Rows}x{A.Columns} and B:{B.Rows}x{B.Columns}");

            Matrix C = new Matrix(A.Rows, B.Columns);

            for (int i = 0; i < A.Rows; i++)
            {
                for (int j = 0; j < B.Columns; j++)
                {
                    for (int k = 0; k < B.Columns; k++)
                    {
                        C[i, j] += A[i, k] * B[k, j];
                    }
                }
            }

            return C;
        }
        public static Matrix operator *(Matrix A, Matrix B)
        {
            return Matrix.Multiply(A, B);
        }


        public Matrix Multiply(double B)
        {

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    this[i, j] *= B;
                }
            }

            return this;
        }
        public static Matrix Multiply(Matrix A, double B)
        {
            Matrix C = new Matrix(A.Rows, A.Columns);

            for (int i = 0; i < A.Rows; i++)
            {
                for (int j = 0; j < A.Columns; j++)
                {
                    A[i, j] = A[i, j] * B;
                }
            }

            return C;
        }
        public static Matrix operator *(Matrix A, double B)
        {
            return Matrix.Multiply(A, B);
        }


        public Matrix HadamardProduct(Matrix B)
        {
            if (B.Columns != Columns || B.Rows != Rows)
                throw new Exception("For Hadmard Product dimension of both matrix should be same -> " + $"A:{Rows}x{Columns} and B:{B.Rows}x{B.Columns}");


            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    this[i, j] *= B[i, j];
                }
            }

            return this;
        }
        public static Matrix HadamardProduct(Matrix A, Matrix B)
        {
            if (B.Columns != A.Columns || B.Rows != A.Rows)
                throw new Exception("For Hadmard Product dimension of both matrix should be same -> " + $"A:{A.Rows}x{A.Columns} and B:{B.Rows}x{B.Columns}");

            Matrix C = new Matrix(A.Rows, B.Columns);

            for (int i = 0; i < A.Rows; i++)
            {
                for (int j = 0; j < A.Columns; j++)
                {
                    C[i, j] = A[i, j] + B[i, j];
                }
            }

            return C;
        }

        public Matrix Transpose()
        {
            Matrix C = new Matrix(Columns, Rows);

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    C[i, j] = this[i, j];
                }
            }

            C.CopyTo(this);

            return C;
        }
        public static Matrix Transpose(Matrix A)
        {
            Matrix C = new Matrix(A.Columns, A.Rows);

            for (int i = 0; i < A.Rows; i++)
            {
                for (int j = 0; j < A.Columns; j++)
                {
                    C[i, j] = A[i, j];
                }
            }

            return C;
        }


        public static Matrix Identity(int n)
        {
            Matrix identity = new Matrix(n, n);
            for (int i = 0; i < n; ++i)
                identity[i, i] = 1;

            return identity;
        }


        public double GetAt(int i, int j)
        {
            return this[i, j];
        }
        public void SetAt(int i, int j, double Value)
        {
            this[i, j] = Value;
        }

        public Matrix Map(Func<double, double> func)
        {


            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    this[i, j] = func(this[i, j]);
                }
            }

            return this;
        }
        public static Matrix Map(Matrix A, Func<double, double> func)
        {

            Matrix C = new Matrix(A.Rows, A.Columns);

            for (int i = 0; i < A.Rows; i++)
            {
                for (int j = 0; j < A.Columns; j++)
                {
                    C[i, j] = func(A[i, j]);
                }
            }

            return C;
        }


        public Matrix FromArray(double[] array, bool rowVector = true)
        {
            Matrix c;

            if (rowVector)
            {
                c = new Matrix(array.Length, 1);
                for (int i = 0; i < array.Length; i++)
                {
                    c[i, 0] = array[i];
                }
            }
            else
            {
                c = new Matrix(1, array.Length);
                for (int i = 0; i < array.Length; i++)
                {
                    c[0, i] = array[i];
                }
            }

            c.CopyTo(this);

            return c;
        }
        public static Matrix fromArray(double[] array, bool rowVector = true)
        {
            Matrix c;

            if (rowVector)
            {
                c = new Matrix(array.Length, 1);
                for (int i = 0; i < array.Length; i++)
                {
                    c[i, 0] = array[i];
                }
            }
            else
            {
                c = new Matrix(1, array.Length);
                for (int i = 0; i < array.Length; i++)
                {
                    c[0, i] = array[i];
                }
            }

            return c;
        }

        public void CopyTo(Matrix Target)
        {
            Target.Rows = Rows;
            Target.Columns = Columns;

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {

                    Target[i, j] = this[i, j];
                }
            }
        }

        public string ToString(int dashMultipiler = 12)
        {
            StringBuilder builder = new StringBuilder("");

            for (int i = 0; i < Rows; i++)
            {
                if (i == 0)
                {
                    for (int k = 0; k < dashMultipiler * Columns; k++)
                        builder.Append("-");
                    builder.Append("\r\n");

                }

                for (int j = 0; j < Columns; j++)
                {
                    if (j == 0)
                        builder.Append("|");

                    string value = data[i][j].ToString();
                    builder.Append(value.PadLeft(7) + " ".PadLeft(System.Math.Abs(7 - value.Length)) + "|");

                }
                builder.Append("\r\n");


                for (int k = 0; k < dashMultipiler * Columns; k++)
                    builder.Append("-");
                builder.Append("\r\n");

            }

            return builder.ToString();
        }


        public delegate void RowChangedHandler(Matrix matrix, uint oldValue);
        public event RowChangedHandler RowChanged;

        public delegate void ColumnChangedHandler(Matrix matrix, uint oldValue);
        public event ColumnChangedHandler ColumnChanged;

        public delegate void DataChangedHandler(Matrix matrix, DataChangeArgs e);
        public event DataChangedHandler DataChanged;

        public class DataChangeArgs : EventArgs
        {
            public uint IndexRow { get; set; }
            public uint IndexCol { get; set; }
            public double OldValue { get; set; }
            public double NewValue { get; set; }
        }
    }
}
