using DAL.EF;

namespace DAL.Repos
{
    internal class Repos
    {
        internal DBContext db;

        internal Repos()
        {
            db = new DBContext(); // This works now because DBContext has a parameterless constructor
        }
    }
}
