using System;
using System.Collections.Generic;

namespace ReactionTest
{
    class TestIntervalGenerator
    {
        private int numberOfTests;

        public List<int> RandomizeIntervals(int testTimeSec)
        {
            List<int> list = new List<int>();
            Random generator = new Random();
            int numberOfTests = testTimeSec / 6;
            int temp;
            bool finished;
            int j = 0;
            int current = 0;
            for (int i = 0; i < numberOfTests; i++)
            {
                temp = generator.Next(3, 10);
                current += temp;

                if (current >= testTimeSec)
                {

                    finished = false;
                    while (!finished)
                    {
                        if (list[j + 1] - list[j] >= 6)
                        {
                            temp = generator.Next(list[j] + 2, list[j + 1] - 2);
                            list.Add(temp);
                            i++;
                        }
                        if (i == numberOfTests)
                        {
                            finished = true;
                        }
                        j++;
                    }
                    QuickSort(list);
                }
                else if (i > 0)
                {
                    list.Add(list[i - 1] + temp);
                }
                else
                {
                    list.Add(temp);
                }
            }
            return list;
        }

        static void QuickSort(List<int> a)
        {
            QuickSort(a, 0, a.Count - 1);
        }

        static void QuickSort(List<int> a, int start, int end)
        {
            if (start >= end)
            {
                return;
            }

            int num = a[start];

            int i = start, j = end;

            while (i < j)
            {
                while (i < j && a[j] > num)
                {
                    j--;
                }

                a[i] = a[j];

                while (i < j && a[i] < num)
                {
                    i++;
                }

                a[j] = a[i];
            }

            a[i] = num;
            QuickSort(a, start, i - 1);
            QuickSort(a, i + 1, end);
        }
    }
}
