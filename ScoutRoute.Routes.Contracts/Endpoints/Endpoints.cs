namespace ScoutRoute.Routes.Contracts.Endpoints
{
    public static class Endpoints
    {
        public const string Base = "/projects";

        public static class Projects
        {
            public const string Base = Endpoints.Base;

            public const string CreateProject = Base;
            public const string UpdateProject = $"{Base}/{{projectId}}";
            public const string DeleteProject = $"{Base}/{{projectId}}";

            public const string GetAll = Base;
            public const string Get = $"{Base}/{{projectId}}";

        }

        public static class Routes
        {
            public const string Base = $"{Endpoints.Base}/{{projectId}}/routes";

            public const string CreateRoute = Base;
            public const string UpdateRoute = $"{Base}/{{routeId}}";
            public const string DeleteRoute = $"{Base}/{{routeId}}";

            public const string AssignTeam = $"{Base}/{{routeId}}/team";
            public const string UnassignTeam = $"{Base}/{{routeId}}/team";

            public const string AddStop = $"{Base}/{{routeId}}/stops";
            public const string RemoveStop = $"{Base}/{{routeId}}/stops/{{stopId}}";

            public const string GetAll = Base;
            public const string Get = $"{Base}/{{routeId}}";
            public const string GetVacant = $"{Base}/vacant";
        }

        public static class Stops
        {
            public const string Base = $"{Endpoints.Base}/{{projectId}}/stops";

            public const string CreateStop = Base;
            public const string CompleteStop = $"{Base}/{{stopId}}/completed";
            public const string DeleteStop = $"{Base}/{{stopId}}";

            public const string AddComment = $"{Base}/{{stopId}}/comments";

            public const string GetAll = Base;
            public const string GetUnassigned = $"{Base}/unassigned";
        }
    }
}
