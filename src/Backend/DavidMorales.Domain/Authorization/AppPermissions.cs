namespace DavidMorales.Domain.Authorization
{
    public class AppPermissions
    {
        public static class Document
        {
            public const string Query = "document.query";
            public const string Add = "document.add";
            public const string View = "document.view";
            public const string Edit = "document.edit";
        }

        public static class Person
        {
            public const string Query = "person.query";
            public const string Add = "person.add";
            public const string View = "person.view";
            public const string Edit = "person.edit";
        }

        public static class Company
        {
            public const string Query = "company.query";
            public const string Add = "company.add";
            public const string View = "company.view";
            public const string Edit = "company.edit";
        }

        public static class User
        {
            public const string Query = "user.query";
            public const string Add = "user.add";
            public const string View = "user.view";
            public const string Edit = "user.edit";
        }

        public static class LogDataChange
        {
            public const string Query = "log.query";
        }
    }
}
