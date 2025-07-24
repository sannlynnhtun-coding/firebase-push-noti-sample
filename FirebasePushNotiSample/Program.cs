using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromFile("serviceAccountKey.json")
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/api/notifications/send-to-device", async ([FromBody] NotificationRequest request) =>
{
    var message = new Message()
    {
        Notification = new Notification
        {
            Title = request.Title,
            Body = request.Body
        },
        Token = request.Token,
    };

    try
    {
        string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
        Console.WriteLine("Successfully sent message: " + response);
        return Results.Ok(new { message = "Notification sent successfully!", response });
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error sending message: " + ex.Message);
        return Results.Problem(ex.Message, statusCode: 500);
    }
});

app.MapPost("/api/notifications/send-to-topic", async ([FromBody] TopicRequest request) =>
{
    var message = new Message()
    {
        Notification = new Notification
        {
            Title = request.Title,
            Body = request.Body
        },
        Topic = request.Topic
    };

    try
    {
        string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
        Console.WriteLine("Successfully sent message to topic: " + response);
        return Results.Ok(new { message = "Notification sent to topic successfully!", response });
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error sending message to topic: " + ex.Message);
        return Results.Problem(ex.Message, statusCode: 500);
    }
});

app.Run();

public class NotificationRequest
{
    public string Token { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
}

public class TopicRequest
{
    public string Topic { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
}
