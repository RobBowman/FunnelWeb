﻿using System.Web;
using System.Web.Mvc;
using FunnelWeb.Model.Repositories;
using FunnelWeb.Web.Areas.Admin.Controllers;
using NSubstitute;
using NUnit.Framework;

namespace FunnelWeb.Tests.Web.Areas.Admin.Controllers
{
    [TestFixture]
    public class AdminControllerTests
    {
        protected AdminController Controller { get; set; }
        protected IAdminRepository AdminRepository { get; set; }

        [SetUp]
        public void SetUp()
        {
            Controller = new AdminController
                             {
                                 AdminRepository = AdminRepository = Substitute.For<IAdminRepository>(),
                                 ControllerContext = CreateControllerContext()
                             };
        }

        [Test]
        public void Index()
        {
            var result = (ViewResult)Controller.Index();
            
            Assert.That(result.ViewName, Is.EqualTo(string.Empty));
        }

        [Test]
        public void DeleteComment()
        {
            var result = (RedirectToRouteResult)Controller.DeleteComment(0);

            Assert.That(result.RouteValues["Action"], Is.EqualTo("Comments"));
        }

        [Test]
        public void DeleteAllSpam()
        {
            var result = (RedirectToRouteResult)Controller.DeleteAllSpam();

            Assert.That(result.RouteValues["Action"], Is.EqualTo("Comments"));
        }

        [Test]
        public void DeletePingback()
        {
            var result = (RedirectToRouteResult)Controller.DeletePingback(0);

            Assert.That(result.RouteValues["Action"], Is.EqualTo("Pingbacks"));
        }

        [Test]
        public void ToggleSpam()
        {
            var result = (RedirectToRouteResult)Controller.ToggleSpam(0);

            Assert.That(result.RouteValues["Action"], Is.EqualTo("Comments"));
        }

        [Test]
        public void ToggePingbackSpam()
        {
            var result = (RedirectToRouteResult)Controller.TogglePingbackSpam(0);

            Assert.That(result.RouteValues["Action"], Is.EqualTo("Pingbacks"));
        }

        private static ControllerContext CreateControllerContext()
        {
            var controllerContext = new ControllerContext();
            var httpContext = Substitute.For<HttpContextBase>();
            var httpServer = Substitute.For<HttpServerUtilityBase>();
            httpServer.MapPath(Arg.Any<string>()).Returns(@"C:\Windows");
            httpContext.Server.Returns(httpServer);
            controllerContext.HttpContext = httpContext;
            return controllerContext;
        }
    }
}
