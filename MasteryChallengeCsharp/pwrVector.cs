using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MatOps
{
    class pwrVector //TODO: Implement input checking
    {
        ///<summary>
        ///Only constructor sets real and imaginary components, use polar method after if a vector needs to be initialized as mag and angle
        ///</summary>
        public pwrVector(double re = 0.0, double im = 0.0) 
        {
            this._real = re;
            this._imag = im;
            this._mag = Math.Sqrt((Math.Pow(_real, 2)) + (Math.Pow(_imag, 2)));
            this._ang = Math.Atan2(_imag, _real);

        }
        //Attributes
        private double _real;
        private double _imag;
        private double _mag;
        private double _ang;

        public double real {
            get
            {
                return _real;
            }
            set
            {
                _real = value;
                _mag = Math.Sqrt((Math.Pow(_real, 2)) + (Math.Pow(_imag, 2)));
                _ang = Math.Atan2(_imag, _real);
            }
        }
        public double imag
        {
            get
            {
                return _imag;
            }
            set
            {
                _imag = value;
                _mag = Math.Sqrt((Math.Pow(_real, 2)) + (Math.Pow(_imag, 2)));
                _ang = Math.Atan2(_imag, _real);
            }
        }   
        public double mag
        {
            get
            {
                return _mag;
            }
            set
            {
                _mag = value;
                _real = _mag * Math.Cos(_ang);
                _imag = _mag * Math.Sin(_ang);
            }
        }
        public double angle
        {
            get
            {
                return _ang;
            }
            set
            {
                _ang = value;
                _real = _mag * Math.Cos(_ang);
                _imag = _mag * Math.Sin(_ang);
            }
        }

        //Methods
        ///<summary>Changes polar coordinates, magnitude and angle</summary>
        public void polar(double mg=0, double ag = 0) 
        {
            mag = mg;
            angle = ag;
        }
        ///<summary>Changes current object to complex conjugate</summary>
        public void cmpCnj()        
        {
            this.imag = -this.imag;
        }
        ///<summary>Adds opp to the current object</summary>
        public void add(pwrVector opp)
        {
            if (opp == null)
                return;
            this.real = this.real + opp.real;
            this.imag = this.imag + opp.imag;
        }
        ///<summary>Adds two vecs and stores them in current object</summary>
        public void add(pwrVector one, pwrVector two)
        {
            if (one == null || two == null)
                return;
            this.real = one.real + two.real;
            this.imag = one.imag + two.imag;
        }
        ///<summary>Subtracts opp from the current object</summary>
        public void sub(pwrVector opp)
        {
            if (opp == null)
                return;
            this.real = this.real - opp.real;
            this.imag = this.imag - opp.imag;
        }
        ///<summary>Subtracts two from one and stores them in current object</summary>
        public void sub(pwrVector one, pwrVector two)
        {
            if (one == null || two == null)
                return;
            this.real = one.real - two.real;
            this.imag = one.imag - two.imag;
        }
        ///<summary>Multiplies opp to the current object</summary>
        public void mult(pwrVector opp) 
        {
            if (opp == null)    
                return; //If operand is invalid the method returns without doing anything
            this.mag = this.mag * opp.mag;
            this.angle = this.angle + opp.angle;  
        }
        ///<summary>Multiplies one and two and stores it in current object</summary>
        public void mult(pwrVector one, pwrVector two)
        {
            if (one == null || two == null)
                return;
            this.mag = one.mag * two.mag;
            this.angle = one.angle + two.angle;
        }
        ///<summary>Divides current object by opp</summary>
        public void div(pwrVector opp) 
        {
            if (opp == null)
                return; //If operand is invalid the method returns without doing anything
            this.mag = this.mag / opp.mag;
            this.angle = this.angle - opp.angle;
        }
        ///<summary>Divides one by two and stores in current object</summary>
        public void div(pwrVector one, pwrVector two)
        {
            if (one == null || two == null)
                return;
            this.mag = one.mag / two.mag;
            this.angle = one.angle - two.angle;
        }
        ///<summary>Copying function returns pwrVector</summary>
        public pwrVector deepCopy()
        {
            return new MatOps.pwrVector(this.real, this.imag);
        }

    }//End of class

    public static class radExtension
    {
        /// <summary>
        /// Returns radians equal to num degrees
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static double DegToRad(this double num)
        {
            return num * (Math.PI / 180);
        }
        /// <summary>
        /// Returns degrees equal to num radians
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static double RadToDeg(this double num)
        {
            return num * (180 / Math.PI);
        }

    }
}
