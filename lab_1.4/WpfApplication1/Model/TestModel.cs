using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;



namespace TestApp.Model
{
    [Serializable]
    public class TestModel : MyList<Question>, IEnumerable<Question>, INotifyCollectionChanged, ISerializable
    {
        private TestModel( SerializationInfo info, StreamingContext context )        
        {
            this.Name = info.GetString("Name");
        
            int count = info.GetInt32("NumOfQuestion");  
            for (int iy = 0; iy < count; iy++)
            {
                string key = "Question_" + iy.ToString();        
                Question question = (Question)info.GetValue(key, typeof(Question));       
                this.Add(question);       
            }
        }
        
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]       
        public void GetObjectData( SerializationInfo info, StreamingContext context )
        {
            info.AddValue("Name", this.Name);
                                             
            info.AddValue("NumOfQuestion", this.Count);       
            int iy = 0;       
            foreach (Question question in this)       
            {        
                string key = "Question_" + iy.ToString();        
                info.AddValue(key, question, typeof(Question));       
                iy++;        
            }        
        }    
              
        const string dataPath = @"Res/data.dat";
        const string gzipPath = @"Res/data.gzip";
        const string textPath = @"Res/Collection.doc";
        const string queuryPath = @"Res/QueuryResult.doc";
        const string Path = @"Res/";

        public string Name { get; set; }

        public TestModel()
        {
            this.Name = "Общие вопросы";
        }

        public TestModel(string name)
        {
            this.Name = name;

            Add(new Question("Пишется \"A\" на месте пропуска в словах"));
            this[0].Answers.Add(new Answer("несг...раемый"));
            this[0].Answers.Add(new Answer("з...рница", true));
            this[0].Answers.Add(new Answer("раск...ленный", true));
            this[0].Answers.Add(new Answer("подр...стковый"));
            this[0].Answers.Add(new Answer("ст...рожил", true));


            Add(new Question("Пишется \"Е\" на месте всех пропусков в рядах:"));
            this[1].Answers.Add(new Answer("пос..деть за столом; сл..паться в комок"));
            this[1].Answers.Add(new Answer("пр..кращение споров; скр..пить подписью", true));
            this[1].Answers.Add(new Answer("пр...стегнуть ремень;  ув...дший от жары"));
            this[1].Answers.Add(new Answer("пр..стрелять ружье; см..рение"));
            this[1].Answers.Add(new Answer("оскв...рнить память; разв...вается стяг", true));


            Add(new Question("Двойные согласные пишутся в словах:"));
            this[2].Answers.Add(new Answer("ки(л,лл)ер", true));
            this[2].Answers.Add(new Answer("о(к/кк)упация", true));
            this[2].Answers.Add(new Answer("ра(с,сс)четливый человек"));
            this[2].Answers.Add(new Answer("мирово(з/зз)рение", true));
            this[2].Answers.Add(new Answer("заветре(н/нн)ый", true));


            Add(new Question("Пишется «О» на месте пропуска в словах:"));
            this[3].Answers.Add(new Answer("крюч…к", true));
            this[3].Answers.Add(new Answer("ш...мпольный", true));
            this[3].Answers.Add(new Answer("возмещ...нный"));
            this[3].Answers.Add(new Answer("бельч..нок", true));
            this[3].Answers.Add(new Answer("еж..вые рукавицы", true));


            Add(new Question("Пишется мягкий знак на месте пропуска:"));
            this[4].Answers.Add(new Answer("лесная глуш..", true));
            this[4].Answers.Add(new Answer("отвлеч.. внимание", true));
            this[4].Answers.Add(new Answer("пять краж.."));
            this[4].Answers.Add(new Answer("монтаж.."));
            this[4].Answers.Add(new Answer("сверх..яростный"));
        }
        public event NotifyCollectionChangedEventHandler CollectionChanged;
        #region class TestEnumeraror
        private sealed class TestEnumerator : IEnumerator<Question>
        {


            public int curpos = -1;
            private MyList<Question> questions;

            public TestEnumerator(MyList<Question> questions)
            {
                this.questions = questions;

            }

            public Question Current
            {
                get { return questions[curpos]; }
            }

            public bool MoveNext()
            {
                if (curpos < questions.Count - 1)
                {
                    curpos++;
                    return true;
                }
                return false;
            }

            public void Reset()
            {
                curpos = -1;
            }

            public void Dispose()
            {
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }
        }
        #endregion
        #region IEnumerator
        public override IEnumerator<Question> GetEnumerator()
        {
            return new TestEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
        #region methods Add, Remove
        public override void Add(Question q)
        {
            base.Add(q);

            if (CollectionChanged != null)
            {
                CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, q));
            }
        }

        public override bool Remove(Question q)
        {
            base.RemoveAt(this.IndexOf(q));
            if (CollectionChanged != null)
            {
                //CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, q));
                CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset)); //remove не пашет сучка
            }
            return true;
        }
        public override void RemoveAt(int Index)
        {
            base.RemoveAt(Index);
            if (CollectionChanged != null)
            {
                CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }
        public override void Clear()
        {
            base.Clear();
            if (CollectionChanged != null)
            {
                CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }
        public int FindIndex(Question q)
        {
            return this.IndexOf(q);
        }
        #endregion

        public void ReadFromFile()
        {
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }
            using (FileStream input = File.Open(gzipPath, FileMode.OpenOrCreate))
            using (BinaryWriter bw = new BinaryWriter(File.Create(dataPath)))
            using (GZipStream gzip = new GZipStream(input, CompressionMode.Decompress))
            {
                gzip.CopyTo(bw.BaseStream);
            }

            using (BinaryReader br = new BinaryReader(File.Open(dataPath, FileMode.OpenOrCreate)))
            {           
                int qCount = 0;
                while (br.PeekChar() != -1)
                {
                    this.Add(new Question(br.ReadString()));
                    int count = br.ReadInt32();
                    for (int i = 0; i < count; i++)
                    {
                        this[qCount].Answers.Add(new Answer(br.ReadString(),br.ReadBoolean()));
                    }
                    this[qCount].SuccessStats = br.ReadInt32();
                    this[qCount].FailStats = br.ReadInt32();
                    qCount++;
                }
            }
        }

        public void SaveToFile()
        {
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }
            using (BinaryWriter bw = new BinaryWriter(File.Open(dataPath, FileMode.Create)))
            {          
                foreach (Question q in this)
                {
                    if (q != null)
                    {
                        bw.Write(q.Text);
                        bw.Write(q.Answers.Count);
                        foreach (Answer a in q)
                        {
                            bw.Write(a.Text);
                            bw.Write(a.Correct);
                        }
                        bw.Write(q.SuccessStats);
                        bw.Write(q.FailStats);
                    }
                }
            }

            using (FileStream output = File.Create(gzipPath))
            {
                using (GZipStream gzip = new GZipStream(output, CompressionMode.Compress))
                using (BinaryWriter bw = new BinaryWriter(File.Open(dataPath, FileMode.OpenOrCreate)))
                {
                    bw.BaseStream.CopyTo(gzip);
                }
            }

            using (StreamWriter writer = File.CreateText(textPath))
            {
                writer.WriteLine(Name);
                writer.WriteLine();
                int j = 0;
                foreach (var q in this)
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
       
        public void SaveToFile(IEnumerable<Question> test)
        {
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }
                      
            using (StreamWriter writer = File.CreateText(queuryPath))
            {
                writer.WriteLine(this.Name);
                writer.WriteLine();
                int j = 0;
                foreach (var q in test)
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
    }
}
