using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVVMagain.Models;
using System.Windows.Input;

namespace MVVMagain.Infrastructure
{
    public class Publisher
    {
       private static  Dictionary<string, ICommand> container;
        private static Publisher instance;

        private Publisher()
        {
            container = new Dictionary<string, ICommand>();
        }

        public Publisher GetInstance()
        {
            if (instance == null)
                instance = new Publisher();
            return instance;
        }

        public static void Subscribe(string eventName, ICommand command)
        {
            if(!container.ContainsKey(eventName))
                container.Add(eventName, command);
        }

        public static ICommand Publish(string eventName, Game game)
        {
            ICommand result;
            if (container.TryGetValue(eventName, out result))
                return result;
            throw new KeyNotFoundException();
        }
    }
}
