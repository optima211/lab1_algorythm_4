using System;

namespace lab1_algorythm_4
{
    internal static class Program
    {
        /// <summary>
        /// Maksimalnaya koordinata matrici po X.
        /// </summary>
        private const int MaxX = 6;

        /// <summary>
        /// Maksimalnaya koordinata matrici po Y.
        /// </summary>
        private const int MaxY = 5;

        /// <summary>
        /// Osnovnaya programma.
        /// </summary>
        private static void Main()
        {
            var arr = GetDefaultData();

            Console.WriteLine("Ishodnaya sistema uravneniy:");
            DisplayArray(arr);

            CalcOneStep(arr, 0);
            CalcOneStep(arr, 1);
            CalcOneStep(arr, 2);
            CalcOneStep(arr, 3);
            CalcOneStep(arr, 4);

            DisplayResult(arr);

            Console.ReadKey();
        }

        /// <summary>
        /// Metod dlya polucheniya iznachalnih dannih sistemi uravneniy.
        /// </summary>
        /// <returns></returns>
        private static double[,] GetDefaultData()
        {
            var arr = new double[MaxX, MaxY];
            arr[0, 0] = 3; arr[1, 0] = 1; arr[2, 0] = 5; arr[3, 0] = -2; arr[4, 0] = 3; arr[5, 0] = 35;
            arr[0, 1] = 4; arr[1, 1] = 3; arr[2, 1] = -7; arr[3, 1] = 5; arr[4, 1] = 6; arr[5, 1] = 54;
            arr[0, 2] = -7; arr[1, 2] = 5; arr[2, 2] = 4; arr[3, 2] = 1; arr[4, 2] = -1; arr[5, 2] = -96;
            arr[0, 3] = 1; arr[1, 3] = 4; arr[2, 3] = 1; arr[3, 3] = -3; arr[4, 3] = -10; arr[5, 3] = -71;
            arr[0, 4] = 6; arr[1, 4] = -9; arr[2, 4] = -8; arr[3, 4] = -8; arr[4, 4] = -2; arr[5, 4] = 59;
            return arr;
        }

        /// <summary>
        /// Metod dlya rascheta odnogo shaga po rezultiruyushemu elementu.
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="razElemIndex"></param>
        private static void CalcOneStep(double[,] arr, int razElemIndex)
        {
            var iterationNumber = razElemIndex + 1;
            var columnNumber = razElemIndex + 1;
            Console.WriteLine($"Iteraciya {iterationNumber}:");
            Console.WriteLine($"Viberem maksimalniy po modulu element v {columnNumber} stolbce.");

            var maxValueRowIndex = razElemIndex;
            double maxValue = 0;
            var maxValueAbs = double.MinValue;

            for (var j = 0; j < MaxY; j++)
            {
                var val = arr[razElemIndex, j];
                var absVal = Math.Abs(arr[razElemIndex, j]);
                if (absVal > maxValueAbs)
                {
                    maxValueAbs = absVal;
                    maxValue = val;
                    maxValueRowIndex = j;
                }
            }

            var maxValueRowNumber = maxValueRowIndex + 1;

            Console.WriteLine($"On nahodiysya v {maxValueRowNumber} stroke (eto element {maxValue:F}).");
            Console.WriteLine($"Pomenyaem mestami stroki {maxValueRowNumber} i {iterationNumber}.");

            for (var i = 0; i < MaxX; i++)
            {
                var valueBefore = arr[i, razElemIndex];
                arr[i, razElemIndex] = arr[i, maxValueRowIndex];
                arr[i, maxValueRowIndex] = valueBefore;
            }

            DisplayArray(arr);

            Console.WriteLine($"Razdelim {iterationNumber} stroku na diagonalniy element a{iterationNumber}{iterationNumber} ({arr[razElemIndex, razElemIndex]:F}).");
            Console.WriteLine($"Vse elementi {columnNumber} stolbca, krome a{iterationNumber}{iterationNumber}, ravni 0.");
            Console.WriteLine($"Dlya ostalnih elementov matrici postroim pryamougolniki s vershinami v etih elementah aij i videlennom elemente a{iterationNumber}{iterationNumber}.");

            var razElem = arr[razElemIndex, razElemIndex];
            for (var i = 0; i < MaxX; i++)
            {
                var val = arr[i, razElemIndex];
                var newVal = val / razElem;
                arr[i, razElemIndex] = newVal;
            }
            for (var j = 0; j < MaxY; j++)
            {
                if (j == razElemIndex) continue;

                var valInColumn = arr[razElemIndex, j];

                for (var i = 0; i < MaxX; i++)
                {
                    var val = arr[i, j];
                    var newVal = val - arr[i, razElemIndex] * valInColumn;
                    arr[i, j] = newVal;
                }
            }

            DisplayArray(arr);
        }

        /// <summary>
        /// Metod dlya otobrazheniya tekushego sostoyanie massiva (sistemi uravneniy).
        /// </summary>
        /// <param name="arr"></param>
        private static void DisplayArray(double[,] arr)
        {
            for (var j = 0; j < MaxY; j++)
            {
                switch (j)
                {
                    case 0:
                        Console.Write("/");
                        break;
                    case MaxY - 1:
                        Console.Write("\\");
                        break;
                    default:
                        Console.Write("|");
                        break;
                }

                for (var i = 0; i < MaxX; i++)
                {
                    var val = arr[i, j];
                    var index = j + 1;
                    Console.Write(i != MaxX - 1 ? $" {val,6:F}*x{index}" : $" = {val,6:F}");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Metod dlya otobrazheniya rezultatov vichisleniy.
        /// </summary>
        /// <param name="arr"></param>
        private static void DisplayResult(double[,] arr)
        {
            Console.WriteLine("Rezultat:");
            for (var j = 0; j < MaxY; j++)
            {
                var val = arr[MaxX - 1, j];
                Console.WriteLine($"x{j + 1} = {val,5:F}");
            }
        }
    }
}
