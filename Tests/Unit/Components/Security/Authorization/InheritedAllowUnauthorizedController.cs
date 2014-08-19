﻿using System.Web.Mvc;

namespace MvcTemplate.Tests.Unit.Components.Security
{
    public class InheritedAllowUnauthorizedController : AllowUnauthorizedController
    {
        [HttpGet]
        public ActionResult GetAction()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NonGetAction()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public override ActionResult AllowAnonymousGetAction()
        {
            return base.AllowAnonymousGetAction();
        }
    }
}
