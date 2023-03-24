using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using paybyrd.AutoMapper;
using paybyrd.Context;
using paybyrd.Entities.Response;
using paybyrd.Interfaces.Repository;
using paybyrd.Interfaces.Services;
using paybyrd.Services;
using System;
using System.Threading.Tasks;
using Xunit;

namespace PayByrdTest
{
    public class DiffTest 
    {
        private ServiceProvider services;
        public DiffTest()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddScoped<IDiffService, DiffService>();
            serviceCollection.AddScoped<IDiffRepository, DiffRepository>();
            serviceCollection.AddAutoMapper(typeof(ConfigurationMapping));
            serviceCollection.AddDbContext<PayByrdContext>(opt =>
              opt.UseInMemoryDatabase(new Guid().ToString()));
            this.services = serviceCollection.BuildServiceProvider();
        }
        [Fact]
        public void GetDiff_Differences_NotException()
        {

            var _work1 = new Mock<IDiffService>();
            _work1.Setup(x => x.SaveLeft(new paybyrd.Entities.Request.DiffRequest() { Id = 1, JsonValue = "VEVTVEUgREUgUEFMQVZSQQ==" }));
            _work1.Setup(x => x.SaveRight(new paybyrd.Entities.Request.DiffRequest() {Id = 1, JsonValue = "VEVTVEUgREUgUEFMQVZSQQ==" } ));
            _work1.Setup(x => x.GetDiff(1, true));
        }

        [Fact]
        public void GetDiff_Differences_Equals_IgnoreUpperCaseLowerCase()
        {
            var _diffService = this.services.GetService<IDiffService>();
            _diffService.SaveLeft(new paybyrd.Entities.Request.DiffRequest() { Id = 1, JsonValue =  "VEVTVEUgREUgUEFMQVZSQQ==" });
            _diffService.SaveRight(new paybyrd.Entities.Request.DiffRequest() { Id = 1, JsonValue = "VEVTVEUgREUgUEFMQVZSQQ==" });
            var result = _diffService.GetDiff(1, true);
            Assert.True(result.Equals);
            Assert.Empty(result.Differences);
        }
        [Fact]

        public void GetDiff_Differences_WithDifferences_IgnoreUpperCaseLowerCase()
        {
            var _diffService = this.services.GetService<IDiffService>();
            _diffService.SaveLeft(new paybyrd.Entities.Request.DiffRequest() { Id = 1, JsonValue = "VEVbrEUgREUgUEFMQVZSQQ==" });
            _diffService.SaveRight(new paybyrd.Entities.Request.DiffRequest() { Id = 1, JsonValue = "VEVTVEUgREUgUEFMQVZSQQ==" });
            var result = _diffService.GetDiff(1, true);
            

            Assert.False(result.Equals);
            Assert.NotEmpty(result.Differences);
        }


          [Fact]
        public void GetDiff_Differences_Equals_NotIgnoreUpperCaseLowerCase()
        {
            var _diffService = this.services.GetService<IDiffService>();
            _diffService.SaveLeft(new paybyrd.Entities.Request.DiffRequest() { Id = 1, JsonValue =  "VEVTVEUgREUgUEFMQVZSQQ==" });
            _diffService.SaveRight(new paybyrd.Entities.Request.DiffRequest() { Id = 1, JsonValue = "VEVTVEUgREUgUEFMQVZSQQ==" });
            var result = _diffService.GetDiff(1, false);
            Assert.True(result.Equals);
            Assert.Empty(result.Differences);
        }
        [Fact]
        public void GetDiff_Differences_WithDifferences_NotIgnoreUpperCaseLowerCase()
        {
            var _diffService = this.services.GetService<IDiffService>();
            _diffService.SaveLeft(new paybyrd.Entities.Request.DiffRequest() { Id = 1, JsonValue =  "VEVvVEUgREUgUEFMQVZSQQ==" });
            _diffService.SaveRight(new paybyrd.Entities.Request.DiffRequest() { Id = 1, JsonValue = "VEVTVEUgREUgUEFMQVZSQQ==" });
            var result = _diffService.GetDiff(1, false);

            Assert.False(result.Equals);
            Assert.NotEmpty(result.Differences);
        }

        [Fact]
        public void GetDiff_Differences_DiferenteSize()
        {
            var _diffService = this.services.GetService<IDiffService>();
            _diffService.SaveLeft(new paybyrd.Entities.Request.DiffRequest() { Id = 1, JsonValue = "VEVvVEdsFMQVZSQQ==" });
            _diffService.SaveRight(new paybyrd.Entities.Request.DiffRequest() { Id = 1, JsonValue = "VEVTVEUgREUgUEFMQVZSQQ==" });
            var result = _diffService.GetDiff(1, false);
            Assert.False(result.Equals);
            Assert.Same("Different size", result.Result);
        }


        [Fact]
        public void GetDiff_Differences_AllEmptyDiff()
        {
            var _diffService = this.services.GetService<IDiffService>();
            Exception expectedException = null;
            try
            {
                _diffService.GetDiff(1, false);
            }
            catch (Exception e)
            {
                expectedException = e;
            }
            Assert.NotNull(expectedException);
        }

        [Fact]
     
        public void GetDiff_Differences_OneEmptyDiff()
        {
            var _diffService = this.services.GetService<IDiffService>();
            _diffService.SaveRight(new paybyrd.Entities.Request.DiffRequest() { Id = 2, JsonValue = "VEVTVEUgREUgUEFMQVZSQQ==" });

            Exception expectedException = null;
            try
            {
                _diffService.GetDiff(2, false);
            }
            catch (Exception e)
            {
                expectedException = e;
            }
            Assert.NotNull(expectedException);
        }
        [Fact]
        public void SaveRight_Save_Ok()
        {
            var _diffService = this.services.GetService<IDiffService>();
            _diffService.SaveRight(new paybyrd.Entities.Request.DiffRequest() { Id = 1, JsonValue = "VEVTVEUgREUgUEFMQVZSQQ==" });
        }

        [Fact]
        public void SaveLeft_Save_Ok()
        {
            var _diffService = this.services.GetService<IDiffService>();
            _diffService.SaveRight(new paybyrd.Entities.Request.DiffRequest() { Id = 1, JsonValue = "VEVTVEUgREUgUEFMQVZSQQ==" });
        }
        [Fact]
        public void SaveLeft_Save_Id0()
        {
            var _diffService = this.services.GetService<IDiffService>();
            Exception expectedException = null;
            try
            {
                
                _diffService.SaveRight(new paybyrd.Entities.Request.DiffRequest() { Id = 0, JsonValue = "VEVTVEUgREUgUEFMQVZSQQ==" });
            }
            catch (Exception e)
            {
                expectedException = e;
            }
            Assert.NotNull(expectedException);
        }
        [Fact]
        public void SaveLeft_Save_JsonValurEmpty()
        {
            var _diffService = this.services.GetService<IDiffService>();
            Exception expectedException = null;
            try
            {

                _diffService.SaveRight(new paybyrd.Entities.Request.DiffRequest() { Id = 0, JsonValue = "" });
            }
            catch (Exception e)
            {
                expectedException = e;
            }
            Assert.NotNull(expectedException);
        }

    }
}
