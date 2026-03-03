using Submission.API;
using Submission.API.Endpoints;
using Submission.Application;
using Submission.Persistence;

var builder = WebApplication.CreateBuilder(args);

#region Add Services
builder.Services
    .AddApiServices(builder.Configuration)
    .AddApplicationServices(builder.Configuration)
    .AddPersistenceServices(builder.Configuration);
#endregion

var app = builder.Build();

#region Use Services
app
    .UseSwagger()
    .UseSwaggerUI()
    .UseRouting();   // match the HTTP request to an endpoint based on the url

app.MapAllEndpoints();
// TODO - Create first migration

if (app.Environment.IsDevelopment())
{
   // TODO - seed test data
}
#endregion

app.Run();
