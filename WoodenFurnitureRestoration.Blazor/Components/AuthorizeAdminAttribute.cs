using System;

namespace WoodenFurnitureRestoration.Blazor.Components
{
    /// <summary>
    /// Admin sayfaları için authorization attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class AuthorizeAdminAttribute : Attribute
    {
    }
}