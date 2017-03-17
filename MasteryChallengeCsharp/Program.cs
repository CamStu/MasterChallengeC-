using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatOps;
using MatOps.Matrix;

namespace MatrixTesting
{
    class Program
    {
        static int Main(string[] args) 
        {
            if(args.Length == 0)
            {
                Console.WriteLine("No command line arguments provided");
                return 1;
            }
            //Expecting command line arguments in the form: ./matrixtesting <REAL> <IMAG>
            double real;
            if (!double.TryParse(args[0],out real))
            {
                Console.WriteLine("Error with first numerical command line input");
                return 1;
            }
            double imag;
            if (!double.TryParse(args[1], out imag))
            {
                Console.WriteLine("Error with second numerical command line input");
                return 1;
            }
            pwrVector vector = new pwrVector(real, imag);
            pwrVector tester = new pwrVector(1, 1);

            Console.WriteLine("Adding tester vector to input vector: ");
            Console.WriteLine("Input: " + vector.real + "+j" + vector.imag);
            Console.WriteLine("Tester: " + tester.real + "+j" + vector.imag);
            vector.add(tester);
            Console.WriteLine("Result: " + vector.real + "+j" + vector.imag);

            Console.WriteLine("Create a 3x3 matrix with all entries equal to input vector");
            pwrVector[] col = new pwrVector[3] { vector.deepCopy(), vector.deepCopy(), vector.deepCopy() };
            pwrMatrix mat = new pwrMatrix(col, col, col);
            mat.prnMat();

            Console.WriteLine("Now to invert that matrix: ");
            mat.invert();
            mat.prnMat();

            Console.ReadLine();
            return 0;
           
        }
    }
}
