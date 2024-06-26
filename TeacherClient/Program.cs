using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

class Program
{
    static async Task Main(string[] args)
    {
        var connection = new HubConnectionBuilder()
            .WithUrl("https://apigessignrecette-c5e974013fbd.herokuapp.com/presenceHub")
            .Build();
        
        connection.Closed += async (error) =>
        {
            Console.WriteLine("Connection closed. Trying to reconnect...");
            await Task.Delay(new Random().Next(0, 5) * 1000);
            await connection.StartAsync();
        };

        connection.On<string>("ReceiveCode", (code) =>
        {
            Console.WriteLine($"Received code: {code}");
        });

        await connection.StartAsync();

        Console.WriteLine("Connected to the hub.");
        Console.WriteLine("Joining room...");
        await connection.InvokeAsync("JoinRoom", 1); // Replace 1 with the actual subjectHourId

        Console.WriteLine("Press Enter to exit.");
        Console.ReadLine();

       
    }
}
