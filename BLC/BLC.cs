using Interfaces;
using System.Reflection;

namespace BLC
{
    //Zaimplementować jako singleton
    public class BLC
    {
        private Interfaces.IDaoMock dao;
        public Interfaces.IDaoMock DAO
        {
            get { return dao; }
        }

        public BLC(string liblaryName) {
            Assembly hiddenLibrary = Assembly.UnsafeLoadFrom(liblaryName);
            Type typeToCreate = null;

            foreach (Type type in hiddenLibrary.GetTypes())
            {
                if (type.IsAssignableTo(typeof(Interfaces.IDaoMock)))
                {
                    typeToCreate = type;
                    break;
                }
            }

            dao = Activator.CreateInstance(typeToCreate) as Interfaces.IDaoMock;
        }
    }
}
