using System;
using System.Collections.Generic;
using System.Linq;

namespace Collection
{
    public class State
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Capitol { get; set; }
        public int Population { get; set; }
        public State(string code, string name, string capitol, int population)
        {
            Code = code;
            Name = name;
            Capitol = capitol;
            Population = population;
        }
        private static State[] states = {
            new State("VA", "Virginia", "Richmond", 8535519),
            new State("AL", "Alabama", "Montgomery", 4903185),
            new State("MS", "Mississippi", "Jackson", 2976149),
            new State("IL", "Illinois", "Springfield", 12671821),
            new State("TX", "Texas", "Austin", 28995881),
            new State("NC", "North Carolina", "Raleigh", 10488084),
            new State("UT", "Utah", "Salt Lake City", 3205958),
            new State("PA", "Pennsylvania", "Harrisburg", 12801989)
        };
        public static State[] States
        {
            get
            {
                return states;
            }
        }

        public static List<State> StateList
        {
            get
            {
                return new List<State>(states);
            }
        }

        public static SortedDictionary<string, string> StatesDictionary
        {
            get
            {
                var dict = new SortedDictionary<string, string>();
                foreach (var state in states)
                {
                    dict.Add(state.Code, state.Name);
                }
                return dict;
            }
        }

        public static SortedList<string, State> SortedStates
        {
            get
            {
                var sortedList = new SortedList<string, State>();
                foreach (var state in states)
                {
                    sortedList.Add(state.Code, state);
                }
                return sortedList;
            }
        }

        public static List<int> StatePops
        {
            get
            {
                return states.Select(s => s.Population).ToList();
            }
        }

        public static int[] Populations
        {
            get
            {
                int len = states.Length;
                int[] pops = new int[len];
                for (int i = 0; i < len; i++)
                {
                    pops[i] = states[i].Population;
                }
                return pops;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create a new State object
            State newState = new State("NY", "New York", "Albany", 19453561);

            // List<State>
            Console.WriteLine("--- List<State> ---");
            List<State> stateList = State.StateList;
            stateList.Add(newState);

            Console.WriteLine("States in List:");
            foreach (var state in stateList)
            {
                Console.WriteLine($"{state.Code} - {state.Name}");
            }

            stateList.Remove(newState);
            Console.WriteLine("\nState removed from List.");

            // Dictionary<string, string>
            Console.WriteLine("\n--- Dictionary<string, string> ---");
            SortedDictionary<string, string> statesDictionary = State.StatesDictionary;
            statesDictionary.Add(newState.Code, newState.Name);

            Console.WriteLine("\nStates in Dictionary:");
            foreach (var kvp in statesDictionary)
            {
                Console.WriteLine($"{kvp.Key} - {kvp.Value}");
            }

            statesDictionary.Remove(newState.Code);
            Console.WriteLine("\nState removed from Dictionary.");

            // SortedList<string, State>
            Console.WriteLine("\n--- SortedList<string, State> ---");
            SortedList<string, State> sortedStates = State.SortedStates;
            sortedStates.Add(newState.Code, newState);

            Console.WriteLine("\nStates in SortedList:");
            foreach (var kvp in sortedStates)
            {
                Console.WriteLine($"{kvp.Key} - {kvp.Value.Name}");
            }

            sortedStates.Remove(newState.Code);
            Console.WriteLine("\nState removed from SortedList.");

            // List<int>
            Console.WriteLine("\n--- List<int> ---");
            List<int> statePops = State.StatePops;
            statePops.Add(newState.Population);

            Console.WriteLine("\nSum of State Populations: " + statePops.Sum());
        }
    }
}
