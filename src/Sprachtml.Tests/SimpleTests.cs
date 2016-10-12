﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Shouldly;
using Sprache;
using Sprachtml.Models;

namespace Sprachtml.Tests
{
    [TestFixture]
    public class SimpleTests
    {
        [Test]
        public void GivenDivMarkup_Should_ParseToDiv()
        {
            var html = "<div></div>";

            var parsed = Sprachtml.Parse(html);

            parsed.Length.ShouldBe(1);

            parsed[0].NodeType.ShouldBe(HtmlNodeType.Div);
            parsed[0].Children.ShouldBeEmpty();
        }


        [Test]
        public void GivenSpanInsideDivMarkup_Should_ParseToDivWithChildOfSpan()
        {
            var html = "<div><span></span></div>";

            var parsed = Sprachtml.Parse(html);

            parsed.Length.ShouldBe(1);

            parsed[0].NodeType.ShouldBe(HtmlNodeType.Div);
            parsed[0].Children.Length.ShouldBe(1);

            parsed[0].Children[0].NodeType.ShouldBe(HtmlNodeType.Span);
            parsed[0].Children[0].Children.ShouldBeEmpty();
        }

        [Test]
        public void GivenDivWithIdMarkup_Should_ParseToDivWithIdAttribute()
        {
            var html = "<div id=\"TestId\"></div>";

            var parsed = Sprachtml.Parse(html);

            parsed.Length.ShouldBe(1);

            var div = parsed.Single();

            div.NodeType.ShouldBe(HtmlNodeType.Div);
            div.Attributes.Length.ShouldBe(1);

            div.Attributes[0].Name.ShouldBe("id");
            div.Attributes[0].Value.ShouldBe("TestId");
            div.Attributes[0].Binary.ShouldBeFalse("Id should not be a binary attribute");
        }

        [Test]
        public void BinaryAttribute_Should_ParseAndBeMarkedAsBinary()
        {
            var html = "<textarea readonly></textarea>";

            var parsed = Sprachtml.Parse(html);

            parsed.Length.ShouldBe(1);
            parsed[0].NodeType.ShouldBe(HtmlNodeType.Textarea);
            parsed[0].Attributes.Length.ShouldBe(1);
            parsed[0].Attributes[0].Name.ShouldBe("readonly");
            parsed[0].Attributes[0].Binary.ShouldBeTrue();
        }
        [Test]
        public void SelfClosingTag_Should_ParseAndHaveNoChildren()
        {
            var html = "<br />";

            var parsed = Sprachtml.Parse(html);

            parsed.Length.ShouldBe(1);
            parsed[0].Children.ShouldBeEmpty();
            parsed[0].NodeType.ShouldBe(HtmlNodeType.Br);
        }
        [Test]
        public void Text_Should_ParseAsTextNode()
        {
            var html = "I am text with no html";

            var parsed = Sprachtml.Parse(html);

            parsed.Length.ShouldBe(1);
            parsed[0].Children.ShouldBeEmpty();
            parsed[0].NodeType.ShouldBe(HtmlNodeType.Text);
            ((TextNode) parsed[0]).Contents.ShouldBe("I am text with no html");

        }
        [Test]
        public void TextWithHtml_Should_ParseAsHtmlAndTextNodes()
        {
            var html = "<p>This is a paragraph with <strong>strong</strong> content</p>";

            var parsed = Sprachtml.Parse(html);

            parsed.Length.ShouldBe(1);


            parsed.Single().NodeType.ShouldBe(HtmlNodeType.P);

            parsed.Single().Children.Length.ShouldBe(3);

            var children = parsed.Single().Children;

            children[0].NodeType.ShouldBe(HtmlNodeType.Text);
            children[1].NodeType.ShouldBe(HtmlNodeType.Strong);
            children[2].NodeType.ShouldBe(HtmlNodeType.Text);


        }

       /*
        * TODO
        * Comments
        * Attributes without quotes
        * Doc types
        * Script tags
        * Style tags
        * 
        * 
        * Different cases?
        * 
        */
    }
}
