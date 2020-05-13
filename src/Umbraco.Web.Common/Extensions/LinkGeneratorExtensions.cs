﻿using System;
using Umbraco.Core;
using Microsoft.AspNetCore.Routing;
using System.Reflection;
using Umbraco.Web.Common.Install;

namespace Umbraco.Extensions
{
    public static class LinkGeneratorExtensions
    {
        /// <summary>
        /// Return the back office url if the back office is installed
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetBackOfficeUrl(this LinkGenerator linkGenerator)
        {

            Type backOfficeControllerType;
            try
            {
                backOfficeControllerType = Assembly.Load("Umbraco.Web.BackOffice")?.GetType("Umbraco.Web.BackOffice.Controllers.BackOfficeController");
                if (backOfficeControllerType == null) return "/"; // this would indicate that the installer is installed without the back office
            }
            catch (Exception)
            {
                return "/"; // this would indicate that the installer is installed without the back office
            }

            return linkGenerator.GetPathByAction("Default", ControllerExtensions.GetControllerName(backOfficeControllerType), new { area = Constants.Web.Mvc.BackOfficeArea });
        }

        /// <summary>
        /// Returns the URL for the installer
        /// </summary>
        /// <param name="linkGenerator"></param>
        /// <returns></returns>
        public static string GetInstallerUrl(this LinkGenerator linkGenerator)
        {
            return linkGenerator.GetPathByAction("Index", ControllerExtensions.GetControllerName<InstallController>(), new { area = Constants.Web.Mvc.InstallArea });
        }
    }
}