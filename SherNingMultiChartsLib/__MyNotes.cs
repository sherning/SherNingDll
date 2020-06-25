//==============================================================================
// Name           : MyNotes
// Description    : Good practices for Multichart.Net programming
// Version        : v.1.0
// Date Created   : 03 - June - 2020
// Time Taken     : 
// Remarks        :
//==============================================================================
// Copyright      : 2020, Sher Ning Technologies           
// License        :      
//==============================================================================

/* ------------------------------- Version History -------------------------------
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SherNingMultiChartsLib
{
    class MyNotes
    {
        #region MC.Net Namespace
        /* With regards to PowerLanguage Namespace.
         * We can use PowerLanguage namespace itself without the postfix.
         * We cannot create our own namespace.
         * We can only use 1 of 3 powerlanugage namespaces. postfix function, strategy, indicator.
         * or the global powerlanguage namespace.
         */
         
        #endregion
        #region Notes on Programming classes for MC.NET
        /* My Notes on Programming classes for Multicharts .Net 
         * 
         * Classes are instaniated in Create() once.
         * Subsequently, class methods are called once before calculating the first bar in StartCalc()
         * Thereafter, classes are called once every tick/bar from the first bar to the current(last) bar
         * in the Calc() method.
         * 
         * When classes are alled in the Create(), you can use the constructor to initialize class members.
         * When you want to update class properties, it is best call a method to do so. Unlike MC Functions,
         * updating properties will not automatically call calculation methods.
         * 
         * Fields and member properties will retain their current state whenever MultiChart Refreshes.
         * Therefore, you will need to have a clear method to reset class members to default values.
         * There is two approaches to this problem, either instantiate the class in StartCalc(),
         * or have a Clear() to reset to default.
         * 
         * It's good practice to use fewer class members and more local variables, as local variables do not
         * retain their state, and local variable uses the stack, which is more efficient.
         * 
         * Data for calculation, should be added on a bar by bar, tick by tick basis using an AddData() in Calc()
         * Use the constructor to initialize all class members that will cause exception errors.
         * 
         * If you have Len1 Len2 Len3 use a int[] Lengths to store, avoid using [0] for readability
         * 
         * The main advantage of building regular classes is that you are able to test using breakpoints 
         * to check the state of each processing method.
         * 
         * Create a new MC Indicator, using PowerLanguage name space to put my custom classes there.
         */

        #endregion

        #region Notes on Good Programming Practices for MC.Net
        /* A good practice for Programming is the use of versions.
         * Version 1.0   ->   v10
         * 
         * The first digit is used when there is a change in class functions
         * or, there are added features.
         * 
         * The Second digit is used when there is no change in functionality,
         * but there are improvements through factorization or optimization.
         * 
         * Final Version will have no version number behind.
         * It is good to keep track of what each version does, so you can refer or roll back
         * if the previous version does better than the current.
         */
        #endregion
    }
}
