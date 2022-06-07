using APIDiff.DataAccess.Models;
using APIDiff.Extensions;
using System.Collections.Generic;
using Xunit;

namespace APIDiff.Tests
{
    public class TestsForFindEntityMethod
    {
        [Fact]
        public void TestFindEntityMethodWhenIdExists()
        {
            List<Entity> items = new List<Entity>{
                new Left
                {
                    Id = 1,
                    Data = "AAA="
                },
                new Left
                {
                    Id = 2,
                    Data = "AAAAAA=="
                }
            };

            var result = items.FindEntity(2);

            Assert.NotNull(result);
            Assert.Equal(2, result.Id);
            Assert.Equal("AAAAAA==", result.Data);
        }

        [Fact]
        public void TestFindEntityMethodWhenIdNotExists()
        {
            List<Entity> items = new List<Entity>{
                new Left
                {
                    Id = 2,
                    Data = "AAAAAA=="
                }
            };

            var result = items.FindEntity(1);

            Assert.Null(result);
        }

        [Fact]
        public void TestFindEntityMethodWhenEntityListIsEmpty()
        {
            List<Entity> items = new List<Entity>();

            var result = items.FindEntity(1);

            Assert.Null(result);
        }

        [Fact]
        public void TestFindEntityMethodWhenThereAreMoreSameIds()
        {
            List<Entity> items = new List<Entity>{
                new Left
                {
                    Id = 1,
                    Data = "AAA="
                },
                new Left
                {
                    Id = 1,
                    Data = "AAAAAA=="
                }
            };

            var result = items.FindEntity(1);

            Assert.NotNull(result);
            Assert.NotEqual("AAAAAA==", result.Data);
        }
    }
}
