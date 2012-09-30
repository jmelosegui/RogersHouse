using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RogerHouse.Domain.Abstract;
using RogerHouse.Domain.Entities;
using RogersHouse.WebUI.Controllers;
using RogersHouse.UnitTests;
using System.Collections.Generic;
using Moq;

namespace RogersHouse.Tests.Controllers
{
    [TestClass]
    public class PageControllerTest
    {
        [TestMethod]
        public void Can_View_A_Single_Page_Of_Pages()
        {
            // Arrange
            IPagesRepository repository = UnitTestHelpers.MockPagesRepository(
                new Page {PageId = 1},
                new Page {PageId = 2},
                new Page {PageId = 3},
                new Page {PageId = 4},
                new Page {PageId = 5}
                );

            var controller = new AdminController(repository, null) {PageSize = 3};
            // Act
            var result = controller.Pages(2) as ViewResult;

            // Assert
            if (result != null)
            {
                var displayedPages = (IList<Page>) result.ViewBag.Pages;
                displayedPages.Count.ShouldEqual(2);
                displayedPages[0].PageId.ShouldEqual(4);
                displayedPages[1].PageId.ShouldEqual(5);
            }
        }

        [TestMethod]
        public void Can_Get_a_CMSPage_In_English_If_No_Language_Specified()
        {
            //Arrange
            IPagesRepository repository = UnitTestHelpers.MockPagesRepository(
                new Page {Path = "A", Language = "en"},
                new Page { Path = "A", Language = "es" },
                new Page { Path = "B", Language = "en" }
                );
            var controller = new NavController(repository);
            
            //Act
            var result = controller.Page("A") as ViewResult;

            //Assert
            ((Page)result.Model).Language.ShouldEqual("en");
        }

        [TestMethod]
        public void Can_Get_a_CMSPage_In_Other_Language_But_English()
        {
            //Arrange
            IPagesRepository repository = UnitTestHelpers.MockPagesRepository(
                new Page { Path = "A", Language = "en" },
                new Page { Path = "A", Language = "es" },
                new Page { Path = "B", Language = "en" }
                );
            var controller = new NavController(repository);

            //Act
            var result = controller.Page("A", "es") as ViewResult;

            //Assert
            ((Page)result.Model).Language.ShouldEqual("es");
        }

        [TestMethod]
        public void Can_Get_a_CMSPage_In_English_If_Language_Not_found()
        {
            //Arrange
            IPagesRepository repository = UnitTestHelpers.MockPagesRepository(
                new Page { Path = "A", Language = "en" },
                new Page { Path = "A", Language = "es" },
                new Page { Path = "B", Language = "en" }
                );
            var controller = new NavController(repository);

            //Act
            var result = controller.Page("B", "es") as ViewResult;


            //Assert
            ((Page)result.Model).Language.ShouldEqual("en");
        }

        [TestMethod]
        public void Can_Save_Edited_Page()
        {
            var mockRepository = new Mock<IPagesRepository>();
            var page = new Page { Path = "A", Body = "Content" };

            var result = new AdminController(mockRepository.Object, null).EditPage(page);

            mockRepository.Verify(x =>x.SavePage(page));
            result.ShouldBeRedirectionTo(new {action = "Pages"});
        }

        [TestMethod]
        public void If_Invalid_Data_On_Edit_Page_Redisplay_Default_View()
        {
            //Arrange
            var mockRepository = new Mock<IPagesRepository>();
            var page = new Page{Path = "A", Body = "Content"};
            var controller = new AdminController(mockRepository.Object, null);
            controller.ModelState.AddModelError("Body", "the Body can not be empty.");

            //Act
            var result = controller.EditPage(page);

            //Assert
            ((ViewResult)result).Model.ShouldEqual(page);
        }
    }
}