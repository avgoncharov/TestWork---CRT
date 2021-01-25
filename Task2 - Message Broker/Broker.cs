using System.Collections.Generic;
using System;
using System.Linq;

public interface ISubscriber
{
    void Post(string message);
    string GetName();
}

public interface IPublisher
{
    void Subscribe(ISubscriber subscriber);
    void PostForAll(string message);
    void Unsubscribe(ISubscriber subscriber);
}

public class Publisher : IPublisher
{
    private List<ISubscriber> subscribers;

    public Publisher()
    {
        this.subscribers = new List<ISubscriber>();
    }

    public void Subscribe(ISubscriber subscriber)
    {
        this.subscribers.Add(subscriber); Console.WriteLine("Только что подписался " + subscriber.GetName() + "!");
    }

    public void PostForAll(string message)
    {
        foreach (var subscriber in this.subscribers)
            subscriber.Post(message);
    }

    public void Unsubscribe(ISubscriber subscriber)
    {
        this.subscribers.Remove(subscriber); Console.WriteLine("Только что отписался " + subscriber.GetName() + "!");
    }
}

public class Subscriber : ISubscriber
{
    private static List<string> SubscriberNameList = (new string[] { "Михаил Матюшко", "Андрей Смирнов", "Евгений Тихонов" }).ToList();
    string SubscriberName;
    public Subscriber()
    {
        this.SubscriberName = SubscriberNameList[new Random().Next(SubscriberNameList.Count)];
    }

    public void Post(string message)
    {
        Console.WriteLine($"{this.SubscriberName} received: {message}");
    }

    public string GetName()
    {
        string Name = this.SubscriberName; return Name;
    }
}

class Task2
{
    static void Main(string[] args)
    {
        List<string> control = (new string[] { "Name" }).ToList();
        var publisher = new Publisher();

        var subscriber1 = new Subscriber();
        control.Add(subscriber1.GetName());

        var subscriber2 = new Subscriber();
        if (control.Contains(subscriber2.GetName()))
        {
            do
            {
                subscriber2 = new Subscriber();
            }
            while (control.Contains(subscriber2.GetName()));

        }
        control.Add(subscriber2.GetName());

        var subscriber3 = new Subscriber();
        if (control.Contains(subscriber3.GetName()))
        {
            do
            {
                subscriber3 = new Subscriber();
            }
            while (control.Contains(subscriber3.GetName()));

        }
        control.Add(subscriber3.GetName());
        publisher.PostForAll("Никто не видит это сообщение, так как пока что никто не подписался!");
        //Подписываются первый и второй человек
        publisher.Subscribe(subscriber1);
        publisher.Subscribe(subscriber2);
        //Постим для всех сабов сообщение
        publisher.PostForAll($"Hello! -- Это сообщение видят {subscriber1.GetName()} и {subscriber2.GetName()}, так как они теперь подписчики!");
        //Подписывается третий человек и отписывается первый
        publisher.Subscribe(subscriber3);
        publisher.Unsubscribe(subscriber1);
        //Постим сообщение для всех текущих подписчиков
        publisher.PostForAll($"Have a nice day? -- {subscriber1.GetName()} только что отписался, поэтому он это сообщение не увидел.");


        Console.ReadLine();
    }
}

