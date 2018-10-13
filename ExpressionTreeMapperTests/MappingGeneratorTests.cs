using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExpressionTreeMapper;

namespace ExpressionTreeMapperTest
{
    public class Foo { }
    public class Bar { }

    [TestClass]
    public class MappingGeneratorTests
    {
        [TestMethod]
        public void TestMethod3()
        {
            var mapGenerator = new MappingGenerator();
            var mapper = mapGenerator.Generate<Foo, Bar>();

            var res = mapper.Map(new Foo());
        }
    }
}
