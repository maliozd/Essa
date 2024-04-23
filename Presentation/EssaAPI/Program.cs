using Application;
using EssaAPI.Responses;
using Infrastructure;
using Infrastructure.Mail;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using Persistence;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddHttpClient();
builder.Services.AddControllers().ConfigureApiBehaviorOptions(opt =>
{
    opt.InvalidModelStateResponseFactory = context =>
    {
        var modelState = context.ModelState;
        var errors = modelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToArray();

        var response = new ApiResponse<object>
        {
            Data = null,
            Errors = errors
        };

        return new BadRequestObjectResult(response);
    };
});
builder.Services.Configure<MailConfiguration>(builder.Configuration.GetSection("Mail"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServices();
builder.Services.RegisterDbContext(builder.Configuration);
builder.Services.AddInfrastructureServices();
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("defaultPolicy", builder => builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
});
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua");
    logging.ResponseHeaders.Add("qwerty");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
    logging.CombineLogs = true;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("defaultPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



app.Run();
