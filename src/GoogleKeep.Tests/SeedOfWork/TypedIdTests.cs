using GoogleKeep.Domain.SeedWork;
using Xunit;

namespace GoogleKeep.Tests.SeedOfWork
{
    public class TypedIdTests
    {
        [Fact]
        public void EqualityTests()
        {
            var guid = Guid.NewGuid();

            Assert.Equal(new TypedId(guid), new TypedId(guid));
            Assert.NotEqual(new TypedId(guid), new TypedId(Guid.NewGuid()));
        }

        [Fact]
        public void DifferentTypesTests()
        {
            var guid = Guid.NewGuid();
            var entityId = new TypedId(guid);

            Assert.NotEqual(entityId, (object)guid);
        }

        [Fact]
        public void EqualityOperatorsTests()
        {
            var guid = Guid.NewGuid();

            var same1 = new TypedId(guid);
            var same2 = new TypedId(guid);

            var different = new TypedId(Guid.NewGuid());

            Assert.True(same1 == same2);
            Assert.True(same1 != different);
            Assert.False(same1 == different);
            Assert.False(same1 != same2);
        }
    }
}
