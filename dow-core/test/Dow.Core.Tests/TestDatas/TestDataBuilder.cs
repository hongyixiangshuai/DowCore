using Dow.Core.EntityFrameworkCore;

namespace Dow.Core.Tests.TestDatas
{
    public class TestDataBuilder
    {
        private readonly CoreDbContext _context;

        public TestDataBuilder(CoreDbContext context)
        {
            _context = context;
        }

        public void Build()
        {
            //create test data here...
        }
    }
}