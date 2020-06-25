using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OddBallProjects
{
    class AbstractClassInheritance
    {
        private void Test()
        {
            SqlConnect sql = new SqlConnect();
            sql.Path = "WTF";
        }
        
    }

    class KeyConfig
    {
        public string Path { get; set; }
    }
    class SqlConnect : DataAcess
    {
        public override string DataConnection(string path)
        {
            return string.Empty;
        }
        public sealed override void Load(string path)
        {
            throw new NotImplementedException();
        }

        public sealed override void Save(string path)
        {
            throw new NotImplementedException();
        }
    }

    abstract class DataAcess : KeyConfig, IDataAccess
    {
        public virtual string DataConnection(string path)
        {
            return Path;
        }
        public abstract void Load(string path);
        public abstract void Save(string path);
    }

    interface IDataAccess
    {
        string DataConnection(string path);
        void Load(string path);
        void Save(string path);
    }

    abstract class AbstractClassA
    {
        private int NumA;
        public AbstractClassA(int numA)
        {
            NumA = numA;
        }
    }

    abstract class AbstractClassB : AbstractClassA
    {
        private int NumB;
        public AbstractClassB(int numA, int numB) : base(numA)
        {

        }
    }

    sealed class Test : AbstractClassB
    {
        public Test(int numA, int numB) : base(numA, numB)
        {
        }
    }


}
