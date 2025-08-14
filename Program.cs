using GameStore.Endpoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// All endpoints here are grouped under the "/games" prefix.
// - 'app' is a WebApplication (it implements IEndpointRouteBuilder).
// - app.MapGroup("/games") returns a RouteGroupBuilder (the route group for "/games").
// - Each call inside the group (GamesGroup.MapGet/MapPost/MapPut/MapDelete) returns a RouteHandlerBuilder
//   which represents and configures a single endpoint.
// - You can write extension methods for IEndpointRouteBuilder (static method with `this IEndpointRouteBuilder builder`).
//   A typical extension creates a RouteGroupBuilder, registers all related endpoints, and returns the group.
//   This keeps Program.cs tidy and lets you reuse or further configure the returned RouteGroupBuilder.
//  See the EndPoints Folder -> GamesEndpoints.cs

app.MapGamesEndpoints();

app.Run();
