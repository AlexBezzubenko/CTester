using System;
using System.Linq;
using TestApp.Model;
using System.Windows;
using TestApp.View;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
 

namespace TestApp.Model
{
    public class QueuryMethods
    {
        private TestModel test;
        private string stringParam;
        private string[] stringParametrs;
        public MyDelegate[] foos = new MyDelegate[6];
        public delegate void MyDelegate();

        public QueuryMethods(TestModel test, string stringParam)
        {
            this.test = test;
            this.stringParam = stringParam;
            stringParam = stringParam.Trim();
            
            stringParam = System.Text.RegularExpressions.Regex.Replace(stringParam, " +", " ");
            stringParametrs = stringParam.Split(' ');

            int i = 1;
            foreach (var q in test)
            {
                q.Index = i++;
            }
            foos[0] = SortQueury;
            foos[1] = FailStatsQueury;
            foos[2] = SuccessStatsQueury;
            foos[3] = GroupQueury;
            foos[4] = Serialize;
            foos[5] = Deserialize;
        }

        public void SortQueury()
        {
            if (this.test != null)
            {
                IEnumerable<Question> Test = null;
                if (stringParametrs[0] == "<")
                {
                    Test = this.test.OrderByDescending(question => question.Answers.Count);
                }
                else
                {
                    Test = this.test.OrderBy(question => question.Answers.Count);
                }

                if (Test != null)
                {

                    TestModel t = new TestModel();
                    foreach (Question q in Test)
                    {
                        t.Add(q);
                    }
                    this.test.Clear();
                    foreach (Question q in t)
                    {
                        test.Add(q);
                    }
                    MessageBox.Show("Sorted");
                }
            }
        }
        public void FailStatsQueury()
        {
            UpdateIndex();
            if (stringParametrs.Length < 2 || stringParametrs.Length > 3)
            {
                MessageBox.Show("Check input");
                return;
            }
            if (this.test != null)
            {
                int number;
                bool isInt = Int32.TryParse(stringParametrs[1], out number);

                if ( (stringParametrs[0] != ">" && stringParametrs[0] != "<"
                    && stringParametrs[0] != "=") || !isInt)
                {
                    MessageBox.Show("Check input");
                    return;
                }
                IEnumerable<Question> Test = null;
                              
                if (stringParametrs.Length == 3 && stringParametrs[2] == "%")
                {
                    if (stringParametrs[0] == ">")
                    {
                         Test = from Question question in test
                                where (question.SuccessStats + question.FailStats != 0) 
                                && ( (double)question.FailStats / ((double)question.SuccessStats + (double)question.FailStats) * 100
                                   > number)
                                   select question;
                        
                    }
                    if (stringParametrs[0] == "<")
                    {
                         Test = from Question question in test
                                where (question.SuccessStats + question.FailStats != 0) 
                                && ((double)question.FailStats / ((double)question.SuccessStats + (double)question.FailStats) * 100
                               < number)
                                   select question;
                    }
                    if (stringParametrs[0] == "=")
                    {
                        Test = from Question question in test
                               where (question.SuccessStats + question.FailStats != 0)
                               && ((int)((double)question.FailStats / ((double)question.SuccessStats + (double)question.FailStats) * 100)
                              == number)
                               select question;
                    }
                }
                if (stringParametrs.Length == 2)
                {
                    if (stringParametrs[0] == ">")
                    {
                        Test = from Question question in test
                               where question.FailStats > number
                               select question;
                    }
                    if (stringParametrs[0] == "<")
                    {
                        Test = from Question question in test
                               where question.FailStats < number
                               select question;
                    }
                    if (stringParametrs[0] == "=")
                    {
                        Test = from Question question in test
                               where question.FailStats == number
                               select question;
                    }
                }

                if (Test != null)
                {                   
                    ChartWindowView chartWindow = new ChartWindowView(Test, 0);
                    chartWindow.ShowDialog();
                }                
            }
        }
        public void SuccessStatsQueury()
        {
            UpdateIndex();
            if (this.test != null)
            {
                int number;
                bool isInt = Int32.TryParse(stringParametrs[1], out number);

                if (stringParametrs.Length > 3 || (stringParametrs[0] != ">" && stringParametrs[0] != "<"
                    && stringParametrs[0] != "=") || !isInt)
                {
                    MessageBox.Show("Check input");
                    return;
                }
                IEnumerable<Question> Test = null;

                if (stringParametrs.Length == 3 && stringParametrs[2] == "%")
                {
                    if (stringParametrs[0] == ">")
                    {
                        Test = from Question question in test
                               where (question.SuccessStats + question.FailStats != 0)
                               && ((double)question.SuccessStats / ((double)question.SuccessStats + (double)question.FailStats) * 100
                                  > number)
                               select question;
                    }
                    if (stringParametrs[0] == "<")
                    {
                        Test = from Question question in test
                               where (question.SuccessStats + question.FailStats != 0)
                               && ((double)question.SuccessStats / ((double)question.SuccessStats + (double)question.FailStats) * 100
                              < number)
                               select question;
                    }
                    if (stringParametrs[0] == "=")
                    {
                        Test = from Question question in test
                               where (question.SuccessStats + question.FailStats != 0)
                               && ((int)((double)question.SuccessStats / ((double)question.SuccessStats + (double)question.FailStats) * 100)
                              == number)
                               select question;
                    }
                }
                if (stringParametrs.Length == 2)
                {
                    if (stringParametrs[0] == ">")
                    {
                        Test = from Question question in test
                               where question.SuccessStats > number
                               select question;
                    }
                    if (stringParametrs[0] == "<")
                    {
                        Test = from Question question in test
                               where question.SuccessStats < number
                               select question;
                    }
                    if (stringParametrs[0] == "=")
                    {
                        Test = from Question question in test
                               where question.SuccessStats == number
                               select question;
                    }
                }

                if (Test != null)
                {
                    ChartWindowView chartWindow = new ChartWindowView(Test, 1);
                    chartWindow.ShowDialog();
                }
            }
        }

        public void GroupQueury()
        {
            if (this.test != null)
            {             
                var Test = test.GroupBy(question => question.FailStats);
                
                const string queuryPath = @"Res/QueuryResult.doc";
                const string Path = @"Res/";
                if (!Directory.Exists(Path))
                {
                    Directory.CreateDirectory(Path);
                }
                using (StreamWriter writer = File.CreateText(queuryPath))
                {                    
                    writer.WriteLine("Queury: group by FailStats");
                    writer.WriteLine();
                    int j = 0;
                    foreach (IGrouping<int, Question> group in Test)
                    {
                        writer.WriteLine("FailStats count: {0}",group.Key.ToString());
                        foreach (Question q in group)
                        {
                            writer.WriteLine("{0}. " + q.Text, ++j);
                            int i = 0;
                            foreach (var a in q)
                            {
                                writer.WriteLine("\t" + "{0}) " + a.Text, ++i);
                            }
                            writer.WriteLine();
                        }
                    }                   
                }
                MessageBox.Show("Grouped");         
            }
        }
          
        public void UpdateIndex()
        {
            int id = 0;
            foreach (var q in this.test)
            {
                id++;
                q.Index = id;
            }
        }

        
        const string serializerPath = @"Res/serialize.dat";
        const string Path = @"Res/";

        public void Serialize()
        {
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }
            FileSerializer.Serialize(serializerPath, this.test);
            MessageBox.Show("Serialized");
        }

        public void Deserialize()
        {
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }
            
            IEnumerable<Question> Test = FileSerializer.Deserialize<TestModel>(serializerPath);             
            TestModel t = new TestModel();
            foreach (Question q in Test)
            {
                t.Add(q);
            }
            this.test.Clear();
            foreach (Question q in t)
            {
                 test.Add(q);
            }
            MessageBox.Show("Deserialized");
         }                  
    }
}
