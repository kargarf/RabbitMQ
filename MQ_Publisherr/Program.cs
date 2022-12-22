
using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory();
factory.Uri = new Uri("amqps://fjnesknx:g9B0IaNL03U57MLrHGlp3l0U-Boebcq9@hawk.rmq.cloudamqp.com/fjnesknx"); //from rabbitmq cloud

using var connection = factory.CreateConnection();

var channel = connection.CreateModel();

channel.QueueDeclare("myqueue", true, false, false);

const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
string strrandom = new string(Enumerable.Repeat(chars, 6).Select(s => s[Random.Shared.Next(s.Length)]).ToArray());

var msg = strrandom + "_" + DateTime.Now.ToString("yyyyMMddTHH:mm:ss");
var body = Encoding.UTF8.GetBytes(msg);

channel.BasicPublish("", "myqueue", null, body);

Console.WriteLine($"Message Published! {msg}");
Console.ReadLine();
