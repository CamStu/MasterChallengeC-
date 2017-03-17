using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatOps.Matrix
{
    class pwrMatrix
    {
        //Constructors
        /// <summary>
        /// Default constructor creates 3x3 matrix of all zeros
        /// </summary>
        public pwrMatrix()
        {
            pwrVector zer1 = new pwrVector(0, 0);
            this.data = new pwrVector[3, 3];
            for(int i=0; i<3; i++)
            {
                for(int j=0; j<3; j++)
                {
                    this.data[i, j] = zer1.deepCopy();
                }
            }
        }
        /// <summary>
        /// Creates a 3x1 pwrMatrix of the form [one,two,three]
        /// </summary>
        /// <param name="one">Top value</param>
        /// <param name="two">Middles value</param>
        /// <param name="three">Bottom value</param>
        public pwrMatrix(pwrVector one, pwrVector two, pwrVector three)
        {
            data = new pwrVector[3, 1];
           // rows = 3;
           // cols = 1;
            data[0, 0] = one.deepCopy();//Array syntax should be [row,col]
            data[1, 0] = two.deepCopy();
            data[2, 0] = three.deepCopy();

        }
        /// <summary>
        /// Creates a 3x1 pwrMatrix from given column
        /// </summary>
        /// <param name="one_col"></param>
        public pwrMatrix(pwrVector[] one_col)
        {
            data = new pwrVector[3, 1];
           // rows = 3;
           // cols = 1;
            data[0,0] = one_col[0].deepCopy();//Array syntax should be [row,col]
            data[1, 0] = one_col[1].deepCopy();
            data[2, 0] = one_col[2].deepCopy();

        }
        /// <summary>
        /// Creates a 3x3 pwrMatrix from three columns of the form [one_col,two_col,three_col] does not need deep copied operands
        /// </summary>
        /// <param name="one_col">Left-most column</param>
        /// <param name="two_col">Middle column</param>
        /// <param name="three_col">Right most column</param>
        public pwrMatrix(pwrVector[] one_col, pwrVector[] two_col, pwrVector[] three_col)
        {
            data = new pwrVector[3, 3];
            for(int i = 0; i < 3; i++) {
                data[i, 0] = one_col[i].deepCopy();
            }
            for (int i = 0; i < 3; i++)
            {
                data[i, 1] = two_col[i].deepCopy();
            }
            for (int i = 0; i < 3; i++)
            {
                data[i, 2] = three_col[i].deepCopy();
            }
        }
        /// <summary>
        /// Constructor specifically for creating a 2x2 matrix, likely used for inverse operation
        /// </summary>
        /// <param name="a11">index 0,0</param>
        /// <param name="a12">index 0,1</param>
        /// <param name="a21">index 1,0</param>
        /// <param name="a22">index 1,1</param>
        public pwrMatrix(pwrVector a11, pwrVector a12, pwrVector a21, pwrVector a22)
        {
            data = new pwrVector[2, 2];
            data[0, 0] = a11.deepCopy();
            data[0, 1] = a12.deepCopy();
            data[1, 0] = a21.deepCopy();
            data[1, 1] = a22.deepCopy();
        }
        /// <summary>
        /// Copy matric constructor 
        /// </summary>
        /// <param name="tocopy"></param>
        public pwrMatrix(pwrMatrix tocopy)
        {
            this.data = new pwrVector[tocopy.rows, tocopy.cols];
            for (int i = 0; i < this.rows; i++)
            {
                for(int j=0;j<this.cols; j++)
                {
                    this.data[i, j] = tocopy.data[i, j].deepCopy();
                }
            }
        }

        //Attributes
        public pwrVector[,] data;   //Don't forget importance of deepCopy!
        public int rows //Should always be 3
        {
            get
            {
                return data.GetLength(0);
            }
        }
        public int cols //Should vary between 1 and 3
        {
            get
            {
                return data.GetLength(1);
            }
        }

        //Methods
        /// <summary>
        /// Prints the out the current matrix object's contents one row per line
        /// </summary>
        public void prnMat()
        {
            for(int i=0; i<this.rows; i++)
            {
                for(int j=0; j < this.cols; j++)
                {
                    Console.Write(this.data[i, j].real + "+j" + this.data[i,j].imag);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }
        /// <summary>
        /// Returns the row corresponding to the index "num"
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public pwrVector[] row(int num)
        {
            if (num > this.rows || num < 0)
            {
                return null;
            }

            List<pwrVector> temp = new List<pwrVector>();
              
            for(int i=0; i<this.cols; i++)
            {
                temp.Add(this.data[num, i]);
            }
            return temp.ToArray();
        }
        /// <summary>
        /// Returns column corresponding to index "num"
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public pwrVector[] col(int num)
        {
            if (num > this.cols || num < 0)
            {
                return null;
            }       

            List<pwrVector> temp = new List<pwrVector>();

            for (int i = 0; i < this.rows; i++)
            {
                temp.Add(this.data[i, num]);
            }
            return temp.ToArray();
        }
        /// <summary>
        /// Takes dot product of op1 and op2 storing in current object
        /// </summary>
        /// <param name="op1"></param>
        /// <param name="op2"></param>
        /// <returns></returns>
        private pwrVector dotProd(pwrVector[] op1, pwrVector[] op2) //Returns dot product of two arrays (could be row and col, etc.)
        {
            pwrVector temp = new pwrVector(0, 0);
            pwrVector sum = new pwrVector(0, 0);

            if (op1.Length != op2.Length)
            {
                return null;
            }
            for(int i=0; i<op1.Length; i++)
            {
                temp = new pwrVector(0, 0);
                temp.mult(op1[i], op2[i]);
                sum.add(temp);
            }
            return sum.deepCopy();
        }
        /// <summary>
        /// Adds to matricies stores result in current object
        /// </summary>
        /// <param name="op1"></param>
        /// <param name="op2"></param>
        public void add(pwrMatrix op1, pwrMatrix op2)   //Operation can only be done with 3x3 + 3x3
        {
            //Start off with size checks...
            if((op1.rows != op2.rows) || (op1.cols != op2.cols)){   //Is this logic correct?
                return; //Change nothing
            }
            //Sizes equal at this point
            pwrVector temp = new pwrVector();
            for(int i=0; i < op1.rows; i++)
            {
                for(int j = 0; j < op1.cols; j++)
                {
                    temp.add(op1.data[i, j], op2.data[i, j]);
                    this.data[i, j] = temp.deepCopy();  //Deep copy was needed here due to object references
                }
            }
        }
        /// <summary>
        /// Subtracts two matrices stores result in current object
        /// </summary>
        /// <param name="op1"></param>
        /// <param name="op2"></param>
        public void sub(pwrMatrix op1, pwrMatrix op2)   //Operation can only be done with 3x3 - 3x3
        {
            //Start off with size checks...
            if ((op1.rows != op2.rows) || (op1.cols != op2.cols))
            {   //Is this logic correct?
                return; //Change nothing
            }
            //Sizes equal at this point
            pwrVector temp = new pwrVector();
            for (int i = 0; i < op1.rows; i++)
            {
                for (int j = 0; j < op1.cols; j++)
                {
                    temp.sub(op1.data[i, j], op2.data[i, j]);
                    this.data[i, j] = temp.deepCopy();  //Deep copy was needed here due to object references
                }
            }
        }
        /// <summary>
        /// Multiplies two matricies stores in current object
        /// </summary>
        /// <param name="op1"></param>
        /// <param name="op2"></param>
        public void mult(pwrMatrix op1, pwrMatrix op2)  //Operation working for 3x3 and 3x1, looks like its working as a general function
        {
            //Need to do some checks at the start of the function for correct sizing
            //Additionally, there needs to be something for the [3x3] * [3x1]

            this.data = new pwrVector[op1.rows, op2.cols];
            for (int i=0; i<op1.rows; i++)
            {
                for(int j=0; j<op2.cols; j++)
                {
                    this.data[i, j] = this.dotProd(op1.row(i), op2.col(j));
                }
            }
        }
        /// <summary>
        /// Multiplies current matrix by a scalar value
        /// </summary>
        /// <param name="value"></param>
        public void scalarMult(double value)    //Multiply matrix by scalar
        {
            for(int i=0; i<this.rows; i++)
            {
                for(int j=0; j<this.cols; j++)
                {
                    this.data[i, j].mult(new pwrVector(value, 0));  //Treat a scalar as a vector w/out imaginary component
                }
            }
        }
        /// <summary>
        /// Multiplies current matrix by a scalar vector
        /// </summary>
        /// <param name="val"></param>
        public void vectorScalarMult(pwrVector val)
        {
            for (int i = 0; i < this.rows; i++)
            {
                for (int j = 0; j < this.cols; j++)
                {
                    this.data[i, j].mult(val);  //Treat a scalar as a vector w/out imaginary component
                }
            }
        }
        /// <summary>
        /// Turns current matrix into its complex conjugate form (of pwrVectors)
        /// </summary>
        public void complxConj()    //Complex conj of entire matrix
        {
            for (int i = 0; i < this.rows; i++)
            {
                for (int j = 0; j < this.cols; j++)
                {
                    this.data[i, j].cmpCnj();
                }
            }
        }
        /// <summary>
        /// Inverts current matrix, only if the matrix is 3x3
        /// </summary>
        public void invert() 
        {
            if(this.rows != 3 && this.cols != 3)
            {
                return; //  Only handling 3x3 case for now
            }

            pwrVector detinv = this.det3b3(this);   //  This gets the 3x3 determinant of the current matrix
            if(detinv.real == 0 && detinv.imag ==0) //  A det of zero won't have a unique inverse
            {
                return;
            }
            detinv.div(new pwrVector(1, 0), detinv);    // Returns 1/det(A)
            //  These matrix operations are screaming for a parallel implementation, multithreading, etc.
            //  Create adjugate matrix
            pwrVector zeros = new pwrVector(0, 0);
            pwrVector[] zcol = new pwrVector[3] { zeros, zeros, zeros };
            pwrMatrix adj = new pwrMatrix(zcol, zcol, zcol);
            int topdex = 1;
            int botdex = 2;
            pwrVector sign = new pwrVector(1, 0);   
            for (int i=0; i<3; i++) //Only supporting 3x3 at this point
            {
                topdex = topdex - (i / 1) + 2*(i/2);  //  Using integer division so that at index 1, topdex has one subtracted, at index 2 topdex = topdex -2 + 2 (so net zero)
                botdex = botdex - (i / 2);  //  and at index 2 botdex has one subtracted
              
                pwrMatrix tM = new pwrMatrix(this.data[topdex, 1], this.data[topdex, 2], this.data[botdex, 1], this.data[botdex, 2]);
                pwrVector tV = det2by2(tM);
                tV.mult(sign.deepCopy());
                adj.data[i, 0] = tV.deepCopy();
                sign.mult(new pwrVector(-1, 0));    //  Flip sign

                tM = new pwrMatrix(this.data[topdex, 0], this.data[topdex, 2], this.data[botdex, 0], this.data[botdex, 2]);
                tV = det2by2(tM);
                tV.mult(sign.deepCopy());
                adj.data[i, 1] = tV.deepCopy();
                sign.mult(new pwrVector(-1, 0));    //  Flip sign

                tM = new pwrMatrix(this.data[topdex, 0], this.data[topdex, 1], this.data[botdex, 0], this.data[botdex, 1]);
                tV = det2by2(tM);
                tV.mult(sign.deepCopy());
                adj.data[i, 2] = tV.deepCopy();
                sign.mult(new pwrVector(-1, 0));    //  Flip sign
            }
            pwrMatrix transpose = adj.deepCopy();   //Matrix is then transposed to get the final adjugate matrix
            for(int i =0; i<3; i++)
            {
                for(int j=0; j<3; j++)
                {
                    transpose.data[i, j] = adj.data[j, i].deepCopy();
                }
            }
            pwrMatrix inv = transpose.deepCopy();
            inv.vectorScalarMult(detinv);
            for(int i=0; i<this.rows; i++)
            {
                for(int j=0; j < this.cols; j++)
                {
                    this.data[i, j] = inv.data[i, j];   //No deep copy needed due to local reference that'll be discarded
                }
            }
        }   //  End of method
        /// <summary>
        /// Does element by element division for op1/op2 and stores in current object. Assuming 3x1 operands
        /// </summary>
        /// <param name="op1">A 3x1 matrix</param>
        /// <param name="op2">A 3x1 matrix</param>
        public void eleByEleDiv(pwrMatrix op1, pwrMatrix op2)   //Checks out for implementing into the simulation
        {
            pwrVector tmDiv = new pwrVector();

            for(int i=0; i<3; i++)
            {
                tmDiv.div(op1.data[i, 0], op2.data[i, 0]);
                this.data[i, 0] = tmDiv.deepCopy();
            }
        }
        /// <summary>
        /// Returns a copy of the current object
        /// </summary>
        /// <returns></returns>
        public pwrMatrix deepCopy()
        {
            pwrVector v1 = new pwrVector(0, 0);
            pwrVector[] col = new pwrVector[3] { v1, v1, v1 };  //Assuming 3 rows for all matricies in this context
            pwrMatrix temp = new pwrMatrix();
            if (this.cols == 1)
            {
                temp = new pwrMatrix(col);
            }
            else if(this.cols == 3)
            {
                temp = new pwrMatrix(col,col,col);
            }
            else
            {
                return null;
            }

            for(int i=0; i<this.rows; i++)
            {
                for(int j=0; j < this.cols; j++)
                {
                    temp.data[i, j] = this.data[i, j].deepCopy();
                }

            }
            return temp;    //This should work as temp won't be used anywhere else, but watch out for errors
        }
        //Determinant methods testing well
        public pwrVector det3b3(pwrMatrix opp)  //  Final implementation = private?
        {
            pwrMatrix first = new pwrMatrix(opp.data[1, 1].deepCopy(), opp.data[1, 2].deepCopy(), opp.data[2, 1].deepCopy(), opp.data[2, 2].deepCopy());
            pwrMatrix second = new pwrMatrix(opp.data[1, 0].deepCopy(), opp.data[1, 2].deepCopy(), opp.data[2, 0].deepCopy(), opp.data[2, 2].deepCopy());
            pwrMatrix third = new pwrMatrix(opp.data[1, 0].deepCopy(), opp.data[1, 1].deepCopy(), opp.data[2, 0].deepCopy(), opp.data[2, 1].deepCopy());

            pwrVector firstD = det2by2(first);
            pwrVector secondD = det2by2(second);
            pwrVector thirdD = det2by2(third);

            pwrVector sum = new pwrVector();
            firstD.mult(opp.data[0, 0]);
            secondD.mult(opp.data[0, 1]);
            thirdD.mult(opp.data[0, 2]);

            sum.sub(firstD, secondD);
            sum.add(sum, thirdD);

            return sum; //  No deep copy needed here since the reference was just created locally (and won't be touched again)
        }
        //  Operand must be a 2x2 matrix
        public pwrVector det2by2(pwrMatrix opp) //  Final implementation should be private
        {
            pwrVector one = new pwrVector(0, 0);
            one.mult(opp.data[0, 0], opp.data[1, 1]);
            pwrVector two = new pwrVector(0, 0);
            two.mult(opp.data[0, 1], opp.data[1, 0]);
            one.sub(one.deepCopy(), two);   //Lets clear up this subtracting itself thing, it should be aaaall goooooood
            return one.deepCopy();
        }
        

    }//End of class
}
