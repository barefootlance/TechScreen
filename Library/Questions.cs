using System.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Library
{
    public class Questions
    {
        // 1. ConvertToNumber – Find Flaws and Limitations            
        // Identify the flaws / limitations in the following ConvertToNumber method:
        public static bool ConvertToNumber(string str) // TODO: returns success or failure, but not the value.
        {
            bool canConvert = false;
            try
            {
                int n = Int16.Parse(str); // TODO: throws exception on floats and longer integers

                if (n != 0) // TODO: fails if the number is zero
                {
                    canConvert = true;
                }
            }
            catch (Exception ex) // TODO: ex is never used
            {

            }
            bool retval = false; // TODO: retval is unnecessary duplicate of canConvert
            if (canConvert == true)
            {
                retval = true;
            }
            return retval;
        }

        // 1b: this is a generic implementation that is simpler and handles any numeric object
        public static bool TryConvertToNumber<T>(string str, out T value)
        {
            try
            {
                // conversion based on https://stackoverflow.com/questions/2961656/generic-tryparse
                // for additional discussion see: https://stackoverflow.com/questions/32664/is-there-a-constraint-that-restricts-my-generic-method-to-numeric-types/4834066#4834066
                value = (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(str);
                return true;
            }
            catch
            {
                value = default(T);
                return false;
            }
        }

        // 2. Increment & Decrement – Find The Bugs!            
        // The following code is intended to increment / decrement A and B until A is equal to X and B is equal to Y. Find the bugs!
        public static void MakeTheNumbersMatch(int a, int b, int x, int y)
        {
            while (a != x && b != y) // TODO: will keep looping even if a == x or b == y
            {
                // TODO: so a gets changed even if a == x
                if (a > x)
                {
                    a++; // TODO: a should decrement if it's greater, not increment
                }
                else // TODO: shouldn't do anything if a == x
                {
                    a--;
                }

                // TODO: and the same problems for b and y
                if (b > y)
                {
                    b++;
                }
                else
                {
                    b--;
                }

                // TODO: so even if things increment or decrement correctly, this can still mean an infinite loop, a == x but b != y, b == y but a != x...
            }
        }

        // returns the number of loops
        public static int MakeNumbersMatchBetter(int a, int b, int x, int y) 
        {
            int result = 0;
            while (a != x || b != y) 
            {
                result++;
                a = x switch 
                {
                    _ when a > x => a - 1,
                    _ when a < x => a + 1,
                    _ => a
                };

                b = y switch 
                {
                    _ when b > y => b - 1,
                    _ when b < y => b + 1,
                    _ => b
                };
            }

            return result;
        }

        // 4. Sort Products by Priority    
        // Products are identified by alphanumeric codes. Each code is stored as a string. We have three types of products: high priority, medium priority, and low priority. Given an array of product codes, sort the array so that the highest priority products come at the beginning of the array, the medium priority products come in the middle, and the low priority products come at the end. Within a priority group, order does not matter. You are given a priority function which, given a product code, returns 1 for high, 2 for medium and 3 for low. This array may contain a large number of product codes, so do your best to minimize additional storage.
        // You are given this function for usage:
        // private int GetPriority(string productCode).
        //      You don’t need to implement this function.
        // Please Implement:
        // public void OrderProductsByPriority(string[] productCodes)
        public void OrderProductsByPriority(string[] productCodes) 
        {
            Array.Sort(productCodes, (lhs, rhs) => GetPriority(lhs) - GetPriority(rhs));
        }

        private int GetPriority(string productCode) 
        {
            var high = "aeiou";
            var medium = "0123456789";
            var key = Char.ToLower(productCode[0]);
            return key switch
            {
                _ when high.Contains(key) => 1,
                _ when medium.Contains(key) => 2,
                _ => 3
            };
        }

        // 5     Generate the Lowest Number    
        // You are tasked with implementing a method that returns the lowest possible number that could be generated after removing n characters from a string of digits. The method signature should look like:
        //     public static string GenerateLowestNumber(string number, int n)
        // Where the number parameter is a string that contains a number (e.g. “4205123”), and the n parameter represents the number of characters to remove from the string. The goal of this method is to return the lowest number that can be generated by removing n characters from the number provided while keeping the positions of the remaining characters relative to each other the same (i.e. the method should remove n characters from the string, but it cannot re-order the characters).
        // For example, if number is “4205123” and n is 4, the lowest possible number that can be generated after removing any 4 characters is “012”. If number is “216504” and n is 3, the lowest possible number that can be generated after removing 3 characters is “104”.
        public static string GenerateLowestNumber(string number, int n)
        {
            var head = new Stack<char>();
            var tail = new Queue<char>(number);
            var length = number.Length - n;

            // Each time through the loop we either remove the previous character or add the next one
            while (head.Count + tail.Count > length) 
            {
                // If the previous character is greater than the
                // next character, remove it
                if (head.Count > 0 && head.Peek() > tail.Peek()) 
                {
                    head.Pop();
                }
                else
                {
                    // Add the next character to the beginning of the string.
                    head.Push(tail.Dequeue());

                    // If that was the last character, break out of the loop
                    if (tail.Count == 0) break;
                }
            }

            // Now reconstruct the string
            var charList = head.ToList();
            charList.Reverse();
            charList.AddRange(tail);
            var result = new string(charList.Take(length).ToArray());

            return result;
        }
    }
}
