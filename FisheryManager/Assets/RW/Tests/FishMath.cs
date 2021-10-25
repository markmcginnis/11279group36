using System;
namespace MathModel
{
    public class FishMath
    {
        /*
        assume assumption that maxPopulation is 100 "fish"
        assume rate of decomposition of 5 kg/m^3/week
        assume constant time step of 7 days
        assume "maximum" fecal matter concetration of 10 kg/m^3
        */

        public int testAdd(int a, int b)
        {
            return a + b;
        }
        /*
        structure for unit testing
        1. Check valid probability (0 < x < 1) on lowest input value
        2. Check valid probaibility (0 < x < 1) on highest input value
        3. Check valid probability at median value
        4. Check that probability is nonlinear f(0.5) - f(0.25) != f(0.75) = f(0.5)
        */
        public int EulerLogistic(int currentPop, int carryingCap, double n, double h)
        {
            // this method uses Euler's Method and a logistic ODE
            if(currentPop == 0)
            {
                return currentPop;
            }
            double result = currentPop;
            for (double i = 0; i <= n; i += h)
            {
                result += h * 0.5 * result * (1.0 - (result / carryingCap));
            }
            int finalResult = (int)Math.Floor(result);
            return finalResult;
        }

        public double disease(int population, double maxConc, double fecalMatterConc, double antibiotics, double antibioticsMax)
        {
            // this method uses an exponential distribution with lambda = 1
            double exp = -1 * (maxConc - fecalMatterConc) / 2;
            double probability = Math.Exp(exp);
            double antibioticsMultiplier = Math.Log(2.5 - antibiotics / antibioticsMax);
            // this method returns the probability of a fish catching the disease, puts a cap at 0.75
            double result = antibioticsMultiplier * probability > 0.75 ? antibioticsMultiplier * probability : 0.75;
            return result;
        }

        public double recovery(int population, double maxConc, double fecalMatterConc, double antibiotics, double antibioticsMax)
        {
            double probability = Math.Exp(-1 * (fecalMatterConc / maxConc));
            probability /= 4;
            return probability;
        }

        public int harvest(int population, int harvest)
        {
            return population -= harvest;
        }

        public double getFecalConcentration(int population, double fecalMatterConc, double decomposition)
        {
            double result = fecalMatterConc + 0.1 * population;
            result -= decomposition; // assume a decomposition of 5
            result = result < 0 ? 0 : result;
            return result;
        }

    }
}

