using System;
using System.Collections.Generic;

namespace wj.AgnosticSql
{
    /// <summary>
    /// Provides helper argument validation functions.
    /// </summary>
    internal static class Guard
    {
        /// <summary>
        /// Throws an exception if the provided argument value is null.
        /// </summary>
        /// <typeparam name="TArg">The type of argument.</typeparam>
        /// <param name="argument">The argument value to test.</param>
        /// <param name="paramName">The corresponding parameter name.</param>
        /// <param name="errorMsg">An optional error message to customize the exception's error 
        /// message.</param>
        /// <exception cref="ArgumentNullException">Thrown if the given argument value is null.</exception>
        public static void IsNotNull<TArg>(TArg argument, string paramName, string errorMsg = null)
        {
            if (argument == null)
            {
                ArgumentNullException ex = string.IsNullOrWhiteSpace(errorMsg) ?
                    new ArgumentNullException(paramName) :
                    new ArgumentNullException(paramName, errorMsg);
                throw ex;
            }
        }

        /// <summary>
        /// Throws an exception if the provided argument value is not equal to the expected value.
        /// </summary>
        /// <typeparam name="TValue">The type of argument value.</typeparam>
        /// <param name="arg">The argument value.</param>
        /// <param name="expectedValue">The expected argument value.</param>
        /// <param name="paramName">The corresponding parameter name.</param>
        /// <param name="errorMsg">An optional error message to customize the exception's error 
        /// message.</param>
        /// <exception cref="ArgumentException">Thrown if the argument value is not equal to the 
        /// expected value.</exception>
        public static void AreEqual<TValue>(TValue arg, TValue expectedValue, string paramName, string errorMsg = null)
        {
            if ((!arg?.Equals(expectedValue) ?? false) || (!expectedValue?.Equals(arg) ?? false))
            {
                throw new ArgumentException(errorMsg ?? "Expected equal values.", paramName);
            }
        }

        /// <summary>
        /// Throws an exception if the provided argument value is equal to the prohibited value.
        /// </summary>
        /// <typeparam name="TValue">The type of argument value.</typeparam>
        /// <param name="arg">The argument value.</param>
        /// <param name="prohibitedValue">The prohibited argument value.</param>
        /// <param name="paramName">The corresponding parameter name.</param>
        /// <param name="errorMsg">An optional error message to customize the exception's error 
        /// message.</param>
        /// <exception cref="ArgumentException">Thrown if the argument value is equal to the 
        /// prohibited value.</exception>
        public static void AreNotEqual<TValue>(TValue arg, TValue prohibitedValue, string paramName, string errorMsg = null)
        {
            if ((arg?.Equals(prohibitedValue) ?? false) || (arg == null && prohibitedValue == null))
            {
                throw new ArgumentException(errorMsg ?? "The argument value is not permitted.", paramName);
            }
        }

        /// <summary>
        /// Throws an exception if the given collections have different counts.
        /// </summary>
        /// <typeparam name="T">Type of element in the first collection.</typeparam>
        /// <typeparam name="U">Type of element in the second collection.</typeparam>
        /// <param name="coll1">First collection.</param>
        /// <param name="coll2">Second collection.</param>
        /// <param name="paramName">The corresponding parameter name.</param>
        /// <param name="errorMsg">An optional error message to customize the exception's error 
        /// message.</param>
        public static void EnsureEqualCounts<T, U>(ICollection<T> coll1, ICollection<U> coll2, string paramName, string errorMsg = null)
            => AreEqual(coll1.Count, coll2.Count, paramName, errorMsg ?? "The collection given as argument does not match the required number of elements.");
    }
}
