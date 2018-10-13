using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExpressionTreeMapper;

namespace ExpressionTreeMapperTest
{
    public class Foo
    {
        public string ValueFoo1 { get; set; }
    }

    public class Bar
    {
        public string ValueBar1 { get; set; }
    }

    [TestClass]
    public class MappingGeneratorTests
    {
        [TestMethod]
        public void MapPropertiesTest()
        {
            var mapGenerator = new MappingGenerator();
            var mapper = mapGenerator.Generate<Foo, Bar>();

            var foo = new Foo { ValueFoo1 = "Foo1" };
            var bar = mapper.Map(new Foo());

            Assert.AreEqual(foo.ValueFoo1, bar.ValueBar1);
        }
    }
}
