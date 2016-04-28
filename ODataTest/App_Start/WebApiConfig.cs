using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using ODataTest.Models;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;

namespace ODataTest
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务


            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            ODataModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Product>("Products");

            builder.Namespace = "ODataTest";
            builder.EntityType<Product>()
                .Action("Rate")
                .Parameter<int>("Rating");

            builder.Namespace = "ODataTest";
            builder.EntityType<Product>().Collection
                .Function("MostExpensive")
                .Returns<double>();

            builder.Function("GetSalesTaxRate")
                .Returns<double>()
                .Parameter<int>("PostalCode");


            builder.EntitySet<Supplier>("Suppliers");
            config.MapODataServiceRoute(
                routeName: "ODataRoute",
                routePrefix: null,
                model: builder.GetEdmModel()
                );

        }
    }
}
