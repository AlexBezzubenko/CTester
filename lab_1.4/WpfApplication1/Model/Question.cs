using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace TestApp.Model
{
    [Serializable]
    public class Question : IEnumerable<Answer>, INotifyPropertyChanged, IComparable<Question>, ISerializable
    {      
        private sealed class QuestionEnumerator : IEnumerator<Answer>
        {
            public int curpos = -1;
            private MyList<Answer> answers;

            public Answer Current
            {
                get { return answers[curpos]; }
            }
            public bool MoveNext()
            {
                if (curpos < answers.Count - 1)
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

            public QuestionEnumerator(MyList<Answer> answers)
            {
                this.answers = answers;
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }
        }

        public IEnumerator<Answer> GetEnumerator()
        {
            return new QuestionEnumerator(this.Answers);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public MyList<Answer> Answers = new MyList<Answer>();

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

               
        public Question(string text)
        {
            this._text = text;
        }
        public Question()
        {
            this._text = "----";
        }
        
        public string Text
        {
            get { return this._text; }
            set
            {
                _text = value;
                if (value != this._text)
                {
                    NotifyPropertyChanged("Text");
                }
            }
        }
        private string _text = String.Empty;

        public override string ToString()
        {
            return Text;
        }

        public int FailStats{ get; set;}  
        public int SuccessStats {get; set;}
        public int Index { get; set; }  
        
        public int CompareTo(Question other)
        {
            return this.Answers.Count.CompareTo(other.Answers.Count);
        }
        
        private Question( SerializationInfo info, StreamingContext context )        
        {
            this.Text = info.GetString("QText");
            this.FailStats = info.GetInt32("FailStats");
            this.SuccessStats = info.GetInt32("SuccessStats");

            int count = info.GetInt32("NumOfAnswers");  
            for (int ix = 0; ix < count; ix++)
            {
                string key = "Answer_" + ix.ToString();        
                Answer answer = (Answer)info.GetValue(key, typeof(Answer));       
                this.Answers.Add(answer);       
            }
        }
        
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]       
        public void GetObjectData( SerializationInfo info, StreamingContext context )
        {
            info.AddValue("QText", this.Text);
            info.AddValue("FailStats", this.FailStats);
            info.AddValue("SuccessStats", this.SuccessStats);
                                 
            info.AddValue("NumOfAnswers", Answers.Count);       
            int ix = 0;       
            foreach (Answer answer in this)       
            {        
                string key = "Answer_" + ix.ToString();        
                info.AddValue(key, answer, typeof(Answer));       
                ix++;        
            }        
        }        
    }
}