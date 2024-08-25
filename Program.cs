using RestSharp;
using System;

DotNetEnv.Env.Load();
DotNetEnv.Env.TraversePath().Load();

Console.WriteLine("Enter subject:");
string _subject = System.Console.ReadLine();
Console.WriteLine("Enter name:");
string _name = System.Console.ReadLine();
Console.WriteLine("Enter email:");
string _email = System.Console.ReadLine();
Console.WriteLine("Enter body:");
string _body = System.Console.ReadLine();
var client = new RestSharp.RestClient("https://api.us.nylas.com/v3/grants/" + Environment.GetEnvironmentVariable("NYLAS_GRANT_ID") + "/messages/send");
client.Timeout = -1;
var request = new RestSharp.RestRequest(Method.POST);
request.AddHeader("Content-Type", "application/json");
request.AddHeader("Authorization", "Bearer " + System.Environment.GetEnvironmentVariable("NYLAS_API_KEY"));

var body = @"{"
    + "\n"
    + $@"    ""body"" : ""{_body}"","
    + "\n"
    + $@"    ""subject"": ""{_subject}"","
    + "\n"
    + $@"    ""to"": ["
    + "\n"
    + @"        {"
    + "\n"
    + $@"            ""name"": ""{_name}"","
    + "\n"
    + $@"            ""email"": ""{_email}"""
    + "\n"
    + @"        }"
    + "\n"
    + @"    ]"
    + "\n"
    + @"}";
request.AddParameter("application/json", body, ParameterType.RequestBody);
IRestResponse response = client.Execute(request);
Console.WriteLine(response.Content);
Console.Read();
