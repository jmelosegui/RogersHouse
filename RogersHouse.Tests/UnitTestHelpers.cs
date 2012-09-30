using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RogerHouse.Domain.Abstract;
using RogerHouse.Domain.Entities;

namespace RogersHouse.UnitTests
{
    public static class UnitTestHelpers
    {
        public static void ShouldEqual<T>(this T actualValue, T expectedValue)
        {
            Assert.AreEqual(expectedValue, actualValue);
        }

        public static IPagesRepository MockPagesRepository(params Page[] pages)
        {
            // Generate an implementer of IProductsRepository at runtime using Moq
            var mockProductsRepos = new Mock<IPagesRepository>();
            mockProductsRepos.Setup(x => x.Pages).Returns(pages.AsQueryable());

            var englishPage = pages.Where(p => p.Language == "en" && p.Path == "A").FirstOrDefault();
            mockProductsRepos.Setup(x => x.GetPage("/A", "en")).Returns(englishPage);

            var spanishPage = pages.Where(p => p.Language == "es" && p.Path == "A").FirstOrDefault();
            mockProductsRepos.Setup(x => x.GetPage("/A", "es")).Returns(spanishPage);

            var notExistSpanishPage = pages.Where(p => p.Language == "en" && p.Path == "B").FirstOrDefault();
            mockProductsRepos.Setup(x => x.GetPage("/B", "es")).Returns(notExistSpanishPage);

            return mockProductsRepos.Object;
        }

        public static void ShouldBeRedirectionTo(this ActionResult actionResult, object expectedRouteValues)
        {
            var actualValues = ((RedirectToRouteResult)actionResult).RouteValues;
            var expectedValues = new RouteValueDictionary(expectedRouteValues);
            foreach (string key in expectedValues.Keys)
                actualValues[key].ShouldEqual(expectedValues[key]);
        }
    }
}
