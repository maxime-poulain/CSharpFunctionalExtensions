using Castle.DynamicProxy;
using FluentAssertions;
using Xunit;

namespace CSharpFunctionalExtensions.Tests.Extensions;

public class TypeExtensionsTests
{
    public class SomeEntity : Entity
    {
        protected SomeEntity() {}
        public SomeEntity(int id) : base(id){}
    }

    [Fact]
    public void Unproxying_the_type_of_a_proxied_object_should_return_its_actual_type()
    {
        var someEntity = new SomeEntity(1);
        var proxiedEntity = new ProxyGenerator().CreateClassProxy(someEntity.GetType());

        var unproxiedType = proxiedEntity.GetType().Unproxy();

        unproxiedType.Should().Be(typeof(SomeEntity));
    }
}
