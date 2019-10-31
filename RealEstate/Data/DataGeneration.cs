namespace RealEstate.Data
{
    using RealEstate.Models;

    public abstract class DataGeneration
    {
        protected readonly _Context _context;

        public DataGeneration(_Context context)
        {
            _context = context;
        }

        public abstract void Generate();
    }
}