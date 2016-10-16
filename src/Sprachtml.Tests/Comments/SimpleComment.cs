using NUnit.Framework;
using Shouldly;
using Sprachtml.Models;

namespace Sprachtml.Tests.Comments
{
    public class SimpleComment: ParsingTestBase
    {
        [Test]
        public void ShouldParseAsHtmlComment()
        {
            var parsed = GetNodes();

            parsed.Length.ShouldBe(1);

            parsed[0].NodeType.ShouldBe(HtmlNodeType.Comment);
        }

        protected override string Markup => "<!-- test -->";
    }
}