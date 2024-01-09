using GoogleKeep.Domain.SeedWork;

namespace GoogleKeep.Tests.SeedOfWork
{
    public class EntityIdTests
    {
        [Fact]
        public void EqualityTests()
        {
            var guid = Guid.NewGuid();

            Assert.Equal(new EntityId(guid), new EntityId(guid));
            Assert.NotEqual(new EntityId(guid), new EntityId(Guid.NewGuid()));
        }

        [Fact]
        public void DifferentTypesTests()
        {
            var guid = Guid.NewGuid();
            var entityId = new EntityId(guid);

            Assert.NotEqual(entityId, (object)guid);
        }

        [Fact]
        public void EqualityOperatorsTests()
        {
            var guid = Guid.NewGuid();

            var same1 = new EntityId(guid);
            var same2 = new EntityId(guid);

            var different = new EntityId(Guid.NewGuid());

            Assert.True(same1 == same2);
            Assert.True(same1 != different);
            Assert.False(same1 == different);
            Assert.False(same1 != same2);
        }
    }
}
