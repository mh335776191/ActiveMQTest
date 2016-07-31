using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apache.NMS;
using Apache.NMS.ActiveMQ;

namespace MQRead
{
    class Program
    {
        static void Main(string[] args)
        {         //创建连接工厂
            IConnectionFactory factory = new ConnectionFactory("tcp://localhost:61616");
            //通过工厂构建连接
            IConnection connection = factory.CreateConnection();
            //这个是连接的客户端名称标识
            connection.ClientId = "firstQueueListener" + DateTime.Now;//可以起多个进程，只要clientid不一样就行
            //启动连接，监听的话要主动启动连接
            connection.Start();
            //通过连接创建一个会话
            ISession session = connection.CreateSession();
            //通过会话创建一个消费者，这里就是Queue这种会话类型的监听参数设置
            IMessageConsumer consumer = session.CreateConsumer(new Apache.NMS.ActiveMQ.Commands.ActiveMQQueue("firstQueue"));

            while (true)
            {
                IMessage msg = consumer.Receive();
                Console.WriteLine("读取到信息:" + ((ITextMessage)msg).Text);
            }
            //注册监听事件             

            Console.ReadKey();
        }
    }
}
