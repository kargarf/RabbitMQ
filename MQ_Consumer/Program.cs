
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory();
factory.Uri = new Uri("amqps://fjnesknx:g9B0IaNL03U57MLrHGlp3l0U-Boebcq9@hawk.rmq.cloudamqp.com/fjnesknx");//from rabbitmq cloud (https://www.cloudamqp.com/)

using var connection = factory.CreateConnection();

var channel = connection.CreateModel();

channel.QueueDeclare("myqueue", true, false, false);

var consumer = new EventingBasicConsumer(channel);

channel.BasicConsume("myqueue", true, consumer);

consumer.Received += Consumer_Received;

void Consumer_Received(object? sender, BasicDeliverEventArgs e)
{
    Console.WriteLine("Msj: " + Encoding.UTF8.GetString(e.Body.ToArray()));
}

Console.ReadLine();
