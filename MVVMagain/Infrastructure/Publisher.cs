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
        private static volatile Dictionary<string, IList<object>> container = new Dictionary<string, IList<object>>();
        private static Publisher instance = new Publisher();

        private Publisher()
        {
        }

        public static Publisher GetInstance()
        {
                return instance;
        }

        public static void Subscribe<T>(string eventName, Action<T> action)
        {
            IList<object> listOfActions;
            if (!container.TryGetValue(eventName, out listOfActions))
            {
                listOfActions = new List<object>();
                container[eventName] = listOfActions;
            }
            listOfActions.Add(action);
        }

        public static void Publish<T>(string eventName, T iGame)
        {
            IList<object> listOfActions;
            if (container.TryGetValue(eventName, out listOfActions))
            {
                foreach (object o in listOfActions)
                {
                    (o as Action<T>).Invoke(iGame);
                }
            }
        }
    }
}
