using Microsoft.EntityFrameworkCore;

namespace BethanyPieShop.Models
{
    public class PieRepository : IPieRepository
    {
        private readonly BethanyPieShopDbContext _bethanyPieShopDbContext;

        public PieRepository(BethanyPieShopDbContext bethanyPieShopDbContext)
        {
            this._bethanyPieShopDbContext = bethanyPieShopDbContext;
        }

        public IEnumerable<Pie> AllPies
        {
            get
            {
                return _bethanyPieShopDbContext.Pies.Include(c => c.Category);
            }
        } 

        public IEnumerable<Pie> PiesOfTheWeek
        {
            get
            {
                return _bethanyPieShopDbContext.Pies.Include(c => c.Category).Where(p => p.IsPieOfTheWeek);
            }
        } 

        public Pie? GetPieById(int pieid)
        {
            return _bethanyPieShopDbContext.Pies.FirstOrDefault(p => p.PieId == pieid);
        }
    }
}
