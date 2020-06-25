using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    class MultiChartDesignPattern : CStudyControlFactory
    {
        // To understand MC.Net architecture
        // https://www.youtube.com/watch?v=n4mCbaqH8zs&t=566s
        // interfaces
        // factor design pattern

        // What are interfaces for ?
        // Enforce rules, contract, abstraction, decoupling, version, inheritance, category
        // Standardization, but the basic use of an interface is contract.

        // Contract - legal binding contract between two parties.
        // Implicit contract between a caller and callee.

        // Impact analysis - where will it affect.
        // Caller is not aware of callee, then you need interface. client and server.

        public static void Main()
        {
            // this is me here using multichart.
            // I have certain expectations when using PowerLanguage.
            // The contract between me and MC developer.
            IArrowObject arrow = DrwArrow.Create();
            bool upOrDown = arrow.Direction;

            IDrawObject arrow2 = DrwArrow.Create();
            int id = arrow2.ID;
        }
    }

    //--------------------------------------- Different Framework ---------------------------------------//
    class CStudyControlFactory
    {
        public static IArrowObject DrwArrow { get; }
    }
    //--------------------------------------- Different Framework ---------------------------------------//

    class Arrow : IArrowObject
    {
        public bool Direction { get; }

        public int ID { get; }

        public Arrow Create()
        {
            return new Arrow();
        }
    }
    interface IDrawObject
    {
        int ID { get; }
    }
    interface IArrowObject : IDrawObject
    {
        bool Direction { get; }
        Arrow Create();
    }

}
