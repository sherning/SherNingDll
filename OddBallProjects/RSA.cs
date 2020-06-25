using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OddBallProjects
{
    /// <summary>
    /// Leaning RSA Encryption with encrypt method and decrypt method.
    /// </summary>
    public class RSA
    {
        public static void Main()
        {
            Console.WriteLine(Exponent(2,4));

            //Console.WriteLine(string.Join(" ",GenerateLockAndKey(20)));
            Console.WriteLine(Encrypt(97, 5, 14));
            Console.WriteLine((int)'a');
            Console.WriteLine(Decrypt(4, 11, 14));

            //Console.WriteLine(Decrypt(Encrypt('b',5,14),11,14));
        }

        private static int Encrypt(int c, int encryptKey, int n)
        {
            return (Exponent(c, encryptKey) % n);
        }

        private static int Decrypt(int cryptedMessage, int decryptKey, int n)
        {
            return (Exponent(cryptedMessage, decryptKey) % n);
        }

        private static int[] GenerateLockAndKey(int maxPrime)
        {
            // choose two very large primes primeOne and primeTwo
            List<int> primeOneList = GetPrimeList(maxPrime);
            List<int> primeTwoList = GetPrimeList(maxPrime);

            int primeOne = primeOneList[new Random().Next(0,primeOneList.Count() - 1)];
            int primeTwo = primeTwoList[new Random().Next(0, primeTwoList.Count() - 1)];

            // Calculate n = primeOne * primeTwo
            int n = 0;
            n = primeOne * primeTwo;

            // calculate constant phi or Euler Totient Function
            // x^phi % n = 1
            // phi(n) = (primeOne - 1)(primeTwo - 1)

            int phi = 0;
            phi = (primeOne - 1) * (primeTwo - 1);

            // Find encrypt key
            int encryptKey = 0;
            bool encryptKeyFound = false;

            // coprime with n, phi(n)
            List<int> phiList = GetPrimeList(phi);

            // 1 < e < phi(n)
            while (encryptKeyFound == false)
            {
                int i = new Random().Next(2, phiList.Count() - 1);

                // Coprime with n and phi(n)
                if (phi % phiList[i] != 0 && n % phiList[i] != 0)
                {
                    encryptKey = phiList[i];
                    encryptKeyFound = true;
                }
            }

            // find decrypt key
            int decryptKey = 0;
            bool decryptKeyFound = false;

            // e * d % phi(n) = 1
            while (decryptKeyFound == false)
            {
                int i = new Random().Next(2, 100);
                if ((encryptKey * i) % phi == 1)
                {
                    decryptKey = i;
                    decryptKeyFound = true;
                }
            }

            // the keys, encrypt, N and decrypt
            int[] keys = new int[3];
            keys[0] = encryptKey;
            keys[1] = n;
            keys[2] = decryptKey;

            // return keys
            return keys;
        }

        /// <summary>
        /// Generate the largest possible prime number using the sieve method.
        /// </summary>
        /// <param name="maxPrime"></param>
        /// <returns></returns>
        private static List<int> GetPrimeList(int maxPrime)
        {
            // if input value is less than 2
            if (maxPrime < 2)
            {
                // the largest prime number is 2
                // please enter a number greater than 2.
                Console.WriteLine("please enter a number greater than 2");
                return null;
            }

            // else
            // Build an array, using the index to keep track of the composites / non-prime numbers.
            int[] composites = new int[maxPrime];

            // Use a list to keep track of the primes
            List<int> primes = new List<int>();

            // the numerical value 0 and 1 are not prime numbers
            composites[0] = 1;
            composites[1] = 1;

            // for other numbers, the maximum runs to determine prime numbers is square root of max prime.
            for (int i = 2; i <= Math.Sqrt(maxPrime); i++)
            {
                // if composite has not been marked as a prime
                if (composites[i] != 1)
                {
                    // mark all multiples of the non prime as 1
                    for (int j = i*i; j < maxPrime; j = j + i)
                    {
                        composites[j] = 1;
                    }
                }
            }

            // Get the prime numbers, filling in the blanks after the sieve above.
            for (int i = 0; i < maxPrime; i++)
            {
                if (composites[i] != 1)
                {
                    primes.Add(i);
                }
            }

            // Return the largest prime.
            return primes;
        }
       
        /// <summary>
        /// Exponent Function: Returns the value of baseCase ^ power.
        /// </summary>
        /// <param name="baseCase"></param>
        /// <param name="power"></param>
        /// <returns></returns>
        private static int Exponent(int baseCase, int power)
        {
            // Define base case.
            if (power == 0)
            {
                return 1;
            }

            // start recursion. Power needs to be pre-fix, post-fix will 
            // generate a stack overflow exception.
            return baseCase * Exponent(baseCase, --power);
        }
    }
}
