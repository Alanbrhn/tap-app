using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication1.Services
{
    public interface IKafkaService
    {
        Task SendMessageAsync(string key, string value);
        Task<List<string>> GetMessagesAsync(); 
    }

    public class KafkaService : IKafkaService
    {
        private readonly string _bootstrapServers;
        private readonly string _topic;
        private readonly string _consumerGroup;
        private readonly string _saslUsername;
        private readonly string _saslPassword;

        public KafkaService(IConfiguration configuration)
        {
            _bootstrapServers = configuration["Kafka:BootstrapServers"];
            _topic = configuration["Kafka:Topic"];
            _consumerGroup = configuration["Kafka:ConsumerGroup"];
            _saslUsername = configuration["Kafka:SaslUsername"];
            _saslPassword = configuration["Kafka:SaslPassword"];
        }

      
        public async Task SendMessageAsync(string key, string value)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = _bootstrapServers,
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SaslMechanism = SaslMechanism.Plain,
                SaslUsername = _saslUsername,
                SaslPassword = _saslPassword
            };

            using var producer = new ProducerBuilder<string, string>(config).Build();
            var message = new Message<string, string> { Key = key, Value = value };

            try
            {
                var deliveryResult = await producer.ProduceAsync(_topic, message);
                Console.WriteLine($"Pesan terkirim: {deliveryResult.Value}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

       
        public async Task<List<string>> GetMessagesAsync()
        {
            var messages = new List<string>();

            var config = new ConsumerConfig
            {
                BootstrapServers = _bootstrapServers,
                GroupId = _consumerGroup,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SaslMechanism = SaslMechanism.Plain,
                SaslUsername = _saslUsername,
                SaslPassword = _saslPassword
            };

            using var consumer = new ConsumerBuilder<string, string>(config).Build();
            consumer.Subscribe(_topic);

            try
            {
               
                var consumeResult = consumer.Consume(TimeSpan.FromSeconds(10)); 
                if (consumeResult != null)
                {
                    messages.Add($"{consumeResult.Message.Key}: {consumeResult.Message.Value}");
                }
            }
            catch (ConsumeException ex)
            {
                Console.WriteLine($"Error: {ex.Error.Reason}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return messages;
        }
    }
}
