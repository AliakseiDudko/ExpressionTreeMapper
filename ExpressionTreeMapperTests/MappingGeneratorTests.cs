using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExpressionTreeMapper;

namespace ExpressionTreeMapperTest
{
    public class Foo
    {
        public string Value1 { get; set; }
        public string Value2 { get; set; }
        public int Value3 { get; set; }
    }

    public class Bar
    {
        public string Value1 { get; set; }
        public string Value2 { get; set; }
        public int Value3 { get; set; }
    }

    [TestClass]
    public class MappingGeneratorTests
    {
        [TestMethod]
        public void MapPropertiesTest()
        {
            var mapGenerator = new MappingGenerator();
            var mapper = mapGenerator.Generate<Foo, Bar>();

            var foo = new Foo { Value1 = "Foo1", Value2 = "Foo2" };
            var bar = mapper.Map(foo);

            Assert.AreEqual(foo.Value1, bar.Value1);
            Assert.AreEqual(foo.Value2, bar.Value2);
            Assert.AreEqual(foo.Value3, bar.Value3);
        }
    }
}
