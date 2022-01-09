using AutoMapper;
using Blog.Business.AutoMapper;
using Blog.Repository.EntityFramework.Abstract.UnitOfWork;
using Blog.Repository.EntityFramework.Concrete.UnitOfWork;
using Blog.Repository.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace Blog.ApiTest.TestSetup
{
    public class CommonTestFixture
    {
        public IUnitOfWork UnitOfWork { get; set; }
        public BlogDbContext Context { get; set; }
        public IMapper Mapper { get; set; }

        public CommonTestFixture()
        {
            var options = new DbContextOptionsBuilder<BlogDbContext>().UseInMemoryDatabase(databaseName: "BlogTestDb").Options;
            Context = new BlogDbContext(options);
            UnitOfWork = new UnitOfWork(Context);
            Context.Database.EnsureCreated();

            Mapper = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfiles>(); }).CreateMapper();
        }
    }
}
